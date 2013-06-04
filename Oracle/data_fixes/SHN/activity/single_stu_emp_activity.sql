accept student_id char prompt "Enter student ID to check for activity:"
insert into paradba.temp_students values (&&student_id);
accept emp_id char prompt "Enter emp ID to check for activity:"
insert into paradba.temp_emp values (&&emp_id);
commit;

select count(*) from para.screenings 
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp)
and service_date < sysdate;


select count(*) from para.assessments x
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp)
and service_date < sysdate;


select count(*) from para.assessments_iep
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp)
and service_date < sysdate;

select count(*) from nursing.sp_students
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp);

select count(*) from nursing.sp_visits
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp)
and visit_date < sysdate;

select count(*) from para.ap
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp)
and date_sent < sysdate;

select count(*) from para.treatments
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp)
and service_date < sysdate;

select count(*) from nursing.office_visits
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp)
and visit_date < sysdate;

select count(*) from nursing.referrals
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp)
and screening_date < sysdate;

select count(*) from para.notes
where sid in (select stu_id from paradba.temp_students)
and empid in (select emp_id from paradba.temp_emp)
and note_date < sysdate;


