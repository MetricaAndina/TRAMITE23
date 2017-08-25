<%@ Language=VBScript %>
<!--#include file="../conexion_A.inc"-->
<!--#include file="sesiones.inc"-->
<!--#include file="../funcsist.inc"-->
<!--#include file="sf_autorizacion.inc"-->
<!--#include file="funcsist_tramites.inc"-->
<!--#include file="funcsist_tramites.js"-->
<!--#include file="Anidar_Solicitudes.inc"-->
<!--#include file="adjunto_documentos.inc"-->

  <%Response.Buffer = True 
  'TRAMITE: SOLICITU DE RETIRO EXTRAORDINARIO DE ASIGNATURA
  'PAGINA: EVALUACION DEL COMITE
  
  'MODIFICACIONES:
  'Se modificaron las conexiones   -  04/09/2007  12:47
  'SEP 2007-392
  
   %>
<HTML>
<HEAD>
<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<TITLE>Sócrates - Intranet </TITLE>
<script language="JavaScript1.2" src="../funcsist4.js" >
</script>
</HEAD>
<BODY topmargin=0 bgcolor=#ffefc6 leftmargin=0 rightmargin=0>
<!--#include file="../include/cabecera.asp"-->
<SCRIPT LANGUAGE="JAvaScript">
  window.status="Cargando los datos de la página, espere por favor..."  
</SCRIPT>
<%
If isnull(session("CUSUARIO")) or session("CUSUARIO") = "" then
	ruta="resultado.asp?WCODMENSAJE=010"
	response.redirect(ruta)		
End if 

'############################## HALLAMOS LA SEDE ######################################
strSQL= " SELECT SF_SEDE_USUARIO('" & UCASE(session("CUSUARIO")) & "') FROM DUAL"

SET rsUSede=server.CreateObject("ADODB.recordset")		
		rsUSede.CursorType =0
		rsUSede.LockType = 1			
		rsUSede.CursorLocation = 3		
		rsUSede.Open strSQL,objConexion		    
    
On Error Resume Next
	    	
LUSede=rsUSede.getrows()	
USEDE=""
	
rsUSede.close()
set rsUSede=nothing	
	
FOR i=0 to ubound(LUSede,2)
	Session("WCOD_LOCAL")=LUSede(0,i)				
NEXT
'###########################################################


if Request.form("codunico") <>"" and Request.form("codperiodo")<>"" and Request.form("rutafoto") <> "" then
	LCOD_UNICO   = Request.form("codunico")
	LCOD_PERIODO = Request.form("codperiodo")
	LRUTAFOTO    = Request.Form ("rutafoto")
	
	strSQL="select cod_tramite, cod_motivo, flag2, flag4, usuario_asesor  from solicitud where cod_unico=" &LCOD_UNICO& " and cod_periodo='"&LCOD_PERIODO&"'"
	'On Error Resume Next
	Set rsMot= objConexion.Execute(strSQL)
	'F_CONTROLA_ERROR(1)	
	LCOD_MOTIVO=rsMot("cod_motivo")
	LCOD_TRAMITE=rsMot("cod_tramite")
	LFLAG2 = rsMot("flag2")
	LFLAG4 = rsMot("flag4")
	LUSUARIO_ASESOR = rsMot("usuario_asesor")
	rsMot.close
	set rsMot=Nothing
	
	'obtiene el punto donde esta la solicitud en el workflow
	STRSQL="SELECT  ACTIVITY_STATUS_CODE, ACTIVITY_LABEL,  ACTIVITY_DISPLAY_NAME, ITEM_KEY "&_
		"FROM WF_ITEM_ACTIVITY_STATUSES_V "&_
		"WHERE ITEM_TYPE = LPAD(" &LCOD_TRAMITE& ",3,0) "&_
		"AND ITEM_KEY = '" &LCOD_PERIODO&LCOD_UNICO& "' " &_
		"AND ACTIVITY_STATUS_CODE ='NOTIFIED'"	
										
	'On  error resume next
	set rstAmbiente = objConexion.Execute(strSQL)
	F_CONTROLA_ERROR(1)
	'On error goto 0
	if not rstAmbiente.eof then
		WITEM_ACT = rstAmbiente("ACTIVITY_LABEL")
	end if
	rstAmbiente.Close
	set rstAmbiente = nothing
											
	IF WITEM_ACT = "BLK_COMITE" OR WITEM_ACT = "BLK_COMITE_TIMEOUT"  THEN
		WAMBIENTE = "COMITE"
	ELSEIF WITEM_ACT = "BLK_ASESOR" OR WITEM_ACT = "BLK_ASESOR_TIMEOUT" THEN
		WAMBIENTE = "ASESOR"
	ELSEIF WITEM_ACT = "BLK_CARRERA" OR WITEM_ACT = "BLK_CARRERA_TIMEOUT" OR WITEM_ACT = "BLK_CARRERA_DIA" THEN
	'ELSEIF WITEM_ACT = "BLK_CARRERA" OR WITEM_ACT = "BLK_CARRERA_TIMEOUT" THEN
		WAMBIENTE = "CARRERA"
	ELSE 
		WAMBIENTE = ""
	END IF

	'VALIDA PERMISOS EN LA PAGINA
	sf_autoriza_reserva pagina,"TR065OP" ,trim(ucase(session("CUSUARIO"))),null,1,LPERMISO_1 ' INGRESO = evaluador
	sf_autoriza_reserva pagina,"TR065OP" ,trim(ucase(session("CUSUARIO"))),null,8,LPERMISO_8 ' Envio de solicitud
	'si no tiene permiso para ingresar, lo bota
	
	If LPERMISO_1="SI"  Then 
		If WAMBIENTE <> "COMITE" then readonly = "readonly"
	Else
		ruta="resultado.asp?WCODMENSAJE=003&WAPLICACION="&pagina
		response.redirect(ruta)
		response.end
	End if

end if	

