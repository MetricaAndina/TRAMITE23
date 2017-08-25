Imports ADTRAMITES.ADTRAMITES


Namespace tramites


    Partial Class tr2305
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
        Protected WithEvents TABLE1 As System.Web.UI.HtmlControls.HtmlTable



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

            ObtieneSede()

            Dim objTram As New clsTramites, va As Boolean, perr As String
            ' VALIDA AUTORIZACION EN EL APLICATIVO
            ValidaAutorizacion("TR2305", Session("CUSUARIO"), "1", "")

            If Not Page.IsPostBack Then

                'attibutes para validar fechas
                dbDesde.Attributes.Add("onBlur", "DateFormat(this,this.value,event,true,'3');Valida_Fecha(this,this.value);")
                dbDesde.Attributes.Add("onFocus", "javascript:vDateType='3'")
                dbDesde.Attributes.Add("onKeyUp", "DateFormat(this,this.value,event,false,'3')")

                dbHasta.Attributes.Add("onBlur", "DateFormat(this,this.value,event,true,'3');Valida_Fecha(this,this.value);")
                dbHasta.Attributes.Add("onFocus", "javascript:vDateType='3'")
                dbHasta.Attributes.Add("onKeyUp", "DateFormat(this,this.value,event,false,'3')")

                WFECHAHOY.Text = C.FechaActual(conexion)
                LFECHAHOY.Text = "Fecha: " & C.FechaActual(conexion)
                'LFECHAHOY.Visible = False
                dv_Criterios.Visible = True
                dv_Resultado.Visible = False

                DarColorControles()
                objTram.CadenaConexion = conexion

                ' carga modalidad
                va = C.ListaModalidad(conexion, Session("WCOD_LINEA_NEGOCIO"), Me.ddlModalidad, perr)
                If va = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al mostrar la lista de modalidades de estudio", perr)
                End If

                ' carga IDICAOR
                va = C.ListaIndicador(conexion, Me.ddlIndicador, perr)
                If va = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al mostrar la lista de indicadores", perr)
                End If

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

        Private Sub DarColorControles()
            'PONER COLOR A LOS BOTONES
            Me.btnEnviar.BackColor = Session("WCOLOR_LINEA_NEGOCIO")
            Me.btn_Reconsultar.BackColor = Session("WCOLOR_LINEA_NEGOCIO")
            Me.dgTramite.BorderColor = Session("WCOLOR_LINEA_NEGOCIO")
            Me.dgTramite.HeaderStyle.ForeColor = Session("WCOLOR_LINEA_NEGOCIO")
            Me.dgTramite.FooterStyle.ForeColor = Session("WCOLOR_LINEA_NEGOCIO")
        End Sub

        Private Sub limpia()
            lit.Text = ""
            lblError.Text = ""
            Me.paTabla.Visible = False
            Me.paLimite.Visible = False
        End Sub
        Private Sub ddlIndicador_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlIndicador.SelectedIndexChanged
            Dim objTram As New clsTramites, va As Boolean, perr As String, ds As New DataSet
            ' carga tramites
            va = C.ListaTramites(conexion, Session("WCOD_LINEA_NEGOCIO"), _
                Me.ddlModalidad.SelectedValue, Me.ddlIndicador.SelectedValue, ds, perr)
            If va = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al mostrar la lista de trámites", perr)
            Else
                dgTramite.DataSource = ds
                dgTramite.DataBind()
                If ds.Tables(0).Rows.Count = 0 Then
                    dgTramite.Visible = False
                Else
                    dgTramite.Visible = True
                End If
                limpia()
            End If
        End Sub

        Private Sub ddlModalidad_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlModalidad.SelectedIndexChanged
            Dim objTram As New clsTramites, va As Boolean, perr As String, ds As New DataSet
            ' carga tramites
            va = C.ListaTramites(conexion, Session("WCOD_LINEA_NEGOCIO"), _
                Me.ddlModalidad.SelectedValue, Me.ddlIndicador.SelectedValue, ds, perr)
            If va = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al mostrar la lista de trámites", perr)
            Else
                dgTramite.DataSource = ds
                dgTramite.DataBind()
                If ds.Tables(0).Rows.Count = 0 Then
                    dgTramite.Visible = False
                Else
                    dgTramite.Visible = True
                End If
            End If
            limpia()
        End Sub

        Private Function ValidaFechas() As Boolean
            Dim fdesde, fhasta As String
            fdesde = Mid(dbDesde.Text, 7) & Mid(dbDesde.Text, 4, 2) & Mid(dbDesde.Text, 1, 2)
            fhasta = Mid(dbHasta.Text, 7) & Mid(dbHasta.Text, 4, 2) & Mid(dbHasta.Text, 1, 2)
            If fdesde > fhasta Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub btnEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
            Dim wtipo As String = C.TipoIndicador(conexion, ddlIndicador.SelectedValue)
            Dim wId As String, i As Integer, wok As Boolean = False, wcont As Integer = 0

            If Not ValidaFechas() Then
                lblError.Text = "La fecha 'desde' debe ser menor o igual a la fecha 'hasta'"
                Exit Sub
            End If

            lblError.Text = ""

            ' tipo atendidas Fuera de fecha  o fuera de fecha de programacion o dias promedio de atencion y es linea
            If wtipo = 1 Or wtipo = 4 Then
                wId = C.ID_SEGUMIENTO(conexion)
                For i = 0 To dgTramite.Items.Count - 1
                    Dim Procesa As CheckBox = CType(dgTramite.Items(i).FindControl("chkSel"), CheckBox)
                    If Procesa.Checked = True Then
                        wok = True
                        ProcesaTmp(wtipo, ddlModalidad.SelectedValue, dgTramite.Items(i).Cells(0).Text, _
                        ddlIndicador.SelectedValue, wId, dbDesde.Text, dbHasta.Text, ddlFrecuencia.SelectedValue)
                    End If
                Next
                If wok = False Then
                    limpia()
                    lblError.Text = "Por lo menos debe seleccionar un trámite."
                    Exit Sub
                Else
                    If wtipo = 1 Or wtipo = 4 Then
                        LlenaTabla1(wId)
                    End If
                    If wtipo = 3 Then
                        LlenaTabla3(wId)
                    End If
                End If
            End If

            ' tipo Nro solicitudes por estado y dias prrmedio de atención y es barra
            If wtipo = 2 Or wtipo = 3 Then

                'valida que solo se haya seleccionado 1
                For i = 0 To dgTramite.Items.Count - 1
                    Dim Procesa As CheckBox = CType(dgTramite.Items(i).FindControl("chkSel"), CheckBox)
                    If Procesa.Checked = True Then
                        wcont = wcont + 1
                    End If
                Next

                If wcont = 0 Then
                    limpia()
                    lblError.Text = "Debe seleccionar un trámite."
                    Exit Sub
                ElseIf wcont > 1 Then
                    limpia()
                    lblError.Text = "Debe seleccionar sólo un trámite."
                    Exit Sub
                Else
                    wId = C.ID_SEGUMIENTO(conexion) ' obtiene el sgte correlativo
                End If

                For i = 0 To dgTramite.Items.Count - 1
                    Dim Procesa As CheckBox = CType(dgTramite.Items(i).FindControl("chkSel"), CheckBox)
                    If Procesa.Checked = True Then
                        ProcesaTmp(wtipo, ddlModalidad.SelectedValue, dgTramite.Items(i).Cells(0).Text, _
                        ddlIndicador.SelectedValue, wId, dbDesde.Text, dbHasta.Text, ddlFrecuencia.SelectedValue)
                    End If
                Next

                If wtipo = 2 Then
                    LlenaTabla2(wId)
                End If

                If wtipo = 3 Then
                    LlenaTabla3(wId)
                End If

            End If

            Me.paTabla.Visible = True
            Me.paLimite.Visible = True
            dv_Criterios.Visible = False
            dv_Resultado.Visible = True

            'LFECHAHOY.Visible = True
        End Sub

        Private Function fColorRgb(ByVal n As Integer) As String
            Dim nmod As Integer = n Mod 10

            If nmod = 1 Then
                Return "#00CC00"
            ElseIf nmod = 2 Then
                Return "#FF0066"
            ElseIf nmod = 3 Then
                Return "#6633CC"
            ElseIf nmod = 4 Then
                Return "#FF9933"
            ElseIf nmod = 5 Then
                Return "#CCCCCC"
            ElseIf nmod = 6 Then
                Return "#993366"
            ElseIf nmod = 7 Then
                Return "#FFCCCC"
            ElseIf nmod = 8 Then
                Return "#FFCC00"
            ElseIf nmod = 9 Then
                Return "#3399FF"
            ElseIf nmod = 0 Then
                Return "#999933"
            End If

        End Function

        Private Sub PintaSemaforo(ByVal dsT As DataSet, ByVal dsP As DataSet)
            Dim drow, drowval, drowP As DataRow, vbolV, vbolT As Boolean, perror As String, dsV As DataSet
            Dim wceleste, wrojo, wamarillo, wverde As String

            Me.paLimite.Controls.Add(New LiteralControl("<TABLE borderColor='" & Session("WCOLOR_LINEA") & "' cellSpacing=0 borderColorDark='#ffffff' cellPadding=4  align=left border=1>"))
            Me.paLimite.Controls.Add(New LiteralControl("<TR><TD align=left width='100' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>Descripción del trámite</B></FONT></TD>"))
            Me.paLimite.Controls.Add(New LiteralControl("<TD align=left width='50' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>Color</B></FONT></TD>"))

            ' si es por mes, obtiene ciclos
            If ddlFrecuencia.SelectedValue = 1 Then
                vbolT = C.ObtieneCiclos(conexion, Session("WCOD_LINEA_NEGOCIO"), ddlModalidad.SelectedValue, _
                                    dbDesde.Text, dbHasta.Text, dsP, perror)
                If vbolT = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los trámites procesados.", perror)
                End If
            End If

            For Each drow In dsP.Tables(0).Rows
                Me.paLimite.Controls.Add(New LiteralControl("<TD align=left width='30' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>" & drow("DES_FRECUENCIA") & "</B></FONT></TD>"))
            Next

            Me.paLimite.Controls.Add(New LiteralControl("</TR>"))

            For Each drow In dsT.Tables(0).Rows
                Me.paLimite.Controls.Add(New LiteralControl("<TR height=17><TD align=left bgColor='white' rowspan=3 ><FONT face='Arial' size=1>" & drow("des_tramite") & "</FONT></TD>"))
                'wceleste = "<TD align=left bgColor='LightBlue'><FONT face='Arial' size=1>&nbsp;</FONT></TD>"
                'wamarillo = "<TR height=20><TD align=left bgColor='yellow'><FONT face='Arial' size=1>&nbsp;</FONT></TD>"
                wamarillo = "<TR height=20><TD align=left bgColor='yellow'><FONT face='Arial' size=1>&nbsp;</FONT></TD>"
                wrojo = "<TR height=20><TD align=left bgColor='red'><FONT face='Arial' size=1>&nbsp;</FONT></TD>"
                wverde = "<TD align=left bgColor='#99cc66'><FONT face='Arial' size=1>&nbsp;</FONT></TD>"

                'por cada periodo
                For Each drowP In dsP.Tables(0).Rows
                    'drowP("DES_FRECUENCIA") 
                    vbolV = C.ObtieneLimites(conexion, Session("WCOD_LINEA_NEGOCIO"), ddlModalidad.SelectedValue, _
                                            drowP("DES_FRECUENCIA"), drow("Cod_tramite"), ddlIndicador.SelectedValue, dbDesde.Text, _
                                            dbHasta.Text, dsV, perror)
                    If vbolV = False Then
                        EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los limites del indicador.", perror)
                    End If

                    For Each drowval In dsV.Tables(0).Rows
                        'wceleste = wceleste & "<TD align=left bgColor='white'><FONT face='Arial' size=1> No hubo trámites </FONT></TD>"
                        wrojo = wrojo & "<TD align=left bgColor='white'><FONT face='Arial' size=1> >= a " & drowval("LIMITE_2") & "% </FONT></TD>"
                        wamarillo = wamarillo & "<TD align=left bgColor='white'><FONT face='Arial' size=1> >= a " & drowval("LIMITE_1") & "% y < a " & drowval("LIMITE_2") & "% </FONT></TD>"
                        wverde = wverde & "<TD align=left bgColor='white'><FONT face='Arial' size=1> < a " & drowval("LIMITE_1") & "% </FONT></TD>"
                        'Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>" & drowval("VALOR") & "</FONT></TD>"))
                    Next

                    If dsV.Tables(0).Rows.Count = 0 Then
                        'wceleste = wceleste & "<TD align=left bgColor='white'><FONT face='Arial' size=1> No hubo trámites </FONT></TD>"
                        wrojo = wrojo & "<TD align=left bgColor='white'><FONT face='Arial' size=1> No configurado </FONT></TD>"
                        wamarillo = wamarillo & "<TD align=left bgColor='white'><FONT face='Arial' size=1> No configurado </FONT></TD>"
                        wverde = wverde & "<TD align=left bgColor='white'><FONT face='Arial' size=1> No configurado </FONT></TD>"
                    End If
                Next

                'wceleste = wceleste & "</TR>"
                wrojo = wrojo & "</TR>"
                wamarillo = wamarillo & "</TR>"
                wverde = wverde & "</TR>"
                Me.paLimite.Controls.Add(New LiteralControl(wceleste & wverde & wamarillo & wrojo))
            Next
            Me.paLimite.Controls.Add(New LiteralControl("</TABLE>"))

        End Sub

        Private Sub LlenaTabla1(ByVal pid As String)
            Dim dsT As New DataSet, vbolT, vbolV, vbolP As Boolean, perror As String
            Dim dsP As New DataSet, drow, drowval As DataRow, val, color, tooltip As String
            Dim dsV As New DataSet, wini As Boolean = True

            vbolT = C.SegTramites(conexion, pid, dsT, perror)
            vbolP = C.SegPeriodos(conexion, pid, dsP, perror)

            If vbolT = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los trámites procesados.", perror)
            End If

            If vbolP = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los periodos procesados.", perror)
            End If

            If dsP.Tables(0).Rows.Count = 0 Then
                lblError.Text = "No existe información para el rango de fechas ingresado"
                Return
            End If

            'pone la cabecera de la tabla
            Me.paTabla.Controls.Add(New LiteralControl("<FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size='5'><b><asp:label id='lblTituloSol' runat='server'>Indicador: " & ddlIndicador.SelectedItem.Text & "</asp:label></b></FONT><br>"))
            Me.paTabla.Controls.Add(New LiteralControl("<font face='Arial' color='black' size='2'><STRONG>del " & dbDesde.Text & " al " & dbHasta.Text & " </STRONG></font><br>"))
            Me.paTabla.Controls.Add(New LiteralControl("<HR align='left' width='700' color='" & Session("WCOLOR_LINEA") & "'> <BR>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TABLE borderColor='" & Session("WCOLOR_LINEA") & "' cellSpacing=0 borderColorDark='#ffffff' cellPadding=4  align=left border=1>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TR><TD align=left width='100' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>Descripción del trámite</B></FONT></TD>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TD align=left width='50' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>Usuario</B></FONT></TD>"))

            Dim ancho As Integer = 300, wEjeX As String, contS As Integer = 0
            Dim XSCALE_MAX As Double = 0

            For Each drow In dsP.Tables(0).Rows
                Me.paTabla.Controls.Add(New LiteralControl("<TD align=left width='30' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>" & drow("DES_FRECUENCIA") & "</B></FONT></TD>"))
                ancho = ancho + 30
                XSCALE_MAX = XSCALE_MAX + 1
                wEjeX = wEjeX & drow("DES_FRECUENCIA") & "|"
            Next
            wEjeX = Mid(wEjeX, 1, Len(wEjeX) - 1)
            XSCALE_MAX = XSCALE_MAX + 0.5

            Me.paTabla.Controls.Add(New LiteralControl("</TR>"))
            Dim SERIE, SERIE_TYPE, SERIE_STYLE, SERIE_FONT, SERIE_DATA, SERIE_COLOR, SERIE_POINT, SERIE_DATA_X As String
            Dim wmax_fin, wmax, wmax_aux As Double, ColorRGB As String
            Dim ContX As Integer

            wmax = 0
            For Each drow In dsT.Tables(0).Rows
                wmax = 0
                'arma cadenas de grafico
                contS = contS + 1
                SERIE = SERIE & " <PARAM NAME='SERIE_" & contS.ToString & "' VALUE='" & drow("des_tramite") & "'> "

                ColorRGB = fColorRgb(contS)

                If ddlGrafico.SelectedValue = 1 Then
                    SERIE_STYLE = SERIE_STYLE & " <PARAM NAME='SERIE_STYLE_" & contS.ToString & "' VALUE = '0.2|" & ColorRGB & "|LINE'>"
                    SERIE_COLOR = SERIE_COLOR & " <PARAM NAME = 'SERIE_COLOR_" & contS.ToString & "' VALUE = '" & ColorRGB & "'>"
                    SERIE_TYPE = SERIE_TYPE & " <PARAM NAME = 'SERIE_TYPE_" & contS.ToString & "' VALUE = 'LINE'> "
                    SERIE_POINT = SERIE_POINT & " <PARAM NAME = 'SERIE_POINT_" & contS.ToString & "' VALUE = 'true'> "
                    SERIE_DATA_X = SERIE_DATA_X & " <PARAM NAME='SERIE_DATAX_" & contS.ToString & "' VALUE='"
                Else
                    SERIE_STYLE = SERIE_STYLE & " <PARAM NAME='SERIE_STYLE_" & contS.ToString & "' VALUE='" & ColorRGB & "'> "
                    SERIE_TYPE = SERIE_TYPE & " <PARAM NAME='SERIE_TYPE_" & contS.ToString & "' VALUE='BAR'> "
                End If

                SERIE_FONT = SERIE_FONT & " <PARAM NAME='SERIE_FONT_" & contS.ToString & "' VALUE='Arial|PLAIN|8'> "
                SERIE_DATA = SERIE_DATA & " <PARAM NAME='SERIE_DATA_" & contS.ToString & "' VALUE='"

                Me.paTabla.Controls.Add(New LiteralControl("<TR height=17><TD align=left bgColor='white' rowspan=4 ><FONT face='Arial' size=1>" & drow("des_tramite") & "</FONT></TD>"))

                ' counter
                vbolV = C.SegValores(conexion, pid, "Counter", drow("Cod_tramite"), dsV, perror)
                If vbolV = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los valores de la tabla.", perror)
                End If
                Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>Counter</FONT></TD>"))
                For Each drowval In dsV.Tables(0).Rows
                    Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>" & drowval("VALOR") & "</FONT></TD>"))
                Next
                Me.paTabla.Controls.Add(New LiteralControl("</TR>"))

                dsV.Clear()

                ' alumno
                vbolV = C.SegValores(conexion, pid, "Alumno", drow("Cod_tramite"), dsV, perror)
                If vbolV = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los valores de la tabla.", perror)
                End If
                Me.paTabla.Controls.Add(New LiteralControl("<TR height=20><TD align=left bgColor='white'><FONT face='Arial' size=1>Alumno</FONT></TD>"))
                For Each drowval In dsV.Tables(0).Rows
                    Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>" & drowval("VALOR") & "</FONT></TD>"))
                Next
                Me.paTabla.Controls.Add(New LiteralControl("</TR>"))

                dsV.Clear()

                ' total
                vbolV = C.SegValores(conexion, pid, "Total", drow("Cod_tramite"), dsV, perror)
                If vbolV = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los valores de la tabla.", perror)
                End If
                Me.paTabla.Controls.Add(New LiteralControl("<TR height=20><TD align=left bgColor='white'><FONT face='Arial' size=1>Total</FONT></TD>"))
                For Each drowval In dsV.Tables(0).Rows
                    ' si es barras , muestra totales
                    If ddlGrafico.SelectedValue = 2 Then
                        wmax_aux = Convert.ToDecimal(drowval("VALOR"))
                        If wmax_aux > wmax Then
                            wmax = wmax_aux
                        End If
                        SERIE_DATA = SERIE_DATA & drowval("VALOR") & "|"
                    End If
                    Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>" & drowval("VALOR") & "</FONT></TD>"))
                Next
                Me.paTabla.Controls.Add(New LiteralControl("</TR>"))

                dsV.Clear()

                ' porcentaje
                vbolV = C.SegValores(conexion, pid, "Porc", drow("Cod_tramite"), dsV, perror)
                If vbolV = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los valores de la tabla.", perror)
                End If
                Me.paTabla.Controls.Add(New LiteralControl("<TR height=20><TD align=left bgColor='white'><FONT face='Arial' size=1>Porc</FONT></TD>"))
                ContX = 0
                For Each drowval In dsV.Tables(0).Rows
                    ' si es linea
                    If ddlGrafico.SelectedValue = 1 Then
                        SERIE_DATA = SERIE_DATA & IIf(IsDBNull(drowval("VALOR")), "NULL", drowval("VALOR")) & "|"
                        ContX = ContX + 1
                        SERIE_DATA_X = SERIE_DATA_X & ContX.ToString & "|"
                        wmax_aux = Convert.ToDouble(IIf(IsDBNull(drowval("VALOR")), 0, drowval("VALOR")))
                        If wmax_aux > wmax Then
                            wmax = wmax_aux
                        End If
                    End If
                    val = IIf(IsDBNull(drowval("VALOR")), "&nbsp;", drowval("VALOR") & "%")
                    color = IIf(IsDBNull(drowval("COLOR")), "V", drowval("COLOR"))
                    'pone tooltip cuado no hubo trámites
                    If color = "C" Then
                        tooltip = "title='No hubo trámites'"
                    Else
                        tooltip = ""
                    End If

                    Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='" & FCOLOR(color) & "' " & tooltip & "><FONT face='Arial' size=1>" & val & "</FONT></TD>"))
                Next
                Me.paTabla.Controls.Add(New LiteralControl("</TR>"))

                SERIE_DATA = Mid(SERIE_DATA, 1, Len(SERIE_DATA) - 1) & "'>"

                ' si es barras, el maxmo debe ser en base a la suma
                If ddlGrafico.SelectedValue = 2 Then
                    wmax_fin = wmax_fin + wmax
                Else
                    SERIE_DATA_X = Mid(SERIE_DATA_X, 1, Len(SERIE_DATA_X) - 1) & "'>"
                    wmax_fin = wmax
                End If
            Next

            wmax_fin = wmax_fin * 1.2

            Me.paTabla.Controls.Add(New LiteralControl("</TABLE>"))
            Me.paTabla.Visible = True

            PintaSemaforo(dsT, dsP)

            Dim tamano As String = "WIDTH = " & ancho.ToString & " HEIGHT = 500"
            Dim tit As String = ddlIndicador.SelectedItem.Text & "\n del " & dbDesde.Text & " al " & dbHasta.Text

            Dim YLabel, xlabel, BARCHART_CUMULATIVE, XAXIS_TICKATBASE As String

            'mes
            If ddlFrecuencia.SelectedValue = 1 Then
                xlabel = "Meses"
            Else
                xlabel = "Ciclos"
            End If

            'lineas
            If ddlGrafico.SelectedValue = 1 Then
                YLabel = "Porcentaje"
                BARCHART_CUMULATIVE = ""
                XAXIS_TICKATBASE = "<PARAM NAME = 'XAXIS_TICKATBASE' VALUE = 'true'>"
            Else
                YLabel = "Solicitudes"
                BARCHART_CUMULATIVE = "<PARAM NAME='BARCHART_CUMULATIVE' VALUE='true'> <PARAM NAME='BARCHART_BARSPACE' VALUE='1'>"
                XAXIS_TICKATBASE = ""
            End If

            CargaApplet(tamano, tit, YLabel, xlabel, wEjeX, SERIE, SERIE_TYPE, SERIE_STYLE, SERIE_FONT, _
                        SERIE_DATA, wmax_fin.ToString, XSCALE_MAX.ToString, BARCHART_CUMULATIVE, SERIE_POINT, _
                        XAXIS_TICKATBASE, SERIE_DATA_X)
        End Sub

        Private Sub LlenaTabla2(ByVal pid As String)
            Dim dsT As New DataSet, vbolT, vbolV, vbolP As Boolean, perror As String
            Dim dsP As New DataSet, drow, drowval As DataRow, val, color, tooltip As String
            Dim dsV As New DataSet, wini As Boolean = True

            vbolT = C.SegTramites(conexion, pid, dsT, perror)
            vbolP = C.SegPeriodos(conexion, pid, dsP, perror)

            If vbolT = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los trámites procesados.", perror)
            End If

            If vbolP = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los periodos procesados.", perror)
            End If

            'pone la cabecera de la tabla
            Me.paTabla.Controls.Add(New LiteralControl("<FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size='5'><b><asp:label id='lblTituloSol' runat='server'>Indicador: " & ddlIndicador.SelectedItem.Text & "</asp:label></b></FONT><br>"))
            Me.paTabla.Controls.Add(New LiteralControl("<font face='Arial' color='black' size='2'><STRONG>del " & dbDesde.Text & " al " & dbHasta.Text & " </STRONG></font><br>"))
            Me.paTabla.Controls.Add(New LiteralControl("<HR align='left' width='700' color='" & Session("WCOLOR_LINEA") & "'> <BR>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TABLE borderColor='" & Session("WCOLOR_LINEA") & "' cellSpacing=0 borderColorDark='#ffffff' cellPadding=4  align=left border=1>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TR><TD align=left width='100' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>Descripción del trámite</B></FONT></TD>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TD align=left width='50' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>Solicitudes</B></FONT></TD>"))

            Dim ancho As Integer = 300, wEjeX, wCodTramite, wDesTramite As String
            Dim XSCALE_MAX As Double = 0

            For Each drow In dsT.Tables(0).Rows
                wCodTramite = drow("cod_tramite")
                wDesTramite = drow("des_tramite")
            Next

            For Each drow In dsP.Tables(0).Rows
                Me.paTabla.Controls.Add(New LiteralControl("<TD align=left width='30' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>" & drow("DES_FRECUENCIA") & "</B></FONT></TD>"))
                ancho = ancho + 30
                XSCALE_MAX = XSCALE_MAX + 1
                wEjeX = wEjeX & drow("DES_FRECUENCIA") & "|"
            Next
            wEjeX = Mid(wEjeX, 1, Len(wEjeX) - 1)
            XSCALE_MAX = XSCALE_MAX + 0.5

            Me.paTabla.Controls.Add(New LiteralControl("</TR>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TR height=17><TD align=left bgColor='white' rowspan=13 ><FONT face='Arial' size=1>" & wDesTramite & "</FONT></TD>"))
            Dim SERIE, SERIE_TYPE, SERIE_STYLE, SERIE_FONT, SERIE_COLOR, SERIE_DATA, SERIE_POINT, SERIE_DATA_X As String
            Dim wmax_fin, wmax, wmax_aux As Double, i, CONTX As Integer, wDesEstado, wCodEstado, colorrgb As String

            wmax = 0

            For i = 1 To 7
                wmax_aux = 0
                ' si es barras, lmpia el max
                If ddlGrafico.SelectedValue = 2 Then
                    wmax = 0
                End If
                'i 1:PROCEDE, 2:PROCEDE PARCIALMENTE, 3:PENDIENTE, 4:NO PROCEDE, 5:ANULADO, 6:INCOMPLETO, 7:TOTAL
                If i = 1 Then
                    wDesEstado = "Procede"
                    wCodEstado = "PR"
                ElseIf i = 2 Then
                    wDesEstado = "Procede Parcialmente"
                    wCodEstado = "PP"
                ElseIf i = 3 Then
                    wDesEstado = "Pendiente"
                    wCodEstado = "PE"
                ElseIf i = 4 Then
                    wDesEstado = "No Procede"
                    wCodEstado = "NP"
                ElseIf i = 5 Then
                    wDesEstado = "Anulado"
                    wCodEstado = "AN"
                ElseIf i = 6 Then
                    wDesEstado = "Incompleto"
                    wCodEstado = "IN"
                ElseIf i = 7 Then
                    wDesEstado = "Total"
                    wCodEstado = "TOT"
                End If

                colorrgb = fColorRgb(i)

                'arma cadenas de grafico
                If i <> 7 Then
                    SERIE = SERIE & " <PARAM NAME='SERIE_" & i.ToString & "' VALUE='" & wCodEstado & "'> "
                    If ddlGrafico.SelectedValue = 1 Then
                        SERIE_STYLE = SERIE_STYLE & " <PARAM NAME='SERIE_STYLE_" & i.ToString & "' VALUE = '0.2|" & colorrgb & "|LINE'>"
                        SERIE_COLOR = SERIE_COLOR & " <PARAM NAME = 'SERIE_COLOR_" & i.ToString & "' VALUE = '" & colorrgb & "'>"
                        SERIE_TYPE = SERIE_TYPE & " <PARAM NAME = 'SERIE_TYPE_" & i.ToString & "' VALUE = 'LINE'> "
                        SERIE_POINT = SERIE_POINT & " <PARAM NAME = 'SERIE_POINT_" & i.ToString & "' VALUE = 'true'> "
                        SERIE_DATA_X = SERIE_DATA_X & " <PARAM NAME='SERIE_DATAX_" & i.ToString & "' VALUE='"
                    Else
                        SERIE_STYLE = SERIE_STYLE & " <PARAM NAME='SERIE_STYLE_" & i.ToString & "' VALUE='" & colorrgb & "'> "
                        SERIE_TYPE = SERIE_TYPE & " <PARAM NAME='SERIE_TYPE_" & i.ToString & "' VALUE='BAR'> "
                    End If
                    SERIE_FONT = SERIE_FONT & " <PARAM NAME='SERIE_FONT_" & i.ToString & "' VALUE='Arial|PLAIN|8'> "
                    SERIE_DATA = SERIE_DATA & " <PARAM NAME='SERIE_DATA_" & i.ToString & "' VALUE='"
                End If

                dsV.Clear()

                'VALORES
                vbolV = C.SegValores(conexion, pid, wCodEstado, wCodTramite, dsV, perror)
                If vbolV = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los valores de la tabla.", perror)
                End If
                Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>" & wCodEstado & " - " & wDesEstado & "</FONT></TD>"))
                For Each drowval In dsV.Tables(0).Rows
                    ' si es barras , muestra totales
                    If ddlGrafico.SelectedValue = 2 And i <> 7 Then
                        wmax_aux = Convert.ToDecimal(drowval("VALOR"))
                        If wmax_aux > wmax Then
                            wmax = wmax_aux
                        End If
                        SERIE_DATA = SERIE_DATA & drowval("VALOR") & "|"
                    End If
                    Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>" & drowval("VALOR") & "</FONT></TD>"))
                Next
                Me.paTabla.Controls.Add(New LiteralControl("</TR>"))

                dsV.Clear()

                ' PORCENTAJE 
                If i <> 7 Then
                    vbolV = C.SegValores(conexion, pid, "POR" & wCodEstado, wCodTramite, dsV, perror)
                    If vbolV = False Then
                        EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los valores de la tabla.", perror)
                    End If
                    Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>Porcentaje " & wCodEstado & "</FONT></TD>"))
                    CONTX = 0
                    For Each drowval In dsV.Tables(0).Rows
                        ' si es linea
                        If ddlGrafico.SelectedValue = 1 And i <> 7 Then
                            CONTX = CONTX + 1
                            SERIE_DATA_X = SERIE_DATA_X & CONTX.ToString & "|"
                            SERIE_DATA = SERIE_DATA & IIf(IsDBNull(drowval("VALOR")), "NULL", drowval("VALOR")) & "|"
                            'SERIE_DATA = SERIE_DATA & drowval("VALOR") & "|"
                            wmax_aux = Convert.ToDouble(IIf(IsDBNull(drowval("VALOR")), 0, drowval("VALOR")))
                            If wmax_aux > wmax Then
                                wmax = wmax_aux
                            End If
                        End If
                        val = IIf(IsDBNull(drowval("VALOR")), "&nbsp;", drowval("VALOR") & "%")

                        If i = 4 Or i = 5 Or i = 6 Then
                            Dim clr As String
                            If String.IsNullOrEmpty(drowval("COLOR")) Then

                                clr = "V"
                            Else
                                clr = drowval("COLOR")
                            End If
                            color = FCOLOR(clr)
                        Else
                            color = "WHITE"
                        End If

                        If color = "C" Then
                            tooltip = "title='No hubo trámites'"
                        Else
                            tooltip = ""
                        End If

                        Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='" & color & "' " & tooltip & "><FONT face='Arial' size=1>" & val & "</FONT></TD>"))
                    Next
                    Me.paTabla.Controls.Add(New LiteralControl("</TR>"))

                    SERIE_DATA = Mid(SERIE_DATA, 1, Len(SERIE_DATA) - 1) & "'>"

                End If

                ' si es linea, inidca donde iran los valors
                If ddlGrafico.SelectedValue = 1 Then
                    SERIE_DATA_X = Mid(SERIE_DATA_X, 1, Len(SERIE_DATA_X) - 1) & "'>"
                End If

                ' si es barras, el maxmo debe ser en base a la suma
                If ddlGrafico.SelectedValue = 2 And i <> 7 Then

                    wmax_fin = wmax_fin + wmax
                End If
            Next
            ' SI ES LINEAS, PONE EL MAX VALOR
            If ddlGrafico.SelectedValue = 1 Then
                wmax_fin = wmax * 1.2
            End If

            '        wmax_fin = wmax_fin * 1.2

            Me.paTabla.Controls.Add(New LiteralControl("</TABLE>"))
            Me.paTabla.Visible = True

            PintaSemaforo(dsT, dsP)

            Dim tamano As String = "WIDTH = " & ancho.ToString & " HEIGHT = 500"
            Dim tit As String = ddlIndicador.SelectedItem.Text & "\n del " & dbDesde.Text & " al " & dbHasta.Text

            Dim yLabel, xlabel, BARCHART_CUMULATIVE, XAXIS_TICKATBASE As String

            'mes
            If ddlFrecuencia.SelectedValue = 1 Then
                xlabel = "Meses"
            Else
                xlabel = "Ciclos"
            End If

            'lineas
            If ddlGrafico.SelectedValue = 1 Then
                BARCHART_CUMULATIVE = ""
                yLabel = "Porcentaje"
                XAXIS_TICKATBASE = "<PARAM NAME = 'XAXIS_TICKATBASE' VALUE = 'true'>"
            Else
                yLabel = "Solicitudes"
                BARCHART_CUMULATIVE = "<PARAM NAME='BARCHART_CUMULATIVE' VALUE='true'> <PARAM NAME='BARCHART_BARSPACE' VALUE='1'>"
                XAXIS_TICKATBASE = ""
            End If

            CargaApplet(tamano, tit, yLabel, xlabel, wEjeX, SERIE, SERIE_TYPE, SERIE_STYLE, SERIE_FONT, _
                        SERIE_DATA, wmax_fin.ToString, XSCALE_MAX.ToString, BARCHART_CUMULATIVE, SERIE_POINT, XAXIS_TICKATBASE, SERIE_DATA_X)
        End Sub

        Private Sub LlenaTabla3(ByVal pid As String)
            Dim dsT As New DataSet, vbolT, vbolV, vbolP As Boolean, perror As String
            Dim dsP As New DataSet, drow, drowval As DataRow, val, color, tooltip As String
            Dim dsV As New DataSet, wini As Boolean = True

            vbolT = C.SegTramites(conexion, pid, dsT, perror)
            vbolP = C.SegPeriodos(conexion, pid, dsP, perror)

            If vbolT = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los trámites procesados.", perror)
            End If

            If vbolP = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los periodos procesados.", perror)
            End If

            'pone la cabecera de la tabla
            Me.paTabla.Controls.Add(New LiteralControl("<FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size='5'><b><asp:label id='lblTituloSol' runat='server'>Indicador: " & ddlIndicador.SelectedItem.Text & "</asp:label></b></FONT><br>"))
            Me.paTabla.Controls.Add(New LiteralControl("<font face='Arial' color='black' size='2'><STRONG>del " & dbDesde.Text & " al " & dbHasta.Text & " </STRONG></font><br>"))
            Me.paTabla.Controls.Add(New LiteralControl("<HR align='left' width='700' color='" & Session("WCOLOR_LINEA") & "'> <BR>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TABLE borderColor='" & Session("WCOLOR_LINEA") & "' cellSpacing=0 borderColorDark='#ffffff' cellPadding=4  align=left border=1>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TR><TD align=left width='100' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>Descripción del trámite</B></FONT></TD>"))
            Me.paTabla.Controls.Add(New LiteralControl("<TD align=left width='50' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>Descripción</B></FONT></TD>"))

            Dim ancho As Integer = 300, wEjeX, ColorRGB As String, contS As Integer = 0
            Dim XSCALE_MAX As Double = 0, CONTX As Integer

            For Each drow In dsP.Tables(0).Rows
                Me.paTabla.Controls.Add(New LiteralControl("<TD align=left width='30' bgColor='#ffe7a6'><FONT face='Arial' color='" & Session("WCOLOR_LINEA") & "' size=2><B>" & drow("DES_FRECUENCIA") & "</B></FONT></TD>"))
                ancho = ancho + 30
                XSCALE_MAX = XSCALE_MAX + 1
                wEjeX = wEjeX & drow("DES_FRECUENCIA") & "|"
            Next
            wEjeX = Mid(wEjeX, 1, Len(wEjeX) - 1)
            XSCALE_MAX = XSCALE_MAX + 0.5

            Me.paTabla.Controls.Add(New LiteralControl("</TR>"))
            Dim SERIE, SERIE_TYPE, SERIE_STYLE, SERIE_FONT, SERIE_DATA, SERIE_COLOR, SERIE_POINT As String
            Dim SERIE_DATA1, SERIE_DATA2, SERIE_DATA_X As String
            Dim SERIE1, SERIE2 As String
            Dim SERIE_TYPE1, SERIE_TYPE2 As String
            Dim SERIE_STYLE1, SERIE_STYLE2 As String
            Dim SERIE_FONT1, SERIE_FONT2 As String
            Dim wmax_fin, wmaxAntes, wmaxDespues, wmax, wmax_aux As Double

            wmaxAntes = 0
            wmaxDespues = 0
            wmax = 0
            For Each drow In dsT.Tables(0).Rows
                wmaxAntes = 0
                wmaxDespues = 0
                wmax = 0
                'arma cadenas de grafico
                contS = contS + 1
                SERIE = SERIE & " <PARAM NAME='SERIE_" & contS.ToString & "' VALUE='" & drow("des_tramite") & "'> "
                SERIE1 = SERIE1 & " <PARAM NAME='SERIE_1' VALUE='Antes'> "
                SERIE2 = SERIE2 & " <PARAM NAME='SERIE_2' VALUE='Después'> "
                SERIE_TYPE1 = SERIE_TYPE1 & " <PARAM NAME = 'SERIE_TYPE_1' VALUE = 'BAR'> "
                SERIE_TYPE2 = SERIE_TYPE2 & " <PARAM NAME = 'SERIE_TYPE_2' VALUE = 'BAR'> "
                SERIE_STYLE1 = SERIE_STYLE1 & " <PARAM NAME='SERIE_STYLE_1' VALUE = '" & fColorRgb(1) & "'>"
                SERIE_STYLE2 = SERIE_STYLE2 & " <PARAM NAME='SERIE_STYLE_2' VALUE = '" & fColorRgb(2) & "'>"
                SERIE_FONT1 = SERIE_FONT1 & " <PARAM NAME='SERIE_FONT_1' VALUE='Arial|PLAIN|8'> "
                SERIE_FONT2 = SERIE_FONT2 & " <PARAM NAME='SERIE_FONT_2' VALUE='Arial|PLAIN|8'> "
                ColorRGB = fColorRgb(contS)

                If ddlGrafico.SelectedValue = 1 Then
                    SERIE_STYLE = SERIE_STYLE & " <PARAM NAME='SERIE_STYLE_" & contS.ToString & "' VALUE = '0.2|" & ColorRGB & "|LINE'>"
                    SERIE_COLOR = SERIE_COLOR & " <PARAM NAME = 'SERIE_COLOR_" & contS.ToString & "' VALUE = '" & ColorRGB & "'>"
                    SERIE_TYPE = SERIE_TYPE & " <PARAM NAME = 'SERIE_TYPE_" & contS.ToString & "' VALUE = 'LINE'> "
                    SERIE_POINT = SERIE_POINT & " <PARAM NAME = 'SERIE_POINT_" & contS.ToString & "' VALUE = 'true'> "
                    SERIE_FONT = SERIE_FONT & " <PARAM NAME='SERIE_FONT_" & contS.ToString & "' VALUE='Arial|PLAIN|8'> "
                    SERIE_DATA_X = SERIE_DATA_X & " <PARAM NAME='SERIE_DATAX_1' VALUE='"
                    'Else
                    '    SERIE_STYLE = SERIE_STYLE & " <PARAM NAME='SERIE_STYLE_" & contS.ToString & "' VALUE='" & ColorRGB & "'> "
                    '    SERIE_TYPE = SERIE_TYPE & " <PARAM NAME='SERIE_TYPE_" & contS.ToString & "' VALUE='BAR'> "
                End If

                SERIE_DATA = SERIE_DATA & " <PARAM NAME='SERIE_DATA_" & contS.ToString & "' VALUE='"
                SERIE_DATA1 = SERIE_DATA1 & " <PARAM NAME='SERIE_DATA_1' VALUE='"
                SERIE_DATA2 = SERIE_DATA2 & " <PARAM NAME='SERIE_DATA_2' VALUE='"

                Me.paTabla.Controls.Add(New LiteralControl("<TR height=17><TD align=left bgColor='white' rowspan=3 ><FONT face='Arial' size=1>" & drow("des_tramite") & "</FONT></TD>"))

                ' DP: Dias Promedio
                vbolV = C.SegValores(conexion, pid, "DP", drow("Cod_tramite"), dsV, perror)
                If vbolV = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los valores de la tabla.", perror)
                End If
                Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>D�as Promedio</FONT></TD>"))
                CONTX = 0
                For Each drowval In dsV.Tables(0).Rows
                    ' si es linea
                    If ddlGrafico.SelectedValue = 1 Then
                        CONTX = CONTX + 1
                        SERIE_DATA_X = SERIE_DATA_X & CONTX.ToString & "|"
                        SERIE_DATA = SERIE_DATA & IIf(IsDBNull(drowval("VALOR")), "NULL", drowval("VALOR")) & "|"
                        wmax_aux = Convert.ToDouble(IIf(IsDBNull(drowval("VALOR")), 0, drowval("VALOR")))
                        If wmax_aux > wmax Then
                            wmax = wmax_aux
                        End If
                    End If
                    val = IIf(IsDBNull(drowval("VALOR")), "&nbsp;", drowval("VALOR"))
                    color = drowval("COLOR")

                    If color = "C" Then
                        tooltip = "title='No hubo trámites'"
                    Else
                        tooltip = ""
                    End If

                    Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='" & FCOLOR(color) & "' " & tooltip & "><FONT face='Arial' size=1>" & val & "</FONT></TD>"))
                Next
                Me.paTabla.Controls.Add(New LiteralControl("</TR>"))
                'limpiar DataSet
                dsV.Clear()

                ' dias atendidos antes del promedio
                vbolV = C.SegValores(conexion, pid, "ANTES", drow("Cod_tramite"), dsV, perror)
                If vbolV = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los valores de la tabla.", perror)
                End If
                Me.paTabla.Controls.Add(New LiteralControl("<TR height=20><TD align=left bgColor='white'><FONT face='Arial' size=1>Nro. sol. antes</FONT></TD>"))
                For Each drowval In dsV.Tables(0).Rows
                    ' si es barras , muestra valor de dias antes de 
                    If ddlGrafico.SelectedValue = 2 Then
                        wmax_aux = Convert.ToDecimal(drowval("VALOR"))
                        If wmax_aux > wmaxAntes Then
                            wmaxAntes = wmax_aux
                        End If
                        SERIE_DATA1 = SERIE_DATA1 & drowval("VALOR") & "|"
                    End If
                    Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>" & drowval("VALOR") & "</FONT></TD>"))
                Next
                Me.paTabla.Controls.Add(New LiteralControl("</TR>"))

                'limpiar DataSet
                dsV.Clear()

                ' dias atendidos despues del promedio
                vbolV = C.SegValores(conexion, pid, "DESPUES", drow("Cod_tramite"), dsV, perror)
                If vbolV = False Then
                    EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "Ocurrió un error al obtener los valores de la tabla.", perror)
                End If
                Me.paTabla.Controls.Add(New LiteralControl("<TR height=20><TD align=left bgColor='white'><FONT face='Arial' size=1>Nro. sol. después</FONT></TD>"))
                For Each drowval In dsV.Tables(0).Rows
                    ' si es barras , muestra el valor de dias despues de 
                    If ddlGrafico.SelectedValue = 2 Then
                        wmax_aux = Convert.ToDecimal(drowval("VALOR"))
                        If wmax_aux > wmaxDespues Then
                            wmaxDespues = wmax_aux
                        End If
                        SERIE_DATA2 = SERIE_DATA2 & drowval("VALOR") & "|"
                    End If
                    Me.paTabla.Controls.Add(New LiteralControl("<TD align=left bgColor='white'><FONT face='Arial' size=1>" & drowval("VALOR") & "</FONT></TD>"))
                Next
                Me.paTabla.Controls.Add(New LiteralControl("</TR>"))


                SERIE_DATA = Mid(SERIE_DATA, 1, Len(SERIE_DATA) - 1) & "'>"
                SERIE_DATA1 = Mid(SERIE_DATA1, 1, Len(SERIE_DATA1) - 1) & "'>"
                SERIE_DATA2 = Mid(SERIE_DATA2, 1, Len(SERIE_DATA2) - 1) & "'>"

                ' si es barras, el maxmo debe ser en base a la suma
                If ddlGrafico.SelectedValue = 2 Then
                    SERIE_DATA = SERIE_DATA1 & SERIE_DATA2
                    SERIE = SERIE1 & SERIE2
                    SERIE_TYPE = SERIE_TYPE1 & SERIE_TYPE2
                    SERIE_STYLE = SERIE_STYLE1 & SERIE_STYLE2
                    SERIE_FONT = SERIE_FONT1 & SERIE_FONT2
                    wmax_fin = wmaxAntes + wmaxDespues
                Else
                    SERIE_DATA_X = Mid(SERIE_DATA_X, 1, Len(SERIE_DATA_X) - 1) & "'>"
                    wmax_fin = wmax
                End If
            Next

            'wmax_fin = wmax_fin * 1.2

            Me.paTabla.Controls.Add(New LiteralControl("</TABLE>"))
            Me.paTabla.Visible = True

            PintaSemaforo(dsT, dsP)

            Dim tamano As String = "WIDTH = " & ancho.ToString & " HEIGHT = 500"
            Dim tit As String = ddlIndicador.SelectedItem.Text & " \n del " & dbDesde.Text & " al " & dbHasta.Text

            Dim xLabel, ylabel, BARCHART_CUMULATIVE, XAXIS_TICKATBASE As String

            'mes
            If ddlFrecuencia.SelectedValue = 1 Then
                xLabel = "Meses"
            Else
                xLabel = "Ciclos"
            End If

            'lineas
            If ddlGrafico.SelectedValue = 1 Then
                BARCHART_CUMULATIVE = ""
                ylabel = "Días promedio"
                XAXIS_TICKATBASE = "<PARAM NAME = 'XAXIS_TICKATBASE' VALUE = 'true'>"
            Else
                ylabel = "Solicitudes"
                BARCHART_CUMULATIVE = "<PARAM NAME='BARCHART_CUMULATIVE' VALUE='true'> <PARAM NAME='BARCHART_BARSPACE' VALUE='1'>"
                XAXIS_TICKATBASE = ""
            End If

            CargaApplet(tamano, tit, ylabel, xLabel, wEjeX, SERIE, SERIE_TYPE, SERIE_STYLE, SERIE_FONT, _
                        SERIE_DATA, wmax_fin.ToString, XSCALE_MAX.ToString, BARCHART_CUMULATIVE, SERIE_POINT, XAXIS_TICKATBASE, SERIE_DATA_X)
        End Sub

        Private Sub CargaApplet(ByVal tamano As String, ByVal tit As String, ByVal ylabel As String, ByVal xlabel As String, _
                                ByVal wEjeX As String, ByVal SERIE As String, ByVal SERIE_TYPE As String, _
                                ByVal SERIE_STYLE As String, ByVal SERIE_FONT As String, ByVal SERIE_DATA As String, _
                                ByVal wmax_fin As String, ByVal XSCALE_MAX As String, ByVal BARCHART_CUMULATIVE As String, _
                                ByVal SERIE_POINT As String, ByVal XAXIS_TICKATBASE As String, ByVal SERIE_DATA_X As String)
            Dim INTERVALO As Double

            If wmax_fin > 0 And wmax_fin < 10 Then
                INTERVALO = 0.5
            ElseIf wmax_fin >= 10 And wmax_fin < 100 Then
                INTERVALO = 5
            ElseIf wmax_fin >= 100 And wmax_fin < 1000 Then
                INTERVALO = 50
            ElseIf wmax_fin >= 1000 And wmax_fin < 10000 Then
                INTERVALO = 500
            ElseIf wmax_fin >= 10000 And wmax_fin < 100000 Then
                INTERVALO = 5000
            Else
                INTERVALO = 50000
            End If

            XAXIS_TICKATBASE = ""
            lit.Text = " <APPLET CODEBASE='.' CODE='com.java4less.rchart.ChartApplet.class' NAME='TestApplet' ARCHIVE='rchart.jar'" & _
                tamano & " HSPACE='0' VSPACE='0' ALIGN='middle' VIEWASTEXT>" & _
                "		<PARAM NAME='TITLECHART' VALUE='" & tit & "'>" & _
                "		<PARAM NAME='LEGEND' VALUE='TRUE'>" & _
                "		<PARAM NAME='YLABEL' VALUE='" & ylabel & "'>" & _
                "		<PARAM NAME='XLABEL' VALUE='" & xlabel & "'>" & _
                "		<PARAM NAME='XAXIS_LABELS' VALUE='" & wEjeX & "'>" & _
                "		<PARAM NAME='LEGEND_POSITION' VALUE='BOTTOM'>" & _
                "		<PARAM NAME='LEGEND_VERTICAL' VALUE='FALSE'>" & _
                "		<PARAM NAME='RIGHT_MARGIN' VALUE='0.01'>" & _
                "		<PARAM NAME='LEGEND_MARGIN' VALUE='0.1'>" & _
                SERIE & _
                SERIE_TYPE & _
                SERIE_STYLE & _
                SERIE_FONT & _
                SERIE_DATA & _
                SERIE_POINT & _
                XAXIS_TICKATBASE & _
                SERIE_DATA_X & _
                "       <PARAM NAME='TITLE_FONT=Dialog|BOLD|12'> " & _
                "		<PARAM NAME='CHART_BORDER' VALUE='0.2|BLACK|LINE'>" & _
                "		<PARAM NAME='CHART_FILL' VALUE='WHITE'>" & _
                "		<PARAM NAME='BIG_TICK_INTERVALX' VALUE='1'>" & _
                "		<PARAM NAME='BIG_TICK_INTERVALY' VALUE='2'>" & _
                "		<PARAM NAME='TICK_INTERVALY' VALUE='" & INTERVALO & "'>" & _
                "		<PARAM NAME='YSCALE_MIN' VALUE='0'>" & _
                "		<PARAM NAME='YSCALE_MAX' VALUE='" & wmax_fin & "'>" & _
                "		<PARAM NAME='XSCALE_MIN' VALUE='0'>" & _
                "		<PARAM NAME='XSCALE_MAX' VALUE='" & XSCALE_MAX & "'>" & _
                "		<PARAM NAME='LEGEND_BORDER' VALUE='0.2|BLACK|LINE'>" & _
                "		<PARAM NAME='LEGEND_FILL' VALUE='WHITE'>" & _
                BARCHART_CUMULATIVE & _
                "		<PARAM NAME='SERIE_BORDER_TYPE_1' VALUE='RAISED'> " & _
                "		<PARAM NAME='SERIE_BORDER_TYPE_2' VALUE='RAISED'> " & _
                "	</APPLET> "

            'SERIE
            '"		<PARAM NAME='SERIE_1' VALUE='Products'>" & _
            '"		<PARAM NAME='SERIE_2' VALUE='Services'>" & _

            'SERIE_TYPE
            '"		<PARAM NAME='SERIE_TYPE_1' VALUE='BAR'>" & _
            '"		<PARAM NAME='SERIE_TYPE_2' VALUE='BAR'>" & _

            'SERIE_STYLE
            '"		<PARAM NAME='SERIE_STYLE_1' VALUE='RED'>" & _
            '"		<PARAM NAME='SERIE_STYLE_2' VALUE='BLUE'>" & _

            'SERIE_FONT
            '"		<PARAM NAME='SERIE_FONT_1' VALUE='Arial|PLAIN|8'>" & _
            '"		<PARAM NAME='SERIE_FONT_2' VALUE='Arial|PLAIN|8'>" & _

            'SERIE_DATA
            '"		<PARAM NAME='SERIE_DATA_2' VALUE='12|43|50|45|30|32|42'>" & _
            '"		<PARAM NAME='SERIE_DATA_1' VALUE='14|48|98|80|60|42|50'>" & _   

        End Sub
        ' tipo 
        Private Sub ProcesaTmp(ByVal id_indi As String, ByVal pcodmodal As String, ByVal ptramite As String, ByVal pindicador As String, _
            ByVal pid As String, ByVal pfecini As String, ByVal pfecfin As String, ByVal ptipo As String)
            Dim wresult1 As Boolean, presultado, pcoderr, pdeserr As String

            ' id_indi   1: solcitudes atendidas fuera de plazo, 
            '           2: tipo nro de solicitudes por estado
            '           3: dias promedio de atncion
            '           4: solicitudes fuera de plazo de programacion
            wresult1 = C.PROCESA_DATOS(id_indi, conexion, Session("WCOD_LINEA_NEGOCIO"), pcodmodal, ptramite, pindicador, pid, pfecini, pfecfin, ptipo, pcoderr, pdeserr)

            'error 
            If pcoderr <> "0" Or wresult1 = False Then
                EnviarPagina("tr23Resultado.aspx", lblTituloSol.Text, "99", "No se pudo procesar el trámite " & ptramite, pdeserr)
            End If

        End Sub

        Private Function FCOLOR(ByVal color As String) As String
            If color = "V" Then
                color = "#99cc66"
            ElseIf color = "R" Then
                color = "RED"
            ElseIf color = "A" Then
                color = "YELLOW"
            ElseIf color = "C" Then
                color = "LightBlue"
            Else
                color = "white"
            End If
            Return color
        End Function

        Private Sub ddlFrecuencia_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFrecuencia.SelectedIndexChanged
            limpia()
        End Sub

        Private Sub ddlGrafico_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlGrafico.SelectedIndexChanged
            limpia()
        End Sub

        Private Sub btn_Reconsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Reconsultar.Click
            Me.paTabla.Visible = False
            Me.paLimite.Visible = False
            dv_Criterios.Visible = True
            dv_Resultado.Visible = False
        End Sub

        Private Sub ObtieneSede()
            Dim var As String
            Session("WCOD_LOCAL") = C.Sede(conexion, Session("CUSUARIO"))
        End Sub

    End Class

End Namespace
