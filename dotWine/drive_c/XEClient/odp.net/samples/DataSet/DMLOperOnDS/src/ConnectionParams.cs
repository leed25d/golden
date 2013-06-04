/**************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .Net
Name of the File               :  ConnectionParams.cs
Creation/Modification History  :
                                  23-July-2002     Created

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
		public static string Datasource="ora9idb"; //Connect String as TNSNames
		public static string Username="scott";      //Username
		public static string Password="tiger";      //Password
	}
}
		