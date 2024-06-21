
Partial Class frmError
    Inherits System.Web.UI.Page

    Sub Page_Load(ByVal Sender As Object, ByVal e As EventArgs)
        Throw (New System.ArgumentNullException())
    End Sub
End Class
