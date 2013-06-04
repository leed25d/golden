/**************************************************************************
* @author                        :  Chandar
* @version 1.0
* Development Environment        :  Microsoft Visual Studio .Net
* Name of the File               :  Products.cs
* Creation/Modification History  :
                                  12-Oct-2002     Created

* Overview:
* This sample application aims to demonstrate the Safe Type Mapping 
* feature of ODP.NET . Safe Type Mapping can be used for database columns
* with high precision where the precision loss is to be avoided when populating 
* a DataSet with .NET type representations of Oracle data through 
* OracleDataAdapter. When Safe Type Mapping is used for a database column, 
* the column data is retrieved as String or byte array as against
* the default .NET type. This ensures that precision of data is retained.

* This class retrieves all the products from database and displays them in 
* a Data Grid. The user can select a particular product and view its details
* for updation.
*****************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Oracle.DataAccess.Client;

namespace SafeTypeMapping  // namespace for sample application
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
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioYes;
		private System.Windows.Forms.RadioButton radioNo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Products()
		{			
     		InitializeComponent();
		}

		
		/*******************************************************************
		 * This method retrieves all the products from database and displays 
		 * them in a Data Grid.
		 * ****************************************************************
		 */
		public bool displayProducts()
		{
			try
			{				
				//Instantiate OracleDataAdapter to create DataSet
				OracleDataAdapter productsAdapter = new OracleDataAdapter();
                
				//Define the Command with Select statement
				productsAdapter.SelectCommand = new OracleCommand("SELECT " +
					               "Product_ID ,Product_Name FROM Products" +
					               " WHERE category='Jewel'",
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
		 * This method is called when "Edit Product Details" button on the form is
		 * clicked. It gets the details of selected product and displays them 
		 * on form.
		 *********************************************************************/
		private void cmdEditProductDetails_Click(object sender, System.EventArgs e)
		{
			try
			{
				// get the product Id from the row selected in Data Grid
				string Id = grdProducts[grdProducts.CurrentRowIndex,0].ToString();

				// get the integer value of product Id
				int prodID = int.Parse(Id);

				// Instantiate the ProductDetails form
				ProductDetails details = new ProductDetails();

				// get details of selected product
				bool value = details.getProductDetails(prodID,radioYes.Checked);

				// show the form
				if(value)
				 details.ShowDialog();
				else 
					details.Dispose();
			}
			catch(Exception ex)
			{
				// display any exceptions
				MessageBox.Show("Exception :"+ ex.Message);
			}

		}
        
		/************************************************************
		 * This method is called when "Close" button on the form is 
		 * clicked. It closes the current form
		 ************************************************************/ 
		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			// close the database connection
			ConnectionManager.closeConnection();

			// close the form and exit the application.
			this.Close();
			Application.Exit();
		}

		/***********************************************************
		 * This method is the starting point of the application.
		***********************************************************/
		static void Main() 
		{
			try
			{
				//Instantiate the starting form of application
				Products products =new Products();

				// connect to database
				bool connected = ConnectionManager.getDBConnection();
				if(connected)
				{
					// method to retrieve and display products
					bool value =products.displayProducts();

					// show the form
					if(!value)
					{ // close connection if products could not be retrieved
						ConnectionManager.closeConnection();
						return;
					}
						Application.Run(products);
				}
			}
			catch(Exception e)
			{
				// display any exceptions
				MessageBox.Show("Exception :" + e.Message);
				Application.Exit();
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			//close database connection
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioYes = new System.Windows.Forms.RadioButton();
			this.radioNo = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.grdProducts)).BeginInit();
			this.groupBox1.SuspendLayout();
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
			this.grdProducts.Location = new System.Drawing.Point(48, 136);
			this.grdProducts.Name = "grdProducts";
			this.grdProducts.ParentRowsBackColor = System.Drawing.SystemColors.Window;
			this.grdProducts.PreferredColumnWidth = 124;
			this.grdProducts.ReadOnly = true;
			this.grdProducts.RowHeadersVisible = false;
			this.grdProducts.SelectionBackColor = System.Drawing.SystemColors.Control;
			this.grdProducts.Size = new System.Drawing.Size(240, 128);
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
			this.Product_Name.Width = 160;
			// 
			// cmdShowDetails
			// 
			this.cmdShowDetails.Enabled = false;
			this.cmdShowDetails.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdShowDetails.Location = new System.Drawing.Point(72, 280);
			this.cmdShowDetails.Name = "cmdShowDetails";
			this.cmdShowDetails.Size = new System.Drawing.Size(128, 24);
			this.cmdShowDetails.TabIndex = 1;
			this.cmdShowDetails.Text = "Edit Product Details";
			this.cmdShowDetails.Click += new System.EventHandler(this.cmdEditProductDetails_Click);
			// 
			// cmdClose
			// 
			this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdClose.Location = new System.Drawing.Point(216, 280);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(72, 24);
			this.cmdClose.TabIndex = 2;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.radioYes,
																					this.radioNo});
			this.groupBox1.Location = new System.Drawing.Point(48, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(240, 96);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Perform Data Manipulation";
			// 
			// radioYes
			// 
			this.radioYes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.radioYes.Location = new System.Drawing.Point(16, 56);
			this.radioYes.Name = "radioYes";
			this.radioYes.Size = new System.Drawing.Size(200, 24);
			this.radioYes.TabIndex = 1;
			this.radioYes.Text = "With Safe Type Mapping";
			// 
			// radioNo
			// 
			this.radioNo.Checked = true;
			this.radioNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.radioNo.Location = new System.Drawing.Point(16, 24);
			this.radioNo.Name = "radioNo";
			this.radioNo.Size = new System.Drawing.Size(200, 24);
			this.radioNo.TabIndex = 0;
			this.radioNo.TabStop = true;
			this.radioNo.Text = "Without Safe Type Mapping";
			// 
			// Products
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(336, 325);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1,
																		  this.cmdClose,
																		  this.cmdShowDetails,
																		  this.grdProducts});
			this.Name = "Products";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Safe Type Mapping Sample";
			((System.ComponentModel.ISupportInitialize)(this.grdProducts)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}
}
