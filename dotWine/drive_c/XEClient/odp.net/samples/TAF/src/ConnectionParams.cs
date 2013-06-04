/**************************************************************************
@author  Chandar
@version 1.0
Development Environment        :  MS Visual Studio .Net
Name of the File               :  ConnectionParams.cs
Creation/Modification History  :
                                 28-Oct-2002     Created

Overview:
This file defines the variables for connection parameters for database.
**************************************************************************/

using System;
namespace TAFSample
{

	public class ConnectionParams
	{
		//Parameters for database connection
		//Change the values to those applicable to your database

		//Replace with Connect String as TNSNames
		public static string Datasource="ora9idb"; 

        
		public static string Username="oranet";      //Username
		public static string Password="oranet";      //Password
	}
}
		