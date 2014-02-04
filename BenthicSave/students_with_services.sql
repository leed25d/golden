select a.entity_id, b.ctr
from common.entity_student a,
     (select sid, count (sid) ctr from para.assessments group by sid having count(sid) > 0) b
where b.sid=a.entity_id;

--  para.ap                           &tab
--  para.assessments                  &tab
--  common.entity_group_link          &tab
--  para.notes                        &tab
--  common.entity_student_link        &tab
--  para.screenings                   &tab
--  para.treatments                   &tab
--  para.treatment_areas              &tab
--  nursing.office_visits             &tab
--  nursing.referrals                 &tab
--  nursing.sp_students               &tab
--  nursing.sp_visits                 &tab

select 'para.ap' tab, sid, count(sid) ctr from para.ap where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;
select 'para.assessments' tab, sid, count(sid) ctr from para.assessments where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;
select 'common.entity_group_link' tab, entity_id sid, count(entity_id) ctr from common.entity_group_link where entity_id in (select entity_id from common.entity_student where ccode='EF') group by entity_id;
select 'para.notes' tab, sid, count(sid) ctr from para.notes where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;
select 'common.entity_student_link' tab, student_id, count(student_id) ctr from common.entity_student_link where student_id in (select entity_id from common.entity_student where ccode='EF') group by student_id;
select 'para.screenings' tab, sid, count(sid) ctr from para.screenings where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;
select 'para.treatments' tab, sid, count(sid) ctr from para.treatments where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;
select 'para.treatment_areas' tab, sid, count(sid) ctr from para.treatment_areas where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;
select 'nursing.office_visits' tab, sid, count(sid) ctr from nursing.office_visits where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;
select 'nursing.referrals' tab, sid, count(sid) ctr from nursing.referrals where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;
select 'nursing.sp_students' tab, sid, count(sid) ctr from nursing.sp_students where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;
select 'nursing.sp_visits' tab, sid, count(sid) ctr from nursing.sp_visits where sid in (select entity_id from common.entity_student where ccode='EF') group by sid;


--stopper
select sysdate from dual;

select distinct tab from (
select 'para.ap' tab, sid, count(sid) ctr from para.ap where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
UNION
select 'para.assessments' tab, sid, count(sid) ctr from para.assessments where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
UNION
select 'common.entity_group_link' tab, entity_id sid, count(entity_id) ctr from common.entity_group_link where entity_id in (select entity_id from common.entity_student where ccode='DM') group by entity_id
UNION
select 'para.notes' tab, sid, count(sid) ctr from para.notes where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
UNION
select 'common.entity_student_link' tab, student_id, count(student_id) ctr from common.entity_student_link where student_id in (select entity_id from common.entity_student where ccode='DM') group by student_id
UNION
select 'para.screenings' tab, sid, count(sid) ctr from para.screenings where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
UNION
select 'para.treatments' tab, sid, count(sid) ctr from para.treatments where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
UNION
select 'para.treatment_areas' tab, sid, count(sid) ctr from para.treatment_areas where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
UNION
select 'nursing.office_visits' tab, sid, count(sid) ctr from nursing.office_visits where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
UNION
select 'nursing.referrals' tab, sid, count(sid) ctr from nursing.referrals where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
UNION
select 'nursing.sp_students' tab, sid, count(sid) ctr from nursing.sp_students where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
UNION
select 'nursing.sp_visits' tab, sid, count(sid) ctr from nursing.sp_visits where sid in (select entity_id from common.entity_student where ccode='DM') group by sid
) order by tab
;

select * from common.students where student_id in (select distinct entity_id from(
select entity_id, ccode, lname, fname, plan_indicator, type, count(service_id) from
(select distinct 'assessment' as type, a.assessment_id as service_id,
s.entity_id, s.ccode, s.lname, s.fname, s.plan_indicator from PARA.ASSESSMENTS a
left join common.entity_student s on a.sid = s.entity_id
where ccode='BF'
UNION
select distinct 'assesment_iep' as type, a.assessment_id as service_id,
s.entity_id, s.ccode, s.lname, s.fname, s.plan_indicator from PARA.ASSESSMENTS_IEP a
left join common.entity_student s on a.sid = s.entity_id
where ccode='BF'
UNION
select distinct 'treatment' as type, t.treatment_id as service_id,
s.entity_id, s.ccode, s.lname, s.fname, s.plan_indicator from PARA.TREATMENTS t
left join common.entity_student s on t.sid = s.entity_id
where ccode='BF'
UNION
select distinct 'notes' as type, n.note_id as service_id,
s.entity_id, s.ccode, s.lname, s.fname, s.plan_indicator from PARA.NOTES n
left join common.entity_student s on n.sid = s.entity_id
where ccode='BF'
UNION
select distinct 'screenings' as type, ps.SCREENING_ID as service_id,
s.entity_id, s.ccode, s.lname, s.fname, s.plan_indicator from PARA.SCREENINGS ps
left join common.entity_student s on ps.sid = s.entity_id
where ccode='BF'
UNION
select distinct 'office visits' as type, n.visit_id as service_id,
s.entity_id, s.ccode, s.lname, s.fname, s.plan_indicator from NURSING.OFFICE_VISITS n
left join common.entity_student s on n.sid = s.entity_id
where ccode='BF'
UNION
select distinct 'referrals' as type, n.referral_id as service_id,
s.entity_id, s.ccode, s.lname, s.fname, s.plan_indicator from NURSING.REFERRALS n
left join common.entity_student s on n.sid = s.entity_id
where ccode='BF'
UNION
select distinct 'special procedures' as type, n.sp_visit_id as service_id,
s.entity_id, s.ccode, s.lname, s.fname, s.plan_indicator from NURSING.SP_VISITS n
left join common.entity_student s on n.sid = s.entity_id
where ccode='BF')
group by entity_id, ccode, lname, fname, plan_indicator, type));
