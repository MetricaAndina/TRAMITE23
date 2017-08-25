Namespace tramites

Partial Class tr23cse
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
    Dim WTitulo As String = "Detalle de Cargos realizados a la boleta."

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            '***********************************************************
            'Nombre: tr23cse.aspx
            'Version X.0 (no ten�a comentarios iniciales)
            '************************************************************************************
            'Este Aplicativo se encarga dentro de la aplicaci�n fnt_tramite23.zip de redireccionar hacia 
            'la p�gina solicitada desde el men� de la intranet y realizar la cargas de la variables
            'de sesi�n para la aplicaci�n solicitada
            '************************************************************************************
            'Requerimiento funcionales:
            'Los valores de selecci�n son pasados desde la intranet S�crates
            '************************************************************************************
            'Funcionalidad : 
            'Carga de variables de sesi�n para la aplicaciones solicitadas
            '************************************************************************************
            'Consideraciones:
            'Ninguna
            '************************************************************************************
            'Desarrollado por: Desconocido
            '************************************************************************************
            'Modificaciones:
            '1.- Se ha modificado la p�gina para que los valores de carga de sesi�n sean recibidos 
            'desde la nueva intranet desarrollada por la DINS 
            'Flor Quino - 09/02/15   09:00
            '            
            '************************************************************************************
            '************************************************************************************

        Dim i As Integer
        Dim Total As Integer = Convert.ToInt32(Request.Form.Count)
        Dim WPAGINA As String
            'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - INICIO
            'Agregado para interface nueva intranet
            For i = 1 To Total - 1
                Session(Request.Form.GetKey(i)) = Request.Form(i).ToString()
            Next
            Session("CODPERIODO") = "" 'Seguridad_Get_Decrypt(Request.QueryString, "codperiodo")
            Session("CODUNICO") = "" 'Seguridad_Get_Decrypt(Request.QueryString, "codunico")

            'Try
            '    Dim col As New ColorConverter
            '    Session("WCOLOR_LINEA_NEGOCIO") = col.ConvertFromString(Session("WCOLOR_LINEA"))
            'Catch ex As Exception
            'Si el color no es v�lido
            Session("WCOLOR_LINEA_NEGOCIO") = Color.Maroon
            Session("WCOLOR_LINEA") = "Maroon"
            Session("COD_ALUMNO_ENC") = "" 'Seguridad_Get_Decrypt2(Request.QueryString, "ALU")
            Session("CTRA_VALIDADO") = "" 'Seguridad_Get_Decrypt(Request.QueryString, "ctra")
            'End Try
            'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - FIN

            Session("CUSUARIO") = UCase(Session("CUSUARIO"))
            'Response.Write(Session("CUSUARIO"))
            'Response.End()
            If Convert.ToString(Session("CUSUARIO")) = "" Then
                EnviarPagina("SesionNet.asp", "tr23resultado.aspx", WTitulo, "010", "", "", "", "", "close")
                Exit Sub
            Else
                WPAGINA = "tr2305.aspx" 'Request.QueryString("WPAG2").Trim
                Response.Redirect(WPAGINA)
            End If
    End Sub

    Public Sub EnviarPagina(ByVal paginaorigen As String, _
                           ByVal paginadestino As String, _
                           ByVal titulo As String, _
                           ByVal imagen As String, _
                           ByVal mensaje1 As String, _
                           ByVal mensaje2 As String, _
                           ByVal mensaje3 As String, _
                           ByVal mensaje4 As String, _
                           ByVal boton As String)

        Response.Redirect(paginadestino & "?paginaorigen=" & paginaorigen & "&mensaje1=" & mensaje1 & "&mensaje2=" & mensaje2 & "&mensaje3=" & mensaje3 & "&mensaje4=" & mensaje4 & "&imagen=" & imagen & "&boton=" & boton)
    End Sub


End Class

End Namespace
