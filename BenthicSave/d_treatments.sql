define trids=8806662,8806698,8806748,8806786,8806878,8806910,8806948,8806970,8807024,8807060,8807858,8807910

select treatment_id, outcome, service_date, minutes_type, minutes, billed
from para.treatments where treatment_id in (&trids)
/

--  update para.treatments set outcome='absent' where treatment_id in (&trids)
--  delete from para.treatments where treatment_id in (&trids);

