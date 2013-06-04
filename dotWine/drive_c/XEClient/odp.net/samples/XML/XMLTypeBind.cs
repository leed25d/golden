using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace XMLTypeBind
{
  /// <summary>
  /// XMLTypeBind: Demonstrates how to bind OracleXmlType as input parameter
  /// </summary>
  class XMLTypeBind
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
      // Connect
      string constr = "User Id=scott;Password=tiger;Data Source=oracle";
      OracleConnection con = Connect(constr);
      
      // Setup the table
      Setup(con);

      // Insert non empty data to a XMLType column
      InsertValue(con);
      
      // Update XMLType column to a NULL value
      UpdateNULLValue(con);
 
      // Cleanup 
      Cleanup(con);
        
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
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }

      // Dispose OracleCommand object
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

      // Dispose OracleCommand object
      cmd.Dispose();
    }

    /// <summary>
    /// Insert non empty XML data to XMLType column
    /// </summary>
    /// <param name="con"></param>
    public static void InsertValue(OracleConnection con)
    {
      // Create the OracleCommand
      OracleCommand cmd = new OracleCommand("INSERT INTO po_tab values(:1)");
      cmd.Connection    = con;
      cmd.CommandType   = CommandType.Text;

      StringBuilder blr = new StringBuilder();
      blr.Append("<?xml version=\"1.0\"?> <PO pono=\"1\"> ");
      blr.Append("<PNAME>Po_1</PNAME> <CUSTNAME>John</CUSTNAME> ");
      blr.Append("<SHIPADDR> <STREET>1033, Main Street</STREET> ");
      blr.Append("<CITY>Sunnyvale</CITY> <STATE>CA</STATE> </SHIPADDR> ");
      blr.Append("</PO>");

      try
      {
        // Bind non empty value for XMLType column
        cmd.Parameters.Add(":1", OracleDbType.XmlType, blr.ToString(), ParameterDirection.Input);

        cmd.ExecuteNonQuery();

        // Read the data inserted above
        cmd.Parameters.Clear();
        cmd.CommandText = "SELECT po FROM po_tab";
        OracleDataReader dr = cmd.ExecuteReader();
        dr.Read();
        Console.WriteLine("XMLType data: " + dr.GetString(0));
        Console.WriteLine("");
        dr.Close();
      }
      catch(Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }

      // Dispose OracleCommand object
      cmd.Dispose();
    }

    /// <summary>
    /// Set XMLType column value to NULL
    /// </summary>
    /// <param name="con"></param>
    public static void UpdateNULLValue(OracleConnection con)
    {
      // Create the OracleCommand
      OracleCommand cmd = new OracleCommand("UPDATE po_tab SET po = :1");
      cmd.Connection    = con;
      cmd.CommandType   = CommandType.Text;

      try
      {
        cmd.Parameters.Clear();

        // Bind NULL to XMLType column
        cmd.Parameters.Add(":1", OracleDbType.XmlType, DBNull.Value, ParameterDirection.Input);
        cmd.ExecuteNonQuery();

        cmd.Parameters.Clear();
        cmd.CommandText = "SELECT po FROM po_tab";
        OracleDataReader dr = cmd.ExecuteReader();
        dr.Read();
        if (dr.IsDBNull(0))
          Console.WriteLine("XMLType data is NULL ");
        else
          Console.WriteLine("XMLType data is Not NULL ");
        
        Console.WriteLine("");
      }
      catch(Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }
    }
  }
}