If LCOD_UNICO = "" Then LCOD_UNICO = Request.Form("hddCodUnico")
If LCOD_PERIODO = "" Then LCOD_PERIODO = Request.Form("hddCodPeriodo")
IF LRUTAFOTO    = "" THEN LRUTAFOTO = Request.Form ("hddrutafoto")
	
If LCOD_UNICO = "" or  LCOD_PERIODO = ""  Then
	ruta="resultado.asp?WCODMENSAJE=033&WMENS1=Página sin datos&WMENS3=Por favor, cierre la página."
    response.redirect(ruta)	
end if

strSQL="select to_char(sysdate,'DD/MM/YYYY')as fecha from dual"
'On Error Resume Next
Set rsFecha = objConexion.Execute(strSQL) 
LFECHA_ACTUAL=rsFecha("fecha")
rsFecha.close
Set rsFecha=Nothing
'F_CONTROLA_ERROR(1)	


LCOD_USUARIO=ucase(trim(Session("CUSUARIO")))			
LCOD_TRAMITE = 16
pagina = FTITULO_TRAMITE(LCOD_TRAMITE)

' obtiene datos de la solicitud DE reserva
Set cmdObtenerSol= Server.CreateObject("ADODB.Command")
Set cmdObtenerSol.ActiveConnection = objConexion
    cmdObtenerSol.CommandText = "Tra$pk_RETEXT_Asp.SP_OBTENER_TRA_RETEXT_ASP"
    cmdObtenerSol.CommandType = 4
	   		    
Set pcodunico   = cmdObtenerSol.CreateParameter("PCOD_UNICO",3,1,15)
			       cmdObtenerSol.Parameters.Append pcodunico
			       cmdObtenerSol.Parameters("PCOD_UNICO")    = LCOD_UNICO								
Set pcodperiodo   = cmdObtenerSol.CreateParameter("PCOD_PERIODO",200,1,6)
			       cmdObtenerSol.Parameters.Append pcodperiodo
			       cmdObtenerSol.Parameters("PCOD_PERIODO")    = LCOD_PERIODO
set pcodusuario   = cmdObtenerSol.CreateParameter("PCOD_USUARIO",200,1,10)
					cmdObtenerSol.Parameters.Append pcodusuario
					cmdObtenerSol.Parameters("PCOD_USUARIO")    = ucase(trim(Session("CUSUARIO")))

Set pcodalumno  =cmdObtenerSol.CreateParameter("PCOD_ALUMNO",200,2,9)
    				cmdObtenerSol.Parameters.Append pcodalumno
Set pnombre     =cmdObtenerSol.CreateParameter("PNOMBRE_ALUMNO",200,2,150)
					 cmdObtenerSol.Parameters.Append pnombre
Set pcod_modal_est     =cmdObtenerSol.CreateParameter("PCOD_MODAL_EST",200,2,2)
					 cmdObtenerSol.Parameters.Append pcod_modal_est 
Set pmodalidad     =cmdObtenerSol.CreateParameter("PMODALIDAD",200,2,100)
					 cmdObtenerSol.Parameters.Append pmodalidad
Set pcodproducto     =cmdObtenerSol.CreateParameter("PCOD_PRODUCTO",200,2,8)
					 cmdObtenerSol.Parameters.Append pcodproducto
Set pdesproducto    =cmdObtenerSol.CreateParameter("PDES_PRODUCTO",200,2,50)
					 cmdObtenerSol.Parameters.Append pdesproducto
set ptelefono  =cmdObtenerSol.CreateParameter("PTELEFONOS",200,2,50)
				  cmdObtenerSol.Parameters.Append ptelefono 
set pmailalu        =cmdObtenerSol.CreateParameter("PMAIL_ALUMNO",200,2,30)
				  cmdObtenerSol.Parameters.Append pmailalu
set pcodmotivo  =cmdObtenerSol.CreateParameter("PCOD_MOTIVO",3,2,15)
				  cmdObtenerSol.Parameters.Append pcodmotivo
set pmotivo     =cmdObtenerSol.CreateParameter("PMOTIVO",200,2,500)
				  cmdObtenerSol.Parameters.Append pmotivo
set pobs_motivo  =cmdObtenerSol.CreateParameter("POBS_MOTIVO",200,2,4000)
				  cmdObtenerSol.Parameters.Append pobs_motivo
set pcod_curso  =cmdObtenerSol.CreateParameter("PCOD_CURSO",200,2,6)
				  cmdObtenerSol.Parameters.Append pcod_curso
set pdes_curso  =cmdObtenerSol.CreateParameter("PDES_CURSO",200,2,50)
				  cmdObtenerSol.Parameters.Append pdes_curso
set pdocumento  =cmdObtenerSol.CreateParameter("PDOCUMENTO",200,2,2)
				  cmdObtenerSol.Parameters.Append pdocumento	
set pruta  =cmdObtenerSol.CreateParameter("PRUTA",200,2,4000)
				  cmdObtenerSol.Parameters.Append pruta
set pdocumento_ev =cmdObtenerSol.CreateParameter("PDOCUMENTO_EV",200,2,2)
				  cmdObtenerSol.Parameters.Append pdocumento_ev
set pruta_ev  =cmdObtenerSol.CreateParameter("PRUTA_EV",200,2,4000)
				  cmdObtenerSol.Parameters.Append pruta_ev
set psustento  =cmdObtenerSol.CreateParameter("PSUSTENTO",200,2,4000)
				  cmdObtenerSol.Parameters.Append psustento
set pfecsol  =cmdObtenerSol.CreateParameter("PFECHA_SOLICITUD",200,2,10)
				  cmdObtenerSol.Parameters.Append pfecsol
set pcodase  =cmdObtenerSol.CreateParameter("PCOD_ASESOR",200,2,10)
				  cmdObtenerSol.Parameters.Append pcodase
set pnombrease  =cmdObtenerSol.CreateParameter("PNOMBRE_ASESOR",200,2,150)
				  cmdObtenerSol.Parameters.Append pnombrease
set pobsase  =cmdObtenerSol.CreateParameter("POBS_ASESOR",200,2,4000)
				  cmdObtenerSol.Parameters.Append pobsase
