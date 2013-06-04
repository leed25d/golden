CREATE PACKAGE BODY MAA."PH_SURVEY_PKG" is

/************************************************************************/
procedure survey_insert(p_lname             in varchar2,
                        p_fname             in varchar2,
                        p_userid            in number,
                        p_mi                in varchar2,
                        p_phone             in number,
                        p_super_signed      in varchar2,
                        p_location          in varchar2,
                        p_jcml_id           in number,
                        p_emp_number        in varchar2,
                        p_cu_id             in number,
                        p_keyer_userid      in number,
                        p_timestamp         in number,
                        p_salary            in number,
                        p_benefits          in number,
                        p_state             in varchar2,
                        p_valid             in varchar2,
                        p_hours_per_day     in number,
                        p_s_type            in varchar2,
                        p_spmp              in varchar2,
                        p_weekends          in varchar2,
                        p_readonly          in varchar2,
                        p_month_rpt         in varchar2,
                        p_year_rpt          in varchar2,
                        p_paction_state_id  in number,
                        p_caction_state_id  in number,
                        p_naction_state_id  in number,
                        p_survey_id         in out number)
is
  v_id             number;
  v_aid            number;
  v_public         number;
  v_virtual_day1   date;
  v_code           varchar2(32);
  v_year_rpt       varchar2(32);
begin
        
  /* ckh: disabling CMAA pl/sql layer for #1090 */
  raise_application_error(-20101, 'INVALID Insert: cannot create CMAA survey');

  v_public := 1; /* status insert is public */

  /* grab a key */
  IF (p_survey_id IS NULL OR p_survey_id = 0) THEN
    SELECT maa.ph_survey_id_seq.nextval INTO v_id FROM DUAL;
  ELSE
    v_id := p_survey_id;
  END IF;

  /* build virtual day1 */
  v_year_rpt := p_year_rpt;
  if (p_month_rpt < 7) then
   v_year_rpt := v_year_rpt+1;
  end if;
  v_virtual_day1 := to_date(p_month_rpt||'/'||'01/'||v_year_rpt,'MM/DD/YYYY');

  /* grab code */
  BEGIN
    SELECT code
      INTO v_code
      FROM common.action_state
     WHERE action_state_id = p_naction_state_id;
    EXCEPTION WHEN NO_DATA_FOUND THEN
      v_code := NULL;
  END;

  /* insert survey record */
  insert into maa.ph_survey
    (survey_id, lname, fname, userid, mi, phone, super_signed,
     location, jcml_id, emp_number, cu_id, keyer_userid,
     timestamp, salary, benefits, state, hours_per_day,
     s_type, spmp, weekends, readonly, month_rpt, virtual_day1,
     year_rpt, paction_state_id, caction_state_id, naction_state_id,
     valid)
  values
    (v_id, p_lname, p_fname, p_userid, p_mi, p_phone, p_super_signed,
     p_location, p_jcml_id, p_emp_number, p_cu_id, p_keyer_userid,
     p_timestamp, p_salary, p_benefits, v_code, p_hours_per_day,
     p_s_type, p_spmp, p_weekends, p_readonly, p_month_rpt, v_virtual_day1,
     p_year_rpt, p_paction_state_id, p_caction_state_id, p_naction_state_id,
     '1');

  /* pull default attributes from claiming unit record onto this survey */
  update maa.ph_survey s
    set s.survey_type_id = (
      select cu.survey_type_id
      from common.entity_cu_ph cu
      where cu.cu_id = p_cu_id
    )
  where s.survey_id = v_id;


  /* create survey activity record */
  v_aid := NULL;
  maa.ph_survey_pkg.survey_activity_insert(v_id, p_userid, v_public,
    p_paction_state_id, p_caction_state_id, p_naction_state_id,
    'INSERT', 'Status', v_aid);

  p_survey_id := v_id;

end survey_insert;

/************************************************************************/
procedure survey_update(p_lname             in varchar2,
                        p_fname             in varchar2,
                        p_userid            in number,
                        p_mi                in varchar2,
                        p_phone             in number,
                        p_super_signed      in varchar2,
                        p_location          in varchar2,
                        p_jcml_id           in number,
                        p_emp_number        in varchar2,
                        p_cu_id             in number,
                        p_keyer_userid      in number,
                        p_timestamp         in number,
                        p_salary            in number,
                        p_benefits          in number,
                        p_state             in varchar2,
                        p_valid             in varchar2,
                        p_hours_per_day     in number,
                        p_spmp              in varchar2,
                        p_weekends          in varchar2,
                        p_readonly          in varchar2,
                        p_month_rpt         in varchar2,
                        p_year_rpt          in varchar2,
                        p_paction_state_id  in number,
                        p_caction_state_id  in number,
                        p_naction_state_id  in number,
                        p_survey_id         in number)
