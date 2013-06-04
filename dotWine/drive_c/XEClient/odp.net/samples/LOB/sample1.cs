using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Sample1
{
  /// <summary>
  /// Sample 1: Demonstrates how an populate and obtain LOB data
  ///           from a DataSet.
  /// </summary>
  class Sample1
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
      OracleCommand cmd = new OracleCommand("select story from multimedia_tab");
      cmd.Connection    = con;
      cmd.CommandType   = CommandType.Text;

      // Create the OracleDataAdapter
      OracleDataAdapter da = new OracleDataAdapter(cmd);
      
      // Populate a DataSet
      DataSet ds = new DataSet();
      try
      {
        da.FillSchema(ds, SchemaType.Source, "Media");

        // Populate the DataSet with LOB data
        da.Fill(ds, "Media");

        // Obtain LOB data from the database and print it out
        Console.WriteLine(ds.Tables["Media"].Rows[0]["story"]);
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }
      finally
      {
        // Dispose OracleCommand object
        cmd.Dispose();

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
    /// <param name="connectStr"></param>
    public static void Setup(OracleConnection con)
    {
      StringBuilder blr;
      OracleCommand cmd = new OracleCommand("", con);

      blr = new StringBuilder();
      blr.Append("DROP TABLE multimedia_tab");
      cmd.CommandText = blr.ToString();
      try 
      {
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      { 
        Console.WriteLine("Warning: {0}", e.Message);
      }
      
      blr = new StringBuilder();
      blr.Append("CREATE TABLE multimedia_tab(thekey NUMBER(4) PRIMARY KEY,");
      blr.Append("story CLOB, sound BLOB)");
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
      blr.Append("INSERT INTO multimedia_tab values(");
      blr.Append("1,");
      blr.Append("'This is a long story. Once upon a time ...',");
      blr.Append("'656667686970717273747576777879808182838485')");
      cmd.CommandText = blr.ToString();
      try 
      {
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }
    }
  }
}
