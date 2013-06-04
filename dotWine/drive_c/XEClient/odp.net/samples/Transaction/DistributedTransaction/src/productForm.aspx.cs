/**************************************************************************
@author                        :  Jagriti
@version                       :  1.0
Development Environment        :  Microsoft Visual Studio .Net
Name of the File               :  productForm.aspx.cs
Creation/Modification History  :
                                  26-Oct-2002     Created

NOTE: 
Overview: The purpose of this ASP.NET sample is to demonstrate how 
distributed transactions can be done using Microsoft Transaction Server(MTS)
through Oracle Data Provider for .NET (ODP.NET).
This web form displays the two datagrids populated with products data from 
HQ and Regional schemas. The user is provided with a facility of updating HQ 
Datagrid.This web form calls the 'updateProducts' method of 
DistributedTransaction.cs file to handle distributed transactions.

Note: To keep the sample setup simple, we have simulated the scenario of 
      two database instances with two schemas in the same database. Here HQ
      and Regional are two schemas within the same database.
**************************************************************************/

//Standard Namespaces referenced in this sample application
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;


namespace DistributedTransaction
{
	public class productForm : System.Web.UI.Page
	{
		// For Database connection 
		private OracleConnection hqConn;
		private OracleConnection regionConn;
        
		// Web Controls
		protected DataGrid hqDataGrid;
		protected DataGrid regionDataGrid;

		protected Button closeBtn;
		protected Label hqLabel;
		protected Label regionalLabel;
		

		/**************************************************************
		 * The purpose of this method is to establish connection to 
		 * HQ and Regional databases and populate the datagrids
		 * with Product data.
		 **************************************************************/
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// Establish connection to HQ database
				bool connectedToHQ = ConnectionManager.getHQDBConnection();

				// Establish connection to Regional database
				bool connectedToRegion = ConnectionManager.getRegionDBConnection();
			    
