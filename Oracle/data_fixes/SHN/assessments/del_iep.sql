define ass_list=384278

col assessment_id heading "Primary|Key"  format 99999999
col  empid heading "Emp | Id"
col  sid heading "Student | Id"
col fname heading "First|Name" format a15
col lname heading "Last|Name" format a25
col g heading "M/F" format a3
col service_date  heading "Service|Date" format a12
col dob  format a12
col lea  format a5
col gender  format a5

set define on
set lines 200
set pages 60

select a.assessment_id, to_char(a.service_date, 'YYYY-MM-DD') as service_date, s.lea, a.empid, a.sid, s.lname, s.fname, s.gender as g, to_char(s.birthdate, 'YYYY-MM-DD') as dob
from para.assessments_iep a, common.students s
where a.assessment_id in (&ass_list) and a.sid=s.student_id
order by service_date
/
--select * from para.assessments_iep_link where assessment_id in (&ass_list);
--
--
--select count(*) 
----delete
--from para.assessments_iep_link where assessment_id in (&ass_list);
--
--select count(*) 
----delete
--from para.assessments_iep where assessment_id in (&ass_list);

