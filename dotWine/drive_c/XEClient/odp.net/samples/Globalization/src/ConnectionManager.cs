/****************************************************************************
* @author :  Chandar
* @version                       :  1.0
* Development Environment        :  Microsoft Visual Studio .Net 1.0
* Name of the File               :  ConectionManager.cs
* Creation/Modification History  :
*                                   12-Oct-2002     Created 
*
* Overview:
* This class manages the database connection used by the sample. 
* It defines methods to create and close the database connection.

***************************************************************************/
//add the namespaces used by program
using System;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace OracleGlobalizationSample  //namespace for sample
{
	/// <summary>
	/// Summary description for ConnectionManager.
	/// </summary>
	public class ConnectionManager
	{
		public static OracleConnection conn;
		public ConnectionManager()
		{}
		
		/*******************************************************************
		* The purpose of this method is to get the database connection 
		* using the connection details from ConnectionParams.cs
		* Note: Replace the datasource parameter with your datasource value
		* in ConnectionParams.cs file.
		********************************************************************/
		public static bool getDBConnection()
		{
			try
			{
				//Connection Information	
				string connectionString = 
					//username
					"User Id=" + Config.Username +

					//password
					";Password=" + Config.Password +

					//datasource  
					";Data Source=" + Config.Datasource;

					
				//create Connection object using the above connect string
				conn = new OracleConnection(connectionString);

				//Open database connection
				conn.Open();
				return true;
			}
				// catch exceptions while connecting to the database 
			catch (Exception ex) 
			{
				//Display error message
				MessageBox.Show(ex.ToString());
				return false;
			}
		}
        
		/*******************************************************************
		* The purpose of this method is to close the database connection 
		********************************************************************/
		public static void closeConnection()
		{
			conn.Close();
		}

	}
}
