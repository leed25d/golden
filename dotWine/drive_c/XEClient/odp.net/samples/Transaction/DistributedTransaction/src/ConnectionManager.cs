/****************************************************************************
* @author                        :  Jagriti
* @version                       :  1.0
* Development Environment        :  Microsoft Visual Studio .Net 
* Name of the File               :  ConectionManager.cs
* Creation/Modification History  :
*      Jagriti    26-Oct-2002     Created 
*
* Overview:
* This class manages the database connection used by the sample. 
* It defines methods to create and close the database connection.

***************************************************************************/
//add the namespaces used by program
using System;

using Oracle.DataAccess.Client;

namespace DistributedTransaction  //namespace for sample
{
	public class ConnectionManager
	{
		public static OracleConnection hqConn;
		public static OracleConnection regionConn;
		public ConnectionManager()
		{}
		
		/*******************************************************************
		* The purpose of this method is to get the database connection 
		* to HQ schema using the connection details from ConnectionParams.cs
		* Note: Replace the datasource parameter with your datasource value
		* in ConnectionParams.cs file.
		********************************************************************/
		public static bool getHQDBConnection()
		{
			try
			{
				//Connection Information	
				string connectionString = 
					//username
					"User Id=" + ConnectionParams.hqUsername +

					//password
					";Password=" + ConnectionParams.hqPassword +

					//datasource  
					";Data Source=" + ConnectionParams.hqDatasource;

					
				//create Connection object using the above connect string
				hqConn = new OracleConnection(connectionString);

				//Open database connection
				hqConn.Open();
				return true;
			}
				// catch exceptions while connecting to the database 
		catch (Exception ex) 
			{
				//Display error message
				//MessageBox.Show(ex.ToString());
				return false;
			}
		}

		/**********************************************************************
		* The purpose of this method is to get the Regional database connection 
		* using the connection details from ConnectionParams.cs
		* Note: Replace the datasource parameter with your datasource value
		* in ConnectionParams.cs file.
		***********************************************************************/
		public static bool getRegionDBConnection()
		{
			try
			{
				//Connection Information	
				string connectionString = 
					//username
					"User Id=" + ConnectionParams.regionUsername +

					//password
					";Password=" + ConnectionParams.regionPassword +

					//datasource  
					";Data Source=" + ConnectionParams.regionDatasource;

					
				//create Connection object using the above connect string
				regionConn = new OracleConnection(connectionString);

				//Open database connection
				regionConn.Open();
				return true;
			}
				// catch exceptions while connecting to the database 
			catch (Exception ex) 
			{
				//Display error message
				//MessageBox.Show(ex.ToString());
				return false;
			}
		}
        
		/*******************************************************************
		* The purpose of this method is to close the HQ database connection 
		********************************************************************/
		public static void closeHQConnection()
		{
			hqConn.Close();
		}

		/**********************************************************************
		* The purpose of this method is to close the region database connection 
		***********************************************************************/
		public static void closeRegionConnection()
		{
			regionConn.Close();
		}
	}
}
