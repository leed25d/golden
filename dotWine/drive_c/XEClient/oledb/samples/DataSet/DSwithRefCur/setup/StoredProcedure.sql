CREATE OR REPLACE PACKAGE oraoledb AS

 /**********************************************************************************
 *  @author  Jagriti
 *  @version 1.0
 *  Name of the File               :  StoredProcedure.sql
 *  Creation/Modification History  :
 *                                  10-JUL-2002     Created
 *  Overview of Package:
 *  This package specification contains definition of a ref cursor type .
 *  The variables of this type will be used by stored procedures in the application
 *  to send multiple records from database to application. 
 ***********************************************************************************/      
  TYPE refcur IS REF CURSOR;
  PROCEDURE getProductsInfo(orderable OUT oraoledb.refcur,udevelopment OUT oraoledb.refcur);
  
END oraoledb;
/

--*******************************************************************
 --Procedure to get the product information using ref cursor arguments
 --*******************************************************************
CREATE OR REPLACE PACKAGE BODY oraoledb is 
PROCEDURE getProductsInfo(orderable OUT oraoledb.refcur,udevelopment OUT oraoledb.refcur) IS
 
 /**
 *  @author  Jagriti
 *  @version 1.0
 *  Name of the File               :  StoredProcedure.sql
 *  Creation/Modification History  :
 *                                  10-JUL-2002     Created
 *  Overview of the Procedure:

 *  It gets the information of products of two categories ('orderable' and 'under development' in two 
 *  different REF CURSOR variables

 *  Parameters:
 *  @orderable - OUT - A Ref Cursor variable passed as OUT parameter GetProductInfo 
 *  @udevelopment - OUT- A Ref Cursor variable passed as OUT parameter GetProductInfo.
 **/

 BEGIN

    --get records for products  with category as 'orderable' in orderable ref cursor
    OPEN orderable FOR SELECT
    product_id, product_name,Product_desc, category, price FROM
    products WHERE lower(product_status)='orderable'
    ORDER BY product_id;

    --get records for products  with category as 'under development' in orderable ref cursor
   OPEN udevelopment FOR SELECT
    product_id, product_name,Product_desc, category, price FROM
    products WHERE lower(product_status)='under development'
    ORDER BY product_id;

    
END getProductsInfo;
END;
/

sho err


