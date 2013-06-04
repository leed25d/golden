/**************************************************************************
@author  Jagriti
@version 1.0
Development Environment        :  MS Visual Studio .Net
Name of the File               :  ConnectionParams.cs
Creation/Modification History  :
                                  23-July-2002     Created

Overview:
This file defines the variables for connection parameters for database.
**************************************************************************/

using System;
namespace DSPopulate
{

	public class ConnectionParams
	{
		//Parameters for database connection
		//Change the values to those applicable to your database

		//Replace with Connect String as TNSNames
		public static string Datasource="orcl9i"; 

		public static string Username="ORANET";      //Username
		public static string Password="ORANET";      //Password
	}
}
		