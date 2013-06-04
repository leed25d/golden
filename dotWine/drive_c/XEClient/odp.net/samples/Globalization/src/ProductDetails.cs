/**************************************************************************
* @author                        :  Chandar
* @version                       :  1.0
* Development Environment        :  Microsoft Visual Studio .Net
* Name of the File               :  ProductDetails.cs
* Creation/Modification History  :
                                  12-Oct-2002     Created

* Overview:
* This class defines the methods to retrieve and update details of a 
* particular product to database. It uses the NLS parameters set for the
* thread and session to store and retrieve the data from database. Safe Mapping
* is used to retrieve the column values as .NET String type since the 
* String type uses thread's culture information to represent the data.
*************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Oracle.DataAccess.Client;

namespace  OracleGlobalizationSample
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
		String todayDate;
		//variable to check if any product was updated
		public static bool updated=false;

		// define the controls on the form
		private System.Windows.Forms.Button cmdUpdate;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Label lblDesc;
		private System.Windows.Forms.TextBox txtDesc;
		private System.Windows.Forms.Label lblProduct;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.Label lblPID;
		private System.Windows.Forms.Label lblPriceHead;
		private System.Windows.Forms.Label lblPrice;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProductDetails()
		{
			
    		InitializeComponent();
		}

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

        
		/*****************************************************************
		 * This method retrieves the details of products from database in 
		 * a DataSet variable using OracleDataAdapter. It uses NLS thread and
		 * session settings for getting culture sensitive data from database.
		 * The NLS thread and session parameters are already set in
		 * Product.cs class
		 ****************************************************************/
		public bool getProductDetails(int productID)
		{
			this.productID =productID;
			try
			{
				//Instantiate OracleDataAdapter to create DataSet
				productsAdapter = new OracleDataAdapter();
  
				// variable to automatically generate commands for making changes
				// through DataSet
				OracleCommandBuilder productsCmdBuilder = new OracleCommandBuilder(productsAdapter); 
				
				//Set the select command to fetch product details
				//The data retrieved using TO_CHAR function uses session's
				//culture info (NLS settings) to get the data onto client.
				 productsAdapter.SelectCommand = new OracleCommand(
					" SELECT Product_ID,Product_Name,Product_Desc,"+
					"TO_CHAR(Price,'L99G999D99') Price, "+
					"Modification_Date,sysdate from products "+
					"where product_id="+productID,ConnectionManager.conn); 
  				
				// Use SafeMapping so that column data for corruptible types like
				// Varchar2,Date etc is retrieved as String type. The .NET String 
				// types use thread's culture info for the data values.
				// This ensures that thread's NLS settings are used to get the 
				// data.
				productsAdapter.SafeMapping.Add("*",typeof(string));
				
				//AddWithKey sets the Primary Key information to complete the 
				//schema information
				productsAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				
				//Instantiate DataSet object
				detailsDataSet = new DataSet("DetailsDataSet");

				//Fill the DataSet with data from 'Products' database table
				productsAdapter.Fill(detailsDataSet, "Products");
                
				// Get the row containing details of product
				DataRow row =detailsDataSet.Tables["Products"].Rows[0];

				// display the product details on form controls
				lblID.Text = row["Product_ID"].ToString();
				txtName.Text = row["Product_Name"].ToString();
				txtDesc.Text = row["Product_Desc"].ToString();
				lblPrice.Text = row["Price"].ToString();
				lblDate.Text = row["Modification_Date"].ToString();
				todayDate =row["sysdate"].ToString();
			}
			catch(Exception ex)
			{
				//Display error message
				MessageBox.Show(ex.ToString());
				return false;
			}
			return true;
		}
        
		/*****************************************************************
		 * This method updates the details of products into the database.  
		 * The  DataSet object is first edited to incorporate the changes
		 * to product details and then this DataSet is used to update the
		 * database table. 
		*****************************************************************/
		private void updateProductDetails()
		{
			try
			{
				//get the row of selected product from dataset
				DataRow detailsRow =detailsDataSet.Tables[0].Rows.Find(productID);

				//check if user has updated the values
				if(txtName.Text.Equals(detailsRow["Product_Name"].ToString()) 
					&& txtDesc.Text.Equals(detailsRow["Product_Desc"].ToString()))
				{
					MessageBox.Show("No changes made");
					return;
				}
				//Start the edit operation on the current row
				detailsRow.BeginEdit();
			
				//Assigning the value of product name
				detailsRow["Product_Name"] = txtName.Text;

				//Assigning the value of product name
				detailsRow["Product_Desc"] = txtDesc.Text;
				
				//Assign modification date to today's date
			    detailsRow["Modification_Date"] = todayDate;

				//End the editing current row operation
				detailsRow.EndEdit();

				// update the changes in DataSet to database
				productsAdapter.Update(detailsDataSet,"Products");

				// message on successful updation.
				MessageBox.Show("Update Successful");
				updated =true;
			}
			catch(Exception e)
			{
				//Display error message
				System.Windows.Forms.MessageBox.Show(e.ToString());				
			}
		}

		/*************************************************************
		 * This event is fired when "Update" button is clicked
		 * It calls method to update changes to database.
		 * ***********************************************************/
		private void cmdUpdate_Click(object sender, System.EventArgs e)
		{
			//validate the user input
			bool proceed =validateInput();
			if(proceed)
			{
				// update product details to database
				updateProductDetails();
				// view updated details on form
				getProductDetails(productID);
			}
		}

		
		/***************************************************************
		 * This method validates the user input before updating it to 
		 * database
		 **************************************************************/
		private bool validateInput()
		{
			//if Name of product is not entered
			if(txtName.Text=="")
			{
				MessageBox.Show("Enter Product Name");
				return false;
			}
			//if description of product is not entered
			else if(txtDesc.Text=="")
			{
				MessageBox.Show("Enter Product description");
				return false;
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
			this.lblDesc = new System.Windows.Forms.Label();
			this.txtDesc = new System.Windows.Forms.TextBox();
			this.lblPriceHead = new System.Windows.Forms.Label();
			this.cmdUpdate = new System.Windows.Forms.Button();
			this.cmdClose = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.lblID = new System.Windows.Forms.Label();
			this.lblPID = new System.Windows.Forms.Label();
			this.lblPrice = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblProduct
			// 
			this.lblProduct.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProduct.Location = new System.Drawing.Point(40, 72);
			this.lblProduct.Name = "lblProduct";
			this.lblProduct.Size = new System.Drawing.Size(112, 24);
			this.lblProduct.TabIndex = 0;
			this.lblProduct.Text = "Product Name";
			// 
			// lblDesc
			// 
			this.lblDesc.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDesc.Location = new System.Drawing.Point(40, 120);
			this.lblDesc.Name = "lblDesc";
			this.lblDesc.Size = new System.Drawing.Size(112, 32);
			this.lblDesc.TabIndex = 2;
			this.lblDesc.Text = "Description";
			// 
			// txtDesc
			// 
			this.txtDesc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtDesc.Location = new System.Drawing.Point(40, 152);
			this.txtDesc.Multiline = true;
			this.txtDesc.Name = "txtDesc";
			this.txtDesc.Size = new System.Drawing.Size(304, 80);
			this.txtDesc.TabIndex = 3;
			this.txtDesc.Text = "textBox1";
			// 
			// lblPriceHead
			// 
			this.lblPriceHead.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPriceHead.Location = new System.Drawing.Point(48, 264);
			this.lblPriceHead.Name = "lblPriceHead";
			this.lblPriceHead.Size = new System.Drawing.Size(80, 24);
			this.lblPriceHead.TabIndex = 4;
			this.lblPriceHead.Text = "Price ";
			// 
			// cmdUpdate
			// 
			this.cmdUpdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdUpdate.Location = new System.Drawing.Point(168, 368);
			this.cmdUpdate.Name = "cmdUpdate";
			this.cmdUpdate.Size = new System.Drawing.Size(80, 24);
			this.cmdUpdate.TabIndex = 6;
			this.cmdUpdate.Text = "Update";
			this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
			// 
			// cmdClose
			// 
			this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdClose.Location = new System.Drawing.Point(264, 368);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(80, 24);
			this.cmdClose.TabIndex = 7;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// txtName
			// 
			this.txtName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtName.Location = new System.Drawing.Point(176, 72);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(168, 23);
			this.txtName.TabIndex = 8;
			this.txtName.Text = "textBox1";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(48, 312);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 23);
			this.label1.TabIndex = 9;
			this.label1.Text = "Last Modified on";
			// 
			// lblDate
			// 
			this.lblDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDate.Location = new System.Drawing.Point(184, 312);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(200, 24);
			this.lblDate.TabIndex = 10;
			this.lblDate.Text = "Date";
			// 
			// lblID
			// 
			this.lblID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblID.Location = new System.Drawing.Point(176, 24);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(104, 32);
			this.lblID.TabIndex = 11;
			this.lblID.Text = "Product ID";
			// 
			// lblPID
			// 
			this.lblPID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPID.Location = new System.Drawing.Point(40, 24);
			this.lblPID.Name = "lblPID";
			this.lblPID.TabIndex = 12;
			this.lblPID.Text = "Product ID";
			// 
			// lblPrice
			// 
			this.lblPrice.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPrice.Location = new System.Drawing.Point(184, 264);
			this.lblPrice.Name = "lblPrice";
			this.lblPrice.Size = new System.Drawing.Size(160, 24);
			this.lblPrice.TabIndex = 13;
			this.lblPrice.Text = "label2";
			// 
			// ProductDetails
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(392, 413);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblPrice,
																		  this.lblPID,
																		  this.lblID,
																		  this.lblDate,
																		  this.label1,
																		  this.txtName,
																		  this.cmdClose,
																		  this.cmdUpdate,
																		  this.lblPriceHead,
																		  this.txtDesc,
																		  this.lblDesc,
																		  this.lblProduct});
			this.MaximizeBox = false;
			this.Name = "ProductDetails";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Product Details";
			this.ResumeLayout(false);

		}
		#endregion
				
	}
}
