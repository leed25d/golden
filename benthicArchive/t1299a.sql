
  /* grab current state */
  select caction_state_id as v_pid,
         naction_state_id as v_cid,
         survey_id,
         readonly
    from maa.ph_survey
   where survey_id in (30059586,30061442,30061444,30061546);

  /* resolve state code */
    select action_state_id as v_nid
      from common.action_state
     where code = 'p';

     select * from common.action_state where action_state_id=2006;

select * from common.action_state;

update maa.ph_survey set caction_state_id=2000 where survey_id in (30059586,30061442,30061444,30061546);

 select     S.survey_id, S.paction_state_id as pid,
      S.caction_state_id as cid,
      S.naction_state_id as nid
    from maa.ph_survey  S
   where survey_id in (30059586,30061442,30061444,30061546);

 select     S.survey_id, S.paction_state_id as pid,
      S.caction_state_id as cid,
      S.naction_state_id as nid
    from maa.ph_survey  S
   where S.paction_state_id=2006 and S.naction_state_id=2006;


update maa.ph_survey set caction_state_id=2000, paction_state_id=2002, naction_state_id=2002 where survey_id=30059586;

select * from maa.ph_survey where survey_id=30061546;

select survey_id, lname, fname, mi, emp_signed, emp_signed_date, phone, super_signed, super_signed_date, location, jcml_id, virtual_day1, emp_number, s.cu_id, keyer_userid, timestamp, group_id, s.state, valid, nvl(hours_per_day,0), s_type, locked_date, paper_recvd, paper_recvd_date, spmp, weekends, readonly, month_rpt, year_rpt, paction_state_id, caction_state_id, naction_state_id, req_flag, userid, valid_str, salary, benefits, s.survey_type_id, days_in_period, cu.name from maa.ph_survey s, common.ph_claiming_units cu where s.cu_id = cu.cu_id and survey_id = 30061546;