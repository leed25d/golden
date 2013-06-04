set lines 100
set pages 60
col student_number format a14
col fname format a15
col lname format a25

select student_number, student_id, fname, lname
from common.students
where lname like upper('&last%') and fname like upper('&first%')
and lea='JY'
order by student_number
/
