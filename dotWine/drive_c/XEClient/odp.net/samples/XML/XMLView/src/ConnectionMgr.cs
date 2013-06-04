/**************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .NET
Name of the File               :  ConnectionMgr.cs
Creation/Modification History  :
                                  16-July-2003     Created

Overview:
This C# source file is used to establish and close the Oracle connection 
used in this application. The OracleConnection instance is maintained in the 
"conn" static variable.
**************************************************************************/

using System;
using System.Windows.Forms;
using Oracle.DataAccess.Client;


namespace RelationalDataSample
{
	/// <summary>
	/// Summary description for ConnectionMgr.
	/// This class is used to establish and close Oracle connection.
	/// </summary>
	public class ConnectionMgr
	{
		// Variable maintains connection
		public static OracleConnection conn;

		public ConnectionMgr()
		{
		}

		/*******************************************************************************
		 * The purpose of this method is to establish a connection to an Oracle database.
		 * The connection parameters are passed to this method from the EmpForm.cs file.
		 ******************************************************************************/
		public static String getDBConnection(string username, string password, string connectString) 
		{
			try
			{
				// Connection Information	
				string connectionString = 

					// Username
					"User Id=" + username +

					// Password
					";Password=" + password +

					// Datasource  
					";Data Source=" + connectString;

					
				// Create Connection object using the above connect string
				conn = new OracleConnection(connectionString);

				// Open database connection
				conn.Open();
				return "Connected";
			}
				// Catch exceptions while connecting to the database 
			catch (Exception ex) 
			{
				MessageBox.Show ("Error occurred while connecting to the database: " + ex.Message);
				return "Not Connected";
			}
		}

		/*******************************************************************
		* The purpose of this method is to close the database connection 
		********************************************************************/
		public static void closeConnection()
		{
			try
			{
				if (ConnectionMgr.conn != null)
				{
					conn.Close();
					conn.Dispose();
				}
			}
			catch (Exception ex) 
			{
				MessageBox.Show ("Error occurred while closing the connection: " + ex.Message);
			}
		}
		   
	}
}
