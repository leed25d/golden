using System;
using System.Data;
using System.Text;
using System.Threading;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Sample7
{
  /// <summary>
  /// Sample7: Demostrates LOB updates using result set locking.
  /// </summary>
  class Sample7
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
      
      OracleTransaction txn = con.BeginTransaction();
      OracleCommand     cmd = new OracleCommand("",con);
      
      try
      {
        // Lock the result set using FOR UPDATE clause
        cmd.CommandText = "select STORY from multimedia_tab for update";

        OracleDataReader reader = cmd.ExecuteReader();
        reader.Read();
        OracleClob clob = reader.GetOracleClob(0);
        Console.WriteLine("Old Data: {0}", clob.Value);
      
        // Modify the CLOB column of the row
        string ending = " The end.";
        clob.Append(ending.ToCharArray(), 0, ending.Length);      

        // Release the lock
        txn.Commit();

        // Fetch the new data; transaction or locking not required.
        cmd.CommandText = "select STORY from multimedia_tab where THEKEY = 1";
        reader = cmd.ExecuteReader();
        reader.Read();
        clob = reader.GetOracleClob(0);
        Console.WriteLine("New Data: {0}", clob.Value);
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
