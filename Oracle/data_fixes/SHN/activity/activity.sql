

accept kill_date date prompt "Enter date training period ended (YYYY-MM-DD): "
accept lea_code char prompt "Enter LEA code to purge training data: "

insert into temp_emp (select userid from common.users where ccode = '&lea_code');
commit;

select count(*) from para.screenings 
where empid in (select emp_id from temp_emp)
and service_date < '&kill_date';


select count(*) from para.assessments x
where empid in (select emp_id from temp_emp)
and service_date < '&kill_date';


select count(*) from para.assessments_iep_link 
where assessment_id in (
	select assessment_id
	from para.assessments_iep x, temp_emp e
	where x.empid = e.emp_id
	and x.service_date < '&kill_date');

select count(*) from para.assessments_iep
where empid in (select emp_id from temp_emp)
and service_date < '&kill_date';

select count(*) from nursing.sp_students
where empid in (select emp_id from temp_emp)
and service_date < '&kill_date';

select count(*) from nursing.sp_visits
where empid in (select emp_id from temp_emp)
and visit_date < '&kill_date';

select count(*) from para.ap
where empid in (select emp_id from temp_emp)
and date_sent < '&kill_date';

select count(*) from para.treatment_areas
where treatment_id in (
        select treatment_id
        from para.treatments x, temp_emp e
        where x.empid = e.emp_id
        and x.service_date < '&kill_date');

select count(*) from para.treatments
where empid in (select emp_id from temp_emp)
and service_date < '&kill_date';

select count(*) from nursing.office_visits
where empid in (select emp_id from temp_emp)
and visit_date < '&kill_date';

select count(*) from nursing.referrals
where empid in (select emp_id from temp_emp)
and screening_date < '&kill_date';

select count(*) from para.notes
where empid in (select emp_id from temp_emp)
and note_date < '&kill_date';


