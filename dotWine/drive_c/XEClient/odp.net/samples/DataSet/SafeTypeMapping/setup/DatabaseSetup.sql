/**
 * @author  Chandar
 * @version 1.0
 *
 * Name of the Application        :  DatabaseSetup.sql
 * Creation/Modification History  :
 *
 *    Chandar        12-Oct-2002       Created
 *
 * Overview of Script: This script performs the clean up and creates tables with integrities 
 * and other database objects like sequence, trigger required by the ODP.NET samples.
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

PROMPT Inserting data into PRODUCTS table


INSERT INTO PRODUCTS  (PRODUCT_ID,PRODUCT_NAME,PRODUCT_DESC,CATEGORY,PRICE,weight)
values(1010,'Platinum Pendant','Platinum Diamond Spiga Donut Pendant'
,'Jewel',765.678,45.857);

INSERT INTO PRODUCTS  (PRODUCT_ID,PRODUCT_NAME,PRODUCT_DESC,CATEGORY,PRICE,WEIGHT)
values(1011,'Coat Broach','Broach with a beautiful large purple cut rhinestone' ,
'Jewel',3343.678,155.857);

INSERT INTO PRODUCTS  (PRODUCT_ID,PRODUCT_NAME,PRODUCT_DESC,CATEGORY,PRICE,WEIGHT) 
values(1012,'Gold Necklace','Pure Gold necklace with design having touch of ancient Indian tradition'
,'Jewel',43.444,55.343);

INSERT INTO PRODUCTS  (PRODUCT_ID,PRODUCT_NAME,PRODUCT_DESC,CATEGORY,PRICE,WEIGHT)
values(1013,'Diamond Ring','Ring with 21 diamonds','Jewel',223.444,22.343);

INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES 
( 1000, 'Sports cap', 'White color, pure cotton', 'clothes', 3, 'orderable'); 

INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES
(1002, 'Kids T-Shirt', 'Soft T-shirt with MickeyMouse logo', 'clothes', 7, 'orderable'); 

INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES 
(1003, 'Black Jacket', 'Trendy range, with 4 pockets', 'clothes', 8, 'orderable'); 

INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS,WEIGHT ) VALUES 
( 1001, 'Pen set', 'Colored pen set', 'stationary', 4, 'under development',11); 

commit;

