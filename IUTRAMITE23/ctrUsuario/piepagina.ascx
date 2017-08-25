<%@ outputcache duration="6000" varybyparam="none" %>
<%@ Control Language="vb" AutoEventWireup="false" Inherits="tramites.PiePagina" CodeFile="PiePagina.ascx.vb" %>
<%
if Session("WCOLOR_LINEA")="" then
	Session("WCOLOR_LINEA")="maroon"
end if
%>
<br>
<br>
<HR style="WIDTH: 98.95%;" color="<%=Session("WCOLOR_LINEA")%>" align="left">
<br>
<table width="650" cellpadding="0" cellspacing="0" border="0" style="WIDTH:650px; FONT-FAMILY:m; HEIGHT:124px">
	<tr valign="top">
		<td width="50%" align="left">
			<% if Session("WCOD_LINEA_NEGOCIO") = "I" or Session("WCOD_LINEA_NEGOCIO") = "X" then %>
			<font face="arial" size="1" color="black"><b>Instituto Superior Tecnológico Privado 
					Cibertec<BR>
					<% else %>
					<font face="arial" size="1" color="black"><b>Universidad Peruana de Ciencias Aplicadas<BR>
							<% end if %>
							UPC Virtu@l ©. 2003. Todos los Derechos reservados.</b></font></b></font>
		</td>
		<td width="50%" align="right">
			<a href="http://intranet.upc.edu.pe/Programas/Ut005.asp" target="_blank"><b><font face="arial" size="1" color="black">
					Sugerencias</b></a>&nbsp;&nbsp;
			<% if Session("WCOD_LINEA_NEGOCIO") = "I" then %>
			<a href="http://www.cibertec.edu.pe" target="_blank"><b><font face="arial" size="1" color="black">
					Home Cibertec</b></a>&nbsp;&nbsp;
			<% else %>
			<a href="http://www.upc.edu.pe" target="_blank"><b><font face="arial" size="1" color="black">
					Home UPC</b></a>&nbsp;&nbsp;
			<% end if %>
			<a href="http://intranet.upc.edu.pe/Ayuda/Ayuda.asp" target="_blank"><b><font face="arial" size="1" color="black">
					Ayuda</b></a> </FONT></FONT></FONT></FONT>
		</td>
	</tr>
	<tr height="10">
		<td height="10">
		</td>
	</tr>
</table>
