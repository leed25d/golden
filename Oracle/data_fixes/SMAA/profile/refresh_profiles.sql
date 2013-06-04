truncate table p_import.temp_survey_profile;

insert into p_import.temp_survey_profile
	(select p.*,null,null from maa.survey_profile p);
commit;

--purge out profiles for inactive users
delete from p_import.temp_survey_profile p1 where p1.profile_id in (
	select p2.profile_id
	from p_import.temp_survey_profile p2, common.entity_user u
	where p2.empid = u.entity_id and u.password is null
	);
commit;

--copy on most recent survey id
update p_import.temp_survey_profile p
	set p.recent_survey_date = (select max(s.rep_date)
		from maa.survey s where s.profile_id = p.profile_id);
update p_import.temp_survey_profile p
	set p.recent_survey_id = (select max(s.survey_id)
		from maa.survey s 
		where s.profile_id = p.profile_id 
		and s.rep_date = p.recent_survey_date);

delete from p_import.temp_survey_profile where recent_survey_id is null;
commit;

--update profile with up-to-date survey data
update p_import.temp_survey_profile p
   set (p.lname, p.fname, p.mi, p.phone, p.site_id, p.site, p.job_class_id, p.job_class
       , p.emp_number, p.hours_per_day, p.training_date, p.survey_days, p.weekends) = (
       select s.lname, s.fname, s.mi, s.phone, s.site_id, s.site, s.job_class_id, s.job_class
       , s.emp_number, s.hours_per_day, s.training_date, s.survey_days, s.weekends
       from maa.survey s where s.survey_id = p.recent_survey_id and s.profile_id = p.profile_id
   );
commit;

--finally, copy temp data back onto main profiles
create table paradba.maa_survey_profile_backup as select * from maa.survey_profile;
update maa.survey_profile p
   set (p.lname, p.fname, p.mi, p.phone, p.site_id, p.site, p.job_class_id, p.job_class
       , p.emp_number, p.hours_per_day, p.training_date, p.survey_days, p.weekends) = (
       select s.lname, s.fname, s.mi, s.phone, s.site_id, s.site, s.job_class_id, s.job_class
       , s.emp_number, s.hours_per_day, s.training_date, s.survey_days, s.weekends
       from p_import.temp_survey_profile s where s.profile_id = p.profile_id
   )
where p.profile_id in (select p2.profile_id from p_import.temp_survey_profile p2);
commit;
