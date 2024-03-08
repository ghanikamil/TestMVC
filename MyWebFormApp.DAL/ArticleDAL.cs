using Dapper;
using MyWebFormApp.BO;
using MyWebFormApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace MyWebFormApp.DAL
{
    public class ArticleDAL : IArticleDAL
    {
        private string GetConnectionString()
        {
            //return @"Data Source=ACTUAL;Initial Catalog=LatihanDb;Integrated Security=True;TrustServerCertificate=True";
            //return ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
            return Helper.GetConnectionString();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"select * from Articles order by Title";
                var results = conn.Query<Article>(strSql);
                return results;
            }
        }

        public IEnumerable<Article> GetArticlesWithCategory()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"select a.ArticleID, a.Title, a.Details, a.PublishDate, a.IsApproved, a.Pic, c.CategoryID, c.CategoryName from Articles as a join Categories as c on a.CategoryID = c.CategoryID";
                //var results = conn.Query<Article, Category, Article>(strSql, (article, category) =>
                //{
                //    article.Category = category;
                //    return article;
                //}, splitOn: "CategoryID"
                //);
                List<Article> articles = new List<Article>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var article = new Article()
                        {
                            ArticleID = Convert.ToInt32(reader["ArticleID"]),
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            Title = Convert.ToString(reader["Title"]),
                            Details = Convert.ToString(reader["Details"]),
                            PublishDate = Convert.ToDateTime(reader["PublishDate"]),
                            Pic = Convert.ToString(reader["Pic"]),
                            IsApproved = Convert.ToBoolean(reader["IsApproved"]),
                            Category = new Category()
                            {
                                CategoryID = Convert.ToInt32(reader["CategoryID"]),
                                CategoryName = Convert.ToString(reader["CategoryName"])
                            }
                        };
                        articles.Add(article);
                    }
                }
                //return results;
                return articles;
            }
        }

        public Article GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"select * from Articles where ArticleID = @ArticleID";
                var param = new { ArticleID = id };
                var result = conn.QueryFirstOrDefault<Article>(strSql, param);
                return result;
            }
        }

        public void Insert(Article entity)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"insert into Articles(CategoryID, Title, Details, IsApproved, Pic) 
                               values(@CategoryID, @Title, @Details, @IsApproved, @Pic)";
                var param = new
                {
                    CategoryID = entity.CategoryID,
                    Title = entity.Title,
                    Details = entity.Details,
                    IsApproved = entity.IsApproved,
                    Pic = entity.Pic
                };
                try
                {
                    int result = conn.Execute(strSql, param);
                    if (result != 1)
                    {
                        throw new Exception("Data tidak berhasil ditambahkan");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Kesalahan: " + ex.Message);
                }
            }
        }

        public void Update(Article entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetArticleByCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {

                var strSql = @"select a.ArticleID, a.CategoryID, a.Title, a.Details, a.PublishDate, a.IsApproved, a.Pic, c.CategoryID, c.CategoryName from Articles a inner join Categories c on a.CategoryID = c.CategoryID 
                               where a.CategoryID=@CategoryID";
                var param = new { CategoryID = categoryId };
                var results = conn.Query<Article, Category, Article>(strSql, (article, category) =>
                {
                    article.Category = category;
                    return article;
                }, param, splitOn: "CategoryID");
                return results;
            }
        }
    }
}
