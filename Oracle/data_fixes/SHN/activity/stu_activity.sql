

accept kill_date date prompt "Enter date training period ended (YYYY-MM-DD): "
accept lea_code char prompt "Enter LEA code to purge training data: "

insert into temp_students (select student_id from common.students where lea = '&lea_code');
commit;

select count(*) from para.screenings 
where sid in (select stu_id from temp_students)
and service_date < '&kill_date';


select count(*) from para.assessments x
where sid in (select stu_id from temp_students)
and service_date < '&kill_date';


select count(*) from para.assessments_iep_link 
where assessment_id in (
	select assessment_id
	from para.assessments_iep x, temp_students e
	where x.sid = e.stu_id
	and x.service_date < '&kill_date');

select count(*) from para.assessments_iep
where sid in (select stu_id from temp_students)
and service_date < '&kill_date';

select count(*) from nursing.sp_students
where sid in (select stu_id from temp_students);

select count(*) from nursing.sp_visits
where sid in (select stu_id from temp_students)
and visit_date < '&kill_date';

select count(*) from para.ap
where sid in (select stu_id from temp_students)
and date_sent < '&kill_date';

select count(*) from para.treatment_areas
where treatment_id in (
        select treatment_id
        from para.treatments x, temp_students e
        where x.sid = e.stu_id
        and x.service_date < '&kill_date');

select count(*) from para.treatments
where sid in (select stu_id from temp_students)
and service_date < '&kill_date';

select count(*) from nursing.office_visits
where sid in (select stu_id from temp_students)
and visit_date < '&kill_date';

select count(*) from nursing.referrals
where sid in (select stu_id from temp_students)
and screening_date < '&kill_date';

select count(*) from para.notes
where sid in (select stu_id from temp_students)
and note_date < '&kill_date';


