select treatment_id, service_date, minutes_type, minutes, billed
from para.treatments
where empid=&empid and sid=&student_id
order by service_date
/
