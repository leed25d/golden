/**************************************************************************
* @author                        :  Chandar
* @version 1.0
* Development Environment        :  Microsoft Visual Studio .Net
* Name of the File               :  ConnectionParams.cs
* Creation/Modification History  :
                                  12-Oct-2002     Created

* Overview:
* This file defines the variables for connection parameters for database.
* Replace the values with those applicable for your database.
**************************************************************************/

using System;

namespace SafeTypeMapping
{
	public class ConnectionParams
	{
		//Parameters for database connection
		//Change the values to those applicable to your database

		//Replace with Connect String as TNSNames
		public static string Datasource="ora9idb"; 

		public static string Username="oranet";      //database username
		public static string Password="oranet";      //database password


	}
}
