/**************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .NET
Name of the File               :  EmpForm.cs
Creation/Modification History  :
                                  21-Aug-2003     Created

Overview:
This C# source file manages the GUI of this sample application. For all the
data operations it calls methods from ManageEmp.cs file. For maintaining 
connection it calls methods from ConnectionMgr.cs class.
**************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;

namespace RelationalDataSample
{
	/// <summary>
	/// Demonstrates how to insert/update/delete/query from XMLType views
	/// available with Oracle9i release 2 (9.2) version.
	/// </summary>
	public class EmpForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.GroupBox grpBxConnection;
		private System.Windows.Forms.Label lblPwd;
		private System.Windows.Forms.TextBox txtPwd;
		private System.Windows.Forms.Label lblConstr;
		private System.Windows.Forms.TextBox txtConStr;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.DataGrid empDataGrid;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		public System.Windows.Forms.RichTextBox empRichTextBox;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnNewRec;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RichTextBox hintTextBox;
		
		private System.Windows.Forms.DataGridBoolColumn boolCol;

		private String conStatus = "N";
		private System.Windows.Forms.DataGridTextBoxColumn empno;
		private System.Windows.Forms.DataGridTextBoxColumn ename;
		private System.Windows.Forms.DataGridTextBoxColumn job;
		private System.Windows.Forms.DataGridTextBoxColumn sal;
		private System.Windows.Forms.DataGridTextBoxColumn deptno;
		private System.Windows.Forms.ListBox listDeptno;
		private System.Windows.Forms.Button DelBtn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label3;
		private String curEmployeeid = "";

		// Create instance of class that contains database operations methods
		private ManageEmp  manageEmp = new ManageEmp();
				
		public EmpForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Disable UI controls initially
            empRichTextBox.Enabled = false;
			btnNewRec.Enabled = false;
			btnInsert.Enabled = false;
			listDeptno.Enabled = false;
			btnUpdate.Enabled = false;
			DelBtn.Enabled = false;

			// Text for hintTextBox
			hintTextBox.Text = "Enter valid connection details to connect to an Oracle database";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				ConnectionMgr.closeConnection();
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EmpForm));
			this.label1 = new System.Windows.Forms.Label();
			this.empDataGrid = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.empno = new System.Windows.Forms.DataGridTextBoxColumn();
			this.ename = new System.Windows.Forms.DataGridTextBoxColumn();
			this.job = new System.Windows.Forms.DataGridTextBoxColumn();
			this.sal = new System.Windows.Forms.DataGridTextBoxColumn();
			this.deptno = new System.Windows.Forms.DataGridTextBoxColumn();
			this.boolCol = new System.Windows.Forms.DataGridBoolColumn();
			this.empRichTextBox = new System.Windows.Forms.RichTextBox();
			this.btnInsert = new System.Windows.Forms.Button();
			this.grpBxConnection = new System.Windows.Forms.GroupBox();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnConnect = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.txtConStr = new System.Windows.Forms.TextBox();
			this.lblConstr = new System.Windows.Forms.Label();
			this.txtPwd = new System.Windows.Forms.TextBox();
			this.lblPwd = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.btnNewRec = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.hintTextBox = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listDeptno = new System.Windows.Forms.ListBox();
			this.DelBtn = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.empDataGrid)).BeginInit();
			this.grpBxConnection.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(240, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(344, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Employee Management System";
			// 
			// empDataGrid
			// 
			this.empDataGrid.CaptionText = "List of Employees";
			this.empDataGrid.DataMember = "";
			this.empDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.empDataGrid.Location = new System.Drawing.Point(24, 120);
			this.empDataGrid.Name = "empDataGrid";
			this.empDataGrid.ReadOnly = true;
			this.empDataGrid.Size = new System.Drawing.Size(360, 328);
			this.empDataGrid.TabIndex = 2;
			this.empDataGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																									this.dataGridTableStyle1});
			this.empDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.empDataGrid_MouseUp);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.empDataGrid;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.empno,
																												  this.ename,
																												  this.job,
																												  this.sal,
																												  this.deptno});
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "emp_view";
			this.dataGridTableStyle1.ReadOnly = true;
			// 
			// empno
			// 
			this.empno.Format = "";
			this.empno.FormatInfo = null;
			this.empno.HeaderText = "Emp #";
			this.empno.MappingName = "empno";
			this.empno.Width = 60;
			// 
			// ename
			// 
			this.ename.Format = "";
			this.ename.FormatInfo = null;
			this.ename.HeaderText = "Name";
			this.ename.MappingName = "ename";
			this.ename.Width = 72;
			// 
			// job
			// 
			this.job.Format = "";
			this.job.FormatInfo = null;
			this.job.HeaderText = "Job";
			this.job.MappingName = "job";
			this.job.Width = 77;
			// 
			// sal
			// 
			this.sal.Format = "";
			this.sal.FormatInfo = null;
			this.sal.HeaderText = "Salary";
			this.sal.MappingName = "sal";
			this.sal.Width = 60;
			// 
			// deptno
			// 
			this.deptno.Format = "";
			this.deptno.FormatInfo = null;
			this.deptno.HeaderText = "Dept #";
			this.deptno.MappingName = "deptno";
			this.deptno.Width = 50;
			// 
			// boolCol
			// 
			this.boolCol.FalseValue = false;
			this.boolCol.HeaderText = "Info Current";
			this.boolCol.MappingName = "Current";
			this.boolCol.NullValue = ((System.DBNull)(resources.GetObject("boolCol.NullValue")));
			this.boolCol.TrueValue = true;
			this.boolCol.Width = 70;
			// 
			// empRichTextBox
			// 
			this.empRichTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.empRichTextBox.Location = new System.Drawing.Point(424, 120);
			this.empRichTextBox.Name = "empRichTextBox";
			this.empRichTextBox.Size = new System.Drawing.Size(320, 328);
			this.empRichTextBox.TabIndex = 3;
			this.empRichTextBox.Text = "";
			// 
			// btnInsert
			// 
			this.btnInsert.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnInsert.Location = new System.Drawing.Point(584, 480);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(56, 24);
			this.btnInsert.TabIndex = 7;
			this.btnInsert.Text = "Insert";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// grpBxConnection
			// 
			this.grpBxConnection.Controls.AddRange(new System.Windows.Forms.Control[] {
																						  this.btnExit,
																						  this.btnConnect,
																						  this.lblStatus,
																						  this.txtConStr,
																						  this.lblConstr,
																						  this.txtPwd,
																						  this.lblPwd,
																						  this.label4,
																						  this.txtName});
			this.grpBxConnection.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpBxConnection.Location = new System.Drawing.Point(24, 40);
			this.grpBxConnection.Name = "grpBxConnection";
			this.grpBxConnection.Size = new System.Drawing.Size(720, 64);
			this.grpBxConnection.TabIndex = 11;
			this.grpBxConnection.TabStop = false;
			this.grpBxConnection.Text = "Connection Details";
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(552, 24);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(40, 24);
			this.btnExit.TabIndex = 9;
			this.btnExit.Text = "Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(448, 24);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(96, 24);
			this.btnConnect.TabIndex = 8;
			this.btnConnect.Text = "Connect";
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.Red;
			this.lblStatus.Location = new System.Drawing.Point(608, 32);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(104, 16);
			this.lblStatus.TabIndex = 7;
			this.lblStatus.Text = "Not Connected";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtConStr
			// 
			this.txtConStr.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtConStr.Location = new System.Drawing.Point(368, 24);
			this.txtConStr.Name = "txtConStr";
			this.txtConStr.Size = new System.Drawing.Size(72, 23);
			this.txtConStr.TabIndex = 5;
			this.txtConStr.Text = "orcl";
			// 
			// lblConstr
			// 
			this.lblConstr.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblConstr.Location = new System.Drawing.Point(280, 27);
			this.lblConstr.Name = "lblConstr";
			this.lblConstr.Size = new System.Drawing.Size(88, 16);
			this.lblConstr.TabIndex = 4;
			this.lblConstr.Text = "Host String";
			// 
			// txtPwd
			// 
			this.txtPwd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPwd.Location = new System.Drawing.Point(216, 24);
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.Size = new System.Drawing.Size(56, 23);
			this.txtPwd.TabIndex = 3;
			this.txtPwd.Text = "tiger";
			// 
			// lblPwd
			// 
			this.lblPwd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPwd.Location = new System.Drawing.Point(144, 27);
			this.lblPwd.Name = "lblPwd";
			this.lblPwd.Size = new System.Drawing.Size(72, 16);
			this.lblPwd.TabIndex = 2;
			this.lblPwd.Text = "Password";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "User Name";
			// 
			// txtName
			// 
			this.txtName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtName.Location = new System.Drawing.Point(88, 24);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(56, 23);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "scott";
			// 
			// btnNewRec
			// 
			this.btnNewRec.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnNewRec.Location = new System.Drawing.Point(8, 16);
			this.btnNewRec.Name = "btnNewRec";
			this.btnNewRec.Size = new System.Drawing.Size(144, 24);
			this.btnNewRec.TabIndex = 12;
			this.btnNewRec.Text = "Create New Record";
			this.btnNewRec.Click += new System.EventHandler(this.btnNewRec_Click);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnUpdate.Location = new System.Drawing.Point(16, 16);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(64, 24);
			this.btnUpdate.TabIndex = 11;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.richTextBox1});
			this.groupBox4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox4.ForeColor = System.Drawing.Color.Black;
			this.groupBox4.Location = new System.Drawing.Point(24, 456);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(376, 120);
			this.groupBox4.TabIndex = 17;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Sample Highlights";
			// 
			// richTextBox1
			// 
			this.richTextBox1.BackColor = System.Drawing.Color.White;
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox1.ForeColor = System.Drawing.Color.Maroon;
			this.richTextBox1.Location = new System.Drawing.Point(8, 16);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(360, 96);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "* Access data from an XMLType view in an Oracle 9.2 or higher database server wit" +
				"h ODP.NET classes and native XMLType data types\n\n* Insert/update/delete data fro" +
				"m the XMLType view using instead-of-triggers on the view from ODP.NET";
			// 
			// hintTextBox
			// 
			this.hintTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.hintTextBox.ForeColor = System.Drawing.Color.Blue;
			this.hintTextBox.Location = new System.Drawing.Point(24, 600);
			this.hintTextBox.Name = "hintTextBox";
			this.hintTextBox.ReadOnly = true;
			this.hintTextBox.Size = new System.Drawing.Size(720, 72);
			this.hintTextBox.TabIndex = 18;
			this.hintTextBox.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 584);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 19;
			this.label2.Text = "Hints to Execute the Sample";
			// 
			// listDeptno
			// 
			this.listDeptno.ItemHeight = 14;
			this.listDeptno.Location = new System.Drawing.Point(136, 16);
			this.listDeptno.Name = "listDeptno";
			this.listDeptno.ScrollAlwaysVisible = true;
			this.listDeptno.Size = new System.Drawing.Size(80, 18);
			this.listDeptno.TabIndex = 20;
			// 
			// DelBtn
			// 
			this.DelBtn.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DelBtn.Location = new System.Drawing.Point(8, 16);
			this.DelBtn.Name = "DelBtn";
			this.DelBtn.Size = new System.Drawing.Size(72, 24);
			this.DelBtn.TabIndex = 20;
			this.DelBtn.Text = "Delete";
			this.DelBtn.Click += new System.EventHandler(this.DelBtn_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.btnNewRec});
			this.groupBox1.Location = new System.Drawing.Point(424, 464);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(224, 48);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.btnUpdate});
			this.groupBox2.Location = new System.Drawing.Point(648, 464);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(96, 48);
			this.groupBox2.TabIndex = 22;
			this.groupBox2.TabStop = false;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.DelBtn});
			this.groupBox3.Location = new System.Drawing.Point(424, 512);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(88, 48);
			this.groupBox3.TabIndex = 23;
			this.groupBox3.TabStop = false;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.label3,
																					this.listDeptno});
			this.groupBox5.Location = new System.Drawing.Point(520, 512);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(224, 48);
			this.groupBox5.TabIndex = 24;
			this.groupBox5.TabStop = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 18);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 16);
			this.label3.TabIndex = 21;
			this.label3.Text = "Valid Deptno Values";
			// 
			// EmpForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(768, 677);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox5,
																		  this.groupBox3,
																		  this.groupBox2,
																		  this.label2,
																		  this.hintTextBox,
																		  this.groupBox4,
																		  this.grpBxConnection,
																		  this.empRichTextBox,
																		  this.empDataGrid,
																		  this.label1,
																		  this.btnInsert,
																		  this.groupBox1});
			this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.MaximizeBox = false;
			this.Name = "EmpForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Employee Management System";
			((System.ComponentModel.ISupportInitialize)(this.empDataGrid)).EndInit();
			this.grpBxConnection.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new EmpForm());
		}

		/*******************************************************************************
		 * The purpose of this method is to establish a database connection based on the 
		 * connection parameters entered in the textboxes and then populate the datagrid 
		 * and generate XML. The "getDBConnection" method of "ConnectionMgr.cs" class is 
		 * used to establish the connection. Methods from the "ManageEmp.cs" class are 
		 * called to populate the datagrid and generate XML.
		 *******************************************************************************/
		private void btnConnect_Click(object sender, System.EventArgs e)
		{			
			if (conStatus == "N")
			{
				// Set cursor to hour glass
				Cursor.Current = Cursors.WaitCursor;

				// Establish connection
				String ConStatus =	ConnectionMgr.getDBConnection
					(txtName.Text,txtPwd.Text,txtConStr.Text);

				// If connection is established
				if (ConStatus == "Connected") 
				{
			    			
					// Populate empDataGrid with data
					DataSet empDataSet = new DataSet();
				    empDataSet = manageEmp.populateEmpDataGrid();
					empDataGrid.SetDataBinding(empDataSet,"Emp_View");

					// Populate empRichTextBox with generated XML
					empRichTextBox.Text = manageEmp.getXmlFromDB();
             
					// Populate Deptno list 
					DataSet deptDataSet = new DataSet();
					deptDataSet = manageEmp.populateDeptno();
					listDeptno.DisplayMember = deptDataSet.Tables[0].Columns["Deptno"].ToString () ;
					listDeptno.DataSource = deptDataSet.Tables["Dept"].DefaultView;	
					listDeptno.Visible = true ; 
			
					// Enable/Disable appropriate UI controls
					empRichTextBox.Enabled = true;
					btnNewRec.Enabled = true;
					listDeptno.Enabled = true;

					btnUpdate.Enabled = false;
					DelBtn.Enabled = false;
                
					lblStatus.Text = "Connected";
					lblStatus.ForeColor = Color.Green;

					// Set btnConnect label to disconnect 
					btnConnect.Text = "Disconnect";

					conStatus = "Y";

					// disable connection information textboxes
					txtName.Enabled = false;
					txtPwd.Enabled = false;
					txtConStr.Enabled = false;

					
					// Text for hintTextBox
					hintTextBox.Text = 
						"To insert a new record - click on the 'Create New Record' button. " + 
						"To update a record - Select a record from the datagrid, make changes to the XML in " +
						"the textbox for the corresponding record and " +
						"click on the 'Update' button. "+
						"To delete a record - Select a record from the datagrid and click on the "+
						"'Delete' button. " +
                        "Empno is a key column, so do not change this. " +
						"View valid values for deptno from the listbox." ;

					// Set cursor to default
					Cursor.Current = Cursors.Default;
				
				} 
			}
			else
			{
				// Set cursor to default
				Cursor.Current = Cursors.Default;

				// If not connected
				// Disable UI controls
				btnNewRec.Enabled = false;
				btnInsert.Enabled = false;
				listDeptno.Enabled = false;
				btnUpdate.Enabled = false;
				empRichTextBox.Enabled = false;
				DelBtn.Enabled = false;

				// Enable connection information textboxes
				txtName.Enabled = true;
				txtPwd.Enabled = true;
				txtConStr.Enabled = true;


				lblStatus.Text = "Not Connected";
				lblStatus.ForeColor = Color.Red;

				btnConnect.Text = "Connect";

				// Once disconnected, clear contents of UI controls
				conStatus = "N";

				empRichTextBox.Text = "";

				// Dept # listbox
				empDataGrid.SetDataBinding(null,"");
				listDeptno.DisplayMember	=null;
				listDeptno.Visible = false; 
				listDeptno.DataSource =  null;

				// Hint message
				hintTextBox.Text = "Enter valid connection details to connect to an Oracle database";
			}
		}
		
		/*************************************************************************
		 * The purpose of this method is to close the Oracle connection and exit
		 * the application.
		 * ***********************************************************************/
		private void btnExit_Click(object sender, System.EventArgs e)
		{
			ConnectionMgr.closeConnection();
			Application.Exit();
		}

		/****************************************************************************
		 * The purpose of this method is to clear the empRichTextBox, and then 
		 * display a sample XML record for insert.
		 ****************************************************************************/
		private void btnNewRec_Click(object sender, System.EventArgs e)
		{
			// Set cursor to hour glass
			Cursor.Current = Cursors.WaitCursor;

			// Clear the contents of empRichTextBox
			empRichTextBox.Text = "";

			// Display a sample XML record
			empRichTextBox.Text = manageEmp.createNewRecord();

			// Disable update and delete buttons during insert the operation
			btnUpdate.Enabled = false;
			DelBtn.Enabled = false;

			// Enable the insert button 
			btnInsert.Enabled = true;
			
			// Update the hint message
			hintTextBox.Text = "Do not change 'Empno', as it is automatically generated. " +
				                         "View valid values for deptno from the listbox. " +
				                            "Click 'Insert' button to create a new record.";

			// Set cursor to default
			Cursor.Current = Cursors.Default;
		}

		/***********************************************************************************
		 * The purpose of this method is to insert a record into the 'Emp_View' XMLType view.
		 * The 'insertEmployee' method of 'ManageEmp' class is called. After insertion 
		 * is successful, the DataGrid and the XML records in the textbox are refreshed.
		 **********************************************************************************/
		private void btnInsert_Click(object sender, System.EventArgs e)
		{
			// Set cursor to hour glass
			Cursor.Current = Cursors.WaitCursor;

			// Insert record using XML data given in the empRichTextBox
			bool result = manageEmp.insertRecord(empRichTextBox.Text);
            
			// If record inserted
			if (result)
			{
				// Refresh the XML records given in the empRichTextBox
				empRichTextBox.Text = manageEmp.getXmlFromDB();
					
				
				// Refresh the DataGrid
				DataSet empDataSet = new DataSet();
				empDataSet = manageEmp.populateEmpDataGrid();
				empDataGrid.SetDataBinding(empDataSet,"Emp_View");

				// Enable the update button
				btnUpdate.Enabled = false;
				DelBtn.Enabled = false;

				btnInsert.Enabled = false;

				// Text for hintTextBox
				hintTextBox.Text = 
					"To insert a new record - click on the 'Create New Record' button. " + 
					"To update a record - Select a record from the datagrid, make changes to the XML in " +
					"the textbox for the corresponding record and " +
					"click on the 'Update' button. "+
					"To delete a record - Select a record from the datagrid and click on the "+
					"'Delete' button. " +
					"Empno is a key column, so do not change this. " +
					"View valid values for deptno from the listbox." ;

				// Set cursor to default
				Cursor.Current = Cursors.Default;
			}
		}
		
		/****************************************************************************
		 * The purpose of this method is to update 'Emp_View', XMLType view table 
		 * using the XML records modified in the empRichTextBox.
		 ***************************************************************************/
		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			// Set cursor to hour glass
			Cursor.Current = Cursors.WaitCursor;

			// Update data using XML data given in empRichTextBox
			bool result = manageEmp.updateRecord(empRichTextBox.Text,curEmployeeid );

			// If data updated successfully
			if (result) 
			{

				// Refresh the XML records given in the empRichTextBox
				empRichTextBox.Text = manageEmp.getXmlFromDB();

				btnUpdate.Enabled = false;
				DelBtn.Enabled = false;
				btnInsert.Enabled = false;

                // Refresh the DataGrid
				DataSet empDataSet = new DataSet();
				empDataSet = manageEmp.populateEmpDataGrid();
				empDataGrid.SetDataBinding(empDataSet,"Emp_View");

				// Text for hintTextBox
				hintTextBox.Text = 
					"To insert a new record - click on the 'Create New Record' button. " + 
					"To update a record - Select a record from the datagrid, make changes to the XML in " +
					"the textbox for the corresponding record and " +
					"click on the 'Update' button. "+
					"To delete a record - Select a record from the datagrid and click on the "+
					"'Delete' button. " +
					"Empno is a key column, so do not change this. " +
					"View valid values for deptno from the listbox." ;

				// Set cursor to default
				Cursor.Current = Cursors.Default;
			}
		}

		/*******************************************************************************
		 * The purpose of this method is to show a delete confirmation dialog box to the
		 * user. If the user confirms the delete, then the record displayed in the 
		 * textbox is deleted from the 'Emp_View'. 
		 * ****************************************************************************/
		private void DelBtn_Click(object sender, System.EventArgs e)
		{
			DialogResult dialogResult;

			// Display Message Box to ask user for confirmation, before deleting record(s)
			dialogResult = MessageBox.Show(this,
				"The record displayed in the Text Box will get deleted. "+
				"Are you sure you want to delete record ?",
				"Delete Record Confirmation", 
				MessageBoxButtons.YesNo);

			// If user confirms delete
			if (dialogResult == DialogResult.Yes) 
			{
				// Set cursor to hour glass
				Cursor.Current = Cursors.WaitCursor;

				// Delete a record from the 'Emp_View' using XML data given in empRichTextBox
				bool result = manageEmp.deleteRecord(empRichTextBox.Text);

				// If data updated successfully
				if (result) 
				{
					// Refresh the XML records given in the empRichTextBox
					empRichTextBox.Text = manageEmp.getXmlFromDB();

					btnUpdate.Enabled = false;
					DelBtn.Enabled = false;
					btnInsert.Enabled =false;
					
					// Refresh the DataGrid
					DataSet empDataSet = new DataSet();
					empDataSet = manageEmp.populateEmpDataGrid();
					empDataGrid.SetDataBinding(empDataSet,"Emp_View");

					// Text for hintTextBox
					hintTextBox.Text = 
						"To insert a new record - click on the 'Create New Record' button. " + 
						"To update a record - Select a record from the datagrid, make changes to the XML in " +
						"the textbox for the corresponding record and " +
						"click on the 'Update' button. "+
						"To delete a record - Select a record from the datagrid and click on the "+
						"'Delete' button. " +
						"Empno is a key column, so do not change this. " +
						"View valid values for deptno from the listbox." ;

					// Set cursor to default
					Cursor.Current = Cursors.Default;
				}
			}
		}
		/*******************************************************************************
		 * The purpose of this method is to select a row in the datagrid on which the 
		 * mouse pointer is navigated currently.
		 *******************************************************************************/
		private void empDataGrid_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) 
		{
			System.Drawing.Point pt = new Point(e.X, e.Y); 
 			DataGrid.HitTestInfo hti = empDataGrid.HitTest(pt); 
 			if(hti.Type == DataGrid.HitTestType.Cell) 
 			{ 
				// Set cursor to hour glass
				Cursor.Current = Cursors.WaitCursor;

 				empDataGrid.CurrentCell = new DataGridCell(hti.Row, hti.Column); 
 				empDataGrid.Select(hti.Row); 

				int rowNum = empDataGrid.CurrentRowIndex;
				object cell1Value = empDataGrid[rowNum, 0]; 
				string empno = cell1Value.ToString();
			    curEmployeeid = empno;
				empRichTextBox.Text = manageEmp.getSelectedEmpRecord(empno);
                
				// Enable the update and delete buttons
				btnUpdate.Enabled = true;
				DelBtn.Enabled = true;

				// Disable insert button
				btnInsert.Enabled = false;

				// Text for hintTextBox
				hintTextBox.Text = 
					"To insert a new record - click on the 'Create New Record' button. " + 
					"To update a record - Select a record from the datagrid, make changes to the XML in " +
					"the textbox for the corresponding record and " +
					"click on the 'Update' button. "+
					"To delete a record - Select a record from the datagrid and click on the "+
					"'Delete' button. " +
					"Empno is a key column, so do not change this. " +
					"View valid values for deptno from the listbox." ;

				// Set cursor to default
				Cursor.Current = Cursors.Default;
 			} 
 		}
    }
}
