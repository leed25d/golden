/**
 * @author                        :  Jagriti
 * @version                       :  1.0
 *
 * Name of the Application        :  CreateView.sql
 * Creation/Modification History  :
 *
 *    Jagriti        29-Aug-2003       Created
 *
 * Overview of Script: This script creates the XMLType view and the instead-of
 * triggers for insert/update/delete operations on the XMLType view. 
 */
 
CREATE OR REPLACE VIEW Emp_view OF XMLTYPE WITH OBJECT ID
   (EXTRACT(sys_nc_rowinfo$,'/EMP/@EMPNO').getnumberval())
  AS SELECT XMLELEMENT("EMP",
   XMLFOREST(empno, ename, job,sal,deptno)
  ) FROM emp ;

CREATE OR REPLACE TRIGGER insert_emp_trig INSTEAD OF INSERT ON emp_view
BEGIN
  INSERT INTO emp (empno, ename, job, sal, deptno)
   VALUES
        (EXTRACTVALUE(:new.sys_nc_rowinfo$,'/EMP@EMPNO'),
         EXTRACTVALUE(:new.sys_nc_rowinfo$,'/EMP/ENAME/text()'),
         EXTRACTVALUE(:new.sys_nc_rowinfo$,'/EMP/JOB/text()'),
         EXTRACTVALUE(:new.sys_nc_rowinfo$,'/EMP/SAL/text()'),
         EXTRACTVALUE(:new.sys_nc_rowinfo$,'/EMP/DEPTNO/text()'));
END;
/

CREATE OR REPLACE TRIGGER update_emp_trig INSTEAD OF UPDATE ON emp_view
  BEGIN
  UPDATE emp  SET
  ename = extractvalue(:new.sys_nc_rowinfo$,'/EMP/ENAME/text()'),
  job =  extractvalue(:new.sys_nc_rowinfo$,'/EMP/JOB/text()'),
  sal  =  extractvalue(:new.sys_nc_rowinfo$,'/EMP/SAL/text()'),
  deptno  =  extractvalue(:new.sys_nc_rowinfo$,'/EMP/DEPTNO/text()')
  WHERE empno = extractvalue(:new.sys_nc_rowinfo$,'/EMP/EMPNO');
END;
/

CREATE OR REPLACE TRIGGER delete_emp_trig INSTEAD OF DELETE ON emp_view
  BEGIN
  DELETE FROM emp  WHERE
  empno = extractvalue(:old.sys_nc_rowinfo$,'/EMP/EMPNO');
END;
/
