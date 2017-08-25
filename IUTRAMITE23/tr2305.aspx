<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="ctrUsuario/Cabecera.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MisControles" Assembly="MisControles" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="tramites.tr2305" CodeFile="tr2305.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="PiePagina" Src="ctrUsuario/PiePagina.ascx" %>
<HTML>
	<HEAD>
		<TITLE>Sócrates - Intranet </TITLE>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!-- <script language="JavaScript" src="fechas.js"></script> -->
		<script>
		if (history.forward(1)){location.replace(history.forward(1))}
		</script>
<script type="text/javascript" language = "Javascript">
    // Declarar fecha válida.
    var dtCh = "/";
    var minYear = 1900;
    var maxYear = 2900;

    function esEntero(s) {
        var i;
        for (i = 0; i < s.length; i++) {
            // Comprueba que los caracteres son números
            var c = s.charAt(i);
            if (((c < "0") || (c > "9"))) return false;
        }
        return true;
    }

    function stripCharsInBag(s, bag) {
        var i;
        var returnString = "";
        // Busca a través de la cadena de caracteres uno por uno.
        // Si el carácter no está, deberá adjuntarse al retorno de cadena.
        for (i = 0; i < s.length; i++) {
            var c = s.charAt(i);
            if (bag.indexOf(c) == -1) returnString += c;
        }
        return returnString;
    }

    function diasFebrero(year) {
        // Febrero tiene 29 días en un año divisible por cuatro, salvo en los años bisiestos.
        return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
    }
    function Dias(n) {
        for (var i = 1; i <= n; i++) {
            this[i] = 31
            if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
            if (i == 2) { this[i] = 29 }
        }
        return this
    }

    function Fecha(dtStr) {
        var daysInMonth = Dias(12)
        var pos1 = dtStr.indexOf(dtCh)
        var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
        var strDay = dtStr.substring(0, pos1)
        var strMonth = dtStr.substring(pos1 + 1, pos2)
        var strYear = dtStr.substring(pos2 + 1)
        strYr = strYear
        if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
        if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
        for (var i = 1; i <= 3; i++) {
            if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
        }
        month = parseInt(strMonth)
        day = parseInt(strDay)
        year = parseInt(strYr)
        if (pos1 == -1 || pos2 == -1) {
            alert("El formato de fecha debe ser : dd/mm/yyyy")
            return false
        }
        if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > diasFebrero(year)) || day > daysInMonth[month]) {
            alert("Por favor, introduzca un día válido")
            return false
        }
        if (strMonth.length < 1 || month < 1 || month > 12) {
            alert("Por favor, introduzca un mes válido")
            return false
        }
        if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
            alert("Por favor, introduzca un año de 4 digitos. Entre " + minYear + " y " + maxYear)
            return false
        }
        if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || esEntero(stripCharsInBag(dtStr, dtCh)) == false) {
            alert("Por favor, introduzca una fecha válida")
            return false
        }
        return true
    }

    function ValidaForm(dt) {
        if (Fecha(dt.value) == false) { dt.focus(); dt.value = ""; return false; }
        return true
    }

    function CerrarVentana() {
        var win = window.self;
        win.opener = window.self;
        win.close();
    }
	/*	function Valida_Fecha(objeto,fecha)
		{
		var fecha_actual;
		var fobjeto = new String();
		var factual = new String();
		var finicial = new String();
		fecha_actual = document.getElementById("WFECHAHOY").value;

			fobjeto = fecha.substr(6,4)+ fecha.substr(3,2) +fecha.substr(0,2)   ;
			factual = fecha_actual.substr(6,4)+ fecha_actual.substr(3,2) +fecha_actual.substr(0,2)   ;
			finicial=19000101

	 		if ((fobjeto > factual || fobjeto<finicial) && (fobjeto!=""))
			{
			alert("La fecha de registro debe estar entre 1900 y la actual: "+fecha_actual);
			objeto.value="";
			objeto.focus();
			return false;
			}
			else
			{
				return true;
			}

        }*/
		
		//-->
