'********************************************************************************************

' @author                        :  Jagriti
' @version                       :  1.0
' Development Environment        :  Microsoft Visual Studio.NET
' Name of the File               :  ProductForm.aspx.vb
' Creation/Modification History  :
'                        27-Jan-2003     Created 
'
' Sample Overview: 
' The purpose of this VB.NET sample is to demonstrate how a Data Set populated through
' REF CURSORs can be updated using ODP.NET. Though REF CURSORs are 
' not updateable directly but data retrieved into Data Set is updateable. By 
' using custom SQL statements OracleDataAdapter can flush any REF CURSOR updates to database.
'
' When this sample is run, a webform with two Data Grids is displayed. One Data Grid is
' populated with records for products with "orderable" status and other Data Grid 
' with records for products with "under development" status. The data is fetched
' from database stored procedure "getProductsInfo" from "ODPNet" database package. 
' The data from REF CURSORs is fetched in "productsDataset". "productsDataset" contains
' two Data Tables 'Products' and 'Products1"'.
' To change the Product Status for products with "under development" product status,
' the user can select one or more CheckBoxes available in "UDevlopmentDataGrid".
' To update the Data Rows with changed product status, the user can click the 
' "Update Status" button. The Data Rows for which the user wants to change the 
' Product Status are retrieved based on the Product IDs selected. The product status
' for these data rows is set to 'orderable'. 'productsCmd' OracleCommand's command type
' is set to stored procedure to execute 'ODPNet.updateStatus' database stored procedure.
' Parameters for Product ID and Product Status are bound to the OracleCommand object.
' updateProductsCmd' is set and executed through 'productsAdapter', hence, updating the 
' Data Set populated through REF CURSORs using ODP.NET.
'********************************************************************************************

' Standard Namespaces referenced in this sample
Imports System.Data
Imports System.IO

' ODP.NET specific Namespaces referenced in this sample
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types



Public Class ProductForm
    Inherits System.Web.UI.Page

    ' UI controls 
    Protected WithEvents UDevelopmentDataGrid As System.Web.UI.WebControls.DataGrid
    Protected WithEvents OrderableDataGrid As System.Web.UI.WebControls.DataGrid
    Protected WithEvents HeadingLbl As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents UpdateButton As System.Web.UI.WebControls.Button
    Protected WithEvents CloseButton As System.Web.UI.WebControls.Button

    ' For database connection
    Dim conn As New OracleConnection()

    ' To fill Data Set from datasource
    Dim productsAdapter As New OracleDataAdapter()

     
    'Command object for fetching data
    Dim productsCmd As New OracleCommand()

    ' Command object for updating data
    Dim updateProductsCmd As New OracleCommand()

    ' In-memory cache of data
    Dim productsDataset As New DataSet("ProductsDataset")

    ' Counter variable
    Dim count As Int64


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '*****************************************************************************
    ' This method contains code for initializing components and maintaining sesion
    ' state for productsDataset.
    '*****************************************************************************
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        If (Session.Count > 0) Then
            Dim tempDS As Object = Session.Item(0)

            productsDataset = CType(tempDS, DataSet)

        End If

    End Sub