is
  o_nid            number;
  o_cid            number;
  o_pid            number;
  v_aid            number;
  v_public         number;
  v_hours_per_day  number;
  v_virtual_day1   date;
  v_code           varchar2(32);
  v_year_rpt       varchar2(32);
begin

  /* ckh: disabling CMAA pl/sql layer for #1090 */
  raise_application_error(-20101, 'INVALID Insert: cannot create CMAA survey');

  v_public := 1; /* status insert is public */

  /* check for state change / hours change */
  select paction_state_id,
         caction_state_id,
         naction_state_id,
         state,
         hours_per_day
    into o_pid,o_cid,o_nid, v_code, v_hours_per_day
    from maa.ph_survey
   where survey_id = p_survey_id;

  if (o_pid != p_paction_state_id or o_cid != p_caction_state_id or o_nid != p_naction_state_id) then
    /* insert activity record */
    maa.ph_survey_pkg.survey_activity_insert(p_survey_id,p_userid, v_public,
                                   p_paction_state_id,p_caction_state_id,
                                   p_naction_state_id,'UPDATE','Status', v_aid);

    /* grab code */
    BEGIN
      SELECT code
        INTO v_code
        FROM common.action_state
       WHERE action_state_id = p_naction_state_id;
      EXCEPTION WHEN NO_DATA_FOUND THEN
        v_code := NULL;
    END;
  end if;

  /* build virtual day1 */
  v_year_rpt := p_year_rpt;
  if (p_month_rpt < 7) then
   v_year_rpt := v_year_rpt+1;
  end if;
  v_virtual_day1 := to_date(p_month_rpt||'/'||'01/'||v_year_rpt,'MM/DD/YYYY');

  /* update survey record */
  update maa.ph_survey
     set lname            = p_lname,
         fname            = p_fname,
         userid           = p_userid,
         mi               = p_mi,
         phone            = p_phone,
         super_signed     = p_super_signed,
         location         = p_location,
         jcml_id          = p_jcml_id,
         emp_number       = p_emp_number,
         cu_id            = p_cu_id,
         keyer_userid     = p_keyer_userid,
         timestamp        = p_timestamp,
         salary           = p_salary,
         benefits         = p_benefits,
         state            = v_code,
         valid            = p_valid,
         hours_per_day    = p_hours_per_day,
         spmp             = p_spmp,
         weekends         = p_weekends,
         readonly         = p_readonly,
         month_rpt        = p_month_rpt,
         year_rpt         = p_year_rpt,
         virtual_day1     = v_virtual_day1,
         paction_state_id = p_paction_state_id,
         caction_state_id = p_caction_state_id,
         naction_state_id = p_naction_state_id
   where survey_id  = p_survey_id;

  /* if hours per day has changed, then re validate survey */
  IF v_hours_per_day != p_hours_per_day THEN
    validate_survey(p_survey_id);
  END IF;

end survey_update;

/************************************************************************/
procedure survey_delete(p_survey_id in number)
is
begin

  delete from maa.ph_survey_activity_text where activity_id in
    (select activity_id from maa.ph_survey_activity
     where survey_id = p_survey_id);

  delete from maa.ph_survey_activity where survey_id = p_survey_id;
  delete from maa.ph_survey_hour     where survey_id = p_survey_id;
  delete from maa.ph_survey_sample   where survey_id = p_survey_id;
  delete from maa.ph_survey          where survey_id = p_survey_id;

end survey_delete;

/************************************************************************/
procedure update_survey_status(p_paction_state_id  in number,
                               p_caction_state_id  in number,
                               p_naction_state_id  in number,
                               p_userid            in number,
                               p_survey_id         in number)
is
  o_nid            number;
  o_cid            number;
  o_pid            number;
  v_aid            number;
  v_public         number;
  v_code           varchar2(32);
