/********************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .NET
Name of the File               :  ManageEmp.cs
Creation/Modification History  :
                                  16-July-2003     Created

Overview:
This C# source file is the main file of this sample that does operations 
of querying data as XML and inserting/updating/deleting records using XML.
It contains the following methods:
Method Name             Purpose
-----------             -------
generateEmpXml          Retrieves relational data from 'Emp' table and displays
                        it in XML Format.
						
insertEmployee          Inserts record in 'Emp' table using XML data.
deleteEmployees         Delete records in 'Emp' table using XML data.
updateEmployees         Updates records in 'Emp' table using XML data.
createNewRecord         Displays a sample XML record for insertion.
getEmpSelectedRecord    Retrieves the selected 'Emp' record from the datagrid 
                        and displays it in XML format.
						
populateEmpDataGrid     Populates the DataGrid with data from 'Emp' table.
populateDeptno          Populates the Deptno list from 'Dept' table.

NOTE: This sample uses 'Scott' schema available with the Oracle database.
      For more information refer to the Database Setup section of the Readme.html 
	  file under the doc directory.
**********************************************************************************/

using System;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
	
namespace RelationalDataSample
{
	/// <summary>
	/// This class contains methods that does operations for querying 
	/// relational data as XML and manipulating relational data using XML.
	/// </summary>
	public class ManageEmp
	{		
		/****************************************************************************
		 * This method is called when the 'Connect' button is clicked. After the 
		 * Oracle connection is successfully established. It accesses relational
		 * data from an Oracle database and displays it as XML.
		 * The XmlCommandType property denotes XML operations on OracleCommand.
		 * The ExecuteXmlReader method of OracleCommand returns a .NET framework 
		 * XmlDocument object which is returned as a string.
		 ****************************************************************************/
		public string generateEmpXml()
		{
			try
			{
				// Represents SQL command for execution, to return results
				OracleCommand empCmd = new OracleCommand("", ConnectionMgr.conn);
				empCmd.CommandText = "SELECT empno, ename, sal, deptno FROM emp " +
					                                       " ORDER BY empno DESC" ;

				// Represents XML operations on OracleCommand. Setting
				// OracleXmlCommandType.Query returns result as XML document
				empCmd.XmlCommandType = OracleXmlCommandType.Query;

				// XmlReader provides read-only fast access to XML data,
				// OracleCommand.ExecuteXMLReader returns an XML document as result
				XmlReader empReader = empCmd.ExecuteXmlReader();

				// .NET framework class representing XmlDocument
				XmlDocument empDoc = new XmlDocument();

				// Handles white spaces during XmlDocument load process
				empDoc.PreserveWhitespace = true;
			
				// Loads data from the specified XmlReader
				empDoc.Load(empReader);

				// Gets markup representing root node and all its children
				String str = empDoc.OuterXml;

				empCmd.Dispose();
			
				return str;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error " + ex.Message);
				return ex.Message;
			}
		}

	     /****************************************************************************
		 * This method is called when 'Insert' button is clicked. It takes the entire 
		 * XML document from the 'empRichTextBox' and passses it as a parameter to 
		 * this method. It inserts data into 'Emp' table using the XML document 
		 * passed in. XmlSaveProperties are set for the OracleCommand object. 
		 ****************************************************************************/
		public bool insertEmployee(string insertEmployee)
		{
			string[] UpdateColumnsList = null;

			try
			{
			   	OracleCommand insCmd = new OracleCommand("",ConnectionMgr.conn);

				// Set the XML document as command text
				insCmd.CommandText = insertEmployee;

				// Denotes that the inserts are to be made using an XML document
				insCmd.XmlCommandType = OracleXmlCommandType.Insert;
				
				// List of columns for update
				UpdateColumnsList    = new string[4];
				UpdateColumnsList[0] = "EMPNO";
				UpdateColumnsList[1] = "ENAME";
				UpdateColumnsList[2] = "SAL";
				UpdateColumnsList[3] = "DEPTNO";


				// Set the XML save properties

				// Specifies list of the columns for insertion
				insCmd.XmlSaveProperties.UpdateColumnsList = UpdateColumnsList;

				// Specifies the name of the table to which changes are saved
				insCmd.XmlSaveProperties.Table = "emp";

				// Specifies the value for the XML element that identifies 
				// row of data in the XML document
				insCmd.XmlSaveProperties.RowTag = "ROW";							

				// Execute the insert operation
				insCmd.ExecuteNonQuery();
				
				MessageBox.Show("Data inserted successfully!");

				insCmd.Dispose();

				return true;

			}
			catch(Exception ex)
			{
				MessageBox.Show("Error " + ex.Message);
				return false;
			}
        }

		
        /******************************************************************************
		 * This method is called when the 'Update' button is clicked. It takes the XML 
		 * document from empRichTextBox as a parameter passed to it. 
		 * The XmlSaveProperties for OracleCommand are set and the records are updated.
		 ******************************************************************************/ 
		public bool updateEmployees(string updText)
		{
			// Set the key columns to locate existing row(s) for update
			string[] KeyColumnsList = new string[1]; 
			KeyColumnsList[0] = "EMPNO";
            
			string[] UpdateColumnsList = null;
			UpdateColumnsList = new string[3];
			try
			{
				OracleCommand empCmd = new OracleCommand("", ConnectionMgr.conn);

				// Denotes that the update has to be made using an XML document
				empCmd.XmlCommandType = OracleXmlCommandType.Update;

                // Set command text to the XML document 
				empCmd.CommandText = updText;
		
				// List of columns for update
				
				UpdateColumnsList[0] = "ENAME";
				UpdateColumnsList[1] = "SAL";
				UpdateColumnsList[2] = "DEPTNO";

				// Set XML save properties
				// Specifies the name of the table to which changes are saved
				empCmd.XmlSaveProperties.Table = "EMP";

				// Specifies the value for the XML element that identifies 
				// row of data in the XML document
				empCmd.XmlSaveProperties.RowTag = "ROW";

				// Specifies columns that are used to locate row for update
				empCmd.XmlSaveProperties.KeyColumnsList = KeyColumnsList;

				// Specifies columns for update
				empCmd.XmlSaveProperties.UpdateColumnsList = UpdateColumnsList;

				// Execute the update operation
				empCmd.ExecuteNonQuery();
				
				MessageBox.Show("Data updated successfully!");

				empCmd.Dispose();

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error " + ex.Message);
				return false;
			}
		}

