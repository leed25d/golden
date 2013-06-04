delete from para.screenings 
where empid in (select emp_id from temp_emp)
and service_date < '&kill_date';


delete from para.assessments x
where empid in (select emp_id from temp_emp)
and service_date < '&kill_date';


delete from para.assessments_iep_link 
where assessment_id in (
	select assessment_id
	from para.assessments_iep x, temp_emp e
	where x.empid = e.emp_id
	and x.service_date < '&kill_date');

delete from para.assessments_iep
where empid in (select emp_id from temp_emp)
and service_date < '&kill_date';

REMARK May not always want to delete these, so not "on" by default
REMARK delete from nursing.sp_students
REMARK where empid in (select emp_id from temp_emp)
REMARK and service_date < '&kill_date';

delete from nursing.sp_visits
where empid in (select emp_id from temp_emp)
and visit_date < '&kill_date';

delete from para.ap
where empid in (select emp_id from temp_emp)
and date_sent < '&kill_date';

delete from para.treatment_areas
where treatment_id in (
        select treatment_id
        from para.treatments x, temp_emp e
        where x.empid = e.emp_id
        and x.service_date < '&kill_date');

delete from para.treatments
where empid in (select emp_id from temp_emp)
and service_date < '&kill_date';

delete from nursing.office_visits
where empid in (select emp_id from temp_emp)
and visit_date < '&kill_date';

delete from nursing.referrals
where empid in (select emp_id from temp_emp)
and screening_date < '&kill_date';

delete from para.notes
where empid in (select emp_id from temp_emp)
and note_date < '&kill_date';


