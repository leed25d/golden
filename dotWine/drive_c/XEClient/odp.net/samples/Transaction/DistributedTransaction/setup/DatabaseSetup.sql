/**
 * @author                        :  Jagriti
 * @version                       :  1.0
 *
 * Name of the Application        :  DatabaseSetup.sql
 * Creation/Modification History  :
 *
 *    Jagriti        30-Oct-2002       Created
 *
 * Overview of Script: This script creates the database users with 
 * tables required for DistributedTransactionSample.
 * The created users are:
 *                      1. HQDODP
 *                      2. REGIONODP
 * You can specify the TNSName of the database in which you wishes to create the
 * above mentioned users. A table 'Products' is created both the users.
 */

SET serveroutput ON


PROMPT Connecting as SYSTEM user 
CONN SYSTEM/&SystemPassword@&&tnsname

PROMPT Creating HQODP database Schema
DROP USER HQODP CASCADE
/

CREATE USER HQODP IDENTIFIED BY HQODP
/

GRANT CONNECT, RESOURCE TO HQODP
/

ALTER USER HQODP DEFAULT TABLESPACE USERS
/

PROMPT Creating REGIONODP database Schema
DROP USER REGIONODP CASCADE
/

CREATE USER REGIONODP IDENTIFIED BY REGIONODP
/

GRANT CONNECT, RESOURCE TO REGIONODP
/

ALTER USER REGIONODP DEFAULT TABLESPACE USERS
/

PROMPT Connecting to HQODP/HQODP schema
CONN HQODP/HQODP@&tnsname


DROP TABLE PRODUCTS CASCADE CONSTRAINTS ; 

PROMPT Creating Products table in HQODP schema

CREATE TABLE PRODUCTS ( 
  PRODUCT_ID    NUMBER (5)    NOT NULL, 
  PRODUCT_NAME  VARCHAR2 (200), 
  PRICE         NUMBER (10,2) ) ; 

PROMPT Inserting data in Products table 

INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRICE ) VALUES ( 
1000, 'Sports cap', 3); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRICE ) VALUES ( 
1002, 'Kids T-Shirt', 7); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRICE ) VALUES ( 
1003, 'Black Jacket', 8); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRICE ) VALUES ( 
1001, 'Pen set', 4); 
commit;
 

PROMPT Connecting to REGIONODP/REGIONODP schema
CONN REGIONODP/REGIONODP@&tnsname


DROP TABLE PRODUCTS CASCADE CONSTRAINTS ; 

PROMPT Creating Products table in REGIONODP schema

CREATE TABLE PRODUCTS ( 
  PRODUCT_ID    NUMBER (5)    NOT NULL, 
  PRODUCT_NAME  VARCHAR2 (200), 
  PRICE         NUMBER (10,2) ) ; 

INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRICE ) VALUES ( 
1000, 'Sports cap', 3); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRICE ) VALUES ( 
1002, 'Kids T-Shirt', 7); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRICE ) VALUES ( 
1003, 'Black Jacket', 8); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRICE ) VALUES ( 
1001, 'Pen set', 4); 
commit;
 

