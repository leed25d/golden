/**************************************************************************
* @author                        :  Chandar
* @version                       :  1.0
* Development Environment        :  Microsoft Visual Studio .Net
* Name of the File               :  Products.cs
* Creation/Modification History  :
                                  12-Oct-2002     Created

* Overview:
* This sample aims to demonstrate the Oracle Globalization support using
* ODP.NET for interacting with Oracle database through C# applications.
* The NLS parameters such as NLS_LANGUAGE, NLS_TERRITORY, NLS_CURRENCY etc. 
* can be set for the thread and session when data is retrieved in a DataSet using 
* OracleDataAdapter. These NLS settings  will then be used for retrieving and 
* storing culture sensitive data into the database. Safe Type Mapping is 
* used to retrieve the column data as String types rather than default .NET
* types so that NLS settings modified on the current thread take effect.
* The default .NET types use client's NLS settings and do not used current
* thread's or session's settings.
* For the data that is retrieved as String type from database itself(for example using
* TO_CHAR function in SQL statement ), session's culture settings are used to
* convert the data appropriately.


* This class retrieves all the products from database and displays them in 
* a Data Grid. The method setGlobalizationSettings() changes the NLS settings 
* for the current thread and session.The user can select a particular product
* and view its details for updation.
*****************************************************************************/


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Oracle.DataAccess.Client;

namespace OracleGlobalizationSample  // namespace for sample application
{
	/// <summary>
	/// Definition of Products class.
	/// </summary>
	public class Products : System.Windows.Forms.Form
	{
        // defines form controls
        private System.Windows.Forms.DataGrid grdProducts;
		private DataGridTableStyle productsGridTableStyle;
		private DataGridTextBoxColumn Product_ID;
		private DataGridTextBoxColumn Product_Name;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Button cmdShowDetails;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Products()
		{			
     		InitializeComponent();
		}

				
		/*******************************************************************
		* The method sets the NLS parameters for the current thread and session.
		* These NLS values will be used while retrieving and updating the data into
		* the database. 
		********************************************************************/
		private bool setGlobalizationParams()
		{
			try
			{
				// Get the default thread setting 
				OracleGlobalization threadGlob =OracleGlobalization.GetThreadInfo();
		
				//modify the NLS_LANGUAGE parameter
				threadGlob.Language = Config.language;
		
				// modify the NLS_TERRITORY parameter
				threadGlob.Territory= Config.territory;
		    
				// modify the NLS_DATE_FORMAT parameter
				threadGlob.DateFormat=Config.dateformat;
		
				// set the modified NLS parameters for thread
				//Thread's NLS settings are used by any data retrieved
				//as .NET String type.
				OracleGlobalization.SetThreadInfo(threadGlob);     
                
				//Get session's default NLS settings
				OracleGlobalization sessionGlob =ConnectionManager.conn.GetSessionInfo();
				
				// modify the NLS_TERRITORY parameter
				sessionGlob.Territory=Config.territory;

				// set the modified NLs parameters for session
				// Session's NLS settings are used by data retrieved using
				//TO_CHAR function used in SELECT statements.
				ConnectionManager.conn.SetSessionInfo(sessionGlob);     
			}
			catch(Exception e)
			{ MessageBox.Show(e.Message);
				return false;
			}
			return true;
		}

		/*******************************************************************
		 * This method retrieves all the products from database and displays 
		 * them in a Data Grid.
		 *******************************************************************/
		public  bool displayProducts()
		{
			try
			{				
				//Instantiate OracleDataAdapter to create DataSet
				OracleDataAdapter productsAdapter = new OracleDataAdapter();
                
				//Define the Command with Select statement
				productsAdapter.SelectCommand = new OracleCommand(
				     "SELECT Product_ID ,Product_Name FROM Products",
				     ConnectionManager.conn); 
  				
				//Instantiate DataSet object
				DataSet productsDataSet = new DataSet("productsDataSet");

				//Fill the DataSet with data from 'Products' database table
				productsAdapter.Fill(productsDataSet, "Products");

				//setting 'productsDataSet' as  the datasouce and 'Products' table
				//as the table to which the DataGrid is to be bound.
				grdProducts.SetDataBinding(productsDataSet,"Products"); 

				//enable edit button only if Products table contains data
				if(productsDataSet.Tables["Products"].Rows.Count!=0)
					cmdShowDetails.Enabled=true;

			}
			catch(Exception ex)
			{
				//Display error message
				MessageBox.Show(ex.ToString());				
				return false;
			}
			return true;
		}

		

		/**********************************************************************
		 * This method is called when "Edit Product Details" button on the form 
		 * is clicked. It gets the details of selected product and displays them 
		 * on form.
		 *********************************************************************/
		private void cmdEditProductDetails_Click(object sender, System.EventArgs e)
		{
			string Id = grdProducts[grdProducts.CurrentRowIndex,0].ToString();
			int prodID = int.Parse(Id);
			ProductDetails details =new ProductDetails();
			bool value = details.getProductDetails(prodID);
			// show the form
			if(value)
				details.ShowDialog();
			else 
				details.Dispose();
			

		}