begin

  v_public := 1; /* status insert is public */

  /* check for state change */
  select paction_state_id,
         caction_state_id,
         naction_state_id,
         state
    into o_pid,o_cid,o_nid, v_code
    from maa.ph_survey
   where survey_id = p_survey_id;

  if (o_pid != p_paction_state_id or o_cid != p_caction_state_id or o_nid != p_naction_state_id) then
    /* insert activity record */
    maa.ph_survey_pkg.survey_activity_insert(p_survey_id,p_userid, v_public,
                                   p_paction_state_id,p_caction_state_id,
                                   p_naction_state_id,'UPDATE','Status', v_aid);

    /* grab code */
    BEGIN
      SELECT code
        INTO v_code
        FROM common.action_state
       WHERE action_state_id = p_naction_state_id;
      EXCEPTION WHEN NO_DATA_FOUND THEN
        v_code := NULL;
    END;
  end if;

  /* update survey record */
  update maa.ph_survey
     set paction_state_id = p_paction_state_id,
         caction_state_id = p_caction_state_id,
         naction_state_id = p_naction_state_id,
         state            = v_code
   where survey_id        = p_survey_id;

end update_survey_status;

/************************************************************************/
procedure update_survey_state(p_state     in varchar2,
                              p_userid    in number,
                              p_survey_id in number)
is
  v_nid             number;
  v_cid             number;
  v_pid             number;
  v_aid             number;
  v_public          number;
  v_readonly        char(1);
begin

  v_public := 1; /* status insert is public */

  /* grab current state */
  select caction_state_id,
         naction_state_id,
         readonly
    into v_pid, v_cid, v_readonly
    from maa.ph_survey
   where survey_id = p_survey_id;

  /* resolve state code */
  begin
    select action_state_id
      into v_nid
      from common.action_state
     where code = p_state;
  exception when NO_DATA_FOUND then
    v_nid := NULL;
  end;

  if (v_nid is not NULL and v_readonly is NULL) then
    /* check for a state change */
    if (v_nid != v_cid) then
      /* the state has changed */

      /* insert activity record */
      maa.ph_survey_pkg.survey_activity_insert(p_survey_id, p_userid, v_public,
                                               v_pid, v_cid, v_nid, 'UPDATE',
                                               'Status', v_aid);

      /* update survey record */
      update maa.ph_survey
         set paction_state_id = v_pid,
             caction_state_id = v_cid,
             naction_state_id = v_pid,
             state            = p_state
       where survey_id        = p_survey_id;

    end if;

  end if;

end update_survey_state;

/************************************************************************/
procedure update_survey_reqs(p_survey_id in number)
is
  v_req_flag      number;
  v_req_cnt       number;
begin
  v_req_flag := null;

  select count(*)
    into v_req_cnt from maa.ph_survey_activity
   where survey_id = p_survey_id
     and (type_code = 'Requirement' or type_code = 'Correction')
     and (completed_id is null or completed_id = 0);

  if (v_req_cnt > 0) then
    v_req_flag := 1;
  end if;

  update maa.ph_survey
     set req_flag  = v_req_flag
   where survey_id = p_survey_id;

end update_survey_reqs;

/************************************************************************/
PROCEDURE validate_survey(p_survey_id IN NUMBER)
IS
  v_valid_str       VARCHAR2(1024);
  v_valid           VARCHAR2(1);
  v_weekends        CHAR(1);
  v_day             CHAR(2);
  v_hours_per_day   NUMBER;
  v_sum             NUMBER;
  v_tmp             NUMBER;
  v_survey_type_id  NUMBER;

  /* returns days that do not total to hours worked per day */
  CURSOR c1 IS
    SELECT h.week, h.day, s.hours_per_day, sum(h.time) time
      FROM maa.ph_survey_hour h, maa.ph_survey s
     WHERE s.survey_id = h.survey_id
       AND s.survey_id = p_survey_id
     GROUP BY h.week, h.day, s.hours_per_day
    HAVING SUM(h.time) != s.hours_per_day;
  CUR1 c1%ROWTYPE;

  /* returns days that ve more than 24 hours worked per day */
  CURSOR c3 IS
    SELECT h.week, h.day, s.hours_per_day, sum(h.time) time
      FROM maa.ph_survey_hour h, maa.ph_survey s
     WHERE s.survey_id = h.survey_id
       AND s.survey_id = p_survey_id
     GROUP BY h.week, h.day, s.hours_per_day
    HAVING SUM(h.time) > 24;
  CUR3 c3%ROWTYPE;

  /* returns activities with time recorded  and need a sample */
  CURSOR c2 IS
    SELECT h.activity_id, sum(h.time) time
      FROM maa.ph_survey_hour h, maa.ph_survey s, maa.ph_activities a
     WHERE s.survey_id   = h.survey_id
       AND h.activity_id = a.activity_id
       AND a.sample      = '1'
       AND s.survey_id   = p_survey_id
     GROUP BY  h.activity_id
    HAVING SUM(h.time) > 0;
  CUR2 c2%ROWTYPE;
