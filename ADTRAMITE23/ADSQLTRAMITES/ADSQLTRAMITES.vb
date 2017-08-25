Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ADSQLTRAMITES
    Private connSQLServer As New SqlClient.SqlConnection

    Public Sub AbreConn(ByVal conn As String)
        connSQLServer.ConnectionString = conn
        connSQLServer.Open()
    End Sub

    Public Function VendedorSpring(ByVal conn As String, ByVal pempleado As String, _
                            ByRef pnum As Integer, ByRef pcoderr As String, ByRef pdeserr As String) As Boolean
        Dim ds As New DataSet
        Dim da As New SqlClient.SqlDataAdapter
        Dim cadena As String

        Try

            cadena = ""
            cadena = "select * from EMPLEADOMAST  where empleado = @EMPLEADO "
            AbreConn(conn)

            Dim cmd1 As New SqlClient.SqlCommand(cadena, connSQLServer)

            Dim param1 As New SqlParameter("@EMPLEADO", SqlDbType.VarChar)
            param1.Value = pempleado
            cmd1.Parameters.Add(param1)

            da = New SqlClient.SqlDataAdapter(cmd1)

            'Crea y llena DataSet.
            ds = New DataSet
            da.Fill(ds, "TC")
            pnum = ds.Tables(0).Rows.Count()

            Return True
        Catch e As Exception
            pcoderr = "28"
            pdeserr = e.Message
            connSQLServer.Close()
            Return False
        Finally
            connSQLServer.Close()
            da.Dispose()
        End Try

    End Function

    Public Function CargaBoleta(ByVal conn As String, _
                            ByVal pservicio As String, _
                            ByVal pcompania As String, _
                            ByVal psucursal As String, _
                            ByVal pcodlinea As String, _
                            ByVal pcodalumno As String, _
                            ByVal pcodmodal As String, _
                            ByVal pcodperiodo As String, _
                            ByVal ppreciounit As String, _
                            ByVal pformapago As String, _
                            ByVal pdias As String, _
                            ByVal pvendedor As String, _
                            ByVal pultmusuario As String, _
                            ByVal ppreciototal As String, _
                            ByVal coutasfac As String, _
                            ByVal pcantidad As String, _
                            ByRef perror As String) As DataSet
        Dim ds As New DataSet
        Dim da As New SqlClient.SqlDataAdapter
        Dim cadena As String
        Try
            cadena = "usp_ins_cargaboleta "
            cadena = cadena & "'" & pservicio & "','" & pcompania & "','" & psucursal & "','" & _
            pcodlinea & "','" & pcodalumno & "'," & pcodmodal & ",'" & pcodperiodo & "','" & ppreciounit & "','" & _
            pformapago & "','" & pdias & "','" & pvendedor & "','" & pultmusuario & "','" & ppreciototal & "','" & _
            coutasfac & "','" & pcantidad & "'"

            'Crea la conexión y DataAdapter para la tabla Authors.
            AbreConn(conn)

            Dim cmd1 = New SqlDataAdapter(cadena, connSQLServer)

            'Crea y llena DataSet.
            ds = New DataSet
            cmd1.Fill(ds, "NUMORDEN")

            Return ds
        Catch e As Exception
            perror = e.Message
            connSQLServer.Close()
        Finally
            connSQLServer.Close()
            da.Dispose()
        End Try

    End Function
End Class
