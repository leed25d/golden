undef SERVICE_DATE
undef STU_NUMBER
define lea_code='SS'
define prov_code='S15'
accept service_date char prompt "Enter date of service (YYMMDD): "
--accept lea_code char prompt "Enter LEA code for provider and student: "
--accept prov_code char prompt "Enter provider code: "
accept stu_number char prompt "Enter student number: "

select student_id, lname, fname, birthdate
from common.students where lea='&&lea_code' and student_number='&&stu_number';

col username format a15 trunc
col prov_code format a8 trunc
select userid, username, fname, lname, prov_code
from common.users where ccode='&&lea_code' and prov_code='&&prov_code';

select treatment_id, sid, empid, minutes, minutes_type, outcome
from para.treatments
where service_date = to_date('&&service_date', 'YYMMDD')
and empid in (select userid from common.users
	where ccode='&&lea_code' and prov_code='&&prov_code')
and sid in (select student_id from common.students
	where lea='&&lea_code' and student_number='&&stu_number')
order by treatment_id;

--undef trmt_id
--accept trmt_id number prompt "Enter treatment id to delete: "
--delete from para.treatments where treatment_id = &&trmt_id;


