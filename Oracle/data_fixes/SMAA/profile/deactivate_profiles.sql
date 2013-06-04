truncate table p_import.temp_survey_profile;

insert into p_import.temp_survey_profile (profile_id, empid)
	(select profile_id, empid from maa.survey_profile);

delete from p_import.temp_survey_profile p2 where p2.profile_id in (
select distinct p.profile_id --profiles connected with recent-ish surveys
from p_import.temp_survey_profile p, maa.survey s
where p.profile_id = s.profile_id
and p.empid = s.empid
and s.quarter_reporting>=22);--22 is the id for FY08/09 Q1

delete from p_import.temp_survey_profile p2 where p2.profile_id in (
select distinct p.profile_id --profiles for users with shn privs
from p_import.temp_survey_profile p, common.entity_role r
where p.empid = r.entity_id
and r.role_id like 'shn%');

update maa.survey_profile set active=0 where profile_id in (
	select p2.profile_id from p_import.temp_survey_profile p2);
update common.entity_user set password = null where ccode<>'XX' and entity_id in (
	select distinct p2.empid from p_import.temp_survey_profile p2);

commit;

