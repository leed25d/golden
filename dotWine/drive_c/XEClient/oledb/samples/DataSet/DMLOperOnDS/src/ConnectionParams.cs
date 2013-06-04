/**************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .Net
Name of the File               :  ConnectionParams.cs
Creation/Modification History  :
                                  21-June-2002     Created

Overview
This file defines the variables for connection parameters for database.
**************************************************************************/

using System;
namespace ManipulateProducts
{

	public class ConnectionParams
	{
		//Parameters for database connection
		//Change the values to those applicable to your database
		public static string Datasource="orcl9i.idc.oracle.com"; //Connect String as TNSNames
		public static string Username="ORANET";      //Username
		public static string Password="ORANET";      //Password
	}
}
		