Imports System
Imports SYSTEM.IO
Imports System.Data
Imports System.Data.OracleClient
Imports Oracle.ApplicationBlocks.Data

Namespace ADTRAMITES

    Public Class Configuracion

        Private strCadenaConexion As String
        Private strMensajeError As String
        'Variables que son campos de la tabla Parametros_Configuration
        Private strRUTA_FOTO_ALUMNO As String
        Private strPREFIJO_FOTO As String
        Private strRUTA_LOGIN_DIRECTO As String
        Private strLOGO_UNIDAD As String
        Private strCOLOR_FONDO_SUP As String
        Private strCOLOR_FONDO_INF As String
        Private strGRAFICO As String
        Private strCOLOR As String
        Private strRUTA_IMAGEN_URL As String

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

        Public ReadOnly Property RUTA_FOTO_ALUMNO() As String
            Get
                Return strRUTA_FOTO_ALUMNO
            End Get
        End Property

        Public ReadOnly Property PREFIJO_FOTO() As String
            Get
                Return strPREFIJO_FOTO
            End Get
        End Property

        Public ReadOnly Property RUTA_LOGIN_DIRECTO() As String
            Get
                Return strRUTA_LOGIN_DIRECTO
            End Get
        End Property

        Public ReadOnly Property LOGO_UNIDAD() As String
            Get
                Return strLOGO_UNIDAD
            End Get
        End Property

        Public ReadOnly Property COLOR_FONDO_SUP() As String
            Get
                Return strCOLOR_FONDO_SUP
            End Get
        End Property

        Public ReadOnly Property COLOR_FONDO_INF() As String
            Get
                Return strCOLOR_FONDO_INF
            End Get
        End Property

        Public ReadOnly Property GRAFICO() As String
            Get
                Return strGRAFICO
            End Get
        End Property

        Public ReadOnly Property COLOR() As String
            Get
                Return strCOLOR
            End Get
        End Property

        Public ReadOnly Property RUTA_IMAGEN_URL() As String
            Get
                Return strRUTA_IMAGEN_URL
            End Get
        End Property

        Public Function Configuracion( _
            ByVal cod_alumno As String, _
            ByVal cod_linea_negocio As String, _
            ByVal sAPPL_PHYSICAL_PATH As String) As Boolean

            Dim sqlOraParam(10) As OracleParameter

            Try

                sqlOraParam(0) = New OracleParameter("INTCOD_LINEA_NEGOCIO", OracleType.Char, 1)
                sqlOraParam(0).Direction = ParameterDirection.Input
                sqlOraParam(0).Value = cod_linea_negocio

                sqlOraParam(1) = New OracleParameter("INTCOD_ALUMNO", OracleType.Char, 9)
                sqlOraParam(1).Direction = ParameterDirection.Input
                sqlOraParam(1).Value = cod_alumno

                sqlOraParam(2) = New OracleParameter("strRUTA_FOTO_ALUMNO", OracleType.VarChar, 240)
                sqlOraParam(2).Direction = ParameterDirection.Output

                sqlOraParam(3) = New OracleParameter("strPREFIJO_FOTO", OracleType.VarChar, 10)
                sqlOraParam(3).Direction = ParameterDirection.Output

                sqlOraParam(4) = New OracleParameter("strRUTA_LOGIN_DIRECTO", OracleType.VarChar, 240)
                sqlOraParam(4).Direction = ParameterDirection.Output

                sqlOraParam(5) = New OracleParameter("strLOGO_UNIDAD", OracleType.VarChar, 30)
                sqlOraParam(5).Direction = ParameterDirection.Output

                sqlOraParam(6) = New OracleParameter("strCOLOR_FONDO_SUP", OracleType.VarChar, 30)
                sqlOraParam(6).Direction = ParameterDirection.Output

                sqlOraParam(7) = New OracleParameter("strCOLOR_FONDO_INF", OracleType.VarChar, 30)
                sqlOraParam(7).Direction = ParameterDirection.Output

                sqlOraParam(8) = New OracleParameter("strGRAFICO", OracleType.VarChar, 30)
                sqlOraParam(8).Direction = ParameterDirection.Output

                sqlOraParam(9) = New OracleParameter("strCOLOR", OracleType.VarChar, 30)
                sqlOraParam(9).Direction = ParameterDirection.Output

                sqlOraParam(10) = New OracleParameter("strMensaje", OracleType.VarChar, 200)
                sqlOraParam(10).Direction = ParameterDirection.Output

                Dim p_Oper As Integer
                p_Oper = OraHelper.ExecuteNonQuery(strCadenaConexion, CommandType.StoredProcedure, "TRA$PK_DOCIDE_ASP.SP_OBTENER_DATOS_CONFIGURACION", sqlOraParam)

                If p_Oper = 1 Then

                    strRUTA_FOTO_ALUMNO = IIf(IsDBNull(sqlOraParam(2).Value), "", Convert.ToString(sqlOraParam(2).Value))
                    strPREFIJO_FOTO = IIf(IsDBNull(sqlOraParam(3).Value), "", Convert.ToString(sqlOraParam(3).Value))
                    strRUTA_LOGIN_DIRECTO = IIf(IsDBNull(sqlOraParam(4).Value), "", Convert.ToString(sqlOraParam(4).Value))
                    strLOGO_UNIDAD = IIf(IsDBNull(sqlOraParam(5).Value), "", Convert.ToString(sqlOraParam(5).Value))
                    strCOLOR_FONDO_SUP = IIf(IsDBNull(sqlOraParam(6).Value), "", Convert.ToString(sqlOraParam(6).Value))
                    strCOLOR_FONDO_INF = IIf(IsDBNull(sqlOraParam(7).Value), "", Convert.ToString(sqlOraParam(7).Value))
                    strGRAFICO = IIf(IsDBNull(sqlOraParam(8).Value), "", Convert.ToString(sqlOraParam(8).Value))
                    strCOLOR = IIf(IsDBNull(sqlOraParam(9).Value), "", Convert.ToString(sqlOraParam(9).Value))
                    strMensajeError = IIf(IsDBNull(sqlOraParam(10).Value), "", Convert.ToString(sqlOraParam(10).Value))

                    '/****************************************************************************************************************/
                    'Aqui obtenemos la ruta URL de la imagen
                    '/****************************************************************************************************************/
                    Dim sRUTA_FOTO_ALUMNO As String = strRUTA_FOTO_ALUMNO
                    Dim sPREFIJO_FOTO As String = strPREFIJO_FOTO
                    Dim strRutaFoto As String = strRUTA_LOGIN_DIRECTO & "programas/imagen/fotos/no_dispon.gif"


                    'verificar si existe carpeta
                    Dim i As Integer = InStr(UCase(sAPPL_PHYSICAL_PATH), "TRAMITES")
                    If i <= 0 Then
                        strRUTA_IMAGEN_URL = strRutaFoto
                        Return False
                    End If

                    'Esta asignacion en duro de sAPPL_PHYSICAL_PATH debe borrarse a copiarse en DESE o DESI
                    'sAPPL_PHYSICAL_PATH = "G:\\wwwroot\upcvirtual\"

                    sAPPL_PHYSICAL_PATH = Mid(sAPPL_PHYSICAL_PATH, 1, i - 1)

                    Dim strRutaPadre As String = sAPPL_PHYSICAL_PATH 'Left(sAPPL_PHYSICAL_PATH, i - 1)
                    Dim strNombreFoto As String = sPREFIJO_FOTO & UCase(cod_alumno) & ".jpg"
                    Dim strRutaCarpeta As String = sAPPL_PHYSICAL_PATH & "programas\" & Replace(sRUTA_FOTO_ALUMNO, "/", "\")

                    Dim strRutaArchivo As String = strRutaCarpeta & strNombreFoto
                    'Dim strRutaArchivo As String = strRUTA_LOGIN_DIRECTO & "programas/" & sRUTA_FOTO_ALUMNO & strNombreFoto

                    If System.IO.File.Exists(strRutaArchivo) Then
                        strRutaFoto = strRUTA_LOGIN_DIRECTO & "programas/" & sRUTA_FOTO_ALUMNO & strNombreFoto
                    End If

                    strRUTA_IMAGEN_URL = strRutaFoto
                    '/****************************************************************************************************************/
                    Return True

                End If

            Catch ex As Exception
                strMensajeError = ex.Message
                Return False

            End Try

        End Function


    End Class

End Namespace

