'**************************************************************************
'@author                        :  Jagriti
'@version                       :  1.0
'Development Environment        :  MS Visual Studio.NET
'Name of the File               :  ConnectionManager.vb
'Creation/Modification History  :
'                                 23-Sep-2003     Created
'Overview:
'This class manages the database connection used by the sample. 
'It defines methods to create and close the database connection.
'*************************************************************************

Imports System
Imports System.Windows.Forms
Imports Oracle.DataAccess.Client

Public Class ConnectionMgr
    Public Shared conn As OracleConnection

    '*****************************************************************************
    ' The purpose of this method is to establish connection to an Oracle database.
    ' The connection parameters are passed to this method from the EmpForm.
    '*****************************************************************************
    Public Shared Function getDBConnection(ByVal username As String, ByVal password As String, ByVal connectString As String) As String
        Try

            ' Connection Information	
            Dim connectionString As String = "User Id=" + username & _
                                             ";Password=" + password & _
                                             ";Data Source=" + connectString

            'Create Connection object using the above connect string
            conn = New OracleConnection(connectionString)

            ' Open database connection
            conn.Open()
            Return "Connected"

            ' Catch exceptions while connecting to the database 
        Catch ex As Exception
            MessageBox.Show("Error occurred: " + ex.Message)
            Return "Not Connected"
        End Try
    End Function

    '*******************************************************************
    ' The purpose of this method is to close the database connection 
    '********************************************************************
    Public Shared Sub closeConnection()
        Try
            If ConnectionMgr.conn Is Nothing Then
                conn.Close()
                conn.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("Error Occured: " + ex.Message)
        End Try

    End Sub

End Class