set pfecase  =cmdObtenerSol.CreateParameter("PFECHA_ASESOR",200,2,10)
				  cmdObtenerSol.Parameters.Append pfecase

set pcodcom  =cmdObtenerSol.CreateParameter("PCOD_COMITE",200,2,10)
				  cmdObtenerSol.Parameters.Append pcodcom
set pnombrecom  =cmdObtenerSol.CreateParameter("PNOMBRE_COMITE",200,2,150)
				  cmdObtenerSol.Parameters.Append pnombrecom
set pobscom  =cmdObtenerSol.CreateParameter("POBS_COMITE",200,2,4000)
				  cmdObtenerSol.Parameters.Append pobscom
set pfeccom  =cmdObtenerSol.CreateParameter("PFECHA_COMITE",200,2,10)
				  cmdObtenerSol.Parameters.Append pfeccom
				  
set pcodeva  =cmdObtenerSol.CreateParameter("PCOD_EVALUADOR",200,2,10)
				  cmdObtenerSol.Parameters.Append pcodeva
set pnombreeva  =cmdObtenerSol.CreateParameter("PNOMBRE_EVALUADOR",200,2,150)
				  cmdObtenerSol.Parameters.Append pnombreeva
set pobseva  =cmdObtenerSol.CreateParameter("POBS_EVALUADOR",200,2,4000)
				  cmdObtenerSol.Parameters.Append pobseva
set pfeceva  =cmdObtenerSol.CreateParameter("PFECHA_EVALUACION",200,2,10)
				  cmdObtenerSol.Parameters.Append pfeceva

set pestado  =cmdObtenerSol.CreateParameter("PESTADO",200,2,30)
				  cmdObtenerSol.Parameters.Append pestado
set pdesestado  =cmdObtenerSol.CreateParameter("PDES_ESTADO",200,2,30)
				  cmdObtenerSol.Parameters.Append pdesestado
set pcodtramite =cmdObtenerSol.CreateParameter("PCOD_TRAMITE",3,2,15)
				  cmdObtenerSol.Parameters.Append pcodtramite
set pdestramite  =cmdObtenerSol.CreateParameter("PDES_TRAMITE",200,2,100)
				  cmdObtenerSol.Parameters.Append pdestramite
set pflag1  =cmdObtenerSol.CreateParameter("PFLAG1",3,2,10)
				  cmdObtenerSol.Parameters.Append pflag1				  
set pflag2  =cmdObtenerSol.CreateParameter("PFLAG2",3,2,10)
				  cmdObtenerSol.Parameters.Append pflag2				  
set pflag4  =cmdObtenerSol.CreateParameter("PFLAG4",3,2,10)
				  cmdObtenerSol.Parameters.Append pflag4				  
set pflag5  =cmdObtenerSol.CreateParameter("PFLAG5",3,2,10)
				  cmdObtenerSol.Parameters.Append pflag5				  
set pflagESTADO =cmdObtenerSol.CreateParameter("PFLAGESTADO",3,2,10)
				  cmdObtenerSol.Parameters.Append pflagESTADO
set pmail        =cmdObtenerSol.CreateParameter("PMAIL",200,2,30)
				  cmdObtenerSol.Parameters.Append pmail
set perror  =cmdObtenerSol.CreateParameter("PERROR",3,2,10)
				  cmdObtenerSol.Parameters.Append perror				  
Set presultado   =cmdObtenerSol.CreateParameter("PRESULTADO",200,2,200)
				  cmdObtenerSol.Parameters.Append presultado

'On Error Resume Next
cmdObtenerSol.Execute 
'F_CONTROLA_ERROR(1)		
LERROR=cmdObtenerSol.Parameters("PERROR")
LRESULTADO=cmdObtenerSol.Parameters("PRESULTADO")
	
'OBTENEMOS LOS DATOS PERSONALES
LCOD_ALUMNO				=trim(cmdObtenerSol.Parameters("PCOD_ALUMNO"))
LNOMBRE_ALUMNO			=trim(cmdObtenerSol.Parameters("PNOMBRE_ALUMNO"))
LMODALIDAD				=trim(cmdObtenerSol.Parameters("PMODALIDAD"))
LCOD_MODAL_EST			=trim(cmdObtenerSol.Parameters("PCOD_MODAL_EST"))
LCOD_PRODUCTO			=trim(cmdObtenerSol.Parameters("PCOD_PRODUCTO"))
LDES_PRODUCTO			=trim(cmdObtenerSol.Parameters("PDES_PRODUCTO"))
LTELEFONOS				=trim(cmdObtenerSol.Parameters("PTELEFONOS"))
LCOD_MOTIVO				=trim(cmdObtenerSol.Parameters("PCOD_MOTIVO"))
LMOTIVO					=trim(cmdObtenerSol.Parameters("PMOTIVO"))
LOBS_MOTIVO				=trim(cmdObtenerSol.Parameters("POBS_MOTIVO"))    
LRUTAALU				=trim(cmdObtenerSol.Parameters("PRUTA"))
LRUTA					=trim(cmdObtenerSol.Parameters("PRUTA_EV"))
LSUSTENTO				=trim(cmdObtenerSol.Parameters("PSUSTENTO"))

LCOD_ASESOR          =trim(cmdObtenerSol.Parameters("PCOD_ASESOR")) 
LNOMBRE_ASESOR		=trim(cmdObtenerSol.Parameters("PNOMBRE_ASESOR"))
LOBS_ASESOR			=trim(cmdObtenerSol.Parameters("POBS_ASESOR"))
LFECHA_ASESOR		=trim(cmdObtenerSol.Parameters("PFECHA_ASESOR"))

LCOD_COMITE          =trim(cmdObtenerSol.Parameters("PCOD_COMITE")) 
LNOMBRE_COMITE		=trim(cmdObtenerSol.Parameters("PNOMBRE_COMITE"))
LOBS_COMITE			=trim(cmdObtenerSol.Parameters("POBS_COMITE"))
LFECHA_COMITE		=trim(cmdObtenerSol.Parameters("PFECHA_COMITE"))

