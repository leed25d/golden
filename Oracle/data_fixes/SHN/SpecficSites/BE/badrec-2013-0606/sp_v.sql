--  select count(*) from nursing.sp_visits where empid=&&empid and sid=&&student_id;


col sp_visit_id format 99999999
col visit_date  format a12
col common_sid  format 99999999
col sps_sid     format 99999999
col lea         format a6
col lname       format a15
col fname       format a15
col dob         format a12

set lines 200
set page 30

select n.sp_visit_id, to_char(n.visit_date, 'YYYY-MM-DD') as visit_date, n.sp_student_id as sps_sid, s.student_id as common_sid, s.lea, s.lname, s.fname, s.gender, to_char(s.birthdate, 'YYYY-MM-DD') as dob
from nursing.sp_visits n, common.students s
where n.empid=28045628 and n.sid=25230714 and n.sid=s.student_id
order by visit_date desc;
