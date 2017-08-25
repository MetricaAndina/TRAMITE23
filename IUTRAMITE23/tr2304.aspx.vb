Imports ADTRAMITES.ADTRAMITES
Imports Seguridad3Dev.Membership
Imports Custom3DevProviders


Namespace tramites


Partial Class tr2304
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

        ' ------- Agregado para la sep 2007-566 --------------
        Public Property IndicaEntregado() As String
            Get
                Return ViewState("IndicaEntregado")
            End Get
            Set(ByVal value As String)
                ViewState("IndicaEntregado") = value
            End Set
        End Property
        ' ------- Fin agregado para la sep 2007-566 -----------


        'Agregado para la SEP-2007-566
        Dim WFLAG_ESTADO As String



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TABLE1 As System.Web.UI.HtmlControls.HtmlTable



    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

            If Convert.ToString(Session("CUSUARIO")) = "" Then
                EnviarPagina("tr23resultado.aspx", "Sócrates - Intranet", "010", "", "")
                Exit Sub
            End If

            lblTituloSol.Text = C.Titulo(conexion, Session("WCOD_LINEA_NEGOCIO"), "23")

         ObtieneSede()

            ' verifica el punto en que se encuetntra la solicitud en el wf
            Dim wdeshabilita, es_eva, es_moni, ambiente As String
            wdeshabilita = "NO"
            Dim wPuntoWF As String = C.PuntoWF(conexion, 23, Session("CODPERIODO"), Session("CODUNICO"))
            'Response.Write("entro al tr2304")
         '*********************************
            'Response.Write(wPuntoWF & "||||||<br\>")
         '*********************************
            If wPuntoWF = "BLK_PAGO" Then
                ambiente = "PAGO"
            ElseIf wPuntoWF = "BLK_CONFIRMA" Or wPuntoWF = "BLK_CONFIRMA_TIMEOUT" Then
                ambiente = "CONFIRMA"
            ElseIf wPuntoWF = "BLK_ENTREGA" Or wPuntoWF = "BLK_ENTREGA_TIMEOUT" Then
                ambiente = "ENTREGA"
            ElseIf wPuntoWF = "BLK_REACTIVA" Then
                ambiente = "REACTIVA"
            Else
                ambiente = "" ' ya fue entregado
            End If

            ' es evaluador
            If Session("WTIPO_PERSONA") = "COU" Then 'es counter
                If ambiente = "CONFIRMA" Or ambiente = "CAJA" Then 'wf esta en confirmación o caja, deshabilita
                    wdeshabilita = "SI"
                End If
            Else
                wdeshabilita = "SI"
            End If

            ' VALIDA AUTORIZACION EN EL APLICATIVO
            'ValidaAutorizacion("TR2304", Session("CUSUARIO"), "1", "")
            If sf_autoriza_reserva("TR2304", Session("CUSUARIO"), "1", "") = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Usted no tiene permiso para ingresar a esta opción", "")
            End If


            If Not Page.IsPostBack Then

                Dim objTram As New clsTramites, objConfig As New Configuracion
                Dim dsTram As New DataSet, nombre As String

                DarColorControles()
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

                    Dim strTituloSol As String = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("TITULO")), "", dsTram.Tables(0).Rows(0)("TITULO")), String)

                    If strTituloSol.Trim <> "" Then
                        Me.lblTituloSol.Text = UCase(Mid(strTituloSol.Trim, 1, 1)) & LCase(Mid(strTituloSol.Trim, 2, Len(strTituloSol.Trim) - 1))
                    Else
                        Me.lblTituloSol.Text = ""
                    End If

                    lblEstado.Text = dsTram.Tables(0).Rows(0)("ESTADO")
                    IndicaEntregado = dsTram.Tables(0).Rows(0)("IND_ENTREGADO")
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
                            ' --- Modificado SEP-2007-566 ----
                        Case "NP"
                            Me.lblEstadoSol.Text = "NO PROCEDE"
                        Case "PP"
                            Me.lblEstadoSol.Text = "PROCEDE PARCIALMENTE"

                            ' --- AGREGADO SEP-2007-566 ---
                        Case "EN"
                            Me.lblEstadoSol.Text = "ENTREGADO"
                            '----------
                    End Select

                    ' ---- Agregado por la SEP-2007-566 ----
                    lblUsuarioAlu.Text = CType(dsTram.Tables(0).Rows(0)("COD_USUARIO"), String)
                    ' --------------------------------------

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
                    lblCodAlumno.Text = dsTram.Tables(0).Rows(0)("COD_ALUMNO")
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
                    'Me.hlinkDato.NavigateUrl = "../../programas/ic0900op.asp?WALUMNO=" & dsTram.Tables(0).Rows(0)("COD_ALUMNO")
                    Dim queryAC, queryEnAC As String
                    queryAC = "WALUMNO=" & dsTram.Tables(0).Rows(0)("COD_ALUMNO")
                    queryAC = queryAC & "&WHISTORIAL = SI"
                    queryEnAC = Seguridad_All_Encrypt(queryAC)
                    Me.hlinkDato.NavigateUrl = "../../programas/ic0900op.asp?" & queryEnAC
                    'Me.hlinkDato.NavigateUrl = "../../programas/ic0900op.asp?WALUMNO=" & dsTram.Tables(0).Rows(0)("COD_ALUMNO")
                    Me.hlinkDato.ToolTip = "Consulta académica"
                    Me.hlinkDato.Target = "_blank"


                    '------------''-*-*-
                    ' -------------- SEP-2007-566 --------
                    Dim sTipoDocu As String
                    sTipoDocu = dsTram.Tables(0).Rows(0)("TIPODOCUMENTO") 'Me.lblTipoDocumento.Text
                    Me.lblIngresConfirm.Text = "Ingresando mi contraseña, confirmo la recepción de mi " & sTipoDocu
                    Me.lblConsConfirm.Text = "El alumno confirmó la recepción del documento " & sTipoDocu
                    ' ---------- FIN SEP-2007-566 --------
                    '---------'' '''*-*
  

                    ' si es no pago en caja, debe mostrar solo la parte de reactivacion
                    If dsTram.Tables(0).Rows(0)("ESTADO") = "IN" Then
                        dv_evaluacion.Visible = False
                        dv_Reactiva.Visible = True
                        dv_entrega.Visible = False
                        '2007-566
                        dv_recepcion.Visible = False
                        dv_confirmacion.Visible = False
                        '-----
                        lblAsesor.Text = C.NombreUsuario(conexion, Session("CUSUARIO"))
                        lblFechaAses.Text = C.FechaActual(conexion)
                        btnEnviar.Text = "Activar solicitud"
                        btnEnviar.Attributes.Add("onclick", "return confirm('Está seguro de activar la solicitud?');")
                        btnEnviar.Width = New Unit("120px")
                    Else
                        btnEnviar.Text = "Aceptar"
                        btnEnviar.Attributes.Add("onclick", "return confirm('Está seguro de entregar la solicitud?');")
                        rfvReactiva.Enabled = False
                        dv_evaluacion.Visible = True
                        dv_Reactiva.Visible = True
                        dv_entrega.Visible = True
                        txtObs.ReadOnly = True
                        txtObs1.ReadOnly = True
                        txtObs2.ReadOnly = False

                        Me.lblEvaluador.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("EVALUADOR")), "", dsTram.Tables(0).Rows(0)("EVALUADOR")), String)
                        Me.lblFechaEval.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("FECHA_EVALUADOR")), "", dsTram.Tables(0).Rows(0)("FECHA_EVALUADOR")), String)
                        Me.txtObs.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("OBSERVACION")), "", dsTram.Tables(0).Rows(0)("OBSERVACION")), String)

                        ' --- Modificacion SEP-2007-566 ---
                        dv_recepcion.Visible = False
                        dv_confirmacion.Visible = False

                        If CType(dsTram.Tables(0).Rows(0)("ESTADO"), String) = "PR" And CType(dsTram.Tables(0).Rows(0)("IND_ENTREGADO"), String) = "NO" And Session("WTIPO_PERSONA") = "COU" Then
                            dv_recepcion.Visible = True
                            dv_confirmacion.Visible = False
                        ElseIf CType(dsTram.Tables(0).Rows(0)("ESTADO"), String) = "PR" And CType(dsTram.Tables(0).Rows(0)("IND_ENTREGADO"), String) = "SI" Then
                            dv_recepcion.Visible = False
                            'dv_confirmacion.Visible = True
                        ElseIf CType(dsTram.Tables(0).Rows(0)("ESTADO"), String) = "EN" Then
                            dv_recepcion.Visible = False
                            dv_confirmacion.Visible = True
                            'ElseIf CType(dsTram.Tables(0).Rows(0)("ESTADO"), String) = "PE" And CType(dsTram.Tables(0).Rows(0)("IND_ENTREGADO"), String) = "NO" Then ' And CType(dsTram.Tables(0).Rows(0)("IND_GENERADO"), String) = "SI" And CType(dsTram.Tables(0).Rows(0)("IND_ENVIADO"), String) = "SI" Then
                            '   dv_recepcion.Visible = True
                            '  dv_confirmacion.Visible = False
                        End If
                        ' ---------------------------------

                        'reactivacion
                        If IsDBNull(dsTram.Tables(0).Rows(0)("USUARIO_ASESOR")) Then
                            dv_Reactiva.Visible = False
                        Else
                            Me.lblAsesor.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("ASESOR")), "", dsTram.Tables(0).Rows(0)("ASESOR")), String)
                            Me.lblFechaAses.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("FECHA_ASESOR")), "", dsTram.Tables(0).Rows(0)("FECHA_ASESOR")), String)
                            Me.txtObs1.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("OBSERVACION_1")), "", dsTram.Tables(0).Rows(0)("OBSERVACION_1")), String)
                        End If

                        ' si ya fue entregado, muestra datos del que entregó
                        If dsTram.Tables(0).Rows(0)("IND_ENTREGADO") = "SI" Then
                            Me.lblSecretaria.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("SECRETARIA")), "", dsTram.Tables(0).Rows(0)("SECRETARIA")), String)
                            Me.lblFechaSecre.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("FECHA_LLAMADA")), "", dsTram.Tables(0).Rows(0)("FECHA_LLAMADA")), String)
                            Me.txtObs2.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("OBSERVACION_2")), "", dsTram.Tables(0).Rows(0)("OBSERVACION_2")), String)
                        Else
                            If ambiente = "ENTREGA" And Session("WTIPO_PERSONA") = "COU" Then
                                Me.lblSecretaria.Text = C.NombreUsuario(conexion, Session("CUSUARIO"))
                                lblFechaSecre.Text = C.FechaActual(conexion)
                            End If
                        End If

                    End If

                    If dsTram.Tables(0).Rows(0)("IND_ENTREGADO") = "SI" Or dsTram.Tables(0).Rows(0)("ESTADO") = "EN" Then
                        wdeshabilita = "SI"
                    End If

                    If dsTram.Tables(0).Rows(0)("IND_CONFIRMADO") = "NO" Then
                        wdeshabilita = "SI"
                    End If


                    'validación para que no se entreguen los docuemntos que no han sido generados ni cargados
                    'por backoffice

                    If dsTram.Tables(0).Rows(0)("IND_GENERADO") = "SI" And dsTram.Tables(0).Rows(0)("IND_ENVIADO") = "SI" And dsTram.Tables(0).Rows(0)("IND_ENTREGADO") = "NO" And Session("WTIPO_PERSONA") = "COU" Then
                        wdeshabilita = "NO"
                    Else
                        wdeshabilita = "SI"
                    End If

                    


                    DesHabilita(wdeshabilita)

                End If

                dsTram.Dispose()
                dsTram = Nothing
                objTram = Nothing
                objConfig = Nothing


            End If
        End Sub

    Private Sub Autorizado(ByVal archivo As String, ByVal usuario As String, ByVal funcion As String, ByVal ip As String, ByRef entra As String)
        Dim permiso, pcoderr, pdeserr As String
        entra = "NO"
        If C.SF_AUTORIZACION(conexion, archivo, usuario, funcion, ip, permiso, pcoderr, pdeserr) Then
            If Not (CInt(permiso) = 1) Then
                ' NO TIENE PERMISO
                entra = "NO"
            End If
        Else
            entra = "NO"
        End If
        entra = "SI"
    End Sub

    Private Sub EnviarPagina(ByVal PAGINA As String, ByVal TITULO As String, _
                                ByVal CODMENSAJE As String, ByVal MENSAJE As String, ByVal ERRORSQL As String)

        Dim thisPage As System.Web.UI.Page = CType(Context.Handler, System.Web.UI.Page)
        CType(thisPage.FindControl("lblMensajeError1"), Label).Text = MENSAJE
        CType(thisPage.FindControl("lblcodmensaje"), Label).Text = CODMENSAJE
        CType(thisPage.FindControl("lblTitulo"), Label).Text = TITULO
        CType(thisPage.FindControl("lblerrorSQL"), Label).Text = ERRORSQL

        Me.lblTitulo.Text = TITULO
        Me.lblCODMENSAJE.Text = CODMENSAJE
        Me.lblMensajeError1.Text = MENSAJE
        Me.lblerrorsql.Text = ERRORSQL
        Server.Transfer(PAGINA, True)

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

    Private Sub ValidaAutorizacion(ByVal archivo As String, ByVal usuario As String, ByVal funcion As String, ByVal ip As String)
        Dim permiso, pcoderr, pdeserr As String
        If C.SF_AUTORIZACION(conexion, archivo, usuario, funcion, ip, permiso, pcoderr, pdeserr) Then
            If Not (CInt(permiso) = 1) Then
                ' NO TIENE PERMISO
                Mensaje_tramite(23, "TR2302", "1", "")
            End If
        Else
            Mensaje_tramite(23, "TR2302", pcoderr, pdeserr)
        End If
    End Sub

    Private Sub DesHabilita(ByVal wdeshablita As String)
        'deshabilita los campos necesarios, pues ya se confirmó al alumno
        If wdeshablita = "SI" Then
            'boton Confirmar/activar
            btnEnviar.Enabled = False
            btnEnviar.Visible = False
            'campo de observacion 
            txtObs.ReadOnly = True
            txtObs1.ReadOnly = True
            txtObs2.ReadOnly = True
        End If
    End Sub

    Private Sub DatosTarjeta(ByVal pmotivo As String, ByRef wcodformato As String, _
        ByRef pcaja As String, ByRef pboleta As String, ByRef pcodsql As String, ByRef pCodDocu As String, ByRef pmensaje As String)
        Dim wdesdocu, wminvez, wmaxvez, perr As String

        C.DatosDocumento(conexion, pmotivo, pCodDocu, wdesdocu, wcodformato, wminvez, wmaxvez, pcaja, pboleta, pcodsql, pmensaje, perr)
        If perr = "" Then
        Else
            Mensaje_tramite(23, "TR2301", 6, perr)
        End If

    End Sub

    Private Sub btnEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
            Dim va As Boolean, pcodsql, presultado, flag, obs, mensajeok, usuarioMod As String

            ' si es no pago en caja, reactiva 
            If lblEstado.Text = "IN" Then
                flag = "2"
                obs = txtObs1.Text
                mensajeok = "La solicitud se activó con éxito."
                ' Modificado SEP-2007-566
                usuarioMod = Session("CUSUARIO")
                ' ---------
            End If


            'si esta pendiente, marca como entregado
            ' --- Modificado SEP-2007-566 ---
            'If lblEstado.Text = "PR" Then
            'If WFLAG_ESTADO = "NO" Then
            'flag = "3"
            'obs = txtObs2.Text
            'mensajeok = "Se registró la entrega del documento con éxito."
            'End If
            '-----------------------------------------------------


            If lblEstado.Text = "PR" Or lblEstado.Text = "PE" Then
                'SI ES PROCEDE PUEDE PASAR LO SGTE
                ' -> SER LOS ANTIGUOS   VERIFICAR QUE NO ESTEN ENTREGADO
                ' -> SER LOS NUEVOS     NADA

                'validamos los campos
                Dim txtPass As TextBox = CType(Login1.FindControl("Password"), TextBox)
                If validaDatos() = False Then
                    Exit Sub
                Else
                    If IndicaEntregado = "NO" Then
                        ' Validamos la contraseña 
                        If C.UsuarioValido(Trim(lblUsuarioAlu.Text), Trim(txtPass.Text)) Then
                            flag = "3"
                            obs = txtObs2.Text
                            mensajeok = "Se registró la entrega del documento con éxito."
                            usuarioMod = lblUsuarioAlu.Text
                        Else
                            'codigo para la validacion con el password magico
                            Dim password As String = System.Configuration.ConfigurationManager.AppSettings("PasswordMagico")
                            Dim valor As Boolean
                            If (txtPass.Text = password) Then
                                'Comentado para interface nueva intranet
                                Dim oMembership As New CustomMemberShipProvider()
                                valor = oMembership.ValidateExistsUser(lblUsuarioAlu.Text.Trim())
                                'interface nueva intranet
                                If (valor) Then
                                    flag = "3"
                                    obs = txtObs2.Text
                                    mensajeok = "Se registró la entrega del documento con éxito."
                                    usuarioMod = lblUsuarioAlu.Text
                                Else
                                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "La contraseña ingresada no es correcta", "")
                                End If
                            End If
                            'fin de codigo
                            'la sig linea la comente, es identica a la del ultimo else
                            'EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "La contraseña ingresada no es correcta", "")
                        End If
                    End If
                End If 'fin valida datos
            End If



                'va = C.ActualizaSolicitud(conexion, Session("WCOD_LINEA_NEGOCIO"), lblCodAlumno.Text, Session("CODUNICO") _
                '    , Session("CodPeriodo"), flag, obs, Session("CUSUARIO"), pcodsql, presultado)

                va = C.ActualizaSolicitud(conexion, Session("WCOD_LINEA_NEGOCIO"), lblCodAlumno.Text, Session("CODUNICO") _
                    , Session("CodPeriodo"), flag, obs, usuarioMod, pcodsql, presultado)
                '--- Fin Modificado SEP-2007-566 ---

                If va = False Or presultado <> "0" Then
                    Mensaje_tramite(23, "TR2302", presultado, pcodsql)
                Else
                    'mensaje ok
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "02", mensajeok, "")
                End If

        End Sub

        Private Sub DarColorControles()
            'PONER COLOR A LOS BOTONES
            Me.btnEnviar.BackColor = Session("WCOLOR_LINEA_NEGOCIO")
            'MOD PROYECTO NAVEGADORES - DENYS RONDAN - 05/08/2010 : Poner cursor al boton
            Me.btnEnviar.Style.Add("cursor", "pointer")
        End Sub

        Private Sub ObtieneSede()
            Dim var As String
            Session("WCOD_LOCAL") = C.Sede(conexion, Session("CUSUARIO"))
        End Sub

        'agregado para la SEP-2007-566
        Private Function validaDatos() As Boolean
            Dim txtPass As TextBox = CType(Login1.FindControl("Password"), TextBox)
            If Trim(txtObs2.Text) = "" Then
                lbl_Val_Entrega.Visible = True
                Return False
            Else
                lbl_Val_Entrega.Visible = False
            End If

            If Trim(txtPass.Text) = "" Then
                lbl_val_Confirma.Visible = True
                Return False
            Else
                lbl_val_Confirma.Visible = False
            End If
            Return True

        End Function


    End Class

End Namespace
