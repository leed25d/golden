set pages 999
set lines 200
col complaint format a20 wrap
col care_given format a20 wrap
col disposition format a20 wrap
col comments format a20 wrap

select visit_id,visit_date, time_in,time_out, complaint, care_given,disposition,comments
from nursing.office_visits
where empid=&&empid and sid=&&student_id
/