LCOD_EVALUADOR          =trim(cmdObtenerSol.Parameters("PCOD_EVALUADOR")) 
LNOMBRE_EVALUADOR		=trim(cmdObtenerSol.Parameters("PNOMBRE_EVALUADOR"))
LOBS_EVALUADOR			=trim(cmdObtenerSol.Parameters("POBS_EVALUADOR"))
LFECHA_EVALUACION		=trim(cmdObtenerSol.Parameters("PFECHA_EVALUACION"))

LFECHA_SOLICITUD		=trim(cmdObtenerSol.Parameters("PFECHA_SOLICITUD"))
LESTADO					=trim(cmdObtenerSol.Parameters("PESTADO"))
LDES_ESTADO				=trim(cmdObtenerSol.Parameters("PDES_ESTADO"))
LCOD_TRAMITE			=trim(cmdObtenerSol.Parameters("PCOD_TRAMITE")) 
LDES_TRAMITE			=trim(cmdObtenerSol.Parameters("PDES_TRAMITE")) 
LFLAG5					=trim(cmdObtenerSol.Parameters("PFLAG5"))  
LMAIL					= trim(cmdObtenerSol.Parameters("PMAIL"))
LMAIL_ALUMNO			= trim(cmdObtenerSol.Parameters("PMAIL_ALUMNO"))  
LCOD_CURSO				= trim(cmdObtenerSol.Parameters("PCOD_CURSO"))
LDES_CURSO				= trim(cmdObtenerSol.Parameters("PDES_CURSO"))    

If isnull(LOBS_ASESOR) THEN LOBS_ASESOR = ""
If isnull(LOBS_COMITE) THEN LOBS_COMITE = ""
If isnull(LOBS_EVALUADOR) THEN LOBS_EVALUADOR = ""

LCODESTADO = Request.Form("cmbSolicitudEstado")
LCONTINUAR = Request.Form ("hddContinuar")
If isnull(LCONTINUAR) or LCONTINUAR = "" then 	LCONTINUAR = "NO"		

LOBS_REQUEST = Request.Form ("txaObs")
IF LOBS_COMITE <> LOBS_REQUEST AND LOBS_REQUEST <> "" THEN LOBS_COMITE = LOBS_REQUEST

LPASAVALIDACION = Request.Form ("hddPasaValidacion")
IF LPASAVALIDACION = "" THEN LPASAVALIDACION = NULL
LCOD_ERROR = Request.Form ("hddCodError")
LDES_ERROR = Request.Form ("hddDescError")
IF LCOD_ERROR = "" THEN 
	LCOD_ERROR = NULL
	LDES_ERROR = NULL
END IF

if LERROR<>0 then 'SI HUBO ERROR
	set cmdObtenerSol=Nothing
	strSQL="select texto,texto1,texto2 from mensaje_tramites where cod_tramite="&LCOD_TRAMITE&" and archivo='TR065OP' and cod_mensaje="&LERROR&" and cod_local = '"& Session("WCOD_LOCAL") &"' and cod_linea_negocio = '" & Session("WCOD_LINEA_NEGOCIO") & "' "
	'On Error Resume Next
	
	Set rsMensaje= objConexion.Execute(strSQL)
	'F_CONTROLA_ERROR(1)	
   	strMensaje1=rsMensaje("texto")
	strMensaje2=rsMensaje("texto1")
	rsMensaje.close
	set rsMensaje=Nothing
	ruta="resultado.asp?WCODMENSAJE=033&WAPLICACION="&pagina&"&WMENS1="&strMensaje1&"&WMENS3="&strMensaje2&"&WMENS4="&LRESULTADO
    response.redirect(ruta)
end if
set cmdObtenerSol=Nothing

If IsNull(LCODESTADO) or LCODESTADO = "" then LCODESTADO = LESTADO

LGRABA=Request.Form("hddGraba")
if LGRABA="GRABA" then ' 0:graba 1:envia 2:actualiza reserva  
	LFLAG=cint(0) 
elseif LGRABA="ENVIA" then
	LFLAG=cint(1) 
end if

