--CMAA Generate Process
--
--  2 streams here: a set of SELECTs to see what you're going to generate,
--                  a set of SELECT/INSERTs to create the actual surveys
-- Part I: establish parameters and verify logic
-- 1. Survey date endpoints:
-- 1a. identify start date : &vday1 : to_date('12/26/10', 'MM/DD/YY')
-- 1b. identify end date   : &end_date : to_date('1/1/11','MM/DD/YY')
select (trunc(to_date('&end_date','MM/DD/YY')) - trunc(to_date('&vday1', 'MM/DD/YY')) + 1) days_in_period from dual;
-- 1c. days_in_period : 5 --result of prev query, note adj for off-by-one
-- 1d. ccodes in ('XX', '41', '14')
-- 1e. fy_start : 2010
-- 1f. fy_end   : 2011   --probably set these dynamically from survey_date?



--TODO: put parameters into lexicals so no chance of mis-typing
-- &vday1 = 12/26/10                                   --select to_date('&vday1', 'MM/DD/YY') virtual_day1 from dual;
-- &dip = 6                                            --select (to_date('&vday1', 'MM/DD/YY') + &dip -1) should_match_end_date from dual;
select (to_date('&vday1', 'MM/DD/YY') + &dip -1) should_match_end_date, to_date('&end_date','MM/DD/YY') end_date from dual;
-- &ccode_list = 'XX','14','41' --note no parens, etc: --select count(*) from common.entity_cu_ph where ccode in (&ccode_list);


-- 2. Test-Generate survey data for client code in question
--      input parameters are: virtual_day1 (twice!), days_in_period, and client_code
--      note all the hard-coded state and status flags
select cu.name cu, 12345 survey_id, upper(u.lname) lname, upper(u.fname) fname, u.userid, u.userid, u.phone, c.jclass civil_srv_class
, to_date('&vday1','MM/DD/YY') virtual_day1, to_char(to_date('&vday1','MM/DD/YY'), 'MM') month_rpt, j.year1 year_rpt
, j.cu_id, c.jcml_id, 117, 2000, 2002, 'o' state, 1 valid, 'online' s_type, p.spmp, 'n' weekends, cu.survey_type_id
, &dip days_in_period
from maa.ph_jcm_pcode_link p, maa.ph_jcm_class_link c, maa.ph_jcm j, common.users_info u, common.entity_cu_ph cu
where p.userid = u.userid and p.jcml_id = c.jcml_id and c.jcm_id = j.jcm_id
and j.year1 = &fy_start and j.year2 = &fy_end and j.cu_id = cu.cu_id
and cu.survey_type_id=1 and cu.archive<>1 and cu.ccode in (&ccode_list);


-- 3. Test-Generate audit trail activity for survey-create events
--      input parameters are: client_code, virtual_day1
--      should not return anything until after the insert/select in Part II.
select 1234 activity_id, survey_id, 1 ordernum, sysdate, 1 created_by, 'INSERT' action_item, 'Survey ; Open' action_text
, paction_state_id, caction_state_id, naction_state_id, 'Status' type_code, 1 activity_public
from maa.ph_survey s, common.entity_cu_ph cu
where s.cu_id = cu.cu_id and cu.survey_type_id = s.survey_type_id
and cu.ccode in (&ccode_list) and virtual_day1 = to_date('&vday1','MM/DD/YY')
and s.survey_id in (
    select x.survey_id from maa.ph_survey x MINUS select distinct y.survey_id from maa.ph_survey_activity y
    ); --stupid, probably need a batch table to record our params and thus single out the just-created surveys

-- Part II: SELECT/INSERTs
-- 4. Insert surveys
insert into maa.ph_survey(survey_id, lname, fname, userid, keyer_userid, phone, civil_srv_class, virtual_day1, month_rpt, year_rpt
, cu_id, jcml_id, paction_state_id, caction_state_id, naction_state_id, state, valid, s_type, spmp, weekends
, survey_type_id, days_in_period)
select maa.ph_survey_id_seq.nextval survey_id
, upper(u.lname) lname, upper(u.fname) fname, u.userid, u.userid, u.phone, c.jclass civil_srv_class
, to_date('&vday1','MM/DD/YY') virtual_day1, to_char(to_date('&vday1','MM/DD/YY'), 'MM') month_rpt, j.year1 year_rpt
, j.cu_id, c.jcml_id, 117, 2000, 2002, 'o' state, 1 valid, 'online' s_type, p.spmp, 'n' weekends, cu.survey_type_id
, &dip days_in_period
from maa.ph_jcm_pcode_link p, maa.ph_jcm_class_link c, maa.ph_jcm j, common.users_info u, common.entity_cu_ph cu
where p.userid = u.userid and p.jcml_id = c.jcml_id and c.jcm_id = j.jcm_id
and j.year1 = &fy_start and j.year2 = &fy_end and j.cu_id = cu.cu_id
and cu.survey_type_id=1 and cu.archive<>1 and cu.ccode in (&ccode_list);


-- 5. Redo Test-Generate audit trail activity for survey-create events
--      input parameters are: client_code, virtual_day1
--      should now match number of surveys inserted above
select 1234 activity_id, survey_id, 1 ordernum, sysdate, 1 created_by, 'INSERT' action_item, 'Survey ; Open' action_text
, paction_state_id, caction_state_id, naction_state_id, 'Status' type_code, 1 activity_public
from maa.ph_survey s, common.entity_cu_ph cu
where s.cu_id = cu.cu_id and cu.survey_type_id = s.survey_type_id
and cu.ccode in (&ccode_list) and virtual_day1 = to_date('&vday1','MM/DD/YY')
and s.survey_id in (
    select x.survey_id from maa.ph_survey x MINUS select distinct y.survey_id from maa.ph_survey_activity y
    ); --stupid, probably need a batch table to record our params and thus single out the just-created surveys

-- 6. Insert activity records for our new surveys
insert into maa.ph_survey_activity (activity_id, survey_id, ordernum, created_on, created_by, action_item
, action_text, paction_state_id, caction_state_id, naction_state_id, type_code, activity_public)
select MAA.PH_SURVEY_ACTIVITY_ID_SEQ.nextval activity_id, survey_id, 1 ordernum, sysdate, 1 created_by, 'INSERT' action_item
, 'Survey ; Open' action_text, paction_state_id, caction_state_id, naction_state_id, 'Status' type_code, 1 activity_public
from maa.ph_survey s, common.entity_cu_ph cu
where s.cu_id = cu.cu_id and cu.survey_type_id = s.survey_type_id
and cu.ccode in (&ccode_list) and virtual_day1 = to_date('&vday1','MM/DD/YY')
and s.survey_id in (
    select x.survey_id from maa.ph_survey x MINUS select distinct y.survey_id from maa.ph_survey_activity y
    ); --stupid, probably need a batch table to record our params and thus single out the just-created surveys

