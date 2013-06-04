

select count(*) from para.screenings where empid=&&empid ;

select count(*) from para.assessments where empid=&&empid;

select count(*) from para.assessments_iep_link
where assessment_id in (
        select assessment_id
        from para.assessments_iep x, temp_emp e where empid=&&empid);

select count(*) from para.assessments_iep where empid=&&empid ;

select count(*) from nursing.sp_visits where empid=&&empid ;

select count(*) from para.ap where empid=&&empid;


select count(*) from para.treatment_areas
where treatment_id in (select treatment_id
     from para.treatments x, temp_emp e	where empid=&&empid);

 select count(*) from para.treatments where empid=&&empid ;

 select count(*) from nursing.office_visits where empid=&&empid ;

 select count(*) from nursing.referrals where empid=&&empid ;

 select count(*) from para.notes where empid=&&empid ;