' *** graba la solicitud
If LGRABA="GRABA" OR LGRABA="ENVIA" then
		
	wexito = "1"	  
		
	LRUTA        = CSTR(Session("NOM_DOCUMENTO"))
	if isnull(LRUTA) then LRUTA = ""
			
	'adjunto
	if SESSION("RutaFisica") <> "" then				
		LEN_DOC = LEN(Session("NOM_DOCUMENTO"))
		LEN_TOT = LEN(Session("ARCHIVOFISICO"))

		If NOT(Session("ARCHIVOFISICO") = "" or ISNULL(Session("ARCHIVOFISICO")))THEN
			LRUTA_ORIGINAL = MID(Session("ARCHIVOFISICO"),1,LEN_TOT-LEN_DOC) & Session("RutaOriginal")
		ELSE
			LRUTA_ORIGINAL = ""
		END IF
				
		On Error Resume Next
		Session("Upload").DeleteFile LRUTA_ORIGINAL
		On error goto 0	

		LRUTA_DOCUMENTO = SESSION("RutaFisica")
		On error resume Next					
		Session("fleArchivo").SaveAs LRUTA_DOCUMENTO
		if Err.number <>0 then
		   MENSAJE 13,0,0,"Ha ocurrido un error al adjuntar el documento","Le aconsejamos que consulte a Help Desk.","",Err.Description					    
		end if
		On error goto 0
	else
		if Session("ARCHIVOFISICO") <>"" then
		    On Error Resume Next				    
			Session("Upload").DeleteFile Session("ARCHIVOFISICO")
			On error goto 0
		END IF
	End If

	'Graba la solicitud
	Set cmdGrabarSol= Server.CreateObject("ADODB.Command")
	Set cmdGrabarSol.ActiveConnection = objConexion
	    cmdGrabarSol.CommandText = "Tra$pk_RETEXT_Asp.SP_ACTUALIZA_RETEXT_ASP"
	    cmdGrabarSol.CommandType = 4
			   		    
	Set pcodlinea  =	cmdGrabarSol.CreateParameter("PCOD_LINEA",200,1,1)
	    				cmdGrabarSol.Parameters.Append pcodlinea
	    				cmdGrabarSol.Parameters("PCOD_LINEA")    = Session("WCOD_LINEA_NEGOCIO")
	Set pcodalumno  =	cmdGrabarSol.CreateParameter("PCOD_ALUMNO",200,1,9)
	    				cmdGrabarSol.Parameters.Append pcodalumno
	    				cmdGrabarSol.Parameters("PCOD_ALUMNO") = LCOD_ALUMNO
	Set pcodunico   =	cmdGrabarSol.CreateParameter("PCOD_UNICO",3,1,15)
						cmdGrabarSol.Parameters.Append pcodunico
						cmdGrabarSol.Parameters("PCOD_UNICO")    = LCOD_UNICO
	Set pcodperiodo =	cmdGrabarSol.CreateParameter("PCOD_PERIODO",200,1,6)
						cmdGrabarSol.Parameters.Append pcodperiodo
						cmdGrabarSol.Parameters("PCOD_PERIODO")    = LCOD_PERIODO
	Set pflag		=	cmdGrabarSol.CreateParameter("PFLAG",3,1,6)
						cmdGrabarSol.Parameters.Append pflag
						cmdGrabarSol.Parameters("PFLAG")    = LFLAG
	set ptipousu=	cmdGrabarSol.CreateParameter("PTIPO_USU",3,1,1)
						cmdGrabarSol.Parameters.Append ptipousu
						cmdGrabarSol.Parameters("PTIPO_USU")    = 2 ' COMITE
	set pobservacion=	cmdGrabarSol.CreateParameter("POBSERVACION",200,1,4000)
						cmdGrabarSol.Parameters.Append pobservacion
						cmdGrabarSol.Parameters("POBSERVACION")    = LOBS_COMITE
	set pruta		=	cmdGrabarSol.CreateParameter("PRUTA",200,1,4000)
						cmdGrabarSol.Parameters.Append pruta
						cmdGrabarSol.Parameters("PRUTA")    = NULL
	set pusuevalua =	cmdGrabarSol.CreateParameter("PUSUARIO",200,1,10)
						cmdGrabarSol.Parameters.Append pusuevalua
						cmdGrabarSol.Parameters("PUSUARIO")    = UCASE(trim(Session("CUSUARIO")))
	set pestado		=	cmdGrabarSol.CreateParameter("PESTADO",200,1,2)
						cmdGrabarSol.Parameters.Append pestado
						cmdGrabarSol.Parameters("PESTADO")    = NULL
	set ppasavali	=	cmdGrabarSol.CreateParameter("PPASA_VALIDACION",200,1,2)
						cmdGrabarSol.Parameters.Append ppasavali
						cmdGrabarSol.Parameters("PPASA_VALIDACION")    = NULL
	set pcoderror	=	cmdGrabarSol.CreateParameter("PCOD_ERROR",200,1,2)
						cmdGrabarSol.Parameters.Append pcoderror
						cmdGrabarSol.Parameters("PCOD_ERROR")    = NULL
	set pdeserror	=	cmdGrabarSol.CreateParameter("PDES_ERROR",200,1,500)
						cmdGrabarSol.Parameters.Append pdeserror
						cmdGrabarSol.Parameters("PDES_ERROR")    = NULL
	set pcodsql		=	cmdGrabarSol.CreateParameter("PCODSQL",200,2,200)
						cmdGrabarSol.Parameters.Append pcodsql				  
	Set presultado  =	cmdGrabarSol.CreateParameter("PRESULTADO",200,2,2)
						cmdGrabarSol.Parameters.Append presultado

	'On Error Resume Next
	cmdGrabarSol.Execute 
	'F_CONTROLA_ERROR(1)		
	LERROR1=cmdGrabarSol.Parameters("PCODSQL")
	LRESULTADO1=cmdGrabarSol.Parameters("PRESULTADO")
	if LRESULTADO1<>0 then 'SI HUBO ERROR
		wexito = "0"
	else
		wexito = "1"
	end if

	set cmdGrabarSol=Nothing
	strSQL="select texto,texto1,texto2 from mensaje_tramites where cod_tramite=" &LCOD_TRAMITE& " and archivo='TR065OP' and cod_mensaje="&LRESULTADO1&" and cod_local = '"& Session("WCOD_LOCAL") &"' and cod_linea_negocio = '" & Session("WCOD_LINEA_NEGOCIO") & "' "
	'On Error Resume Next
	Set rsMensaje= objConexion.Execute(strSQL)
	'F_CONTROLA_ERROR(1)	

	if wexito = "1" then
		if LFLAG ="1" then ' SE ENVIA
		  strMensaje1=rsMensaje("texto")		  
		else  'SE GUARDA	
		  strMensaje1=rsMensaje("texto1")
		end IF
	else
		strMensaje1=rsMensaje("texto")
		strMensaje2=rsMensaje("texto1")
	end if
		
	rsMensaje.close
	set rsMensaje=Nothing
	MENSAJE LCOD_TRAMITE,wexito, "0",strMensaje1,strMensaje2,"",LERROR1
		
	set cmdGrabarSol=Nothing

End If 'graba


'VERIFICAMOS SI ESTA PENDIENTE DE PAGO EN CAJA
strSQL = " 	SELECT COUNT(1)  "&_
		" FROM WF_ITEM_ACTIVITY_STATUSES_V "&_
		" WHERE "&_
		" ITEM_TYPE = '016' "&_
		" AND ITEM_KEY LIKE '"&LCOD_PERIODO&LCOD_UNICO &"' "&_
		" AND ACTIVITY_LABEL = 'BLK_PAGO'  "&_
		" AND ACTIVITY_STATUS_CODE = 'NOTIFIED' "
				
