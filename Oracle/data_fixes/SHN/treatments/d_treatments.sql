define trids = 7494188
select treatment_id, outcome, service_date, minutes_type, minutes, billed
from para.treatments where treatment_id in (&trids)
/

--  update para.treatments set outcome='absent' where treatment_id in (&trids)
--  delete from para.treatments where treatment_id in (&trids)

