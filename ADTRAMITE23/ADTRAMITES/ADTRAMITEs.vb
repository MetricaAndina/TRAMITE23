Imports System
Imports System.Data
Imports System.Data.OracleClient
Imports Oracle.ApplicationBlocks.Data

Namespace ADTRAMITES

    Public Class clsTramites

        Private strCadenaConexion As String
        Private strMensajeError As String

        Public WriteOnly Property CadenaConexion() As String
            Set(ByVal Value As String)
                strCadenaConexion = Value
            End Set
        End Property

        Public ReadOnly Property ErrorMensaje() As String
            Get
                Return strMensajeError
            End Get
        End Property
        
         '---- FUNCIÓN AGREGADA SEP-2007-566 ----

        Public Function UsuarioValido(ByVal sUSER As String, ByVal sPWD As String) As Boolean
            Dim oLogon As New UPCLGNLib.NetClient
            Dim Err As Integer

            Try
                '' **** OJO: Comentar cuando se haga el pase a DESO y PROD ****
                Err = oLogon.Logon(UPCLGNLib.snLogonStyle.snLogonStyleSilent, LCase(Trim(sUSER)), sPWD)
                If Err <> 0 Then
                    Return False
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        '-----------------------------------------

        Public Function ConsultaAlumno(ByVal cod_Unico As String, _
            ByVal cod_periodo As String) As DataSet

            Dim sqlOraParam(2) As OracleParameter
            Try
                sqlOraParam(0) = New OracleParameter("INTCOD_UNICO", OracleType.Number, 15)
                sqlOraParam(0).Direction = ParameterDirection.Input
                sqlOraParam(0).Value = cod_Unico

                sqlOraParam(1) = New OracleParameter("INTCOD_PERIODO", OracleType.Char, 6)
                sqlOraParam(1).Direction = ParameterDirection.Input
                sqlOraParam(1).Value = cod_periodo

                sqlOraParam(2) = New OracleParameter("SEL_CURSOR", OracleType.Cursor)
                'sqlOraParam(2) = New OracleParameter("xxxxxx", OracleType.Cursor)
                sqlOraParam(2).Direction = ParameterDirection.Output

                strMensajeError = ""
                Return OraHelper.ExecuteDataset(strCadenaConexion, CommandType.StoredProcedure, "TRA$PK_DOCIDE_ASP.SP_OBTENER_TRA_DOCIDE_ASP", sqlOraParam)

            Catch ex As Exception
                strMensajeError = ex.Message
                Return Nothing
            End Try
        End Function

        Public Function DatosAlumno(ByVal plinea As String, ByVal palumno As String) As DataSet

            Dim sqlOraParam(2) As OracleParameter
            Try
                sqlOraParam(0) = New OracleParameter("PLINEA", OracleType.VarChar, 1)
                sqlOraParam(0).Direction = ParameterDirection.Input
                sqlOraParam(0).Value = plinea

                sqlOraParam(1) = New OracleParameter("PALUMNO", OracleType.VarChar, 10)
                sqlOraParam(1).Direction = ParameterDirection.Input
                sqlOraParam(1).Value = palumno

                sqlOraParam(2) = New OracleParameter("SEL_CURSOR", OracleType.Cursor)
                sqlOraParam(2).Direction = ParameterDirection.Output

                strMensajeError = ""
                Return OraHelper.ExecuteDataset(strCadenaConexion, CommandType.StoredProcedure, "TRA$PK_DOCIDE_ASP.SP_DATOS_ALUMNO_ASP", sqlOraParam)
            Catch ex As Exception
                strMensajeError = ex.Message
                Return Nothing
            End Try
        End Function

        Public Function DatosMatricula(ByVal plinea As String, ByVal palumno As String) As DataSet

            Dim sqlOraParam(2) As OracleParameter
            Try
                sqlOraParam(0) = New OracleParameter("PLINEA", OracleType.Char, 1)
                sqlOraParam(0).Direction = ParameterDirection.Input
                sqlOraParam(0).Value = plinea

                sqlOraParam(1) = New OracleParameter("PALUMNO", OracleType.Char, 9)
                sqlOraParam(1).Direction = ParameterDirection.Input
                sqlOraParam(1).Value = palumno

                sqlOraParam(2) = New OracleParameter("SEL_CURSOR", OracleType.Cursor)
                sqlOraParam(2).Direction = ParameterDirection.Output

                strMensajeError = ""
                Return OraHelper.ExecuteDataset(strCadenaConexion, CommandType.StoredProcedure, "TRA$PK_DOCIDE_ASP.SP_OBTENER_MATRICULA_ASP", sqlOraParam)
            Catch ex As Exception
                strMensajeError = ex.Message
                Return Nothing
            End Try
        End Function

        Public Function DatosReserva(ByVal plinea As String, ByVal palumno As String) As DataSet

            Dim sqlOraParam(2) As OracleParameter
            Try
                sqlOraParam(0) = New OracleParameter("PLINEA", OracleType.Char, 1)
                sqlOraParam(0).Direction = ParameterDirection.Input
                sqlOraParam(0).Value = plinea

                sqlOraParam(1) = New OracleParameter("PALUMNO", OracleType.Char, 9)
                sqlOraParam(1).Direction = ParameterDirection.Input
                sqlOraParam(1).Value = palumno

                sqlOraParam(2) = New OracleParameter("SEL_CURSOR", OracleType.Cursor)
                sqlOraParam(2).Direction = ParameterDirection.Output

                strMensajeError = ""
                Return OraHelper.ExecuteDataset(strCadenaConexion, CommandType.StoredProcedure, "TRA$PK_DOCIDE_ASP.SP_OBTENER_RESERVA_ASP", sqlOraParam)
            Catch ex As Exception
                strMensajeError = ex.Message
                Return Nothing
            End Try
        End Function

        Public Function Titulo(ByVal conexion As String, _
                                ByVal PLINEA As String, _
                                ByVal PTRAMITE As String) As String

            Dim CADENA As String = "SELECT DISTINCT substr(upper(DESCRIPCION),1,1)||substr(lower(DESCRIPCION),2) " & _
                                   "FROM TRAMITES " & _
                                   "WHERE COD_LINEA_NEGOCIO = :COD_LINEA_NEGOCIO " & _
                                   "  AND COD_TRAMITE = :COD_TRAMITE "

            Dim oraConn As OracleConnection = New OracleConnection(conexion)

            Dim CMD As New OracleCommand(CADENA, oraConn)
            Dim param1 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            Dim param2 As New OracleParameter("COD_TRAMITE", OracleType.Number)
            param1.Value = PLINEA
            param2.Value = PTRAMITE
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)

            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                myReader.Close()
            Catch ex As Exception
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try
        End Function

        Public Function Sede(ByVal conexion As String, _
                                ByVal PUSUARIO As String) As String

            Dim CADENA As String = " SELECT SF_SEDE_USUARIO(:COD_USUARIO) FROM DUAL"

            'Dim CADENA As String = "SELECT DISTINCT substr(upper(DESCRIPCION),1,1)||substr(lower(DESCRIPCION),2) " & _
            '                       "FROM TRAMITES " & _
            '                       "WHERE COD_LINEA_NEGOCIO = '" & PLINEA & "' " & _
            '                       "  AND COD_TRAMITE = " & PTRAMITE

            Dim oraConn As OracleConnection = New OracleConnection(conexion)

            Dim CMD As New OracleCommand(CADENA, oraConn)
            Dim param1 As New OracleParameter("COD_USUARIO", OracleType.VarChar)
            param1.Value = UCase(PUSUARIO)
            CMD.Parameters.Add(param1)

            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                myReader.Close()
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try
        End Function

        Public Function PuntoWF(ByVal conexion As String, _
                                ByVal ptramite As String, _
                                ByVal pciclo As String, _
                                ByVal punico As String) As String

            ' "SELECT to_char(count(1)) " & _
            Dim CADENA As String = " SELECT ACTIVITY_LABEL " & _
                                    " FROM WF_ITEM_ACTIVITY_STATUSES_V " & _
                                    " WHERE ITEM_TYPE = LPAD(:ITEM_TYPE,3,0) " & _
                                    " AND ITEM_KEY = :ITEM_KEY " & _
                                    " AND ACTIVITY_STATUS_CODE ='NOTIFIED'"

            Dim oraConn As OracleConnection = New OracleConnection(conexion)

            Dim CMD As New OracleCommand(CADENA, oraConn)
            Dim param1 As New OracleParameter("ITEM_TYPE", OracleType.Number)
            Dim param2 As New OracleParameter("ITEM_KEY", OracleType.VarChar)
            param1.Value = ptramite
            param2.Value = pciclo & punico
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)

            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                Return ""
                myReader.Close()
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try
        End Function

        Public Function NombreUsuario(ByVal conexion As String, _
                                        ByVal pusuario As String) As String

            Dim CADENA As String = " SELECT P.apellido_patern ||' '|| apellido_matern||', '||nombres nombre FROM USUARIO U, PERSONA P" & _
                                    " WHERE U.COD_USUARIO = :COD_USUARIO  " & _
                                    "  AND U.COD_PERSONA = P.COD_PERSONA"

            Dim oraConn As OracleConnection = New OracleConnection(conexion)

            Dim CMD As New OracleCommand(CADENA, oraConn)
            Dim param1 As New OracleParameter("COD_USUARIO", OracleType.VarChar)
            param1.Value = pusuario
            CMD.Parameters.Add(param1)

            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                myReader.Close()
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try

        End Function

        Public Function CodAlumno(ByVal conexion As String, _
        ByVal plinea As String, ByVal PUSUARIO As String) As String

            Dim CADENA As String = "select cod_alumno " & _
                                    " from alumno a " & _
                                    " where a.cod_usuario = :ALUMNO " & _
            "and cod_linea_negocio = :COD_LINEA_NEGOCIO "

            Dim oraConn As OracleConnection = New OracleConnection(conexion)

            Dim CMD As New OracleCommand(CADENA, oraConn)
            Dim param1 As New OracleParameter("ALUMNO", OracleType.VarChar)
            Dim param2 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            param1.Value = PUSUARIO
            param2.Value = plinea
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)

            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                myReader.Close()
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try
        End Function

        Public Function SolicitudPendiente(ByVal conexion As String, ByVal plinea As String, _
        ByVal palumno As String, ByVal pdocumento As String) As String

            Dim CADENA As String = "select to_char(count(1)) num from solicitud " & _
                                    " where cod_linea_negocio = :COD_LINEA_NEGOCIO " & _
                                    "  and cod_alumno = :COD_ALUMNO " & _
                                    "  and cod_tramite = 23 " & _
                                    "  and cod_documento = :COD_DOCUMENTO " & _
                                    "  and ind_entregado <> 'SI' " & _
                                    "  and estado = 'PE'"

            Dim oraConn As OracleConnection = New OracleConnection(conexion)

            Dim CMD As New OracleCommand(CADENA, oraConn)
            Dim param1 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            Dim param2 As New OracleParameter("COD_ALUMNO", OracleType.VarChar)
            Dim param3 As New OracleParameter("COD_DOCUMENTO", OracleType.VarChar)
            param1.Value = plinea
            param2.Value = palumno
            param3.Value = pdocumento
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)
            CMD.Parameters.Add(param3)

            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                myReader.Close()
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try
        End Function

        Public Function FechaActual(ByVal conexion As String) As String

            Dim CADENA As String = "SELECT to_char(sysdate,'dd/mm/yyyy') from dual"

            Dim CMD As New OracleCommand
            Dim oraConn As OracleConnection = New OracleConnection(conexion)
            CMD = New OracleCommand(CADENA, oraConn)
            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                myReader.Close()
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try
        End Function

        Public Function EsMonitor(ByVal conexion As String, ByVal punico As String, _
        ByVal pciclo As String, ByVal pusuario As String) As String

            Dim CADENA As String = "select decode(COUNT(1),0,'NO','SI') " & _
                                    " from solicitud s, motivo m, usuario_rol r  " & _
                                    " where s.cod_unico = :COD_UNICO " & _
                                    "  and s.cod_periodo = :COD_PERIODO " & _
                                    "  and s.cod_motivo = m.cod_motivo " & _
                                    "  and m.cod_rol = r.cod_rol  " & _
                                    "  and r.cod_usuario = :COD_USUARIO "

            Dim oraConn As OracleConnection = New OracleConnection(conexion)

            Dim CMD As New OracleCommand(CADENA, oraConn)
            Dim param1 As New OracleParameter("COD_UNICO", OracleType.VarChar)
            Dim param2 As New OracleParameter("COD_PERIODO", OracleType.VarChar)
            Dim param3 As New OracleParameter("COD_USUARIO", OracleType.VarChar)
            param1.Value = punico
            param2.Value = pciclo
            param3.Value = pusuario
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)
            CMD.Parameters.Add(param3)

            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                myReader.Close()
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try
        End Function

        Public Function Mensaje(ByVal conexion As String, ByVal parchivo As String, _
            ByVal pcod_mensaje As String, ByVal pcod_tramite As String, _
            ByVal pcod_local As String, _
            ByRef ptexto As String, _
            ByRef ptexto1 As String, _
            ByRef ptexto2 As String, _
            ByRef ptexto3 As String, _
            ByRef perror As String) As Boolean

            Mensaje = False
            Dim strSQL As String, pcount As Integer
            Dim DataSet As New DataSet
            Dim dr As DataRow
            strSQL = "select TEXTO3, TEXTO2, TEXTO1, TEXTO   " & _
                    " from mensaje_tramites " & _
                    " where ARCHIVO = :ARCHIVO and " & _
                    " COD_MENSAJE = :COD_MENSAJE and " & _
                    " COD_TRAMITE = :COD_TRAMITE " '& " and " & _
            '" COD_LOCAL = '" & pcod_local & "' "

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("ARCHIVO", OracleType.VarChar)
            Dim param2 As New OracleParameter("COD_MENSAJE", OracleType.Number)
            Dim param3 As New OracleParameter("COD_TRAMITE", OracleType.Number)
            param1.Value = parchivo
            param2.Value = pcod_mensaje.Trim
            param3.Value = pcod_tramite.Trim
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)
            CMD.Parameters.Add(param3)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(DataSet)
                pcount = DataSet.Tables(0).Rows.Count()

                If pcount = 0 Then
                    perror = "No se encontró la descripción del mensaje."
                Else
                    For Each dr In DataSet.Tables(0).Rows
                        ptexto = dr("texto")
                        ptexto1 = IIf(IsDBNull(dr("texto1")), "", dr("texto1"))
                        ptexto2 = IIf(IsDBNull(dr("texto2")), "", dr("texto2"))
                        ptexto3 = IIf(IsDBNull(dr("texto3")), "", dr("texto3"))
                        perror = ""
                    Next
                End If
            Catch ex As Exception
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
            Mensaje = True

        End Function

        Public Function ObtieneLimites(ByVal conexion As String, ByVal plinea As String, _
                                        ByVal pmodal As String, ByVal pciclo As String, _
                                        ByVal ptramite As String, ByVal pindicador As String, _
                                        ByVal pfecini As String, ByVal pfecfin As String, _
                                        ByRef pds As DataSet, ByRef perror As String) As Boolean

            ObtieneLimites = False
            Dim strSQL As String, pcount As Integer
            Dim DataSet As New DataSet
            Dim dr As DataRow
            strSQL = "SELECT DISTINCT C.COD_TRAMITE, T.DESCRIPCION, C.LIMITE_1, C.LIMITE_2, c.cod_periodo " & _
                    " FROM CONFIGURACION_TRAMITE C, TRAMITES T " & _
                    " WHERE C.COD_LINEA_NEGOCIO = :COD_LINEA_NEGOCIO " & _
                    "   AND C.COD_MODAL_EST = :COD_MODAL_EST " & _
                    "   AND C.COD_TRAMITE = :COD_TRAMITE " & _
                    "   AND C.COD_LINEA_NEGOCIO = T.COD_LINEA_NEGOCIO " & _
                    "   AND C.COD_MODAL_EST = T.COD_MODAL_EST " & _
                    "   AND C.COD_PERIODO = T.COD_PERIODO " & _
                    "   AND C.COD_TRAMITE = T.COD_TRAMITE " & _
                    "   AND C.COD_INDICADOR = :COD_INDICADOR " & _
                    "   AND C.COD_PERIODO = :COD_PERIODO " & _
                    " Order by c.cod_periodo asc "
            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            Dim param2 As New OracleParameter("COD_MODAL_EST", OracleType.VarChar)
            Dim param3 As New OracleParameter("COD_TRAMITE", OracleType.Number)
            Dim param4 As New OracleParameter("COD_INDICADOR", OracleType.Number)
            Dim param5 As New OracleParameter("COD_PERIODO", OracleType.VarChar)
            param1.Value = plinea
            param2.Value = pmodal
            param3.Value = ptramite
            param4.Value = pindicador
            param5.Value = pciclo
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)
            CMD.Parameters.Add(param3)
            CMD.Parameters.Add(param4)
            CMD.Parameters.Add(param5)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(DataSet)
                pds = DataSet
            Catch ex As Exception
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
            ObtieneLimites = True

        End Function


        Public Function ObtieneCiclos(ByVal conexion As String, ByVal plinea As String, _
                                        ByVal pmodal As String, ByVal pfecini As String, _
                                        ByVal pfecfin As String, ByRef pds As DataSet, _
                                        ByRef perror As String) As Boolean

            ObtieneCiclos = False
            Dim strSQL As String, pcount As Integer
            Dim DataSet As New DataSet
            Dim dr As DataRow
            strSQL = "SELECT DISTINCT P.COD_PERIODO DES_FRECUENCIA" & _
                    " FROM PERIODO P " & _
                    " WHERE P.COD_LINEA_NEGOCIO = :COD_LINEA_NEGOCIO " & _
                    " 	AND P.COD_MODAL_EST = :COD_MODAL_EST " & _
                    "	AND (P.FECHA_INICIO BETWEEN TO_DATE(:FECHA_INICIO,'DD/MM/YYYY') AND TO_DATE(:FECHA_INICIO2,'DD/MM/YYYY') OR" & _
                    "  	     P.FECHA_TERMINO BETWEEN TO_DATE(:FECHA_TERMINO,'DD/MM/YYYY') AND TO_DATE(:FECHA_TERMINO,'DD/MM/YYYY') OR" & _
                    "        TO_DATE(:FECHA,'DD/MM/YYYY') BETWEEN P.FECHA_INICIO AND P.FECHA_TERMINO  )"
            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            Dim param2 As New OracleParameter("COD_MODAL_EST", OracleType.VarChar)
            Dim param3 As New OracleParameter("FECHA_INICIO", OracleType.VarChar)
            Dim param4 As New OracleParameter("FECHA_INICIO2", OracleType.VarChar)
            Dim param5 As New OracleParameter("FECHA_TERMINO", OracleType.VarChar)
            Dim param6 As New OracleParameter("FECHA_TERMINO2", OracleType.VarChar)
            Dim param7 As New OracleParameter("FECHA", OracleType.VarChar)
            param1.Value = plinea
            param2.Value = pmodal
            param3.Value = pfecini
            param4.Value = pfecfin
            param5.Value = pfecini
            param6.Value = pfecfin
            param7.Value = pfecini
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)
            CMD.Parameters.Add(param3)
            CMD.Parameters.Add(param4)
            CMD.Parameters.Add(param5)
            CMD.Parameters.Add(param6)
            CMD.Parameters.Add(param7)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(DataSet)
                pds = DataSet
            Catch ex As Exception
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
            ObtieneCiclos = True

        End Function

        Public Function SegTramites(ByVal conexion As String, ByVal pid As String, _
            ByRef dsT As DataSet, ByRef perror As String) As Boolean

            SegTramites = True
            Dim strSQL As String

            strSQL = "select distinct cod_tramite, substr(upper(des_tramite),1,1)||substr(lower(des_tramite),2) des_tramite " & _
                        " from tmp_seguimiento " & _
                        " where seq_id = :SEQ_ID "

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("SEQ_ID", OracleType.Number)
            param1.Value = pid
            CMD.Parameters.Add(param1)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(dsT)
            Catch ex As Exception
                SegTramites = False
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try

        End Function

        Public Function SegPeriodos(ByVal conexion As String, ByVal pid As String, _
            ByRef dsT As DataSet, ByRef perror As String) As Boolean

            SegPeriodos = True
            Dim strSQL As String

            strSQL = "select distinct ANYO, DES_FRECUENCIA,mes, ciclo  " & _
                        " from tmp_seguimiento " & _
                        " where seq_id = :SEQ_ID " & _
                        " order by ANYO, mes, ciclo "

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("SEQ_ID", OracleType.Number)
            param1.Value = pid
            CMD.Parameters.Add(param1)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(dsT)
            Catch ex As Exception
                SegPeriodos = False
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try

        End Function
        Public Function SegValores(ByVal conexion As String, ByVal pid As String, _
        ByVal pusuario As String, ByVal ptramite As String, _
           ByRef dsT As DataSet, ByRef perror As String) As Boolean

            'OBTIENE VALOR Y COLOR DE SEMAFORO POR USUARIO 
            ' Usuario:  indicador tipo  1: COUNTER, ALUMNO, TOTAL, PORC
            '                           2: PR, NP, PE, PP, IN, AN
            '                           3: DP, ANTES, DESPUES
            '                           4: TOTAL, PORC

            SegValores = True
            Dim strSQL As String

            strSQL = "select VALOR, COLOR " & _
                    " from tmp_seguimiento " & _
                    " where seq_id = :SEQ_ID " & _
                    "  and UPPER(usuario) = :USUARIO " & _
                    "  and cod_tramite = :COD_TRAMITE " & _
                    " order by ANYO, ciclo, MES "

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("SEQ_ID", OracleType.Number)
            Dim param2 As New OracleParameter("USUARIO", OracleType.VarChar)
            Dim param3 As New OracleParameter("COD_TRAMITE", OracleType.Number)
            param1.Value = pid
            param2.Value = UCase(pusuario)
            param3.Value = ptramite
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)
            CMD.Parameters.Add(param3)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(dsT)
            Catch ex As Exception
                SegValores = False
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try

        End Function

        Public Function ObtieneDatosMatricula(ByVal conexion As String, _
                    ByVal pid As String, _
                    ByRef pmodal As String, _
                    ByRef pnombre As String, _
                    ByRef pciclo As String, _
                    ByRef pproducto As String, _
                    ByRef pdesproducto As String, _
                    ByRef perror As String) As Boolean

            ObtieneDatosMatricula = False
            Dim strSQL As String, pcount As Integer
            Dim DataSet As New DataSet
            Dim dr As DataRow
            strSQL = "select m.cod_modal_est, mo.nombre, m.cod_period_mat, cod_producto, nvl(p.desc_especial,p.descripcion) desp " & _
                    " from matricula m, modalidad_estud mo, producto p " & _
                    " where m.id  = :SEQ_ID " & _
                    " and m.cod_linea_negocio = mo.cod_linea_negocio" & _
                    " and m.cod_modal_est = mo.cod_modal_est" & _
                    " and m.cod_produc_mat = p.cod_producto" & _
                    " and m.cod_linea_negocio = p.cod_linea_negocio"

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("SEQ_ID", OracleType.Number)
            param1.Value = pid
            CMD.Parameters.Add(param1)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(DataSet)
                pcount = DataSet.Tables(0).Rows.Count()

                If pcount = 0 Then
                    perror = "No se encontró la descripción del mensaje."
                Else
                    For Each dr In DataSet.Tables(0).Rows
                        pmodal = dr("cod_modal_est")
                        pnombre = IIf(IsDBNull(dr("nombre")), "", dr("nombre"))
                        pciclo = IIf(IsDBNull(dr("cod_period_mat")), "", dr("cod_period_mat"))
                        pproducto = IIf(IsDBNull(dr("cod_producto")), "", dr("cod_producto"))
                        pdesproducto = IIf(IsDBNull(dr("desp")), "", dr("desp"))
                        perror = ""
                    Next
                End If
            Catch ex As Exception
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
            ObtieneDatosMatricula = True

        End Function

        Public Function DatosDocumento(ByVal conexion As String, ByVal pmotivo As String, _
                    ByRef pCodDocu As String, _
                    ByRef pDesDocu As String, _
                    ByRef pCodFormato As String, _
                    ByRef pMinVez As String, _
                    ByRef pMaxVez As String, _
                    ByRef pCaja As String, _
                    ByRef pBoleta As String, _
                    ByRef pCodSql As String, _
                    ByRef pMensaje As String, _
                    ByRef perror As String) As Boolean

            DatosDocumento = False
            Dim strSQL As String, pcount As Integer
            Dim DataSet As New DataSet
            Dim dr As DataRow
            strSQL = "select d.cod_documento, d.DESCRIPCION, cod_formato, min_vez, " & _
            " max_vez, m.PAGA_CAJA, m.PAGA_BOLETA, m.CODIGO_SQL, D.MENSAJE  " & _
                    "from motivo m, documento_tramite d " & _
                    "where m.cod_motivo = :COD_MOTIVO " & _
                    " and m.cod_documento = d.cod_documento "

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("COD_MOTIVO", OracleType.Number)
            param1.Value = pmotivo
            CMD.Parameters.Add(param1)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(DataSet)
                pcount = DataSet.Tables(0).Rows.Count()

                If pcount = 0 Then
                    perror = "No se encontró la descripción del mensaje."
                Else
                    For Each dr In DataSet.Tables(0).Rows
                        pCodDocu = dr("cod_documento")
                        pDesDocu = IIf(IsDBNull(dr("DESCRIPCION")), "", dr("DESCRIPCION"))
                        pCodFormato = IIf(IsDBNull(dr("cod_formato")), "", dr("cod_formato"))
                        pMinVez = IIf(IsDBNull(dr("min_vez")), "", dr("min_vez"))
                        pMaxVez = IIf(IsDBNull(dr("max_vez")), "", dr("max_vez"))
                        pCaja = IIf(IsDBNull(dr("PAGA_CAJA")), "", dr("PAGA_CAJA"))
                        pBoleta = IIf(IsDBNull(dr("PAGA_BOLETA")), "", dr("PAGA_BOLETA"))
                        pCodSql = IIf(IsDBNull(dr("CODIGO_SQL")), "", dr("CODIGO_SQL"))
                        pMensaje = IIf(IsDBNull(dr("MENSAJE")), "", dr("MENSAJE"))
                        perror = ""
                    Next
                End If
            Catch ex As Exception
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
            DatosDocumento = True

        End Function

        Public Function ListaDocumentos(ByVal conexion As String, _
                                    ByVal plinea As String, _
                                    ByRef obj As Object, _
                                    ByRef perror As String) As Boolean

            '********* Obtiene la lista de documentos
            Dim strSQL As String, dataset As New DataSet

            strSQL = "select 0 cod_motivo, '-- Seleccione un documento --' descripcion from dual union " & _
            "select cod_motivo, descripcion " & _
            "from motivo " & _
            "where cod_tramite = 23 " & _
            "  and cod_linea_negocio = :COD_LINEA_NEGOCIO " & _
            "  and habilitado = 'SI'"

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            param1.Value = plinea
            CMD.Parameters.Add(param1)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(dataset)

                obj.DataSource = dataset.Tables(0).DefaultView
                obj.DataValueField = "cod_motivo"
                obj.DataTextField = "descripcion"
                obj.DataBind()

                ListaDocumentos = True
            Catch ex As Exception
                ListaDocumentos = False
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function ValidaIngreso(ByVal conexion As String, _
                                        ByVal PLINEA As String, _
                                        ByVal PALUMNO As String, _
                                        ByVal PMOTIVO As String, _
                                        ByVal PTIPOUSU As String, _
                                        ByRef PCODERROR As String, _
                                        ByRef PMENERROR As String, _
                                        ByRef PPARA As String) As Boolean
            Dim strSQL As String
            strSQL = "TRA$PK_DOCIDE_ASP.SP_VALIDA_INGRESO"

            Dim valor As Integer
            Dim oCx As New OracleConnection(conexion)
            Dim arParms(6) As OracleParameter

            arParms(0) = New OracleParameter("PLINEA", OracleType.Char, 1)
            arParms(0).Value = PLINEA
            arParms(1) = New OracleParameter("PALUMNO", OracleType.Char, 9)
            arParms(1).Value = PALUMNO
            arParms(2) = New OracleParameter("PMOTIVO", OracleType.Number, 15)
            arParms(2).Value = PMOTIVO
            arParms(3) = New OracleParameter("PTIPOUSU", OracleType.VarChar, 3)
            arParms(3).Value = PTIPOUSU
            arParms(4) = New OracleParameter("PCODERR", OracleType.VarChar, 20)
            arParms(4).Direction = ParameterDirection.Output
            arParms(5) = New OracleParameter("PMENERR", OracleType.VarChar, 300)
            arParms(5).Direction = ParameterDirection.Output
            arParms(6) = New OracleParameter("PPARA", OracleType.VarChar, 2)
            arParms(6).Direction = ParameterDirection.Output

            Try
                oCx.Open()
                valor = OraHelper.ExecuteNonQuery(oCx, CommandType.StoredProcedure, strSQL, arParms)
                PCODERROR = Convert.ToString(arParms(4).Value)
                PMENERROR = Convert.ToString(arParms(5).Value)
                PPARA = Convert.ToString(arParms(6).Value)

                If valor = 1 Then
                    ValidaIngreso = True
                Else
                    ValidaIngreso = False
                End If
            Catch ex As Exception
                PCODERROR = "99"
                PMENERROR = ex.Message
                PPARA = "SI"
                ValidaIngreso = False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function ValidaIngMatri(ByVal conexion As String, _
                                        ByVal PLINEA As String, _
                                        ByVal PALUMNO As String, _
                                        ByVal PMOTIVO As String, _
                                        ByVal PMODAL As String, _
                                        ByVal PPERIODO As String, _
                                        ByVal PPRODUCTO As String, _
                                        ByVal PTIPOUSU As String, _
                                        ByRef PCODERROR As String, _
                                        ByRef PMENERROR As String, _
                                        ByRef PPARA As String) As Boolean
            Dim strSQL As String
            strSQL = "TRA$PK_DOCIDE_ASP.SP_VALIDA_INGMATRI"

            Dim valor As Integer
            Dim oCx As New OracleConnection(conexion)
            Dim arParms(9) As OracleParameter

            arParms(0) = New OracleParameter("PLINEA", OracleType.Char, 1)
            arParms(0).Value = PLINEA
            arParms(1) = New OracleParameter("PALUMNO", OracleType.Char, 9)
            arParms(1).Value = PALUMNO
            arParms(2) = New OracleParameter("PMOTIVO", OracleType.Number, 15)
            arParms(2).Value = PMOTIVO
            arParms(3) = New OracleParameter("PMODAL", OracleType.Char, 2)
            arParms(3).Value = PMODAL
            arParms(4) = New OracleParameter("PPERIODO", OracleType.Char, 6)
            arParms(4).Value = PPERIODO
            arParms(5) = New OracleParameter("PPRODUCTO", OracleType.VarChar, 8)
            arParms(5).Value = PPRODUCTO
            arParms(6) = New OracleParameter("PTIPOUSU", OracleType.VarChar, 3)
            arParms(6).Value = PTIPOUSU
            arParms(7) = New OracleParameter("PCODERR", OracleType.VarChar, 20)
            arParms(7).Direction = ParameterDirection.Output
            arParms(8) = New OracleParameter("PMENERR", OracleType.VarChar, 300)
            arParms(8).Direction = ParameterDirection.Output
            arParms(9) = New OracleParameter("PPARA", OracleType.VarChar, 2)
            arParms(9).Direction = ParameterDirection.Output

            Try
                oCx.Open()
                valor = OraHelper.ExecuteNonQuery(oCx, CommandType.StoredProcedure, strSQL, arParms)
                PCODERROR = Convert.ToString(arParms(7).Value)
                PMENERROR = Convert.ToString(arParms(8).Value)
                PPARA = Convert.ToString(arParms(9).Value)

                If valor = 1 Then
                    ValidaIngMatri = True
                Else
                    ValidaIngMatri = False
                End If
            Catch ex As Exception
                PCODERROR = "99"
                PMENERROR = ex.Message
                PPARA = "SI"
                ValidaIngMatri = False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function PrecioSpring(ByVal conexion As String, ByVal plinea As String, _
        ByVal pmodal As String, ByVal pciclo As String, ByVal pcodsql As String, _
        ByRef pprecio As String, ByRef pmoneda As String, ByRef pmensaje As String) As Boolean
            Dim cmdAdd As OracleCommand, pAdd As OracleParameter
            Dim oCx As OracleConnection = New OracleConnection(conexion)
            Dim pvarVALOR As Integer

            cmdAdd = New OracleCommand("SF_RECUPERA_PRECIO_SPRING", oCx)
            With (cmdAdd)
                .CommandType = CommandType.StoredProcedure
                pAdd = .Parameters.Add("Return_Value", OracleType.Number, 4)
                pAdd.Direction = ParameterDirection.ReturnValue
                pAdd = .Parameters.Add("PCOD_LINEA_NEGOCIO", OracleType.Char, 1)
                pAdd.Value = plinea
                pAdd = .Parameters.Add("PCOD_MODAL_EST", OracleType.Char, 2)
                pAdd.Value = pmodal
                pAdd = .Parameters.Add("PCOD_PERIODO", OracleType.Char, 6)
                pAdd.Value = pciclo
                pAdd = .Parameters.Add("PCOD_PRODUCTO", OracleType.VarChar, 10)
                pAdd.Value = pcodsql
                pAdd = .Parameters.Add("PCOD_CATEGORIA", OracleType.VarChar, 10)
                pAdd.Value = "00"
                pAdd = .Parameters.Add("PNUM_CUOTA", OracleType.VarChar, 10)
                pAdd.Value = ""
                pAdd = .Parameters.Add("PCOD_TIPO_SERVICIO", OracleType.Char, 1)
                pAdd.Value = "S"
                pAdd = .Parameters.Add("PPRECIO", OracleType.VarChar, 20)
                pAdd.Direction = ParameterDirection.Output
                pAdd = .Parameters.Add("PMONEDA", OracleType.VarChar, 4)
                pAdd.Direction = ParameterDirection.Output
                pAdd = .Parameters.Add("PMENSAJE", OracleType.VarChar, 400)
                pAdd.Direction = ParameterDirection.Output
            End With
            Try
                oCx.Open()
                cmdAdd.ExecuteNonQuery()
                pvarVALOR = Convert.ToInt32(cmdAdd.Parameters.Item("Return_Value").Value)
                pprecio = Convert.ToString(cmdAdd.Parameters.Item("pprecio").Value)
                pmoneda = Convert.ToString(cmdAdd.Parameters.Item("pmoneda").Value)
                pmensaje = Convert.ToString(cmdAdd.Parameters.Item("PMENSAJE").Value)

                If pvarVALOR = 0 Then ' no hubo error

                    Return True
                Else
                    Return False
                End If
            Catch a As System.Exception
                pmensaje = a.Message
                Return False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try

        End Function

        Public Function ObtieneDiasPago(ByVal conexion As String, ByVal plinea As String, _
                                        ByVal pmodal As String, ByVal pciclo As String, _
                                        ByVal ptramite As String, _
                                        ByVal plocal As String, _
                                        ByRef pDias As String, ByRef pcoderror As String, _
                                        ByRef pdeserror As String) As Boolean

            ObtieneDiasPago = False
            Dim strSQL As String, pcount As Integer
            Dim DataSet As New DataSet
            Dim dr As DataRow
            strSQL = " select num_dias from plazos where cod_linea_negocio= :COD_LINEA_NEGOCIO and " & _
                 " cod_modal_est= :COD_MODAL_EST and " & _
                 " label_actividad='PAGO_CAJA' and " & _
                 " cod_tramite= :COD_TRAMITE and " & _
                 " cod_local = :COD_LOCAL "


            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            Dim param2 As New OracleParameter("COD_MODAL_EST", OracleType.VarChar)
            Dim param3 As New OracleParameter("COD_TRAMITE", OracleType.Number)
            Dim param4 As New OracleParameter("COD_LOCAL", OracleType.VarChar)
            param1.Value = plinea
            param2.Value = pmodal
            param3.Value = ptramite
            param4.Value = plocal
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)
            CMD.Parameters.Add(param3)
            CMD.Parameters.Add(param4)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(DataSet)
                pcount = DataSet.Tables(0).Rows.Count()

                If pcount = 0 Then
                    pcoderror = "25"
                    pdeserror = "Modalidad de estudio : " & pmodal & " ciclo: " & pciclo
                    Exit Function
                Else
                    For Each dr In DataSet.Tables(0).Rows
                        pDias = IIf(IsDBNull(dr("num_dias")), "", dr("num_dias"))
                    Next
                End If
            Catch ex As Exception
                pcoderror = "24"
                pdeserror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
            ObtieneDiasPago = True

        End Function

        Public Function VendedorIntranet(ByVal conexion As String, ByVal plinea As String, _
                                        ByRef pvendedor As String, ByRef pdesvendedor As String, _
                                        ByRef pcoderror As String, _
                                        ByRef pdeserror As String) As Boolean

            VendedorIntranet = False
            Dim strSQL As String, pcount As Integer
            Dim DataSet As New DataSet
            Dim dr As DataRow
            strSQL = "select cod_vendedor_intranet,desc_vendedor_intranet " & _
                    " from linea_negocio " & _
                    " where cod_linea_negocio= :COD_LINEA_NEGOCIO "
            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            param1.Value = plinea
            CMD.Parameters.Add(param1)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(DataSet)
                pcount = DataSet.Tables(0).Rows.Count()

                If pcount = 0 Then
                Else
                    For Each dr In DataSet.Tables(0).Rows
                        pvendedor = IIf(IsDBNull(dr("cod_vendedor_intranet")), "", dr("cod_vendedor_intranet"))
                        pdesvendedor = IIf(IsDBNull(dr("desc_vendedor_intranet")), "", dr("desc_vendedor_intranet"))
                    Next
                End If
            Catch ex As Exception
                pcoderror = "26"
                pdeserror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
                oCx = Nothing
            End Try
            VendedorIntranet = True

        End Function

        Public Function ValidaParametros(ByVal conexion As String, _
                                        ByVal PLINEA As String, _
                                        ByRef pcompania As String, _
                                        ByRef psucursal As String, _
                                        ByRef pid_grupo_def As String, _
                                        ByRef ptipo_doc_spr As String, _
                                        ByRef pid_dscto_def As String, _
                                        ByRef presultado As String, _
                                        ByRef pcoderr As String, _
                                        ByRef pdeserr As String) As Boolean

            Dim cmdAdd As OracleCommand, pAdd As OracleParameter
            Dim oCx As OracleConnection = New OracleConnection(conexion)
            Dim pvarVALOR As Integer

            cmdAdd = New OracleCommand("PQ_OBLIGACION.SF_VERIFICA_PARAMETROS", oCx)
            With (cmdAdd)
                .CommandType = CommandType.StoredProcedure
                pAdd = .Parameters.Add("Return_Value", OracleType.Number, 4)
                pAdd.Direction = ParameterDirection.ReturnValue
                pAdd = .Parameters.Add("PCOD_LINEA_NEGOCIO", OracleType.Char, 1)
                pAdd.Value = PLINEA
                pAdd = .Parameters.Add("PTIPO_PERSONA", OracleType.Char, 1)
                pAdd.Value = "N"
                pAdd = .Parameters.Add("PCOD_COMPANIA", OracleType.VarChar, 8)
                pAdd.Direction = ParameterDirection.Output
                pAdd = .Parameters.Add("PCOD_SUCURSAL", OracleType.VarChar, 4)
                pAdd.Direction = ParameterDirection.Output
                pAdd = .Parameters.Add("PID_GRUPO_DEFAULT", OracleType.Number, 20)
                pAdd.Direction = ParameterDirection.Output
                pAdd = .Parameters.Add("PCOD_TIPO_DOCU_SPRING", OracleType.VarChar, 2)
                pAdd.Direction = ParameterDirection.Output
                pAdd = .Parameters.Add("PID_DESCUENTO_DEFAULT", OracleType.Number, 5)
                pAdd.Direction = ParameterDirection.Output
                pAdd = .Parameters.Add("PRESULTADO", OracleType.VarChar, 200)
                pAdd.Direction = ParameterDirection.Output
            End With
            Try
                oCx.Open()
                cmdAdd.ExecuteNonQuery()
                pvarVALOR = Convert.ToInt32(cmdAdd.Parameters.Item("Return_Value").Value)
                pcompania = Convert.ToString(cmdAdd.Parameters.Item("PCOD_COMPANIA").Value)
                psucursal = Convert.ToString(cmdAdd.Parameters.Item("PCOD_SUCURSAL").Value)
                pid_grupo_def = Convert.ToString(cmdAdd.Parameters.Item("PID_GRUPO_DEFAULT").Value)
                ptipo_doc_spr = Convert.ToString(cmdAdd.Parameters.Item("PCOD_TIPO_DOCU_SPRING").Value)
                pid_dscto_def = Convert.ToString(cmdAdd.Parameters.Item("PID_DESCUENTO_DEFAULT").Value)
                presultado = Convert.ToString(cmdAdd.Parameters.Item("PRESULTADO").Value)

                If pvarVALOR = 0 Then ' no hubo error
                    Return True
                Else
                    pcoderr = "29"
                    pdeserr = presultado
                    Return False
                End If
            Catch a As System.Exception
                pcoderr = "29"
                pdeserr = a.Message
                Return False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function ValidaSitAlumno(ByVal conexion As String, _
                                        ByVal PLINEA As String, _
                                        ByVal PMODAL As String, _
                                        ByVal PCICLO As String, _
                                        ByVal PALUMNO As String, _
                                        ByVal pcompania As String, _
                                        ByRef PNUMERO_CUOTAS As String, _
                                        ByRef PCUOTAS_FAC As String, _
                                        ByRef PDEUDA As String, _
                                        ByRef PRESULTADO As String, _
                                        ByRef pcoderr As String, _
                                        ByRef pdeserr As String) As Boolean
            Dim strSQL As String
            strSQL = "SP_REQUISITOS_TRA_RECEXA_ASP"

            Dim valor As Integer
            Dim oCx As New OracleConnection(conexion)
            Dim arParms(8) As OracleParameter

            arParms(0) = New OracleParameter("PCOD_LINEA_NEGOCIO", OracleType.Char, 1)
            arParms(0).Value = PLINEA
            arParms(1) = New OracleParameter("PCOD_MODAL_EST", OracleType.Char, 2)
            arParms(1).Value = PMODAL
            arParms(2) = New OracleParameter("PCOD_PERIODO", OracleType.Char, 6)
            arParms(2).Value = PCICLO
            arParms(3) = New OracleParameter("PCOD_ALUMNO", OracleType.Char, 9)
            arParms(3).Value = PALUMNO
            arParms(4) = New OracleParameter("PCOD_COMPANIA", OracleType.Char, 8)
            arParms(4).Value = pcompania
            arParms(5) = New OracleParameter("PNUMERO_CUOTAS", OracleType.Number, 10)
            arParms(5).Direction = ParameterDirection.Output
            arParms(6) = New OracleParameter("PCUOTAS_FAC", OracleType.Number, 10)
            arParms(6).Direction = ParameterDirection.Output
            arParms(7) = New OracleParameter("PDEUDA", OracleType.VarChar, 500)
            arParms(7).Direction = ParameterDirection.Output
            arParms(8) = New OracleParameter("PRESULTADO", OracleType.VarChar, 100)
            arParms(8).Direction = ParameterDirection.Output

            Try
                oCx.Open()
                valor = OraHelper.ExecuteNonQuery(oCx, CommandType.StoredProcedure, strSQL, arParms)
                PNUMERO_CUOTAS = Convert.ToString(arParms(5).Value)
                PCUOTAS_FAC = Convert.ToString(arParms(6).Value)
                PDEUDA = Convert.ToString(arParms(7).Value)
                PRESULTADO = Convert.ToString(arParms(8).Value)

                If valor = 1 Then
                    ValidaSitAlumno = True
                Else
                    ValidaSitAlumno = False
                End If
            Catch ex As Exception
                pcoderr = "30"
                pdeserr = ex.Message
                ValidaSitAlumno = False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function InsertaLog(ByVal conexion As String, _
                                        ByVal pcodunico As String, _
                                        ByVal pcodperiod As String, _
                                        ByVal pcodcompania As String, _
                                        ByVal ptipodoc As String, _
                                        ByVal pnumdoc As String, _
                                        ByVal pprecio As String, _
                                        ByVal pnumcuotas As String, _
                                        ByVal pusuario As String, _
                                        ByRef pnum As String, _
                                        ByRef pcoderr As String, _
                                        ByRef pdeserr As String) As Boolean
            Dim strSQL As String
            strSQL = "SP_INSERTA_LOG"

            Dim valor As Integer
            Dim oCx As New OracleConnection(conexion)
            Dim arParms(8) As OracleParameter

            arParms(0) = New OracleParameter("PCOD_UNICO", OracleType.Number, 15)
            arParms(0).Value = pcodunico
            arParms(1) = New OracleParameter("PCOD_PERIODO", OracleType.Char, 6)
            arParms(1).Value = pcodperiod
            arParms(2) = New OracleParameter("PCOMPANIASOCIO", OracleType.VarChar, 10)
            arParms(2).Value = pcodcompania
            arParms(3) = New OracleParameter("PTIPODOC", OracleType.VarChar, 2)
            arParms(3).Value = ptipodoc
            arParms(4) = New OracleParameter("PNUMDOC", OracleType.Char, 14)
            arParms(4).Value = pnumdoc
            arParms(5) = New OracleParameter("PPRECIO", OracleType.Number, 20)
            arParms(5).Value = pprecio
            arParms(6) = New OracleParameter("PNUMCUOTAS", OracleType.VarChar, 10)
            arParms(6).Value = pnumcuotas
            arParms(7) = New OracleParameter("PUSUARIO", OracleType.VarChar, 10)
            arParms(7).Value = pusuario
            arParms(8) = New OracleParameter("WNUM", OracleType.Number, 6)
            arParms(8).Direction = ParameterDirection.Output

            Try
                oCx.Open()
                valor = OraHelper.ExecuteNonQuery(oCx, CommandType.StoredProcedure, strSQL, arParms)
                pnum = Convert.ToString(arParms(8).Value)

                If valor = 1 Then
                    InsertaLog = True
                Else
                    InsertaLog = False
                End If
            Catch ex As Exception
                pcoderr = "31"
                pdeserr = ex.Message
                InsertaLog = False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function VerificaFechas(ByVal conexion As String, ByVal pcod_linea_negocio As String, _
                                  ByVal pcod_modal_est As String, _
                                  ByVal pcod_tramite As Integer, _
                                  ByVal pcod_local As String, _
                                  ByRef presultado As String, _
                                    ByRef pcoderror As String, ByRef descerror As String) As Integer

            Dim cmdAdd As OracleCommand
            Dim pAdd As OracleParameter
            Dim oCx As OracleConnection = New OracleConnection(conexion)

            pcoderror = "0"

            cmdAdd = New OracleCommand("SF_VALIDA_FECHAS", oCx)
            With (cmdAdd)
                .CommandType = CommandType.StoredProcedure
                pAdd = .Parameters.Add("Return_Value", OracleType.Number, 4)
                pAdd.Direction = ParameterDirection.ReturnValue

                pAdd = .Parameters.Add("PCOD_LINEA_NEGOCIO", OracleType.Char, 1)
                pAdd.Value = pcod_linea_negocio

                pAdd = .Parameters.Add("PCOD_MODAL_EST", OracleType.Char, 2)
                pAdd.Value = pcod_modal_est

                pAdd = .Parameters.Add("PCOD_TRAMITE", OracleType.Double, 15)
                pAdd.Value = pcod_tramite

                pAdd = .Parameters.Add("PCOD_LOCAL", OracleType.Char, 1)
                pAdd.Value = pcod_local

                pAdd = .Parameters.Add("PRESULTADO", OracleType.VarChar, 500)
                pAdd.Direction = ParameterDirection.Output
            End With

            Try
                oCx.Open()
                cmdAdd.ExecuteNonQuery()
                presultado = Convert.ToString(cmdAdd.Parameters.Item("PRESULTADO").Value)
                descerror = Convert.ToString(cmdAdd.Parameters.Item("PRESULTADO").Value)
                Return Convert.ToInt32(cmdAdd.Parameters.Item("Return_Value").Value)
            Catch a As System.Exception
                pcoderror = "34"
                descerror = "Error en VerificaFechas. " & Space(1) & a.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try

        End Function

        Public Function InsertaSolicitud(ByVal conexion As String, _
                                        ByRef pcodunico As String, _
                                        ByRef pfecha As String, _
                                        ByVal plinea As String, _
                                        ByVal pmodal As String, _
                                        ByVal palumno As String, _
                                        ByVal pciclo As String, _
                                        ByVal psustento As String, _
                                        ByVal pusuario As String, _
                                        ByVal pmotivo As String, _
                                        ByVal ppaga As String, _
                                        ByVal pmatid As String, _
                                        ByVal pproducto As String, _
                                        ByVal pcoddocumento As String, _
                                        ByRef presultado As String, _
                                        ByRef psql As String) As Boolean
            Dim strSQL As String
            strSQL = "TRA$PK_DOCIDE_ASP.SP_INSERT_SOLICITUD23_ASP"
            InsertaSolicitud = True

            Dim valor As Integer
            Dim oCx As New OracleConnection(conexion)
            Dim arParms(14) As OracleParameter

            arParms(0) = New OracleParameter("PCOD_UNICO", OracleType.Number, 6)
            arParms(0).Direction = ParameterDirection.Output
            arParms(1) = New OracleParameter("PCOD_FECHA", OracleType.VarChar, 12)
            arParms(1).Direction = ParameterDirection.Output
            arParms(2) = New OracleParameter("PCOD_LINEA", OracleType.Char, 1)
            arParms(2).Value = plinea
            arParms(3) = New OracleParameter("PCOD_MODAL_EST", OracleType.Char, 2)
            arParms(3).Value = pmodal
            arParms(4) = New OracleParameter("PCOD_ALUMNO", OracleType.VarChar, 10)
            arParms(4).Value = palumno
            arParms(5) = New OracleParameter("PCOD_PERIODO", OracleType.VarChar, 6)
            arParms(5).Value = pciclo
            arParms(6) = New OracleParameter("PSUSTENTO", OracleType.VarChar, 500)
            arParms(6).Value = psustento
            arParms(7) = New OracleParameter("PUSUARIO_CREADOR", OracleType.VarChar, 10)
            arParms(7).Value = pusuario
            arParms(8) = New OracleParameter("PCOD_MOTIVO", OracleType.Number, 15)
            arParms(8).Value = pmotivo
            arParms(9) = New OracleParameter("PPAGA", OracleType.VarChar, 2)
            arParms(9).Value = ppaga
            arParms(10) = New OracleParameter("PID_MATRICULA", OracleType.Number, 15)
            arParms(10).Value = pmatid
            arParms(11) = New OracleParameter("PCOD_PRODUCTO", OracleType.VarChar, 8)
            arParms(11).Value = pproducto
            arParms(12) = New OracleParameter("PCOD_DOCUMENTO", OracleType.VarChar, 3)
            arParms(12).Value = pcoddocumento
            arParms(13) = New OracleParameter("PRESULTADO", OracleType.VarChar, 6)
            arParms(13).Direction = ParameterDirection.Output
            arParms(14) = New OracleParameter("PSQL", OracleType.VarChar, 200)
            arParms(14).Direction = ParameterDirection.Output

            Try
                oCx.Open()
                valor = OraHelper.ExecuteNonQuery(oCx, CommandType.StoredProcedure, strSQL, arParms)
                pcodunico = Convert.ToString(arParms(0).Value)
                pfecha = Convert.ToString(arParms(1).Value)
                presultado = Convert.ToString(arParms(13).Value)
                psql = Convert.ToString(arParms(14).Value)
            Catch ex As Exception
                presultado = "99"
                psql = ex.Message
                InsertaSolicitud = False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function SF_AUTORIZACION(ByVal conexion As String, ByVal archivo As String, _
                                    ByVal usuario As String, ByVal funcion As String, ByVal ip As String, ByRef permiso As String, _
                                    ByRef pcoderror As String, ByRef pdeserror As String) As Boolean

            Dim strsql As String, dr As DataRow
            Dim rstrol As New DataSet
            SF_AUTORIZACION = False

            ' VALIDACION DE AUTORIZACION DE LA PAGINA
            If ip = "" Then
                strsql = " SELECT " & _
                         " SF_AUTORIZACION(:ARCHIVO,:USUARIO,:FUNCION,null) permiso "
            Else
                strsql = " SELECT " & _
                         " SF_AUTORIZACION(:ARCHIVO,:USUARIO,:FUNCION,:IP) permiso "
            End If
            strsql = strsql & " FROM DUAL "

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strsql, oCx)
            Dim param1 As New OracleParameter("ARCHIVO", OracleType.VarChar)
            Dim param2 As New OracleParameter("USUARIO", OracleType.VarChar)
            Dim param3 As New OracleParameter("FUNCION", OracleType.Number)
            param1.Value = archivo
            param2.Value = Trim(UCase(usuario))
            param3.Value = funcion
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)
            CMD.Parameters.Add(param3)

            If ip <> "" Then
                Dim param4 As New OracleParameter("IP", OracleType.VarChar)
                param4.Value = ip
                CMD.Parameters.Add(param4)
            End If

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(rstrol)

                For Each dr In rstrol.Tables(0).Rows
                    permiso = IIf(IsDBNull(dr("permiso")), "", dr("permiso"))
                Next
                SF_AUTORIZACION = True
            Catch ex As Exception
                pcoderror = "2"
                pdeserror = ex.Message
                SF_AUTORIZACION = False
            Finally
                rstrol = Nothing
                oCx.Close()
                oCx.Dispose()
            End Try

        End Function

        Public Function ActualizaSolicitud(ByVal conexion As String, _
                                        ByVal plinea As String, _
                                        ByVal palumno As String, _
                                        ByVal pcodunico As String, _
                                        ByVal pcodciclo As String, _
                                        ByVal pflag As String, _
                                        ByVal pobservacion As String, _
                                        ByVal pusuario As String, _
                                        ByRef pcodsql As String, _
                                        ByRef presultado As String) As Boolean
            Dim strSQL As String
            strSQL = "TRA$PK_DOCIDE_ASP.SP_ACTUALIZA_DOCIDE_ASP"

            Dim valor As Integer
            Dim oCx As New OracleConnection(conexion)
            Dim arParms(8) As OracleParameter

            arParms(0) = New OracleParameter("PCOD_LINEA", OracleType.Char, 1)
            arParms(0).Value = plinea
            arParms(1) = New OracleParameter("PCOD_ALUMNO", OracleType.Char, 10)
            arParms(1).Value = palumno
            arParms(2) = New OracleParameter("PCOD_UNICO", OracleType.Number, 15)
            arParms(2).Value = pcodunico
            arParms(3) = New OracleParameter("PCOD_PERIODO", OracleType.VarChar, 6)
            arParms(3).Value = pcodciclo
            arParms(4) = New OracleParameter("PFLAG", OracleType.Char, 1)
            arParms(4).Value = pflag
            arParms(5) = New OracleParameter("POBSERVACION", OracleType.VarChar, 500)
            arParms(5).Value = pobservacion
            arParms(6) = New OracleParameter("PUSUARIO", OracleType.VarChar, 10)
            arParms(6).Value = pusuario
            arParms(7) = New OracleParameter("PCODSQL", OracleType.VarChar, 200)
            arParms(7).Direction = ParameterDirection.Output
            arParms(8) = New OracleParameter("PRESULTADO", OracleType.VarChar, 6)
            arParms(8).Direction = ParameterDirection.Output

            Try
                oCx.Open()
                valor = OraHelper.ExecuteNonQuery(oCx, CommandType.StoredProcedure, strSQL, arParms)
                pcodsql = Convert.ToString(arParms(7).Value)
                presultado = Convert.ToString(arParms(8).Value)

                If valor = 1 Then
                    ActualizaSolicitud = True
                Else
                    ActualizaSolicitud = False
                End If
            Catch ex As Exception
                presultado = "7"
                pcodsql = ex.Message
                ActualizaSolicitud = False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function ListaModalidad(ByVal conexion As String, _
                                    ByVal plinea As String, _
                                    ByRef obj As Object, _
                                    ByRef perror As String) As Boolean

            '********* Obtiene la lista de modalidades de estudio
            Dim strSQL As String, dataset As New DataSet

            strSQL = "select '00' cod_modal_est, '-- Seleccione una modalidad --' nombre from dual " & _
            " union " & _
            "select cod_modal_est, cod_modal_est ||' - '|| nombre  nombre " & _
            " from modalidad_estud " & _
            " where cod_linea_negocio = :COD_LINEA_NEGOCIO  " & _
            "  order by nombre"

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            param1.Value = plinea
            CMD.Parameters.Add(param1)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(dataset)

                obj.DataSource = dataset.Tables(0).DefaultView
                obj.DataValueField = "cod_modal_est"
                obj.DataTextField = "nombre"
                obj.DataBind()

                ListaModalidad = True
            Catch ex As Exception
                ListaModalidad = False
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function ListaIndicador(ByVal conexion As String, _
                                            ByRef obj As Object, _
                                            ByRef perror As String) As Boolean

            '********* Obtiene la lista indicadores
            Dim strSQL As String, dataset As New DataSet

            strSQL = "select 0 COD_INDICADOR, '-- Seleccione un indicador --' DESC_INDICADOR from dual " & _
            " union " & _
            " select COD_INDICADOR, substr(DESC_INDICADOR,1,1) || lower(substr(DESC_INDICADOR,2)) DESC_INDICADOR " & _
            " from INDICADORES_TRAMITE " & _
            "  order by DESC_INDICADOR"

            Dim oCx As New OracleConnection(conexion)
            Try
                oCx.Open()
                dataset = OraHelper.ExecuteDataset(oCx, CommandType.Text, strSQL)

                obj.DataSource = dataset.Tables(0).DefaultView
                obj.DataValueField = "COD_INDICADOR"
                obj.DataTextField = "DESC_INDICADOR"
                obj.DataBind()

                ListaIndicador = True
            Catch ex As Exception
                ListaIndicador = False
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function ListaTramites(ByVal conexion As String, _
                                        ByVal plinea As String, _
                                        ByVal pmodal As String, _
                                        ByVal pindicador As String, _
                                        ByRef ds As DataSet, _
                                        ByRef perror As String) As Boolean

            '********* Obtiene la lista de modalidades de estudio
            Dim strSQL As String, dataset As New DataSet

            strSQL = "select distinct c.cod_tramite, substr(t.DESCRIPCION,1,1) || lower(substr(t.DESCRIPCION,2)) DESCRIPCION " & _
                    " from configuracion_tramite c, tramites t " & _
                    " where c.cod_linea_negocio = :COD_LINEA_NEGOCIO " & _
                    " and c.cod_modal_est = :COD_MODAL_EST " & _
                    " and c.cod_indicador = :COD_INDICADOR " & _
                    " and c.cod_linea_negocio = t.cod_linea_negocio " & _
                    " and c.cod_modal_est = t.cod_modal_est " & _
                    " and c.cod_periodo = t.cod_periodo " & _
                    " and c.cod_tramite = t.cod_tramite"

            Dim oCx As New OracleConnection(conexion)

            Dim CMD As New OracleCommand(strSQL, oCx)
            Dim param1 As New OracleParameter("COD_LINEA_NEGOCIO", OracleType.VarChar)
            Dim param2 As New OracleParameter("COD_MODAL_EST", OracleType.VarChar)
            Dim param3 As New OracleParameter("COD_INDICADOR", OracleType.Number)
            param1.Value = plinea
            param2.Value = pmodal
            param3.Value = pindicador
            CMD.Parameters.Add(param1)
            CMD.Parameters.Add(param2)
            CMD.Parameters.Add(param3)

            Dim dap As New OracleDataAdapter(CMD)

            Try
                oCx.Open()
                dap.Fill(dataset)
                ds = dataset
                ListaTramites = True
            Catch ex As Exception
                ListaTramites = False
                perror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function

        Public Function TipoIndicador(ByVal conexion As String, _
                                ByVal PIndicador As String) As String

            Dim CADENA As String = "SELECT to_char(COD_TIPO) " & _
                                   "FROM INDICADORES_TRAMITE " & _
                                   "WHERE COD_INDICADOR = :COD_INDICADOR "

            Dim oraConn As OracleConnection = New OracleConnection(conexion)

            Dim CMD As New OracleCommand(CADENA, oraConn)
            Dim param1 As New OracleParameter("COD_INDICADOR", OracleType.Number)
            param1.Value = PIndicador
            CMD.Parameters.Add(param1)

            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                myReader.Close()
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try
        End Function

        Public Function ID_SEGUMIENTO(ByVal conexion As String) As String

            Dim CADENA As String = "SELECT to_char(seq_id_seguimiento.nextval) from dual "

            Dim CMD As New OracleCommand
            Dim oraConn As OracleConnection = New OracleConnection(conexion)
            CMD = New OracleCommand(CADENA, oraConn)
            Try
                oraConn.Open()
                Dim myReader As OracleDataReader = CMD.ExecuteReader()
                If myReader.Read() = True Then
                    Return myReader.GetString(0)
                End If
                myReader.Close()
            Finally
                oraConn.Close()
                oraConn.Dispose()
            End Try
        End Function

        Public Function PROCESA_DATOS(ByVal id_indi As String, _
                                        ByVal conexion As String, _
                                        ByVal plinea As String, _
                                        ByVal pmodal As String, _
                                        ByVal ptramite As String, _
                                        ByVal pindicador As String, _
                                        ByVal pId As String, _
                                        ByVal pFecIni As String, _
                                        ByVal pFecFin As String, _
                                        ByVal pTipo As String, _
                                        ByRef PCODERR As String, _
                                        ByRef PDESERR As String) As Boolean
            Dim strSQL As String
            If id_indi = "1" Then
                strSQL = "TRA$PK_SEGUIMIENTO.SP_PROCESA_DATOS1"
            ElseIf id_indi = "2" Then
                strSQL = "TRA$PK_SEGUIMIENTO.SP_PROCESA_DATOS2"
            ElseIf id_indi = "3" Then
                strSQL = "TRA$PK_SEGUIMIENTO.SP_PROCESA_DATOS3"
            ElseIf id_indi = "4" Then
                strSQL = "TRA$PK_SEGUIMIENTO.SP_PROCESA_DATOS4"
            Else
                PCODERR = "1"
                PDESERR = "Tipo de indicador no válido."
                Return False
            End If

            Dim valor As Integer
            Dim oCx As New OracleConnection(conexion)
            Dim arParms(9) As OracleParameter

            arParms(0) = New OracleParameter("PLINEA", OracleType.Char, 1)
            arParms(0).Value = plinea
            arParms(1) = New OracleParameter("PMODAL", OracleType.Char, 2)
            arParms(1).Value = pmodal
            arParms(2) = New OracleParameter("PTRAMITE", OracleType.Number, 15)
            arParms(2).Value = ptramite
            arParms(3) = New OracleParameter("PINDICADOR", OracleType.Number, 5)
            arParms(3).Value = pindicador
            arParms(4) = New OracleParameter("PID", OracleType.Number, 15)
            arParms(4).Value = pId
            arParms(5) = New OracleParameter("PFECINI", OracleType.VarChar, 12)
            arParms(5).Value = pFecIni
            arParms(6) = New OracleParameter("PFECFIN", OracleType.VarChar, 12)
            arParms(6).Value = pFecFin
            arParms(7) = New OracleParameter("PTIPO", OracleType.Number, 1)
            arParms(7).Value = pTipo
            arParms(8) = New OracleParameter("PCODERR", OracleType.Number, 5)
            arParms(8).Direction = ParameterDirection.Output
            arParms(9) = New OracleParameter("PDESERR", OracleType.VarChar, 200)
            arParms(9).Direction = ParameterDirection.Output

            Try
                oCx.Open()
                valor = OraHelper.ExecuteNonQuery(oCx, CommandType.StoredProcedure, strSQL, arParms)
                PCODERR = Convert.ToString(arParms(8).Value)
                PDESERR = Convert.ToString(arParms(9).Value)

                If valor = 1 Then
                    PROCESA_DATOS = True
                Else
                    PROCESA_DATOS = False
                End If
            Catch ex As Exception
                PCODERR = "1"
                PDESERR = ex.Message
                PROCESA_DATOS = False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
        End Function
        
          '---------------------- SEP-2007-566 -------------------
        Public Function Valida_ListaDoc(ByVal conexion As String, ByVal plinea As String, _
                                        ByVal ptramite As String, _
                                        ByVal pmotivo As String, _
                                        ByVal pdocumen As String, _
                                        ByRef pcoderror As String, _
                                        ByRef pdeserror As String) As Boolean


            Dim valor As Integer
            Dim strSQL As String
            Dim oCx As New OracleConnection(conexion)
            Dim arParms(9) As OracleParameter
            strSQL = "SP_TRAM23_VAL_FECHAS"

            arParms(0) = New OracleParameter("PCOD_TRAMITE", OracleType.Number, 5)
            arParms(0).Value = CInt(ptramite)
            arParms(1) = New OracleParameter("PCOD_LINEA_NEGOCIO", OracleType.Char, 1)
            arParms(1).Value = CStr(plinea)
            arParms(2) = New OracleParameter("PCOD_MOTIVO", OracleType.Number, 15)
            arParms(2).Value = CInt(pmotivo)
            arParms(3) = New OracleParameter("PERROR", OracleType.VarChar, 500)
            arParms(3).Direction = ParameterDirection.Output
            arParms(4) = New OracleParameter("PMENSAJE", OracleType.VarChar, 500)
            arParms(4).Direction = ParameterDirection.Output
            Try
                oCx.Open()
                valor = OraHelper.ExecuteNonQuery(oCx, CommandType.StoredProcedure, strSQL, arParms)
                pcoderror = Convert.ToString(arParms(3).Value)
                pdeserror = Convert.ToString(arParms(4).Value)

                If pcoderror <> "-1" Then
                    Valida_ListaDoc = True
                End If

            Catch ex As Exception
                Valida_ListaDoc = False
                pdeserror = ex.Message
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try

        End Function




        Public Function Valida_ListaDocSTRING(ByVal conexion As String, ByVal plinea As String, _
                                      ByVal ptramite As String, _
                                      ByVal pmotivo As String, _
                                      ByVal pdocumen As String, _
                                      ByRef pcoderror As String, _
                                      ByRef pdeserror As String) As Boolean



            pdeserror = " SELECT D.FECHA_INICIO, D.FECHA_FIN FROM " & _
                     " DOCUMENTO_TRAMITE D, MOTIVO M " & _
                     " WHERE " & _
                     " D.COD_DOCUMENTO = M.COD_DOCUMENTO " & _
                     " AND M.COD_TRAMITE ='" & ptramite & "'" & _
                     " AND M.COD_LINEA_NEGOCIO = '" & plinea & "' " & _
                     " AND D.COD_DOCUMENTO = '" & pdocumen & "'"

            Valida_ListaDocSTRING = True
           
        End Function

        'CSC-00262755-00: Adaptar los tramites para alumnos de OI
        Public Function Es_AlumnOI(ByVal conexion As String, ByVal pcod_linea_negocio As String, ByVal pcod_alumno As String, _
                                   ByRef pcoderror As String, _
                                   ByRef pdeserror As String) As String


            Dim cmdAdd As OracleCommand
            Dim pAdd As OracleParameter
            Dim oCx As OracleConnection = New OracleConnection(conexion)

            pcoderror = "0"

            cmdAdd = New OracleCommand("SF_ES_ALUMNO_OI", oCx)
            With (cmdAdd)
                .CommandType = CommandType.StoredProcedure
                pAdd = .Parameters.Add("Return_Value", OracleType.VarChar, 2)
                pAdd.Direction = ParameterDirection.ReturnValue

                pAdd = .Parameters.Add("PCOD_LINEA_NEGOCIO", OracleType.Char, 1)
                pAdd.Value = pcod_linea_negocio

                pAdd = .Parameters.Add("PCOD_ALUMNO", OracleType.Char, 9)
                pAdd.Value = pcod_alumno

            End With

            Try
                oCx.Open()
                cmdAdd.ExecuteNonQuery()
                Return cmdAdd.Parameters.Item("Return_Value").Value
            Catch a As System.Exception
                pcoderror = "40"
                pdeserror = "Error en Validar alumno OI. " & Space(1) & a.Message
                Return "NO"
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try
            Return "NO"

        End Function
        'CSC-00262755-00: (05/01/2016) Adaptar los tramites para alumnos de OI
        Public Function Es_CreaPersonaOI_Spr(ByVal conexion As String, ByVal pcod_linea_negocio As String, ByVal pcod_alumno As String, _
        ByVal pcod_usuario As String) As Boolean

            Dim pcoderror As String
            Dim pdeserror As String

            Dim strSQL As String
            strSQL = "OBLPQ_PAGOS_ALUMNOI.sp_crea_alumnOI_spr"

            Dim valor As Integer
            Dim oCx As New OracleConnection(conexion)
            Dim arParms(2) As OracleParameter

            arParms(0) = New OracleParameter("PCOD_LINEA_NEGOCIO", OracleType.Char, 1)
            arParms(0).Value = pcod_linea_negocio
            arParms(1) = New OracleParameter("PCOD_ALUMNO", OracleType.Char, 9)
            arParms(1).Value = pcod_alumno
            arParms(2) = New OracleParameter("PCOD_USUARIO", OracleType.Char, 10)
            arParms(2).Value = pcod_usuario

            Try
                oCx.Open()
                valor = OraHelper.ExecuteNonQuery(oCx, CommandType.StoredProcedure, strSQL, arParms)
                If valor = 1 Then
                    Es_CreaPersonaOI_Spr = True
                Else
                    Es_CreaPersonaOI_Spr = False
                End If
            Catch ex As Exception
                pcoderror = "99"
                pdeserror = ex.Message
                Es_CreaPersonaOI_Spr = False
            Finally
                oCx.Close()
                oCx.Dispose()
            End Try

        End Function

    End Class
End Namespace

