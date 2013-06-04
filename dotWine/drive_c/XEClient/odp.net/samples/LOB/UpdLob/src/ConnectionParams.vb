'**************************************************************************
'@author                        :  Jagriti
'@version                       :  1.0
'Development Environment        :  MS Visual Studio.NET
'Name of the File               :  ConnectionParams.vb
'Creation/Modification History  :
'                                  17-Feb-2003     Created

'Overview:
'This file defines the variables for connection parameters for database.
'It also defines variables for setting connection pooling for the 
'database connection. Change the values according to your system 
'configuration.
'**************************************************************************

Public Class ConnectionParams

    Public Shared datasource As String
    Public Shared username As String
    Public Shared password As String
    Public Shared minpoolsize As String
    Public Shared incrpoolsize As String
    Public Shared decrpoolsize As String

    Public Shared Sub setparams()
        'Parameters for database connection
        'Change the values to those applicable to your database

        'Replace with Connect String as in TNSNames
        datasource = "orcl9i"

        'Username
        username = "Scott"

        'Password
        password = "tiger"

        'min pool size
        minpoolsize = 20

        'incr pool size
        incrpoolsize = 5

        'decr pool size
        decrpoolsize = 2
    End Sub

End Class