BEGIN
  v_valid_str := NULL;




  /* grab some data */
  SELECT hours_per_day, weekends, valid, survey_type_id
    INTO v_hours_per_day, v_weekends, v_valid, v_survey_type_id
    FROM maa.ph_survey
   WHERE survey_id = p_survey_id;

  IF (v_hours_per_day IS NULL OR v_hours_per_day = 0) THEN
    /* validate no hours over 24 */
    FOR cur3 IN c3 LOOP
      v_valid_str := v_valid_str || TO_CHAR(cur3.week) || 'w:' || TO_CHAR(cur3.day) || ',';
    END LOOP;
  ELSE
    /* validate hours per day */
    FOR cur1 IN c1 LOOP
      v_valid_str := v_valid_str || TO_CHAR(cur1.week) || 'w:' || TO_CHAR(cur1.day) || ',';
    END LOOP;
  END IF;

  /* validate samples (content / date) */
  FOR cur2 IN c2 LOOP
    BEGIN
      SELECT survey_id, SUM(sample_number)
        INTO v_tmp, v_sum
        FROM maa.ph_survey_sample
       WHERE survey_id   = p_survey_id
         AND activity_id = cur2.activity_id
         AND sample IS NOT NULL
       GROUP BY survey_id;
      EXCEPTION WHEN NO_DATA_FOUND THEN
        v_sum := 0;
    END;

   /* Hack to turn off double samples for some survey types */
   /* Will need attention as we add nuance to sample rules */
   if v_survey_type_id is not null then
      IF v_sum = 0 THEN
         v_valid_str := v_valid_str || TO_CHAR(cur2.activity_id) || 's:1,';
      END IF;

   else
     /* if sum = 3 then we are good, sum = 1 missing 2
                                     sum = 2 missing 1
                                     sum = 0 missing 1,2 */
    IF v_sum = 0 THEN
      v_valid_str := v_valid_str || TO_CHAR(cur2.activity_id) || 's:1,';
      v_valid_str := v_valid_str || TO_CHAR(cur2.activity_id) || 's:2,';
    ELSIF v_sum = 1 THEN
      v_valid_str := v_valid_str || TO_CHAR(cur2.activity_id) || 's:2,';
    ELSIF v_sum = 2 THEN
      v_valid_str := v_valid_str || TO_CHAR(cur2.activity_id) || 's:1,';
    END IF;

   end if; /* end survey type sample hack */


    /* check dates */
    BEGIN
      SELECT NVL(sample_day,'00')
        INTO v_day
        FROM maa.ph_survey_sample
       WHERE survey_id     = p_survey_id
         AND activity_id   = cur2.activity_id
         AND sample_number = 1;
      EXCEPTION WHEN NO_DATA_FOUND THEN
        v_day := '00';
    END;

    /* More hack...only enforce date assignment for 'original' survey types */
    if v_survey_type_id is null then
    IF v_day = '00' THEN
      v_valid_str := v_valid_str || TO_CHAR(cur2.activity_id) || 'd:1,';
    END IF;
    end if; /* more hack */

    BEGIN
      SELECT NVL(sample_day,'00')
        INTO v_day
        FROM maa.ph_survey_sample
       WHERE survey_id     = p_survey_id
         AND activity_id   = cur2.activity_id
         AND sample_number = 2;
      EXCEPTION WHEN NO_DATA_FOUND THEN
        v_day := '00';
    END;


   /* 2nd part of hack to turn off double samples for some survey types */
   /* Will need attention as we add nuance to sample rules */
   if v_survey_type_id is null then
      IF v_day = '00' THEN
         v_valid_str := v_valid_str || TO_CHAR(cur2.activity_id) || 'd:2,';
      END IF;
   end if; /* 2nd part of hack */


  END LOOP;

  IF v_valid_str IS NOT NULL AND v_valid != '2' THEN
    v_valid := '0';
  ELSE
    IF v_valid_str IS NULL THEN
      v_valid := '1';
    END IF;
  END IF;

  /* update valid flag */
  UPDATE maa.ph_survey
     SET valid     = v_valid,
         valid_str = v_valid_str
   WHERE survey_id = p_survey_id;

END validate_survey;

