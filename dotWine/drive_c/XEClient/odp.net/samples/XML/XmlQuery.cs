using System;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace XmlQuery
{
  /// <summary>
  /// XmlQuery: Demonstrates how to retrieve relational data as an
  ///           XML document.
  /// </summary>
  class XmlQuery
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
      int rows = 0;
      StreamReader sr = null;

      // Define the XSL document for doing the transform.
      string xslstr = "<?xml version='1.0'?>\n" +
                      "<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\n" +
                      "  <xsl:output encoding=\"utf-8\"/>\n" +
                      "  <xsl:template match=\"/\">\n" +
                      "    <EMPLOYEES>\n" +
                      "      <xsl:apply-templates select=\"ROWSET\"/>\n" +
                      "    </EMPLOYEES>\n" +
                      "  </xsl:template>\n" +
                      "  <xsl:template match=\"ROWSET\">\n" +
                      "      <xsl:apply-templates select=\"ROW\"/>\n" +
                      "  </xsl:template>\n" +
                      "  <xsl:template match=\"ROW\">\n" +
                      "    <EMPLOYEE>\n" +
                      "    <EMPLOYEE_ID>\n" +
                      "      <xsl:apply-templates select=\"EMPNO\"/>\n" +
                      "    </EMPLOYEE_ID>\n" +
                      "    <EMPLOYEE_NAME>\n" +
                      "      <xsl:apply-templates select=\"ENAME\"/>\n" +
                      "    </EMPLOYEE_NAME>\n" +
                      "    <HIRE_DATE>\n" +
                      "      <xsl:apply-templates select=\"HIREDATE\"/>\n" +
                      "    </HIRE_DATE>\n" +
                      "    <JOB_TITLE>\n" +
                      "      <xsl:apply-templates select=\"JOB\"/>\n" +
                      "    </JOB_TITLE>\n" +
                      "    </EMPLOYEE>\n" +
                      "  </xsl:template>\n" +
                      "</xsl:stylesheet>\n";

      // Create the connection.
      string constr = "User Id=scott;Password=tiger;Data Source=oracle";
      OracleConnection con = new OracleConnection(constr);
      con.Open();

      // Set the date, and timestamp formats for Oracle 9i Release 2, or later.
      // This is just needed for queries.
      if (!con.ServerVersion.StartsWith("9.0") && !con.ServerVersion.StartsWith("8.1"))
      {
        OracleGlobalization sessionParams = con.GetSessionInfo();
        sessionParams.DateFormat = "YYYY-MM-DD\"T\"HH24:MI:SS";
        sessionParams.TimeStampFormat = "YYYY-MM-DD\"T\"HH24:MI:SS.FF3";
        sessionParams.TimeStampTZFormat = "YYYY-MM-DD\"T\"HH24:MI:SS.FF3";
        con.SetSessionInfo(sessionParams);
      }

      // Create the command.
      OracleCommand cmd = new OracleCommand("", con);

      // Set the XML command type to query.
      cmd.XmlCommandType =  OracleXmlCommandType.Query;

      // Set the SQL query.
      cmd.CommandText = "select * from emp e where e.empno = :empno";

      // Set command properties that affect XML query behaviour.
      cmd.BindByName = true;

      // Bind values to the parameters in the SQL query.
      Int32 empNum = 7369;
      cmd.Parameters.Add(":empno", OracleDbType.Int32, empNum, 
                         ParameterDirection.Input);

      // Set the XML query properties.
      cmd.XmlQueryProperties.MaxRows =  -1;
      cmd.XmlQueryProperties.RootTag =  "ROWSET";
      cmd.XmlQueryProperties.RowTag =  "ROW";
      cmd.XmlQueryProperties.Xslt =  xslstr;

      // Test query execution without returning a result.
      Console.WriteLine("SQL query: select * from emp e where e.empno = 7369");
      Console.WriteLine("Maximum rows: all rows (-1)");
      Console.WriteLine("Return Value from OracleCommand.ExecuteNonQuery():");
      rows = cmd.ExecuteNonQuery();
      Console.WriteLine(rows);
      Console.WriteLine("\n");

      // Get the XML document as an XmlReader.
      Console.WriteLine("SQL query: select * from emp e where e.empno = 7369");
      Console.WriteLine("Maximum rows: all rows (-1)");
      Console.WriteLine("XML Document from OracleCommand.ExecuteXmlReader():");
      XmlReader xmlReader =  cmd.ExecuteXmlReader();
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.PreserveWhitespace = true;
      xmlDocument.Load(xmlReader);
      Console.WriteLine(xmlDocument.OuterXml);
      Console.WriteLine("\n");

      // Change the SQL query, and set the maximum number of rows to 2.
      cmd.CommandText = "select * from emp e";
      cmd.Parameters.Clear();
      cmd.XmlQueryProperties.MaxRows =  2;

      // Get the XML document as a Stream.
      Console.WriteLine("SQL query: select * from emp e");
      Console.WriteLine("Maximum rows: 2");
      Console.WriteLine("XML Document from OracleCommand.ExecuteStream():");
      Stream stream = cmd.ExecuteStream();
      sr = new StreamReader(stream, Encoding.Unicode);
      Console.WriteLine(sr.ReadToEnd());
      Console.WriteLine("\n");

      // Get all the rows.
      cmd.XmlQueryProperties.MaxRows =  -1;

      // Append the XML document to an existing Stream.
      Console.WriteLine("SQL query: select * from emp e");
      Console.WriteLine("Maximum rows: all rows (-1)");
      Console.WriteLine("XML Document from OracleCommand.ExecuteToStream():");
      MemoryStream mstream = new MemoryStream(32);
      cmd.ExecuteToStream(mstream);
      mstream.Seek(0, SeekOrigin.Begin);
      sr = new StreamReader(mstream, Encoding.Unicode);
      Console.WriteLine(sr.ReadToEnd());
      Console.WriteLine("\n");

      // Clean up.
      cmd.Dispose();
      con.Close();
      con.Dispose();
	}
  }
}
