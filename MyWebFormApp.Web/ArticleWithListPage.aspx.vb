Imports System.Web.ModelBinding
Imports MyWebFormApp.BLL
Imports MyWebFormApp.BLL.DTOs

Public Class ArticleWithListPage
    Inherits System.Web.UI.Page

    Dim _categoryBLL As New CategoryBLL()
    Dim _articleBLL As New MyWebFormApp.BLL.ArticleBLL

#Region "Binding Data"


    Sub LoadDataCategories()
        Dim _categoryBLL As New MyWebFormApp.BLL.CategoryBLL
        Dim results = _categoryBLL.GetAll()
        lvCategories.DataSource = results
        lvCategories.DataBind()

        ddCategories.DataSource = results
        ddCategories.DataTextField = "CategoryName"
        ddCategories.DataValueField = "CategoryID"
        ddCategories.DataBind()
    End Sub

    Sub LoadDataArticles(categoryID As String)
        Dim _articleBLL As New MyWebFormApp.BLL.ArticleBLL
        Dim results = _articleBLL.GetArticleByCategory(categoryID)
        lvArticles.DataSource = results
        lvArticles.DataBind()
    End Sub

#End Region

#Region "Validate"
    Function CheckFileType(ByVal fileName As String) As Boolean
        Dim ext As String = IO.Path.GetExtension(fileName)
        Select Case ext.ToLower()
            Case ".gif"
                Return True
            Case ".png"
                Return True
            Case ".jpg"
                Return True
            Case ".jpeg"
                Return True
            Case ".bmp"
                Return True
            Case Else
                Return False
        End Select
    End Function
#End Region

#Region "Initialize"
    Sub InitializeFormAddArticle()
        txtTitle.Text = String.Empty
        txtDetail.Text = String.Empty
        chkIsApproved.Checked = False
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String

    Public Function lvCategories_GetData() As IEnumerable(Of MyWebFormApp.BLL.DTOs.CategoryDTO)
        Dim results = _categoryBLL.GetAll()
        Return results
    End Function

    Protected Sub lvCategories_ItemCommand(sender As Object, e As ListViewCommandEventArgs)
        If e.CommandName = "Select" Then
            'Dim lnkSelect = CType(e.Item.FindControl("lnkSelect"), LinkButton)
            Dim categoryid = e.CommandArgument.ToString()
            ltMessage.Text = categoryid
        End If
    End Sub

    ' The return type can be changed to IEnumerable, however to support
    ' paging and sorting, the following parameters must be added:
    '     ByVal maximumRows as Integer
    '     ByVal startRowIndex as Integer
    '     ByRef totalRowCount as Integer
    '     ByVal sortByExpression as String
    Public Function lvArticles_GetData(<Control("lvCategories")> _categoryID As String) As IEnumerable(Of ArticleDTO)
        Dim results = _articleBLL.GetArticleByCategory(CInt(_categoryID))
        Return results
    End Function

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            'rename upload file
            'Dim fileName As String = Guid.NewGuid.ToString() & fpPic.FileName
            'If fpPic.HasFile Then
            '    If CheckFileType(fileName) Then
            '        Dim _path As String = Server.MapPath("~/Pics/")
            '        fpPic.SaveAs(_path & fileName)
            '    Else
            '        ltMessage.Text = "<span class='alert alert-danger'>Error: Only images are allowed</span><br/><br/>"
            '        Return
            '    End If
            'End If

            Dim _articleBLL As New MyWebFormApp.BLL.ArticleBLL
            Dim _article As New ArticleCreateDTO
            _article.CategoryID = CInt(ddCategories.SelectedValue)
            _article.Title = txtTitle.Text
            _article.Details = txtDetail.Text
            _article.IsApproved = If(chkIsApproved.Checked, 1, 0)
            _article.Pic = txtPic.Text
            _articleBLL.Insert(_article)

            ltMessage.Text = "<span class='alert alert-success'>Artice added successfully</span><br/><br/>"
            InitializeFormAddArticle()
        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub
End Class