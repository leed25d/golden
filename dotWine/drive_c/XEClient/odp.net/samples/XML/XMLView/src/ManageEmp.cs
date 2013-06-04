/**********************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .NET
Name of the File               :  ManageEmp.cs
Creation/Modification History  :
                                  21-Aug-2003     Created

Overview:
This C# file contains methods used for insert/update/delete/query operations 
on the XMLType view based on the relational table 'Emp'.

Method Name              Purpose
-----------				 -------

getXmlFromDB             Retrieves data from XMLType view to display in the textbox.
insertRecord             Inserts record into the 'Emp_View'.
updateRecord			 Updates a record in the 'Emp_View' based on the 'empno' key
                         column.
deleteRecord             Deletes a record from the 'Emp_View' based on the 'empno'
                         key column.
getSelectedEmpRecord     Retrieves XML data from the 'Emp_View' based on the 
                         selected record.
populateEmpDataGrid		 Retrieves data from XMLType view to display in the datagrid.
createNewRecord          Creates and displays a sample 'Emp_View' record in the textbox.
populateDeptno           Populates the Deptno list from the 'Dept' table.

NOTE: 
      To insert/update/delete data from a database view, instead-of triggers are used. 
	  In this sample, instead-of-triggers, 'Insert_Emp_Trig', 'Update_Emp_trig' and
	  'Delete_Emp_Trig' defined on the 'Emp_View' are used to manipulate data 
	  in the 'Emp' table.  These triggers were created by createview.sql.
		  
	  For more information on how to create instead-of triggers, refer to the
	  'Database Setup' section of the Readme.html file available in the 'doc' folder.                   
***************************************************************************************/

using System;
using System.Windows.Forms;
using System.Xml;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

	
namespace RelationalDataSample
{
	/// <summary>
	/// This class contains methods that perform insert/update/delete/query operations 
	/// on the XmlType view, 'Emp_View' which is based on the relational table, 'Emp'. 
	/// </summary>
	public class ManageEmp
	{
		/*********************************************************************
		 * The purpose of this method is to retrieve data from the XMLType 
		 * view, 'Emp_View' which is based on the relational table, 'Emp'.
		 * The XML data is fetched as OracleXmlType using OracleDataReader and 
		 * returned as a string.
		 *********************************************************************/
		public String getXmlFromDB()
		{
            // ODP.NET native XML data type object
			OracleXmlType empXml;

			// OracleCommand object
			OracleCommand empCmd = new OracleCommand();
			
			// Query XMLType view, 'Emp_View'
			empCmd.CommandText = "SELECT * FROM emp_view t " +
				                 "ORDER BY EXTRACTVALUE(VALUE(t), '/EMP/EMPNO/text()') DESC";
			empCmd.Connection = ConnectionMgr.conn;

			// Execute OracleCommand
			OracleDataReader empReader = empCmd.ExecuteReader();
			           	
			string str = "";
           
			// Continue to read data if it exists
			while (empReader.Read())
			{
				// Return OracleXmlType object of the specified XmlType column
				empXml = empReader.GetOracleXmlType(0);

				// Concatenate output for all the records for display in the textbox
				str = str + empXml.Value;
			}
			empCmd.Dispose();
			return (str);
		}

		/*********************************************************************************
		 * This method is called when the insert button is clicked. The purpose of this 
		 * method is to insert an XML record into 'Emp_View'. Using 'Create New Record' 
		 * button, a sample XML record is displayed in the textbox, which is passed as a
		 * string parameter to this method. To insert, the XML record for 'Emp_View' 
		 * is bound as a parameter to the OracleCommand object.
		 ********************************************************************************/
		public bool insertRecord(string rec)
		{
			try
			{
				// OracleCommand for insert operation
				OracleCommand insCmd = new OracleCommand("", ConnectionMgr.conn);

				// Declare the SQL statement to insert the XML record into the view
				insCmd.CommandText = "INSERT INTO emp_view VALUES (:1)";
				insCmd.CommandType = CommandType.Text;

				// Bind the XML record as parameter to the OracleCommand
				insCmd.Parameters.Add(":1", OracleDbType.XmlType, rec, ParameterDirection.Input);

				// Execute the insert command
				insCmd.ExecuteNonQuery();

				MessageBox.Show("Record inserted successfully !");
				insCmd.Dispose();

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("error :"+ ex.Message + ex.StackTrace);
				return false;
			}
		}
		
