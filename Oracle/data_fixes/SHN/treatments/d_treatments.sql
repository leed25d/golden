
set lines 220
set pages 60

define trids = 11476762,11476820,11476908,11476952

select treatment_id, plan, outcome, service_date, minutes_type, minutes, billed
from para.treatments where treatment_id in (&trids)
/

--  update para.treatments set plan='NONE' where treatment_id in (&trids)
--  update para.treatments set outcome='absent' where treatment_id in (&trids)
--  delete from para.treatments where treatment_id in (&trids)

