'**************************************************************************
'@author                        :  Jagriti
'@version                       :  1.0
'Development Environment        :  MS Visual Studio.NET
'Name of the File               :  ProductForm.aspx.vb
'Creation/Modification History  :
'                                  17-Feb-2003     Created

'Overview:
'When this sample is run, ProductForm.aspx is the first web form that is 
'displayed to the user. The purpose of this code behind file is to display
'a dropdown list populated with products from "Printmedia" table. User can
'select a product from this list and click "View/Update Image" button.
'This redirects the control from "productsForm.aspx" to "ImgForm.aspx".
'**************************************************************************

' Importing Standard namespace for Data access
Imports System.Data

' ODP.NET specific Namespaces referenced in this sample
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Public Class ProductForm
    Inherits System.Web.UI.Page

    ' UI controls
    Protected WithEvents productsList As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ViewAdBtn As System.Web.UI.WebControls.Button
    Protected WithEvents CloseBtn As System.Web.UI.WebControls.Button
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    '**************************************************************************
    ' The purpose of this method is:
    ' 1. Establish a connection to database using ODP.NET .
    ' 2. Populate products list, for the first time the page is loaded.
    '**************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' If database connection is established successfully
        If (ConnectionManager.getDBConnection()) Then

            ' Populate list only for the first time the page is loaded
            If Not IsPostBack() Then
                PopulateList()
            End If
        End If
    End Sub

    '*****************************************************************************
    ' This method is called from the page load event of this form. The purpose
    ' of this method is to populate productsList from "Printmedia" database table.
    '*****************************************************************************
    Private Sub PopulateList()
        Dim productsAdapter As New OracleDataAdapter()
        Dim productsDataset As New DataSet("productsDataset")

        Try
            ' Command for fetching "Printmedia" data
            productsAdapter.SelectCommand = New OracleCommand("SELECT product_id,product_name " & _
                                                              " FROM printmedia", ConnectionManager.conn)

            ' Fill dataset 
            productsAdapter.Fill(productsDataset, "Printmedia")

            ' Set the datasource for dropdown list
            productsList.DataSource = productsDataset.Tables("Printmedia").DefaultView

            ' Display "Product_Name" as text for the list items
            productsList.DataTextField = productsDataset.Tables(0).Columns("Product_Name").ToString()

            ' Use "Product_ID" as the actual value of the list items
            productsList.DataValueField = productsDataset.Tables(0).Columns("Product_ID").ToString()

            ' Bind data to list
            productsList.DataBind()

        Catch ex As Exception
            ' Error handling 
            Response.Redirect("Error.aspx?error=" + ex.Message)

        End Try
    End Sub

    '**********************************************************************************
    ' This code is executed on the click event of "View/Update Image" button.
    ' Navigate from this form to "ImgForm.aspx". The value of the selected 
    ' product id from the "ProductList" is passed as a parameter to the Image Form.
    '***********************************************************************************
    Private Sub ViewAdBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewAdBtn.Click
        Response.Redirect("ImgForm.aspx?ProdID=" + productsList.Items(productsList.SelectedIndex).Value + "&ProdName=" + productsList.Items(productsList.SelectedIndex).Text)
    End Sub

    '*******************************************************************
    ' This code is executed on the click event of "CloseForm" button.
    ' It releases the resources held by this sample application.
    '*******************************************************************
    Private Sub CloseBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseBtn.Click
        Try
            ' Close the database connection
            ConnectionManager.closeConnection()

            ' Display close dialog box
            Response.Write("<script>window.close();</script>")

            ' Disposes the webform
            Me.Dispose()

        Catch ex As Exception
            ' Error Handling
            Response.Redirect("Error.aspx?error=" + ex.Message)

        End Try
    End Sub
End Class
