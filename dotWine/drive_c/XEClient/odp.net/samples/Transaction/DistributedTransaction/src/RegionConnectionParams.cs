/**************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .Net
Name of the File               :  RegionConnectionParams.cs
Creation/Modification History  :
                                  23-July-2002     Created

Overview
This file defines the variables for connection parameters for database.
**************************************************************************/

using System;
namespace DistributedTransaction
{

	public class RegionConnectionParams
	{
		//Parameters for database connection
		//Change the values to those applicable to your database
		public static string Datasource="ora9idb.idc.oracle.com"; //Connect String as TNSNames
		public static string Username="regionodp";      //Username
		public static string Password="regionodp";      //Password
	}
}	
		
