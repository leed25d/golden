'**************************************************************************
'@author  Jagriti
'@version 1.0
'Development Environment        :  MS Visual Studio .NET
'Name of the File               :  EmpForm.vb
'Creation/Modification History  :
'                                  25-Sep-2003     Created

'Overview:
'This VB.NET source file manages the GUI of this sample application. For all the
'data operations it calls methods from ManageEmp.vb file. For maintaining 
'connection it calls methods from ConnectionMgr.vb class.
'**************************************************************************

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Text

Namespace XMLViewSample
    ' <summary>
    ' Demonstrates how to insert/update/delete/query from XMLType views
    ' available with Oracle9i release 2 (9.2) version.
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
            listDeptno.Enabled = False
            btnUpdate.Enabled = False
            DelBtn.Enabled = False

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
        Public WithEvents empRichTextBox As System.Windows.Forms.RichTextBox
        Friend WithEvents empDataGrid As System.Windows.Forms.DataGrid
        Friend WithEvents label2 As System.Windows.Forms.Label
        Friend WithEvents hintTextBox As System.Windows.Forms.RichTextBox
        Friend WithEvents groupBox4 As System.Windows.Forms.GroupBox
        Friend WithEvents richTextBox1 As System.Windows.Forms.RichTextBox
        Friend WithEvents groupBox5 As System.Windows.Forms.GroupBox
        Friend WithEvents label3 As System.Windows.Forms.Label
        Friend WithEvents listDeptno As System.Windows.Forms.ListBox
        Friend WithEvents groupBox3 As System.Windows.Forms.GroupBox
        Friend WithEvents DelBtn As System.Windows.Forms.Button
        Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents btnUpdate As System.Windows.Forms.Button
        Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents btnNewRec As System.Windows.Forms.Button
        Friend WithEvents btnInsert As System.Windows.Forms.Button

        Private curEmployeeid As String = ""

        ' Create instance of class that contains database operations methods
        Dim manageemp As manageemp = New manageemp()

        Private conStatus As String = "N"

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
            Me.empRichTextBox = New System.Windows.Forms.RichTextBox()
            Me.empDataGrid = New System.Windows.Forms.DataGrid()
            Me.label2 = New System.Windows.Forms.Label()
            Me.hintTextBox = New System.Windows.Forms.RichTextBox()
            Me.groupBox4 = New System.Windows.Forms.GroupBox()
            Me.richTextBox1 = New System.Windows.Forms.RichTextBox()
            Me.groupBox5 = New System.Windows.Forms.GroupBox()
            Me.label3 = New System.Windows.Forms.Label()
            Me.listDeptno = New System.Windows.Forms.ListBox()
            Me.groupBox3 = New System.Windows.Forms.GroupBox()
            Me.DelBtn = New System.Windows.Forms.Button()
            Me.groupBox2 = New System.Windows.Forms.GroupBox()
            Me.btnUpdate = New System.Windows.Forms.Button()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.btnNewRec = New System.Windows.Forms.Button()
            Me.btnInsert = New System.Windows.Forms.Button()
            Me.grpBxConnection.SuspendLayout()
            CType(Me.empDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.groupBox4.SuspendLayout()
            Me.groupBox5.SuspendLayout()
            Me.groupBox3.SuspendLayout()
            Me.groupBox2.SuspendLayout()
            Me.groupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'label1
            '
            Me.label1.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label1.Location = New System.Drawing.Point(240, 8)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(344, 24)
            Me.label1.TabIndex = 12
            Me.label1.Text = "Employee Management System"
            '
            'grpBxConnection
            '
            Me.grpBxConnection.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnExit, Me.btnConnect, Me.lblStatus, Me.txtConStr, Me.lblConstr, Me.txtPwd, Me.lblPwd, Me.label4, Me.txtName})
            Me.grpBxConnection.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.grpBxConnection.Location = New System.Drawing.Point(24, 40)
            Me.grpBxConnection.Name = "grpBxConnection"
            Me.grpBxConnection.Size = New System.Drawing.Size(720, 64)
            Me.grpBxConnection.TabIndex = 13
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
            Me.lblStatus.Location = New System.Drawing.Point(608, 32)
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
            'empRichTextBox
            '
            Me.empRichTextBox.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.empRichTextBox.Location = New System.Drawing.Point(424, 120)
            Me.empRichTextBox.Name = "empRichTextBox"
            Me.empRichTextBox.Size = New System.Drawing.Size(320, 328)
            Me.empRichTextBox.TabIndex = 15
            Me.empRichTextBox.Text = ""
            '
            'empDataGrid
            '
            Me.empDataGrid.CaptionText = "List of Employees"
            Me.empDataGrid.DataMember = ""
            Me.empDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
            Me.empDataGrid.Location = New System.Drawing.Point(24, 120)
            Me.empDataGrid.Name = "empDataGrid"
            Me.empDataGrid.ReadOnly = True
            Me.empDataGrid.Size = New System.Drawing.Size(360, 328)
            Me.empDataGrid.TabIndex = 14
            '
            'label2
            '
            Me.label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label2.Location = New System.Drawing.Point(24, 584)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(216, 16)
            Me.label2.TabIndex = 22
            Me.label2.Text = "Hints to Execute the Sample"
            '
            'hintTextBox
            '
            Me.hintTextBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.hintTextBox.ForeColor = System.Drawing.Color.Blue
            Me.hintTextBox.Location = New System.Drawing.Point(24, 600)
            Me.hintTextBox.Name = "hintTextBox"
            Me.hintTextBox.ReadOnly = True
            Me.hintTextBox.Size = New System.Drawing.Size(720, 72)
            Me.hintTextBox.TabIndex = 21
            Me.hintTextBox.Text = ""
            '
            'groupBox4
            '
            Me.groupBox4.Controls.AddRange(New System.Windows.Forms.Control() {Me.richTextBox1})
            Me.groupBox4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.groupBox4.ForeColor = System.Drawing.Color.Black
            Me.groupBox4.Location = New System.Drawing.Point(24, 456)
            Me.groupBox4.Name = "groupBox4"
            Me.groupBox4.Size = New System.Drawing.Size(376, 120)
            Me.groupBox4.TabIndex = 20
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
            Me.richTextBox1.Size = New System.Drawing.Size(360, 96)
            Me.richTextBox1.TabIndex = 0
            Me.richTextBox1.Text = "* Access data from an XMLType view in an Oracle 9.2 or higher database server wit" & _
            "h ODP.NET classes and native XMLType data types" & Microsoft.VisualBasic.ChrW(10) & Microsoft.VisualBasic.ChrW(10) & "* Insert/update/delete data fro" & _
            "m the XMLType view using instead-of-triggers on the view from ODP.NET"
            '
            'groupBox5
            '
            Me.groupBox5.Controls.AddRange(New System.Windows.Forms.Control() {Me.label3, Me.listDeptno})
            Me.groupBox5.Location = New System.Drawing.Point(520, 512)
            Me.groupBox5.Name = "groupBox5"
            Me.groupBox5.Size = New System.Drawing.Size(224, 48)
            Me.groupBox5.TabIndex = 29
            Me.groupBox5.TabStop = False
            '
            'label3
            '
            Me.label3.Location = New System.Drawing.Point(8, 18)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(128, 16)
            Me.label3.TabIndex = 21
            Me.label3.Text = "Valid Deptno Values"
            '
            'listDeptno
            '
            Me.listDeptno.Location = New System.Drawing.Point(136, 16)
            Me.listDeptno.Name = "listDeptno"
            Me.listDeptno.ScrollAlwaysVisible = True
            Me.listDeptno.Size = New System.Drawing.Size(80, 17)
            Me.listDeptno.TabIndex = 20
            '
            'groupBox3
            '
            Me.groupBox3.Controls.AddRange(New System.Windows.Forms.Control() {Me.DelBtn})
            Me.groupBox3.Location = New System.Drawing.Point(424, 512)
            Me.groupBox3.Name = "groupBox3"
            Me.groupBox3.Size = New System.Drawing.Size(88, 48)
            Me.groupBox3.TabIndex = 28
            Me.groupBox3.TabStop = False
            '
            'DelBtn
            '
            Me.DelBtn.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.DelBtn.Location = New System.Drawing.Point(8, 16)
            Me.DelBtn.Name = "DelBtn"
            Me.DelBtn.Size = New System.Drawing.Size(72, 24)
            Me.DelBtn.TabIndex = 20
            Me.DelBtn.Text = "Delete"
            '
            'groupBox2
            '
            Me.groupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnUpdate})
            Me.groupBox2.Location = New System.Drawing.Point(648, 464)
            Me.groupBox2.Name = "groupBox2"
            Me.groupBox2.Size = New System.Drawing.Size(96, 48)
            Me.groupBox2.TabIndex = 27
            Me.groupBox2.TabStop = False
            '
            'btnUpdate
            '
            Me.btnUpdate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnUpdate.Location = New System.Drawing.Point(16, 16)
            Me.btnUpdate.Name = "btnUpdate"
            Me.btnUpdate.Size = New System.Drawing.Size(64, 24)
            Me.btnUpdate.TabIndex = 11
            Me.btnUpdate.Text = "Update"
            '
            'groupBox1
            '
            Me.groupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnNewRec})
            Me.groupBox1.Location = New System.Drawing.Point(424, 464)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(224, 48)
            Me.groupBox1.TabIndex = 26
            Me.groupBox1.TabStop = False
            '
            'btnNewRec
            '
            Me.btnNewRec.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnNewRec.Location = New System.Drawing.Point(8, 16)
            Me.btnNewRec.Name = "btnNewRec"
            Me.btnNewRec.Size = New System.Drawing.Size(144, 24)
            Me.btnNewRec.TabIndex = 12
            Me.btnNewRec.Text = "Create New Record"
            '
            'btnInsert
            '
            Me.btnInsert.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnInsert.Location = New System.Drawing.Point(584, 480)
            Me.btnInsert.Name = "btnInsert"
            Me.btnInsert.Size = New System.Drawing.Size(56, 24)
            Me.btnInsert.TabIndex = 25
            Me.btnInsert.Text = "Insert"
            '
            'EmpForm
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(768, 677)
            Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.groupBox5, Me.groupBox3, Me.groupBox2, Me.btnInsert, Me.label2, Me.hintTextBox, Me.groupBox4, Me.empRichTextBox, Me.empDataGrid, Me.label1, Me.grpBxConnection, Me.groupBox1})
            Me.Name = "EmpForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Employee Management System"
            Me.grpBxConnection.ResumeLayout(False)
            CType(Me.empDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
            Me.groupBox4.ResumeLayout(False)
            Me.groupBox5.ResumeLayout(False)
            Me.groupBox3.ResumeLayout(False)
            Me.groupBox2.ResumeLayout(False)
            Me.groupBox1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

#End Region

        '*******************************************************************************
        ' The purpose of this method is to establish a database connection based on the 
        ' connection parameters entered in the textboxes and then populate the datagrid 
        ' and generate XML. The "getDBConnection" method of "ConnectionMgr.vb" class is 
        ' used to establish the connection. Methods from the "ManageEmp.vb" class are 
        ' called to populate the datagrid and generate XML.
        '******************************************************************************
        Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
            If (conStatus = "N") Then

                ' Set cursor to hour glass
                Cursor.Current = Cursors.WaitCursor

                ' Establish connection
                Dim Con As String = ConnectionMgr.getDBConnection(txtName.Text, txtPwd.Text, txtConStr.Text)

                ' If connection is established
                If (Con = "Connected") Then

                    ' Populate empDataGrid with data
                    Dim empDataSet As DataSet = New DataSet()
                    empDataSet = manageemp.populateEmpDataGrid()
                    empDataGrid.SetDataBinding(empDataSet, "Emp_View")

                    ' Populate empRichTextBox with generated XML
                    empRichTextBox.Text = manageemp.getXmlFromDB()

                    ' Populate Deptno list 
                    Dim deptDataSet As DataSet = New DataSet()
                    deptDataSet = manageemp.populateDeptno()
                    listDeptno.DisplayMember = deptDataSet.Tables(0).Columns("Deptno").ToString()
                    listDeptno.DataSource = deptDataSet.Tables("Dept").DefaultView
                    listDeptno.Visible = True

                    ' Enable/Disable appropriate UI controls
                    empRichTextBox.Enabled = True
                    btnNewRec.Enabled = True
                    listDeptno.Enabled = True

                    btnUpdate.Enabled = False
                    DelBtn.Enabled = False

                    lblStatus.Text = "Connected"
                    lblStatus.ForeColor = Color.Green

                    ' Set btnConnect label to disconnect 
                    btnConnect.Text = "Disconnect"

                    conStatus = "Y"

                    ' disable connection information textboxes
                    txtName.Enabled = False
                    txtPwd.Enabled = False
                    txtConStr.Enabled = False

                    ' Text for hintTextBox
                    hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
                     "To update a record - Select a record from the datagrid, make changes to the XML in " & _
                     "the textbox for the corresponding record and " & _
                     "click on the 'Update' button. " & _
                     "To delete a record - Select a record from the datagrid and click on the " & _
                     "'Delete' button. " & _
                                       "Empno is a key column, so do not change this. " & _
                     "View valid values for deptno from the listbox."

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
                listDeptno.Enabled = False
                btnUpdate.Enabled = False
                empRichTextBox.Enabled = False
                DelBtn.Enabled = False

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

                ' Dept # listbox
                empDataGrid.SetDataBinding(Nothing, "")
                listDeptno.DisplayMember = Nothing
                listDeptno.Visible = False
                listDeptno.DataSource = Nothing

                ' Hint message
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
        ' The purpose of this method is to clear the empRichTextBox, and then 
        ' display a sample XML record for insert.
        '***************************************************************************
        Private Sub btnNewRec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewRec.Click
            ' Set cursor to hour glass
            Cursor.Current = Cursors.WaitCursor

            ' Clear the contents of empRichTextBox
            empRichTextBox.Text = ""

            ' Display a sample XML record
            empRichTextBox.Text = manageemp.createNewRecord()

            ' Disable update and delete buttons during insert the operation
            btnUpdate.Enabled = False
            DelBtn.Enabled = False

            ' Enable the insert button 
            btnInsert.Enabled = True

            ' Update the hint message
			hintTextBox.Text = "Do not change 'Empno', as it is automatically generated. " & _
				                         "View valid values for deptno from the listbox. " & _
				                            "Click 'Insert' button to create a new record."

            ' Set cursor to default
            Cursor.Current = Cursors.Default

        End Sub

        '***********************************************************************************
        ' The purpose of this method is to insert a record into the 'Emp_View' XMLType view.
        ' The 'insertEmployee' method of 'ManageEmp' class is called. After insertion 
        ' is successful, the DataGrid and the XML records in the textbox are refreshed.
        ' *********************************************************************************
        Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
            ' Set cursor to hour glass
            Cursor.Current = Cursors.WaitCursor

            ' Insert record using XML data given in the empRichTextBox
            Dim result As Boolean = manageemp.insertRecord(empRichTextBox.Text)

            ' If record inserted
            If (result) Then

                ' Refresh the XML records given in the empRichTextBox
                empRichTextBox.Text = manageemp.getXmlFromDB()


                ' Refresh the DataGrid
                Dim empDataSet As DataSet = New DataSet()
                empDataSet = manageemp.populateEmpDataGrid()
                empDataGrid.SetDataBinding(empDataSet, "Emp_View")

                ' Enable the update button
                btnUpdate.Enabled = False
                DelBtn.Enabled = False

                btnInsert.Enabled = False

                ' Text for hintTextBox
				hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
					"To update a record - Select a record from the datagrid, make changes to the XML in " & _
					"the textbox for the corresponding record and " & _
					"click on the 'Update' button. " & _
					"To delete a record - Select a record from the datagrid and click on the " & _
					"'Delete' button. " & _
					"Empno is a key column, so do not change this. "  & _
					"View valid values for deptno from the listbox." 

                ' Set cursor to default
                Cursor.Current = Cursors.Default
            End If
        End Sub

        '****************************************************************************
        ' The purpose of this method is to update 'Emp_View', XMLType view table 
        ' using the XML records modified in the empRichTextBox.
        '**************************************************************************
        Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
            ' Set cursor to hour glass
            Cursor.Current = Cursors.WaitCursor

            ' Update data using XML data given in empRichTextBox
            Dim result As Boolean = manageemp.updateRecord(empRichTextBox.Text, curEmployeeid)

            ' If data updated successfully
            If (result) Then

                ' Refresh the XML records given in the empRichTextBox
                empRichTextBox.Text = manageemp.getXmlFromDB()

                btnUpdate.Enabled = False
                DelBtn.Enabled = False
                btnInsert.Enabled = False

                ' Refresh the DataGrid
                Dim empDataSet As DataSet = New DataSet()
                empDataSet = manageemp.populateEmpDataGrid()
                empDataGrid.SetDataBinding(empDataSet, "Emp_View")

                ' Text for hintTextBox
				hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
					"To update a record - Select a record from the datagrid, make changes to the XML in "  & _
					"the textbox for the corresponding record and " & _
					"click on the 'Update' button. " & _
					"To delete a record - Select a record from the datagrid and click on the " & _
					"'Delete' button. " & _
					"Empno is a key column so do not change this. " & _
					"View valid values for deptno from the listbox." 

                ' Set cursor to default
                Cursor.Current = Cursors.Default
            End If
        End Sub

        '*******************************************************************************
        ' The purpose of this method is to show a delete confirmation dialog box to the
        ' user. If the user confirms the delete, then the record displayed in the 
        ' textbox is deleted from the 'Emp_View'. 
        ' ****************************************************************************
        Private Sub DelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelBtn.Click
            Dim dialogResult As DialogResult

            ' Display Message Box to ask user for confirmation, before deleting record(s)
            dialogResult = MessageBox.Show(Me, "The record displayed in the Text Box will get deleted. " & _
             "Are you sure you want to delete record ?", "Delete Record Confirmation", MessageBoxButtons.YesNo)

            ' If user confirms delete
            If (dialogResult = dialogResult.Yes) Then

                ' Set cursor to hour glass
                Cursor.Current = Cursors.WaitCursor

                ' Delete a record from the 'Emp_View' using XML data given in empRichTextBox
                Dim result As Boolean = manageemp.deleteRecord(empRichTextBox.Text)

                ' If data updated successfully
                If (result) Then

                    ' Refresh the XML records given in the empRichTextBox
                    empRichTextBox.Text = manageemp.getXmlFromDB()

                    btnUpdate.Enabled = False
                    DelBtn.Enabled = False
                    btnInsert.Enabled = False

                    ' Refresh the DataGrid
                    Dim empDataSet As DataSet = New DataSet()
                    empDataSet = manageemp.populateEmpDataGrid()
                    empDataGrid.SetDataBinding(empDataSet, "Emp_View")

                    ' Text for hintTextBox
					hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
						"To update a record - Select a record from the datagrid, make changes to the XML in " & _
						"the textbox for the corresponding record and " & _
						"click on the 'Update' button. " & _
						"To delete a record - Select a record from the datagrid and click on the " & _
						"'Delete' button. "  & _
						"Empno is a key column, so do not change this. " & _
						"View valid values for deptno from the listbox." 

                    ' Set cursor to default
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

                ' Set cursor to hour glass
                Cursor.Current = Cursors.WaitCursor

                empDataGrid.CurrentCell = New DataGridCell(hti.Row, hti.Column)
                empDataGrid.Select(hti.Row)

                Dim rowNum As Integer = empDataGrid.CurrentRowIndex
                Dim cell1Value As Object = empDataGrid(rowNum, 0)
                Dim empno As String = cell1Value.ToString()
                curEmployeeid = empno
                empRichTextBox.Text = manageemp.getSelectedEmpRecord(empno)

                ' Enable the update and delete buttons
                btnUpdate.Enabled = True
                DelBtn.Enabled = True

                ' Disable insert button
                btnInsert.Enabled = False

                ' Text for hintTextBox
                hintTextBox.Text = "To insert a new record - click on the 'Create New Record' button. " & _
                    "To update a record - Select a record from the datagrid, make changes to the XML in " & _
					"the textbox for the corresponding record and " & _
					"click on the 'Update' button. " & _
					"To delete a record - Select a record from the datagrid and click on the " & _ 
					"'Delete' button. "  & _
					"Empno is a key column, so do not change this. " & _
					"View valid values for deptno from the listbox." 

                ' Set cursor to default
                Cursor.Current = Cursors.Default
            End If

        End Sub
    End Class
End Namespace
