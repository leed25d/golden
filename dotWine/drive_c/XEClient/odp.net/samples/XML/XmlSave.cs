using System;
using System.Data;
using System.IO;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace XmlSave
{
  /// <summary>
  /// XmlSave : Demonstrates how to save changes to relational data
  ///           using an XML document.
  /// </summary>
  class XmlSave
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
      int rows = 0;
      string[] KeyColumnsList = null;
      string[] UpdateColumnsList = null;
      Stream stream = null;
      StreamReader sr = null;

      // Define the XSL document for doing the transform.
      string xslstr = "<?xml version='1.0'?>\n" +
                      "<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\n" +
                      "  <xsl:output encoding=\"utf-8\"/>\n" +
                      "  <xsl:param name=\"param1\">default</xsl:param>\n" +
                      "  <xsl:param name=\"param2\">default</xsl:param>\n" +
                      "  <xsl:template match=\"/\">\n" +
                      "    <ROWSET>\n" +
                      "      <xsl:apply-templates select=\"EMPLOYEES\"/>\n" +
                      "    </ROWSET>\n" +
                      "  </xsl:template>\n" +
                      "  <xsl:template match=\"EMPLOYEES\">\n" +
                      "      <xsl:apply-templates select=\"EMPLOYEE\"/>\n" +
                      "  </xsl:template>\n" +
                      "  <xsl:template match=\"EMPLOYEE\">\n" +
                      "    <ROW>\n" +
                      "    <EMPNO>\n" +
                      "      <xsl:apply-templates select=\"EMPLOYEE_ID\"/>\n" +
                      "    </EMPNO>\n" +
                      "    <ENAME>\n" +
                      "      <xsl:apply-templates select=\"EMPLOYEE_NAME\"/>\n" +
                      "    </ENAME>\n" +
                      "    <HIREDATE>\n" +
                      "        <xsl:value-of select=\"$param1\"/>\n" +
                      "    </HIREDATE>\n" +
                      "    <JOB>\n" +
                      "        <xsl:value-of select=\"$param2\"/>\n" +
                      "    </JOB>\n" +
                      "    </ROW>\n" +
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

      // Set the XML command type to insert.
      cmd.XmlCommandType = OracleXmlCommandType.Insert;

      // Set the XML document.
      cmd.CommandText = "<?xml version=\"1.0\"?>\n" +  
                        "<EMPLOYEES>\n" +  
                        "  <EMPLOYEE>\n" +  
                        "    <EMPLOYEE_ID>1234</EMPLOYEE_ID>\n" +  
                        "    <EMPLOYEE_NAME>Smith</EMPLOYEE_NAME>\n" +  
                        "  </EMPLOYEE>\n" +  
                        "</EMPLOYEES>\n";

      // Set the XML save properties.
      UpdateColumnsList = new string[4];
      UpdateColumnsList[0] = "EMPNO";
      UpdateColumnsList[1] = "ENAME";
      UpdateColumnsList[2] = "HIREDATE";
      UpdateColumnsList[3] = "JOB";

      cmd.XmlSaveProperties.Table = "emp";
      cmd.XmlSaveProperties.RowTag = "ROW";
      cmd.XmlSaveProperties.KeyColumnsList = null;
      cmd.XmlSaveProperties.UpdateColumnsList = UpdateColumnsList;
      cmd.XmlSaveProperties.Xslt = xslstr;
      cmd.XmlSaveProperties.XsltParams = "param1=\"2003-1-1T0:0:0.000\";param2=\"CLERK\"";

      // Do the inserts.
      rows = cmd.ExecuteNonQuery();
      Console.WriteLine("Rows inserted: " + rows);

      // Do a query for the inserted employee.
      Console.WriteLine("Do a query for the inserted employee.");
      cmd.XmlCommandType =  OracleXmlCommandType.Query;
      cmd.CommandText = "select * from emp e where e.empno = 1234";
      stream = cmd.ExecuteStream();
      sr = new StreamReader(stream, Encoding.Unicode);
      Console.WriteLine(sr.ReadToEnd());

      // Set the XML command type to update.
      cmd.XmlCommandType =  OracleXmlCommandType.Update;

      // Set the XML document.
      cmd.CommandText = "<?xml version=\"1.0\"?>\n" +  
                        "<EMPLOYEES>\n" +  
                        "  <EMPLOYEE>\n" +  
                        "    <EMPLOYEE_ID>1234</EMPLOYEE_ID>\n" +  
                        "    <EMPLOYEE_NAME>Adams</EMPLOYEE_NAME>\n" +  
                        "  </EMPLOYEE>\n" +  
                        "</EMPLOYEES>\n";

      // Set the XML save properties.
      KeyColumnsList = new string[1];
      KeyColumnsList[0] = "EMPNO";

      UpdateColumnsList = new string[1];
      UpdateColumnsList[0] = "ENAME";

      cmd.XmlSaveProperties.KeyColumnsList = KeyColumnsList;
      cmd.XmlSaveProperties.UpdateColumnsList = UpdateColumnsList;

      // Do the updates.
      rows = cmd.ExecuteNonQuery();
      Console.WriteLine("Rows updated: " + rows);

      // Do a query for the updated employee.
      Console.WriteLine("Do a query for the updated employee.");
      cmd.XmlCommandType =  OracleXmlCommandType.Query;
      cmd.CommandText = "select * from emp e where e.empno = 1234";
      stream = cmd.ExecuteStream();
      sr = new StreamReader(stream, Encoding.Unicode);
      Console.WriteLine(sr.ReadToEnd());

      // Set the XML command type to delete.
      cmd.XmlCommandType =  OracleXmlCommandType.Delete;

      // Set the XML document.
      cmd.CommandText = "<?xml version=\"1.0\"?>\n" +  
                        "<EMPLOYEES>\n" +  
                        "  <EMPLOYEE>\n" +  
                        "    <EMPLOYEE_ID>1234</EMPLOYEE_ID>\n" +  
                        "  </EMPLOYEE>\n" +  
                        "</EMPLOYEES>\n";

      // Set the XML save properties.
      KeyColumnsList = new string[1];
      KeyColumnsList[0] = "EMPNO";

      cmd.XmlSaveProperties.KeyColumnsList = KeyColumnsList;
      cmd.XmlSaveProperties.UpdateColumnsList = null;

      // Do the deletes.
      rows = cmd.ExecuteNonQuery();
      Console.WriteLine("Rows deleted: " + rows);

      // Do a query for the deleted employee.
      Console.WriteLine("Do a query for the deleted employee.");
      cmd.XmlCommandType =  OracleXmlCommandType.Query;
      cmd.CommandText = "select * from emp e where e.empno = 1234";
      stream = cmd.ExecuteStream();
      sr = new StreamReader(stream, Encoding.Unicode);
      Console.WriteLine(sr.ReadToEnd());

      // Clean up.
      cmd.Dispose();
      con.Close();
      con.Dispose();
	}
  }
}
