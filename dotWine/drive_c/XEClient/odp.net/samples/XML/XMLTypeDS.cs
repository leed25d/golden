using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace XMLTypeDS
{
  /// <summary>
  /// XMLTypeDS: Demonstrates how to populate and obtain XMLType data
  ///            from a DataSet.
  /// </summary>
  class XMLTypeDS
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
      OracleCommand cmd = new OracleCommand("SELECT * FROM po_tab");
      cmd.Connection    = con;
      cmd.CommandType   = CommandType.Text;

      // Create the OracleDataAdapter
      OracleDataAdapter da = new OracleDataAdapter(cmd);
      
      // Populate a DataSet
      DataSet ds = new DataSet();
      try
      {
        da.FillSchema(ds, SchemaType.Source, "po_tab");

        // Populate the DataSet with XMLType data
        da.Fill(ds, "po_tab");

        // Obtain XMLType data from the database and print it out
        Console.WriteLine("XMLType data from Database: " + 
                          ds.Tables["po_tab"].Rows[0]["po"]);
        Console.WriteLine("");
        
        StringBuilder blr = new StringBuilder();
        blr.Append("<?xml version=\"1.0\"?> <PO pono=\"1\"> ");
        blr.Append("<PNAME>NewPo_1</PNAME> <CUSTNAME>John Doe</CUSTNAME> ");
        blr.Append("<SHIPADDR> <STREET>555, Market Street</STREET> ");
        blr.Append("<CITY>San Francisco</CITY> <STATE>CA</STATE> </SHIPADDR> ");
        blr.Append("</PO>");

        // Update the data in the DataSet
        ds.Tables["po_tab"].Rows[0]["PO"] = blr.ToString();

        OracleCommandBuilder CmdBuilder = new OracleCommandBuilder(da);

        // Update the table in the database
        da.Update(ds, "po_tab");

        // Fetch the data from the table
        OracleDataReader dr = cmd.ExecuteReader();
        dr.Read();
        Console.WriteLine("XMLType data from database after update statement: " + 
                          dr.GetOracleXmlType(0).Value);
        dr.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }
      finally
      {
        // Dispose OracleCommand object
        cmd.Dispose();

        // Cleanup 
        Cleanup(con);
        
        // Close and Dispose OracleConnection object
        con.Close();
        con.Dispose();
      }
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
      blr.Append("CREATE TABLE po_tab(po sys.XMLType, ponum NUMBER PRIMARY KEY)");
      cmd.CommandText = blr.ToString();
      try 
      {
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }

      blr = new StringBuilder();
      blr.Append("INSERT INTO po_tab VALUES(sys.XMLType.createXML(");
      blr.Append("'<?xml version=\"1.0\"?> <PO pono=\"1\"> ");
      blr.Append("<PNAME>Po_1</PNAME> <CUSTNAME>John</CUSTNAME> ");
      blr.Append("<SHIPADDR> <STREET>1033, Main Street</STREET> ");
      blr.Append("<CITY>Sunnyvale</CITY> <STATE>CA</STATE> </SHIPADDR> ");
      blr.Append("</PO>'), 1)");

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

  }
}
