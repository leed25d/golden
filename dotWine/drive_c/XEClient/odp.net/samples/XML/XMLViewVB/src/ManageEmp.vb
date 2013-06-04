''**************************************************************************************
'@author  Jagriti
'@version 1.0
'Development Environment        :  MS Visual Studio .NET
'Name of the File               :  ManageEmp.vb
'Creation/Modification History  :
'                                  24-Sep-2003     Created

'Overview:
'This VB.NET file contains methods used for insert/update/delete/query operations 
'on the XMLType view based on the relational table 'Emp'.

' Method Name              Purpose
' -----------				 -------

' getXmlFromDB             Retrieves data from XMLType view to display in the textbox.
' insertRecord             Inserts record into the 'Emp_View'.
' updateRecord			   Updates a record in the 'Emp_View' based on the 'empno' key
'                          column.
' deleteRecord             Deletes a record from the 'Emp_View' based on the 'empno'
'                          key column.
' getSelectedEmpRecord     Retrieves XML data from the 'Emp_View' based on the 
'                          selected record.
' populateEmpDataGrid	   Retrieves data from XMLType view to display in the datagrid.
' createNewRecord          Creates and displays a sample 'Emp_View' record in the textbox.
' populateDeptno           Populates the Deptno list from the 'Dept' table.

' NOTE: 
'      To insert/update/delete data from a database view, instead-of triggers are used. 
'	   In this sample, instead-of-triggers, 'Insert_Emp_Trig', 'Update_Emp_trig' and
'      'Delete_Emp_Trig' defined on the 'Emp_View' are used to manipulate data 
'	   in the 'Emp' table.  These triggers were created by createview.sql.

'	  For more information on how to create instead-of triggers, refer to the
'     'Database Setup' section of the Readme.html file available in the 'doc' folder.                   
'***************************************************************************************

Imports System
Imports System.Windows.Forms
Imports System.Xml
Imports System.Text
Imports System.Data
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types


