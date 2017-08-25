Imports ADTRAMITES.ADTRAMITES


Namespace tramites


Partial Class tr2302
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
        Public Property MensajeObservacion() As String
            Get
                Return ViewState("MensajeObservacion")
            End Get
            Set(ByVal value As String)
                ViewState("MensajeObservacion") = value
            End Set
        End Property
        ' ------- Fin agregado para la sep 2007-566 -----------


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

            ObtieneTitulo()

            ObtieneSede()

            ' verifica el punto en que se encuetntra la solicitud en el wf
            Dim wdeshabilita, es_eva, es_moni, ambiente As String
         wdeshabilita = "NO"



         Dim Estado As String = RTrim(Estado_Indicador())
         Dim wPuntoWF As String = C.PuntoWF(conexion, 23, Session("CODPERIODO"), Session("CODUNICO"))
         '*********************************
         'Response.Write(wPuntoWF & "<br\>")
         '*********************************
            If wPuntoWF = "BLK_PAGO" Then
                ambiente = "CAJA"
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
            'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - INICIO
            'Autorizado("TR2302", Session("CUSUARIO"), "8", "", es_eva)
            If sf_autoriza_reserva("TR2302", Session("CUSUARIO"), "8", "") = True Then
                es_eva = "SI"
            Else
                es_eva = "NO"
            End If
            'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - FIN
            es_moni = C.EsMonitor(conexion, Session("CODUNICO"), Session("CODPERIODO"), Session("CUSUARIO"))

            'Response.Write("es_eva " & es_eva & "<br>")
            'Response.Write("es_moni" & es_moni & "<br>")
            'Response.Write("ambiente " & ambiente & "<br>")
            'Response.Write("tipoper " & Session("WTIPO_PERSONA") & "<br>")

            '------------ SEP2007-566 -----------
            If ambiente = "CAJA" Then
                wdeshabilita = "SI"
            End If
            '------------------------------------

            If (es_eva = "SI" And ambiente = "REACTIVA") Or _
               (Session("WTIPO_PERSONA") = "COU" And ambiente = "CONFIRMA") Then
                wdeshabilita = "SI"
            End If

            If (es_eva = "SI" And (ambiente = "ENTREGA" Or ambiente = "") And Not Estado.Equals("NP")) Then

                Server.Transfer("tr2304.aspx", False)
            End If

            If (Session("WTIPO_PERSONA") = "COU" And (ambiente = "REACTIVA" Or ambiente = "")) Then
                Server.Transfer("tr2304.aspx", False)
            End If

            If (Session("WTIPO_PERSONA") = "COU" And ambiente = "ENTREGA") Then
                Server.Transfer("tr2304.aspx", False)
            End If

            If es_moni = "SI" And es_eva = "NO" Then
                Server.Transfer("tr2304.aspx", False)
            End If

            ' valida acceso a la pagina
            'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - INICIO
            'ValidaAutorizacion("TR2302", Session("CUSUARIO"), "1", "")
            If sf_autoriza_reserva("TR2302", Session("CUSUARIO"), "1", "") = True Then
            Else
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Usted no tiene permiso para ingresar a esta opción", "")
            End If
            'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - FIN

            If Not Page.IsPostBack Then
                btnEnviar.Attributes.Add("onclick", "return confirm('Está seguro de confirmar la solicitud?');")

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

                    Me.lblNumSol.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("COD_SOLICITUD")), "", dsTram.Tables(0).Rows(0)("COD_SOLICITUD")), String)
                    Me.lblFechaSol.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("FECHA_SOLICITUD")), "", dsTram.Tables(0).Rows(0)("FECHA_SOLICITUD")), String)

                    Select Case CType(dsTram.Tables(0).Rows(0)("ESTADO"), String)

                        Case "PE"
                            Me.lblEstadoSol.Text = "PENDIENTE"
                        Case "IN"
                            Me.lblEstadoSol.Text = "NO PAGÓ EN BANCO"
                        Case "PR"
                            Me.lblEstadoSol.Text = "PROCEDE"
                        Case "NP"
                            Me.lblEstadoSol.Text = "NO PROCEDE"
                        Case "PP"
                            Me.lblEstadoSol.Text = "PROCEDE PARCIALMENTE"
                            ' --- Agregado SEP-2007-566 ---
                        Case "EN"
                            Me.lblEstadoSol.Text = "ENTREGADO"

                            ' -----------------------------


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
                    'Me.hlDatos.NavigateUrl = "../../programas/ic0900op.asp?WALUMNO=" & dsTram.Tables(0).Rows(0)("COD_ALUMNO")
                    Dim querystring As String
                    querystring = "WALUMNO=" & dsTram.Tables(0).Rows(0)("COD_ALUMNO")
                    querystring = querystring & "&WHISTORIAL=SI"
                    querystring = Seguridad_All_Encrypt(querystring)
                    Me.hlDatos.NavigateUrl = "../../programas/ic0900op.asp?" & querystring
                    Me.hlDatos.ToolTip = "Consulta académica"
                    Me.hlDatos.Target = "_blank"

                    ' si ya fue confirmado O solamente guardado o rechazado(no procede), muestra datos del que confirmo
                    'Cambiado para la sep-2007-566
                    If dsTram.Tables(0).Rows(0)("IND_CONFIRMADO") = "SI" Or dsTram.Tables(0).Rows(0)("IND_GUARDADO") = "SI" Or CType(dsTram.Tables(0).Rows(0)("ESTADO"), String) = "NP" Then
                        Me.lblEvaluador.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("EVALUADOR")), "", dsTram.Tables(0).Rows(0)("EVALUADOR")), String)
                        Me.lblFechaEval.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("FECHA_EVALUADOR")), "", dsTram.Tables(0).Rows(0)("FECHA_EVALUADOR")), String)
                        Me.txtObs.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("OBSERVACION")), "", dsTram.Tables(0).Rows(0)("OBSERVACION")), String)
                    Else
                        If ambiente = "CONFIRMA" And es_eva = "SI" Then
                            Me.lblEvaluador.Text = C.NombreUsuario(conexion, Session("CUSUARIO"))
                            lblFechaEval.Text = C.FechaActual(conexion)
                            Dim formato, caja, boleta, CodSql, CodDocu
                            DatosTarjeta(dsTram.Tables(0).Rows(0)("COD_MOTIVO"), formato, caja, boleta, CodSql, CodDocu, txtObs.Text)
                        End If
                    End If

                    '---- Agregado para la SEP-2007-566 ----
                    MensajeObservacion = txtObs.Text.Trim
                    If dsTram.Tables(0).Rows(0)("ESTADO") = "NP" Then
                        wdeshabilita = "SI"
                    End If
                    '---- Fin agregado para la SEP-2007-566 ----

                    If dsTram.Tables(0).Rows(0)("IND_CONFIRMADO") = "SI" Then
                        wdeshabilita = "SI"
                    End If



                    Deshabilita(wdeshabilita)

                End If

                dsTram.Dispose()
                dsTram = Nothing
                objTram = Nothing
                objConfig = Nothing

            End If
        End Sub

    Private Sub ObtieneTitulo()
         lblTituloSol.Text = C.Titulo(conexion, Session("WCOD_LINEA_NEGOCIO"), "23")
    End Sub

    Private Sub EnviarPagina(ByVal PAGINA As String, ByVal TITULO As String, _
                                ByVal CODMENSAJE As String, ByVal MENSAJE As String, ByVal ERRORSQL As String)

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
        'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - INICIO

        'Private Sub ValidaAutorizacion(ByVal archivo As String, ByVal usuario As String, ByVal funcion As String, ByVal ip As String)
        '    Dim permiso, pcoderr, pdeserr As String
        '    If C.SF_AUTORIZACION(conexion, archivo, usuario, funcion, ip, permiso, pcoderr, pdeserr) Then
        '        If Not (CInt(permiso) = 1) Then
        '            ' NO TIENE PERMISO
        '            Mensaje_tramite(23, "TR2302", "1", "")
        '        End If
        '    Else
        '        Mensaje_tramite(23, "TR2302", pcoderr, pdeserr)
        '    End If
        'End Sub

        'Private Sub Autorizado(ByVal archivo As String, ByVal usuario As String, ByVal funcion As String, ByVal ip As String, ByRef entra As String)
        '    Dim permiso, pcoderr, pdeserr As String
        '    entra = "NO"
        '    If C.SF_AUTORIZACION(conexion, archivo, usuario, funcion, ip, permiso, pcoderr, pdeserr) Then
        '        If Not (CInt(permiso) = 1) Then
        '            ' NO TIENE PERMISO
        '            entra = "NO"
        '            Exit Sub
        '        End If
        '    Else
        '        entra = "NO"
        '        Exit Sub
        '    End If
        '    entra = "SI"
        'End Sub
        'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - FIN

    Private Sub Deshabilita(ByVal wdeshabilita As String)
        'deshabilita los campos necesarios, pues ya se confirmó al alumno o ingresa un usuario que no tiene permiso para confirmar
        If wdeshabilita = "SI" Then
            'boton Confirmar
            btnEnviar.Enabled = False
            btnEnviar.Visible = False
            'campo de observacion de evaluador
                txtObs.ReadOnly = True
                '--------- SEP-2007-566 cambio de combo y botón guardar-------------
                btnGuardar.Enabled = False
                btnGuardar.Visible = False
                ddlAccion.Enabled = False
                tr_accion.Visible = False
                '-------------------------------------

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
            If ddlAccion.SelectedValue <> 0 Then 'seleccionó una acción
                If ddlAccion.SelectedValue = 1 Then 'confirmó
                    Dim va As Boolean, pcodsql, presultado As String

                    If Trim(txtObs.Text) = "" Then
                        ClientScript.RegisterStartupScript(Me.GetType(), "scripalert", "<script languaje=javascript> javascript:alert('Debe ingresar una observación para la solicitud');</script>")
                        Exit Sub
                    End If

                    If Not Replace(Replace(Trim(txtObs.Text), Chr(10), ""), Chr(13), "").Equals(String.Empty) Then
                        va = C.ActualizaSolicitud(conexion, Session("WCOD_LINEA_NEGOCIO"), lblCodAlumno.Text, Session("CODUNICO") _
                       , Session("CodPeriodo"), "1", txtObs.Text, Session("CUSUARIO"), pcodsql, presultado)
                        If va = False Or presultado <> "0" Then
                            Mensaje_tramite(23, "TR2301", presultado, pcodsql)
                        Else
                            'mensaje ok
                            EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "02", "Se realizó la confirmación al alumno con éxito.", "")
                        End If
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType(), "scripalert", "<script languaje=javascript> javascript:alert('Debe ingresar una observación para la solicitud');</script>")
                    End If



                End If
                If ddlAccion.SelectedValue = 2 Then

                    If Trim(txtObs.Text) = "" Then
                        ClientScript.RegisterStartupScript(Me.GetType(), "scripalert", "<script languaje=javascript> javascript:alert('Debe ingresar una observación para la solicitud');</script>")
                        Exit Sub
                    End If

                    If ddlAccion.SelectedValue = 2 And (Not Replace(Replace(Trim(txtObs.Text), Chr(10), ""), Chr(13), "").Equals(String.Empty)) Then 'si la accion es NO procede e ingresó la glosa(observación)
                        Dim va As Boolean, pcodsql, presultado As String
                        va = C.ActualizaSolicitud(conexion, Session("WCOD_LINEA_NEGOCIO"), lblCodAlumno.Text, Session("CODUNICO") _
                            , Session("CodPeriodo"), "5", txtObs.Text, Session("CUSUARIO"), pcodsql, presultado)
                        If va = False Or presultado <> "0" Then
                            Mensaje_tramite(23, "TR2301", presultado, pcodsql)
                        Else
                            EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "02", "Se guardaron los cambios satisfactoriamente: la solicitud no procede. ", "")
                        End If
                    Else
                        If txtObs.Text.Equals(String.Empty) Then
                            ClientScript.RegisterStartupScript(Me.GetType(), "scripalert", "<script languaje=javascript> javascript:alert('Debe ingresar una observación para cancelar la solicitud');</script>")
                        End If
                    End If
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "scripalert", "<script languaje=javascript> javascript:alert('Debe seleccionar una acción a tomar para enviar la solicitud');</script>")
            End If

        End Sub

    Private Sub DarColorControles()
        'PONER COLOR A LOS BOTONES
            Me.btnEnviar.BackColor = Session("WCOLOR_LINEA_NEGOCIO")
            Me.btnGuardar.BackColor = Session("WCOLOR_LINEA_NEGOCIO")
            'MOD PROYECTO NAVEGADORES - DENYS RONDAN - 05/08/2010 : Poner cursor al boton
            Me.btnEnviar.Style.Add("cursor", "pointer")
            Me.btnGuardar.Style.Add("cursor", "pointer")
    End Sub

    Private Sub ObtieneSede()
         Session("WCOD_LOCAL") = C.Sede(conexion, Session("CUSUARIO"))
        End Sub
        'AGREGADO SEP-2007-566 ----
        Protected Sub ddlAccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAccion.SelectedIndexChanged
            If ddlAccion.SelectedValue = 2 Then
                MensajeObservacion = txtObs.Text.Trim
                txtObs.Text = String.Empty
            Else
                If ddlAccion.SelectedValue = 1 Then
                    txtObs.Text = MensajeObservacion
                Else
                    txtObs.Text = String.Empty
                End If
             End If

        End Sub
      'AGREGADO SEP-2007-566 ----
      Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
         Dim va As Boolean, pcodsql, presultado As String
         '4 -----> Flag para sólo actualizar la solicitud, no cambia ningun indicador o estado.

         va = C.ActualizaSolicitud(conexion, Session("WCOD_LINEA_NEGOCIO"), lblCodAlumno.Text, Session("CODUNICO") _
             , Session("CodPeriodo"), "4", txtObs.Text, Session("CUSUARIO"), pcodsql, presultado)

         If va = False Or presultado <> "0" Then
            Mensaje_tramite(23, "TR2301", presultado, pcodsql)
         Else
            'mensaje ok
            EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "02", "Se guardaron los cambios de la solicitud con éxito.", "")
         End If
      End Sub
      'AGREGADO SEP-2007-566 ----
      Private Function Estado_Indicador() As String
         Dim objTram As New clsTramites, objConfig As New Configuracion
         Dim dsTram As New DataSet
         objTram.CadenaConexion = conexion
         dsTram = objTram.ConsultaAlumno(CType(Session("CODUNICO"), String), CType(Session("CODPERIODO"), String))
         Try
            Dim strMsgError As String = objTram.ErrorMensaje
            If dsTram.Tables(0).Rows.Count > 0 Then
               Return CType(dsTram.Tables(0).Rows(0)("ESTADO"), String)
            End If
            Return "SIN ESTADO"
         Catch
            Return "SIN ESTADO"
         End Try
      End Function

    End Class

End Namespace
