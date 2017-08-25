Imports DataProtector
Imports System.Text
Imports System.Configuration


Namespace tramites

Public Module Variables
    Dim dp As New DataProtector.DataProtector(DataProtector.DataProtector.Store.USE_MACHINE_STORE)

        'Dim appSettingValue As String = ConfigurationSettings.AppSettings("connectionString")
        'Dim dataToDecrypt As Byte() = Convert.FromBase64String(appSettingValue)
        'Public conexion As String = Encoding.ASCII.GetString(dp.Decrypt(dataToDecrypt, Nothing))

        Public conexion As String = "Server=svrdesoe;Data source=deso.world;User id=master;password=ocse7tra"
        ''Public conexionD As String = ""
        ''Public conexionUPC_cert As String = ""
        ''Public conexionPortal As String = ""

        Public conexionD As String = "Server=svrdesoe;Data source=deso.world;User id=master;password=ocse7tra"
        Public conexionUPC_cert As String = "Server=svrdesoe;Data source=deso.world;User id=master;password=ocse7tra"
        Public conexionPortal As String = "Server=svrdesoe;Data source=deso.world;User id=master;password=ocse7tra"

        'Dim appSettingValueD As String = ConfigurationSettings.AppSettings("connectionD")
        ''Dim appSettingValueD As String = ConfigurationSettings.AppSettings("connectionString")
        'Dim dataToDecryptD As Byte() = Convert.FromBase64String(appSettingValueD)
        'Public conexionD As String = Encoding.ASCII.GetString(dp.Decrypt(dataToDecryptD, Nothing))



        'Dim appSettingValueUPC_cert As String = ConfigurationSettings.AppSettings("ConnectionSPRING")
        'Dim dataToDecryptUPC_cert As Byte() = Convert.FromBase64String(appSettingValueUPC_cert)
        'Public conexionUPC_cert As String = Encoding.ASCII.GetString(dp.Decrypt(dataToDecryptUPC_cert, Nothing))

        'Dim appSettingValuePortal As String = ConfigurationSettings.AppSettings("connectionPORTALUPC")
        'Dim dataToDecryptPortal As Byte() = Convert.FromBase64String(appSettingValuePortal)
        'Public conexionPortal As String = Encoding.ASCII.GetString(dp.Decrypt(dataToDecryptPortal, Nothing))

        'Public conexion As String = ConfigurationSettings.AppSettings("connectionString")
        Public cod_linea_negocio As String
    Public cod_alumno As String
    Public cod_usuario As String
    Public cod_modal_est As String
    Public WCOLOR_LINEA As Color
End Module

End Namespace
