Imports ADSQLTRAMITES.ADSQLTRAMITES
Imports ADTRAMITES.ADTRAMITES


Namespace tramites

Partial Class tr23envio
        'Inherits Custom3DevProviders.SeguridadBasePage
        Inherits System.Web.UI.Page
    Dim C As New clsTramites
    Dim SQL As New ADSQLTRAMITES.ADSQLTRAMITES

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

        If Convert.ToString(Session("CUSUARIO")) = "" Then
            EnviarPagina("tr23resultado.aspx", "Sócrates - Intranet", "010", "", "")
            Exit Sub
        End If
        If Not Page.IsPostBack Then
            Dim thisPage As System.Web.UI.Page = CType(Context.Handler, System.Web.UI.Page)
            Dim LCOD_MODAL_EST As Label = CType(thisPage.FindControl("lblCodModalidad"), Label)
            Dim LCOD_PERIODO As Label = CType(thisPage.FindControl("lblciclo"), Label)
            Dim LCODMOTIVO As DropDownList = CType(thisPage.FindControl("ddldocumento"), DropDownList)
            Dim LTIPOPAGO As Label = CType(thisPage.FindControl("lblTPago"), Label)
            Dim LCOD_TRAMITE As String = "23"
            Dim LCOD_USUARIO As String = Convert.ToString(Session("CUSUARIO"))
            Dim LSUSTENTO As TextBox = CType(thisPage.FindControl("txtSustentoSol"), TextBox)
            Dim LCOD_ALUMNO As String = Session("WCOD_ALUMNO")
            Dim LID_MATRICULA As Label = CType(thisPage.FindControl("lblMatriculaID"), Label)
            Dim LCODSQL As Label = CType(thisPage.FindControl("lblCodSql"), Label)
            Dim LPRECIO As Label = CType(thisPage.FindControl("lblCosto"), Label)
            Dim LDESPRECIO As Label = CType(thisPage.FindControl("lblPrecio"), Label)
            Dim LFORMAPAGO As String = "005"
            Dim LDIAS_PAGO As String = "30"
            Dim LCANTIDAD As String = "1"
            Dim LNUMERO_CUOTAS As String = "1"
            Dim LCOD_PRODUCTO As Label = CType(thisPage.FindControl("lblCodProducto"), Label)
            Dim LCOD_DOCUMENTO As Label = CType(thisPage.FindControl("lblCodDocu"), Label)

            Dim LNUM_DIAS As String, pnumcuotas As String, pcuotasfac As String, pdeuda As String
            Dim pcompania, psucursal, pvendedor, pdesvendedor As String
            Dim presultado, psql As String, LCUOTAS_DISPONIBLES As Integer

            Dim LCODUNICO, LFECHA_SOLICITUD, pnumdocu As String
                Dim strMensaje1, strMensaje2 As String
            If LTIPOPAGO.Text = "" Or LTIPOPAGO Is Nothing Then
                LTIPOPAGO.Text = "0"
            End If

            ObtieneTitulo()
            ' ******** si es pago en caja
            If LTIPOPAGO.Text = "1" Then
                ObtieneDiasPago(LCOD_MODAL_EST.Text, LCOD_PERIODO.Text, LCOD_TRAMITE, LNUM_DIAS)
            End If

            '***************** si es pago en boleta, valida otras cosas
            If LTIPOPAGO.Text = "2" Then
                ObtieneVendedor(pvendedor, pdesvendedor)

                ValidaDatos(LCOD_MODAL_EST.Text, LCOD_PERIODO.Text, LCOD_ALUMNO, _
                    pnumcuotas, pcuotasfac, pdeuda, pcompania, psucursal, presultado)

                ' no pasa la validacion
                If presultado <> "0" Then
                    ' ALUMNO MOROSO
                    If presultado = "2" Then
                        Mensaje_tramite(23, "TR23ENVIO", presultado, pdeuda)
                    Else
                        Mensaje_tramite(23, "TR23ENVIO", presultado, "")
                    End If
                Else
                    LCUOTAS_DISPONIBLES = CDbl(pnumcuotas) - CDbl(pcuotasfac)
                    If LCUOTAS_DISPONIBLES <= 0 And presultado = "0" Then
                        Mensaje_tramite(23, "TR23ENVIO", "11", "")
                    Else
                        PAGA_BOLETA(LCODSQL.Text, pcompania, psucursal, Session("WCOD_LINEA_NEGOCIO"), _
                            LCOD_ALUMNO, LCOD_MODAL_EST.Text, LCOD_PERIODO.Text, LPRECIO.Text, _
                            LFORMAPAGO, LDIAS_PAGO, pvendedor, pdesvendedor, LPRECIO.Text, _
                            pcuotasfac, LCANTIDAD, pnumdocu)
                    End If
                End If
            End If

                'Ticket CSC-00256893-00 SGR 19-01-2009
                Dim wnum As Integer = C.SolicitudPendiente(conexion, Session("WCOD_LINEA_NEGOCIO"), Session("WCOD_ALUMNO"), LCOD_DOCUMENTO.Text)
                If wnum <> "0" Then
                    If LTIPOPAGO.Text = "0" Then 'NO TIENE PAGO
                        strMensaje1 = "Existe una solicitud que fue enviada con éxito."
                    ElseIf LTIPOPAGO.Text = "1" Then 'PAGO EN CAJA
                        strMensaje1 = "Tiene " & LNUM_DIAS & " días útiles para cancelar en el banco la suma de " & LDESPRECIO.Text & "."
                    ElseIf LTIPOPAGO.Text = "2" Then 'PAGA EN BOLETA
                        strMensaje1 = "Se cargó en su próxima boleta la suma de " & LDESPRECIO.Text & "."
                    End If
                    strMensaje2 = "<b>Número de solicitud:</b> " & Session("LCOD_UNICO") & "<br>" & _
                      "<b>Ciclo académico:</b> " & LCOD_PERIODO.Text & "<br>" & _
                      "<b>Fecha de la solicitud:</b> " & Session("LFECHA_SOLICITUD")
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "02", strMensaje1, strMensaje2)
                    Session("LCOD_UNICO") = Nothing
                    Session("LFECHA_SOLICITUD") = Nothing
                End If
            ' registra el tramite
            C.InsertaSolicitud(conexion, LCODUNICO, LFECHA_SOLICITUD, Session("WCOD_LINEA_NEGOCIO"), LCOD_MODAL_EST.Text, _
                        LCOD_ALUMNO, LCOD_PERIODO.Text, LSUSTENTO.Text, LCOD_USUARIO, LCODMOTIVO.SelectedValue, LTIPOPAGO.Text, _
                        LID_MATRICULA.Text, LCOD_PRODUCTO.Text, LCOD_DOCUMENTO.Text, presultado, psql)

            ' si ocurrió un error registrar el documento
            If presultado <> "0" Then
                Mensaje_tramite(23, "TR23ENVIO", presultado, psql)
            End If

            ' si se registro ok, se guarda el log en oracle
            If LTIPOPAGO.Text = "2" Then
                InsertaLog(LCODUNICO, LCOD_PERIODO.Text, pcompania, "PE", pnumdocu, LPRECIO.Text, LNUMERO_CUOTAS, _
                 LCOD_USUARIO)
            End If

            'si todo ok, muestra mensaje

            If LTIPOPAGO.Text = "0" Then 'NO TIENE PAGO
                strMensaje1 = "La solicitud fue enviada con éxito."
            ElseIf LTIPOPAGO.Text = "1" Then 'PAGO EN CAJA
                    strMensaje1 = "Tiene " & LNUM_DIAS & " días útiles para cancelar en el banco la suma de " & LDESPRECIO.Text & "."
            ElseIf LTIPOPAGO.Text = "2" Then 'PAGA EN BOLETA
                strMensaje1 = "Se cargó en su próxima boleta la suma de " & LDESPRECIO.Text & "."
            End If

            'Formar los datos de la Solicitud
            strMensaje2 = "<b>Número de solicitud:</b> " & LCODUNICO & "<br>" & _
                  "<b>Ciclo académico:</b> " & LCOD_PERIODO.Text & "<br>" & _
                  "<b>Fecha de la solicitud:</b> " & LFECHA_SOLICITUD

            'mensaje ok
            EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "02", strMensaje1, strMensaje2)
        End If

    End Sub

    Private Sub PAGA_BOLETA(ByVal pservicio As String, ByVal pcompania As String, ByVal psucursal As String, _
            ByVal pcodlinea As String, ByVal pcodalumno As String, ByVal pcodmodal As String, _
            ByVal pcodperiodo As String, ByVal ppreciounit As String, ByVal pformapago As String, _
            ByVal pdias As String, ByVal pvendedor As String, ByVal pultmusuario As String, _
            ByVal ppreciototal As String, ByVal pcoutasfac As String, ByVal pcantidad As String, ByRef pnumdocu As String)

        Dim dsSql As DataSet, dr As DataRow, strErr As String
        dsSql = SQL.CargaBoleta(conexionUPC_cert, pservicio, pcompania, psucursal, pcodlinea, _
                    pcodalumno, pcodmodal, pcodperiodo, ppreciounit, pformapago, _
                    pdias, pvendedor, pultmusuario, ppreciototal, pcoutasfac, pcantidad, strErr)

        '**** si hay error en Spring, muestra mensaje
        If strErr <> "" Then
            Mensaje_tramite(23, "TR2301", "31", strErr)
        End If

        For Each dr In dsSql.Tables(0).Rows
            pnumdocu = IIf(IsDBNull(dr("numdocu")), "", dr("numdocu"))
        Next

    End Sub

    Private Sub ObtieneTitulo()
        Dim var As String
        lblTituloSol.Text = C.Titulo(conexion, Session("WCOD_LINEA_NEGOCIO"), "23")
    End Sub

    Private Sub ObtieneDiasPago(ByVal pmodal As String, ByVal pciclo As String, ByVal ptramite As String, ByRef pdias As String)
        Dim va As Boolean, pcoderr, pdeserr As String
        va = C.ObtieneDiasPago(conexion, Session("WCOD_LINEA_NEGOCIO"), pmodal, pciclo, ptramite, Session("WCOD_LOCAL"), pdias, pcoderr, pdeserr)
        If va = False Then
            Mensaje_tramite(23, "TR2301", pcoderr, pdeserr)
        End If
    End Sub

    Private Sub InsertaLog(ByVal pcodunico As String, ByVal pciclo As String, ByVal pcompania As String, _
                    ByVal ptipodoc As String, ByVal pnumdoc As String, ByVal pprecio As String, _
                    ByVal pnumcuotas As String, ByVal pusuario As String)
        Dim va As Boolean, pcoderr, pdeserr, pnum As String
        va = C.InsertaLog(conexion, pcodunico, pciclo, pcompania, ptipodoc, pnumdoc, pprecio, pnumcuotas, pusuario, pnum, pcoderr, pdeserr)
        If va = False Or pnum <> "1" Then
            Mensaje_tramite(23, "TR2301", pcoderr, pdeserr)
        End If
    End Sub

    Private Sub ObtieneVendedor(ByRef pvendedor As String, ByRef pdesvendedor As String)
        Dim va As Boolean, pcoderr, pdeserr As String, pnum As Integer
        Dim pcompania, psucursal, pid_grupo_def, ptipo_doc_spr, pid_dscto_def, presultado As String

        va = C.VendedorIntranet(conexion, Session("WCOD_LINEA_NEGOCIO"), pvendedor, pdesvendedor, pcoderr, pdeserr)
        If va = False Then
            Mensaje_tramite(23, "TR2301", pcoderr, pdeserr)
        End If

        ' NO SE HA DEFINIDO VENDEDOR EN LA LNEA DE NEGOCIO
        If pvendedor = "" Or pdesvendedor = "" Then
            Mensaje_tramite(23, "TR2301", "27", "")
        End If

        'verifica que el vendedor spring exista
        SQL.VendedorSpring(conexionUPC_cert, pvendedor, pnum, pcoderr, pdeserr)
        If pnum = 0 Then
            Mensaje_tramite(23, "TR2301", "29", "")
        End If

    End Sub

    Private Sub ValidaDatos(ByVal pmodal As String, ByVal pciclo As String, ByVal palumno As String, _
    ByRef pnumcuotas As String, ByRef pcuotasfac As String, ByRef pdeuda As String, _
    ByRef pcompania As String, ByRef psucursal As String, ByRef presultado As String)
        Dim va As Boolean, pcoderr, pdeserr As String
        Dim pid_grupo_def, ptipo_doc_spr, pid_dscto_def As String

        ' verifica parametros oracle
        va = C.ValidaParametros(conexion, Session("WCOD_LINEA_NEGOCIO"), pcompania, psucursal, _
                pid_grupo_def, ptipo_doc_spr, pid_dscto_def, presultado, pcoderr, pdeserr)

        If va = False Then
            Mensaje_tramite(23, "TR2301", pcoderr, pdeserr)
        End If

        ' obtiene datos de pago del alumno
        va = C.ValidaSitAlumno(conexionD, Session("WCOD_LINEA_NEGOCIO"), pmodal, pciclo, _
                palumno, pcompania, pnumcuotas, pcuotasfac, pdeuda, presultado, pcoderr, pdeserr)

        If va = False Then
            Mensaje_tramite(23, "TR2301", pcoderr, pdeserr)
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

        'Llenar datos de los labels de la página ma012cl4, porque es el padre
        Dim thisPage As System.Web.UI.Page = CType(Context.Handler, System.Web.UI.Page)

        CType(thisPage.FindControl("lblMensajeError1"), Label).Text = MENSAJE
        CType(thisPage.FindControl("lblcodmensaje"), Label).Text = CODMENSAJE
        CType(thisPage.FindControl("lblTitulo"), Label).Text = TITULO
        CType(thisPage.FindControl("lblerrorSQL"), Label).Text = ERRORSQL

        Me.lblTitulo.Text = TITULO
        Me.lblCODMENSAJE.Text = CODMENSAJE
        Me.lblMensajeError1.Text = MENSAJE
        Me.lblerrorSQL.Text = ERRORSQL
        Server.Transfer(PAGINA, True)

    End Sub

End Class

End Namespace
