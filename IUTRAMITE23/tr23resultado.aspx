<%@ Register TagPrefix="uc1" TagName="PiePagina" Src="ctrUsuario/PiePagina.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="ctrUsuario/Cabecera.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="tramites.traresultado" CodeFile="tr23resultado.aspx.vb" %>
<HTML>
	<HEAD>
	<TITLE>Sócrates - Intranet </TITLE>
		<%
if session("WCOLOR_LINEA") = "" then
	Session("WCOLOR_LINEA") = "Maroon"
end if
%>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		
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
		//-->
		</script>
		
	</HEAD>
	<body bgColor="#ffefc6" leftMargin="0" topMargin="0">
		<FORM id="Form1" method="post" runat="server">
			<uc1:cabecera id="Cabecera1" runat="server"></uc1:cabecera>
			<TABLE width="750" border="0" cellpadding="0" cellspacing="0">
				<!--MOD PROYECTO NAVEGADORES - DENYS RONDAN - 30/04/10 : Cambios en el HTML y estilos de texto - FIN MOD -->
				<tr>
				    <td colspan="2">&nbsp;</td>
				</tr>
				<TR>
					<td width="35">&nbsp;</td>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="700" border="0">
								<TR>
									<TD colSpan="4"><font face="arial" color="<%=Session("WCOLOR_LINEA")%>" size="4"><asp:label id="lblTitulo" runat="server" Font-Size="Larger" Font-Bold="True"></asp:label></font><br />
								    <HR align="left" width="92.75%" color="<%=Session("WCOLOR_LINEA")%>"></TD>
								</TR>
								<tr>
									<td width="15%">&nbsp;
										<asp:image id="imgRpta" runat="server"></asp:image>
									<td colSpan="3"><font face="arial" size="2">
											<P>&nbsp;</P>
											<P><asp:label id="lblMsgTitulo" runat="server" Font-Bold="True"></asp:label></P>
											<P>
												<asp:label id="lblMensaje" runat="server" Font-Bold="True"></asp:label></P>
											<P>&nbsp;</P>
											<P>
												<asp:Label id="Label1" runat="server">Label</asp:Label><br>
											</P>
										</font>
									</td>
								</tr>
								<tr>
									<td align="center" colSpan="4">
										<P>&nbsp;</P>
										<P>&nbsp;</P>
										<P><font face="arial" size="2"><input style="FONT-WEIGHT: bold; WIDTH: 70px; COLOR: #ffe7a6; BACKGROUND-COLOR: <%=Session("WCOLOR_LINEA")%>; cursor:pointer;"
													onclick="javascript:window.top.close()" type="button" value="Cerrar" name="btnCerrar">
											</font>
										</P>
									</td>
								</tr>
						</TABLE>
					</td>
				</TR>
				<TR>
					<td width="35"></td>
					<td>
						<table>
							<TBODY>
								<tr>
									<td></td>
								</tr>
							</TBODY>
						</table>
						<uc1:piepagina id="PiePagina1" runat="server"></uc1:piepagina></td>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