'	On Error Resume Next
Set rsCaja= objConexion.Execute(strSQL)
'	F_CONTROLA_ERROR(1)			
if Cstr(rsCaja(0)) ="1" then
	rsCaja.close()
	set rsCaja = nothing
		   
	 strSQL="select texto,texto1,texto2 from mensaje_tramites where cod_tramite=" &LCOD_TRAMITE& "  and archivo='TR065OP' and cod_mensaje=-1 and cod_local = '"& Session("WCOD_LOCAL") &"' and cod_linea_negocio = '" & Session("WCOD_LINEA_NEGOCIO") & "' "
	On Error Resume Next
	Set rsMensaje= objConexion.Execute(strSQL)
	F_CONTROLA_ERROR(1)	
	strMensaje1=rsMensaje("texto")
	strMensaje2=rsMensaje("texto1")
	rsMensaje.close
	set rsMensaje=Nothing
		
	MENSAJE LCOD_TRAMITE,0,0,strMensaje1,strMensaje2,"",""					    
			
end if 
rsCaja.close()
set rsCaja = nothing

%>

 <SCRIPT LANGUAGE="JAvaScript">
  <!--
   
  
function FEnviar(flag) 
  { if (flag==0)	//Guardar
		{
			if (confirm("¿Está seguro de guardar la solicitud?",1)) 
			{	frmSolicitud.hddGraba.value="GRABA";
				frmSolicitud.action = "TR065OP.asp";
				frmSolicitud.submit();	
			}
		}
	
	if (flag==1)	//Enviar
		{		
		if ( chkValueTextArea(frmSolicitud.txaObs,500)==false){
		return
		}
		if (trim(frmSolicitud.txaObs.value)=="")
		{	alert("Debe registrar sus observaciones para poder enviar la evaluación.");
			return;
		}
			
		frmSolicitud.hddGraba.value="ENVIA";
		
		if (confirm("¿Está seguro de enviar la solicitud?",1)) 
		{	frmSolicitud.action = "TR065OP.asp";
			frmSolicitud.submit();
		}
	}
  }  

 function FCerrar(){
   if (confirm("¿Está seguro de cerrar la página?")){
   window.top.close();  
   }
   }
   
   
-->
 </SCRIPT>
 
<FORM id="frmSolicitud" name="frmSolicitud"   method=post>
<input type="hidden" name="hddNombres" value="<%=LNOMBRES%>">
<input type="hidden" name="hddLinea_Negocio" value="<%=LCOD_LINEA%>">
<input type="hidden" name="hddCodModalEst" value="<%=LCOD_MODAL_EST%>">
<input type="hidden" name="hddPeriodo" value="<%=LCOD_PERIODO%>">
<input type="hidden" name="hddCodAlumno" value="<%=LCOD_ALUMNO%>">
<input type="hidden" name="hddCarrera" value="<%=LCARRERA%>">
<input type="hidden" name="hddTelefono" value="<%=LTELEFONO%>">
<input type="hidden" name="hddIdMatricula" value="<%=LID_MATRICULA%>" >
<input type="hidden" name="hddUsuarioCreador" value="<%=LCOD_USUARIO%>" >
<input type="hidden" name="pagina" value="<%=pagina%>" >
<input type="hidden" name="hddHost" value="<%=LHOST%>" >
<input type="hidden" name="hddMotivo" >
<input type="hidden" name="hddTipo" value="<%=Request.Form("hddTipo")%>" >
<input type="hidden" name="hddCodUnico" value = "<%=LCOD_UNICO%>">
<input type="hidden" name="hddCodPeriodo" value = "<%=LCOD_PERIODO%>" >
<input type="hidden" name="hddrutafoto" value = "<%=LRUTAFOTO%>" >
<input type=hidden name="hddContinuar" value="<%=LCONTINUAR%>">
<input type=hidden name="hddAspOrigen" value="TR065OP.asp">
<!--hidden para amidar solicitudes-->
<input type="hidden" name="hddCod_Alumno_ANIDA" value="<%=LCOD_ALUMNO%>">
<input type="hidden" name="hddCod_Unico_ANIDA" value="<%=LCOD_UNICO%>">
<input type="hidden" name="hddCod_Periodo_ANIDA" value="<%=LCOD_PERIODO%>">
<input type="hidden" name="hddPagina_ASP_ANIDA" value="TR065OP.asp">
<input type="hidden" name="hddIndice">
<input type="hidden" name="hddEliminaAnida">
<!--fin hidden para amidar solicitudes-->

<!-- hidden para upload-->
<input type=hidden name="hddCodMotivo" value="<%=LCODMOTIVO%>">
<input type=hidden name="hddCodTramite" value="16">
<input type="hidden" name="hddRutaAlu" VALUE="<%=LRUTAALU%>">
<!-- fin  hidden para upload-->

