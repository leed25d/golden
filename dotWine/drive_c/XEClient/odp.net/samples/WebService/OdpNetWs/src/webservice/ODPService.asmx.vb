'**************************************************************************
'@author  Abhijeet Kulkarni
'@version 1.0
'Development Environment        :  MS Visual Studio .Net
'Name of the File               :  ODPService.asmx.vb
'Creation/Modification History  :
'                                  31-Mar-2003     Created
'
'Overview:
'The purpose of this Web Service is to demonstrate how .NET Web Services 
'can serve data by using ODP.NET. This class returns a DataSet object through 
'a WebMethod. It also shows how one can query/update Oracle databases using Web 
'Services.
'**************************************************************************
Imports Oracle.DataAccess.Client
Imports System.Web.Services
Imports System.Data

<WebService(Namespace:="http://otn.oracle.com/OdpNetWs")> _
Public Class ODPService
    Inherits System.Web.Services.WebService
    Protected Shared conStr As String
    Protected Shared con As OracleConnection
    Protected Shared connected As Boolean

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region
    '**************************************************************************
    'This web method queries the ODPNET_WS_COUNTRIES table and returns a DataSet
    '**************************************************************************
    <WebMethod()> Public Function GetCountries() As DataSet
        'Create a OracleDataAdapter instance
        Dim dataAdapter As OracleDataAdapter = New OracleDataAdapter("SELECT countryname, population, language , currency FROM ODPNET_WS_COUNTRIES", con)
        'Create a DataSet instance
        Dim dset As DataSet = New DataSet()
        'Create a OracleCommandBuilder instance
        Dim ocb As OracleCommandBuilder = New OracleCommandBuilder(dataAdapter)
        dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
        'Fill the DataSet
        dataAdapter.Fill(dset, "ODPNET_WS_COUNTRIES")
        'Return the dataset
        GetCountries = dset
    End Function

    '**************************************************************************
    'This web method accepts DataSet as parameter and returns updated DataSet
    '**************************************************************************
    <WebMethod()> Public Function UpdateCountries(ByVal dset As DataSet) As DataSet
        'Create a OracleDataAdapter instance
        Dim dataAdapter As OracleDataAdapter = New OracleDataAdapter("SELECT countryname, population, language , currency FROM ODPNET_WS_COUNTRIES", con)
        'Create OracleCommandBuilder instance
        Dim ocb As OracleCommandBuilder = New OracleCommandBuilder(dataAdapter)
        dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
        'Update the database with the help of DataAdapter
        dataAdapter.Update(dset, "ODPNET_WS_COUNTRIES")
        'Retrun updated DataSet
        UpdateCountries = dset
    End Function

    '**************************************************************************
    'This web method accepts username,password and connect string and opens a
    'database connection
    '**************************************************************************
    <WebMethod()> Public Function OpenConnection(ByVal username As String, ByVal password As String, ByVal datasource As String) As Boolean
        Try
            'Create OracleConnection instance
            con = New OracleConnection()
            'Create the connect string
            conStr = New String("User Id=" + username + ";Password=" + password + ";Data Source=" + datasource + ";")
            'Set the connect String
            con.ConnectionString = conStr
            'Try to open the connection
            con.Open()
            'Set connected to true
            connected = True
        Catch ex As Exception
            'An error ocurred. Set connected to false
            connected = False
        End Try
        'Return the connection status True or False
        OpenConnection = connected
    End Function

    '**************************************************************************
    'This method returns True if connections is obtained False otherwise
    '**************************************************************************
    <WebMethod()> Public Function isConnected() As Boolean
        'Return status
        isConnected = connected
    End Function

    '**************************************************************************
    'This method trys to close the database connection
    '**************************************************************************
    <WebMethod()> Public Function closeConnection() As Boolean
        Try
            con.Dispose()
            con.Close()
        Catch ex As Exception
        Finally
            'Set connected to false
            connected = False
        End Try
        closeConnection = connected
    End Function

    '************************************************************************
    'This method creates table required by this application and 
    'inserts some data in the table.
    '************************************************************************
    <WebMethod()> Public Function CreateTable() As String
        Dim msg As String = "Created table and inserted 5 rows"
        Try
            'Create an OracleCommand object using the connection object
            Dim cmd As OracleCommand = New OracleCommand("DROP TABLE ODPNET_WS_COUNTRIES", con)
            cmd.CommandType = CommandType.Text
            Try
                'Drop the table if it exists
                cmd.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            'Create table ODPNET_WS_COUNTRIES
            cmd = New OracleCommand("CREATE TABLE ODPNET_WS_COUNTRIES(countryname VARCHAR2(30) PRIMARY KEY, population  NUMBER(12),language VARCHAR2(30),currency VARCHAR2(30))", con)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            'Insert rows in the table
            cmd = New OracleCommand("INSERT INTO ODPNET_WS_COUNTRIES VALUES('United States',280000000 ,'English North American','US Dollar $')", con)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cmd = New OracleCommand("INSERT INTO ODPNET_WS_COUNTRIES VALUES('United Kingdom',59000000 ,'English British','Pound Sterling')", con)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cmd = New OracleCommand("INSERT INTO ODPNET_WS_COUNTRIES VALUES('Germany',82000000 ,'German','Euro')", con)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cmd = New OracleCommand("INSERT INTO ODPNET_WS_COUNTRIES VALUES('France',59000000 ,'French','Euro')", con)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cmd = New OracleCommand("INSERT INTO ODPNET_WS_COUNTRIES VALUES('Japan',126000000 ,'Japnese','Yen')", con)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            cmd = New OracleCommand("INSERT INTO ODPNET_WS_COUNTRIES VALUES('Ireland',39000000 ,'Irish','Euro')", con)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch ex As Exception
            'An error ocurred. Set connected to false
            msg = ex.ToString()
            connected = False
        End Try
        CreateTable = msg
    End Function
End Class
