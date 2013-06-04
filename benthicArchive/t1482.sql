select sid, visit_date, time_out, time_in, complaint_minutes from nursing.office_visits order by sid, visit_date, time_in;

alter table nursing.office_visits add (complaint_assessment number);

select *
from
( select  
       sid, visit_date, time_out, time_in, complaint_minutes, 
       nvl(cast(complaint_assessment as varchar(2)), 'NULL') as complaint_assessment
  from  nursing.office_visits
  where  complaint_minutes is not null
  order by  sid, visit_date,time_in )
where ROWNUM <= 100;

select sid, visit_date, time_in, time_out, complaint_minutes, complaint_assessment from nursing.office_visits where visit_id=1641694;

select 'null visit_date: ' || count(*) from nursing.office_visits where visit_date is null
union
select 'null time_in: ' || count(*) from nursing.office_visits where time_in is null
union
select 'null time_out: ' || count(*) from nursing.office_visits where time_out is null
union
select 'null complaint_minutes: ' || count(*) from nursing.office_visits where complaint_minutes is null or complaint_minutes=0;

--  this piece of SQL calculates the treatment span 
--      (defined as (time out)- (time in)) 
--  
--  for all rows that have valid times.
select
         time_in, time_out, visit_date, complaint_minutes,
         cast(((to_date((to_char(visit_date, 'YYYY-MM-DD') || ' ' || time_out), 'YYYY-MM-DD HH24:MI') -
         to_date((to_char(visit_date, 'YYYY-MM-DD') || ' ' || time_in), 'YYYY-MM-DD HH24:MI')) * 24 * 60) as integer) as complaintSpan,
         to_date((to_char(visit_date, 'YYYY-MM-DD') || ' ' || time_out), 'YYYY-MM-DD HH24:MI') as timeOut,
         to_date((to_char(visit_date, 'YYYY-MM-DD') || ' ' || time_in), 'YYYY-MM-DD HH24:MI') as timeIn

from
         nursing.office_visits

where
        time_in  like '%:%' and
        regexp_replace(time_in, ':.*') <= 23 and
        regexp_replace(time_in, '.*:') <= 59 and
        time_out  like '%:%' and
        regexp_replace(time_out, ':.*') <= 23 and
        regexp_replace(time_out, '.*:') <= 59 and
        complaint_minutes >= 0;


select 'bad time_in: ' || count(*) from nursing.office_visits where regexp_replace(time_in, ':.*') > 23;

select time_in from nursing.office_visits where regexp_replace(time_in, ':.*') > 23;

select time_in from nursing.office_visits where regexp_replace(time_in, '.*:') > 59;

select time_in from nursing.office_visits where time_in not like '%:%';

select time_out from nursing.office_visits where time_out not like '%:%';

update nursing.office_visits set complaint_assessment=5 where visit_id=1641694;
