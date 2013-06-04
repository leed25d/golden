'**************************************************************************
'@author  Abhijeet Kulkarni
'@version 1.0
'Development Environment        :  MS Visual Studio .Net
'Name of the File               :  GridPage.aspx.vb
'Creation/Modification History  :
'                                  09-Apr-2003     Created

'Overview: This page displays a dataGrid that contains country information.
'It shows how one can use Web Services and ODP.NET to Update, Insert and
'Delete data. This page is called through Main.aspx and should not be 
'visited directly.
'**************************************************************************
Imports client.webservice
Public Class GridPage
    Inherits System.Web.UI.Page
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents countryGrid As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Table1 As System.Web.UI.WebControls.Table
    Protected Shared wsclient As ODPService
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents InsertButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents LabelCountryName As System.Web.UI.WebControls.Label
    Protected WithEvents LabelPopulation As System.Web.UI.WebControls.Label
    Protected WithEvents LabelLanguage As System.Web.UI.WebControls.Label
    Protected WithEvents LabelCurrency As System.Web.UI.WebControls.Label
    Protected WithEvents TBCurrency As System.Web.UI.WebControls.TextBox
    Protected WithEvents TBLanguage As System.Web.UI.WebControls.TextBox
    Protected WithEvents TBCountryName As System.Web.UI.WebControls.TextBox
    Protected WithEvents TBPopulation As System.Web.UI.WebControls.TextBox
    Protected WithEvents BInsert As System.Web.UI.WebControls.Button
    Protected WithEvents StatusText As System.Web.UI.WebControls.Label
    Protected WithEvents InsertStatus As System.Web.UI.WebControls.Label
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents InsertCancelButton As System.Web.UI.WebControls.Button
    Protected Shared maindset As DataSet

#Region " Web Form Designer Generated Code "
    '************************************************************************
    'This call is required by the Web Form Designer.
    '************************************************************************
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '************************************************************************
    'This method initializes the Web Page
    '************************************************************************
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        'Try to obtain a Web Service reference
        Try
            'Create Web Service Proxy instance
            wsclient = New ODPService()
            'Get the Country list in the form of a DataSet
            maindset = wsclient.GetCountries()
            'Set the countryGrid datasource
            countryGrid.DataSource = maindset.Tables(0)
        Catch ex1 As Exception
            'Display error message
            StatusText.Text = "Either Web Service or database is not available."
        End Try
    End Sub

