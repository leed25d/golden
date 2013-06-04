set pages 999
set lines 150
col lname format a30
col name format a45

select lname, cu.name, s.survey_id, s.locked_date
, s.total_time, s.state
from maa.survey s, common.entity_cu cu
where quarter_reporting is null
and s.claiming_unit_id = cu.cu_id
order by cu.name
/
