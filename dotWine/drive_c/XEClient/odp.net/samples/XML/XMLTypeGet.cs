using System;
using System.Data;
using System.Text;
using System.Xml;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace XMLTypeGet
{
  /// <summary>
  /// XMLTypeGet: Demonstrates how to get XMLType column as Oracle Type or
  ///             .NET type 
  /// </summary>
  class XMLTypeGet
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
      // Connect
      string constr = "User Id=scott;Password=tiger;Data Source=oracle";
      OracleConnection con = Connect(constr);
    
      // Setup the table & Data
      Setup(con);

      // Create the OracleCommand
      OracleCommand cmd = new OracleCommand("SELECT po FROM po_tab");
      cmd.Connection    = con;
      cmd.CommandType   = CommandType.Text;

      OracleDataReader dr = cmd.ExecuteReader();

      dr.Read();

      if (dr.IsDBNull(0))
      {
        Console.WriteLine("XMLType column is NULL");

        // Close the OracleDataReader
        dr.Close();
        
        // Dispose the OracleCommand
        cmd.Dispose();

        // Close the connection
        con.Close();
        con.Dispose();
        return;
      }


      // Get XMLType column as OraType 
      GetOraType(dr);

      // Get XMLType columns as .NET type 
      GetDotNetType(dr);

      // Close OracleDataReader object
      dr.Close();

      // Cleanup
      Cleanup(con);

      // Dispose OracleCommand object
      cmd.Dispose();

      // Close and Dispose OracleConnection object
      con.Close();
      con.Dispose();
		}

    /// <summary>
    /// Wrapper for Opening a new Connection
    /// </summary>
    /// <param name="connectStr"></param>
    /// <returns></returns>
    public static OracleConnection Connect(string connectStr)
    {
      OracleConnection con = new OracleConnection(connectStr);
      try
      {
        con.Open();
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }
      return con;
    }

    /// <summary>
    /// Setup the necessary Tables & Test Data
    /// </summary>
    /// <param name="con"></param>
    public static void Setup(OracleConnection con)
    {
      OracleCommand cmd = new OracleCommand("", con);

      StringBuilder blr = new StringBuilder();
      blr.Append("DROP TABLE po_tab");
      cmd.CommandText = blr.ToString();
      try 
      {
        cmd.ExecuteNonQuery();
      }
      catch (Exception)
      { 
        // Console.WriteLine("Warning: {0}", e.Message);
      }
      
      blr = new StringBuilder();
      blr.Append("CREATE TABLE po_tab(po sys.XMLType)");
      cmd.CommandText = blr.ToString();
      try 
      {
        cmd.ExecuteNonQuery();
      }
      catch (Exception)
      {
        // Console.WriteLine("Error: {0}", e.Message);
      }

      blr = new StringBuilder();
      blr.Append("INSERT INTO po_tab VALUES(sys.XMLType.createXML(");
      blr.Append("'<?xml version=\"1.0\"?> <PO pono=\"1\"> ");
      blr.Append("<PNAME>Po_1</PNAME> <CUSTNAME>John</CUSTNAME> ");
      blr.Append("<SHIPADDR> <STREET>1033, Main Street</STREET> ");
      blr.Append("<CITY>Sunnyvale</CITY> <STATE>CA</STATE> </SHIPADDR> ");
      blr.Append("</PO>'))");

      cmd.CommandText = blr.ToString();

      try 
      {
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Console.WriteLine("Setup Error: {0}", e.Message);
      }
      cmd.Dispose();
    }

    /// <summary>
    /// Cleanup the Tables created for the sample
    /// </summary>
    /// <param name="con"></param>
    public static void Cleanup(OracleConnection con)
    {
      StringBuilder blr = new StringBuilder();
      OracleCommand cmd = con.CreateCommand();

      blr.Append("DROP TABLE po_tab");
      cmd.CommandText = blr.ToString();
      try 
      {
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      { 
        Console.WriteLine("Cleanup Warning: {0}", e.Message);
      }
      cmd.Dispose();
    }
    
    /// <summary>
    /// Get XMLType data as Oracle Type
    /// </summary>
    /// <param name="dr"></param>
    public static void GetOraType(OracleDataReader dr)
    {
      try
      {
        // Get the XMLType column as an OracleXmlType
        OracleXmlType xml = dr.GetOracleXmlType(0);
        // Print out the xml data in the OracleXmlType object
        Console.WriteLine("Get XMLType column using GetOracleXmlType(i)");
        Console.WriteLine("Value: " + xml.Value);
        Console.WriteLine(String.Empty);
        xml.Dispose();

        // Get the XMLType column as an OracleString
        OracleString ostr = dr.GetOracleString(0);
        // Print out the xml data in the OracleString object
        Console.WriteLine("Get XMLType column using GetOracleString(i)");
        Console.WriteLine(ostr);
        Console.WriteLine(String.Empty);

        // Get the XMLType column as an Oracle type
        Object obj = dr.GetOracleValue(0);
        // Print the type name of the Oracle type object
        Console.WriteLine("Get XMLType column using GetOracleValue(i)");
        Console.WriteLine("TypeName: " + obj.GetType().AssemblyQualifiedName);
        Console.WriteLine(String.Empty);
      }
      catch (Exception e)
      {
        Console.WriteLine("GetOraType Error: {0}", e.Message);
      }
    }


    /// <summary>
    /// Get XMLType data as .NET Type
    /// </summary>
    /// <param name="dr"></param>
    public static void GetDotNetType(OracleDataReader dr)
    {
      try
      {
        // Get the XMLType column as a string 
        string str = dr.GetString(0);
        // Print out the xml data in the string object
        Console.WriteLine("Get XMLType column using GetString(i)");
        Console.WriteLine(str);
        Console.WriteLine(String.Empty);

        // Get the XMLType column as a .NET XmlTextReader 
        XmlReader xmlrdr = dr.GetXmlReader(0);
        // Print out the xml data in the XmlTextReader object
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(xmlrdr);
        Console.WriteLine("Get XMLType column using GetXmlReader(i)");
        Console.WriteLine(xmldoc.OuterXml);
        Console.WriteLine(String.Empty);

        // Get the XMLType column as a .NET type
        // XMLType columns will be retrieved as .NET string type
        Object[] obj = new Object[1];
        int numcols = dr.GetValues(obj);
        
        // Print the type name of the .NET objects
        Console.WriteLine("Get XMLType column using GetValues(object[])");
        for (int i = 0; i < numcols; i++)
        {
          Console.WriteLine("TypeName: " + obj[i].GetType().AssemblyQualifiedName);
          Console.WriteLine("Value: " + obj[i].ToString());
        }
        Console.WriteLine(String.Empty);
      }
      catch (Exception e)
      {
        Console.WriteLine("GetDotNetType Error: {0}", e.Message);
      }
    }

  }
}
