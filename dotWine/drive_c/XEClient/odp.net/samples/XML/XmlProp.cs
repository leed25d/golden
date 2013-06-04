using System;
using System.Data;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace XmlProp
{
  /// <summary>
  /// XmlProp : Demonstrates how to use the OracleXmlQueryProperties class.
  ///           Using the OracleXmlSaveProperties class is similar.
  /// </summary>
  class XmlProp
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

      // Create the command.
      OracleCommand cmd = new OracleCommand("", con);

      // Set the XML properties directly on the xml query properties 
      // of the OracleCommand object
      Console.WriteLine("Set the XML properties directly on OracleCommand object.");

      Console.WriteLine("Set row tag to ROW.");
      cmd.XmlQueryProperties.RowTag = "ROW";
      Console.WriteLine("Row tag: " + cmd.XmlQueryProperties.RowTag);
      
      Console.WriteLine("Set row tag to EMPLOYEE.");
      cmd.XmlQueryProperties.RowTag = "EMPLOYEE";
      Console.WriteLine("Row tag: " + cmd.XmlQueryProperties.RowTag);
      
      Console.WriteLine("\n");

      // Set the XML properties using an OracleXmlQueryProperties object.
      Console.WriteLine("Set the XML properties using an OracleXmlQueryProperties object.");
      OracleXmlQueryProperties qprops = new OracleXmlQueryProperties();

      Console.WriteLine("Set row tag to ROW on OracleXmlQueryProperties object.");
      qprops.RowTag = "ROW";

      Console.WriteLine("Set the new XML Query properties on the OracleCommand.");
      cmd.XmlQueryProperties = qprops;

      Console.WriteLine("Set row tag to EMPLOYEE on OracleXmlQueryProperties object.");
      qprops.RowTag = "EMPLOYEE";

      Console.WriteLine("Row tag on OracleXmlQueryProperties object: " + qprops.RowTag);
      Console.WriteLine("Row tag on OracleCommand object: " + cmd.XmlQueryProperties.RowTag);
      Console.WriteLine("\n");
      
      Console.WriteLine("Set row tag to ROW on OracleCommand object.");
      cmd.XmlQueryProperties.RowTag = "ROW";

      // Clone the XmlQueryProperties from the OracleCommand object.
      Console.WriteLine("Clone the XmlQueryProperties from the OracleCommand object.");
      OracleXmlQueryProperties qpropsClone = (OracleXmlQueryProperties)cmd.XmlQueryProperties.Clone();
      
      Console.WriteLine("Set row tag to EMPLOYEE on OracleCommand object.");
      cmd.XmlQueryProperties.RowTag = "EMPLOYEE";

      Console.WriteLine("Row tag on OracleCommand object: " + cmd.XmlQueryProperties.RowTag);
      Console.WriteLine("Row tag on Cloned OracleXmlQueryProperties object: " + qpropsClone.RowTag);
      Console.WriteLine("\n");

      Console.WriteLine("Set row tag to ROW on OracleCommand object.");
      cmd.XmlQueryProperties.RowTag = "ROW";
      
      // Clone the OracleCommand object.
      Console.WriteLine("Clone the OracleCommand object.");
      OracleCommand cmdClone = (OracleCommand)cmd.Clone();
      
      Console.WriteLine("Set row tag to EMPLOYEE on OracleCommand object.");
      cmd.XmlQueryProperties.RowTag = "EMPLOYEE";

      Console.WriteLine("Row tag on OracleCommand object: " + cmd.XmlQueryProperties.RowTag);
      Console.WriteLine("Row tag on Cloned OracleCommand object: " + cmdClone.XmlQueryProperties.RowTag);
      Console.WriteLine("\n");

      // Clean up.
      cmd.Dispose();
      cmdClone.Dispose();
      con.Close();
      con.Dispose();
	  }
  }
}