				// If both the connections were established successfully
				if(connectedToHQ && connectedToRegion)
                  { 
					// Populate the datagrids for the first time this webform is loaded
					if (!Page.IsPostBack)
					{
						// Populate datagrids from HQ and Regional Products Table
						populateHQProductsDataGrid();
						populateRegionProductsDataGrid();
					}
				}
			}
			catch(Exception ex)
            {
				// If error occurs redirect to an error page
				Response.Redirect("error.aspx?error="+"Error :"+ex.Message);
			}
		}
	
		/****************************************************************************
		* The purpose of this method is to populate the "hqDataGrid' with data 
		* from 'Products'  table of "HQODP" schema. 
		* This method is called from Main method.
		*****************************************************************************/  
		private void populateHQProductsDataGrid()
		{
			// To fill DataSet and update Datasource
			OracleDataAdapter productsAdapter;

			// For automatically generating Commands to make changes to Database through Dataset
			OracleCommandBuilder productsCmdBuilder;

			// In-memory cache of data
			DataSet productsDataSet;
			try
			{ 
				// Instantiate OracleDataAdapter to create DataSet
				// Fetch Product Details
				productsAdapter = new OracleDataAdapter("SELECT " +
					" Product_ID,  " +
					" Product_Name, " +
					" Price " +
				    " FROM Products",ConnectionManager.hqConn); 
  
				// For automatically generating commands
				productsCmdBuilder = new OracleCommandBuilder(productsAdapter); 

				// Creating Dataset
				productsDataSet = new DataSet("productsDataSet");
				    
				// AddWithKey sets the Primary Key information to complete the 
				// schema information
				productsAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
			
				// Fill the DataSet 
				productsAdapter.Fill(productsDataSet, "Products");
				hqDataGrid.DataSource = productsDataSet.Tables["Products"].DefaultView;
				hqDataGrid.DataBind();
			}
			catch(Exception ex)
			{
			  // If error occurs redirect to an error page
			  Response.Redirect("error.aspx?error="+"Error :"+ex.Message);
			}
		}

		/****************************************************************************
		* The purpose of this method is to populate the "regionalDataGrid' with data 
		* from 'Products' database table of "regionOdp" schema. 
		* This method is called from Main method.
		*****************************************************************************/  
		private void populateRegionProductsDataGrid()
		{
			// To fill DataSet and update Datasource
			OracleDataAdapter productsAdapter;

			// For automatically generating Commands to make changes to Database through Dataset
			OracleCommandBuilder productsCmdBuilder;

			// In-memory cache of data
			DataSet productsDataSet;
			try
			{ 
				// Instantiate OracleDataAdapter to create DataSet
				// Fetch Product Details
				productsAdapter = new OracleDataAdapter("SELECT " +
					" Product_ID,  " +
					" Product_Name, " +
					" Price " +
					" FROM Products",ConnectionManager.regionConn); 
  
				// For automatically generating commands
				productsCmdBuilder = new OracleCommandBuilder(productsAdapter); 

				// Creating Dataset
				productsDataSet = new DataSet("productsDataSet");
				    
				// AddWithKey sets the Primary Key information to complete the 
				// schema information
				productsAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
			
				// Fill the DataSet 
				productsAdapter.Fill(productsDataSet, "Products");
				regionDataGrid.DataSource = productsDataSet.Tables["Products"].DefaultView;
                
				// Bind data to datagrid
				regionDataGrid.DataBind();
			}
			catch(Exception ex)
			{
			  // If error occurs redirect to an error page
			  Response.Redirect("error.aspx?error="+"Error :"+ex.Message);
			}
		}
	
		/***********************************************************************
		 * The purpose of this method is to display the the hqDataGrid in an 
		 * editable mode. This method is called when the user clicks on the
		 * 'Edit Row' link.
		 ***********************************************************************/
		private void hqDataGrid_EditCommand(object source, 
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		  {
			try
			{
                // Brings the current row of hqDataGrid to an editable mode
				hqDataGrid.EditItemIndex = e.Item.ItemIndex;

				// Repopulate hqDataGrid
				populateHQProductsDataGrid();
			}
			catch (Exception ex)
			{
			  // If error occurs redirect to an error page
			  Response.Redirect("error.aspx?error="+"Error :"+ex.Message);
			}
		}

		/**********************************************************************
		 * This method is called when the user clicks on 'Update' link, when 
		 * the datagrid is in editable mode.
		 * The purpose of this method is to update the data in hqDataGrid.
		 * This value of price gets updated in the HQ schema as well as in the
		 * Regional schema. 'updateProducts' method from
		 * 'DistributedTransaction.cs' is called to perform 
		 * distributed transactions.
		 *********************************************************************/
		private void hqDataGrid_UpdateCommand(object source, 
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				// Product ID is the Ist column
				TableCell prodIDCell = e.Item.Cells[0];
				int productID = System.Int32.Parse(prodIDCell.Text);

				// Price is in the 3th column
				TableCell priceCell = e.Item.Cells[2];

				// The TextBox is the 0th element of the Controls collection.
				TextBox priceBox = (TextBox)priceCell.Controls[0];
            
				// Get the value of price from the box.
				double price = System.Double.Parse(priceBox.Text);

				if (price > 0)
				{
					// Switch out of edit mode.
					hqDataGrid.EditItemIndex = -1;
			    
					// Call the static method from DistributedTransaction.cs
					// to update the HQ, region schema
					DistributedTransaction.updateProducts(productID,price);
					
					// repopulate HQ and Regional Products datagrid with 
					// updated data
					populateHQProductsDataGrid();
					populateRegionProductsDataGrid();
				}
				else
				{
					// Throw exception if price <= 0
					throw new priceException("The value for price must be > 0. Enter valid value for price!");
					
				}
			}
		
			catch(Exception ex)
			{
			  // If error occurs redirect to an error page
	    	  Response.Redirect("error.aspx?error="+"Error :"+ex.Message);
			}
		}		
		
		/***************************************************************
		 * This method is called when the 'Cancel' link is clicked, 
		 * when the hqDataGrid is in editable mode.
		 * This cancels any unsaved changes made to the hqDataGrid.
		 **************************************************************/
		private void hqDataGrid_CancelCommand(object source, 
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				// Switch off the edit mode
				hqDataGrid.EditItemIndex = -1;

				// Repopulate the Datagrid
				populateHQProductsDataGrid();
			}
			catch(Exception ex)
			{
			  // If error occurs redirect to an error page
	  		  Response.Redirect("error.aspx?error="+"Error :"+ex.Message);
			}
		}

		/****************************************************************
		 * The purpose of this method is to close the application window.
		 ****************************************************************/
		private void closeBtn_Click(object sender, System.EventArgs e)
		{
			try
			{
				// Close the connections to database
				ConnectionManager.closeHQConnection();
				ConnectionManager.closeRegionConnection();

				Response.Write("<script>window.close();</script>");
				this.Dispose();
			}
			catch(Exception ex)
			{
				// If error occurs redirect to an error page
				Response.Redirect("error.aspx?error="+"Error :"+ex.Message);
			}
		}
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.hqDataGrid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.hqDataGrid_CancelCommand);
			this.hqDataGrid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.hqDataGrid_EditCommand);
			this.hqDataGrid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.hqDataGrid_UpdateCommand);
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
  }
	public class priceException:Exception
	{
		public priceException ()
			:base() {}

		public priceException (string message)
			:base(message) {}

		public priceException (string message, Exception inner)
			:base(message, inner) {}
	}

}