/************************************************************************/
procedure survey_activity_insert_f(p_survey_id        in number,
                                   p_created_by       in number,
                                   p_public           in number,
                                   p_action_item      in varchar2,
                                   p_type_code        in varchar2,
                                   p_required_of      in number,
                                   p_completed_id     in number,
                                   p_date_due         in date,
                                   p_text             in clob,
                                   p_activity_id      in out number)
is
  v_aid         number;
  v_ordernum    number;
  v_pid         number;
  v_cid         number;
  v_nid         number;
  v_text        varchar2(120);
  v_complete_ts date;
begin
  /* grab a key */
  IF (p_activity_id IS NULL OR p_activity_id = 0) THEN
    SELECT maa.ph_survey_activity_id_seq.nextval INTO v_aid FROM DUAL;
  ELSE
    v_aid := p_activity_id;
  END IF;

  /* setup order number */
  begin
    select max(ordernum) + 1
      into v_ordernum
      from maa.ph_survey_activity
     where survey_id = p_survey_id;
    exception when NO_DATA_FOUND THEN
      v_ordernum := 1;
  end;
  if v_ordernum is null then
    v_ordernum := 1;
  end if;

  /* grab positions */
  select paction_state_id, caction_state_id, naction_state_id
    into v_pid, v_cid, v_nid
    from maa.ph_survey
   where survey_id = p_survey_id;

  /* grab a piece of the text */
  v_text := substr(p_text,1,120);

  /* Compete Processing */
  if p_type_code = 'Complete' then
    update maa.ph_survey_activity
       set complete_ts  = sysdate,
           completed_id = v_ordernum
     where survey_id   = p_survey_id
       and ordernum = p_completed_id;
  end if;

  insert into maa.ph_survey_activity
    (activity_id,survey_id,ordernum,created_on,created_by,
     action_item,action_text,paction_state_id,caction_state_id,
     naction_state_id,type_code,completed_id, date_ordered,
     date_due,required_of,activity_public)
  values
    (v_aid,p_survey_id,v_ordernum,sysdate,p_created_by,
     p_action_item,v_text,v_pid,v_cid,
     v_nid,p_type_code,p_completed_id,sysdate,
     p_date_due,p_required_of,p_public);

  p_activity_id := v_aid;

  if (p_text is not null) then
    insert into maa.ph_survey_activity_text (activity_id, comment_text)
      values (v_aid, p_text);
  end if;

  maa.ph_survey_pkg.update_survey_reqs(p_survey_id);

end survey_activity_insert_f;

/************************************************************************/
procedure survey_activity_update_f(p_survey_id           in number,
                                   p_action_item      in varchar2,
                                   p_type_code        in varchar2,
                                   p_required_of      in number,
                                   p_date_due         in date,
                                   p_text             in clob,
                                   p_public           in number,
                                   p_activity_id      in number)
is
  v_text        varchar2(120);
  v_count       number;
begin
  /* grab a piece of the text */
  v_text := substr(p_text,1,120);

  /* update activity record */
  update maa.ph_survey_activity
     set action_item = p_action_item,
         type_code   = p_type_code,
         required_of = p_required_of,
         date_due    = p_date_due,
         action_text = v_text,
         activity_public      = p_public
   where activity_id = p_activity_id;

  if (p_text is not null) then
    select count(*)
      into v_count
      from maa.ph_survey_activity_text
     where activity_id = p_activity_id;
    if (v_count > 0) then
      update maa.ph_survey_activity_text
         set comment_text = p_text
       where activity_id = p_activity_id;
    else
      insert into maa.ph_survey_activity_text (activity_id, comment_text)
        values (p_activity_id, p_text);
    end if;
  end if;

  maa.ph_survey_pkg.update_survey_reqs(p_survey_id);

end survey_activity_update_f;

/************************************************************************/
procedure survey_activity_insert(p_survey_id           in number,
                                 p_created_by       in number,
                                 p_public           in number,
                                 p_paction_state_id in number,
                                 p_caction_state_id in number,
                                 p_naction_state_id in number,
                                 p_action_item      in varchar2,
                                 p_type_code        in varchar2,
                                 p_activity_id      in out number)
is
  v_aid            number;
  v_ordernum       number;
  v_action_text    varchar2(120);
