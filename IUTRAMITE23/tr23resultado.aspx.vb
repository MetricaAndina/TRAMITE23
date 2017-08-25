Namespace tramites

Partial Class traresultado
        'Inherits Custom3DevProviders.SeguridadBasePage
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim WCODMENSAJE, WMensajeError1, WerrorSQL As String
        Dim WTitulo As String

            'BEGIN: CROSSSITESCRIPTING
            Dim cssError As String
            Dim result1 As Boolean = LNCrossSiteScripting.LN_Permisos.Validar(Seguridad_Get_Decrypt_All(Request.QueryString), conexion, "I", "V", "A", cssError)
            Dim result2 As Boolean = LNCrossSiteScripting.LN_Permisos.Validar(Seguridad_GetQueryString(Request.QueryString), conexion, "I", "V", "A", cssError)
            If (result1 Or result2) Then
                WMensajeError1 = "Esta página ha recibido parámetros incorrectos"
                lblMsgTitulo.Text = ""
                Return
            End If
            'END: CROSSSITESCRIPTING

        Dim thisPage As System.Web.UI.Page = CType(Context.Handler, System.Web.UI.Page)
        Dim lblMensajeError1 As Label = CType(thisPage.FindControl("lblMensajeError1"), Label)
        Dim lblcodmensaje As Label = CType(thisPage.FindControl("lblcodmensaje"), Label)
        Dim lblTitulo1 As Label = CType(thisPage.FindControl("lblTitulo"), Label)
        Dim lblErrorSQL As Label = CType(thisPage.FindControl("lblErrorSQL"), Label)
        WCODMENSAJE = lblcodmensaje.Text
        WMensajeError1 = lblMensajeError1.Text
        WTitulo = lblTitulo1.Text
        WerrorSQL = lblErrorSQL.Text

        imgRpta.ImageUrl = "../../imagen/error.jpg"
        Select Case WCODMENSAJE
            Case "010"
                WMensajeError1 = "Ha expirado su tiempo de conexión con el sistema, por favor salga de la intranet y vuelva a ingresar."
            Case "01"
                WMensajeError1 = "Ha ocurrido un error al realizar la consulta. " & WMensajeError1
                imgRpta.ImageUrl = "../../imagen/error.jpg"
                lblMsgTitulo.Text = ""
            Case "02" 'Cuando es correcto y tiene un mensjae que mostrar
                imgRpta.ImageUrl = "../../imagen/check.jpg"
                lblMsgTitulo.Text = ""
            Case "99" 'Cuando es error y tiene un mensjae que mostrar
                imgRpta.ImageUrl = "../../imagen/error.jpg"
                lblMsgTitulo.Text = ""

        End Select

        lblMensaje.Text = WMensajeError1
        lblTitulo.Text = WTitulo
        Label1.Text = WerrorSQL

        If WMensajeError1 = "" Then
            Me.lblMsgTitulo.Text = "Hubo un error al procesar la página."
        End If

    End Sub

End Class

End Namespace
