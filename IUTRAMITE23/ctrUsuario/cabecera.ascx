<%@ Control Language="vb" AutoEventWireup="false" Inherits="tramites.Cabecera" CodeFile="Cabecera.ascx.vb" %>
<%@ outputcache duration="60" varybyparam="none" %>

<%
    
if Session("PGRAFICO")="" then
	Session("PGRAFICO")="FranjaUpcVirtual.jpg"
end if
%>

<table border="0" cellpadding="0" cellspacing="0" >
	<tr>
		<td>
			<IMG SRC="/Imagen\<%=Session("PGRAFICO")%>">
		</td>
	</tr>
</table>