		/*********************************************************************************
		 * This method is called when the 'Update' button is clicked. The purpose of this
		 * method is to update the 'Emp_View' record based on the 'Empno' key column.
		 * An OracleXmlType object based on the selected record from the dataGrid is 
		 * created. The OracleXmlType is updated using the 'OracleXmlType.Update' method.
		 * This updated OracleXmlType object is bound as a parameter to an OracleCommand 
		 * for updating the XmlType view.
		 ********************************************************************************/
		public bool updateRecord(string rec, string keycolumn)
		{
			try
			{
				// Creating an OracleXMLType object 
				OracleXmlType ox = new OracleXmlType(ConnectionMgr.conn,rec);
                
				// OracleCommand object for updating the 'Emp_View' in the database
				OracleCommand cmd = new OracleCommand("", ConnectionMgr.conn);

				// Declare the SQL statement to update the XML record in the view
				// Use XPATH to identify the 'empno' primary key
				cmd.CommandText = "UPDATE emp_view t SET value(t)=  :1 " +
					" WHERE extractvalue(value(t), '/EMP/EMPNO')=" + keycolumn;
				cmd.CommandType = CommandType.Text;

				// Bind the updated OracleXmlType object to the OracleCommand
				cmd.Parameters.Add(":1", OracleDbType.XmlType, ox, ParameterDirection.Input);
				
				// Execute the update command
				cmd.ExecuteNonQuery();
				
				MessageBox.Show("Record updated successfully !");

				cmd.Dispose();

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("error :"+ ex.Message + ex.StackTrace);
				return false;
			}
		}

		/*********************************************************************************
		 * This method is called when the 'Delete' button is clicked. The purpose of this
		 * method is to delete a record from the 'Emp_View' that is selected from the datagrid.
		 * Deletion happens based on the 'Empno' key column of the selected record.
		 * ********************************************************************************/
		public bool deleteRecord (string xmlRec)
		{
			try
			{
				// OracleCommand object for delete operation
				OracleCommand delCmd = new OracleCommand("", ConnectionMgr.conn);

				// Use OracleXmlType to extract the 'empno' primary key to identify the record
				// to be deleted
				OracleXmlType oraxml = new OracleXmlType(ConnectionMgr.conn, xmlRec);   
				String empno = oraxml.Extract("/EMP/EMPNO/text()","").Value;

				// Declare the SQL statement to delete the XML record from the view
				delCmd.CommandText = "DELETE FROM emp_view t WHERE EXTRACTVALUE(VALUE(t), " +
					"'/EMP/EMPNO')=" + empno;      	
				delCmd.CommandType = CommandType.Text;

				// Execute the delete command
				delCmd.ExecuteNonQuery();
				
				MessageBox.Show("Record deleted successfully");

				oraxml = null;

				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show("error: "+ ex.Message);
				return false;
			}
		}

		/******************************************************************************
		 * The purpose of this method is to retieve data from the XmlType view based on
		 * the record selected from the 'empDataGrid'.
		 ******************************************************************************/
		public string getSelectedEmpRecord(string empno)
		{
			OracleXmlType empXml;
			OracleCommand empCmd = new OracleCommand();

			// Retrieve data for the current/selected record from the datagrid
			// Use the XPath expression to fetch the required record based on
			// key column 'EmpNo'  
			empCmd.CommandText = "SELECT * FROM emp_view t WHERE EXTRACTVALUE(VALUE(t), "+
				"'/EMP/EMPNO') = " + empno;

			// Use the existing connection variable
			empCmd.Connection = ConnectionMgr.conn;

			// Execute commnad text and return OracleDataReader object
			OracleDataReader empReader = empCmd.ExecuteReader();
			           	
			string str = "";
           
			// If record exists
			while (empReader.Read())
			{
				// Retrieve data into OracleXmlType object
				empXml = empReader.GetOracleXmlType(0);

				// Return Xml data as string
				str = empXml.Value;
			}

			empCmd.Dispose();
			return (str);
		}

