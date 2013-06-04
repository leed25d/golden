update p_import.pdox_stu_dupes d
set d.old_student_id = (select e.entity_id
	from common.entity_student e
	where e.student_number = d.old_lea_sid
	and e.ccode = d.lea
	and e.lname = d.last
	and e.fname = d.first
	and e.gender = d.gender)
/