</script>
	</HEAD>
	<BODY style="margin: 0; background-color:#ffefc6; margin-top:0"> 
		<form id="frmSolicitud" runat="server">
		<uc1:cabecera id="Cabecera1" runat="server"></uc1:cabecera>
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
				    <td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td width="35"></td>
					<td>
						<div id="dv_Criterios" runat="server">
							<TABLE cellSpacing="0" cellPadding="0" width="700" align="center" border="0">
								<TR>
									<TD align="left" width="100%"><FONT face="Arial" color="<%=Session("WCOLOR_LINEA")%>" size="5"><b><asp:label id="lblTituloSol" runat="server">Seguimiento a la calidad de servicio</asp:label></b></FONT><br>
										<font face="Arial" color="black" size="2"><STRONG>Consulte la calidad de servicio por 
												fechas y trámite virtual gráficamente.</STRONG></font>
									</TD>
								</TR>
							</TABLE>
							<HR align="left" width="700" color="<%=Session("WCOLOR_LINEA")%>">
							<br>
							<table cellSpacing="0" cellPadding="0" width="650" border="0">
								<tr>
									<td vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" width="400" align="center">
											<TR height="20">
												<TD align="left" width="150"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Modalidad 
															de estudio:</b></FONT></TD>
												<TD width="250"><FONT face="arial" color="#000000" size="1"><asp:dropdownlist id="ddlModalidad" runat="server" Width="235px" AutoPostBack="True" Font-Names="Arial"
															Font-Size="XX-Small"></asp:dropdownlist>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Debe seleccionar una modalidad" Display="Dynamic" 
                                                     ControlToValidate="ddlModalidad" ValueToCompare="00" Operator="NotEqual"></asp:CompareValidator>
															</FONT></TD>
											</TR>
											<TR height="20">
												<TD style="HEIGHT: 13px" align="left"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Fecha 
															desde:</b></FONT></TD>
												<TD style="HEIGHT: 13px"><FONT face="arial" color="#000000" size="1"><asp:textbox id="dbDesde" runat="server" Font-Size="XX-Small"></asp:textbox>dd/mm/yyyy
														<asp:textbox id="WFECHAHOY" runat="server" Width="0px" Enabled="False" Visible="false"></asp:textbox></FONT></TD>
											</TR>
											<TR height="20">
												<TD style="HEIGHT: 20px" align="left"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Fecha 
															hasta:</b></FONT></TD>
												<TD style="HEIGHT: 20px"><FONT face="arial" color="#000000" size="1"><asp:textbox id="dbHasta" runat="server" Font-Size="XX-Small"></asp:textbox>dd/mm/yyyy
													</FONT>
												</TD>
											</TR>
											<TR height="20">
												<TD style="HEIGHT: 16px" align="left"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Frecuencia:</b></FONT></TD>
												<TD style="HEIGHT: 16px"><FONT face="arial" color="#000000" size="1"><asp:dropdownlist id="ddlFrecuencia" runat="server" Width="139px" AutoPostBack="True" Font-Size="XX-Small">
															<asp:ListItem Value="1">Mensual</asp:ListItem>
															<asp:ListItem Value="2">Ciclo</asp:ListItem>
														</asp:dropdownlist></FONT></TD>
											</TR>
											<tr height="20">
												<TD style="HEIGHT: 14px" align="left"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Gráfico:</b></FONT></TD>
												<TD style="HEIGHT: 14px"><FONT face="arial" color="#000000" size="1"><asp:dropdownlist id="ddlGrafico" runat="server" Width="139px" AutoPostBack="True" Font-Size="XX-Small">
															<asp:ListItem Value="1">Lineas</asp:ListItem>
															<asp:ListItem Value="2">Barras</asp:ListItem>
														</asp:dropdownlist></FONT></TD>
											</tr>
											<tr height="20">
												<TD align="left"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Indicador:</b></FONT></TD>
												<TD><FONT face="arial" color="#000000" size="1"><asp:dropdownlist id="ddlIndicador" runat="server" Width="235px" AutoPostBack="True" Font-Size="XX-Small"></asp:dropdownlist>
                                                    <br />
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Debe seleccionar un Indicador"
                                                     Display="Dynamic" ControlToValidate="ddlIndicador"
                                                     ValueToCompare="0" Operator="NotEqual"
                                                    
                                                    ></asp:CompareValidator>   
                                                        
                                                    </FONT></TD>
											</tr>
										</TABLE>
										<P><asp:label id="lblError" runat="server" Font-Names="Arial" Font-Size="X-Small" Font-Bold="True"
												ForeColor="Red"></asp:label></P>
									</td>
									<td vAlign="top" width="250" rowSpan="6"><asp:datagrid id="dgTramite" runat="server" Width="244px" Font-Names="arial" Font-Size="XX-Small"
											AutoGenerateColumns="False" BackColor="White" HorizontalAlign="Left">
											<ItemStyle Font-Size="XX-Small"></ItemStyle>
											<HeaderStyle Font-Size="9pt" Font-Names="arial" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"
												BackColor="#FFE7A6"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="COD_TRAMITE"></asp:BoundColumn>
												<asp:BoundColumn DataField="DESCRIPCION" HeaderText="Tr&#225;mite">
													<ItemStyle Font-Size="XX-Small"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="10%"></HeaderStyle>
													<ItemTemplate>
														<asp:CheckBox id="chkSel" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
							</table>
							<BR>
							<TABLE cellSpacing="0" cellPadding="0" width="650" border="0">
								<TR>
									<TD align="center" width="47%">
										<TABLE align="center" border="0" cellpadding="0" cellspacing="0">
											<TR>
												<TD><asp:button id="btnEnviar" runat="server" Width="95px" Font-Bold="True" ForeColor="#ffe7a6"
														CausesValidation="True" Text="Buscar"></asp:button></TD>
												<td width="10">
												    &nbsp;<TD><INPUT style="FONT-WEIGHT: bold; WIDTH: 92px; CURSOR: pointer; COLOR: #ffe7a6; BACKGROUND-COLOR: <%=Session("WCOLOR_LINEA")%>"
														onclick="javascript:window.top.close()" type="button" value="Cerrar" name="btnCerrar"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</div>
						<div id="dv_Resultado" runat="server">
							<table border="0">
								<tr>
									<td><br>
										<div id="dvtabla" align="left" runat="server"><asp:panel id="paTabla" runat="server"></asp:panel></div>
									</td>
								</tr>
								<tr>
									<td><br>
										<div id="dvlimite" align="left" runat="server"><asp:panel id="paLimite" runat="server"></asp:panel></div>
									</td>
								</tr>
								<tr>
									<td><br>
										<div id="dvlit" align="left" runat="server"><asp:literal id="lit" Runat="server"></asp:literal></div>
									</td>
								</tr>
							</table>
							<TABLE width="650">
								<tr>
									<td align="right">
										<asp:label id="LFECHAHOY" runat="server" Font-Names="Arial" Font-Size="X-Small" Font-Bold="True">Label</asp:label></td>
								</tr>
								<tr>
									<td align="center"><asp:button id="btn_Reconsultar" runat="server" Width="148px" Font-Bold="True" ForeColor="#ffe7a6"
											CausesValidation="True" Text="Volver a consultar"></asp:button>&nbsp;
									</td>
								</tr>
							</TABLE>
						</div>
						<asp:label id="lblTitulo" runat="server" Visible="False"></asp:label><asp:label id="lblCODMENSAJE" runat="server" Visible="False"></asp:label><asp:label id="lblMensajeError1" runat="server" Visible="False"></asp:label><asp:label id="lblerrorsql" runat="server" Visible="False"></asp:label></td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td width="35"></td>
					<td><uc1:piepagina id="PiePagina1" runat="server"></uc1:piepagina></td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
