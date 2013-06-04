using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Sample6
{
  /// <summary>
  /// Sample 6: Demonstrates how to populate a DataSet with 
  ///           multiple REF Cursors selectively
  /// </summary>
  class Sample6
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

      // Get 3 RefCursors
      OracleCommand cmd = new OracleCommand("TEST.Get3Cur", con);
      cmd.CommandType = CommandType.StoredProcedure;

      OracleRefCursor[] refCursors = Get3RefCursors(cmd);
         
      try 
      {
        // Create 2 DataTable in a DataSet
        DataSet ds = new DataSet();
        ds.Tables.Add(new DataTable("refcur0"));
        ds.Tables.Add(new DataTable("refcur2"));

        // Use Adapter.Fill to populate the DataTable using
        // only two of the three OracleRefCursor objects
        OracleDataAdapter adpt = new OracleDataAdapter();
        adpt.Fill(ds.Tables["refcur0"], refCursors[0]);
        adpt.Fill(ds.Tables["refcur2"], refCursors[2]);

        // Display the Row count of each DataTable
        Console.WriteLine("Row Count:{0}", ds.Tables["refcur0"].Rows.Count);
        Console.WriteLine("Row Count:{0}", ds.Tables["refcur2"].Rows.Count);
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
      OracleCommand cmd = new OracleCommand("",con);

      // Create multimedia table
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

      // Create Package Header
      blr = new StringBuilder();
      blr.Append("CREATE OR REPLACE PACKAGE TEST is ");
      blr.Append("TYPE refcursor is ref cursor;");
      blr.Append("FUNCTION Ret1Cur return refCursor;");

      blr.Append("PROCEDURE Get1CurOut(p_cursor1 out refCursor);");

      blr.Append("FUNCTION Get3Cur (p_cursor1 out refCursor,");
      blr.Append("p_cursor2 out refCursor)");
      blr.Append("return refCursor;");

      blr.Append("FUNCTION Get1Cur return refCursor;");

      blr.Append("PROCEDURE UpdateRefCur(new_story in VARCHAR,");
      blr.Append("clipid in NUMBER);");

      blr.Append("PROCEDURE GetStoryForClip1(p_cursor out refCursor);");

      blr.Append("PROCEDURE GetRefCurData (p_cursor out refCursor,myStory out VARCHAR2);");
      blr.Append("end TEST;");

      cmd.CommandText = blr.ToString();

      try
      {
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }

      // Create Package Body
      blr = new StringBuilder();
      
      blr.Append("create or replace package body TEST is ");

      blr.Append("FUNCTION Ret1Cur return refCursor is ");
      blr.Append("p_cursor refCursor; ");
      blr.Append("BEGIN ");
      blr.Append("open p_cursor for select * from multimedia_tab; ");
      blr.Append("return (p_cursor); ");
      blr.Append("END Ret1Cur; ");

      blr.Append("PROCEDURE Get1CurOut(p_cursor1 out refCursor) is ");
      blr.Append("BEGIN ");
      blr.Append("OPEN p_cursor1 for select * from emp; ");
      blr.Append("END Get1CurOut; ");

      blr.Append("FUNCTION Get3Cur (p_cursor1 out refCursor, ");
      blr.Append("p_cursor2 out refCursor)");
      blr.Append("return refCursor is ");
      blr.Append("p_cursor refCursor; ");
      blr.Append("BEGIN ");
      blr.Append("open p_cursor for select * from multimedia_tab; ");
      blr.Append("open p_cursor1 for select * from emp; ");
      blr.Append("open p_cursor2 for select * from dept; ");
      blr.Append("return (p_cursor); ");
      blr.Append("END Get3Cur; ");

      blr.Append("FUNCTION Get1Cur return refCursor is ");
      blr.Append("p_cursor refCursor; ");
      blr.Append("BEGIN ");
      blr.Append("open p_cursor for select * from multimedia_tab; ");
      blr.Append("return (p_cursor); ");
      blr.Append("END Get1Cur; ");

      blr.Append("PROCEDURE UpdateRefCur(new_story in VARCHAR, ");
      blr.Append("clipid in NUMBER) is ");
      blr.Append("BEGIN ");
      blr.Append("Update multimedia_tab set story = new_story where thekey = clipid; ");
      blr.Append("END UpdateRefCur; ");

      blr.Append("PROCEDURE GetStoryForClip1(p_cursor out refCursor) is ");
      blr.Append("BEGIN ");
      blr.Append("open p_cursor for ");
      blr.Append("Select story from multimedia_tab where thekey = 1; ");
      blr.Append("END GetStoryForClip1; ");
      
      blr.Append("PROCEDURE GetRefCurData (p_cursor out refCursor,");
      blr.Append("myStory out VARCHAR2) is ");
      blr.Append("BEGIN ");
      blr.Append("FETCH p_cursor into myStory; ");
      blr.Append("END GetRefCurData; ");

      blr.Append("end TEST;");

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

    /// <summary>
    /// Get 3 Ref Cursors with a PL/SQL
    /// </summary>
    /// <param name="con"></param>
    /// <returns></returns>
    public static OracleRefCursor[] Get3RefCursors(OracleCommand cmd)
    {
      // 1. Get 3 OracleParameters as REF CURSORs
      // Set the command
      
      
      // Bind 
      // select * from multimedia_tab
      OracleParameter p1 = cmd.Parameters.Add("refcursor1", 
        OracleDbType.RefCursor);
      p1.Direction = ParameterDirection.ReturnValue;

      // select * from emp
      OracleParameter p2 = cmd.Parameters.Add("refcursor2", 
        OracleDbType.RefCursor);
      p2.Direction = ParameterDirection.Output;

      // select * from dept
      OracleParameter p3 = cmd.Parameters.Add("refcursor3", 
        OracleDbType.RefCursor);
      p3.Direction = ParameterDirection.Output;

      try 
      {
        cmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }

      OracleRefCursor[] refCursors = new OracleRefCursor[3];
      refCursors[0] = (OracleRefCursor)p1.Value;
      refCursors[1] = (OracleRefCursor)p2.Value;
      refCursors[2] = (OracleRefCursor)p3.Value;

      return refCursors;
    }
  }
}
