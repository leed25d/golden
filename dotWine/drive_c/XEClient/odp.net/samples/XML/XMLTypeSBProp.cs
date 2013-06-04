using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace XMLTypeSBProp
{
	/// <summary>
	/// XMLTypeSBProp : Demonstrates properties and methods of OracleXmlType
	/// for schema based XML data. 
	/// This sample requires Oracle Database version 10g or later
	/// </summary>
	class XMLTypeSBProp
	{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
      // Create the connection.
      string constr = "User Id=scott;Password=tiger;Data Source=oracle";
      OracleConnection con = new OracleConnection(constr);
      con.Open();

      // Register the XML schema in the database
      RegisterXmlSchema(con);

      // Construct the schema based XML data 
      StringBuilder blr = new StringBuilder();
      blr.Append("<?xml version=\"1.0\"?> ");
      blr.Append("<PO xmlns=\"po.xsd\" ");
      blr.Append("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
      blr.Append("xsi:schemaLocation=\"po.xsd po.xsd\" PONO=\"1\"> ");
      blr.Append("<PNAME>Po_1</PNAME> <CUSTNAME>John</CUSTNAME> ");
      blr.Append("<SHIPADDR> <STREET>1033, Main Street</STREET> ");
      blr.Append("<CITY>Sunnyvale</CITY> <STATE>CA</STATE> </SHIPADDR> ");
      blr.Append("</PO>");

      // Create a OracleXmlType from the schema based XML data
      OracleXmlType xml = new OracleXmlType(con, blr.ToString());

      // Print the XML data in the OracleXmlType
      Console.WriteLine("OracleXmlType Value: " + xml.Value);
      Console.WriteLine("");

      // Validate the XML data in the OracleXmlType against the schema registered
      // in the oracle database
      if (xml.Validate("po.xsd"))
        Console.WriteLine("OracleXmlType Schema Validation: Success\n");
      else
        Console.WriteLine("OracleXmlType Schema Validation: Failed\n");


      // Print the RootElement of the XML data in the oraclexmltype
      Console.WriteLine("OracleXmlType RootElement: " + xml.RootElement);
      Console.WriteLine("");

      // Print the SchemaUrl of the XML data in the oraclexmltype
      Console.WriteLine("OracleXmlType SchemaUrl: " + xml.SchemaUrl);
      Console.WriteLine("");

      // Print the XML Schema of the XML data in the oraclexmltype
      Console.WriteLine("OracleXmlType Schema: " + xml.Schema.Value);
      Console.WriteLine("");

      // Dispose the OracleXmlType
      xml.Dispose();

      // Unregister the XML schema from the database
      DeleteXmlSchema(con);

      // Close the connection
      con.Close();
      con.Dispose();
    }

    /// <summary>
    /// Register the XML Schema in the database
    /// </summary>
    /// <param name="con"></param>
    public static void RegisterXmlSchema(OracleConnection con)
    {
      // Construct the XML Schema document
      StringBuilder schemaDoc = new StringBuilder();
      schemaDoc.Append("<schema xmlns=\"http://www.w3.org/2001/XMLSchema\" ");
      schemaDoc.Append("targetNamespace=\"po.xsd\" ");
      schemaDoc.Append("xmlns:po=\"po.xsd\" ");
      schemaDoc.Append("xmlns:xdb=\"http://xmlns.oracle.com/xdb\" ");
      schemaDoc.Append("elementFormDefault=\"qualified\" version=\"1.0\"> ");
      schemaDoc.Append("<complexType name = \"PurchaseOrderType\"> ");
      schemaDoc.Append("<sequence> ");
      schemaDoc.Append("<element name = \"PNAME\" type=\"string\"/> ");
      schemaDoc.Append("<element name = \"CUSTNAME\" type=\"string\"/> ");      
      schemaDoc.Append("<element name = \"SHIPADDR\"> "); 
      schemaDoc.Append("<complexType> <sequence> ");
      schemaDoc.Append("<element name = \"STREET\" type=\"string\"/> ");
      schemaDoc.Append("<element name = \"CITY\" type=\"string\"/> ");
      schemaDoc.Append("<element name = \"STATE\" type=\"string\"/> ");
      schemaDoc.Append("</sequence> </complexType> </element> ");
      schemaDoc.Append("</sequence> ");
      schemaDoc.Append("<attribute name = \"PONO\" type=\"int\"/> ");
      schemaDoc.Append("</complexType> ");
      schemaDoc.Append("<element name = \"PO\" type=\"po:PurchaseOrderType\"/> ");
      schemaDoc.Append("</schema>");

      // Delete the schema if already registered
      DeleteXmlSchema(con);

      OracleCommand cmd = new OracleCommand("", con);
      StringBuilder blr = new StringBuilder();

      // Register the XML schema in the database
      blr.Append("begin ");
      blr.Append("DBMS_XMLSCHEMA.RegisterSchema(\'po.xsd\', :1, true, true, false); ");
      blr.Append("end;");

      cmd.CommandText = blr.ToString();
      cmd.Parameters.Add(":1", OracleDbType.Varchar2, 
                          schemaDoc.ToString(), ParameterDirection.Input);

      try 
      {
        cmd.ExecuteNonQuery();
        Console.WriteLine("Register XML Schema in Database: Success\n");
      }
      catch (OracleException e)
      { 
        Console.WriteLine("Register XML Schema in Database: Failed\n");
        Console.WriteLine("Error: {0}", e.Message);
      }
      
      // Dispose OracleCommand object
      cmd.Parameters.Clear();
      cmd.Dispose();
      return;
    }

    /// <summary>
    /// Delete XML Schema registered in the database
    /// </summary>
    /// <param name="con"></param>
    public static void DeleteXmlSchema(OracleConnection con)
    {
      // Delete the XML Schema registered in the database
      OracleCommand cmd = new OracleCommand("", con);
      StringBuilder blr = new StringBuilder();

      blr.Append("begin ");
      blr.Append("dbms_xmlschema.deleteSchema('po.xsd', DBMS_XMLSCHEMA.DELETE_CASCADE); ");
      blr.Append("end;");

      cmd.CommandText = blr.ToString();
      cmd.CommandType = CommandType.Text;
      try
      {
        cmd.ExecuteNonQuery();
        Console.WriteLine("Unregister XML Schema from Database: Success\n");
      }
      catch(OracleException e)
      {
        if (e.Number != 31000)
        {
          Console.WriteLine("Unregister XML Schema from Database: Failed\n");
          Console.WriteLine("Error: {0}", e.Message);
        }
      }

      // Dispose OracleCommand object
      cmd.Dispose();
      return;
    }
  }
}
