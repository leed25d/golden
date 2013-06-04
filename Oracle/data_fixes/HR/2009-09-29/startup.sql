insert into temp_students (
	select student_id
	from common.students
	where lea = 'HR');

commit;

select x.service_date, x.sid, x.rep_site, to_char(x.service_date, 'YYYY-MM-DD HH24:MI') repdt
from para.assessments_iep x, temp_students s
where x.sid = s.stu_id
and x.service_date < trunc(sysdate-1);


