col student_number format a15
col fname format a15
col lname format a25
col school format a20
col dob format a12
set lines 120
set pages 60

select student_number, student_id, fname, lname, school, to_char(birthdate, 'YYYY-MM-DD') as dob from common.students
where lname like upper('&last%') and fname like upper('&first%') and lea=upper('&lea')
order by student_number
/
