'**************************************************************************
'@author                        :  Jagriti
'@version                       :  1.0
'Development Environment        :  MS Visual Studio.NET
'Name of the File               :  ConnectionManager.vb
'Creation/Modification History  :
'                                 18-Feb-2003     Created
'Overview:
'This class manages the database connection used by the sample. 
'It defines methods to create and close the database connection.
'*************************************************************************

Imports System
Imports Oracle.DataAccess.Client

Public Class ConnectionManager
    Public Shared conn As OracleConnection

    '*******************************************************************
    ' The purpose of this method is to get the database connection 
    ' using the connection details from ConnectionParams.vb
    ' Note: Replace the datasource parameter with your datasource value
    ' in ConnectionParams.vb file.
    '********************************************************************/
    Public Shared Function getDBConnection() As Boolean
        Try
            ' Setting database parameters
            ConnectionParams.setparams()

            ' Connection Information	
            Dim connectionString As String = "Data Source=" + ConnectionParams.datasource & _
                                             ";User ID=" + ConnectionParams.username & _
                                             ";Password=" + ConnectionParams.password & _
                                             ";Min Pool Size=" + ConnectionParams.minpoolsize & _
                                             ";Incr Pool Size=" + ConnectionParams.incrpoolsize & _
                                             ";Decr pool Size=" + ConnectionParams.decrpoolsize

            ' Create Connection object using the above connect string
            conn = New OracleConnection(connectionString)

            ' Open database connection
            conn.Open()
            Return True

            ' catch exceptions while connecting to the database 
        Catch ex As Exception
            Return False

        End Try
    End Function

    '*******************************************************************
    ' The purpose of this method is to close the database connection 
    '********************************************************************
    Public Shared Sub closeConnection()
        conn.Close()
        conn.Dispose()
    End Sub

End Class
