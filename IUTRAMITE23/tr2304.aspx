<%@ Register TagPrefix="uc1" TagName="PiePagina" Src="ctrUsuario/PiePagina.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="tramites.tr2304" CodeFile="tr2304.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="ctrUsuario/Cabecera.ascx" %>
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
				var win = window.self;
				win.opener=window.self;
				win.close();
				}
                /*MOD PROYECTO NAVEGADORES - DENYS RONDAN - 05/08/2010 : Se retiraron "validarDato" y "chkValueTextArea" en su lugar se coloco Funcsist4.js */
		//-->
        </script>
        <script type="text/javascript" language="javascript">
            /*javascript para control login - valida el textbox password al presionar enter*/
            function Password_OnKeyPress(e, obj) 
            {
                var password=document.getElementById("<%=Login1.FindControl("Password").ClientID%>");
                var btn=document.getElementById("<%=btnEnviar.ClientID%>");
                var key = obtenerKey(e);
                if (key == 13) 
                {
                   btn.click();
                }
            }
        
            function obtenerKey(e) 
            {
                var code;
	            if (!e) var e = window.event;
	            if (e.keyCode) code = e.keyCode;
	            else if (e.which) code = e.which;
	            return code;
            }
        </script>
	</HEAD>
	<BODY bgColor="#ffefc6" leftMargin="0" topMargin="0">
	<%
	    'Ticket: CSC-00262816-00
	    'Responsable: Luis Almeyda
	    'Fecha: 14/12/2015
	    'Motivo: En IIS 7.0 genera un conflicto al mantener el action en una página ASP, no se puede realizar
	    '        Transfer.server desde la página ASPX.
	    
	    'Esta es la línea de código original
	    '<FORM id="frmSolicitud" name="frmSolicitud" action="tr065op.asp" method="post" runat="server">
	%>
	<FORM id="frmSolicitud" method="post" runat="server">
	<% 'Fin CSC-00262816-00 %>
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
								<TD align="right" colSpan="2"><FONT face="Arial" size="2"><b>
											<asp:Label id="lblEstado" runat="server" Visible="False"></asp:Label>Estado de 
											la solicitud:</b>&nbsp;<font color="<%=Session("WCOLOR_LINEA")%>"><b>
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
											<TD align="center" width="100" bgColor="#ffffff" rowSpan="4"><asp:image id="imgFoto" runat="server" height="80" width="70"></asp:image></TD>
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
											<TD align="center" bgColor="#ffffff" colSpan="3"><P align="left"><asp:TextBox ID="txtSustentoSol" runat="server" Font-Names="Arial" Font-Size="10px"
													ReadOnly="True" TextMode="MultiLine" Width="476px" Height="61px" Style="background-color: Transparent;"></asp:textbox></P>
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
								<asp:HyperLink id="hlinkDato" runat="server">clic aquí</asp:HyperLink>. </font>
						<br>
						<br>
						<DIV id="dv_Reactiva" runat="server">
							<TABLE id="Table2" borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0"
								width="650" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
								<TR>
									<TD><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><B>Reactivación de la Solicitud
												<asp:RequiredFieldValidator id="rfvReactiva" runat="server" Font-Names="Arial" Font-Size="XX-Small" ErrorMessage="* Debe ingresar las observaciones de reactivación de la solicitud"
													Display="Dynamic" ControlToValidate="txtObs1"></asp:RequiredFieldValidator></B></FONT></TD>
								</TR>
							</TABLE>
							<TABLE id="Table3" borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0"
								width="650" bgColor="<%=Session("WCOLOR_LINEA")%>" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
								<TR>
									<TD>
										<TABLE id="Table4" bgColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="1" cellPadding="0" width="650" align="center" border="0">
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><B>&nbsp;Atendido 
															por:</B></FONT></TD>
												<TD align="left" width="180" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblAsesor" runat="server"></asp:label></FONT></TD>
												<TD align="left" width="130" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><B>&nbsp;Fecha 
															de atención:</B></FONT></TD>
												<TD align="left" width="140" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblFechaAses" runat="server"></asp:label>&nbsp;(dd/mm/aaaa)
													</FONT>
												</TD>
											</TR>
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><B>&nbsp;Observaciones:</B><BR>
														<FONT size="1">&nbsp;(Max. 500 caracteres)</FONT> </FONT>
												</TD>
												<TD align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1"><asp:TextBox ID="txtObs1" onkeypress="return chkValueTextAreaMB(this,500,event);"
														runat="server" Font-Names="Arial" Font-Size="10px" TextMode="MultiLine" Width="476px"
														Height="61px" Style="background-color: Transparent;"></asp:textbox></FONT></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
							<BR>
						</DIV>
						<DIV id="dv_evaluacion" runat="server">
							<TABLE borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0" width="650"
								borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
								<tr>
									<td><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><b>Informe de la evaluación</b></font></td>
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
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><b>&nbsp;Observaciones:</b><br>
														<font size="1">&nbsp;(Max. 500 caracteres)</font> </FONT>
												</TD>
												<TD align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1"><asp:TextBox ID="txtObs" onkeypress="return chkValueTextAreaMB(this,500,event);"
														runat="server" Font-Names="Arial" Font-Size="10px" ReadOnly="True" TextMode="MultiLine"
														Width="476px" Height="61px" Style="background-color: Transparent;"></asp:textbox></FONT></TD>
											</TR>
										</TABLE>
									</td>
								</tr>
							</table>
							<br>
						</DIV>
						<DIV id="dv_entrega" runat="server">
							<TABLE id="Table5" borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0"
								width="650" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
								<TR>
									<TD style="height: 19px"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><B>Entrega del Documento
                                        <asp:Label ID="lbl_Val_Entrega" runat="server" Font-Size="XX-Small" ForeColor="Red"
                                            Text="**Debe ingresar las observaciones de la entrega del documento" Visible="False"
                                            Width="314px"></asp:Label></B></FONT></TD>
								</TR>
							</TABLE>
							<TABLE id="Table6" borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0"
								width="650" bgColor="<%=Session("WCOLOR_LINEA")%>" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
								<TR>
									<TD>
										<TABLE id="Table7" bgColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="1" cellPadding="0" width="650" align="center" border="0">
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><B>&nbsp;Atendido 
															por:</B></FONT></TD>
												<TD align="left" width="180" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblSecretaria" runat="server"></asp:label></FONT></TD>
												<TD align="left" width="130" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><B>&nbsp;Fecha 
															de atención:</B></FONT></TD>
												<TD align="left" width="140" bgColor="#ffffff"><FONT face="arial" color="#000000" size="1"><asp:label id="lblFechaSecre" runat="server"></asp:label>&nbsp;(dd/mm/aaaa)
													</FONT>
												</TD>
											</TR>
											<TR height="20">
												<TD align="left" width="200" bgColor="#ffe7a6"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><B>&nbsp;Observaciones:</B><BR>
														<FONT size="1">&nbsp;(Max. 500 caracteres)</FONT> </FONT>
												</TD>
												<TD align="left" width="450" bgColor="#ffffff" colSpan="3"><FONT face="arial" color="#000000" size="1"><asp:TextBox ID="txtObs2" onkeypress="return chkValueTextAreaMB(this,500,event);"
														runat="server" Font-Names="Arial" Font-Size="10px" TextMode="MultiLine" Width="476px"
														Height="61px" ValidationGroup="grEntregaDoc" Style="background-color: Transparent;"></asp:textbox></FONT></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						
						</DIV>
						
						<!-- MODIFICACION SEP-2007-566 -->
						<!-- Confirmación alumno-->
							<div id = "dv_recepcion" runat="server">
							<BR>
							<TABLE id="Table1" borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0"
								width="650" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
								<TR >
									<TD style="height: 19px"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3"><B>Confirmación
                                        de recepción de documento&nbsp;
                                        <asp:Label ID="lblUsuarioAlu" runat="server" Visible="False" Width="29px"></asp:Label>
                                        <asp:Label ID="lbl_val_Confirma" runat="server" Font-Size="XX-Small" ForeColor="Red"
                                            Text="** El alumno debe ingresar su contraseña" Visible="False" Width="204px"></asp:Label></B></FONT></TD>
								</TR>
							</TABLE>
							<TABLE  borderColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="0" borderColorDark="#ffffff" cellPadding="0"
								width="650" bgColor="<%=Session("WCOLOR_LINEA")%>" borderColorLight="<%=Session("WCOLOR_LINEA")%>" border="0">
								<TR>
									<TD>
										<TABLE id="Table9" bgColor="<%=Session("WCOLOR_LINEA")%>" cellSpacing="1" cellPadding="0" width="650" align="center" border="0">
											<TR height="20">
												<TD align="left" bgColor="#ffe7a6" style="width: 80px; height: 10px;"><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="2"><B>
                                                    <asp:Label ID="lblIngresConfirm" runat="server" Height="25px" Width="479px"></asp:Label></B></FONT></TD>
												<TD align="left" width="180" bgColor="#ffffff" style="height: 10px"><FONT face="arial" color="#000000" size="1">&nbsp; &nbsp;
                                                    &nbsp;&nbsp;
                                                    <%--<asp:TextBox ID="txtPass" runat="server" Width="99px" TextMode="Password" ValidationGroup="grConfirma"></asp:TextBox>--%>
                                                    
                                                    <%--aplicacion del control login para el tramite--%>
                                                    <asp:Login ID="Login1" runat="server">
                                                        <LayoutTemplate>
                                                            <table border="0" cellpadding="0">
                                                                <tr>
                                                                    <td align="right" style="display:none;">
                                                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label></td>
                                                                    <td style="display:none;">
                                                                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="display:none;">
                                                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></td>
                                                                    <td>
                                                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" onKeyPress="Password_OnKeyPress(event, this);"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="2" style="display:none;">
                                                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                    </asp:Login>
                                                    <%--fin de aplicacion del control login para el tramite--%>
                                                    &nbsp;</FONT></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
															
                            </div>
                            <div id ="dv_confirmacion" runat="server">
                                <br />
                            <TABLE cellSpacing="0" cellPadding="0" border="0" style="width: 647px; height: 17px"  >
							    <TR>
								<TD><FONT face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="3">
											<b>
                                                <asp:Label ID="lblConsConfirm" runat="server" Width="526px"></asp:Label></b></font></TR>
						    </TABLE>
                                <br />
							</div>
                            
                            
							<!-- FIN MODIFICACION SEP-2007-566-->
						
						
						<TABLE cellSpacing="0" cellPadding="0" width="650" border="0">
							<TR>
								<TD vAlign="top" align="center" width="47%">
                                    <br />
									<TABLE align="center" border="0">
										<TR>
											<TD><asp:button id="btnEnviar" runat="server" Width="95px" Text="Confirmar" CausesValidation="True"
													ForeColor="#ffe7a6" Font-Bold="True"></asp:button></TD>
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
