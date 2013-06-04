accept student_id char prompt "Enter student ID to check for activity:"

insert into paradba.temp_students values (&&student_id);
commit;

select count(*) from para.screenings 
where sid in (select stu_id from paradba.temp_students)
and service_date < sysdate;


select count(*) from para.assessments x
where sid in (select stu_id from paradba.temp_students)
and service_date < sysdate;


select count(*) from para.assessments_iep_link 
where assessment_id in (
	select assessment_id
	from para.assessments_iep x, paradba.temp_students e
	where x.sid = e.stu_id
	and x.service_date < sysdate);

select count(*) from para.assessments_iep
where sid in (select stu_id from paradba.temp_students)
and service_date < sysdate;

select count(*) from nursing.sp_students
where sid in (select stu_id from paradba.temp_students);

select count(*) from nursing.sp_visits
where sid in (select stu_id from paradba.temp_students)
and visit_date < sysdate;

select count(*) from para.ap
where sid in (select stu_id from paradba.temp_students)
and date_sent < sysdate;

select count(*) from para.treatment_areas
where treatment_id in (
        select treatment_id
        from para.treatments x, paradba.temp_students e
        where x.sid = e.stu_id
        and x.service_date < sysdate);

select count(*) from para.treatments
where sid in (select stu_id from paradba.temp_students)
and service_date < sysdate;

select count(*) from nursing.office_visits
where sid in (select stu_id from paradba.temp_students)
and visit_date < sysdate;

select count(*) from nursing.referrals
where sid in (select stu_id from paradba.temp_students)
and screening_date < sysdate;

select count(*) from para.notes
where sid in (select stu_id from paradba.temp_students)
and note_date < sysdate;


