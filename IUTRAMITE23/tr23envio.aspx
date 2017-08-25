<%@ Page Language="vb" AutoEventWireup="false" Inherits="tramites.tr23envio" CodeFile="tr23envio.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>Sócrates - Intranet </TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script>
			if (history.forward(1)){location.replace(history.forward(1))}
		</script>
	</HEAD>
	<body bgColor="#ffefc6" leftMargin="0" topMargin="0" rightMargin="0"		>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 376px"
				cellSpacing="1" cellPadding="1" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 135px">
						<asp:label id="lblTituloSol" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 64px" align="center">
						<P><FONT face="arial" color="#8000" size="2"><B>Enviando la solicitud, por favor espere un 
									momento.</B></FONT></P>
						<P><IMG src="../../programas/imagen/cargando.gif" border="1"></iMG><BR>
						</P>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 61px"></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblTitulo" runat="server" Font-Size="XX-Small" Visible="False" Font-Names="Arial"></asp:label>
						<asp:label id="lblCODMENSAJE" runat="server" Font-Size="XX-Small" Visible="False" Font-Names="Arial"></asp:label>
						<asp:label id="lblMensajeError1" runat="server" Font-Size="XX-Small" Visible="False" Font-Names="Arial"></asp:label>
						<asp:label id="lblerrorSQL" runat="server" Font-Size="XX-Small" Font-Names="Arial"></asp:label></TD>
				</TR>
			</TABLE>
			<script language="javascript">
			window.status = "Enviando la solicitud..."
			//Form1.submit()
			</script>
		</form>
	</body>
</HTML>
