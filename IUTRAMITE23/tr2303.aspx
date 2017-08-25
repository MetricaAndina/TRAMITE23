<%@ Register TagPrefix="uc1" TagName="PiePagina" Src="ctrUsuario/PiePagina.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="tramites.tr2303" CodeFile="tr2303.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="ctrUsuario/Cabecera.ascx" %>

<script runat="server">

	Protected Sub txtObs1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

	End Sub
</script>

<HTML>
	<HEAD>
		<TITLE>Sócrates - Intranet </TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		<!--
		function CerrarVentana()
				{
				/*var win = window.self;
				win.opener=window.self;
				win.close();*/
				window.top.close;
				}
		//-->
		</script>
	</HEAD>
	<BODY bgColor="#ffefc6" leftMargin="0" topMargin="0">
		<u></u>
		<table cellpadding="0" cellspacing="0" border="0" with="780">
			<tr valign="top">
				<td>
					<uc1:Cabecera id="Cabecera1" runat="server"></uc1:Cabecera>
				</td>
			</tr>
		</table>
		<FORM id="frmSolicitud" name="frmSolicitud"  method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td width="35"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="700" align="center" border="0">
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
									<TABLE cellSpacing="1" cellPadding="0" width="650" align="center" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>"
										bordercolordark="#ffffff" bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Alumno:</b></FONT></TD>
											<TD width="350" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblAlumnoNombre" runat="server"></asp:label></FONT></TD>
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
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>" bordercolordark="#ffffff"
							bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
							<TR>
								<TD><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Datos de la solicitud</b></font></TD>
							</TR>
						</TABLE>
						<table cellSpacing="0" cellPadding="0" width="650" bgColor="<%=Session("WCOLOR_LINEA")%>" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>"
							bordercolordark="#ffffff" bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
							<tr>
								<td>
									<TABLE cellSpacing="1" cellPadding="0" width="650" align="center" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>"
										bordercolordark="#ffffff" bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
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
											<TD align="center" bgColor="#ffffff" colSpan="3"><P align="left"><asp:textbox id="txtSustentoSol" runat="server" Height="61px" Width="476px" TextMode="MultiLine"
														ReadOnly="True" Font-Size="10px" Font-Names="Arial" style="background-color:Transparent;"></asp:textbox></P>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
						<BR>
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>" bordercolordark="#ffffff"
							bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
							<tr>
								<td><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Informe de la evaluación</b></font></td>
							</tr>
						</TABLE>
						<table cellSpacing="0" cellPadding="0" width="650" bgColor="<%=Session("WCOLOR_LINEA")%>" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>"
							bordercolordark="#ffffff" bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
							<tr>
								<td>
									<TABLE cellSpacing="1" cellPadding="0" width="650" align="center" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>"
										bordercolordark="#ffffff" bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
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
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Observaciones:</b><br>
													<font size="1">&nbsp;(Max. 500 caracteres)</font> </FONT>
											</TD>
											<TD align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1"><asp:TextBox ID="txtObs" runat="server" Height="61px" Width="476px" TextMode="MultiLine"
													ReadOnly="True" Font-Size="10px" Font-Names="Arial" Style="background-color: Transparent;"></asp:textbox></FONT></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
						<br>
						<div id="dv_Asesor" runat="server">
							<TABLE cellSpacing="0" cellPadding="0" width="650" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>" bordercolordark="#ffffff"
								bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
								<tr>
									<td><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Reactivación de la Solicitud</b></font></td>
								</tr>
							</TABLE>
							<table cellSpacing="0" cellPadding="0" width="650" bgColor="<%=Session("WCOLOR_LINEA")%>" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>"
								bordercolordark="#ffffff" bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
								<tr>
									<td>
										<TABLE cellSpacing="1" cellPadding="0" width="650" align="center" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>"
											bordercolordark="#ffffff" bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Atendido 
															por:</b></FONT></TD>
												<TD align="left" width="180" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblAsesor" runat="server"></asp:label></FONT></TD>
												<TD align="left" width="130" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Fecha 
															de atención:</b></FONT></TD>
												<TD align="left" width="140" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblFechaAses" runat="server"></asp:label>&nbsp;(dd/mm/aaaa)
													</FONT>
												</TD>
											</TR>
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Observaciones:</b><br>
														<font size="1">&nbsp;(Max. 500 caracteres)</font> </FONT>
												</TD>
												<TD align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1">
													<asp:TextBox ID="txtObs1" runat="server" Height="61px" Width="476px" TextMode="MultiLine"
														ReadOnly="True" Font-Size="10px" Font-Names="Arial" OnTextChanged="txtObs1_TextChanged"
														Style="background-color: Transparent;"></asp:textbox></FONT></TD>
											</TR>
										</TABLE>
									</td>
								</tr>
							</table>
						</div>
						<br>
						<div id="dv_Secretaria" runat="server">
							<TABLE cellSpacing="0" cellPadding="0" width="650" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>" bordercolordark="#ffffff"
								bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
								<tr>
									<td><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Entrega del Documento</b></font></td>
								</tr>
							</TABLE>
							<table cellSpacing="0" cellPadding="0" width="650" bgColor="<%=Session("WCOLOR_LINEA")%>" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>"
								bordercolordark="#ffffff" bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
								<tr>
									<td>
										<TABLE cellSpacing="1" cellPadding="0" width="650" align="center" border="0" bordercolor="<%=Session("WCOLOR_LINEA")%>"
											bordercolordark="#ffffff" bordercolorlight="<%=Session("WCOLOR_LINEA")%>">
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Atendido 
															por:</b></FONT></TD>
												<TD align="left" width="180" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblSecretaria" runat="server"></asp:label></FONT></TD>
												<TD align="left" width="130" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Fecha 
															de atención:</b></FONT></TD>
												<TD align="left" width="140" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblFechaSecre" runat="server"></asp:label>&nbsp;(dd/mm/aaaa)
													</FONT>
												</TD>
											</TR>
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Observaciones:</b><br>
														<font size="1">&nbsp;(Max. 500 caracteres)</font> </FONT>
												</TD>
												<TD align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1"><asp:TextBox ID="txtObs2" runat="server" Height="61px" Width="476px" TextMode="MultiLine"
														ReadOnly="True" Font-Size="10px" Font-Names="Arial" Style="background-color: Transparent;"></asp:textbox></FONT></TD>
											</TR>
										</TABLE>
									</td>
								</tr>
							</table>
						</div>
						<br>
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0">
							<TR>
								<TD vAlign="top" align="center" width="47%">
										<INPUT style="FONT-WEIGHT: bold; WIDTH: 92px; CURSOR: pointer; COLOR: #ffe7a6; HEIGHT: 26px; BACKGROUND-COLOR: <%=Session("WCOLOR_LINEA")%>"
										onclick="javascript:window.top.close()" type="button" value="Cerrar" name="btnCerrar">
						<uc1:PiePagina id="PiePagina1" runat="server"></uc1:PiePagina>
						<br>
						<asp:label id="lblTitulo" runat="server" Visible="False"></asp:label>
						<asp:label id="lblCODMENSAJE" runat="server" Visible="False"></asp:label>
						<asp:label id="lblMensajeError1" runat="server" Visible="False"></asp:label>
						<asp:Label id="lblerrorsql" runat="server" Visible="False"></asp:Label>
					</td>
				</tr>
			</table>
		</FORM>
	</BODY>
</HTML>
