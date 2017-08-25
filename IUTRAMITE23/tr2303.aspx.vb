Imports ADTRAMITES.ADTRAMITES


Namespace tramites


Partial Class tr2303
        'Inherits Custom3DevProviders.SeguridadBasePage
        Inherits System.Web.UI.Page
    Dim C As New clsTramites


    Public ReadOnly Property Titulo() As String
        Get
            Return ViewState("Titulo")
        End Get
    End Property

    Public ReadOnly Property CodMensaje() As String
        Get
            Return ViewState("CodMensaje")
        End Get
    End Property
    Public ReadOnly Property MensajeError() As String
        Get
            Return ViewState("MensajeError")
        End Get
    End Property


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
        'Session("COD_UNICO") = Request.QueryString("cod_unico") ' "5921"
        'Session("COD_PERIODO") = Request.QueryString("cod_periodo") '"200301"

        'Response.Write(Session("COD_ALUMNO_ENC") & "ALU")

        If Convert.ToString(Session("CUSUARIO")) = "" Then
            EnviarPagina("tr23resultado.aspx", "Sócrates - Intranet", "010", "", "")
            Exit Sub
        End If

        ObtieneSede()

        If Not Page.IsPostBack Then

            Dim objTram As New clsTramites
            Dim objConfig As New Configuracion
            Dim dsTram As New DataSet

            objTram.CadenaConexion = conexion
            dsTram = objTram.ConsultaAlumno(CType(Session("CODUNICO"), String), CType(Session("CODPERIODO"), String))

            Dim strMsgError As String = objTram.ErrorMensaje

            If dsTram Is Nothing Then
                If strMsgError <> "" Then
                    EnviarPagina("tr23Resultado.aspx", "Trámites", "99", "Ocurrió un error al mostrar la solicitud del alumno. ", strMsgError)
                End If
            ElseIf dsTram.Tables(0).Rows.Count = 0 Then
                strMsgError = "Solicitud N° " & CType(Session("CODUNICO"), String) & " del período " & CType(Session("CODPERIODO"), String) & " no existe."
                EnviarPagina("tr23Resultado.aspx", "Trámites", "99", strMsgError, "")
            End If

            If dsTram.Tables(0).Rows.Count > 0 Then

                ' SIN PERMISO PARA NGRESAR
                If (Session("CUSUARIO") <> dsTram.Tables(0).Rows(0)("COD_USUARIO")) And (Session("CTRA_VALIDADO") <> "SI") Then
                    Mensaje_tramite(23, "TR2302", "1", "")
                End If

                Dim strTituloSol As String = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("TITULO")), "", dsTram.Tables(0).Rows(0)("TITULO")), String)

                If strTituloSol.Trim <> "" Then
                    Me.lblTituloSol.Text = UCase(Mid(strTituloSol.Trim, 1, 1)) & LCase(Mid(strTituloSol.Trim, 2, Len(strTituloSol.Trim) - 1))
                Else
                    Me.lblTituloSol.Text = ""
                End If

                Me.lblNumSol.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("COD_SOLICITUD")), "", dsTram.Tables(0).Rows(0)("COD_SOLICITUD")), String)
                Me.lblFechaSol.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("FECHA_SOLICITUD")), "", dsTram.Tables(0).Rows(0)("FECHA_SOLICITUD")), String)

                Select Case CType(dsTram.Tables(0).Rows(0)("ESTADO"), String)

                    Case "PE"
                        Me.lblEstadoSol.Text = "PENDIENTE"
                    Case "IN"
                            Me.lblEstadoSol.Text = "NO PAGÓ EN BANCO"
                    Case "PR"
                            ' ---- Modificado SEP-2007-566 ----
                            'Me.lblEstadoSol.Text = "ENTREGADO"
                            If CType(dsTram.Tables(0).Rows(0)("IND_ENTREGADO"), String) = "SI" Then
                                Me.lblEstadoSol.Text = "ENTREGADO"
                            Else
                                Me.lblEstadoSol.Text = "PROCEDE"
                            End If
                            ' --- Fin Modificado SEP-2007-566 ----

                    Case "NP"
                        Me.lblEstadoSol.Text = "NO PROCEDE"
                    Case "PP"
                        Me.lblEstadoSol.Text = "PROCEDE PARCIALMENTE"

                            ' ---- Agregado por la SEP-2007-566
                        Case "EN"
                            Me.lblEstadoSol.Text = "ENTREGADO"
                            ' ---- Fin Agregado por la SEP-2007-566

                    End Select

                If CType(dsTram.Tables(0).Rows(0)("IND_ENVIADO"), String) = "SI" And CType(dsTram.Tables(0).Rows(0)("IND_ENTREGADO"), String) = "NO" Then
                        'lblMensaje.Text = "El documento está pendiente de recoger en el Counter de Secretaría Académica."
                        'CSC-00259237-00
                        lblMensaje.Text = "El documento está pendiente de recoger en el Centro de Atención al Alumno."
                    Me.lblMensaje.Visible = True
                ElseIf CType(dsTram.Tables(0).Rows(0)("IND_ENVIADO"), String) = "RE" And CType(dsTram.Tables(0).Rows(0)("IND_ENTREGADO"), String) = "NO" Then
                        lblMensaje.Text = "El documento no pudo tramitarse, acérquese al Centro de Atención al Alumno para mayor información"
                    Me.lblMensaje.Visible = True
                Else
                    lblMensaje.Text = ""
                    Me.lblMensaje.Visible = False
                End If

                Me.lblAlumnoNombre.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("COD_ALUMNO")), "", dsTram.Tables(0).Rows(0)("COD_ALUMNO")), String) & " - " & CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("NOMBRE")), "", dsTram.Tables(0).Rows(0)("NOMBRE")), String)
                Me.lblAlumnoTelef.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("TELEFONO_CASA")), "", dsTram.Tables(0).Rows(0)("TELEFONO_CASA")), String)
                Me.lnkAlumnoMail.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("EMAIL")), "", dsTram.Tables(0).Rows(0)("EMAIL")), String)
                Me.lnkAlumnoMail.NavigateUrl = "mailto:" & CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("EMAIL")), "", dsTram.Tables(0).Rows(0)("EMAIL")), String)
                Me.lblAlumnoCarrera.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("COD_CARRERA")), "", dsTram.Tables(0).Rows(0)("COD_CARRERA")), String) & " - " & CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("CARRERA")), "", dsTram.Tables(0).Rows(0)("CARRERA")), String)

                objConfig.CadenaConexion = conexion
                If objConfig.Configuracion(CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("COD_ALUMNO")), "", dsTram.Tables(0).Rows(0)("COD_ALUMNO")), String), CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("COD_LINEA_NEGOCIO")), "", dsTram.Tables(0).Rows(0)("COD_LINEA_NEGOCIO")), String), CType(Request.ServerVariables("APPL_PHYSICAL_PATH"), String)) Then
                    Me.imgFoto.ImageUrl = objConfig.RUTA_IMAGEN_URL()
                End If

                Me.lblCiclo.Text = CType(Session("CODPERIODO"), String)
                Me.lblModalidad.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("COD_MODAL_EST")), "", dsTram.Tables(0).Rows(0)("COD_MODAL_EST")), String) & " - " & CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("MODALIDAD")), "", dsTram.Tables(0).Rows(0)("MODALIDAD")), String) & " - " & CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("LINEA_NEGOCIO")), "", dsTram.Tables(0).Rows(0)("LINEA_NEGOCIO")), String)
                Me.lblTipoDocumento.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("COD_MOTIVO")), "", dsTram.Tables(0).Rows(0)("COD_MOTIVO")), String) & " - " & CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("TIPODOCUMENTO")), "", dsTram.Tables(0).Rows(0)("TIPODOCUMENTO")), String)
                Me.txtSustentoSol.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("SUSTENTO")), "", dsTram.Tables(0).Rows(0)("SUSTENTO")), String)

                Me.lblEvaluador.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("EVALUADOR")), "", dsTram.Tables(0).Rows(0)("EVALUADOR")), String)
                Me.lblFechaEval.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("FECHA_EVALUADOR")), "", dsTram.Tables(0).Rows(0)("FECHA_EVALUADOR")), String)
                Me.txtObs.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("OBSERVACION")), "", dsTram.Tables(0).Rows(0)("OBSERVACION")), String)

                Me.lblAsesor.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("ASESOR")), "", dsTram.Tables(0).Rows(0)("ASESOR")), String)
                Me.lblFechaAses.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("FECHA_ASESOR")), "", dsTram.Tables(0).Rows(0)("FECHA_ASESOR")), String)
                Me.txtObs1.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("OBSERVACION_1")), "", dsTram.Tables(0).Rows(0)("OBSERVACION_1")), String)

                Me.lblSecretaria.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("SECRETARIA")), "", dsTram.Tables(0).Rows(0)("SECRETARIA")), String)
                Me.lblFechaSecre.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("FECHA_LLAMADA")), "", dsTram.Tables(0).Rows(0)("FECHA_LLAMADA")), String)
                Me.txtObs2.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("OBSERVACION_2")), "", dsTram.Tables(0).Rows(0)("OBSERVACION_2")), String)

                If IsDBNull(dsTram.Tables(0).Rows(0)("USUARIO_ASESOR")) Then
                    Me.dv_Asesor.Visible = False
                Else
                    Me.dv_Asesor.Visible = True
                End If

                If IsDBNull(dsTram.Tables(0).Rows(0)("USUARIO_SECRETARIA")) Then
                    Me.dv_Secretaria.Visible = False
                Else
                    Me.dv_Secretaria.Visible = True
                End If

            End If

            dsTram.Dispose()
            dsTram = Nothing
            objTram = Nothing
            objConfig = Nothing

        End If

    End Sub

    Sub Mensaje_tramite(ByVal pcod_tramite As String, _
                            ByVal parchivo As String, _
                            ByVal pcod_mensaje As String, _
                            ByVal perrsql As String)
        Dim wtexto, wtexto1, wtexto2, wtexto3, perr As String

        C.Mensaje(conexion, parchivo, pcod_mensaje, pcod_tramite, Session("WMANEJA_LOCAL"), wtexto, wtexto1, wtexto2, wtexto3, perr)
        If perr = "" Then
            If perrsql <> "" Then
                wtexto1 = perrsql
            End If
            EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", wtexto, wtexto1)
        Else
            EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", perr, perr)
        End If
    End Sub

    Private Sub EnviarPagina(ByVal PAGINA As String, ByVal TITULO As String, _
                                ByVal CODMENSAJE As String, ByVal MENSAJE As String, ByVal ERRORSQL As String)

        Me.lblTitulo.Text = TITULO
        Me.lblCODMENSAJE.Text = CODMENSAJE
        Me.lblMensajeError1.Text = MENSAJE
        Me.lblerrorsql.Text = ERRORSQL
        Server.Transfer(PAGINA, True)

    End Sub

    Private Sub EnviarPaginaError(ByVal PAGINA As String, ByVal TITULO As String, _
                                ByVal CODMENSAJE As String, ByVal MENSAJE As String)

        ViewState("Titulo") = TITULO
        ViewState("CodMensaje") = CODMENSAJE
        ViewState("MensajeError") = MENSAJE
        Server.Transfer(PAGINA, True)

    End Sub

    Private Sub ObtieneSede()
        Dim var As String
        Session("WCOD_LOCAL") = C.Sede(conexion, Session("CUSUARIO"))
    End Sub

End Class

End Namespace
