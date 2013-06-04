/** 
*  @author  : Jagriti
*  @version : 1.0 
* 
*  Name of the Application         :  Updating LOBs using ODP.NET LOB Objects
*  Creation/Modification History   :  18-Feb-2003
* 
*  Overview of  Package/Sample     : This SQL script creates the table 
*  required by this application and inserts data in this table.
*    
*  Parameters required to run this script :
*  1. Database user name 
*  2. Password
*  3. Connect string
*     
**/

SET ECHO OFF


REM *****************************************************************
PROMPT Connecting to the database with given credentials
REM *****************************************************************

CONNECT &username/&password@&connectstring;

REM *****************************************************************
PROMPT Dropping the table if it exists
REM *****************************************************************

DROP TABLE Printmedia;

REM *****************************************************************
PROMPT Creating table Printmedia
REM *****************************************************************

CREATE TABLE Printmedia (
  Product_ID NUMBER(5) PRIMARY KEY,
  Product_Name VARCHAR2(100) ,
  Ad_Image BLOB);  


REM *****************************************************************
PROMPT Inserting Data in Printmedia Table
REM *****************************************************************

INSERT INTO Printmedia (Product_ID, Product_Name) VALUES (1000,'Oracle9i Database');

INSERT INTO Printmedia (Product_ID, Product_Name) VALUES (1001,'Oracle Collaboration Suite');

INSERT INTO Printmedia (Product_ID, Product_Name) VALUES (1002,'Oracle9i JDeveloper');

INSERT INTO Printmedia (Product_ID, Product_Name) VALUES (1003,'Oracle9i Application Server');

INSERT INTO Printmedia (Product_ID, Product_Name) VALUES (1004,'Oracle9i Developer Suite');
  

REM *****************************************************************
PROMPT Updating Ad_Image column to Empty_Blob
REM *****************************************************************

UPDATE Printmedia SET Ad_Image = EMPTY_BLOB();

COMMIT;

REM *****************************************************************
PROMPT Data Setup Completed
REM *****************************************************************