		/****************************************************************************
		 * This method is called when the 'Connect' button is clicked. After 
		 * Oracle connection is established successfully, it fetches data from 
		 * 'Emp_View' XmlType view and returns a DataSet that is bound to the datagrid.
		 * 
		 * Note: The extractValue() function is used to return the value of a text
		 * node or attribute associated with an XPath expression from an XML document
		 * stored as an XMLType. It returns a scalar data type.
		 ****************************************************************************/
		public DataSet populateEmpDataGrid()
		{
			OracleDataAdapter empAdapter = new OracleDataAdapter();

			// Retrieve data from the XMLType View. Also specifying alias names
			// for columns
			empAdapter.SelectCommand = new OracleCommand("SELECT " +
			 " EXTRACTVALUE(VALUE(t), '/EMP/EMPNO/text()') EmpNo," +
			" EXTRACTVALUE(VALUE(t), '/EMP/ENAME/text()') Ename, " +
			 	" EXTRACTVALUE(VALUE(t), '/EMP/JOB/text()') job, " +
				" EXTRACTVALUE(VALUE(t), '/EMP/SAL/text()') sal, " +
			"EXTRACTVALUE(VALUE(t), '/EMP/DEPTNO/text()') deptno " +
			 	                               " FROM emp_view t " +
	        "ORDER BY EXTRACTVALUE(VALUE(t), '/EMP/EMPNO/text()') DESC" 
	                                    		,ConnectionMgr.conn);
		

			// Create a DataSet object
			DataSet empDataSet = new DataSet("empDataSet");

			// Fill DataSet with records from 'Emp_View'
			empAdapter.Fill(empDataSet,"emp_view");

			return empDataSet;
		}

		/***************************************************************************
		 * This method is called when 'Create New Record' button is clicked. 
		 * It generates a sample XML record for the 'Emp' table and populates the 
		 * empRichTextBox with it. The empno is generated automatically. 
		 **************************************************************************/
		public string createNewRecord()
		{
			try
			{
				// Generate unique Empno based on the MAX(empno)+1
				OracleCommand empnoCmd = new OracleCommand
					("SELECT  NVL(MAX(EXTRACTVALUE(VALUE(t), '/EMP/EMPNO/text()')) + 1,8000) " +
					 "FROM emp_view t",
					 ConnectionMgr.conn);

				// Retrieve the empno value to ODP.NET
				OracleDataReader empnoReader = empnoCmd.ExecuteReader();
				empnoReader.Read();
				OracleDecimal i = empnoReader.GetDecimal(0);
		            
				// Create a sample XML document
				string str = "<EMP EMPNO = \"" + i + "\" >\n"+
					              "  <ENAME>Angela</ENAME>\n"+
					                 "  <JOB>MANAGER</JOB>\n"+
					                    "  <SAL>3000</SAL>\n"+
					                "  <DEPTNO>20</DEPTNO>\n"+
					                                 "</EMP>";
		
				empnoCmd.Dispose();
				return str;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error " + ex.Message);
				return "";
			}
		}

		/****************************************************************************
		 * This method is called when the 'Connect' button is clicked. After 
		 * Oracle connection is established successfully, it fetches department
		 * numbers from 'Dept' table and returns as DataSet to fill the list of 
		 * valid department numbers required by user to view while inserting/updating
		 * XML data.
		 ****************************************************************************/
		public DataSet populateDeptno()
		{
			OracleDataAdapter deptAdapter = new OracleDataAdapter();
			deptAdapter.SelectCommand = new OracleCommand("SELECT deptno FROM dept",
				                                                ConnectionMgr.conn);
             
			DataSet deptDataSet = new DataSet("deptDataSet");
			deptAdapter.Fill(deptDataSet,"dept");
            
			deptAdapter.Dispose();
			return deptDataSet;
		}

	}
}