#End Region
    '************************************************************************
    'This method is called every time the page is loaded
    '************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Put user code to initialize the page here
            If Not Page.IsPostBack() Then
                'Bind the data to the countryGrid
                countryGrid.DataBind()
            End If
        Catch ex1 As Exception
            'Catch error. An exception is thrown if database or Web Service is not available
            StatusText.Text = "Either Web Service or database is not available. Update failed"
        End Try
    End Sub

    '************************************************************************
    'This method is called if the user wants to update a countryGrid row 
    '************************************************************************
    Public Sub countryGrid_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles countryGrid.EditCommand
        Try
            'Set the row index and point it to the selected row
            countryGrid.EditItemIndex = e.Item.ItemIndex
            'Bind the data to the countryGrid
            countryGrid.DataBind()
        Catch ex1 As Exception
            'Catch error. An exception is thrown if database or Web Service is not available 
            StatusText.Text = "Either Web Service or database is not available. Update failed"
        End Try
    End Sub

    '************************************************************************
    'This method is called if user aborts the update decision
    '************************************************************************
    Public Sub countryGrid_Cancel(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles countryGrid.CancelCommand
        Try
            'Remove updatable row
            countryGrid.EditItemIndex = -1
            'Bind the data to the countryGrid
            countryGrid.DataBind()
            'Set the status text
            StatusText.Text = "Edit operation has been cancelled"
        Catch ex1 As Exception
            'Catch error. An exception is thrown if database or Web Service is not available 
            StatusText.Text = "Either Web Service or database is not available. Update failed"
        End Try
    End Sub

    '************************************************************************
    'This method is called when users decides to commit the changes
    '************************************************************************
    Public Sub countryGrid_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles countryGrid.UpdateCommand
        'Get reference to the first editable control
        Dim popbox As TextBox = e.Item.Cells(1).Controls(0)
        'Set population value to zero
        Dim population As Double = 0
        Try
            'Try to parse the population string
            population = System.Double.Parse(popbox.Text)
        Catch ex1 As Exception
            'Display error message
            StatusText.Text = "You must enter a valid number"
        End Try
        If population < 0 Then
            StatusText.Text = "Population must be a positive integer"
        End If
        Try
            'Execute this block if population is > 0
            If population > 0 Then
                'Get the DataTable from main dataset
                Dim dt As DataTable = maindset.Tables(0)
                'Convert the current index in the grid page to the index in the DataSet
                Dim i As Integer = GridIndexToDataSetIndex(e.Item.ItemIndex, countryGrid.PageSize, countryGrid.CurrentPageIndex)
                Dim drow As DataRow = dt.Rows(i)
                'Set the new population value
                drow.Item(1) = population
                'Send the main DataSet through the Web Service and get updated dataset back
                Dim updateddset As DataSet = wsclient.UpdateCountries(maindset.GetChanges())
                'Merge the updated DataSet with the main DataSet
                maindset.Merge(updateddset)
                'Remove editable row
                countryGrid.EditItemIndex = -1
                'Bind the data to the countryGrid
                countryGrid.DataBind()
                'Set status text
                StatusText.Text = "One record has been updated"
            End If
        Catch ex2 As Exception
            'Catch error. An exception is thrown if database or Web Service is not available 
            StatusText.Text = "Either Web Service or database is not available. Update failed"
        End Try
    End Sub

    '************************************************************************
    'This method is called when user deletes a row from countryGrid 
    '************************************************************************
    Public Sub countryGrid_Delete(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles countryGrid.DeleteCommand
        Try
            'Get the DataTable from main dataset
            Dim dt As DataTable = maindset.Tables(0)
            'Convert the current index in the grid page to the index in the DataSet
            Dim i As Integer = GridIndexToDataSetIndex(e.Item.ItemIndex, countryGrid.PageSize, countryGrid.CurrentPageIndex)
            'Get reference to updated row
            Dim drow As DataRow = dt.Rows(i)
            'Delete the row
            drow.Delete()
            'Send the main DataSet through the Web Service and get updated dataset back
            Dim updateddset As DataSet = wsclient.UpdateCountries(maindset.GetChanges())
            'Merge the updated DataSet with the main DataSet
            maindset.Merge(updateddset)
            'Remove editable row
            countryGrid.EditItemIndex = -1
            'Set the first page in the default view.
            countryGrid.CurrentPageIndex = 0
            'Bind the data to the countryGrid
            countryGrid.DataBind()
            'Set status text
            StatusText.Text = "One record has been deleted"
        Catch ex1 As Exception
            'Catch error. An exception is thrown if database or Web Service is not available 
            StatusText.Text = "Either Web Service or database is not available. Delete failed"
        End Try
    End Sub

    '************************************************************************
    'This method is called if user selects Previous Page / Next page option
    '************************************************************************
    Sub countryGrid_Paged(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs) Handles countryGrid.PageIndexChanged
        Try
            'Cancel editing mode
            countryGrid.EditItemIndex = -1
            'Set the current page index number
            countryGrid.CurrentPageIndex = e.NewPageIndex
            'Set the status text
            StatusText.Text = "Data loaded with the help of DataSet retrieved through Web Service"
            'Bind the data to the countryGrid
            countryGrid.DataBind()
        Catch ex1 As Exception
            'Catch error. An exception is thrown if database or Web Service is not available 
            StatusText.Text = "Either Web Service or database is not available. Update failed"
        End Try
    End Sub

    '************************************************************************
    'This method is called if user clicks Insert row link
    '************************************************************************
    Private Sub InsertButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertButton.Click
        'Display controls that allow users to add one row
        LabelCountryName.Visible = True
        LabelPopulation.Visible = True
        LabelLanguage.Visible = True
        LabelCurrency.Visible = True
        TBCountryName.Visible = True
        TBPopulation.Visible = True
        TBLanguage.Visible = True
        TBCurrency.Visible = True
        BInsert.Visible = True
        InsertCancelButton.Visible = True
        HyperLink1.NavigateUrl = "Main.aspx"
    End Sub

    '************************************************************************
    'This method is called if user presses the Insert Data row
    '************************************************************************
    Private Sub BInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BInsert.Click
        Dim valid As Boolean = True
        'Check Country Name length
        If TBCountryName.Text.Length() < 2 Then
            InsertStatus.Text = "You must enter country name"
            valid = False
            Return
        End If
        'Check Population Name length
        If TBPopulation.Text.Length() < 2 Then
            InsertStatus.Text = "You must enter population"
            valid = False
            Return
        End If
        Dim i As Integer = 0
        Try
            'Try to parse population string and convert it to an integer
            i = System.Double.Parse(TBPopulation.Text)
        Catch ex1 As Exception
        End Try
        'Check if population is <= 0
        If i <= 0 Then
            InsertStatus.Text = "Population must be a positive integer"
            valid = False
            Return
        End If
        'Check Language length
        If TBLanguage.Text.Length() < 2 Then
            InsertStatus.Text = "You must enter language"
            valid = False
            Return
        End If
        'Check Currency length
        If TBCurrency.Text.Length() < 2 Then
            InsertStatus.Text = "You must enter currency"
            valid = False
            Return
        End If
        Try
            'All is well insert the row
            If valid = True Then
                'Get the DataTable from main dataset
                Dim dt As DataTable = maindset.Tables(0)
                'Create a new row
                Dim drow As DataRow = dt.NewRow()
                'Set row contents
                drow.Item(0) = TBCountryName.Text
                drow.Item(1) = System.Double.Parse(TBPopulation.Text)
                drow.Item(2) = TBLanguage.Text
                drow.Item(3) = TBCurrency.Text
                'Add the row to the table in the main DataSet
                maindset.Tables(0).Rows.Add(drow)
                'Send the main DataSet through the Web Service and get updated dataset back
                Dim updateddset As DataSet = wsclient.UpdateCountries(maindset.GetChanges())
                'Merge the updated DataSet with the main DataSet
                maindset.Merge(updateddset)
                'Bind the data to the countryGrid
                countryGrid.DataBind()
                'Hide the controls that allowed user to edit a new row
                LabelCountryName.Visible = False
                LabelPopulation.Visible = False
                LabelLanguage.Visible = False
                LabelCurrency.Visible = False
                TBCountryName.Visible = False
                TBPopulation.Visible = False
                TBLanguage.Visible = False
                TBCurrency.Visible = False
                BInsert.Visible = False
                InsertCancelButton.Visible = False
                'It was a valid row so set status text to empty string
                InsertStatus.Text = ""
                StatusText.Text = "One record has been inserted"
            End If
        Catch ex2 As System.Data.ConstraintException
            'Catch error. An exception is thrown if database or Web Service is not available 
            StatusText.Text = "Duplicate Country Name. Record not inserted"
        End Try
    End Sub

    '************************************************************************
    'This method returns converts the index of a row in the countryGrid to
    'the index of the row in the DataSet
    '************************************************************************
    Private Function GridIndexToDataSetIndex(ByVal rowIndex As Integer, ByVal pageSize As Integer, ByVal currentPageIndex As Integer) As Integer
        If currentPageIndex >= 1 Then
            GridIndexToDataSetIndex = (pageSize * currentPageIndex) + rowIndex
        Else
            GridIndexToDataSetIndex = rowIndex
        End If
    End Function

    '************************************************************************
    'This method hides Row Insert form
    '************************************************************************
    Private Sub InsertCancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertCancelButton.Click
        'Hide controls that allow users to add one row
        LabelCountryName.Visible = False
        LabelPopulation.Visible = False
        LabelLanguage.Visible = False
        LabelCurrency.Visible = False
        TBCountryName.Visible = False
        TBPopulation.Visible = False
        TBLanguage.Visible = False
        TBCurrency.Visible = False
        BInsert.Visible = False
        InsertCancelButton.Visible = False
        HyperLink1.NavigateUrl = "Main.aspx"
    End Sub
End Class
