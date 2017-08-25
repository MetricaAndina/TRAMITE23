<%@ Register TagPrefix="uc1" TagName="PiePagina" Src="ctrUsuario/PiePagina.ascx"  %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="tramites.tr2301" CodeFile="tr2301.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="ctrUsuario/Cabecera.ascx" %>
<HTML>
	<HEAD>
		<TITLE>Sócrates - Intranet </TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!--*mod Savalos-->
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
        <script src="../../funcsist4.js" type="text/javascript"></script>
		<script type="text/javascript" language="javascript">
		if (history.forward(1)){location.replace(history.forward(1))}
		</script>
		<script type="text/javascript" language="javascript">
		<!--
		function CerrarVentana()
				{
				var win = window.self;
				win.opener=window.self;
				win.close();
				}
        /*MOD PROYECTO NAVEGADORES - DENYS RONDAN - 05/08/2010 : Se retiraron "validarDato" y "chkValueTextArea" en su lugar se coloco Funcsist4.js */
		//-->
        </script>
	</HEAD>
	<BODY bgcolor="#ffefc6" style="margin-left:0; margin-top:0;">
		<FORM id="frmSolicitud" name="frmSolicitud" method="post" runat="server">
		<table cellSpacing="0" cellPadding="0" width="780" border="0">
			<tr vAlign="top">
				<td><uc1:cabecera id="Cabecera1" runat="server"></uc1:cabecera></td>
			</tr>
			<tr>
			    <td>&nbsp;</td>
			</tr>
		</table>
		<TABLE cellSpacing="0" cellPadding="0" width="700" border="0">
							<tr>
					<td width="35"></td>
								<TD align="left" width="700" valign="bottom"><FONT face="Arial" color="<%=Session("WCOLOR_LINEA")%>" size="5"><b><asp:label id="lblTituloSol" runat="server"></asp:label></b></FONT></TD>
							</TR>
						</TABLE>
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td width="35"></td>
					<td>
						
						<HR align="left" width="700" color="<%=Session("WCOLOR_LINEA")%>">
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0">
							<TR>
								<TD style="WIDTH: 88px; HEIGHT: 16px" align="left" width="88"><FONT face="Arial" color="black" size="2"><b></b></FONT></TD>
								<TD style="HEIGHT: 16px" align="left" width="200"><font face="Arial" size="2">&nbsp;</font></TD>
								<TD style="WIDTH: 253px; HEIGHT: 16px" align="right" width="253"><FONT face="Arial" color="black" size="2"><b>Fecha 
											de la solicitud:</b></FONT></TD>
								<TD style="HEIGHT: 16px" align="left" width="65"><font face="Arial" size="2"><asp:label id="lblFechaSol" runat="server"></asp:label></font></TD>
							</TR>
						</TABLE>
						<!--MOD PROYECTO NAVEGADORES - DRONDAN - 21/04/2010 : En la Tabla se paso el valor del BORDER al CELLSPACING - FIN MOD PROYECTO NAVEGADORES -->
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0">
							<TR>
								<TD><br>
									<font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Datos personales</b></font></TD>
							</TR>
						</TABLE>
						<table cellSpacing="0" cellPadding="0" width="650" bgColor="<%=Session("WCOLOR_LINEA")%>" border="0">
							<tr>
								<td>
									<TABLE align=center width=650 border=0 cellspacing=1 cellpadding=0 bgcolor="<%=Session("WCOLOR_LINEA")%>">
										<TR height="20">
											<TD style="height:27;" align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Alumno:</b></FONT></TD>
											<TD style="height:27;" width="350" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1">&nbsp;<asp:label id="lblAlumnoNombre" runat="server"></asp:label></FONT></TD>
											<TD align="center" width="100" bgColor="#ffffff" rowSpan="3"><asp:image id="imgFoto" runat="server" width="70" height="80"></asp:image></TD>
										</TR>
										<TR height="20">
											<TD style="height:27;" align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Teléfonos:</b></FONT></TD>
											<TD style="height:27;" align="left" width="350" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1">&nbsp;<asp:label id="lblAlumnoTelef" runat="server"></asp:label></FONT></TD>
										</TR>
										<TR height="20">
											<TD style="height:27;" align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Correo 
														del alumno:</b></FONT></TD>
											<TD style="height:27;" align="left" width="350" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><A href="mailto:LPERALES@UPC.EDU.PE"></A>&nbsp;<asp:hyperlink id="lnkAlumnoMail" runat="server"></asp:hyperlink></FONT></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
						<div id="Div1" runat="server"><br>
							<asp:label id="lblErrorUsu" runat="server" ForeColor="DarkBlue" Width="644px" Font-Names="Arial"
								Font-Bold="True"></asp:label><br>
						</div>
						<br>
						<TABLE borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="650"
							borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
							<TR>
								<TD><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Datos de la solicitud</b></font>
									<asp:label id="lblMatriculaId" runat="server" Visible="False" Font-Size="XX-Small"></asp:label><asp:requiredfieldvalidator id="rfvSustento" runat="server" Font-Names="Arial" Font-Bold="True" Font-Size="XX-Small"
										ControlToValidate="txtSustentoSol" Display="Dynamic" ErrorMessage=" * Debe ingresar el sustento de la solicitud"></asp:requiredfieldvalidator></TD>
							</TR>
						</TABLE>
						<table borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="650"
							bgColor="<%=Session("WCOLOR_LINEA")%>" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
							<tr>
								<td>
									<TABLE  bgColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="1" borderColorDark="#ffffff" cellPadding="0" width="650"
										align="center" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
										<TR height="20">
											<TD style="HEIGHT: 19px" align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Tipo 
														de documento:</b></FONT></TD>
											<TD style="HEIGHT: 19px" align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1">&nbsp;
													<asp:dropdownlist id="ddlDocumento" runat="server" Font-Names="Arial" Font-Size="XX-Small" AutoPostBack="True"></asp:dropdownlist><asp:requiredfieldvalidator id="rfvTipoDocumento" runat="server" Font-Names="Arial" Font-Bold="True" Font-Size="XX-Small"
														ControlToValidate="ddlDocumento" Display="Dynamic" ErrorMessage="* Debe seleccionar un tipo de documento." InitialValue="0"></asp:requiredfieldvalidator><asp:label id="lblCodModalidad" runat="server" Visible="False" Font-Size="XX-Small"></asp:label><asp:label id="lblCodDocu" runat="server" Font-Names="Arial" Visible="False" Font-Size="XX-Small"></asp:label></FONT></TD>
										</TR>
										<TR height="20">
											<TD style="WIDTH: 200px; HEIGHT: 13px" align="left" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Modalidad 
														de estudio:</b></FONT></TD>
											<TD style="WIDTH: 290px; HEIGHT: 13px" align="left" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1">&nbsp;<asp:label id="lblModalidad" runat="server"></asp:label>
													<asp:dropdownlist id="ddlModalidad" runat="server"  Font-Names="Arial" Font-Size="XX-Small"
														AutoPostBack="True"></asp:dropdownlist></FONT></TD>
											<TD style=" HEIGHT: 13px" align="left" width="80" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Ciclo:</b></FONT></TD>
											<TD style=" HEIGHT: 13px" width="80" align="left" bgColor="#ffffff"><font face="arial" color="#000000" size="1">&nbsp;
													<asp:label id="lblCiclo" runat="server"></asp:label></font></TD>
										</TR>
										<TR height="20">
											<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Carrera:</b></FONT></TD>
											<TD align="left" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1">&nbsp;
													<asp:label id="lblCodProducto" runat="server"></asp:label>&nbsp;<asp:label id="lblProducto" runat="server"></asp:label></FONT></TD>
										</TR>
										<TR height="65">
											<TD align="left" width="200" bgColor="#ffe7a6" Height="70"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Sustento 
														de la solicitud:</b><br>
													<font size="1">&nbsp;</font> </FONT>
											</TD>
											<TD align="center" bgColor="#ffffff" Width="450" colSpan="3"><asp:TextBox onchange="return chkValueTextAreaMB(this,500,event)" onkeypress="no_comillaMB(this, event);"
                                                    ID="txtSustentoSol" runat="server" Width="440" Font-Names="Arial" Font-Size="10pt"
                                                    Height="60" TextMode="MultiLine" Style="vertical-align: middle; height: 70; background-color: Transparent;"></asp:textbox>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
						<br>
						<div id="dv_Pago" runat="server">
							<TABLE borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="650"
								borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
								<TR>
									<TD><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>La solicitud necesita de pago&nbsp;para 
												tramitarse.
												<asp:label id="lblCaja" runat="server" Font-Names="Arial" Visible="False" Font-Size="XX-Small"></asp:label><asp:label id="lblboleta" runat="server" Font-Names="Arial" Visible="False" Font-Size="XX-Small"></asp:label><asp:label id="lblCodSql" runat="server" Font-Names="Arial" Visible="False" Font-Size="XX-Small"></asp:label><asp:label id="lblCosto" runat="server" Font-Names="Arial" Visible="False" Font-Size="XX-Small"></asp:label><asp:label id="lblTPago" runat="server" Font-Names="Arial" Visible="False" Font-Size="XX-Small"></asp:label></b></font></TD>
								</TR>
							</TABLE>
							<table borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="650"
								bgColor="<%=Session("WCOLOR_LINEA")%>" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
								<tr>
									<td>
										<TABLE bgColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="1" borderColorDark="#ffffff" cellPadding="0" width="650"
											align="center" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
											<TR height="20">
												<TD style="HEIGHT: 19px" align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Costo 
															del trámite:</b></FONT></TD>
												<TD style="HEIGHT: 19px" align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1">&nbsp;
														<asp:label id="lblPrecio" runat="server" Font-Names="Arial" Font-Size="XX-Small"></asp:label></FONT></TD>
											</TR>
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Tipo 
															de pago:</b></FONT></TD>
												<TD align="left" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1"><asp:radiobuttonlist id="rglTipoPago" runat="server" Font-Names="Arial" Font-Size="XX-Small" Height="6px"
															RepeatDirection="Horizontal">
															<asp:ListItem Value="1" Selected="True">Pagar en banco</asp:ListItem>
															<asp:ListItem Value="2">Cargar a la pr&#243;xima boleta</asp:ListItem>
														</asp:radiobuttonlist><asp:label id="lblTipoPago" runat="server"></asp:label></FONT></TD>
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
									<TABLE align="center" border="0">
										<TR>
											<TD valign="top"><asp:button style="CURSOR: pointer; HEIGHT: 26px;" id="btnEnviar" runat="server" ForeColor="#ffe7a6" Width="95px" Font-Bold="True"
													Text="Enviar" CausesValidation="True"></asp:button></TD>
											<td width="10">
											<td valign="top"><input style="FONT-WEIGHT: bold; WIDTH: 92px; CURSOR: pointer; COLOR: #ffe7a6; HEIGHT: 26px; BACKGROUND-COLOR: <%=Session("WCOLOR_LINEA")%>"
													onclick="javascript:CerrarVentana()" type="button" value="Cerrar" name="btnCerrar" id="btnCerrar"></td>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						<uc1:piepagina id="PiePagina1" runat="server"></uc1:piepagina><br>
						<asp:label id="lblTitulo" runat="server" Font-Names="Arial" Visible="False" Font-Size="XX-Small"></asp:label><asp:label id="lblCODMENSAJE" runat="server" Font-Names="Arial" Visible="False" Font-Size="XX-Small"></asp:label><asp:label id="lblMensajeError1" runat="server" Font-Names="Arial" Visible="False" Font-Size="XX-Small"></asp:label><asp:label id="lblerrorSQL" runat="server" Font-Names="Arial" Font-Size="XX-Small"></asp:label></td>
				</tr>
			</table>
		</FORM>
	</BODY>
</HTML>