		/************************************************************
		 * This method is called when "Close" button on the form is 
		 * clicked. It closes the current form
		 ************************************************************/ 
		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			//close database connection
			ConnectionManager.closeConnection();
			
			//close the form 
			this.Close();
			
			//exit application
			Application.Exit();
		}

		/***********************************************************
		* This method is the starting point of the application.
		***********************************************************/
		static void Main() 
		{
			// Instantiate start up form
			Products products = new Products();
			// get database connection
			bool connected = ConnectionManager.getDBConnection();
			if(connected)
			{
				// set globalization parameters for current thread
				bool check = products.setGlobalizationParams();
				if(!check)
				{
					// close the database connection
					ConnectionManager.closeConnection();
					return;
				}
				// display products from database
				bool value = products.displayProducts();
				//run the application
				if(!value)
				{
					// close the database connection
					ConnectionManager.closeConnection();
					return;
				}
					Application.Run(products);
			}
			
		}
		
		/***********************************************************
		* This method is called when the form is activated. If the 
		* user modified any product details, the latest product info
		* is taken from database and displayed in the DataGrid.
		***********************************************************/
		private void Products_Activated(object sender, System.EventArgs e)
		{
			// check if any product was updated
			if (ProductDetails.updated)
			{
				//display products from database
				this.displayProducts();
			}
		}
		
		/// <summary>
				/// Clean up any resources being used.
				/// </summary>
				protected override void Dispose( bool disposing )
				{
					// close the database connection
					ConnectionManager.closeConnection();
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
					this.grdProducts = new System.Windows.Forms.DataGrid();
					this.productsGridTableStyle = new System.Windows.Forms.DataGridTableStyle();
					this.Product_ID = new System.Windows.Forms.DataGridTextBoxColumn();
					this.Product_Name = new System.Windows.Forms.DataGridTextBoxColumn();
					this.cmdShowDetails = new System.Windows.Forms.Button();
					this.cmdClose = new System.Windows.Forms.Button();
					((System.ComponentModel.ISupportInitialize)(this.grdProducts)).BeginInit();
					this.SuspendLayout();
					// 
					// grdProducts
					// 
					this.grdProducts.BackgroundColor = System.Drawing.SystemColors.Window;
					this.grdProducts.CaptionBackColor = System.Drawing.SystemColors.Control;
					this.grdProducts.CaptionFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
					this.grdProducts.CaptionForeColor = System.Drawing.SystemColors.ControlText;
					this.grdProducts.CaptionText = "Products";
					this.grdProducts.DataMember = "";
					this.grdProducts.HeaderForeColor = System.Drawing.SystemColors.ControlText;
					this.grdProducts.Location = new System.Drawing.Point(48, 40);
					this.grdProducts.Name = "grdProducts";
					this.grdProducts.ParentRowsBackColor = System.Drawing.SystemColors.Window;
					this.grdProducts.PreferredColumnWidth = 124;
					this.grdProducts.ReadOnly = true;
					this.grdProducts.RowHeadersVisible = false;
					this.grdProducts.SelectionBackColor = System.Drawing.SystemColors.Control;
					this.grdProducts.Size = new System.Drawing.Size(240, 184);
					this.grdProducts.TabIndex = 0;
					this.grdProducts.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																											this.productsGridTableStyle});
					// 
					// productsGridTableStyle
					// 
					this.productsGridTableStyle.DataGrid = this.grdProducts;
					this.productsGridTableStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																															 this.Product_ID,
																															 this.Product_Name});
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
					this.Product_Name.Width = 143;
					// 
					// cmdShowDetails
					// 
					this.cmdShowDetails.Enabled = false;
					this.cmdShowDetails.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
					this.cmdShowDetails.Location = new System.Drawing.Point(80, 256);
					this.cmdShowDetails.Name = "cmdShowDetails";
					this.cmdShowDetails.Size = new System.Drawing.Size(128, 24);
					this.cmdShowDetails.TabIndex = 1;
					this.cmdShowDetails.Text = "Edit Product Details";
					this.cmdShowDetails.Click += new System.EventHandler(this.cmdEditProductDetails_Click);
					// 
					// cmdClose
					// 
					this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
					this.cmdClose.Location = new System.Drawing.Point(216, 256);
					this.cmdClose.Name = "cmdClose";
					this.cmdClose.Size = new System.Drawing.Size(72, 24);
					this.cmdClose.TabIndex = 2;
					this.cmdClose.Text = "Close";
					this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
					// 
					// Products
					// 
					this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
					this.ClientSize = new System.Drawing.Size(336, 301);
					this.Controls.AddRange(new System.Windows.Forms.Control[] {
																				  this.cmdClose,
																				  this.cmdShowDetails,
																				  this.grdProducts});
					this.MinimizeBox = false;
					this.Name = "Products";
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "Oracle Globalization Support Sample";
					this.Activated += new System.EventHandler(this.Products_Activated);
					((System.ComponentModel.ISupportInitialize)(this.grdProducts)).EndInit();
					this.ResumeLayout(false);

				}
		#endregion

		
	}
}
