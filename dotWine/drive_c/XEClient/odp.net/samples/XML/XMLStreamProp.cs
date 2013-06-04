using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace XMLStreamProp
{
  /// <summary>
  /// XMLStreamProp : Demonstrates properties and methods of OracleXmlStream
  /// </summary>
  class XMLStreamProp
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

      // Create an OracleXmlType from String
      StringBuilder blr = new StringBuilder();
      blr.Append("<?xml version=\"1.0\"?> <PO pono=\"1\"> ");
      blr.Append("<PNAME>Po_1</PNAME> <CUSTNAME>John</CUSTNAME> ");
      blr.Append("<SHIPADDR> <STREET>1033, Main Street</STREET> ");
      blr.Append("<CITY>Sunnyvale</CITY> <STATE>CA</STATE> </SHIPADDR> ");
      blr.Append("</PO>");

      // Create a OracleXmlStream from OracleXmlType
      OracleXmlType xml = new OracleXmlType(con, blr.ToString());
      OracleXmlStream strm = new OracleXmlStream(xml);

      // Print the length of xml data in the stream
      Console.WriteLine("OracleXmlStream Length: " + strm.Length);
      Console.WriteLine("");

      // Print the xml data in the stream
      Console.WriteLine("OracleXmlStream Value: " + strm.Value);
      Console.WriteLine("");

      // Check CanRead property on the stream
      Console.WriteLine("OracleXmlStream CanRead: " + strm.CanRead);
      Console.WriteLine("");

      // Check CanWrite property on the stream
      Console.WriteLine("OracleXmlStream CanWrite: " + strm.CanWrite);
      Console.WriteLine("");

      // Print current position in stream
      Console.WriteLine("OracleXmlStream Position: " + strm.Position);
      Console.WriteLine("");

      // Read 10 bytes at a time from the stream
      int rb = 0;
      int curpos = 0;
      byte[] bytebuf = new byte[500];
      while((rb = strm.Read(bytebuf, curpos, 10)) > 0)
        curpos += rb;

      // Print the contents of the byte array
      System.Text.Encoding encoding = System.Text.Encoding.Unicode;
      Console.WriteLine("OracleXmlStream Read byte[]: " +
                        encoding.GetString(bytebuf));
      Console.WriteLine("");

      // Print current position in stream
      Console.WriteLine("OracleXmlStream Position: " + strm.Position);
      Console.WriteLine("");

      strm.Dispose();
      xml.Dispose();
      con.Close();
      con.Dispose();
	  }
  }
}
