/**************************************************************************
@author                        :  Jagriti
@version                       :  1.0
Development Environment        :  Microsoft Visual Studio .Net
Name of the File               :  DistributedTransaction.cs
Creation/Modification History  :
                                  26-Oct-2002     Created

Overview:  The 'UpdateProducts' method of this class is used to
perform distributed transaction. This method is called from 
productForm.aspx.cs file.

Following are the steps required to perform distributed transaction:
1. Apply the transaction attribute class to this class for performing 
   automatic transactions.
2. Extend this class from ServicedComponent class.
3. Sign the assembly with a strong name to make sure that the 
   assembly contains a unique key pair. 
4. This Strong Name Key pair containing this class automatically in
   Visual Studio .NET by specifiying the AssemblyKeyFile attribute.
5. Create and execute OracleCommand objects for updating products for HQ and
   Regional schemas.
****************************************************************************/

//Standard Namespaces referenced in this sample application
using System;
using System.EnterpriseServices;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Runtime.CompilerServices;
using System.Reflection;

// returns the name of Assembly, that is used by CLR to bind the Assembly
[assembly: ApplicationName("DistributedTransaction")]

// Specifies the name of a file containing the key pair used to generate a shared name
[assembly: AssemblyKeyFile(@"DistributedTransaction.snk")]

namespace DistributedTransaction
{
	// Applying Transaction Attribute class to this class
	// to specify Automatic Transactions
	[Transaction(TransactionOption.Required)]

	// Extending from ServicedComponent class.
	// ServicedComponent is the base class of all classes that use COM+ services
	public class DistributedTransaction : ServicedComponent 
	{
      public static void updateProducts(int productID, double price)
		{
			try
			{
				string cmdTxt = "UPDATE products SET price =" + price + " WHERE product_id =" + productID;
		
				// OracleCommand for HQ Schema
				OracleCommand hqCmd = new OracleCommand(cmdTxt,ConnectionManager.hqConn);

				// OracleCommand for Regional Schema
				OracleCommand regionCmd = new OracleCommand(cmdTxt,ConnectionManager.regionConn);

				// Executes the Oracle Commands
				hqCmd.ExecuteNonQuery();
				regionCmd.ExecuteNonQuery();

				// Release all resources held by OracleCommand objects
				hqCmd.Dispose();
				regionCmd.Dispose();
			}
			catch(Exception ex)
			{
				throw(ex);
			}
    	}
	}
}