#End Region

    '*******************************************************************************************
    ' This method contains code that is required for this sample application initialization.
    ' Firstly, a database connection is established using "GetDBConnection" method. A method
    ' "PopulateProducts" is called that populates "OrderableDataGrid" and "UDevelopmentDataGrid".
    '********************************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Check if connection to database is established successfully
        If (GetDBConnection()) Then

            ' Populate the Data Grids for the first time this webform is loaded
            If (Not IsPostBack()) Then

                ' To maintain UDevelopmentDataGrid's view state
                UDevelopmentDataGrid.EnableViewState = True

                ' Populate both the Data Grids
                PopulateProducts()
            End If

        End If
    End Sub

    '****************************************************************************************************
    ' This procedure is executed on the 'Update Status' button click event. The purpose of this
    ' method is to update the product status of selected products from 'under development'
    ' to 'orderable' product status.
    ' Flow of execution of this method is as follows:
    ' 1. Loop through each row in UDevelopmentDataGrid to find whether any checkbox is selected.
    ' 2. In case, one or more checkbox(s) is selected, set properties for updateProductsCmd.
    ' 3. Set updateProductsCmd's command type to stored procedure and command text to "ODPNet.UpdateStatus".
    ' 4. As "ODPNet.UpdateStatus" stored procedure takes Product ID, Product Status as input parameters,
    '    bind these two parameters to the updateProductsCmd.
    ' 5. Based on the selected Product IDs, select the DataRows from 'Products1' Data Table 
    '    of productsDataset and set Product Status for the selected Data Rows to "orderable".
    ' 6. Execute the update command through productsAdapter to update productsDataset.
    '******************************************************************************************************
    Sub UpdateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateButton.Click
        Try

            Dim UDevelopmentDataGridItem As DataGridItem
            Dim Cbx As CheckBox = New CheckBox()
            Dim myStr As String
            Dim i As Int32 = 0
            Dim prodid As String
            Dim rowsWanted As DataRow()

            '  Loop through each row in UDevelopmentDataGrid
            For Each UDevelopmentDataGridItem In UDevelopmentDataGrid.Items

                ' Get checkbox control instance
                Cbx = CType(UDevelopmentDataGridItem.Cells(1).FindControl("cbx"), CheckBox)


                ' If checkbox is selected
                If Cbx.Checked = True Then
                    i += 1

                    ' Get value of the first column "Product ID"
                    myStr = UDevelopmentDataGridItem.Cells(0).Text

                    ' Get the Data Row for selected product
                    rowsWanted = productsDataset.Tables("Products1").Select("Product_id = " + myStr + " ")

                    ' Modify Product Status to orderable
                    rowsWanted(0)("product_status") = "orderable"

                End If
            Next

            ' Call 'updateStatus' stored procedure of 'ODPNet' database package
            ' for actual updation
            updateProductsCmd.CommandText = "ODPNet.updateStatus"

            ' Use existing database connection 
            updateProductsCmd.Connection = conn

            ' Set command type to stored procedure
            updateProductsCmd.CommandType = CommandType.StoredProcedure

            If i >= 1 Then
                ' Parameter for binding Product ID 
                Dim productIDParam As OracleParameter = New OracleParameter("productID", OracleDbType.Int32)
                productIDParam.Direction = ParameterDirection.Input

                ' Set Data Row version to orignal to use the existing product ids value
                productIDParam.SourceVersion = DataRowVersion.Original
                productIDParam.SourceColumn = "product_id"
                updateProductsCmd.Parameters.Add(productIDParam)


                ' Parameter for binding Product Status
                Dim productStatusParam As OracleParameter = New OracleParameter("productStatus", OracleDbType.Varchar2, 32)
                productStatusParam.Direction = ParameterDirection.Input
                productStatusParam.SourceColumn = "product_status"

                ' Set Data Row version to current to use the changed Product Status
                productStatusParam.SourceVersion = DataRowVersion.Current

                updateProductsCmd.Parameters.Add(productStatusParam)

                ' Setup the update command on adapter
                productsAdapter.UpdateCommand = updateProductsCmd

                ' Update the changes made to productsDataset
                productsAdapter.Update(productsDataset, "Products1")

                productsDataset.Clear()

                ' Refresh the Data Grids
                PopulateProducts()

            Else
                ' If none of the checkbox(s) is selected, display message
                Response.Write("<b>Atleast one product should be selected before changing product status!</b>")
            End If

            ' Disable Update Button is no products with Under Development exists. 
            If (UDevelopmentDataGrid.Items.Count = 0) Then
                UpdateButton.Enabled = False
            Else
                UpdateButton.Enabled = True
            End If
        Catch ex As Exception
            ' If error occurs redirect to an error page
            Response.Redirect("Error.aspx?error=" + ex.Message + ex.StackTrace)

        End Try
    End Sub


    '********************************************************************************
    ' The purpose of this method is to populate the Data Grids with data from
    ' REF CURSORs returned as OUT parameters from 'ODPNet.getProductsInfo'
    ' database Stored Procedure.
    ' The execution flow for this method is as follows:
    ' 1. For "productsCmd" OracleCommand object, set the command as
    '    a call to database Stored Procedure "ODPNet.getProductsInfo".
    ' 2. Set command type as "Stored Procedure" for "productsCmd".
    ' 3. Bind the REF CURSOR parameters as "OracleDbType.REFCURSOR" to the 
    '    OracleCommand object.
    ' 4. "Fill" method for "productsAdapter" OracleDataAdapter fills "productsDataset"
    '    with data returned upon executing OracleCommand object .
    ' 5. "productDataset" contains two Data Tables "Products" and "Products1".
    '    "Products" Data Table contains data from "orderable" REF CURSOR
    '    parameter. "Products1" Data Table contains data from "udevelopment"
    '    REF CURSOR parameter.
    ' 6. "OrderableDataGrid" is bound to "Products" Data Table and
    '    "UDevelopmentDataGrid" is bound to "Products1" Data Table. 
    '*********************************************************************************
    Private Sub PopulateProducts()
        Try

            ' Call 'getProductsInfo' stored procedure of 'ODPNet' database package
            productsCmd.CommandText = "ODPNet.getProductsInfo"

            ' Set the command Type to Stored Procedure
            productsCmd.CommandType = CommandType.StoredProcedure

            ' Set the connection instance
            productsCmd.Connection = conn

            ' Bind the REF CURSOR parameters to the OracleCommand object
            ' For 'orderable' product status
            productsCmd.Parameters.Add("orderable", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            ' For 'under development' product status
            productsCmd.Parameters.Add("udevelopment", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)

            ' Set the command for the Data Adapter
            productsAdapter.SelectCommand = productsCmd

            ' Fill Data Set with data from Data Adapter
            productsAdapter.Fill(productsDataset, "Products")

            ' Set datasource for Data Grids
            OrderableDataGrid.DataSource = productsDataset.Tables("Products").DefaultView
            UDevelopmentDataGrid.DataSource = productsDataset.Tables("Products1").DefaultView

            ' Bind the data to Data Grids
            OrderableDataGrid.DataBind()
            UDevelopmentDataGrid.DataBind()

            ' Save productsDataset session state
            Session.Add("ProductDS", productsDataset)

            ' Disable Update Button is no products with Under Development exists. 
            If (UDevelopmentDataGrid.Items.Count = 0) Then
                UpdateButton.Enabled = False
            Else
                UpdateButton.Enabled = True
            End If

        Catch ex As Exception
            ' If error occurs redirect to an error page
            Response.Redirect("Error.aspx?error=" + "Error :" + ex.Message + ex.StackTrace)

        End Try
    End Sub


    '*****************************************************************************
    ' The purpose of this method is to establish the database connection 
    ' using the database parameters given in "ConnectionParams.vb"
    '******************************************************************
    Private Function GetDBConnection() As Boolean
        Try

            ' set connection parameters in ConnectionParams.vb module
            ConnectionParams.setparams()

            ' set provider and connection parameters for database connection
            ' get the connection parameters from ConnectionParams.vb file
            Dim connectionString As String = "Data Source=" + ConnectionParams.datasource & _
                                             ";User ID=" + ConnectionParams.username & _
                                             ";Password=" + ConnectionParams.password

            ' Connection to datasource, using connection parameters given above
            conn = New OracleConnection(connectionString)

            conn.Open()

            Return True
        Catch ex As Exception

            ' If error occurs redirect to an error page
            Response.Redirect("Error.aspx?error=" + "Error :" + ex.Message)

        End Try
    End Function

    '****************************************************************
    ' The purpose of this method is to close the web form and release 
    ' resources held by this application.
    '****************************************************************
    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Try
            Response.Write("<script>window.close();</script>")

            ' Release resources
            productsCmd.Dispose()
            updateProductsCmd.Dispose()
            productsAdapter.Dispose()
            conn.Dispose()
            Me.Dispose()
        Catch ex As Exception

            ' If error occurs redirect to an error page
            Response.Redirect("Error.aspx?error=" + "Error :" + ex.Message)

        End Try

    End Sub
End Class
