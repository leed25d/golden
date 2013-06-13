define empid=26378388
define sid=26527234
select a.assessment_id, to_char(a.service_date, 'YYYY-MM-DD') as service_date, s.lea, a.empid, a.sid, s.lname, s.fname, s.gender as g, to_char(s.birthdate, 'YYYY-MM-DD') as dob
from para.assessments_iep a, common.students s
where a.empid=&&empid and a.sid=&&sid and a.sid=s.student_id
order by service_date
/


select distinct assessment_type from para.assessments_iep;

------------------------------------------------------------------------

define ass_list=336516,336826,336445,259786,336800,336861,336640,336556,336892,336701

select  assessment_id, assessment_type
from para.assessments_iep a where a.assessment_id in (&&ass_list)

--  delete from para.assessments_iep where assessment_id in (&&ass_list);

--  update para.assessments_iep set  assessment_type = 'annual' where assessment_id in (&&ass_list);