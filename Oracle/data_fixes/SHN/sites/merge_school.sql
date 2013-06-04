col lea format a3
col location format a12
col name format a25 trunc
set pages 999
set lines 120

select 'Going Away' from common.entity_school where school_id = &&going_away;

select 'Keeping', name, location, lea, school_id
from common.entity_school
where school_id = &&keeping;

select decode(school_id, &&keeping,'Keeping','Going Away') status, school_id, lea, location, name
from common.entity_school where school_id in (&&keeping, &&going_away);

update nursing.office_visits
set school_id = &&keeping
where school_id = &&going_away;

update nursing.sp_students
set school_id = &&keeping
where school_id = &&going_away;

update para.assessments
set school_id = &&keeping
where school_id = &&going_away;

update para.assessments_iep
set school_id = &&keeping
where school_id = &&going_away;

update para.screenings
set school_id = &&keeping
where school_id = &&going_away;

update para.treatments
set school_id = &&keeping
where school_id = &&going_away;

delete from common.entity_school_link lnk
where lnk.school_id = &&going_away and lnk.entity_id in (
        select unlnk.entity_id from common.entity_school_link unlnk
        where unlnk.school_id in (&&going_away, &&keeping)
        group by unlnk.entity_id having count(*)>1
        );

update common.entity_school_link
set school_id = &&keeping
where school_id = &&going_away;

update common.entity_student
set school_id = &&keeping
where school_id = &&going_away;

delete from common.entity_school where school_id = &&going_away;

select 'Commit or Roll Back NOW!' action_required from dual;

