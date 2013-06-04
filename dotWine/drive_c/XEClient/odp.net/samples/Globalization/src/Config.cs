/**************************************************************************
* @author                        :  Chandar
* @version 1.0
* Development Environment        :  Microsoft Visual Studio .Net
* Name of the File               :  Config.cs
* Creation/Modification History  :
                                  12-Oct-2002     Created

* Overview:
* This file defines the variables for connection parameters for database.
* Replace the values with those applicable for your database.
* It also defines variables for setting NLS parameter for the session.
* Change the values to those you want to use for the session.
*************************************************************************/


using System;

namespace OracleGlobalizationSample
{
	public class Config
	{
		//Parameters for database connection
		//Change the values to those applicable to your database

		public static string Datasource="ora9idb";    // TNSName for database
		public static string Username="oranet";      // Username
		public static string Password="oranet";      // Password
      
		/**********NLS Session Parameters***************************/

		public static string language="AMERICAN";     // NLS Language
		public static string territory="AMERICA";      // NLS Territory
		public static string dateformat="Day:Dd Month yyyy"; //NLS Date Format
	}
}