<input type=hidden name="hddGraba" value="<%=LGRABA%>">

 <table cellpadding=0 cellspacing=0 border=0 >
	<tr>
	<td width=35></td>
	<td>
 <%=F_ESCRIBE_TITULO_PAGINA(pagina,"")%>
 <%=F_ESCRIBE_LINEA_SEPARADORA()%>
 
 <%PINTA_DATOS_ALUMNO2 LCOD_UNICO,LFECHA_SOLICITUD,LCOD_ALUMNO, LNOMBRE_ALUMNO, LTELEFONOS, LMAIL_ALUMNO, LMAIL,LDES_ESTADO, LRUTAFOTO%>	
  
  <br>
  
  <TABLE width=650 border=0 cellpadding=0 cellspacing=0>  
  <TR><TD><font face=arial size=3 color="<%=Session("WCOLOR_LINEA")%>"><b><a name=#a>Datos de la solicitud</b></font></TD>
  </TR>
 </TABLE>
 <table  width=650 border=0 cellspacing=0 cellpadding=0 bgcolor="<%=Session("WCOLOR_LINEA")%>">
	<tr><td>
		<TABLE align=center width=650 border=1 cellspacing=0 cellpadding=0  bordercolor="<%=Session("WCOLOR_LINEA")%>" bordercolordark = "#FFFFFF" bordercolorlight = "<%=Session("WCOLOR_LINEA")%>">
				<TR height=20>
					<TD width=200  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Modalidad de estudio:</b></FONT></TD>
					<TD width=230  bgcolor="#ffffff" align=left><FONT size=1 face=arial color="#000000">&nbsp;<%=LMODALIDAD%></FONT></TD>
					<TD width=150  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Ciclo:</b></FONT></TD>
					<TD width=70  bgcolor="#ffffff" align=left> <font face=arial size=1 color="#000000">&nbsp;<%=LCOD_PERIODO%></font></TD>
				</TR>
				<TR height=20>
					<TD width=200  bgcolor="#ffe7a6" align=left ><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Carrera:</b></FONT></TD>
					<TD width=450  bgcolor="#ffffff" align=left colspan=3><FONT size=1 face=arial color="#000000">&nbsp;<%=LCOD_PRODUCTO%> - <%=LDES_PRODUCTO%></FONT></TD>
				</TR>
				<TR height=20>
					<TD width=200  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Asignatura: </b></FONT></TD>
					<TD width=450  bgcolor="#ffffff" align=left colspan=3><FONT size=1 face=arial color="#000000">&nbsp;<%=LCOD_CURSO%> - <%=LDES_CURSO%></font></TD>
				</TR> 
				<TR height=20>
						<TD width=200  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Motivo: </b></FONT></TD>
						<TD width=450  bgcolor="#ffffff" align=left colspan=3><FONT size=1 face=arial color="#000000">&nbsp;<%=LMOTIVO%></FONT></TD>
				</TR> 
				<TR height=40>
					<TD width=200  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Sustento de la solicitud:</b><br><font size=1>&nbsp;</font> </FONT></TD>
					<TD bgcolor="#ffffff" align=middle COLSPAN=3><TEXTAREA readonly style="WIDTH:450px; HEIGHT: 50px; font-family:arial; font-size:10pt" name=txaMotivo cols=76><%=LSUSTENTO%></TEXTAREA></TD>
				</TR>
			</TABLE>
     </TD>
     </TR>
</TABLE>
<!-- Fin de Motivo-->

<%If NOT (LRUTAALU="" or isnull(LRUTAALU)) then%>
	<font size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>La solicitud presenta un archivo adjunto, para ver el documento de </b>&nbsp;<a href="<%=LRUTAALU%>" title="Hacer clic para ver el archivo" target="documento"><b>clic aquí.</b><IMG SRC="imagen\Clip03.ico" height=19 width=22 border=0></a></font>
	<br>
<%end if%>
<br>
<!--#include file="datos_academicos.inc"-->
<br>
<!-- Informe de evaluador-->
<TABLE width=650 border=0 cellpadding=0 cellspacing=0>
	<tr>
		<td><font face=arial size=3 color="<%=Session("WCOLOR_LINEA")%>"><b><a name=#b>Informe del asesor de riesgo</b></font></td>		
   	</tr>
</table>
<table width=650 border = 0 cellspacing = 0 cellpadding = 0 bgcolor = "<%=Session("WCOLOR_LINEA")%>">
	<tr>
	<td>
		<TABLE align=center width=650 border=1 cellspacing=0 cellpadding=0 bordercolor = "<%=Session("WCOLOR_LINEA")%>" bordercolordark = "#FFFFFF" bordercolorlight = "<%=Session("WCOLOR_LINEA")%>">
			<TR height=20>
				<TD width=200  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Atendido por:</b></FONT></TD>
				<TD width=180  bgcolor="#ffffff" align=left><FONT size=1 face=arial color="#000000">&nbsp;<%=LNOMBRE_ASESOR%></FONT></TD>
				<TD width=130  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Fecha de atención:</b></FONT></TD>
				<TD width=140  bgcolor="#ffffff" align=left><FONT size=1 face=arial color="#000000">&nbsp;<%=LFECHA_ASESOR%> (dd/mm/aaaa) </FONT></TD>
			</TR>
			<TR height=20>
				<TD width=200  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Observaciones:</b></FONT></TD>
				<TD width=450  bgcolor="#ffffff" align=left colspan = 3><FONT size=1 face=arial color="#000000">
					<TEXTAREA style="WIDTH: 450px; HEIGHT: 60px; font-family:arial; font-size:10pt" cols=55  name=txaAse readonly ><%=LOBS_ASESOR%></TEXTAREA>
					</FONT></TD>
			</TR>
		</TABLE>
	</td>
	</tr>
</table>

<%If NOT (LRUTA="" or isnull(LRUTA)) then%>
	<font size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>La evaluación del asesor presenta un archivo adjunto, para ver el documento de </b>&nbsp;<a href="<%=LRUTA%>" title="Hacer clic para ver el archivo" target="documento"><b>clic aquí.</b><IMG SRC="imagen\Clip03.ico" height=19 width=22 border=0></a></font>
<BR>
<%end if%>


<BR>
<TABLE width=650 border=0 cellpadding=0 cellspacing=0>
	<tr>
		<td><font face=arial size=3 color="<%=Session("WCOLOR_LINEA")%>"><b><a name=#b>Informe del comité evaluador</b></font></td>		
   	</tr>
