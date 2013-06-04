/**************************************************************************
@author  Chandar
@version 1.0
Development Environment        :  MS Visual Studio .Net
Name of the File               :  TAF.cs
Creation/Modification History  :
                                 28-Oct-2002     Created

Overview:
When a connection to an Oracle instance is unexpectedly severed, Transparent
Application Failover (TAF) seamlessly attempts to failover to another 
Oracle instance. Due to the delay that a failover can incur, the application
may wish to be notified by a TAF callback. ODP.NET supports TAF callback 
through the Failover event of the OracleConnection object. To receive TAF
callbacks, an event handler function must be registered with the Failover
event of the OracleConnection object. Also the connection parameter "enlist"
should be set to false for TAF to work.
This sample demonstrates how the TAF Callback event handlers are fired.

When the sample is run, it shows a blank screen and provides user an option
to get products data from database. Session information is changed on
connection object to change the date format. When the "Get Products Data" button
is clicked, the application execution is stopped just before executing the 
query by appropriately setting a BREAKPOINT. Now the user can go to SQL*Plus
client and issue a command like "STARTUP FORCE" to restart the database.
The user can now continue the execution of application. TAF will reconnect
to database and fetch the data while the user will receive messages from
Callback event handler which are displayed in MessageBox and status bar
of application.
The sample also modifies the session information again to change the date
format when the connection is re-established with the database.

Note: Tnsnames.ora should be modified to enable TAF as per the instructions
      given in Readme.html file.
**************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace TAFSample
{
	/// <summary>
	/// Summary description for TAF.
	/// </summary>
	public class TAF : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		//For database connection 
		OracleConnection conn;

        //To fill DataSet and update datasource
		OracleDataAdapter productsAdapter;

		int doRefresh=0;
		private System.Windows.Forms.DataGrid productsDataGrid;
		private DataGridTableStyle productsGridTableStyle;
		private DataGridTextBoxColumn Product_ID;
		private DataGridTextBoxColumn Product_Name;
        private DataGridTextBoxColumn Product_Desc;
		private DataGridTextBoxColumn modificationDate;
		private DataGridTextBoxColumn Price;
		private System.Windows.Forms.Button closeBtn;
		private System.Windows.Forms.Button dataButton;
		private System.Windows.Forms.Label lblStatus;
                                
		//In-Memory cache of data
		DataSet productsDataSet;

		public TAF()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		public static void Main()
		{

			TAF taf = new TAF();
			//Get database connection
			if (taf.getDBConnection())
			{
			  //When this application is run, Form is displayed
			  Application.Run(taf);
			}
		}

		/*******************************************************************
		 * This method displays the details of products from database in a 
		 * DataGrid. After connecting to database and just before executing 
		 * the SQL query to get the product details, a breakpoint is set so
		 * that user can restart the database. TAF takes care of reconnecting
		 * to database and getting the products data. User receives TAF callbacks
		 * Failover takes place.
		 * ******************************************************************/
		public void displayProducts()
		{
			try
			{
				//Instantiate OracleDataAdapter to create DataSet
				productsAdapter = new OracleDataAdapter();

				//Fetch Product Details
				productsAdapter.SelectCommand = new OracleCommand("SELECT " +
					"Product_ID , " +
					"Product_Name , " +
					"Product_Desc , " +
					"Price, " +
					"to_char(Modification_Date) Modification_Date " +
					"FROM Products",conn); 
				
				//Instantiate DataSet object
				productsDataSet = new DataSet("productsDataSet");

				//Fill the DataSet with data from 'Products' database table
				// Put a breakpoint at the next line, go to SQL PLUS, connect
				//to the your database  and issue a command such as 
                //"startup force".You should now receive callbacks
				//Come back and continue application execution
				productsAdapter.Fill(productsDataSet, "Products");

				//Associate DataGrid with DataSet to display data
				productsDataGrid.SetDataBinding(productsDataSet,"Products"); 
				lblStatus.Text="Selected products from database";
				
				
			}
			catch(Exception ex)
			{
				MessageBox.Show("error"+ex);
			}
		}
        
		/**********************************************************************
		 * This method is FailOver Callback method called when the 
		 * connection to the database is severed. It is called several 
		 * times in the process of re-establishing connection to the 
		 * same or standby database.
		 **********************************************************************/ 
		public FailoverReturnCode OnFailover(object sender, OracleFailoverEventArgs eventArgs)
		{
		// check the Failover event that occurred and display appropriate message
		switch(eventArgs.FailoverEvent)
		{
            // when failover begins
			case FailoverEvent.Begin:
			{
				MessageBox.Show("Callback method called :Failover Begin");
				lblStatus.Text="Callback method called :Failover Begin. Trying to reconnect, Please wait...";
			
			    break;
			}
            // when failover is aborted
			case FailoverEvent.Abort:
			{	
				MessageBox.Show("Callback method called :Failover Aborted");
                lblStatus.Text="Callback method called :Failover Aborted";
				break;
			}
            // when failover is complete
			case FailoverEvent.End:
			{
				MessageBox.Show("Callback method called :Failover End");
                
				// call method to set session information on re-established
				// connection
				alterSessionInfo();
				lblStatus.Text="Callback method called :Failover End";
				break;
			}
            //when error occurs while reconnecting
			case FailoverEvent.Error:
			{
				lblStatus.Text="Failover Error -Sleeping and Retrying to connect to database. Please wait... ";
			    
				//check to do form Refresh only once
				if(doRefresh==0)
					this.Refresh();
				doRefresh=1;
				return FailoverReturnCode.Retry;
			}
            // when reautentication is required during Failover 
			case FailoverEvent.Reauth:
			{
				MessageBox.Show("Callback method called :Failover reauthenticating");
				lblStatus.Text="Callback method called :Failover reauthenticating";
				break;
			}
			default:
			{				
				MessageBox.Show("Bad Failover");
				lblStatus.Text="Bad Failover";
				break;
			}
		}
		return FailoverReturnCode.Success;
		}

	   /*******************************************************************
	   * The purpose of this method is to get the database connection 
	   * using the parameters given in ConnectionParams.cs.
	   * The connection parameter enlist is set to false for the TAF to
	   * work properly.
	   * Note: Replace the datasource parameter with your datasource value
	   * in ConnectionParams.cs file.
	   ********************************************************************/
		private Boolean getDBConnection()
		{
			try
			{
				lblStatus.Text="Connecting to database";
				//Connection Information        
				string connectionString = 
                                        
					//username
					"User Id=" + ConnectionParams.Username +

					//password
					";Password=" + ConnectionParams.Password +
         
					//set enlist parameter to false. Required for TAF
					";enlist=false" +

					//replace with your datasource value (TNSnames)
					";Data Source=" + ConnectionParams.Datasource;
					

                                        
				//Connection to datasource, using connection parameters given above
				conn = new OracleConnection(connectionString);
                
				//Open database connection
				conn.Open();

				lblStatus.Text="Connected to the database. Click 'Get Products Data' to fetch products data ";
			    
				// alter date format for the session 
				this.alterSessionInfo();
				conn.Failover += new OracleFailoverEventHandler(OnFailover);
				
				return true;
			}
				// catch exception when error in connecting to database occurs
			catch (Exception ex) 
			{
				//Display error message
				MessageBox.Show(ex.ToString());
				return false;
			}
		}
        
		/******************************************************************
		 * This method is called to alter session information on database
		 * connection. The method modifies the format in which date data
		 * is received from database.
		 * Note:This method is re-executed when TAF re-establishes the
		 * database connection.
		 * ****************************************************************/
		private void alterSessionInfo()
		{   
			// obtain default session settings
			OracleGlobalization oraGlob =conn.GetSessionInfo();

			// alter the date format for current session
			oraGlob.DateFormat ="Dd Month yyyy";

			// set the session information using connection object
			conn.SetSessionInfo(oraGlob);
		}

	   /*******************************************************************
	   *This method is called when "Get Product Data" button is clicked
	   *to fetch product data from database.
	   ********************************************************************/
		private void dataButton_Click(object sender, System.EventArgs e)
		{
          //Calling 'displayProducts' method to populate DataGrid from database
		  displayProducts();

		}


	   	/************************************************************
		 * This method is called when "Close" button on the form is 
		 * clicked. It closes the database connection and exits the 
		 * Application
		 ************************************************************/ 
		private void closeBtn_Click(object sender, System.EventArgs e)
		{
			// close database connection
			conn.Close();

			//close form and application
			this.Close();
			Application.Exit();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )

			{
				if(components != null)
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
			this.productsDataGrid = new System.Windows.Forms.DataGrid();
			this.productsGridTableStyle = new System.Windows.Forms.DataGridTableStyle();
			this.Product_ID = new System.Windows.Forms.DataGridTextBoxColumn();
			this.Product_Name = new System.Windows.Forms.DataGridTextBoxColumn();
			this.Product_Desc = new System.Windows.Forms.DataGridTextBoxColumn();
			this.Price = new System.Windows.Forms.DataGridTextBoxColumn();
			this.modificationDate = new System.Windows.Forms.DataGridTextBoxColumn();
			this.closeBtn = new System.Windows.Forms.Button();
			this.dataButton = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.productsDataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// productsDataGrid
			// 
			this.productsDataGrid.CaptionFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.productsDataGrid.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.productsDataGrid.CaptionText = "List of Products";
			this.productsDataGrid.DataMember = "";
			this.productsDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.productsDataGrid.Location = new System.Drawing.Point(48, 48);
			this.productsDataGrid.Name = "productsDataGrid";
			this.productsDataGrid.ReadOnly = true;
			this.productsDataGrid.Size = new System.Drawing.Size(543, 240);
			this.productsDataGrid.TabIndex = 4;
			this.productsDataGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																										 this.productsGridTableStyle});
			// 
			// productsGridTableStyle
			// 
			this.productsGridTableStyle.DataGrid = this.productsDataGrid;
			this.productsGridTableStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																													 this.Product_ID,
																													 this.Product_Name,
																													 this.Product_Desc,
																													 this.Price,
																													 this.modificationDate});
			this.productsGridTableStyle.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.productsGridTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.productsGridTableStyle.MappingName = "Products";
			this.productsGridTableStyle.RowHeadersVisible = false;
			// 
			// Product_ID
			// 
			this.Product_ID.Format = "";
			this.Product_ID.FormatInfo = null;
			this.Product_ID.HeaderText = "ID";
			this.Product_ID.MappingName = "Product_ID";
			this.Product_ID.Width = 76;
			// 
			// Product_Name
			// 
			this.Product_Name.Format = "";
			this.Product_Name.FormatInfo = null;
			this.Product_Name.HeaderText = "Product Name";
			this.Product_Name.MappingName = "Product_Name";
			this.Product_Name.Width = 90;
			// 
			// Product_Desc
			// 
			this.Product_Desc.Format = "";
			this.Product_Desc.FormatInfo = null;
			this.Product_Desc.HeaderText = "Description";
			this.Product_Desc.MappingName = "Product_Desc";
			this.Product_Desc.Width = 195;
			// 
			// Price
			// 
			this.Price.Format = "";
			this.Price.FormatInfo = null;
			this.Price.HeaderText = "Price($)";
			this.Price.MappingName = "Price";
			this.Price.Width = 75;
			// 
			// modificationDate
			// 
			this.modificationDate.Format = "";
			this.modificationDate.FormatInfo = null;
			this.modificationDate.HeaderText = "Last Modified on";
			this.modificationDate.MappingName = "Modification_Date";
			this.modificationDate.Width = 103;
			// 
			// closeBtn
			// 
			this.closeBtn.BackColor = System.Drawing.SystemColors.Control;
			this.closeBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.closeBtn.ForeColor = System.Drawing.Color.Black;
			this.closeBtn.Location = new System.Drawing.Point(520, 304);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(72, 24);
			this.closeBtn.TabIndex = 5;
			this.closeBtn.Text = "Close";
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// dataButton
			// 
			this.dataButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataButton.Location = new System.Drawing.Point(368, 304);
			this.dataButton.Name = "dataButton";
			this.dataButton.Size = new System.Drawing.Size(136, 24);
			this.dataButton.TabIndex = 6;
			this.dataButton.Text = "Get Products data";
			this.dataButton.Click += new System.EventHandler(this.dataButton_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lblStatus.Location = new System.Drawing.Point(0, 352);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(632, 32);
			this.lblStatus.TabIndex = 7;
			this.lblStatus.Text = "label1";
			// 
			// TAF
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 381);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblStatus,
																		  this.dataButton,
																		  this.closeBtn,
																		  this.productsDataGrid});
			this.Name = "TAF";
			this.Text = "Transparent Application Failover Sample";
			((System.ComponentModel.ISupportInitialize)(this.productsDataGrid)).EndInit();
			this.ResumeLayout(false);

		}
                #endregion


                
	}
}

