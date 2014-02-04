col sfn format a15
col sln format a25
col visit_date format a12
col comments format a20 wrap
col ufn format a15
col uln format a25
set lines 180
set pages 100

define spk_list=603068,603084,602636,602638,602640,602694,603754,603760,605952,605578,605592,605446,604988,604236

select
   n.sp_visit_id,
   to_char(n.visit_date, 'yyyy-mm-dd') visit_date,
   s.fname   sfn,
   s.lname   sln,
   n.empid,
   u.fname   ufn,
   u.lname   uln
from
   nursing.sp_visits n,
   common.students s,
   common.users u
where  1=1
   and n.sp_visit_id in (&spk_list)
   and s.student_id=n.sid
   and u.userid=n.empid
/
