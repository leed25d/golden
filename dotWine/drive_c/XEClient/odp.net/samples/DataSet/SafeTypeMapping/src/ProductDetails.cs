/**************************************************************************
* @author                        :  Chandar
* @version 1.0
* Development Environment        :  Microsoft Visual Studio .Net
* Name of the File               :  ProductDetails.cs
* Creation/Modification History  :
                                  12-Oct-2002     Created

* Overview:
* This class defines the methods to retrieve and update details of a 
* particular product to database. It also shows how Safe Type Mapping feature 
* of ODP.NET can be used to prevent precision loss while retrieving high
* precision data from database.
*****************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Oracle.DataAccess.Client;
namespace  SafeTypeMapping
{
	/// <summary>
	/// Definition of ProductDetails class.
	/// </summary>
	public class ProductDetails : System.Windows.Forms.Form
	{
   
		// variable to communicate with database
		OracleDataAdapter productsAdapter;
		// variable to get data from the database
		DataSet detailsDataSet;
		// variable to store product ID of the product whose details are sought
		int productID;
		// variable to store the user selection in Products screen
		bool safeType;

		//definition of various controls on the form
		private System.Windows.Forms.Button cmdUpdate;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Label lblPrice;
		private System.Windows.Forms.TextBox txtPrice;
		private System.Windows.Forms.Label lblProduct;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtWeight;
		private System.Windows.Forms.Label lblWeight;
		private System.Windows.Forms.Label lblPID;
		private System.Windows.Forms.Label lblID;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProductDetails()
		{			
			InitializeComponent();
		}
		
		/**********************************************************************
		 * This method retrieves the details of products from database in 
		 * a DataSet variable using OracleDataAdapter. It uses Safe Type 
		 * Mapping for "Weight" and "Price" column to avoid precision loss while 
		 * retrieving it from database. When Safe Type Mapping is used the
		 * column data is retrieved as String type rather than default 
		 * .NET types.
		 * If Safe Type Mapping is not used, the method throws an 
		 * "Arithmetic operation resulted in an overflow"  exception 
		 * when precision of column data is higher than that can be handled
		 * by defalut .NET types.
		**********************************************************************/
		public bool getProductDetails(int productID, bool safeType)
		{
			this.productID =productID;
			this.safeType=safeType;
			try
			{				
				//Instantiate OracleDataAdapter to communicate with database
				productsAdapter = new OracleDataAdapter();
                
				// variable to automatically generate commands for making changes
				// through DataSet
				OracleCommandBuilder productsCmdBuilder = new OracleCommandBuilder(productsAdapter); 
				
				//Set the select command to fetch product details
				productsAdapter.SelectCommand = new OracleCommand("SELECT " +
					"Product_ID,Product_Name,Weight , Price FROM "+
					" products WHERE product_id="+productID,
					ConnectionManager.conn); 
  				
				//AddWithKey sets the Primary Key information to complete the 
				//schema information
				productsAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				
				if(safeType)
				{
					//Add Safe Mapping for Weight column so that it is retrieved
					//as String type instead of default .NET type to avoid precision loss
					productsAdapter.SafeMapping.Add("WEIGHT",typeof(string));
	
					//Add Safe Mapping for Price column  so that it is retrieved
					//as String type instead of default .NET type to avoid prcision loss
					productsAdapter.SafeMapping.Add("PRICE",typeof(string));
				}
				//Instantiate DataSet object
				detailsDataSet = new DataSet("DetailsDataSet");

				//Fill the DataSet with data from 'Products' database table
				productsAdapter.Fill(detailsDataSet, "Products");
                
				// Get the row containing details of product
				DataRow row =detailsDataSet.Tables["Products"].Rows[0];

				// display the product details on form controls
				lblID.Text =row["Product_ID"].ToString();
				lblName.Text = row["Product_Name"].ToString();
				txtWeight.Text = row["Weight"].ToString();
				txtPrice.Text = row["Price"].ToString();
			}
			catch(Exception ex)
			{
				//Display error message
				MessageBox.Show(ex.Message);	
				return false;
			}

			return true;
		}
        
		/*****************************************************************
		 * This method updates the details of products into the database.  
		 * The  DataSet object is first edited to incorporate the changes
		 * to product details and then this DataSet is used to update the
		 * database table. Safe Type Mapping is used for "Weight" and 
		 * "Price" column to avoid precision loss while updating the values
		 * into database. 
		*****************************************************************/
		private void updateProductDetails()
		{
			try
			{
				//get the Row to be updated
				DataRow detailsRow =detailsDataSet.Tables[0].Rows.Find(productID);
			    
				// check if user has made any changes to data values
				if(txtWeight.Text.Equals(detailsRow["Weight"].ToString()) 
					&& txtPrice.Text.Equals(detailsRow["Price"].ToString()))
				{
					MessageBox.Show("No changes made");
					return;
				}
				//Start the edit operation on the current row
				detailsRow.BeginEdit();
			
				//Assigning the value of product weight
				detailsRow["Weight"] = txtWeight.Text;

				//Assigning the value of product price
				detailsRow["Price"] = txtPrice.Text;
			
				//End the editing current row operation
				detailsRow.EndEdit();
                
				// update the changes in DataSet to database
				productsAdapter.Update(detailsDataSet,"Products");

				// message on successful updation.
				MessageBox.Show("Update Successful");
			}
			catch(Exception e)
			{
				//Display error message
				System.Windows.Forms.MessageBox.Show(e.Message);				
			}
		}

		

		/*************************************************************
		 * This event is fired when "Update" button is clicked
		 * It calls method to update changes to database.
		 * ***********************************************************/
		private void cmdUpdate_Click(object sender, System.EventArgs e)
		{
			// check if input data is valid
			bool proceed =validateInput();
			if(proceed)
			{  
				// update details to database
				updateProductDetails();
				// get the udpated details
				getProductDetails(productID,safeType);
			}
		}

		/***************************************************************
		 * This method validates the user input before updating it to 
		 * database
		 **************************************************************/
		private bool validateInput()
		{
			//if weight of product is not entered
			if(txtWeight.Text=="")
			{
				MessageBox.Show("Enter Product Weight");
				return false;
			}
				// check if product weight is valid number
			else if(txtWeight.Text!="")
			{
				try
				{
					//Parse the string to unsigned integer
					double temp =System.Double.Parse(txtWeight.Text);
					if(temp<=0)
					{
						MessageBox.Show("Weight cannot be zero or negative");
						return false;
					}
				}
				catch(Exception ne)
				{
					txtWeight.Text="";
					MessageBox.Show("Enter valid Product Weight");
					return false;
				}
			}
			//if price of product is not entered
			if(txtPrice.Text=="")
			{
				MessageBox.Show("Enter Product Price");
				return false;
			}
				// check if product price is valid number
			else if(txtPrice.Text!="")
			{
				try
				{
					//Parse the string to unsigned integer
					double temp =System.Double.Parse(txtPrice.Text);
					if(temp<=0)
					{
						MessageBox.Show("Price cannot be zero or negative");
						return false;
					}
				}
				catch(Exception ne)
				{
					txtPrice.Text="";
					MessageBox.Show("Enter valid Product Price");
					return false;
				}
			}
			return true;
		}

		/*************************************************************
		 * This event is fired when "Close" button is clicked
		 * It closes the current form.
		 * ***********************************************************/
		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
			
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblProduct = new System.Windows.Forms.Label();
			this.lblWeight = new System.Windows.Forms.Label();
			this.lblPrice = new System.Windows.Forms.Label();
			this.txtPrice = new System.Windows.Forms.TextBox();
			this.cmdUpdate = new System.Windows.Forms.Button();
			this.cmdClose = new System.Windows.Forms.Button();
			this.lblName = new System.Windows.Forms.Label();
			this.txtWeight = new System.Windows.Forms.TextBox();
			this.lblPID = new System.Windows.Forms.Label();
			this.lblID = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblProduct
			// 
			this.lblProduct.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProduct.Location = new System.Drawing.Point(32, 56);
			this.lblProduct.Name = "lblProduct";
			this.lblProduct.Size = new System.Drawing.Size(104, 24);
			this.lblProduct.TabIndex = 0;
			this.lblProduct.Text = "Product Name";
			// 
			// lblWeight
			// 
			this.lblWeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblWeight.Location = new System.Drawing.Point(32, 112);
			this.lblWeight.Name = "lblWeight";
			this.lblWeight.Size = new System.Drawing.Size(80, 32);
			this.lblWeight.TabIndex = 2;
			this.lblWeight.Text = "Weight (g)";
			// 
			// lblPrice
			// 
			this.lblPrice.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPrice.Location = new System.Drawing.Point(32, 160);
			this.lblPrice.Name = "lblPrice";
			this.lblPrice.Size = new System.Drawing.Size(64, 24);
			this.lblPrice.TabIndex = 4;
			this.lblPrice.Text = "Price ($)";
			// 
			// txtPrice
			// 
			this.txtPrice.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPrice.Location = new System.Drawing.Point(136, 152);
			this.txtPrice.MaxLength = 15;
			this.txtPrice.Name = "txtPrice";
			this.txtPrice.Size = new System.Drawing.Size(160, 23);
			this.txtPrice.TabIndex = 5;
			this.txtPrice.Text = "Price";
			// 
			// cmdUpdate
			// 
			this.cmdUpdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdUpdate.Location = new System.Drawing.Point(136, 208);
			this.cmdUpdate.Name = "cmdUpdate";
			this.cmdUpdate.Size = new System.Drawing.Size(72, 24);
			this.cmdUpdate.TabIndex = 6;
			this.cmdUpdate.Text = "Update";
			this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
			// 
			// cmdClose
			// 
			this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdClose.Location = new System.Drawing.Point(216, 208);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(80, 24);
			this.cmdClose.TabIndex = 7;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// lblName
			// 
			this.lblName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblName.Location = new System.Drawing.Point(144, 56);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(216, 24);
			this.lblName.TabIndex = 8;
			this.lblName.Text = "Product Name";
			// 
			// txtWeight
			// 
			this.txtWeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtWeight.Location = new System.Drawing.Point(136, 104);
			this.txtWeight.MaxLength = 38;
			this.txtWeight.Name = "txtWeight";
			this.txtWeight.Size = new System.Drawing.Size(160, 23);
			this.txtWeight.TabIndex = 9;
			this.txtWeight.Text = "Weight";
			// 
			// lblPID
			// 
			this.lblPID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPID.Location = new System.Drawing.Point(32, 16);
			this.lblPID.Name = "lblPID";
			this.lblPID.TabIndex = 10;
			this.lblPID.Text = "Product ID";
			// 
			// lblID
			// 
			this.lblID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblID.Location = new System.Drawing.Point(144, 16);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(120, 24);
			this.lblID.TabIndex = 11;
			this.lblID.Text = "Product ID";
			// 
			// ProductDetails
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(336, 253);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblID,
																		  this.lblPID,
																		  this.txtWeight,
																		  this.lblName,
																		  this.cmdClose,
																		  this.cmdUpdate,
																		  this.txtPrice,
																		  this.lblPrice,
																		  this.lblWeight,
																		  this.lblProduct});
			this.Name = "ProductDetails";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Product Details";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
	}	
}
