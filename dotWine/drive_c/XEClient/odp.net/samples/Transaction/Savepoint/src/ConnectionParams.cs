/**************************************************************************
@author                        :  Jagriti
@version                       :  1.0
Development Environment        :  Microsoft Visual Studio .Net
Name of the File               :  ConnectionParams.cs
Creation/Modification History  :
                                  22-Oct-2002     Created

Overview
This file defines the variables for connection parameters for database.
**************************************************************************/

using System;
namespace Savepoint
{

	public class ConnectionParams
	{
		//Parameters for database connection
		//Change the values to those applicable to your database
		public static string Datasource = "orcl9i";    // Connect String as TNSNames
		public static string Username = "oranet";       // Username
		public static string Password = "oranet";       // Password
	}
}
		