begin

  /* grab a key */
  IF (p_activity_id IS NULL OR p_activity_id = 0) THEN
    SELECT maa.ph_survey_activity_id_seq.nextval INTO v_aid FROM DUAL;
  ELSE
    v_aid := p_activity_id;
  END IF;

  /* if type_code is Status then the comment is the position */
  if (p_type_code = 'Status') then
    select action_state
      into v_action_text
      from common.action_state_view
     where action_state_id = p_naction_state_id;
  end if;

  /* setup order number */
  begin
    select max(ordernum) + 1
      into v_ordernum
      from maa.ph_survey_activity
     where survey_id = p_survey_id;
    exception when NO_DATA_FOUND THEN
      v_ordernum := 1;
  end;
  if v_ordernum is null then
    v_ordernum := 1;
  end if;

  insert into maa.ph_survey_activity
    (activity_id,survey_id,ordernum,created_on,created_by,
     action_item,action_text,paction_state_id,caction_state_id,
     naction_state_id,type_code,activity_public)
  values
    (v_aid,p_survey_id,v_ordernum,sysdate,p_created_by,
     p_action_item,v_action_text,p_paction_state_id,p_caction_state_id,
     p_naction_state_id,p_type_code,p_public);

  p_activity_id := v_aid;

end survey_activity_insert;

/************************************************************************/

PROCEDURE survey_sample_save(p_survey_id        in number,
                             p_activity_id      in number,
                             p_sample_number    in number,
                             p_sample           in varchar2,
                             p_sample_day       in varchar2,
                             p_sample_month     in varchar2,
                             p_sample_year      in varchar2)
IS
  v_count NUMBER;
BEGIN

  SELECT count(*)
    INTO v_count
    FROM maa.ph_survey_sample
   WHERE survey_id     = p_survey_id
     AND activity_id   = p_activity_id
     AND sample_number = p_sample_number;

  IF (v_count > 0) THEN
    IF (p_sample IS NULL OR p_sample = '') THEN
      DELETE FROM maa.ph_survey_sample
       WHERE survey_id     = p_survey_id
         AND activity_id   = p_activity_id
         AND sample_number = p_sample_number;
    ELSE
      UPDATE maa.ph_survey_sample
         SET sample        = p_sample,
             sample_day    = p_sample_day,
             sample_month  = p_sample_month,
             sample_year   = p_sample_year
       WHERE survey_id     = p_survey_id
         AND activity_id   = p_activity_id
         AND sample_number = p_sample_number;
    END IF;
  ELSE
    IF p_sample IS NOT NULL THEN
      INSERT INTO maa.ph_survey_sample
        (survey_id, activity_id, sample_number, sample,
         sample_day, sample_month, sample_year)
      VALUES
        (p_survey_id, p_activity_id, p_sample_number, p_sample,
         p_sample_day, p_sample_month, p_sample_year);
    END IF;
  END IF;

END survey_sample_save;

/************************************************************************/
PROCEDURE survey_match(p_survey_id in number,
                       p_userid    in number)
IS
BEGIN
  /* associate user with survey */
  UPDATE maa.ph_survey SET userid = p_userid WHERE survey_id = p_survey_id;
END survey_match;

/************************************************************************/
PROCEDURE survey_paper_recvd(p_survey_id in NUMBER,
                             p_date      in DATE)
IS
  v_date date;
BEGIN

  IF p_date IS NULL THEN
    v_date := sysdate;
  ELSE
    v_date := p_date;
  END IF;

  /* update survey */
  UPDATE maa.ph_survey
     SET paper_recvd      = 'y',
         paper_recvd_date = v_date
   WHERE survey_id = p_survey_id;

END survey_paper_recvd;


/* functions *********************************************************/
FUNCTION total_days(p_survey_id IN NUMBER) RETURN NUMBER
IS
  v_days NUMBER;
BEGIN
  SELECT COUNT(DISTINCT day)
    INTO v_days
    FROM maa.ph_survey_hour
   WHERE survey_id = p_survey_id;

  RETURN v_days;
END total_days;

FUNCTION total_time(p_survey_id IN NUMBER) RETURN NUMBER
IS
  v_time NUMBER;
BEGIN
  SELECT SUM(time)
    INTO v_time
    FROM maa.ph_survey_hour
   WHERE survey_id = p_survey_id;

  RETURN v_time;
END total_time;

FUNCTION maa_time(p_survey_id IN NUMBER) RETURN NUMBER
IS
  v_time NUMBER;
BEGIN
  SELECT SUM(time)
    INTO v_time
    FROM maa.ph_survey_hour h, maa.ph_activities a
   WHERE h.activity_id = a.activity_id
     AND a.activity_id in (
     	20000094,20000096,20000100,20000104,20000108,20000110,
     	20000114,20000118,20000116,20000120,20000122,20000124)
     AND survey_id = p_survey_id;

  RETURN v_time;
END maa_time;

END ph_survey_pkg;
