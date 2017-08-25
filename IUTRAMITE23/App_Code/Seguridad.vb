Imports Microsoft.VisualBasic
Imports System.Web.HttpRequest
Imports ADTRAMITES.ADTRAMITES
Imports tramites.Variables


Public Module Seguridad
    Dim encripta As New Custom3DevProviders.CommonMethods
    Dim rutaLogin = "/LoginIntranet/LoginCib.aspx"
    Dim rutaDenegaPermi = "/LoginIntranet/FrmDenegacionPermisos.aspx"
    Const getMethodKeyName As String = "var1"
    Const getMethodKeyName2 As String = "param"

    ''' <summary>
    ''' Encripta las variables enviadas por QueryString (metodo GET)
    ''' </summary>
    ''' <param name="querystring"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Seguridad_Get_Encrypt(ByVal querystring As String) As String
        Dim rtn As String = ""
        If (Trim(querystring) <> "") Then
            rtn = HttpUtility.UrlEncode(encripta.EncriptarCadena(querystring))
        Else
            rtn = ""
        End If
        rtn = String.Format("{0}={1}", getMethodKeyName, rtn)
        Return rtn
    End Function

    ''' <summary>
    ''' Desencripta el contenido de la variable comodin, devuelve todo el querystring desencriptado (solo es usado por css. junior, no lo uses!!!)
    ''' </summary>
    ''' <param name="querystring"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Seguridad_Get_Decrypt_All(ByVal querystring As NameValueCollection) As String
        Dim rtn As String = querystring(getMethodKeyName)
        'Dim rtnDsn As String = ""

        If (Trim(rtn) <> "") Then
            'rtn = HttpUtility.UrlDecode(rtn.ToString)
            'rtn = encripta.QueryString(keyName, rtn)

            Dim qs As String = encripta.DesencriptarCadena(rtn)
            rtn = qs
        Else
            rtn = ""
        End If
        Return rtn
    End Function

    ''' <summary>
    ''' Devuelve el contenido desencriptado de una variable enviada por querystring ej: Seguridad_Get_Decrypt(Request.QueryString, "WPAG1")
    ''' </summary>
    ''' <param name="querystring"></param>
    ''' <param name="keyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Seguridad_Get_Decrypt(ByVal querystring As NameValueCollection, ByVal keyName As String) As String
        Dim rtn As String = querystring(getMethodKeyName)
        'Dim rtnDsn As String = ""

        If (Trim(rtn) <> "") Then
            'rtn = HttpUtility.UrlDecode(rtn.ToString)
            'rtn = encripta.QueryString(keyName, rtn)

            Dim qs As String = encripta.DesencriptarCadena(rtn)
            Dim elements() As String = qs.Split("&")
            For Each element As String In elements
                If (element.StartsWith(keyName + "=")) Then
                    rtn = element.Substring((keyName.Length + 1))
                    Exit For
                End If
            Next
        Else
            rtn = ""
        End If
        Return rtn
    End Function
    Function Seguridad_Get_Decrypt2(ByVal querystring As NameValueCollection, ByVal keyName As String) As String
        Dim rtn As String = querystring(getMethodKeyName2)
        'Dim rtnDsn As String = ""

        If (Trim(rtn) <> "") Then
            'rtn = HttpUtility.UrlDecode(rtn.ToString)
            'rtn = encripta.QueryString(keyName, rtn)

            Dim qs As String = encripta.DesencriptarCadena(rtn)
            Dim elements() As String = qs.Split("&")
            For Each element As String In elements
                If (element.StartsWith(keyName + "=")) Then
                    rtn = element.Substring((keyName.Length + 1))
                    Exit For
                End If
            Next
        Else
            rtn = ""
        End If
        Return rtn
    End Function

    ''' <summary>
    ''' Encripta la cadena enviada.
    ''' </summary>
    ''' <param name="texto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Seguridad_All_Encrypt(ByVal texto As String) As String
        Dim rtn As String = ""
        If (Trim(texto) <> "") Then
            rtn = encripta.EncriptarCadena(texto)
        Else
            rtn = ""
        End If
        Return rtn
    End Function

    ''' <summary>
    ''' Desencripta la cadena enviada.
    ''' </summary>
    ''' <param name="texto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Seguridad_All_Decrypt(ByVal texto As String) As String
        Dim rtn As String = texto
        Dim rtnDsn As String = ""

        If (Trim(rtn) <> "") Then
            rtnDsn = encripta.DesencriptarCadena(rtn)
        Else
            rtnDsn = ""
        End If
        Return rtnDsn
    End Function

    ''' <summary>
    ''' Llena el control DropDownList con los parametros indicados
    ''' </summary>
    ''' <param name="ddl">Control DropDownList</param>
    ''' <param name="dt">Datos</param>
    ''' <param name="valueField">Campo valor</param>
    ''' <param name="textField">Campo texto</param>
    ''' <param name="encryptValue">Indica si el campo valor debe ser encriptado o no</param>
    ''' <remarks></remarks>
    Public Sub Seguridad_PopulateDDL(ByVal ddl As DropDownList, ByVal dt As DataTable, ByVal valueField As String, ByVal textField As String, ByVal encryptValue As Boolean)
        Dim dt2 As DataTable = dt.Copy()

        If (encryptValue) Then
            For Each dr As DataRow In dt2.Rows
                dr(valueField) = Seguridad_All_Encrypt(dr(valueField))
            Next
        End If

        ddl.DataSource = dt2
        ddl.DataValueField = valueField
        ddl.DataTextField = textField
        ddl.DataBind()
    End Sub

    ''' <summary>
    ''' Usada por css
    ''' </summary>
    ''' <param name="querystring"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Seguridad_GetQueryString(ByVal querystring As NameValueCollection) As String
        If (querystring.Count <= 0) Then Return ""

        Dim sb As New StringBuilder()
        Dim i As Integer
        For i = 0 To querystring.Count - 1
            sb.AppendFormat("&{0}={1}", querystring.GetKey(i), querystring(i))
        Next

        If (sb.ToString().Length > 0) Then
            Return sb.ToString().Substring(1)
        End If
        Return ""
    End Function


    Private Sub WriteLog(ByVal text As String, ByVal path As String)
        My.Computer.FileSystem.WriteAllText(path + "\Error.txt", text, True)
    End Sub
    Private Function Trim(ByVal text As String) As String
        Dim rtn As String = text
        If (IsNothing(text)) Then
            rtn = ""
        ElseIf (String.IsNullOrEmpty(text)) Then
            rtn = ""
        Else
            rtn = text.Trim()
        End If
        Return rtn
    End Function
    'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - INICIO
    Public Function sf_autoriza_reserva(ByVal archivo As String, ByVal usuario As String, ByVal funcion As Integer, ByVal ip As String) As Boolean
        'Comentado para interface de nueva intranet
        'Dim obj2 As New Custom3DevProviders.GenericDataBaseController
        'Dim var As String = obj2.ExecuteSFAuthorization(archivo, usuario, funcion, ip)
        'Return (var = 1)

        Dim obj As New clsTramites
        Dim permiso As String
        Dim perror As String
        Dim perrordes As String

        Dim var As Boolean = obj.SF_AUTORIZACION(conexion, archivo, usuario, funcion, ip, permiso, perror, perrordes)
        Return (var)
    End Function
    'MODIFICACION PROYECTO SEGURIDAD - ALEJANDRO ZARATE - FIN
End Module


