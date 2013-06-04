/**************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .Net
Name of the File               :  ConnectionParams.cs
Creation/Modification History  :
                                  21-July-2002     Created

Overview:
This file defines the variables for connection parameters for database.
**************************************************************************/

using System;
namespace ManipulateAds
{

	public class ConnectionParams
	{
		//Parameters for database connection
		//Change the values to those applicable to your database

		//Replace with Connect String as TNSNames
		public static string Datasource="ora9idb"; 
		public static string Username="scott";      //Username
		public static string Password="tiger";      //Password
	}
}
		