</table>
<table width=650 border = 0 cellspacing = 0 cellpadding = 0 bgcolor = "<%=Session("WCOLOR_LINEA")%>">
	<tr>
	<td>
		<TABLE align=center width=650 border=1 cellspacing=0 cellpadding=0 bordercolor = "<%=Session("WCOLOR_LINEA")%>" bordercolordark = "#FFFFFF" bordercolorlight = "<%=Session("WCOLOR_LINEA")%>">
			<TR height=20>
				<TD width=200  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Atendido por:</b></FONT></TD>
		<%	if readonly="readonly" then%>
				<TD width=180  bgcolor="#ffffff" align=left><FONT size=1 face=arial color="#000000">&nbsp;<%=LNOMBRE_COMITE%></FONT></TD>
				
			<%else 
				strSQL="SELECT U.cod_usuario, P.APELLIDO_PATERN ||' '|| P.APELLIDO_MATERN ||' '|| P.NOMBRES NOMBRE "&_
				" FROM usuario U, PERSONA P "&_
				" where U.cod_usuario = '" &LCOD_USUARIO& "'"&_ 
				"   AND U.COD_PERSONA = P.COD_PERSONA "     

				'On error resume next
				Set rsEvacou= objConexion.Execute(strSQL)
				if not rsEvacou.eof then %>
				   <TD width=180  bgcolor="#ffffff" align=left><FONT size=1 face=arial color="#000000">&nbsp;<%=rsEvacou("Nombre")%></FONT></TD>
				<%end if
				rsEvacou.close
				set rsEvacou=Nothing
			end if%>
				<TD width=130  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Fecha de atención:</b></FONT></TD>
				<%If readonly = "readonly" then%>
				<TD width=140  bgcolor="#ffffff" align=left><FONT size=1 face=arial color="#000000">&nbsp;<%=LFECHA_COMITE%> (dd/mm/aaaa) </FONT></TD>
				<%ELSE%>
				<TD width=140  bgcolor="#ffffff" align=left><FONT size=1 face=arial color="#000000">&nbsp;<%=LFECHA_ACTUAL%> (dd/mm/aaaa) </FONT></TD>
				<%END IF%>
			</TR>
			<TR height=20>
				<TD width=200  bgcolor="#ffe7a6" align=left><FONT size=2 face=arial color="<%=Session("WCOLOR_LINEA")%>"><b>&nbsp;Observaciones:</b><br><font size=1>&nbsp;(Max. 500 caracteres)</font> </FONT></TD>
				<TD width=450  bgcolor="#ffffff" align=left COLSPAN = 3><FONT size=1 face=arial color="#000000">
						<TEXTAREA onkeypress="validarDato('x');"  style="WIDTH: 450px; HEIGHT: 60px; font-family:arial; font-size:10pt" cols=55 <%=readonly%> name=txaObs ><%=LOBS_COMITE%></TEXTAREA>
					</FONT></TD>
			</TR>
		</TABLE>
		</td>
		</tr>
	</table>

<br>
 <!--Botones --> 
<TABLE  width=650 border=0 cellspacing=0 cellpadding=0>
	<TR>
	<% 
	If LESTADO = "PE" and readonly <> "readonly" then%>
		<%if LPERMISO_8 = "SI" then%>
			<TD  width="40%" vAlign=top align=right>
				<table align=rigth width=100 border=1 bordercolor="#000000" bordercolordark="#ffffff" bordercolorlight="#000000" cellpadding=0 cellspacing=0>
					<tr>
					<td  width=120 align=middle onClick="FEnviar(0);" bgcolor="<%=Session("WCOLOR_LINEA")%>" height=30  style="CURSOR:  hand">
					<font face=arial size=2 color="#ffe7a6"><b>Guardar</b></font></td>
					</tr>
				</table>&nbsp;
			</TD>
			<td width=25% vAlign=top align=center>
				<table align=center width=100 border=1 bordercolor="#000000" bordercolordark="#FFFFFF" bordercolorlight="#000000" cellpadding=0 cellspacing=0>
					<tr>
					<td  width=130 align=center bgcolor="<%=Session("WCOLOR_LINEA")%>" height=30  onclick="FEnviar(1);" style="CURSOR:  hand">
					<font face=arial size=2 color="#FFE7A6"><b>Enviar al director</b></td>
					</tr>
				</table>
			</td>
			<TD align=left vAlign=top width="47%">
				<table align=left width=100 border=1 bordercolor="#000000" bordercolordark="#ffffff" bordercolorlight="#000000" cellpadding=0 cellspacing=0>
					<tr>
					<td  width=130 align=middle bgcolor="<%=Session("WCOLOR_LINEA")%>" height=30  Onclick="FCerrar();" style="CURSOR:  hand">
					<font face=arial size=2 color="#ffe7a6"><b>Cerrar</b></font></td>
					</tr>
				</table>	 
			</TD>
		<%else%>
			<TD  width="47%" vAlign=top align=right>
				<table align=rigth width=100 border=1 bordercolor="#000000" bordercolordark="#ffffff" bordercolorlight="#000000" cellpadding=0 cellspacing=0>
					<tr>
					<td  width=120 align=middle onClick="FEnviar(0);" bgcolor="<%=Session("WCOLOR_LINEA")%>" height=30  style="CURSOR:  hand">
					<font face=arial size=2 color="#ffe7a6"><b>Guardar</b></font></td>
					</tr>
				</table>&nbsp;
			</TD>
			<td width="5%">
			</td>
			<TD align=left vAlign=top width="47%">
				<table align=left width=100 border=1 bordercolor="#000000" bordercolordark="#ffffff" bordercolorlight="#000000" cellpadding=0 cellspacing=0>
					<tr>
					<td  width=130 align=middle bgcolor="<%=Session("WCOLOR_LINEA")%>" height=30  Onclick="FCerrar();" style="CURSOR:  hand">
					<font face=arial size=2 color="#ffe7a6"><b>Cerrar</b></font></td>
					</tr>
				</table>	 
			</TD>
		<%End If%>
	<%else%>
		<TD align=center vAlign=top width="47%">
			<table align=center width=100 border=1 bordercolor="#000000" bordercolordark="#ffffff" bordercolorlight="#000000" cellpadding=0 cellspacing=0>
				<tr>
				<td  width=130 align=middle bgcolor="<%=Session("WCOLOR_LINEA")%>" height=30  Onclick="FCerrar();" style="CURSOR:  hand">
				<font face=arial size=2 color="#ffe7a6"><b>Cerrar</b></font></td>
				</tr>
			</table>	 
		</TD>
	<%End if%>
	</TR>
</TABLE><!-- Fin de Botones-->
	<!--Pie de Página-->
<!--#include file="../include/piepagina.asp" -->
<!-- FIn de Pie de Página-->
	</td>
	</tr>
   </table>	
	
</FORM>


<SCRIPT LANGUAGE="JAvaScript">
  window.status="Listo."
</SCRIPT>
<!--#include file="../cierra_conexion_A.inc"-->
</BODY>
</HTML>
 