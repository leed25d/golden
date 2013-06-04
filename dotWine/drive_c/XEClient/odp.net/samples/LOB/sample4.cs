using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Sample4
{
  /// <summary>
  /// Sample 4: Demonstrates how the LOB column data can be
  ///           read as a .NET type by utilizing stream reads.
  /// </summary>l
  class Sample4
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
        "select story from multimedia_tab where thekey = 1");
      cmd.Connection = con;
      cmd.CommandType = CommandType.Text; 

      OracleDataReader reader;
      try
      {
        // Create DataReader
        reader = cmd.ExecuteReader();

        // Read the first row 
        while(reader.Read())
        {
          // Set the OracleClob object to the CLOB selected
          OracleClob clob = reader.GetOracleClob(0);

          // Read data all data
          Byte [] clob_data = new Byte[120];
          Int64 amountRead = 0;
          int readSize = 8;
          Int64 totalRead = 0;

          do
          {
            amountRead = clob.Read(clob_data, (int)totalRead, readSize);
            Console.WriteLine("Actual read: {0} bytes", amountRead);
            totalRead += amountRead;
          } while(amountRead > 0);
            
          Console.WriteLine("Total number of bytes read: {0}", totalRead);

          // Dispose OracleClob object
          clob.Dispose();
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception:" + e.Message);
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
