/**
 * @author  Jagriti
 * @version 1.0
 *
 * Name of the Application        :  DatabaseSetup.sql
 * Creation/Modification History  :
 *
 *    Chandar        22-Oct-2002       Created
 *
 * Overview of Script: This script performs the clean up and creates tables with integrities 
 * required by the ODP.NET samples.
 * To view the list Database Objects which this file creates, refer to the "Database Setup" 
 * Section in the Readme.html file.
 */

SET serveroutput ON

PROMPT Connecting as SYSTEM user
CONN SYSTEM/&SystemPassword@&&TNSName

DROP USER ORANET CASCADE
/

CREATE USER ORANET IDENTIFIED BY ORANET
/

GRANT CONNECT, RESOURCE TO ORANET
/

ALTER USER ORANET DEFAULT TABLESPACE USERS
/

CONN ORANET/ORANET@&tnsname


DROP TABLE PRODUCTS CASCADE CONSTRAINTS ; 

PROMPT Creating PRODUCTS table
CREATE TABLE PRODUCTS (product_id        NUMBER(5) PRIMARY KEY,
                       product_name      VARCHAR2(200),
                       product_desc      NVARCHAR2(1000),
                       category          VARCHAR2(100),
                       price             NUMBER(15,8),
                       product_status    VARCHAR2(30),
                       weight            NUMBER(37,32),
                       modification_date DATE);