		/******************************************************************************
		 * This method is called when the 'Delete' button is clicked and the user 
		 * confirms deleting of the record. The record(s) for deletion are passed to 
		 * this method in the form of XML from the textbox. 
		 ******************************************************************************/
		public bool deleteEmployees(string delText)
		{
			// Set the key column to locate existing row(s) for delete
			string[] KeyColumnsList = new string[1]; 
			KeyColumnsList[0] = "EMPNO";

			try 
			{
				OracleCommand empCmd = new OracleCommand("", ConnectionMgr.conn);

				// Set XmlCommandType to Delete
				empCmd.XmlCommandType = OracleXmlCommandType.Delete;

				empCmd.CommandText = delText;

				// Set key column list 
				empCmd.XmlSaveProperties.KeyColumnsList = KeyColumnsList;

				// Specifies the name of the table to which changes are saved
				empCmd.XmlSaveProperties.Table = "EMP";

				// Specifies the value for the XML element that identifies 
				// row of data in the XML document
				empCmd.XmlSaveProperties.RowTag = "ROW";

				// Execute command
				empCmd.ExecuteNonQuery();

				MessageBox.Show("Data deleted successfully!");
				
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
				return false;
			}
		}

		/*****************************************************************************
		 * The method is called when the user selects a record in the datagrid. Its 
		 * purpose is to return selected record in XML format. 
		 ****************************************************************************/
		public string getSelectedEmpRecord(string empno)
		{
			try
			{
				// Represents SQL command for execution, to return results
				OracleCommand empCmd = new OracleCommand("", ConnectionMgr.conn);
				empCmd.CommandText = " SELECT empno, ename, sal, deptno FROM emp " +
					" WHERE empno=" + empno + " ORDER BY empno DESC";

				// Represents XML operations on OracleCommand. By setting
				// OracleXmlCommandType.Query returns result as XML document
				empCmd.XmlCommandType = OracleXmlCommandType.Query;

				// XmlReader provides read-only fast access to XML data,
				// OracleCommand.ExecuteXMLReader returns XmlDocument as result
				XmlReader empReader = empCmd.ExecuteXmlReader();

				// .NET framework class representing XmlDocument
				XmlDocument empDoc = new XmlDocument();

				// Handles white spaces during XmlDocument load process
				empDoc.PreserveWhitespace = true;
			
				// Loads data from the specified XmlReader
				empDoc.Load(empReader);

				// Gets markup representing root node and all its children
				String str = empDoc.OuterXml;

				empCmd.Dispose();
			
				return str;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error " + ex.Message);
				return ex.Message;
			}
		}

		/*****************************************************************************
		 * This method is called when 'Create New Record' button is clicked. 
		 * It generates a sample XML record for the 'Emp' table and populates the 
		 * empRichTextBox with it. The empno is generated automatically. 
		 ****************************************************************************/
		public string createNewRecord()
		{
			try
			{
				// Generate unique Empno based on the MAX(empno)+1
				OracleCommand empnoCmd = new OracleCommand("SELECT NVL((MAX(empno)+1),8000)" +
					                                       "FROM emp ",ConnectionMgr.conn);
				OracleDataReader empnoReader = empnoCmd.ExecuteReader();
				empnoReader.Read();
				OracleDecimal i = empnoReader.GetDecimal(0);
            
				// Create a sample XML document
				string str = "<?xml version=\"1.0\"?>\n" +  
					"<ROWSET>\n" +  
					"  <ROW>\n" +  
					"    <EMPNO>"+ i.ToString() +"</EMPNO>\n" +  
					"    <ENAME>Angela</ENAME>\n" +  
					"    <SAL>1000</SAL>\n" +  
					"    <DEPTNO>10</DEPTNO>\n" +  
					"  </ROW>\n" +  
					"</ROWSET>\n";

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
		 * Oracle connection is established successfully, it fetches data from 'Emp'
		 * table and returns a DataSet that is bound to the DataGrid.
		 ****************************************************************************/
		public DataSet populateEmpDataGrid()
		{
			OracleDataAdapter empAdapter = new OracleDataAdapter();
			empAdapter.SelectCommand = new OracleCommand("SELECT empno, ename, " +
				                                         "sal, deptno FROM emp " +
				                                            "ORDER BY empno DESC",
				                                              ConnectionMgr.conn);

			DataSet empDataSet = new DataSet("empDataSet");
			empAdapter.Fill(empDataSet,"emp");

			return empDataSet;
		}

		/***********************************************************************
		 * This method is called when the 'Connect' button is clicked. After 
		 * Oracle connection is established successfully, it fetches department
		 * numbers from 'Dept' table and returns as DataSet to fill the list of 
		 * valid department numbers required by user to view while inserting/updating
		 * XML data.
		 **********************************************************************/
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