Namespace XMLViewSample

    ' <summary>
    '   This class contains methods that perform insert/update/delete/query operations 
    '   on the XmlType view, 'Emp_View' which is based on the relational table, 'Emp'. 
    ' </summary>
    Public Class ManageEmp

        '*********************************************************************
        ' The purpose of this method is to retrieve data from the XMLType 
        ' view, 'Emp_View' which is based on the relational table, 'Emp'.
        ' The XML data is fetched as OracleXmlType using OracleDataReader and 
        ' returned as a string.
        ' ********************************************************************
        Public Function getXmlFromDB() As String

            ' ODP.NET native XML data type object
            Dim empXml As OracleXmlType

            ' OracleCommand object
            Dim empCmd As OracleCommand = New OracleCommand()

            ' Query XMLType view, 'Emp_View'
            empCmd.CommandText = "SELECT * FROM emp_view t " & _
                              "ORDER BY EXTRACTVALUE(VALUE(t), '/EMP/EMPNO/text()') DESC"
            empCmd.Connection = ConnectionMgr.conn

            ' Execute OracleCommand
            Dim empReader As OracleDataReader = empCmd.ExecuteReader()

            Dim str As String = ""

            ' Continue to read data if it exists
            While (empReader.Read())

                ' Return OracleXmlType object of the specified XmlType column
                empXml = empReader.GetOracleXmlType(0)

                ' Concatenate output for all the records for display in the textbox
                str = str & empXml.Value
            End While
            empCmd.Dispose()
            Return str
        End Function

        '*********************************************************************************
        ' This method is called when the insert button is clicked. The purpose of this 
        ' method is to insert an XML record into 'Emp_View'. Using 'Create New Record' 
        ' button, a sample XML record is displayed in the textbox, which is passed as a
        ' string parameter to this method. To insert, the XML record for 'Emp_View' 
        ' is bound as a parameter to the OracleCommand object.
        '*******************************************************************************
        Public Function insertRecord(ByVal rec As String) As Boolean
            Try

                ' OracleCommand for insert operation
                Dim insCmd As OracleCommand = New OracleCommand("", ConnectionMgr.conn)

                ' Declare the SQL statement to insert the XML record into the view
                insCmd.CommandText = "INSERT INTO emp_view VALUES (:1)"
                insCmd.CommandType = CommandType.Text

                ' Bind the XML record as parameter to the OracleCommand
                insCmd.Parameters.Add(":1", OracleDbType.XmlType, rec, ParameterDirection.Input)

                ' Execute the insert command
                insCmd.ExecuteNonQuery()

                MessageBox.Show("Record inserted successfully !")
                insCmd.Dispose()

                Return True

            Catch ex As Exception

                MessageBox.Show("error :" + ex.Message + ex.StackTrace)
                Return False
            End Try
        End Function

        '********************************************************************************
        ' This method is called when the 'Update' button is clicked. The purpose of this
        ' method is to update the 'Emp_View' record based on the 'Empno' key column.
        ' An OracleXmlType object based on the selected record from the dataGrid is 
        ' created. The OracleXmlType is updated using the 'OracleXmlType.Update' method.
        ' This updated OracleXmlType object is bound as a parameter to an OracleCommand 
        ' for updating the XmlType view.
        '*******************************************************************************
        Public Function updateRecord(ByVal rec As String, ByVal keycolumn As String) As Boolean

            Try

                ' Creating an OracleXMLType object 
                Dim ox As OracleXmlType = New OracleXmlType(ConnectionMgr.conn, rec)

                ' OracleCommand object for updating the 'Emp_View' in the database
                Dim cmd As OracleCommand = New OracleCommand("", ConnectionMgr.conn)

                ' Declare the SQL statement to update the XML record in the view
                ' Use XPATH to identify the 'empno' primary key
                cmd.CommandText = "UPDATE emp_view t SET value(t)=  :1 " & _
                                  " WHERE extractvalue(value(t), '/EMP/EMPNO')=" & keycolumn
                cmd.CommandType = CommandType.Text

                ' Bind the updated OracleXmlType object to the OracleCommand
                cmd.Parameters.Add(":1", OracleDbType.XmlType, ox, ParameterDirection.Input)

                ' Execute the update command
                cmd.ExecuteNonQuery()

                MessageBox.Show("Record updated successfully !")

                cmd.Dispose()

                Return True

            Catch ex As Exception

                MessageBox.Show("error :" + ex.Message + ex.StackTrace)
                Return False
            End Try
        End Function

        '**********************************************************************************
        ' This method is called when the 'Delete' button is clicked. The purpose of this
        ' method is to delete a record from the 'Emp_View' that is selected from the 
        ' datagrid. Deletion happens based on the 'Empno' key column of the selected record.
        '***********************************************************************************
        Public Function deleteRecord(ByVal xmlRec As String) As Boolean

            Try

                ' OracleCommand object for delete operation
                Dim delCmd As OracleCommand = New OracleCommand("", ConnectionMgr.conn)

                ' Use OracleXmlType to extract the 'empno' primary key to identify the record
                ' to be deleted
                Dim oraxml As OracleXmlType = New OracleXmlType(ConnectionMgr.conn, xmlRec)
                Dim empno As String = oraxml.Extract("/EMP/EMPNO/text()", "").Value

                ' Declare the SQL statement to delete the XML record from the view
                delCmd.CommandText = "DELETE FROM emp_view t WHERE EXTRACTVALUE(VALUE(t), " & _
                 "'/EMP/EMPNO')=" + empno
                delCmd.CommandType = CommandType.Text

                ' Execute the delete command
                delCmd.ExecuteNonQuery()

                MessageBox.Show("Record deleted successfully")

                oraxml = Nothing

                Return True

            Catch ex As Exception

                MessageBox.Show("error: " + ex.Message)
                Return False
            End Try
        End Function


        '******************************************************************************
        ' The purpose of this method is to retieve data from the XmlType view based on
        ' the record selected from the 'empDataGrid'.
        '*****************************************************************************
        Public Function getSelectedEmpRecord(ByVal empno As String) As String

            Dim empXml As OracleXmlType
            Dim empCmd As OracleCommand = New OracleCommand()

            ' Retrieve data for the current/selected record from the datagrid
            ' Use the XPath expression to fetch the required record based on
            ' key column 'EmpNo'  
            empCmd.CommandText = "SELECT * FROM emp_view t WHERE EXTRACTVALUE(VALUE(t), " & _
                                 "'/EMP/EMPNO') = " + empno

            ' Use the existing connection variable
            empCmd.Connection = ConnectionMgr.conn

            ' Execute commnad text and return OracleDataReader object
            Dim empreader As OracleDataReader = empCmd.ExecuteReader()

            Dim str As String = ""

            ' If record exists
            While (empreader.Read())

                ' Retrieve data into OracleXmlType object
                empXml = empreader.GetOracleXmlType(0)

                ' Return Xml data as string
                str = empXml.Value
            End While

            empCmd.Dispose()
            Return (str)
        End Function

        '****************************************************************************
        ' This method is called when the 'Connect' button is clicked. After 
        ' Oracle connection is established successfully, it fetches data from 
        ' 'Emp_View' XmlType view and returns a DataSet that is bound to the datagrid.
        ' 
        ' Note: The extractValue() function is used to return the value of a text
        ' node or attribute associated with an XPath expression from an XML document
        ' stored as an XMLType. It returns a scalar data type.
        '***************************************************************************
        Public Function populateEmpDataGrid() As DataSet

            Dim empadapter As OracleDataAdapter = New OracleDataAdapter()

            ' Retrieve data from the XMLType View. Also specifying alias names
            ' for columns
            empadapter.SelectCommand = New OracleCommand("SELECT " & _
             " EXTRACTVALUE(VALUE(t), '/EMP/EMPNO/text()') EmpNo," & _
            " EXTRACTVALUE(VALUE(t), '/EMP/ENAME/text()') Ename, " & _
                " EXTRACTVALUE(VALUE(t), '/EMP/JOB/text()') job, " & _
                " EXTRACTVALUE(VALUE(t), '/EMP/SAL/text()') sal, " & _
            "EXTRACTVALUE(VALUE(t), '/EMP/DEPTNO/text()') deptno " & _
                               " FROM emp_view t ", ConnectionMgr.conn)



            ' Create a DataSet object
            Dim empDataSet As DataSet = New DataSet("empDataSet")

            ' Fill DataSet with records from 'Emp_View'
            empadapter.Fill(empDataSet, "emp_view")

            Return empDataSet
        End Function

        '*************************************************************************
        ' This method is called when 'Create New Record' button is clicked. 
        ' It generates a sample XML record for the 'Emp' table and populates the 
        ' empRichTextBox with it. The empno is generated automatically. 
        '*************************************************************************
        Public Function createNewRecord() As String

            Try

                ' Generate unique Empno based on the MAX(empno)+1
                Dim empnoCmd As OracleCommand = New OracleCommand("SELECT  NVL(MAX(EXTRACTVALUE(VALUE(t), '/EMP/EMPNO/text()')) + 1,8000) " & _
                                                   "FROM emp_view t", ConnectionMgr.conn)

                ' Retrieve the empno value to ODP.NET
                Dim empnoReader As OracleDataReader = empnoCmd.ExecuteReader()
                empnoReader.Read()
                Dim i As OracleDecimal = empnoReader.GetOracleDecimal(0)

                ' Create a sample XML document
                Dim str As String = "<EMP EMPNO = """ & i.ToString & """>" & Environment.NewLine & _
                                           "  <ENAME>Angela</ENAME>" & Environment.NewLine & _
                                              "  <JOB>MANAGER</JOB>" & Environment.NewLine & _
                                                 "  <SAL>3000</SAL>" & Environment.NewLine & _
                                             "  <DEPTNO>20</DEPTNO>" & Environment.NewLine & _
                                                            "</EMP>"

                empnoCmd.Dispose()
                Return str

            Catch ex As Exception

                MessageBox.Show("Error " + ex.Message)
                Return ""
            End Try
        End Function

        '****************************************************************************
        ' This method is called when the 'Connect' button is clicked. After 
        ' Oracle connection is established successfully, it fetches department
        ' numbers from 'Dept' table and returns as DataSet to fill the list of 
        ' valid department numbers required by user to view while inserting/updating
        ' XML data.
        '***************************************************************************
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
