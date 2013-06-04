===============
ODP.NET Samples
===============

ODP.NET samples cover the following topics.  
Each topic has a solution (.sln) file as noted below:
1) LOB            (%ORACLE_HOME%\ODP.NET\Samples\LOB\LOB.sln and more)
2) REF Cursor     (%ORACLE_HOME%\ODP.NET\Samples\RefCursor\REfCursor.sln and more)
3) Array Binding  (%ORACLE_HOME%\ODP.NET\Samples\ArrayBind\ArrayBind.sln)
4) Assoc. Array   (%ORACLE_HOME%\ODP.NET\Samples\AssocArray\AssocArray.sln)
5) DataSet        (%ORACLE_HOME%\ODP.NET\Samples\DataSet)
6) Globalization  (%ORACLE_HOME%\ODP.NET\Samples\Globalization)
7) TAF            (%ORACLE_HOME%\ODP.NET\Samples\TAF)
8) Transaction    (%ORACLE_HOME%\ODP.NET\Samples\Transaction)
9) XML            (%ORACLE_HOME%\ODP.NET\Samples\XML\XML.sln and more)
10)EventHandler   (%ORACLE_HOME%\ODP.NET\Samples\EventHandler\RowUpdateEventHandler\src)
11)WebService     (%ORACLE_HOME%\ODP.NET\Samples\WebService\OdpNetWs\src)

Following are the requirements for running the samples:
1) Appropriate modification of the Data Source attribute in the 
   connection strings
2) A SCOTT/TIGER schema in the Oracle database.
3) A correct reference path to Oracle.DataAccess.dll.
4) Read <sample>\doc\Readme.html, if any. 


Sample Descriptions:

===========
LOB Samples 
===========

Sample 1: Demonstrates how an populate and obtain LOB data
          from a DataSet.

Sample 2: Demonstrates how an OracleClob object is obtained 
          as an output parameter of an anonymous PL/SQL block

Sample 3: Demonstrates how an OracleClob object is obtained 
          from an output parameter of a stored procedure

Sample 4: Demonstrates how the LOB column data can be
          read as a .NET type by utilizing stream reads.

Sample 5: Demonstrates how to bind an OracleClob object 
          as a parameter.  This sample also refetches the newly 
          updated CLOB data using an OracleDataReader and an 
          OracleClob object.

Sample6: Demostrates LOB updates using row-level locking.

Sample7: Demostrates LOB updates using result set locking.

AccessBFile: demonstrates accessing BFILEs through ODP.NET.

UpdLob: demonstrates how LOBs can be updated using ODP.NET LOB objects.


==================
REF CURSOR SAMPLES
==================

Sample 1: Demonstrates how a REF Cursor is obtained as an 
          OracleDataReader

Sample 2: Demonstrates how a REF Cursor is obtained as an 
          OracleDataReader through the use of an OracleRefCursor object.

Sample 3: Demonstrates how multiple REF Cursors can be accessed 
          by a single OracleDataReader

Sample 4: Demonstrates how a DataSet can be populated from a 
          REF Cursor. The sample also demonstrates how a REF 
          Cursor can be updated.

Sample 5: Demonstrates how a DataSet can be populated from an
          OracleRefCursor object.

Sample 6: Demonstrates how to populate a DataSet with 
          multiple REF Cursors selectively

Sample 7: Demonstrates how to selectively obtain 
          OracleDataReader objects from the REF Cursors

UpdRefCursor: demonstrates how the data retrieved in a Data Set populated 
	using REF CURSORs is updated through ODP.NET.


====================
ARRAY BINDING SAMPLE
====================

Sample: Demonstrates array binding

===================
ASSOC. ARRAY SAMPLE
===================

Sample: Demonstrates PL/SQL Associative Array binding

==============
DATASET SAMPLE
==============

Sample 1: DMLOperOnDS
Sample 2: DSDRwithLOB
Sample 3: DSPopulate
Sample 4: DSPopulateVB
Sample 5: DSwithRefCur
Sample 6: SafeTypeMapping
Sample 7: RelationalData
Sample 8: RelationalDataVB


=============
Globalization
=============

Sample 1: Globalization

===
TAF
===

Sample 1: Failover

===========
TRANSACTION
===========

Sample 1: DistributedTransactionSample
Sample 2: SavepointSample

===========
XML Samples 
===========

XmlProp:        Demonstrates how to use the OracleXmlQueryProperties class.
                Using the OracleXmlSaveProperties class is similar.

XmlQuery:       Demonstrates how to retrieve relational data as an
                XML document.

XmlSave:        Demonstrates how to save changes to relational data
                using an XML document.

XMLStreamProp:  Demonstrates properties and methods of OracleXmlStream

XMLTypeBind:    Demonstrates how to bind OracleXmlType as input parameter

XMLTypeDemo:    Demonstrates properties and methods of OracleXmlType

XMLTypeDS:      Demonstrates how to populate and obtain XMLType data
                from a DataSet.

XMLTypeGet:     Demonstrates how to get XMLType column as Oracle Type or
                .NET type

XMLView:        Usage of OracleXmlType class to retrieve native XML, etc.

XMLViewVB:      VB version -  Usage of OracleXmlType class to retrieve 
                native XML, etc.

XMLTypeSBProp:  Demonstrates properties and methods of OracleXmlType for 
                schema based XML data.

============
EventHandler
============

RowUpdateEventHandler: demonstrate how one can trap the OracleRowUpdatingEvent 
	and OracleRowUpdatedEvent using VB.NET.


==========
WebService
==========

OdpNetWs: demonstrate how Oracle Data Provider for .NET (ODP.NET) can be used in 
	a .NET Web Service. This application also shows how ASP.NET DataGrid can 
	be used in conjunction with .NET Web Services and ODP.NET.  
