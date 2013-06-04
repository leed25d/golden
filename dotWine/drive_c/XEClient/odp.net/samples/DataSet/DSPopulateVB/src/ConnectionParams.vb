'**************************************************************************
'@author  Jagriti
'@version 1.0
'Development Environment        :  MS Visual Studio .Net
'Name of the File               :  ConnectionParams.vb
'Creation/Modification History  :
'                                  24-July-2002     Created

'Overview:
'This file defines the variables for connection parameters for database.
'**************************************************************************

Module ConnectionParams

    Public datasource As String
    Public username As String
    Public password As String

    Public Sub setparams()
        'Parameters for database connection
        'Change the values to those applicable to your database

        'Replace with Connect String as TNSNames
        datasource = "orcl9i"
        'Username
        username = "ORANET"
        'Password
        password = "ORANET"
    End Sub

End Module
