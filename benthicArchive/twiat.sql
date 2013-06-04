CREATE PACKAGE BODY NURSING."OFFICE_VISIT_PKG" is

/************************************************************************/
procedure visit_insert(p_sid                   in number,
                       p_empid                 in number,
                       p_time                  in number,
                       p_visit_date            in date,
                       p_time_in               in varchar2,
                       p_time_out              in varchar2,
                       p_complaint             in varchar2,
                       p_other_complaint       in varchar2,
                       p_temperature           in varchar2,
                       p_care_given            in varchar2,
                       p_care_given_other      in varchar2,
                       p_care_assessment_notes in varchar2,
                       p_care_assessment_type  in varchar2,
                       p_care_transport        in varchar2,
                       p_care_vital_notes      in varchar2,
                       p_parent_contact        in varchar2,
                       p_disposition           in varchar2,
                       p_comments              in varchar2,
                       p_locked                in number,
                       p_parent_contact_other  in varchar2,
                       p_referral              in varchar2,
                       p_recorded_by           in number,
                       p_data_entry            in number,
                       p_disposition_other     in varchar2,
                       p_school_id             in number,
                       p_plan                  in number,
                       p_complaint_minutes     in number,
                       p_contact_provider      in varchar2,
                       p_contact_agency        in varchar2,
                       p_contact_school        in varchar2,
                       p_contact_other         in varchar2,
                       p_disposition_provider  in varchar2,
                       p_disposition_agency    in varchar2,
                       p_disposition_school    in varchar2,
                       p_disposition_onsite    in varchar2,
                       p_visit_id              in out number)
is
  v_id        number;
begin

  /* grab a key */
  if (p_visit_id is null or p_visit_id = 0) then
    select nursing.office_visits_visit_id_seq.nextval into v_id from dual;
  else
    v_id := p_visit_id;
  end if;

  /* insert record */
  insert into nursing.office_visits
    (sid, empid, time, visit_date, time_in, time_out, complaint,
     other_complaint, temperature, care_given, care_given_other,
     care_assessment_notes, care_assessment_type, care_transport,
     care_vital_notes, parent_contact, disposition, comments, locked,
     parent_contact_other, referral, recorded_by, data_entry, disposition_other,
     school_id, plan, complaint_minutes, visit_id,
     contact_provider, contact_agency, contact_school, contact_other,
     disposition_provider, disposition_agency, disposition_school,
     disposition_onsite)
  values
    (p_sid, p_empid, p_time, p_visit_date, p_time_in, p_time_out, p_complaint,
     p_other_complaint, p_temperature, p_care_given, p_care_given_other,
     p_care_assessment_notes, p_care_assessment_type, p_care_transport,
     p_care_vital_notes, p_parent_contact, p_disposition, p_comments, p_locked,
     p_parent_contact_other, p_referral, p_recorded_by, p_data_entry,
     p_disposition_other, p_school_id, p_plan, p_complaint_minutes, v_id,
     p_contact_provider, p_contact_agency, p_contact_school, p_contact_other,
     p_disposition_provider, p_disposition_agency, p_disposition_school,
     p_disposition_onsite);

  /* ship back id */
  p_visit_id := v_id;

end visit_insert;

/************************************************************************/
procedure visit_update(p_sid                   in number,
                       p_empid                 in number,
                       p_time                  in number,
                       p_visit_date            in date,
                       p_time_in               in varchar2,
                       p_time_out              in varchar2,
                       p_complaint             in varchar2,
                       p_other_complaint       in varchar2,
                       p_temperature           in varchar2,
                       p_care_given            in varchar2,
                       p_care_given_other      in varchar2,
                       p_care_assessment_notes in varchar2,
                       p_care_assessment_type  in varchar2,
                       p_care_transport        in varchar2,
                       p_care_vital_notes      in varchar2,
                       p_parent_contact        in varchar2,
                       p_disposition           in varchar2,
                       p_comments              in varchar2,
                       p_locked                in number,
                       p_parent_contact_other  in varchar2,
                       p_referral              in varchar2,
                       p_recorded_by           in number,
                       p_data_entry            in number,
                       p_disposition_other     in varchar2,
                       p_school_id             in number,
                       p_plan                  in number,
                       p_complaint_minutes     in number,
                       p_contact_provider      in varchar2,
                       p_contact_agency        in varchar2,
                       p_contact_school        in varchar2,
                       p_contact_other         in varchar2,
                       p_disposition_provider  in varchar2,
                       p_disposition_agency    in varchar2,
                       p_disposition_school    in varchar2,
                       p_disposition_onsite    in varchar2,
                       p_visit_id              in number)
is
begin

  /* update record */
  update nursing.office_visits
     set sid                   = p_sid,
         empid                 = p_empid,
         time                  = p_time,
         visit_date            = p_visit_date,
         time_in               = p_time_in,
         time_out              = p_time_out,
         complaint             = p_complaint,
         other_complaint       = p_other_complaint,
         temperature           = p_temperature,
         care_given            = p_care_given,
         care_given_other      = p_care_given_other,
         care_assessment_notes = p_care_assessment_notes,
         care_assessment_type  = p_care_assessment_type,
         care_transport        = p_care_transport,
         care_vital_notes      = p_care_vital_notes,
         parent_contact        = p_parent_contact,
         disposition           = p_disposition,
         comments              = p_comments,
         locked                = p_locked,
         parent_contact_other  = p_parent_contact_other,
         referral              = p_referral,
         --recorded_by           = p_recorded_by,
         data_entry            = p_data_entry,
         disposition_other     = p_disposition_other,
         school_id             = p_school_id,
         plan                  = p_plan,
         complaint_minutes     = p_complaint_minutes,
         contact_provider      = p_contact_provider,
         contact_agency        = p_contact_agency,
         contact_school        = p_contact_school,
         contact_other         = p_contact_other,
         disposition_provider  = p_disposition_provider,
         disposition_agency    = p_disposition_agency,
         disposition_school    = p_disposition_school,
         disposition_onsite    = p_disposition_onsite
   where visit_id              = p_visit_id;

end visit_update;

/************************************************************************/
procedure visit_billed(p_visit_id in number,
                       p_billed   in date)
is
  v_date date;
begin

  if p_billed is null then
    v_date := sysdate;
  else
    v_date := p_billed;
  end if;

  /* set billed flag */
  update nursing.office_visits
     set billed   = v_date
   where visit_id = p_visit_id;

end visit_billed;

/************************************************************************/
procedure visit_approved(p_visit_id in number)
is
begin

  /* set approved flag */
  update nursing.office_visits set approved = 1 where visit_id = p_visit_id;

end visit_approved;

/************************************************************************/
procedure visit_delete(p_visit_id in number)
is
begin
  /* remove visit */
  delete from nursing.office_visits where visit_id = p_visit_id;

end visit_delete;

/************************************************************************/
end office_visit_pkg;
