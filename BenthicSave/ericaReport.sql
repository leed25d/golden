select count(*)

from para.treatments

where
    service_date >= to_date('7/1/2012', 'MM/DD/YYYY') and
    service_date <= to_date('5/22/2013','MM/DD/YYYY');


select * from common.schools where lower(name) like '%itchell%';
select distinct diagnostic_code from para.treatments;

select diagnostic_code, procedure_code from nursing.procedure_ref;

select
  S.fname || ' ' || S.lname student,
  T.service_date,
  T.minutes,
  T.minutes_type,
  T.outcome,
  T.plan,
  SCH.name,
  P.fname || ' ' || P.lname provider


from
  para.treatments T,
  common.users P,
  common.students S,
  common.schools SCH
where
  T.empid = P.userid and
  T.sid = S.student_id and
  P.type='S' and
  T.outcome = 'ok' and
  T.school_id = SCH.school_id(+) and
  SCH.location = '77' and
  T.service_date >= to_date('7/1/2012','MM/DD/YYYY') and T.service_date <= to_date('5/22/2013','MM/DD/YYYY')
order by
  T.service_date desc,
  T.time desc


--select
--  T.billed,
--  T.service_date,
--  T.school_id,
--  T.outcome,
--  T.minutes,
--  T.minutes_type,
--  T.sid,
--  T.comments,
--  T.accuracy,
--  T.diagnostic_code,
--  T.empid,
--  T.dismissed,
--  T.dismissed_other,
--  T.treatment_id,
--  P.fname || ' ' || P.lname provider,
--  S.fname || ' ' || S.lname student,
--  SCH.name,
--  T.plan,
--  common.util_pkg.from_unixtime( T.time ) date_entered,
--  T.authorization_id
--from
--  para.treatments T,
--  common.users P,
--  common.students S,
--  common.schools SCH
--where
--  T.empid = P.userid and
--  T.sid = S.student_id and
--  T.school_id = SCH.school_id(+) and
--  SCH.location = '77' and
--  T.service_date >= to_date('7/1/2012','MM/DD/YYYY') and T.service_date <= to_date('5/22/2013','MM/DD/YYYY')
--order by
--  T.service_date desc,
--  T.time desc
