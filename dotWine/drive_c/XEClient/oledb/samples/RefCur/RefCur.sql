CREATE OR REPLACE PACKAGE Employees AS
  TYPE empcur IS REF CURSOR;

  PROCEDURE GetEmpRecords(p_cursor OUT empcur,
                          indeptno IN NUMBER,
                          p_errorcode OUT NUMBER); 

  FUNCTION GetDept(inempno IN NUMBER,  
                   p_errorcode OUT NUMBER)      
    RETURN empcur; 
END Employees; 

/

CREATE OR REPLACE PACKAGE BODY Employees AS  

  PROCEDURE GetEmpRecords(p_cursor OUT empcur,  
                          indeptno IN NUMBER,  
                          p_errorcode OUT NUMBER) IS    
  BEGIN   
    p_errorcode := 0;      
    OPEN p_cursor FOR        
      SELECT *        
      FROM emp  
      WHERE deptno = indeptno        
      ORDER BY empno;     

  EXCEPTION  
    WHEN OTHERS THEN        
      p_errorcode:= SQLCODE;     

  END GetEmpRecords;   

  FUNCTION GetDept(inempno IN NUMBER,  
                   p_errorcode OUT NUMBER)      
    RETURN empcur IS  
      p_cursor empcur;    
  BEGIN       
    p_errorcode := 0;      
    OPEN p_cursor FOR  
      SELECT deptno        
      FROM emp        
      WHERE empno = inempno;  
    RETURN (p_cursor);     

  EXCEPTION      
    WHEN OTHERS THEN  
      p_errorcode:= SQLCODE;    

  END GetDept;    

END Employees; 

/
