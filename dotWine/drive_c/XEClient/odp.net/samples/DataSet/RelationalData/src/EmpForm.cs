/**************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .NET
Name of the File               :  EmpForm.cs
Creation/Modification History  :
                                  16-July-2003     Created

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

namespace RelationalDataSample
{
	/// <summary>
	/// Demonstrates how to retrieve relational data from database as XML 
	/// using ODP.NET and how to insert/update/delete relational data using XML.
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
		private System.Windows.Forms.DataGridTextBoxColumn empno;
		private System.Windows.Forms.DataGridTextBoxColumn ename;
		private System.Windows.Forms.DataGridTextBoxColumn sal;
		private System.Windows.Forms.DataGridTextBoxColumn deptno;
		public System.Windows.Forms.RichTextBox empRichTextBox;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnNewRec;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListBox listDeptno;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RichTextBox hintTextBox;

		// Create instance of class that contains operational methods
		private ManageEmp  manageEmp = new ManageEmp();
		private System.Windows.Forms.Button delBtn;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox5;

		private String conStatus = "N";

				
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
			delBtn.Enabled = false;
			listDeptno.Enabled = false;
			btnUpdate.Enabled = false;

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
			this.label1 = new System.Windows.Forms.Label();
			this.empDataGrid = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.empno = new System.Windows.Forms.DataGridTextBoxColumn();
			this.ename = new System.Windows.Forms.DataGridTextBoxColumn();
			this.sal = new System.Windows.Forms.DataGridTextBoxColumn();
			this.deptno = new System.Windows.Forms.DataGridTextBoxColumn();
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listDeptno = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.hintTextBox = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.delBtn = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.empDataGrid)).BeginInit();
			this.grpBxConnection.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(184, 8);
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
			this.empDataGrid.Location = new System.Drawing.Point(24, 104);
			this.empDataGrid.Name = "empDataGrid";
			this.empDataGrid.ReadOnly = true;
			this.empDataGrid.Size = new System.Drawing.Size(320, 368);
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
																												  this.sal,
																												  this.deptno});
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "emp";
			this.dataGridTableStyle1.ReadOnly = true;
			// 
			// empno
			// 
			this.empno.Format = "";
			this.empno.FormatInfo = null;
			this.empno.HeaderText = "Emp No";
			this.empno.MappingName = "empno";
			this.empno.Width = 60;
			// 
			// ename
			// 
			this.ename.Format = "";
			this.ename.FormatInfo = null;
			this.ename.HeaderText = "Name";
			this.ename.MappingName = "ename";
			this.ename.Width = 95;
			// 
			// sal
			// 
			this.sal.Format = "";
			this.sal.FormatInfo = null;
			this.sal.HeaderText = "Salary";
			this.sal.MappingName = "sal";
			this.sal.Width = 62;
			// 
			// deptno
			// 
			this.deptno.Format = "";
			this.deptno.FormatInfo = null;
			this.deptno.HeaderText = "Dept No";
			this.deptno.MappingName = "deptno";
			this.deptno.Width = 62;
			// 
			// empRichTextBox
			// 
			this.empRichTextBox.Location = new System.Drawing.Point(384, 104);
			this.empRichTextBox.Name = "empRichTextBox";
			this.empRichTextBox.Size = new System.Drawing.Size(312, 368);
			this.empRichTextBox.TabIndex = 3;
			this.empRichTextBox.Text = "";
			// 
			// btnInsert
			// 
			this.btnInsert.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnInsert.Location = new System.Drawing.Point(544, 504);
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
			this.grpBxConnection.Location = new System.Drawing.Point(8, 40);
			this.grpBxConnection.Name = "grpBxConnection";
			this.grpBxConnection.Size = new System.Drawing.Size(704, 56);
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
			this.lblStatus.Location = new System.Drawing.Point(592, 32);
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
			this.btnNewRec.Location = new System.Drawing.Point(392, 504);
			this.btnNewRec.Name = "btnNewRec";
			this.btnNewRec.Size = new System.Drawing.Size(144, 24);
			this.btnNewRec.TabIndex = 12;
			this.btnNewRec.Text = "Create New Record";
			this.btnNewRec.Click += new System.EventHandler(this.btnNewRec_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(384, 488);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(224, 48);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			// 
			// listDeptno
			// 
			this.listDeptno.ItemHeight = 14;
			this.listDeptno.Location = new System.Drawing.Point(144, 24);
			this.listDeptno.Name = "listDeptno";
			this.listDeptno.Size = new System.Drawing.Size(64, 18);
			this.listDeptno.TabIndex = 10;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(488, 560);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(136, 16);
			this.label9.TabIndex = 9;
			this.label9.Text = "Valid  Deptno Values";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.listDeptno});
			this.groupBox3.Location = new System.Drawing.Point(480, 536);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(216, 48);
			this.groupBox3.TabIndex = 16;
			this.groupBox3.TabStop = false;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.richTextBox1});
			this.groupBox4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox4.ForeColor = System.Drawing.Color.Black;
			this.groupBox4.Location = new System.Drawing.Point(24, 488);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(336, 96);
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
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.richTextBox1.Size = new System.Drawing.Size(320, 72);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "* Retrieve relational data as XML from an Oracle8i (or higher) database server in" +
				"to .NET\n\n* Insert/update/delete records in the database as XML";
			// 
			// hintTextBox
			// 
			this.hintTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.hintTextBox.ForeColor = System.Drawing.Color.Blue;
			this.hintTextBox.Location = new System.Drawing.Point(24, 608);
			this.hintTextBox.Name = "hintTextBox";
			this.hintTextBox.ReadOnly = true;
			this.hintTextBox.Size = new System.Drawing.Size(672, 80);
			this.hintTextBox.TabIndex = 18;
			this.hintTextBox.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 592);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 19;
			this.label2.Text = "Hints to Execute the Sample";
			// 
			// delBtn
			// 
			this.delBtn.Location = new System.Drawing.Point(400, 552);
			this.delBtn.Name = "delBtn";
			this.delBtn.Size = new System.Drawing.Size(64, 24);
			this.delBtn.TabIndex = 20;
			this.delBtn.Text = "Delete";
			this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnUpdate.Location = new System.Drawing.Point(624, 504);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(64, 24);
			this.btnUpdate.TabIndex = 11;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(384, 536);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(96, 48);
			this.groupBox2.TabIndex = 23;
			this.groupBox2.TabStop = false;
			// 
			// groupBox5
			// 
			this.groupBox5.Location = new System.Drawing.Point(616, 488);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(80, 48);
			this.groupBox5.TabIndex = 24;
			this.groupBox5.TabStop = false;
			// 
			// EmpForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(720, 693);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.empRichTextBox,
																		  this.label2,
																		  this.hintTextBox,
																		  this.groupBox4,
																		  this.btnNewRec,
																		  this.grpBxConnection,
																		  this.btnInsert,
																		  this.empDataGrid,
																		  this.label1,
																		  this.groupBox1,
																		  this.label9,
																		  this.groupBox3,
																		  this.btnUpdate,
																		  this.delBtn,
																		  this.groupBox2,
																		  this.groupBox5});
			this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.MaximizeBox = false;
			this.Name = "EmpForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Employees Management System";
			((System.ComponentModel.ISupportInitialize)(this.empDataGrid)).EndInit();
			this.grpBxConnection.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
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
		 * used to establish connection. Methods from "ManageEmp.cs" class are called
		 * to populate datagrid and generate XML.
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
					empDataGrid.SetDataBinding(empDataSet,"Emp");

					// Populate empRichTextBox with generated XML
					empRichTextBox.Text = manageEmp.generateEmpXml();
             
					// Populate Deptno list 
					DataSet deptDataSet = new DataSet();
					deptDataSet = manageEmp.populateDeptno();
					listDeptno.DisplayMember = deptDataSet.Tables[0].Columns["Deptno"].ToString () ;
					listDeptno.DataSource = deptDataSet.Tables["Dept"].DefaultView;	
					listDeptno.Visible = true ; 
			
					// Enable UI controls
					empRichTextBox.Enabled = true;
					btnNewRec.Enabled = true;
					listDeptno.Enabled = true;
					btnUpdate.Enabled = true;
					delBtn.Enabled = true;
					                
					// Connection status
					lblStatus.Text = "Connected";
					lblStatus.ForeColor = Color.Green;

					// Set btnConnect label to disconnect 
					btnConnect.Text = "Disconnect";

					conStatus = "Y";

					// Disable connection information textboxes
					txtName.Enabled = false;
					txtPwd.Enabled = false;
					txtConStr.Enabled = false;

					
					// Text for hintTextBox
					hintTextBox.Text = 
						"To insert a new record - click on the 'Create New Record' button. " + 
						"To update the records - modify XML data and click on the 'Update' "+
						"button. Empno is a key column, so do not change this. " +
						"View valid values for deptno from the listbox. " +
						"To delete record - select a record from the datagrid and "+
						"click on the 'Delete' button. " ;

					// Set cursor to default
					Cursor.Current = Cursors.Default;
				
				} 
			}
			else
			{
				// Set cursor to default
				Cursor.Current = Cursors.Default ;

				// If not connected
				// Disable UI controls
				btnNewRec.Enabled = false;
				btnInsert.Enabled = false;
				delBtn.Enabled = false;
				listDeptno.Enabled = false;
				btnUpdate.Enabled = false;
				empRichTextBox.Enabled = false;

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

				empDataGrid.SetDataBinding(null,"");
				listDeptno.DisplayMember = null;
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
		 * The purpose of this method is to clear empRichTextBox, and then display a
		 * sample XML record for insert.
		 ****************************************************************************/
		private void btnNewRec_Click(object sender, System.EventArgs e)
		{
			// Clear the contents of empRichTextBox
			empRichTextBox.Text = "";

			// Display a sample XML record
			empRichTextBox.Text = manageEmp.createNewRecord();

			// Disable other buttons during insert operation
			btnUpdate.Enabled = false;
			delBtn.Enabled = false;
		
			// Enable insert button 
			btnInsert.Enabled = true;

			// Hint message
			hintTextBox.Text = "Do not change 'Empno', as it is automatically generated. " +
				                         "View valid values for deptno from the listbox. " +
				                           "Click 'Insert' button to create a new record.";
		}

		/********************************************************************************
		 * The purpose of this method is to insert a record into 'Emp' table using XML.
		 * The 'insertEmployee' method of 'ManageEmp' class is called. After insertion 
		 * is successful, the DataGrid and the XML records in the textbox are refreshed.
		 ********************************************************************************/
		private void btnInsert_Click(object sender, System.EventArgs e)
		{

			// Set cursor to hour glass
			Cursor.Current = Cursors.WaitCursor;

			// Insert record using XML data given in the empRichTextBox
			bool result = manageEmp.insertEmployee(empRichTextBox.Text);
            
			// If record inserted
			if (result)
			{
				// Refresh the XML records given in the empRichTextBox
				empRichTextBox.Text = manageEmp.generateEmpXml();
				
				// Refresh the DataGrid
				DataSet empDataSet = new DataSet();
				empDataSet = manageEmp.populateEmpDataGrid();
				empDataGrid.SetDataBinding(empDataSet,"Emp");

				// Enable/disable appropriate buttons
				btnUpdate.Enabled = true;
				delBtn.Enabled = true;
				btnInsert.Enabled = false;

				// Text for hintTextBox
				hintTextBox.Text = 
					"To insert a new record - click on the 'Create New Record' button. " + 
					"To update the records - modify XML data and click on the 'Update' "+
					"button. Empno is a key column, so do not change this. " +
					"View valid values for deptno from the listbox. " +
					"To delete record - select a record from the datagrid and "+
					"click on the 'Delete' button. " ;

				// Set cursor to default
				Cursor.Current = Cursors.Default;
						
			}
		}
		
		/****************************************************************************
		 * The purpose of this method is to update 'Emp' table using the XML records
		 * modified in the empRichTextBox.
		 ***************************************************************************/
		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			// Set cursor to hour glass
			Cursor.Current = Cursors.WaitCursor;

			// Update 'Emp' table data using XML data given in empRichTextBox
			bool result = manageEmp.updateEmployees(empRichTextBox.Text);

			// If data updated successfully
			if (result) 
			{
				// Refresh the XML records given in the empRichTextBox
				empRichTextBox.Text = manageEmp.generateEmpXml();

                // Refresh the DataGrid
				DataSet empDataSet = new DataSet();
				empDataSet = manageEmp.populateEmpDataGrid();
				empDataGrid.SetDataBinding(empDataSet,"Emp");

				// Text for hintTextBox
				hintTextBox.Text = 
					"To insert a new record - click on the 'Create New Record' button. " + 
					"To update the records - modify XML data and click on the 'Update' "+
					"button. Empno is a key column, so do not change this. " +
					"View valid values for deptno from the listbox. " +
					"To delete record - select a record from the datagrid and "+
					"click on the 'Delete' button. " ;

				// Set cursor to default
				Cursor.Current = Cursors.Default;
			}
		}

		/*********************************************************************************
		 * The purpose of this method is to delete record(s) from 'Emp' table. 
		 * The user can select a particular record for deletion or delete all records.
		 *********************************************************************************/
		private void delBtn_Click(object sender, System.EventArgs e)
		{
			DialogResult dialogResult;

			// Display Message Box to ask user for confirmation, before deleting record(s)
			dialogResult = MessageBox.Show(this,
				                           "All the record(s) displayed in the Text Box will get deleted. "+
				                           "Are you sure you want to delete record(s)?",
				                           "Delete Record(s) Confirmation", 
				                           MessageBoxButtons.YesNo);

			// If user confirms delete
			if (dialogResult == DialogResult.Yes) 
			{
				// Set cursor to hour glass
				Cursor.Current = Cursors.WaitCursor;
				
				// Call deleteEmployees method of ManageEmp class for delete
          		bool result = manageEmp.deleteEmployees(empRichTextBox.Text);
			
				// If record(s) deleted successfully
				if (result)
				{

					// Refresh the DataGrid
					DataSet empDataSet = new DataSet();
					empDataSet = manageEmp.populateEmpDataGrid();
					empDataGrid.SetDataBinding(empDataSet,"Emp");

					// Refresh the XML records given in the empRichTextBox
					empRichTextBox.Text = manageEmp.generateEmpXml();

					// Text for hintTextBox
					hintTextBox.Text = 
						"To insert a new record - click on the 'Create New Record' button. " + 
						"To update the records - modify XML data and click on the 'Update' "+
						"button. Empno is a key column, so do not change this. " +
						"View valid values for deptno from the listbox. " +
						"To delete record - select a record from the datagrid and "+
						"click on the 'Delete' button. " ;

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
				empDataGrid.CurrentCell = new DataGridCell(hti.Row, hti.Column); 
				empDataGrid.Select(hti.Row); 

				int rowNum = empDataGrid.CurrentRowIndex;
				object cell1Value = empDataGrid[rowNum, 0]; 
				string empno = cell1Value.ToString();
				empRichTextBox.Text = manageEmp.getSelectedEmpRecord(empno);

				// Enable/disable appropriate buttons
				btnUpdate.Enabled = true;
				delBtn.Enabled = true;
				btnInsert.Enabled = false;

				// Text for hintTextBox
				hintTextBox.Text = 
					"To insert a new record - click on the 'Create New Record' button. " + 
					"To update the records - modify XML data and click on the 'Update' "+
					"button. Empno is a key column, so do not change this. " +
					"View valid values for deptno from the listbox. " +
					"To delete record - select a record from the datagrid and "+
					"click on the 'Delete' button. " ;
				} 
		}
    }
}
