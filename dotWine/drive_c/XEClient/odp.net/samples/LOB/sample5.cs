using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Sample5
{
  /// <summary>
  /// Sample 5: Demonstrates how to bind an OracleClob object 
  ///           as a parameter.  This sample also refetches the newly 
  ///           updated CLOB data using an OracleDataReader and an 
  ///           OracleClob object.
  /// </summary>
  class Sample5
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
      // Connect
      string constr = "User Id=scott;Password=tiger;Data Source=oracle";
      OracleConnection con = Connect(constr);
           
      // Setup
      Setup(con);

      // Set the command
      OracleCommand cmd = new OracleCommand(
        "update multimedia_tab set story = :1 where thekey = 1");
      cmd.Connection = con;
      cmd.CommandType = CommandType.Text; 

      // Create an OracleClob object, specifying no caching and not a NCLOB
      OracleClob clob = new OracleClob(con, false, false);

      // Write data to the OracleClob object, clob, which is a temporary LOB
      string str = "this is a new story";
      clob.Write(str.ToCharArray(), 0, str.Length);

      // Bind a parameter with OracleDbType.Clob
      cmd.Parameters.Add("clobdata", 
                          OracleDbType.Clob, 
                          clob, 
                          ParameterDirection.Input);

      try 
      {
        // Execute command
        cmd.ExecuteNonQuery();
        
        // A new command text
        cmd.CommandText = "select thekey, story from multimedia_tab where thekey = 1";

        // Create DataReader
        OracleDataReader reader = null;
        try
        {
          reader = cmd.ExecuteReader();
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
          Console.WriteLine(e.StackTrace);
        }

        // Read the first row
        reader.Read();

        // Get an OracleClob object from the DataReader. Column index is 0-based.
        // clob is no more a temporary LOB. It's now a persistent LOB. 
        clob = reader.GetOracleClob(1);

        // Display the new data using Value property
        Console.WriteLine(clob.Value);
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception:" + e.Message);
      }
      finally
      {
        // Dispose OracleClob object
        clob.Dispose();

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
