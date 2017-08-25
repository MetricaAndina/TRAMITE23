Imports System.Web
Imports System.Web.SessionState


Namespace tramites


Public Class [Global]
    Inherits System.Web.HttpApplication

#Region " Component Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
            'Fires when the session is started
            ''Session("CODUNICO") = "447920"
            ''Session("CODPERIODO") = "200600"
            Session("WCOD_LINEA_NEGOCIO") = "U"
            'Session("CUSUARIO") = "U011088"
            Session("CUSUARIO") = "CROJAS"
            'Session("COD_MODAL_EST") = "FC"
            Session("WCOLOR_LINEA_NEGOCIO") = Color.Maroon
            ''Session("WCOD_ALUMNO") = "200020252" 'doble modalidad
            'Session("WCOD_ALUMNO") = "200011088"
            'Session("COD_ALUMNO_ENC") = "200011088"
            Session("WTIPO_PERSONA") = "SEC"
        End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

        Public Function Evalua(ByVal m As Match) As String
            Return String.Format("ReturnUrl={0}", HttpUtility.UrlEncode(Request.Url.AbsoluteUri))
        End Function

        Protected Sub Application_EndRequest(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim redirectUrl As String = Me.Response.RedirectLocation
            If (Not (Me.Request.RawUrl.Contains("ReturnUrl=")) And Not (String.IsNullOrEmpty(redirectUrl))) Then
                Response.RedirectLocation = Regex.Replace(redirectUrl, _
                "ReturnUrl=(?'url'[^&]*)", New MatchEvaluator(AddressOf Evalua), _
                              RegexOptions.Singleline Or RegexOptions.IgnoreCase Or _
                              RegexOptions.ExplicitCapture)
            End If
        End Sub

End Class

End Namespace
