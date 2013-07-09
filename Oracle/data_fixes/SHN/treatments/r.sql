col sfn format a10
col sln format a10
col ufn format a10
col uln format a10
col comments format a20
col minutes format a4 hea min
col minutes_type format a7 hea m-type
set lines 220
set pages 60

select
   t.treatment_id,
   t.sid,
   s.fname   sfn,
   s.lname   sln,
   t.empid,
   u.fname   ufn,
   u.lname   uln,
   substr(t.comments, 1, 15),
   t.service_date,
   t.minutes_type,
   t.minutes minutes,
   t.billed
from
   para.treatments t,
   common.students s,
   common.users u
where  1=1
   and t.sid=29256836
   and t.empid=27530314
   and substr(t.comments, 1, 15) is null
   and s.student_id=t.sid
   and u.userid=t.empid
order by service_date desc
/
