'********************************************************************************
'@author  Jagriti
'@version 1.0
'Development Environment        :  MS Visual Studio .NET
'Name of the File               :  EmpForm.vb
'Creation/Modification History  :
'                                  23-Sept-2003     Created

'Overview:
'This VB.NET source file manages the GUI of this sample application. For all the
'data operations it calls methods from ManageEmp.vb file. For maintaining 
'connection it calls methods from ConnectionMgr.vb class.
'*******************************************************************************

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace RelationalDataSample
    ' <summary>
    ' Demonstrates how to retrieve relational data from database as XML 
    ' using ODP.NET and how to insert/update/delete relational data using XML.
    ' </summary>
    Public Class EmpForm
        Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Disable UI controls initially
            empRichTextBox.Enabled = False
            btnNewRec.Enabled = False
            btnInsert.Enabled = False
            delBtn.Enabled = False
            listDeptno.Enabled = False
            btnUpdate.Enabled = False

            ' Text for hintTextBox
            hintTextBox.Text = "Enter valid connection details to connect to an Oracle database"
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (ConnectionMgr.conn Is Nothing) Then
                    ConnectionMgr.closeConnection()
                End If
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'Create instance of class that contains operational methods
        Dim manageEmp As ManageEmp = New ManageEmp()

        Private conStatus As String = "N"


        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Friend WithEvents label1 As System.Windows.Forms.Label
        Friend WithEvents grpBxConnection As System.Windows.Forms.GroupBox
        Friend WithEvents btnExit As System.Windows.Forms.Button
        Friend WithEvents btnConnect As System.Windows.Forms.Button
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents txtConStr As System.Windows.Forms.TextBox
        Friend WithEvents lblConstr As System.Windows.Forms.Label
        Friend WithEvents txtPwd As System.Windows.Forms.TextBox
        Friend WithEvents lblPwd As System.Windows.Forms.Label
        Friend WithEvents label4 As System.Windows.Forms.Label
        Friend WithEvents txtName As System.Windows.Forms.TextBox
        Friend WithEvents empDataGrid As System.Windows.Forms.DataGrid
        Public WithEvents empRichTextBox As System.Windows.Forms.RichTextBox
        Friend WithEvents groupBox4 As System.Windows.Forms.GroupBox
        Friend WithEvents richTextBox1 As System.Windows.Forms.RichTextBox
        Friend WithEvents btnNewRec As System.Windows.Forms.Button
        Friend WithEvents btnInsert As System.Windows.Forms.Button
        Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents btnUpdate As System.Windows.Forms.Button
        Friend WithEvents groupBox5 As System.Windows.Forms.GroupBox
        Friend WithEvents label9 As System.Windows.Forms.Label
        Friend WithEvents groupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents listDeptno As System.Windows.Forms.ListBox
        Friend WithEvents delBtn As System.Windows.Forms.Button
        Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents label2 As System.Windows.Forms.Label
        Friend WithEvents hintTextBox As System.Windows.Forms.RichTextBox
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.label1 = New System.Windows.Forms.Label()
            Me.grpBxConnection = New System.Windows.Forms.GroupBox()
            Me.btnExit = New System.Windows.Forms.Button()
            Me.btnConnect = New System.Windows.Forms.Button()
            Me.lblStatus = New System.Windows.Forms.Label()
            Me.txtConStr = New System.Windows.Forms.TextBox()
            Me.lblConstr = New System.Windows.Forms.Label()
            Me.txtPwd = New System.Windows.Forms.TextBox()
            Me.lblPwd = New System.Windows.Forms.Label()
            Me.label4 = New System.Windows.Forms.Label()
            Me.txtName = New System.Windows.Forms.TextBox()
            Me.empDataGrid = New System.Windows.Forms.DataGrid()
            Me.empRichTextBox = New System.Windows.Forms.RichTextBox()
            Me.groupBox4 = New System.Windows.Forms.GroupBox()
            Me.richTextBox1 = New System.Windows.Forms.RichTextBox()
            Me.btnNewRec = New System.Windows.Forms.Button()
            Me.btnInsert = New System.Windows.Forms.Button()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.btnUpdate = New System.Windows.Forms.Button()
            Me.groupBox5 = New System.Windows.Forms.GroupBox()
            Me.label9 = New System.Windows.Forms.Label()
            Me.groupBox3 = New System.Windows.Forms.GroupBox()
            Me.listDeptno = New System.Windows.Forms.ListBox()
            Me.delBtn = New System.Windows.Forms.Button()
            Me.groupBox2 = New System.Windows.Forms.GroupBox()
            Me.label2 = New System.Windows.Forms.Label()
            Me.hintTextBox = New System.Windows.Forms.RichTextBox()
            Me.grpBxConnection.SuspendLayout()
            CType(Me.empDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.groupBox4.SuspendLayout()
            Me.groupBox3.SuspendLayout()
            Me.SuspendLayout()
            '
            'label1
            '
            Me.label1.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label1.Location = New System.Drawing.Point(216, 8)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(344, 24)
            Me.label1.TabIndex = 1
            Me.label1.Text = "Employee Management System"
            '
            'grpBxConnection
            '
            Me.grpBxConnection.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnExit, Me.btnConnect, Me.lblStatus, Me.txtConStr, Me.lblConstr, Me.txtPwd, Me.lblPwd, Me.label4, Me.txtName})
            Me.grpBxConnection.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpBxConnection.Location = New System.Drawing.Point(8, 40)
            Me.grpBxConnection.Name = "grpBxConnection"
            Me.grpBxConnection.Size = New System.Drawing.Size(704, 56)
            Me.grpBxConnection.TabIndex = 12
            Me.grpBxConnection.TabStop = False
            Me.grpBxConnection.Text = "Connection Details"
            '
            'btnExit
            '
            Me.btnExit.Location = New System.Drawing.Point(552, 24)
            Me.btnExit.Name = "btnExit"
            Me.btnExit.Size = New System.Drawing.Size(40, 24)
            Me.btnExit.TabIndex = 9
            Me.btnExit.Text = "Exit"
            '
            'btnConnect
            '
            Me.btnConnect.Location = New System.Drawing.Point(448, 24)
            Me.btnConnect.Name = "btnConnect"
            Me.btnConnect.Size = New System.Drawing.Size(96, 24)
            Me.btnConnect.TabIndex = 8
            Me.btnConnect.Text = "Connect"
            '
            'lblStatus
            '
            Me.lblStatus.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStatus.ForeColor = System.Drawing.Color.Red
            Me.lblStatus.Location = New System.Drawing.Point(592, 32)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(104, 16)
            Me.lblStatus.TabIndex = 7
            Me.lblStatus.Text = "Not Connected"
            Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'txtConStr
            '
            Me.txtConStr.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtConStr.Location = New System.Drawing.Point(368, 24)
            Me.txtConStr.Name = "txtConStr"
            Me.txtConStr.Size = New System.Drawing.Size(72, 23)
            Me.txtConStr.TabIndex = 5
            Me.txtConStr.Text = "orcl"
            '
            'lblConstr
            '
            Me.lblConstr.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblConstr.Location = New System.Drawing.Point(280, 27)
            Me.lblConstr.Name = "lblConstr"
            Me.lblConstr.Size = New System.Drawing.Size(88, 16)
            Me.lblConstr.TabIndex = 4
            Me.lblConstr.Text = "Host String"
            '
            'txtPwd
            '
            Me.txtPwd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtPwd.Location = New System.Drawing.Point(216, 24)
            Me.txtPwd.Name = "txtPwd"
            Me.txtPwd.Size = New System.Drawing.Size(56, 23)
            Me.txtPwd.TabIndex = 3
            Me.txtPwd.Text = "tiger"
            '
            'lblPwd
            '
            Me.lblPwd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblPwd.Location = New System.Drawing.Point(144, 27)
            Me.lblPwd.Name = "lblPwd"
            Me.lblPwd.Size = New System.Drawing.Size(72, 16)
            Me.lblPwd.TabIndex = 2
            Me.lblPwd.Text = "Password"
            '
            'label4
            '
            Me.label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label4.Location = New System.Drawing.Point(8, 27)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(80, 16)
            Me.label4.TabIndex = 0
            Me.label4.Text = "User Name"
            '
            'txtName
            '
            Me.txtName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtName.Location = New System.Drawing.Point(88, 24)
            Me.txtName.Name = "txtName"
            Me.txtName.Size = New System.Drawing.Size(56, 23)
            Me.txtName.TabIndex = 1
            Me.txtName.Text = "scott"
            '
            'empDataGrid
            '
            Me.empDataGrid.CaptionText = "List of Employees"
            Me.empDataGrid.DataMember = ""
            Me.empDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
            Me.empDataGrid.Location = New System.Drawing.Point(24, 112)
            Me.empDataGrid.Name = "empDataGrid"
            Me.empDataGrid.ReadOnly = True
            Me.empDataGrid.Size = New System.Drawing.Size(320, 368)
            Me.empDataGrid.TabIndex = 13

            '
            'empRichTextBox
            '
            Me.empRichTextBox.Location = New System.Drawing.Point(384, 112)
            Me.empRichTextBox.Name = "empRichTextBox"
            Me.empRichTextBox.Size = New System.Drawing.Size(312, 368)
            Me.empRichTextBox.TabIndex = 14
            Me.empRichTextBox.Text = ""
            '
            'groupBox4
            '
            Me.groupBox4.Controls.AddRange(New System.Windows.Forms.Control() {Me.richTextBox1})
            Me.groupBox4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.groupBox4.ForeColor = System.Drawing.Color.Black
            Me.groupBox4.Location = New System.Drawing.Point(24, 488)
            Me.groupBox4.Name = "groupBox4"
            Me.groupBox4.Size = New System.Drawing.Size(336, 96)
            Me.groupBox4.TabIndex = 18
            Me.groupBox4.TabStop = False
            Me.groupBox4.Text = "Sample Highlights"
            '
            'richTextBox1
            '
            Me.richTextBox1.BackColor = System.Drawing.Color.White
            Me.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.richTextBox1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.richTextBox1.ForeColor = System.Drawing.Color.Maroon
            Me.richTextBox1.Location = New System.Drawing.Point(8, 16)
            Me.richTextBox1.Name = "richTextBox1"
            Me.richTextBox1.ReadOnly = True
            Me.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
            Me.richTextBox1.Size = New System.Drawing.Size(320, 72)
            Me.richTextBox1.TabIndex = 0
            Me.richTextBox1.Text = "* Retrieve relational data as XML from an Oracle8i (or higher) database server in" & _
            "to .NET" & Microsoft.VisualBasic.ChrW(10) & Microsoft.VisualBasic.ChrW(10) & "* Insert/update/delete records in the database as XML"
            '
            'btnNewRec
            '
            Me.btnNewRec.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnNewRec.Location = New System.Drawing.Point(392, 504)
            Me.btnNewRec.Name = "btnNewRec"
            Me.btnNewRec.Size = New System.Drawing.Size(144, 24)
            Me.btnNewRec.TabIndex = 20
            Me.btnNewRec.Text = "Create New Record"
            '
            'btnInsert
            '
            Me.btnInsert.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnInsert.Location = New System.Drawing.Point(544, 504)
            Me.btnInsert.Name = "btnInsert"
            Me.btnInsert.Size = New System.Drawing.Size(56, 24)
            Me.btnInsert.TabIndex = 19
            Me.btnInsert.Text = "Insert"
            '
            'groupBox1
            '
            Me.groupBox1.Location = New System.Drawing.Point(384, 488)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(224, 48)
            Me.groupBox1.TabIndex = 21
            Me.groupBox1.TabStop = False
            '
            'btnUpdate
            '
            Me.btnUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnUpdate.Location = New System.Drawing.Point(624, 504)
            Me.btnUpdate.Name = "btnUpdate"
            Me.btnUpdate.Size = New System.Drawing.Size(64, 24)
            Me.btnUpdate.TabIndex = 25
            Me.btnUpdate.Text = "Update"
            '
            'groupBox5
            '
            Me.groupBox5.Location = New System.Drawing.Point(616, 488)
            Me.groupBox5.Name = "groupBox5"
            Me.groupBox5.Size = New System.Drawing.Size(80, 48)
            Me.groupBox5.TabIndex = 26
            Me.groupBox5.TabStop = False
            '
            'label9
            '
            Me.label9.Location = New System.Drawing.Point(488, 560)
            Me.label9.Name = "label9"
            Me.label9.Size = New System.Drawing.Size(136, 16)
            Me.label9.TabIndex = 27
            Me.label9.Text = "Valid  Deptno Values"
            '
            'groupBox3
            '
            Me.groupBox3.Controls.AddRange(New System.Windows.Forms.Control() {Me.listDeptno})
            Me.groupBox3.Location = New System.Drawing.Point(480, 536)
            Me.groupBox3.Name = "groupBox3"
            Me.groupBox3.Size = New System.Drawing.Size(216, 48)
            Me.groupBox3.TabIndex = 28
            Me.groupBox3.TabStop = False
            '
            'listDeptno
            '
            Me.listDeptno.Location = New System.Drawing.Point(144, 24)
            Me.listDeptno.Name = "listDeptno"
            Me.listDeptno.Size = New System.Drawing.Size(64, 17)
            Me.listDeptno.TabIndex = 10
            '
            'delBtn
            '
            Me.delBtn.Location = New System.Drawing.Point(400, 552)
            Me.delBtn.Name = "delBtn"
            Me.delBtn.Size = New System.Drawing.Size(64, 24)
            Me.delBtn.TabIndex = 29
            Me.delBtn.Text = "Delete"
            '
            'groupBox2
            '
            Me.groupBox2.Location = New System.Drawing.Point(384, 536)
            Me.groupBox2.Name = "groupBox2"
            Me.groupBox2.Size = New System.Drawing.Size(96, 48)
            Me.groupBox2.TabIndex = 30
            Me.groupBox2.TabStop = False
            '
            'label2
            '
            Me.label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label2.Location = New System.Drawing.Point(24, 592)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(216, 16)
            Me.label2.TabIndex = 32
            Me.label2.Text = "Hints to Execute the Sample"
            '
            'hintTextBox
            '
            Me.hintTextBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.hintTextBox.ForeColor = System.Drawing.Color.Blue
            Me.hintTextBox.Location = New System.Drawing.Point(24, 608)
            Me.hintTextBox.Name = "hintTextBox"
            Me.hintTextBox.ReadOnly = True
            Me.hintTextBox.Size = New System.Drawing.Size(672, 80)
            Me.hintTextBox.TabIndex = 31
            Me.hintTextBox.Text = ""
            '
            'EmpForm
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(720, 693)
            Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.label2, Me.hintTextBox, Me.label9, Me.groupBox3, Me.delBtn, Me.groupBox2, Me.btnUpdate, Me.groupBox5, Me.btnNewRec, Me.btnInsert, Me.groupBox1, Me.groupBox4, Me.empRichTextBox, Me.empDataGrid, Me.grpBxConnection, Me.label1})
            Me.Name = "EmpForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Employees Management System"
            Me.grpBxConnection.ResumeLayout(False)
            CType(Me.empDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
            Me.groupBox4.ResumeLayout(False)
            Me.groupBox3.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

#End Region

        '*******************************************************************************
        ' The purpose of this method is to establish a database connection based on the 
        ' connection parameters entered in the textboxes and then populate the datagrid 
        ' and generate XML. The "getDBConnection" method of "ConnectionMgr.vb" class is 
        ' used to establish connection. Methods from "ManageEmp.vb" class are called
        ' to populate datagrid and generate XML.
        ' *******************************************************************************
        Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click

            If (conStatus = "N") Then

                'Set cursor to hour glass
                Cursor.Current = Cursors.WaitCursor


                ' Establish connection
                Dim Con As String = ConnectionMgr.getDBConnection(txtName.Text, txtPwd.Text, txtConStr.Text)

                ' If connection is established
                If (Con = "Connected") Then

                    'Populate empDataGrid with data
                    Dim empDataSet As DataSet = New DataSet()
                    empDataSet = manageEmp.populateEmpDataGrid()
                    empDataGrid.SetDataBinding(empDataSet, "Emp")

                    ' Populate empRichTextBox with generated XML
                    empRichTextBox.Text = manageEmp.generateEmpXml()

                    ' Populate Deptno list 
                    Dim deptDataSet As DataSet = New DataSet()
                    deptDataSet = manageEmp.populateDeptno
                    listDeptno.DisplayMember = deptDataSet.Tables(0).Columns("Deptno").ToString()
                    listDeptno.DataSource = deptDataSet.Tables("Dept").DefaultView
                    listDeptno.Visible = True

                    ' Enable UI controls
                    empRichTextBox.Enabled = True
                    btnNewRec.Enabled = True
                    listDeptno.Enabled = True
                    btnUpdate.Enabled = True
                    delBtn.Enabled = True

                    ' Connection status
                    lblStatus.Text = "Connected"
                    lblStatus.ForeColor = Color.Green

                    ' Set btnConnect label to disconnect 
                    btnConnect.Text = "Disconnect"

                    conStatus = "Y"

                    ' Disable connection information textboxes
                    txtName.Enabled = False
                    txtPwd.Enabled = False
                    txtConStr.Enabled = False


                    ' Text for hintTextBox
                    hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
                     "To update the records - modify XML data and click on the 'Update' " & _
                     "button. Empno is a key column, so do not change this. " & _
                     "View valid values for deptno from the listbox. " & _
                     "To delete record - select a record from the datagrid and " & _
                     "click on the 'Delete' button. "

                    ' Set cursor to default
                    Cursor.Current = Cursors.Default

                End If
            Else

                ' Set cursor to default
                Cursor.Current = Cursors.Default

                ' If not connected
                ' Disable UI controls
                btnNewRec.Enabled = False
                btnInsert.Enabled = False
                delBtn.Enabled = False
                listDeptno.Enabled = False
                btnUpdate.Enabled = False
                empRichTextBox.Enabled = False

                ' Enable connection information textboxes
                txtName.Enabled = True
                txtPwd.Enabled = True
                txtConStr.Enabled = True

                lblStatus.Text = "Not Connected"
                lblStatus.ForeColor = Color.Red

                btnConnect.Text = "Connect"

                ' Once disconnected, clear contents of UI controls
                conStatus = "N"

                empRichTextBox.Text = ""

                empDataGrid.SetDataBinding(Nothing, "")
                listDeptno.DisplayMember = Nothing
                listDeptno.Visible = False
                listDeptno.DataSource = Nothing

                'Hint message
                hintTextBox.Text = "Enter valid connection details to connect to an Oracle database"
            End If
        End Sub

        '*************************************************************************
        ' The purpose of this method is to close the Oracle connection and exit
        ' the application.
        ' ***********************************************************************
        Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
            ConnectionMgr.closeConnection()
            Application.Exit()
        End Sub

        '****************************************************************************
        ' The purpose of this method is to clear empRichTextBox, and then display a
        ' sample XML record for insert.
        '****************************************************************************
        Private Sub btnNewRec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewRec.Click
            ' Clear the contents of empRichTextBox
            empRichTextBox.Text = ""

            ' Display a sample XML record
            empRichTextBox.Text = manageEmp.createNewRecord()

            ' Disable other buttons during insert operation
            btnUpdate.Enabled = False
            delBtn.Enabled = False

            ' Enable insert button 
            btnInsert.Enabled = True

            ' Hint message
            hintTextBox.Text = "Do not change 'Empno', as it is automatically generated. " & _
                                      "View valid values for deptno from the listbox. " & _
                                        "Click 'Insert' button to create a new record."
        End Sub

        '*******************************************************************************
        ' The purpose of this method is to insert a record into 'Emp' table using XML.
        ' The 'insertEmployee' method of 'ManageEmp' class is called. After insertion 
        ' is successful, the DataGrid and the XML records in the textbox are refreshed.
        '*******************************************************************************
        Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click

            ' Set cursor to hour glass
            Cursor.Current = Cursors.WaitCursor

            ' Insert record using XML data given in the empRichTextBox
            Dim result As Boolean = manageEmp.insertEmployee(empRichTextBox.Text)

            ' If record inserted
            If (result) Then

                ' Refresh the XML records given in the empRichTextBox
                empRichTextBox.Text = manageEmp.generateEmpXml()

                ' Refresh the DataGrid
                Dim empDataSet As DataSet = New DataSet()
                empDataSet = manageEmp.populateEmpDataGrid()
                empDataGrid.SetDataBinding(empDataSet, "Emp")

                ' Enable/disable appropriate buttons
                btnUpdate.Enabled = True
                delBtn.Enabled = True
                btnInsert.Enabled = False

                ' Text for hintTextBox
                hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
                 "To update the records - modify XML data and click on the 'Update' " & _
                 "button. Empno is a key column, so do not change this. " & _
                 "View valid values for deptno from the listbox. " & _
                 "To delete record - select a record from the datagrid and " & _
                 "click on the 'Delete' button. "

                ' Set cursor to default
                Cursor.Current = Cursors.Default

            End If
        End Sub

        '****************************************************************************
        ' The purpose of this method is to update 'Emp' table using the XML records
        ' modified in the empRichTextBox.
        '****************************************************************************
        Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

            ' Set cursor to hour glass
            Cursor.Current = Cursors.WaitCursor

            ' Update 'Emp' table data using XML data given in empRichTextBox
            Dim result As Boolean = manageEmp.updateEmployees(empRichTextBox.Text)

            ' If data updated successfully
            If (result) Then

                ' Refresh the XML records given in the empRichTextBox
                empRichTextBox.Text = manageEmp.generateEmpXml()

                ' Refresh the DataGrid
                Dim empDataSet As DataSet = New DataSet()
                empDataSet = manageEmp.populateEmpDataGrid()
                empDataGrid.SetDataBinding(empDataSet, "Emp")

                ' Text for hintTextBox
                hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
                 "To update the records - modify XML data and click on the 'Update' " & _
                 "button. Empno is a key column, so do not change this. " & _
                 "View valid values for deptno from the listbox. " & _
                 "To delete record - select a record from the datagrid and " & _
                 "click on the 'Delete' button. "

                ' Set cursor to default
                Cursor.Current = Cursors.Default
            End If
        End Sub

        '*********************************************************************************
        ' The purpose of this method is to delete record(s) from 'Emp' table. 
        ' The user can select a particular record for deletion or delete all records.
        '*********************************************************************************
        Private Sub delBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delBtn.Click
            Dim dialogResult As DialogResult

            ' Display Message Box to ask user for confirmation, before deleting record(s)
            dialogResult = MessageBox.Show(Me, "All the record(s) displayed in the Text Box will get deleted. " & _
                                        "Are you sure you want to delete record(s)?", "Delete Record(s) Confirmation", MessageBoxButtons.YesNo)

            ' If user confirms delete
            If (dialogResult = dialogResult.Yes) Then

                ' Set cursor to hour glass
                Cursor.Current = Cursors.WaitCursor

                ' Call deleteEmployees method of ManageEmp class for delete
                Dim result As Boolean = manageEmp.deleteEmployees(empRichTextBox.Text)

                ' If record(s) deleted successfully
                If (result) Then

                    ' Refresh the DataGrid
                    Dim empDataSet As DataSet = New DataSet()
                    empDataSet = manageEmp.populateEmpDataGrid()
                    empDataGrid.SetDataBinding(empDataSet, "Emp")

                    ' Refresh the XML records given in the empRichTextBox
                    empRichTextBox.Text = manageEmp.generateEmpXml()

                    ' Text for hintTextBox
                    hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
                     "To update the records - modify XML data and click on the 'Update' " & _
                     "button. Empno is a key column, so do not change this. " & _
                     "View valid values for deptno from the listbox. " & _
                     "To delete record - select a record from the datagrid and " & _
                     "click on the 'Delete' button. "

                    'Set cursor to default
                    Cursor.Current = Cursors.Default
                End If
            End If

        End Sub

        '*******************************************************************************
        ' The purpose of this method is to select a row in the datagrid on which the 
        ' mouse pointer is navigated currently.
        '******************************************************************************
        Private Sub empDataGrid_Mouseup(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles empDataGrid.MouseUp
            Dim pt As System.Drawing.Point = New Point(e.X, e.Y)
            Dim hti As DataGrid.HitTestInfo = empDataGrid.HitTest(pt)
            If (hti.Type = DataGrid.HitTestType.Cell) Then

                empDataGrid.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                empDataGrid.Select(hti.Row)

                Dim rowNum As Integer = empDataGrid.CurrentRowIndex
                Dim cell1Value As Object = empDataGrid(rowNum, 0)
                Dim empno As String = cell1Value.ToString()
                empRichTextBox.Text = manageEmp.getSelectedEmpRecord(empno)

                ' Enable/disable appropriate buttons
                btnUpdate.Enabled = True
                delBtn.Enabled = True
                btnInsert.Enabled = False

                ' Text for hintTextBox
                hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
                 "To update the records - modify XML data and click on the 'Update' " & _
                 "button. Empno is a key column, so do not change this. " & _
                 "View valid values for deptno from the listbox. " & _
                 "To delete record - select a record from the datagrid and " & _
                 "click on the 'Delete' button. "
            End If

        End Sub
    End Class
End Namespace
