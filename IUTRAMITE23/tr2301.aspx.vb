Imports ADTRAMITES.ADTRAMITES


Namespace tramites


Partial Class tr2301
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            Session("CUSUARIO") = "CROJAS"
            Session("WCOD_LINEA_NEGOCIO") = "U"


            If Convert.ToString(Session("CUSUARIO")) = "" Then
                EnviarPagina("tr23resultado.aspx", "Sócrates - Intranet", "010", "", "")
                Exit Sub
            End If

            ObtieneTitulo()

            ObtieneSede()

            ' VALIDA AUTORIZACION EN EL APLICATIVO
            'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - INICIO
            'ValidaAutorizacion("TR2301", Session("CUSUARIO"), "1", "")

            If sf_autoriza_reserva("TR2301", Session("CUSUARIO"), 1, "") = True Then
            Else
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Usted no tiene permiso para ingresar a esta opción", "")
            End If
            'Response.End()
            'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - FIN

            If Not Page.IsPostBack Then
                'obtieneFecha()
                lblFechaSol.Text = C.FechaActual(conexion)

                If Session("WTIPO_PERSONA") = "ALU" Then
                    Session("WCOD_ALUMNO") = C.CodAlumno(conexion, Session("WCOD_LINEA_NEGOCIO"), Session("CUSUARIO"))
                Else
                    Session("WCOD_ALUMNO") = Session("COD_ALUMNO_ENC")
                End If

                btnEnviar.Attributes.Add("onclick", "return confirm('Está seguro de enviar la solicitud?');")
                ObtieneDatosAlu(Session("WCOD_LINEA_NEGOCIO"), Session("WCOD_ALUMNO"))

                ObtieneDocumentos()
                ddlModalidad.Visible = False
                Div1.Visible = False
                dv_Pago.Visible = False
                Limpiar()
                DarColorControles()
                '------ CSC-00262755-00:(05/01/2016) LTORRES Si es alumno OI debe de tener habilitada solo la opcion pago en banco
                Dim wEs_AlumnOI As String
                wEs_AlumnOI = "NO"
                Es_AlumnOI(conexion, Session("WCOD_LINEA_NEGOCIO"), Session("WCOD_ALUMNO"), wEs_AlumnOI)
                If wEs_AlumnOI = "SI" Then
                    rglTipoPago.Items(1).Enabled = False
                End If
                '------ CSC-00262755-00:(05/01/2016) LTORRES Si es alumno OI debe de tener habilitada solo la opcion pago en banco

            End If
        End Sub

    Private Sub ValidaProgramacion(ByVal codmodal As String, ByRef ppara As String)
        Dim wresult1 As Integer
        Dim presultado, pcoderror, pdeserror, wtexto, wtexto1, wtexto2, wtexto3, perr As String
        ppara = "NO"
        wresult1 = C.VerificaFechas(conexion, Session("WCOD_LINEA_NEGOCIO"), codmodal, 23, Session("WCOD_LOCAL"), presultado, pcoderror, pdeserror)

        'error generico
        If pcoderror <> "0" Then
            Mensaje_tramite(23, "TR2301", pcoderror, pdeserror)
        End If

        If wresult1 <> 1 Then
            If presultado = "" Then
                C.Mensaje(conexion, "TR2301", 33, 23, Session("WCOD_LOCAL"), wtexto, wtexto1, wtexto2, wtexto3, perr)
            Else
                wtexto = presultado
            End If

            Div1.Visible = True
            dv_Pago.Visible = False
            lblErrorUsu.Text = wtexto
            btnEnviar.Visible = False
            ppara = "SI"
        End If
    End Sub

    Private Sub Limpiar()
        lblModalidad.Text = ""
        lblCodModalidad.Text = ""
        lblCiclo.Text = ""
        lblCodProducto.Text = ""
        lblProducto.Text = ""
        lblMatriculaId.Text = ""
        btnEnviar.Visible = False
        Div1.Visible = False
        dv_Pago.Visible = False
    End Sub

    Private Sub ObtieneFecha()
        lblFechaSol.Text = C.FechaActual(conexion)
    End Sub

    Private Sub ObtieneTitulo()
        Dim var As String
        lblTituloSol.Text = C.Titulo(conexion, Session("WCOD_LINEA_NEGOCIO"), "23")
    End Sub

    Private Sub ObtieneSede()
        Dim var As String
        Session("WCOD_LOCAL") = C.Sede(conexion, Session("CUSUARIO"))
    End Sub

    Private Sub ObtieneDocumentos()
        Dim va As Boolean, perr As String
        va = C.ListaDocumentos(conexion, Session("WCOD_LINEA_NEGOCIO"), Me.ddlDocumento, perr)
        If va = False Then
            Mensaje_tramite(23, "TR2301", 5, perr)
        End If
    End Sub

    Private Sub DatosMatricula(ByVal pformato As String, ByRef ppara As String)
        Dim objTram As New clsTramites, dsTram As New DataSet, Y As Integer
        Dim wtexto, wtexto1, wtexto2, wtexto3, perr As String, FILA As DataRow

        objTram.CadenaConexion = conexion
        dsTram = objTram.DatosMatricula(Session("WCOD_LINEA_NEGOCIO"), CType(Session("WCOD_ALUMNO"), String))

        Dim strMsgError As String = objTram.ErrorMensaje

        ' si no está matriculado, no lo deja pasar
        If dsTram Is Nothing Then
            If strMsgError <> "" Then
                Mensaje_tramite(23, "TR2301", 1, strMsgError)
            End If
        ElseIf dsTram.Tables(0).Rows.Count = 0 Then
            C.Mensaje(conexion, "TR2301", 2, 23, Session("WMANEJA_LOCAL"), wtexto, wtexto1, wtexto2, wtexto3, perr)
            Div1.Visible = True
            lblErrorUsu.Text = wtexto
            ' SI ES CU PARA
            If pformato = "1" Then
                ppara = "SI"
            Else ' FORMATO TIU NO PARA , PUES VERIFICA RESERVA
                ppara = "NO"
            End If
            dsTram.Dispose()
            dsTram = Nothing
            objTram = Nothing
            Exit Sub
        End If

        ' Está matriculado en un sólo ciclo, modalidad, producto de trámite
        If dsTram.Tables(0).Rows.Count = 1 Then
            ddlModalidad.Visible = False
            lblCodModalidad.Text = dsTram.Tables(0).Rows(0)("COD_MODAL_EST")
            lblMatriculaId.Text = dsTram.Tables(0).Rows(0)("Id")
            lblCiclo.Text = dsTram.Tables(0).Rows(0)("COD_PERIOD_MAT")
            lblModalidad.Text = dsTram.Tables(0).Rows(0)("COD_MODAL_EST") & " - " & dsTram.Tables(0).Rows(0)("NOMBRE")
            lblCodProducto.Text = dsTram.Tables(0).Rows(0)("COD_PRODUC_MAT")
            lblProducto.Text = dsTram.Tables(0).Rows(0)("desp")
            'SI ES CU, NO PARA, SI ES TIU SI PARA 
            If pformato = "2" Then
                ppara = "SI"
            Else
                ppara = "NO"
            End If
        Else

            ddlModalidad.Visible = True
            ddlModalidad.Items.Add("-- Seleccione un ciclo, modalidad--")
            ddlModalidad.Items.Item(0).Value = "0"

            Y = 1
            For Each FILA In dsTram.Tables(0).Rows
                ddlModalidad.Items.Add(Convert.ToString(FILA("desc_larga")))
                ddlModalidad.Items.Item(Y).Value = Convert.ToString(FILA("Id"))
                Y += 1
            Next

            Limpiar()
            ppara = "SI"
        End If
        Div1.Visible = False

        dsTram.Dispose()
        dsTram = Nothing
        objTram = Nothing
    End Sub

    Private Sub DatosReserva(ByRef ppara As String)
        Dim objTram As New clsTramites, dsTram As New DataSet, FILA As DataRow
        Dim wtexto, wtexto1, wtexto2, wtexto3, perr As String, Y As Integer

        objTram.CadenaConexion = conexion
            dsTram = objTram.DatosReserva(Trim(Session("WCOD_LINEA_NEGOCIO")), CType(Trim(Session("WCOD_ALUMNO")), String))
            

        Dim strMsgError As String = objTram.ErrorMensaje

        ' si no tiene reserva, no lo deja pasar
        If dsTram Is Nothing Then
            If strMsgError <> "" Then
                Mensaje_tramite(23, "TR2301", 15, strMsgError)
            End If
        ElseIf dsTram.Tables(0).Rows.Count = 0 Then
            ' no tiene reserva
            C.Mensaje(conexion, "TR2301", 16, 23, Session("WMANEJA_LOCAL"), wtexto, wtexto1, wtexto2, wtexto3, perr)
            Div1.Visible = True
            lblErrorUsu.Text = wtexto
            dsTram.Dispose()
            dsTram = Nothing
            objTram = Nothing

            ppara = "SI"
            btnEnviar.Visible = False
            Exit Sub
        End If

        ' Tiene reserva en un sólo ciclo, modalidad, producto de trámite
        If dsTram.Tables(0).Rows.Count = 1 Then
            ddlModalidad.Visible = False
            lblCodModalidad.Text = dsTram.Tables(0).Rows(0)("COD_MODAL_EST")
            lblMatriculaId.Text = dsTram.Tables(0).Rows(0)("Id")
                'lblCiclo.Text = dsTram.Tables(0).Rows(0)("COD_PERIOD_MAT")
                lblCiclo.Text = dsTram.Tables(0).Rows(0)("COD_PERIODO")
            lblModalidad.Text = dsTram.Tables(0).Rows(0)("COD_MODAL_EST") & " - " & dsTram.Tables(0).Rows(0)("NOMBRE")
            lblCodProducto.Text = dsTram.Tables(0).Rows(0)("COD_PRODUC_MAT")
            lblProducto.Text = dsTram.Tables(0).Rows(0)("desp")
            ppara = "NO"
        Else
            'llena combo, mas de una reserva
            ddlModalidad.Visible = True
            ddlModalidad.Items.Add("-- Seleccione un ciclo, modalidad--")
            ddlModalidad.Items.Item(0).Value = "0"

            Y = 1
            For Each FILA In dsTram.Tables(0).Rows
                ddlModalidad.Items.Add(Convert.ToString(FILA("desc_larga")))
                ddlModalidad.Items.Item(Y).Value = Convert.ToString(FILA("Id"))
                Y += 1
            Next
            'Limpiar()
            ppara = "NO"
        End If
        Div1.Visible = False

        dsTram.Dispose()
        dsTram = Nothing
        objTram = Nothing
    End Sub

    Private Sub ObtieneDatosAlu(ByVal plinea As String, ByVal pcodalu As String)
        Dim objTram As New clsTramites
        Dim objConfig As New Configuracion
        Dim dsTram As New DataSet

        objTram.CadenaConexion = conexion
        dsTram = objTram.DatosAlumno(plinea, pcodalu)

        Dim strMsgError As String = objTram.ErrorMensaje, strTele As String

        ' no pudo leer los datos del alumno
        If dsTram Is Nothing Then
            If strMsgError <> "" Then
                Mensaje_tramite(23, "TR2301", 3, strMsgError)
            End If
        ElseIf dsTram.Tables(0).Rows.Count = 0 Then
            ' no se encontro los datos del alumno
            Mensaje_tramite(23, "TR2301", 4, strMsgError)
        End If
        strTele = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("telefonos")), "", dsTram.Tables(0).Rows(0)("telefonos")), String)
        lblAlumnoNombre.Text = Session("WCOD_ALUMNO") & " - " & dsTram.Tables(0).Rows(0)("nombres")
        If strTele = "" Then
            lblAlumnoTelef.Text = ""
        Else
            lblAlumnoTelef.Text = Mid(strTele, 1, Len(strTele) - 2)
        End If

        lnkAlumnoMail.Text = CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("email")), "", dsTram.Tables(0).Rows(0)("email")), String) 'dsTram.Tables(0).Rows(0)("email")
        lnkAlumnoMail.NavigateUrl = "mailto:" & CType(IIf(IsDBNull(dsTram.Tables(0).Rows(0)("email")), "", dsTram.Tables(0).Rows(0)("email")), String) 'dsTram.Tables(0).Rows(0)("email")

        dsTram.Dispose()
        dsTram = Nothing
        objTram = Nothing

        objConfig.CadenaConexion = conexion
        If objConfig.Configuracion(pcodalu, plinea, CType(Request.ServerVariables("APPL_PHYSICAL_PATH"), String)) Then
            Me.imgFoto.ImageUrl = objConfig.RUTA_IMAGEN_URL()
        End If
    End Sub
    Private Sub EnviarPagina(ByVal PAGINA As String, ByVal TITULO As String, _
                                ByVal CODMENSAJE As String, ByVal MENSAJE As String, ByVal ERRORSQL As String)

        Me.lblTitulo.Text = TITULO
        Me.lblCODMENSAJE.Text = CODMENSAJE
        Me.lblMensajeError1.Text = MENSAJE
        Me.lblerrorSQL.Text = ERRORSQL
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

    Private Sub ddlDocumento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlDocumento.SelectedIndexChanged
        Dim WFORMATO As String, PPARA As String, wnum, wtexto, wtexto1, wtexto2, wtexto3, perr As String

            '------------ sep-2007-566
            Dim nerror, deserror As String
            nerror = ""
            deserror = ""
            '------------ sep-2007-566

        Limpiar()
        ddlModalidad.Visible = False

        If ddlDocumento.SelectedValue <> "0" Then

            ' VALIDA EL INGRESO SEGUN EL DOCUMENTO SELECCIONADO
                Valida_Ingreso(conexion, Session("WCOD_LINEA_NEGOCIO"), Session("WCOD_ALUMNO"), ddlDocumento.SelectedValue, Session("WTIPO_PERSONA"), PPARA)

                If PPARA = "NO" Then
                    ' Obtiene datos del tipo de documento
                    DatosTarjeta(ddlDocumento.SelectedValue, WFORMATO, lblCaja.Text, lblboleta.Text, lblCodSql.Text, lblCodDocu.Text)


                    wnum = C.SolicitudPendiente(conexion, Session("WCOD_LINEA_NEGOCIO"), Session("WCOD_ALUMNO"), lblCodDocu.Text)
                    If wnum <> "0" Then
                        C.Mensaje(conexion, "TR2301", 35, 23, Session("WMANEJA_LOCAL"), wtexto, wtexto1, wtexto2, wtexto3, perr)
                        Div1.Visible = True
                        lblErrorUsu.Text = wtexto
                        Exit Sub
                    End If
                    'obtiene la matricula del alumno
                    DatosMatricula(WFORMATO, PPARA)

                    ' SI ES FORMATO TIU Y NO TIENE MATRICULA, VERIFICA SI TIENE RESERVA
                    If PPARA = "NO" And WFORMATO = 2 Then
                        DatosReserva(PPARA)
                    ElseIf PPARA = "SI" And WFORMATO = 2 Then
                        'SI TIENE MATRICULA Y ES TIU
                        PPARA = "NO"
                    End If

                    ' si no para, valida dado de baja y egreso 
                    If PPARA = "NO" Then
                        ' valida fecha del tramite
                        ValidaProgramacion(lblCodModalidad.Text, PPARA)
                        If PPARA = "NO" Then
                            Valida_IngMatri(conexion, Session("WCOD_LINEA_NEGOCIO"), Session("WCOD_ALUMNO"), _
                            ddlDocumento.SelectedValue, lblCodModalidad.Text, lblCiclo.Text, lblProducto.Text, Session("WTIPO_PERSONA"), PPARA)
                            ' si no para, muestra opciones de pago
                            If PPARA = "NO" Then
                                MuestraPago(ddlDocumento.SelectedValue, Session("WTIPO_PERSONA"))
                            End If
                        End If
                    End If

                    '-----SEP-2007-566 - Verificamos que el trámites del documento esté en el rago de fechas 

                    If PPARA = "NO" Then
                        Valida_ListaDoc(conexion, Session("WCOD_LINEA_NEGOCIO"), "23", ddlDocumento.SelectedValue, "", nerror, deserror)
                    End If

                End If
            End If

        End Sub

        '-------- AGREGADO SEP-2007-566------
        Private Sub Valida_ListaDoc(ByVal conexion As String, ByVal plinea As String, _
                ByVal ptramite As String, ByVal pmotivo As String, ByVal pdocumento As String, ByVal pNError As String, ByVal pDesError As String)

            Dim resultado As Boolean


            lblErrorUsu.Text = ""
            resultado = C.Valida_ListaDoc(conexion, plinea, "23", pmotivo, pdocumento, pNError, pDesError)
          
            'resultado = C.Valida_ListaDoc(conexion, "U", "23", "31", "", pNError, pDesError)

            If pNError = "-1" Then
                Mensaje_tramite(23, "TR2301", "99", "Lo sentimos, la intranet está no disponible.")
            End If

            'MENSAJE EN LA PANTALLA
            If Trim(pDesError) = "" Then
                Div1.Visible = False
            Else
                Div1.Visible = True
                dv_Pago.Visible = False
                lblErrorUsu.Text = pDesError
                btnEnviar.Visible = False
                Exit Sub
            End If

        End Sub


        '-------- AGREGADO SEP-2007-566------

    Private Sub Valida_Ingreso(ByVal conexion As String, ByVal plinea As String, _
        ByVal palumno As String, ByVal pmotivo As String, ByVal ptipoUsu As String, ByRef PPARA As String)

        Dim pCodError, pMenError

        lblErrorUsu.Text = ""
        C.ValidaIngreso(conexion, plinea, palumno, pmotivo, ptipoUsu, pCodError, pMenError, PPARA)

        'error generico
        If pCodError = "99" Then
            Mensaje_tramite(23, "TR2301", "99", pMenError)
        End If

        'MENSAJE EN LA PANTALLA
        If pCodError = "0" Then
            Div1.Visible = False
        Else
            Dim wtexto, wtexto1, wtexto2, wtexto3, perr As String
            C.Mensaje(conexion, "TR2301", pCodError, 23, Session("WMANEJA_LOCAL"), wtexto, wtexto1, wtexto2, wtexto3, perr)
            Div1.Visible = True
            lblErrorUsu.Text = wtexto
            lblerrorSQL.Text = pMenError

            ' si pPara = SI, se detiene y no debe permitir enviar la solicitud
            If PPARA = "SI" Then
                btnEnviar.Visible = False
                Exit Sub
            Else
                btnEnviar.Visible = True
            End If
        End If

        End Sub


        'Valida_ListaDocSTRING
    Private Sub DarColorControles()
            'PONER COLOR A LOS BOTONES
            Me.btnEnviar.BackColor = Session("WCOLOR_LINEA_NEGOCIO")

    End Sub

    Private Sub DatosTarjeta(ByVal pmotivo As String, ByRef wcodformato As String, _
    ByRef pcaja As String, ByRef pboleta As String, ByRef pcodsql As String, ByRef pCodDocu As String)
        Dim wdesdocu, wminvez, wmaxvez, perr, pmensaje As String

        C.DatosDocumento(conexion, pmotivo, pCodDocu, wdesdocu, wcodformato, wminvez, wmaxvez, pcaja, pboleta, pcodsql, pmensaje, perr)
        If perr = "" Then
        Else
            Mensaje_tramite(23, "TR2301", 6, perr)
        End If

    End Sub

    Private Sub ddlModalidad_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlModalidad.SelectedIndexChanged
        Dim pmodal, pnombre, pciclo, pproducto, pdesproducto, perror, ppara As String
        If ddlDocumento.SelectedValue = "0" Then
            Limpiar()
        Else
            C.ObtieneDatosMatricula(conexion, ddlModalidad.SelectedValue, pmodal, pnombre, pciclo, pproducto, pdesproducto, perror)
            'ddlModalidad.Visible = False
            lblModalidad.Visible = False
            lblCodModalidad.Text = pmodal
            lblMatriculaId.Text = ddlDocumento.SelectedValue
            lblCiclo.Text = pciclo
            lblModalidad.Text = pmodal & " - " & pnombre
            lblCodProducto.Text = pproducto
            lblProducto.Text = pdesproducto

            ' valida fecha del tramite
            ValidaProgramacion(lblCodModalidad.Text, ppara)
            If ppara = "NO" Then
                'valida dado de baja y egresado
                Valida_IngMatri(conexion, Session("WCOD_LINEA_NEGOCIO"), Session("WCOD_ALUMNO"), _
                ddlDocumento.SelectedValue, pmodal, pciclo, pproducto, Session("WTIPO_PERSONA"), ppara)
                If ppara = "NO" Then
                    MuestraPago(ddlDocumento.SelectedValue, Session("WTIPO_PERSONA"))
                End If
            End If
        End If
    End Sub

    Private Sub MuestraPago(ByVal pmotivo As String, ByVal ptipousu As String)
        Dim pprecio, pmoneda, pmensaje As String

        '***** tipo 0: no tiene pago, 1:pago en caja, 2 pago en boleta
        lblTPago.Text = "0"

        ' si no tiene codigo de precio ingresado, no tiene pago
        If lblCodSql.Text = "" Or (lblCaja.Text = "NO" And lblboleta.Text = "NO") Then
            dv_Pago.Visible = False
            Exit Sub
        Else
            If C.PrecioSpring(conexion, Session("WCOD_LINEA_NEGOCIO"), lblCodModalidad.Text, _
            lblCiclo.Text, lblCodSql.Text, pprecio, pmoneda, pmensaje) = False Then
                ' no se pudo obtener el precio
                Div1.Visible = True
                lblErrorUsu.Text = pmensaje
                btnEnviar.Visible = False
                Exit Sub
            Else
                lblPrecio.Text = Mid(pmensaje, 1, 4) & " " & pprecio
                lblCosto.Text = pprecio
                dv_Pago.Visible = True
            End If
        End If

        If lblCaja.Text = "SI" And lblboleta.Text = "SI" Then
            ' counter solo puede pagar en caja
            If ptipousu = "COU" Then
                rglTipoPago.Visible = False
                lblTPago.Text = "1"
                lblTipoPago.Visible = True
                    lblTipoPago.Text = "Pagar en banco"
            Else
                rglTipoPago.Visible = True
                lblTPago.Text = "1"
                lblTipoPago.Visible = False
            End If
        End If

        If lblCaja.Text = "SI" And lblboleta.Text = "NO" Then
            rglTipoPago.Visible = False
            lblTPago.Text = "1"
            lblTipoPago.Visible = True
                lblTipoPago.Text = "Pagar en banco"
        End If

        If lblCaja.Text = "NO" And lblboleta.Text = "SI" Then
            ' counter solo puede pagar en caja
            If ptipousu = "COU" Then
                    lblErrorUsu.Text = "El trámite no está configurado para pagar en banco."
                dv_Pago.Visible = True
                Exit Sub
            Else
                rglTipoPago.Visible = False
                lblTipoPago.Visible = True
                lblTPago.Text = "2"
                lblTipoPago.Text = "Cargar a la próxima boleta"
            End If
        End If

    End Sub
    Private Sub Valida_IngMatri(ByVal conexion As String, ByVal plinea As String, _
        ByVal palumno As String, ByVal pmotivo As String, ByVal pmodal As String, _
        ByVal pciclo As String, ByVal pproducto As String, ByVal ptipoUsu As String, ByRef PPARA As String)

        Dim pCodError, pMenError

        PPARA = "NO"
        lblErrorUsu.Text = ""
        C.ValidaIngMatri(conexion, plinea, palumno, pmotivo, pmodal, pciclo, pproducto, ptipoUsu, pCodError, pMenError, PPARA)

        'error generico
        If pCodError = "99" Then
            Mensaje_tramite(23, "TR2301", "99", pMenError)
        End If

        'MENSAJE EN LA PANTALLA
        If pCodError = "0" Then
            Div1.Visible = False
            btnEnviar.Visible = True
        Else
            Dim wtexto, wtexto1, wtexto2, wtexto3, perr As String
            C.Mensaje(conexion, "TR2301", pCodError, 23, Session("WMANEJA_LOCAL"), wtexto, wtexto1, wtexto2, wtexto3, perr)
            Div1.Visible = True
            lblErrorUsu.Text = wtexto
            lblerrorSQL.Text = pMenError

            'si pPara = SI, se detiene y no debe permitir enviar la solicitud
            PPARA = "SI"
            btnEnviar.Visible = False
            Exit Sub
        End If

    End Sub

        Private Sub btnEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
            'CSC-00262755-00:(05/01/2016) Crear persona en spring
            Crea_AlumnOI_Spr(conexion, Session("WCOD_LINEA_NEGOCIO"), Session("WCOD_ALUMNO"), Convert.ToString(Session("CUSUARIO")))
            'CSC-00262755-00:(05/01/2016) Crear persona en spring
            If validaDatos() Then
                Server.Transfer("tr23envio.aspx", True)
            End If
        End Sub

    Private Function validaDatos() As Boolean
        If ddlDocumento.SelectedValue = "0" Then
            Div1.Visible = True
            lblErrorUsu.Text = "Debe seleccionar un tipo de documento."
            Return False
        End If
        If lblCodModalidad.Text = "" Then
            Div1.Visible = True
            lblErrorUsu.Text = "No se ha seleccionado una modalidad de estudio."
            Return False
        End If

        If txtSustentoSol.Text = "" Then
            Div1.Visible = True
            lblErrorUsu.Text = "Debe ingresar el sustento de la solicitud."
            Return False
        End If

        If lblTPago.Text = "" Then
            Div1.Visible = True
            lblErrorUsu.Text = "Debe seleccionar el tipo de pago."
            Return False
        End If

        Return True
    End Function

    Private Sub ValidaAutorizacion(ByVal archivo As String, ByVal usuario As String, ByVal funcion As String, ByVal ip As String)
        Dim permiso, pcoderr, pdeserr As String
        If C.SF_AUTORIZACION(conexion, archivo, usuario, funcion, ip, permiso, pcoderr, pdeserr) Then
            If Not (CInt(permiso) = 1) Then
                ' NO TIENE PERMISO
                Mensaje_tramite(23, "TR2301", "37", "")
            End If
        Else
            Mensaje_tramite(23, "TR2301", pcoderr, pdeserr)
        End If
    End Sub

    Private Sub rglTipoPago_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rglTipoPago.SelectedIndexChanged
        If rglTipoPago.SelectedValue = 1 Then
            lblTPago.Text = 1
        Else
            lblTPago.Text = 2
        End If

End Sub

        'CSC-00262755-00:(05/01/2016) Adaptar Tramite para alumno OI.
        Private Sub Es_AlumnOI(ByVal conexion As String, ByVal pcod_linea_negocio As String, ByVal pcod_alumno As String, ByRef pEs_AlumoOI As String)
            Dim pcoderr, pdeserr As String

            pEs_AlumoOI = C.Es_AlumnOI(conexion, pcod_linea_negocio, pcod_alumno, pcoderr, pdeserr)

            lblErrorUsu.Text = pdeserr
        End Sub
        'CSC-00262755-00:(05/01/2016) Adaptar Tramite para alumno OI.
        Private Sub Crea_AlumnOI_Spr(ByVal conexion As String, ByVal pcod_linea_negocio As String, ByVal pcod_alumno As String, ByVal pCod_usuario As String)
            Dim pCreo As String
            If C.Es_CreaPersonaOI_Spr(conexion, pcod_linea_negocio, pcod_alumno, pCod_usuario) Then
                pCreo = "OK"
            End If

        End Sub
End Class

End Namespace
