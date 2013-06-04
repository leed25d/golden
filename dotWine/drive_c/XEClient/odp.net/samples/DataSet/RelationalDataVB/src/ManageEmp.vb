'********************************************************************************
'@author  Jagriti
'@version 1.0
'Development Environment        :  MS Visual Studio .NET
'Name of the File               :  ManageEmp.vb
'Creation/Modification History  :
'                                  23-Sept-2003     Created

'Overview:
'This VB.NET source file is the main file of this sample that does operations 
'of querying data as XML and inserting/updating/deleting records using XML.
'It contains the following methods:
'Method Name             Purpose
'-----------             -------
'generateEmpXml          Retrieves relational data from 'Emp' table and displays
'                        it in XML Format.

'insertEmployee          Inserts record in 'Emp' table using XML data.
'deleteEmployees         Delete records in 'Emp' table using XML data.
'updateEmployees         Updates records in 'Emp' table using XML data.
'createNewRecord         Displays a sample XML record for insertion.
'getEmpSelectedRecord    Retrieves the selected 'Emp' record from the datagrid 
'                        and displays it in XML format.

'populateEmpDataGrid     Populates the DataGrid with data from 'Emp' table.
'populateDeptno          Populates the Deptno list from 'Dept' table.

'NOTE: This sample uses 'Scott' schema available with the Oracle database.
'      For more information refer to the Database Setup section of the Readme.html 
'	  file under the doc directory.
'**********************************************************************************
Imports System
Imports System.Windows.Forms
Imports System.Xml
Imports System.Data
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Namespace RelationalDataSample
    Public Class ManageEmp
        ' <summary>
        ' This class contains methods that does operations for querying 
        ' relational data as XML and manipulating relational data using XML.
        ' </summary>

        '****************************************************************************
        'This method is called when the 'Connect' button is clicked. After the 
        'Oracle connection is successfully established. It accesses relational
        'data from an Oracle database and displays it as XML.
        'The XmlCommandType property denotes XML operations on OracleCommand.
        'The ExecuteXmlReader method of OracleCommand returns a .NET framework 
        'XmlDocument object which is returned as a string.
        '****************************************************************************
        Public Function generateEmpXml() As String
            Try

                ' Represents SQL command for execution, to return results
                Dim empCmd As OracleCommand = New OracleCommand("", ConnectionMgr.conn)
                empCmd.CommandText = "SELECT empno, ename, sal, deptno FROM emp " & _
                                                           " ORDER BY empno DESC"

                ' Represents XML operations on OracleCommand. Setting
                ' OracleXmlCommandType.Query returns result as XML document
                empCmd.XmlCommandType = OracleXmlCommandType.Query

                ' XmlReader provides read-only fast access to XML data,
                ' OracleCommand.ExecuteXMLReader returns an XML document as result
                Dim empReader As XmlReader = empCmd.ExecuteXmlReader()

                ' .NET framework class representing XmlDocument
                Dim empDoc As XmlDocument = New XmlDocument()

                ' Handles white spaces during XmlDocument load process
                empDoc.PreserveWhitespace = True

                ' Loads data from the specified XmlReader
                empDoc.Load(empReader)

                ' Gets markup representing root node and all its children
                Dim str As String = empDoc.OuterXml

                empCmd.Dispose()

                Return str
            Catch ex As Exception
                MessageBox.Show("Error " + ex.Message)
                Return ex.Message
            End Try
        End Function

        '****************************************************************************
        ' This method is called when 'Insert' button is clicked. It takes the entire 
        ' XML document from the 'empRichTextBox' and passses it as a parameter to 
        ' this method. It inserts data into 'Emp' table using the XML document 
        ' passed in. XmlSaveProperties are set for the OracleCommand object. 
        '****************************************************************************
        Public Function insertEmployee(ByVal insertEmployeeTxt As String) As Boolean

            Dim UpdateColumnsList As String() = Nothing

            Try
                Dim insCmd As OracleCommand = New OracleCommand("", ConnectionMgr.conn)

                ' Set the XML document as command text
                insCmd.CommandText = insertEmployeeTxt

                ' Denotes that the inserts are to be made using an XML document
                insCmd.XmlCommandType = OracleXmlCommandType.Insert

                ' List of columns for update
                'UpdateColumnsList = New String(4)
                ReDim UpdateColumnsList(4)
                UpdateColumnsList(0) = "EMPNO"
                UpdateColumnsList(1) = "ENAME"
                UpdateColumnsList(2) = "SAL"
                UpdateColumnsList(3) = "DEPTNO"


                ' Set the XML save properties

                'Specifies list of the columns for insertion
                insCmd.XmlSaveProperties.UpdateColumnsList = UpdateColumnsList

                ' Specifies the name of the table to which changes are saved
                insCmd.XmlSaveProperties.Table = "emp"

                ' Specifies the value for the XML element that identifies 
                ' row of data in the XML document
                insCmd.XmlSaveProperties.RowTag = "ROW"

                ' Execute the insert operation
                insCmd.ExecuteNonQuery()

                MessageBox.Show("Data inserted successfully!")

                insCmd.Dispose()

                Return True

            Catch ex As Exception
                MessageBox.Show("Error " + ex.Message)
                Return False
            End Try
        End Function


        '******************************************************************************
        ' This method is called when the 'Update' button is clicked. It takes the XML 
        ' document from empRichTextBox as a parameter passed to it. 
        ' The XmlSaveProperties for OracleCommand are set and the records are updated.
        '*****************************************************************************
        Public Function updateEmployees(ByVal updText As String) As Boolean

            ' Set the key columns to locate existing row(s) for update
            Dim KeyColumnsList As String()
            ReDim KeyColumnsList(1)

            KeyColumnsList(0) = "EMPNO"

            Dim UpdateColumnsList As String()
            ReDim UpdateColumnsList(3)

            Try
                Dim empCmd As OracleCommand = New OracleCommand("", ConnectionMgr.conn)

                'Denotes that the update has to be made using an XML document
                empCmd.XmlCommandType = OracleXmlCommandType.Update

                ' Set command text to the XML document 
                empCmd.CommandText = updText

                ' List of columns for update
                UpdateColumnsList(0) = "ENAME"
                UpdateColumnsList(1) = "SAL"
                UpdateColumnsList(2) = "DEPTNO"

                ' Set XML save properties
                ' Specifies the name of the table to which changes are saved
                empCmd.XmlSaveProperties.Table = "EMP"

                ' Specifies the value for the XML element that identifies 
                ' row of data in the XML document
                empCmd.XmlSaveProperties.RowTag = "ROW"

                ' Specifies columns that are used to locate row for update
                empCmd.XmlSaveProperties.KeyColumnsList = KeyColumnsList

                ' Specifies columns for update
                empCmd.XmlSaveProperties.UpdateColumnsList = UpdateColumnsList

                ' Execute the update operation
                empCmd.ExecuteNonQuery()

                MessageBox.Show("Data updated successfully!")

                empCmd.Dispose()

                Return True
            Catch ex As Exception
                MessageBox.Show("Error " + ex.Message)
                Return False
            End Try
        End Function

        '******************************************************************************
        ' This method is called when the 'Delete' button is clicked and the user 
        ' confirms deleting of the record. The record(s) for deletion are passed to 
        ' this method in the form of XML from the textbox. 
        '******************************************************************************
        Public Function deleteEmployees(ByVal delText As String) As Boolean

            ' Set the key column to locate existing row(s) for delete
            Dim KeyColumnsList() As String
            ReDim KeyColumnsList(1)
            KeyColumnsList(0) = "EMPNO"

            Try
                Dim empCmd As OracleCommand = New OracleCommand("", ConnectionMgr.conn)

                ' Set XmlCommandType to Delete
                empCmd.XmlCommandType = OracleXmlCommandType.Delete

                empCmd.CommandText = delText

                ' Set key column list 
                empCmd.XmlSaveProperties.KeyColumnsList = KeyColumnsList

                ' Specifies the name of the table to which changes are saved
                empCmd.XmlSaveProperties.Table = "EMP"

                ' Specifies the value for the XML element that identifies 
                ' row of data in the XML document
                empCmd.XmlSaveProperties.RowTag = "ROW"

                ' Execute command
                empCmd.ExecuteNonQuery()

                MessageBox.Show("Data deleted successfully!")

                Return True
            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message)
                Return False
            End Try
        End Function

        '*****************************************************************************
        ' The method is called when the user selects a record in the datagrid. Its 
        ' purpose is to return selected record in XML format. 
        '***************************************************************************
        Public Function getSelectedEmpRecord(ByVal empno As String) As String
            Try
                ' Represents SQL command for execution, to return results
                Dim empCmd As OracleCommand = New OracleCommand("", ConnectionMgr.conn)
                empCmd.CommandText = " SELECT empno, ename, sal, deptno FROM emp " & _
                 " WHERE empno=" + empno + " ORDER BY empno DESC"

                ' Represents XML operations on OracleCommand. By setting
                ' OracleXmlCommandType.Query returns result as XML document
                empCmd.XmlCommandType = OracleXmlCommandType.Query

                ' XmlReader provides read-only fast access to XML data,
                ' OracleCommand.ExecuteXMLReader returns XmlDocument as result
                Dim empReader As XmlReader = empCmd.ExecuteXmlReader()

                ' .NET framework class representing XmlDocument
                Dim empDoc As XmlDocument = New XmlDocument()

                'Handles white spaces during XmlDocument load process
                empDoc.PreserveWhitespace = True

                ' Loads data from the specified XmlReader
                empDoc.Load(empReader)

                ' Gets markup representing root node and all its children
                Dim str As String = empDoc.OuterXml

                empCmd.Dispose()

                Return str
            Catch ex As Exception
                MessageBox.Show("Error " + ex.Message)
                Return ex.Message
            End Try
        End Function

        '*****************************************************************************
        ' This method is called when 'Create New Record' button is clicked. 
        ' It generates a sample XML record for the 'Emp' table and populates the 
        ' empRichTextBox with it. The empno is generated automatically. 
        '***************************************************************************
        Public Function createNewRecord() As String
            Try
                ' Generate unique Empno based on the MAX(empno)+1
                Dim empnoCmd As OracleCommand = New OracleCommand("SELECT NVL((MAX(empno)+1),8000)" & _
                                                      "FROM emp ", ConnectionMgr.conn)
                Dim empnoReader As OracleDataReader = empnoCmd.ExecuteReader()
                empnoReader.Read()
                Dim i As OracleDecimal = empnoReader.GetOracleDecimal(0)

                'Create a sample XML document
                Dim str As String = "<?xml version=""1.0""?>" & _
                    "<ROWSET>" & Environment.NewLine & _
                    "  <ROW>" & Environment.NewLine & _
                    "    <EMPNO>" & i.ToString() & "</EMPNO>" & Environment.NewLine & _
                    "    <ENAME>Angela</ENAME>" & Environment.NewLine & _
                    "    <SAL>1000</SAL>" & Environment.NewLine & _
                    "    <DEPTNO>10</DEPTNO>" & Environment.NewLine & _
                    "  </ROW>" & Environment.NewLine & _
                    "</ROWSET>"

                empnoCmd.Dispose()
                Return str
            Catch ex As Exception

                MessageBox.Show("Error " + ex.Message)
                Return ""

            End Try
        End Function

        '****************************************************************************
        ' This method is called when the 'Connect' button is clicked. After 
        ' Oracle connection is established successfully, it fetches data from 'Emp'
        ' table and returns a DataSet that is bound to the DataGrid.
        '****************************************************************************
        Public Function populateEmpDataGrid() As DataSet

            Dim empAdapter As OracleDataAdapter = New OracleDataAdapter()
            empAdapter.SelectCommand = New OracleCommand("SELECT empno, ename, " & _
                                                         "sal, deptno FROM emp " & _
                                                         "ORDER BY empno DESC", ConnectionMgr.conn)

            Dim empDataSet As DataSet = New DataSet("empDataSet")
            empAdapter.Fill(empDataSet, "emp")

            Return empDataSet
        End Function

        '***********************************************************************
        ' This method is called when the 'Connect' button is clicked. After 
        ' Oracle connection is established successfully, it fetches department
        ' numbers from 'Dept' table and returns as DataSet to fill the list of 
        ' valid department numbers required by user to view while inserting/updating
        ' XML data.
        '*********************************************************************
        Public Function populateDeptno() As DataSet

            Dim deptAdapter As OracleDataAdapter = New OracleDataAdapter()
            deptAdapter.SelectCommand = New OracleCommand("SELECT deptno FROM dept", ConnectionMgr.conn)

            Dim deptDataSet As DataSet = New DataSet("deptDataSet")
            deptAdapter.Fill(deptDataSet, "dept")

            deptAdapter.Dispose()
            Return deptDataSet
        End Function

    End Class
End Namespace



