'**************************************************************************
'@author  Abhijeet Kulkarni
'@version 1.0
'Development Environment        :  MS Visual Studio .Net
'Name of the File               :  Main.aspx.vb
'Creation/Modification History  :
'                                  09-Apr-2003     Created

'Overview: This is the main page of the application. This page prompts 
'user to enter Username,password and connect string to connect to 
'Oracle database. Once this string is obtained it is passed to the Web
'Service to open a database connection. If the connection is obtained then
'the link to GridPage.aspx is enabled. 
'**************************************************************************
Imports client.webservice
Public Class MainForm
    Inherits System.Web.UI.Page
    Protected WithEvents Table1 As System.Web.UI.WebControls.Table
    Protected WithEvents Step1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Username As System.Web.UI.WebControls.TextBox
    Protected WithEvents Password As System.Web.UI.WebControls.TextBox
    Protected WithEvents Datasource As System.Web.UI.WebControls.TextBox
    Protected WithEvents LabStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ConnectButton As System.Web.UI.WebControls.Button
    Protected WithEvents StatusText As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents GoToAppButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Description As System.Web.UI.WebControls.Label
    Protected Shared wsclient As ODPService
    Protected WithEvents DisConnectButton As System.Web.UI.WebControls.Button
    Protected WithEvents ExitButton As System.Web.UI.WebControls.Button
    Protected Shared connected As Boolean = False

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '************************************************************************
    'This method initializes the page
    '************************************************************************
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        'Create the Web Service proxy instance
        wsclient = New ODPService()
    End Sub

#End Region
    '************************************************************************
    'This method is called every time the page is loaded
    '************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Check if the database connection is alive
            connected = wsclient.isConnected()
            'If database connection is obtained disable/enable certain user controls
            If connected = True Then
                Username.Enabled = False
                Password.Enabled = False
                Datasource.Enabled = False
                ConnectButton.Enabled = False
                ExitButton.Enabled = False
                Label4.Enabled = True
                GoToAppButton.Enabled = True
                DisConnectButton.Enabled = True
                StatusText.ForeColor = Color.Green
                StatusText.Text = "Database connection obtained by the Web Service"
            Else
                'If database connection is not obtained disable/enable certain user controls
                Username.Enabled = True
                Password.Enabled = True
                Datasource.Enabled = True
                ExitButton.Enabled = True
                ConnectButton.Enabled = True
                Label4.Enabled = False
                DisConnectButton.Enabled = False
                GoToAppButton.Enabled = False
            End If
        Catch ex As Exception
            'Display error message
            StatusText.Text = "Fatal Error: Unable to contact the Web Service"
        End Try
    End Sub

    '************************************************************************
    'This method is called when ConnectButton is pressed
    '************************************************************************
    Private Sub ConnectButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectButton.Click
        Try
            'Try to open a connection to the database
            wsclient.OpenConnection(Username.Text, Password.Text, Datasource.Text)
            'Check if connection is obtained or not
            connected = wsclient.isConnected()
            Dim msg As String = ""
            If connected = True Then
                'Create necessary tables and insert some data
                msg = wsclient.CreateTable()
            End If
            'Check if we are still connected after creating the tables
            connected = wsclient.isConnected()
            'Enable/disable certain controls if connected
            If connected = True Then
                Username.Enabled = False
                Password.Enabled = False
                Datasource.Enabled = False
                ConnectButton.Enabled = False
                ExitButton.Enabled = False
                Label4.Enabled = True
                GoToAppButton.Enabled = True
                DisConnectButton.Enabled = True
                StatusText.ForeColor = Color.Green
                StatusText.Text = msg
            End If
        Catch ex As Exception
            'Display error message
            StatusText.Text = "Fatal Error: Unable to contact the Web Service or Database"
        End Try
    End Sub

    '************************************************************************
    'Everything is set up redirect to the GridPage.aspx
    '************************************************************************
    Private Sub GoToAppButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoToAppButton.Click
        Response.Redirect("GridPage.aspx")
    End Sub

    Private Sub DisConnectButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisConnectButton.Click
        Try
            wsclient.closeConnection()
        Catch ex1 As Exception
        Finally
            StatusText.ForeColor = Color.Red
            StatusText.Text = "Not Connected"
            Username.Enabled = True
            Password.Enabled = True
            Datasource.Enabled = True
            ConnectButton.Enabled = True
            ExitButton.Enabled = True
            Label4.Enabled = False
            DisConnectButton.Enabled = False
            GoToAppButton.Enabled = False
        End Try
    End Sub

    '************************************************************************
    'This method closes the browser window and disposes the ASP page.
    '************************************************************************
    Private Sub ExitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitButton.Click
        Response.Write("<SCRIPT>window.close()</SCRIPT>")
        Me.Dispose()
    End Sub
End Class
