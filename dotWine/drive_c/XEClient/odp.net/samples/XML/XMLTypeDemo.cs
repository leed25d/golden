using System;
using System.Data;
using System.Text;
using System.Xml;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace XMLTypeDemo
{
  /// <summary>
  /// XMLTypeDemo: Demonstrates properties and methods of OracleXmlType
  /// </summary>
  class XMLTypeDemo
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
      // Connect
      string constr = "User Id=scott;Password=tiger;Data Source=oracle";
      OracleConnection con = Connect(constr);
    
      // Create an OracleXmlType from String
      StringBuilder blr = new StringBuilder();
      blr.Append("<?xml version=\"1.0\"?> <PO pono=\"1\"> ");
      blr.Append("<PNAME>Po_1</PNAME> <CUSTNAME>John</CUSTNAME> ");
      blr.Append("<SHIPADDR> <STREET>1033, Main Street</STREET> ");
      blr.Append("<CITY>Sunnyvale</CITY> <STATE>CA</STATE> </SHIPADDR> ");
      blr.Append("</PO>");

      OracleXmlType xml = new OracleXmlType(con, blr.ToString());

      //Demonstrate various properties on OracleXmlType
      PropDemo(xml);

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
    /// Demonstrate the properties and methods on OracleXmlType 
    /// </summary>
    /// <param name="connectStr"></param>
    /// <returns></returns>
    public static void PropDemo(OracleXmlType xml)
    {
      try
      {
        // Value property
        Console.WriteLine("Value property: ");
        Console.WriteLine(xml.Value);
        Console.WriteLine("");
        
        // Get an Oracle XML Stream 
        OracleXmlStream strm = xml.GetStream();
        Console.WriteLine("GetStream() method: ");
        Console.WriteLine(strm.Value);
        Console.WriteLine("");
        strm.Dispose();
        
        // Get a .NET XmlReader 
        XmlReader xmlRdr = xml.GetXmlReader();
        XmlDocument xmlDocFromRdr = new XmlDocument();
        xmlDocFromRdr.Load(xmlRdr);
        Console.WriteLine("GetXmlReader() method: ");
        Console.WriteLine(xmlDocFromRdr.OuterXml);
        Console.WriteLine("");
        xmlDocFromRdr = null;
        xmlRdr = null;

        // Get a .NET XmlDocument
        XmlDocument xmlDoc  = xml.GetXmlDocument();
        Console.WriteLine("GetXmlDocument() method: ");
        Console.WriteLine(xmlDoc.OuterXml);
        Console.WriteLine("");
        xmlDoc = null;

        // IsExists method
        string xpathexpr = "/PO/SHIPADDR";
        string nsmap = null;
        if (xml.IsExists(xpathexpr, nsmap))
        {
          // Extract method
          OracleXmlType xmle = xml.Extract(xpathexpr, nsmap);

          // IsEmpty property
          if (xmle.IsEmpty)
            Console.WriteLine("Extract() method returns empty xml data");
          else
          {
            Console.WriteLine("Extracted XML data: ");
            Console.WriteLine(xmle.Value);
          }
          Console.WriteLine("");
          xmle.Dispose();
        }

        // Use XSLT on the OracleXmlType
        StringBuilder blr = new StringBuilder();
        blr.Append("<?xml version=\"1.0\"?> ");
        blr.Append("<xsl:stylesheet version=\"1.0\" ");
        blr.Append("xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"> ");
        blr.Append(" <xsl:template match=\"/\"> ");
        blr.Append(" <NEWPO> ");
        blr.Append(" <xsl:apply-templates select=\"PO\"/> ");
        blr.Append(" </NEWPO> ");
        blr.Append(" </xsl:template> ");
        blr.Append(" <xsl:template match=\"PO\"> ");
        blr.Append(" <xsl:apply-templates select=\"CUSTNAME\"/> " );
				blr.Append(" </xsl:template> ");
				blr.Append(" <xsl:template match=\"CUSTNAME\"> ");
        blr.Append(" <CNAME> <xsl:value-of select=\".\"/> </CNAME> ");
				blr.Append(" </xsl:template> </xsl:stylesheet> ");

        string pmap = null;
        OracleXmlType xmlt = xml.Transform(blr.ToString(), pmap);

        //Print the transformed xml data
        Console.WriteLine("XML Data after Transform(): ");
        Console.WriteLine(xmlt.Value);

        // Update the CNAME in the transformed xml data
        xpathexpr = "/NEWPO/CNAME/text()";
        xmlt.Update(xpathexpr, nsmap, "NewName");

        // See the updated xml data
        Console.WriteLine("XML Data after Update(): ");
        Console.WriteLine(xmlt.Value);
        Console.WriteLine("");
        
        xmlt.Dispose();
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: {0}", e.Message);
      }
    }
  }
}
