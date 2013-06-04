col student_number format a15
col fname format a15
col lname format a25
col school format a20
set lines 120
set pages 60

select student_number, student_id, fname, lname, school from common.students
where lea=upper('&lea') and student_number in (&student_number_list)
order by student_number
/
