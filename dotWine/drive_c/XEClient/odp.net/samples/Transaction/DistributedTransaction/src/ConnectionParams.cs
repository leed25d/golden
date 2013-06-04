/**************************************************************************
@author                        :  Jagriti
@version                       :  1.0
Development Environment        :  Microsoft Visual Studio .Net
Name of the File               :  ConnectionParams.cs
Creation/Modification History  :
                                  26-Oct-2002     Created

Overview
This file defines the variables for connection parameters for database.
**************************************************************************/

using System;
namespace DistributedTransaction
{
	public class ConnectionParams
	{
		//Parameters for database connection for HQ
		//Change the values to those applicable to your database
		public static string hqDatasource = "ora9idb.idc.oracle.com"; //Connect String as TNSNames
		public static string hqUsername = "hqodp";      //Username
		public static string hqPassword = "hqodp";      //Password

		//Parameters for database connection for REGION
		//Change the values to those applicable to your database
		public static string regionDatasource = "ora9idb.idc.oracle.com"; //Connect String as TNSNames
		public static string regionUsername = "regionodp";      //Username
		public static string regionPassword = "regionodp";      //Password
	}
}
		
