select treatment_id, service_date, minutes_type, minutes, billed
from para.treatments
where treatment_id in (9830666)
order by service_date;


--delete from para.treatments where treatment_id in (9830666);