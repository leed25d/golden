update maa.survey set esig_on='y' where survey_id=749016;

update maa.survey set profile_id=66944 where survey_id=749016;

update maa.survey set keyer_empid=25399666 where survey_id=749016;

update maa.survey set job_class_id=1001 where survey_id=749016;

update maa.survey_profile set (job_class_id, empid) = (select 1001, 25399666 from dual) where profile_id=86437;

select empid, esig_on, state, profile_id, job_class_id from maa.survey where survey_id=749016;

update maa.survey set empid=25399666 where survey_id=749016;

select * from common.users where userid=27497540;

select * from common.users where lname='DOOLAN';

select * from maa.survey_profile where empid=27497540;

select * from maa.survey where survey_id=749016;

select * from maa.survey_profile;

select * from maa.survey_profile where profile_id=66924;

select s.profile_id, s.empid, s.job_class_id, j.title from maa.survey_profile s, common.job_class j where s.profile_id=86437 and j.id = s.job_class_id order by s.empid;

select * from maa.survey_profile where profile_id=86437;


select s.profile_id, s.empid, s.job_class_id, j.title from maa.survey_profile s, common.job_class j where  j.id = s.job_class_id order by s.empid;

select P.profile_id, P.profile_name, P.site_id, P.site, P.job_class_id, P.job_class, P.survey_days, P.hours_per_day, P.training_date, P.emp_number, P.claiming_unit_id, P.lname, P.fname, P.mi, P.empid userid, P.phone, P.salary, P.benefits, P.survey_type, P.spmp, P.weekends, P.active, P.notes from maa.survey_profile P where P.empid = 25399666 order by upper( P.lname ), upper( P.fname ), P.timestamp desc
