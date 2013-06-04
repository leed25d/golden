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
 * and other database objects like sequence, trigger required by the Oracle Provider for OLEDB samples.
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

DROP SEQUENCE PRODID_SEQ;

PROMPT Creating PRODID_SEQ Sequence

CREATE SEQUENCE PRODID_SEQ START WITH 1200;

DROP SEQUENCE ADID_SEQ;

PROMPT Creating ADID_SEQ Sequence

CREATE SEQUENCE ADID_SEQ START WITH 1200;

PROMPT Creating POPULATE_PRODUCTID trigger

CREATE OR REPLACE TRIGGER POPULATE_PRODUCTID
 BEFORE INSERT ON PRODUCTS FOR EACH ROW
DECLARE
TEMPVAL NUMBER;
BEGIN
  SELECT PRODID_SEQ.NEXTVAL INTO TEMPVAL FROM DUAL;
  :NEW.PRODUCT_ID := TEMPVAL;
END;
/

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
1000, 'White Kids T-Shirt ', 'This 100otton white T-shirt is decorated with the words "Oracle Kids" in many
colors across the front. '
, 'clothes', 9, 'under development'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1001, 'White Mugs', 'Long nights in front of the monitor got you down? Perk up with some hot java in this
classic E-Business Network 15 oz. stoneware mug. '
, 'accessories', 5, 'under development'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1002, 'Team Oracle Hat ', 'Support Team Oracle by wearing the official Team Oracle Hat! This is a black baseball
style hat with the Team Oracle logo embroidered on the front. '
, 'accessories', 12, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1003, 'Palm Pilot Case ', 'Black leather case features a zip closure, microfiber lining, Velcro holder, business
card slots and pen loop. Oracle logo is debossed on the front. '
, 'bags and cases', 20, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1004, 'Kenneth Cole Woman''s Watch', 'Set the standard in style with this fashionable timepiece. Kenneth Cole watch
features black leather strap, silver-tone bezel and white dial. Oracle logo screened in black on dial. '
, 'accessories', 48, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1005, 'Gemini Pen/Pencil Set', 'Black matte finish, solid brass ball-point pen with black ink and a 0.5mm mechanical
lead pencil. Each features black rubber grip, silver trim and click-action mechanism.Silver Oracle logo is screened on each barrel. '
, 'desk items', 10, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1006, 'Golf Balls and Tees', 'This is perfect for golf lovers. A convenient kit which includes 2 Pinnacle Gold golf
balls and 9 tees. All have the Oracle logo. '
, 'sports', 8.75, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1007, 'Flip Calculator', 'Hand size calculator with a flip up window. ', 'desk items'
, 7.25, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1008, 'Team Oracle T-shirt ', 'Support Team Oracle by wearing the official Team Oracle T-shirt! This is a white
Gildan Ultra Cotton shirt with the Team Oracle logo screened on the front. '
, 'clothes', 9, 'orderable'); 
INSERT INTO PRODUCTS ( PRODUCT_ID, PRODUCT_NAME, PRODUCT_DESC, CATEGORY, PRICE, PRODUCT_STATUS ) VALUES ( 
1009, 'Denim Shirt ', '100tonewashed cotton denim button down shirt with a chest pocket. The Oracle
logo is embroidered in red above the pocket. '
, 'clothes', 42, 'orderable'); 

commit;

