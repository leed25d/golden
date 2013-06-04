/**
 * @author  Jagriti
 * @version 1.0
 *
 * Name of the Application        :  DatabaseSetup.sql
 * Creation/Modification History  :
 *
 *    Jagriti        27-June-2002       Created
 *
 * Overview of Script: This script performs the clean up and creates tables with integrities 
 * and other database objects like sequence, trigger required by the Oracle Data Provider for 
 * .Net samples. To view the list Database Objects which this file creates, refer to the 
 " "Database Setup" Section in the Readme.html file.
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

CREATE TABLE PRODUCTS ( 
  PRODUCT_ID      NUMBER (5)    NOT NULL, 
  PRODUCT_NAME    VARCHAR2 (200), 
  PRODUCT_DESC    VARCHAR2 (500), 
  CATEGORY        VARCHAR2 (100), 
  PRICE           NUMBER (10,2), 
  PRODUCT_STATUS  VARCHAR2 (30), 
  PRIMARY KEY ( PRODUCT_ID ) ) ; 


DROP TABLE PRINTMEDIA CASCADE CONSTRAINTS ; 

PROMPT Creating PRINTMEDIA table

CREATE TABLE PRINTMEDIA ( 
  AD_ID             NUMBER (5)    NOT NULL, 
  PRODUCT_ID        NUMBER (5), 
  AD_TEXT           VARCHAR2 (200), 
  AD_IMAGE          BLOB, 
  DATE_OF_CREATION  DATE, 
  PRIMARY KEY ( AD_ID ) ) ; 

ALTER TABLE PRINTMEDIA ADD 
 FOREIGN KEY (PRODUCT_ID) 
  REFERENCES PRODUCTS (PRODUCT_ID) ;


DROP SEQUENCE ADID_SEQ;

PROMPT Creating ADID_SEQ Sequence

CREATE SEQUENCE ADID_SEQ START WITH 1200;


PROMPT Creating POPULATE_ADID trigger

CREATE OR REPLACE TRIGGER POPULATE_ADID
 BEFORE INSERT ON PRINTMEDIA FOR EACH ROW
DECLARE
TEMPVAL NUMBER;
BEGIN
  SELECT ADID_SEQ.NEXTVAL INTO TEMPVAL FROM DUAL;
  :NEW.AD_ID := TEMPVAL;
END;
/

PROMPT Inserting data into PRODUCTS table

INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1000, 'Sports cap', 'White color, pure cotton', 'clothes', 3, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1002, 'Kids T-Shirt', 'Soft T-shirt with MickeyMouse logo', 'clothes', 7, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1003, 'Black Jacket', 'Trendy range, with 4 pockets', 'clothes', 8, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1001, 'Pen set', 'Colored pen set', 'stationary', 4, 'under development'); 
commit;
 
