<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="ctrUsuario/Cabecera.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="tramites.tr2302" CodeFile="tr2302.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="PiePagina" Src="ctrUsuario/PiePagina.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//ES">
<HTML>
	<HEAD>
		<TITLE>Sócrates - Intranet </TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <script src="../../funcsist4.js" type="text/javascript"></script>
		<script>
		if (history.forward(1)){location.replace(history.forward(1))}
		</script>
		<script language="javascript">
		<!--
		function CerrarVentana()
				{
				/*var win = window.self;
				win.opener=window.self;
				win.close();*/
				window.top.close;
				}
		/*MOD PROYECTO NAVEGADORES - DENYS RONDAN - 05/08/2010 : Se retitiraron "validarDato" y "chkValueTextArea" en su lugar se coloco Funcsist4.js */
		//-->
        </script>
	</HEAD>
	<BODY bgColor="#ffefc6" leftMargin="0" topMargin="0">
		<% '<FORM id="frmSolicitud" name="frmSolicitud" action="tr065op.asp" method="post" runat="server"> %>
		<FORM id="frmSolicitud" name="frmSolicitud" method="post" runat="server">
        <uc1:cabecera id="Cabecera1" runat="server"></uc1:cabecera>
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td width="35"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="700" align="center" border="0">
							<tr><td>&nbsp;</td></tr>
							<TR>
								<TD align="left" width="100%"><FONT face="Arial" color="<%=Session("WCOLOR_LINEA")%>" size="5"><b><asp:label id="lblTituloSol" runat="server"></asp:label></b></FONT></TD>
							</TR>
						</TABLE>
						<HR align="left" width="700" color="<%=Session("WCOLOR_LINEA")%>">
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0">
							<TR>
								<TD style="HEIGHT: 16px" align="left" width="80"><FONT face="Arial" color="black" size="2"><b>Solicitud 
											Nº:</b></FONT></TD>
								<TD style="HEIGHT: 16px" align="left" width="200"><font face="Arial" size="2">&nbsp;
										<asp:label id="lblNumSol" runat="server"></asp:label></font></TD>
								<TD style="WIDTH: 253px; HEIGHT: 16px" align="right" width="253"><FONT face="Arial" color="black" size="2"><b>Fecha 
											de la solicitud:</b></FONT></TD>
								<TD style="HEIGHT: 16px" align="left" width="65"><font face="Arial" size="2"><asp:label id="lblFechaSol" runat="server"></asp:label></font></TD>
							</TR>
							<TR>
								<TD align="left" width="80">&nbsp;</TD>
								<TD align="left" width="200"></TD>
								<TD align="right" colSpan="2"><FONT face="Arial" size="2"><b>Estado de la solicitud:</b>&nbsp;<font color="<%=Session("WCOLOR_LINEA")%>"><b>
												<asp:label id="lblEstadoSol" runat="server"></asp:label></b></font></FONT></TD>
							</TR>
						</TABLE>
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0">
							<TR>
								<TD><br>
									<font face="arial" color="darkblue" size="3"><b>
											<asp:label id="lblMensaje" runat="server"></asp:label></b></font></TD>
							</TR>
						</TABLE>
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0">
							<TR>
								<TD><br>
									<font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Datos personales</b></font></TD>
							</TR>
						</TABLE>
						<table cellSpacing="0" cellPadding="0" width="650" bgColor="<%=Session("WCOLOR_LINEA")%>" border="0">
							<tr>
								<td>
									<TABLE bgColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="1" cellPadding="0" width="650" align="center" border="0">
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Alumno:</b></FONT></TD>
											<TD width="350" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblAlumnoNombre" runat="server"></asp:label><asp:label id="lblCodAlumno" runat="server" Visible="False"></asp:label></FONT></TD>
											<TD align="center" width="100" bgColor="#ffffff" rowSpan="4"><asp:image id="imgFoto" runat="server" width="70" height="80"></asp:image></TD>
										</TR>
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Teléfonos:</b></FONT></TD>
											<TD align="left" width="350" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblAlumnoTelef" runat="server"></asp:label></FONT></TD>
										</TR>
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Correo 
														del alumno:</b></FONT></TD>
											<TD align="left" width="350" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><A href="mailto:LPERALES@UPC.EDU.PE"></A><asp:hyperlink id="lnkAlumnoMail" runat="server">HyperLink</asp:hyperlink></FONT></TD>
										</TR>
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Carrera:</b></FONT></TD>
											<TD align="left" width="350" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblAlumnoCarrera" runat="server"></asp:label></FONT></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
						<br>
						<TABLE borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="650"
							borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
							<TR>
								<TD><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Datos de la solicitud</b></font></TD>
							</TR>
						</TABLE>
						<table borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="650"
							bgColor="<%=Session("WCOLOR_LINEA")%>" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
							<tr>
								<td>
									<TABLE bgColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="1" cellPadding="0" width="650" align="center" border="0">
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Modalidad 
														de estudio:</b></FONT></TD>
											<TD align="left" width="230" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblModalidad" runat="server"></asp:label></FONT></TD>
											<TD align="left" width="150" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Ciclo:</b></FONT></TD>
											<TD align="left" width="70" bgColor="#ffffff"><font face="arial" color="#000000" size="1">&nbsp;
													<asp:label id="lblCiclo" runat="server"></asp:label></font></TD>
										</TR>
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Tipo 
														de documento:</b></FONT></TD>
											<TD align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1"><asp:label id="lblTipoDocumento" runat="server"></asp:label></FONT></TD>
										</TR>
										<TR height="40">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Sustento 
														de la solicitud:</b><br>
													<font size="1">&nbsp;</font> </FONT>
											</TD>
											<TD align="center" bgColor="#ffffff" colSpan="3"><P align="left"><asp:TextBox ID="txtSustentoSol" runat="server" Height="61px" Width="476px" TextMode="MultiLine"
													ReadOnly="True" Font-Size="10px" Font-Names="Arial" Style="background-color: Transparent;"></asp:textbox></P>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
						<BR>
						<script language="javascript">
							function FAbreLinks(codalu)
							{
							window.open("../../programas/ic0900op.asp?WALUMNO=" + codalu ,"f5","height=400,width=600,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,status=no,titlebar=no,left=20,top=20");
							//window.open("http://www.intranetdese.upc.edu.pe/programas/ic0900o.asp","f5","height=400,width=600,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,status=no,titlebar=no,left=20,top=20");
							}
						</script>
						<font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3">Si desea consultar datos académicos del 
								alumno, haga&nbsp;
								<asp:hyperlink id="hlDatos" runat="server">clic aquí</asp:hyperlink>. </font>
						<br>
						<br>
						<TABLE borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="650"
							borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
							<tr>
								<td><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Informe de la evaluación
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="XX-Small"
												ErrorMessage="** Debe ingresar la observación" ControlToValidate="txtObs" Display="Dynamic"></asp:RequiredFieldValidator></b></font></td>
							</tr>
						</TABLE>
						<table borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="650"
							bgColor="<%=Session("WCOLOR_LINEA")%>" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
							<tr>
								<td>
									<TABLE bgColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="1" cellPadding="0" width="650" align="center" border="0">
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Atendido 
														por:</b></FONT></TD>
											<TD align="left" width="180" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblEvaluador" runat="server"></asp:label></FONT></TD>
											<TD align="left" width="130" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Fecha 
														de atención:</b></FONT></TD>
											<TD align="left" width="140" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblFechaEval" runat="server"></asp:label>&nbsp;(dd/mm/aaaa)
												</FONT>
											</TD>
										</TR>
										
										<!-- AGREGADO POR LA SEP-2007-566 -->
                                        <TR id="tr_accion" runat= server  height="20">
                                        <TD align="left" bgColor="#ffe7a6" style="width: 200px"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Acción
                                                a tomar:</b></FONT></TD>
											<TD align="left" width="450" bgColor="#ffe7a6" colSpan="3"><FONT face="arial" color="#000000" size="1"> <asp:DropDownList ID="ddlAccion" runat="server" AutoPostBack="True" Width="192px">
                                                <asp:ListItem Value="0" Selected="True">Seleccione un estado</asp:ListItem>
                                                <asp:ListItem Value="1">CONFIRMAR</asp:ListItem>
                                                <asp:ListItem Value="2">NO PROCEDE</asp:ListItem>
                                            </asp:DropDownList></FONT></TD>
										</TR>										
										<!-- FIN AGREGADO POR LA SEP-2007-566 -->										
										
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Observaciones:</b><br>
													<font size="1">&nbsp;(Max. 500 caracteres)</font> </FONT>
											</TD>
											<TD align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1"><asp:TextBox ID="txtObs" onkeypress="chkValueTextAreaMB(this,500,event);" runat="server"
													Height="61px" Width="476px" TextMode="MultiLine" Font-Size="10px" Font-Names="Arial"
													Style="background-color: Transparent;"></asp:textbox></FONT></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
						<br>
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0">
							<TR>
								<TD vAlign="top" align="center" width="47%">
									<TABLE align="center" border="0">
										<TR>
											<TD><asp:button id="btnEnviar" runat="server" Width="95px" Font-Bold="True" ForeColor="#ffe7a6"
													CausesValidation="True" Text="Enviar"></asp:button></TD>
									        <td width="10">
                                            
                                            <TD><asp:button id="btnGuardar" runat="server" Width="95px" Font-Bold="True" ForeColor="#ffe7a6"
													CausesValidation="True" Text="Guardar"></asp:button></TD>
									        <td width="10">
											
											<TD><INPUT style="FONT-WEIGHT: bold; WIDTH: 92px; CURSOR: pointer; COLOR: #ffe7a6; HEIGHT: 26px; BACKGROUND-COLOR: <%=Session("WCOLOR_LINEA")%>"
													onclick="javascript:window.top.close()" type="button" value="Cerrar" name="btnCerrar"></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						<uc1:piepagina id="PiePagina1" runat="server"></uc1:piepagina><br>
						<asp:label id="lblTitulo" runat="server" Visible="False"></asp:label><asp:label id="lblCODMENSAJE" runat="server" Visible="False"></asp:label><asp:label id="lblMensajeError1" runat="server" Visible="False"></asp:label><asp:label id="lblerrorsql" runat="server" Visible="False"></asp:label></td>
				</tr>
			</table>
		</FORM>
	</BODY>
</HTML>
