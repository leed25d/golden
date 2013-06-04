define empid=20654622;
define student_id=27702896;
define tids=6861390,6982932,7100492,9138370;

select treatment_id, service_date, minutes_type, minutes, outcome, billed
from para.treatments
where empid=&empid and sid=&student_id and treatment_id in (&tids)
order by service_date
/


--update para.treatments set outcome='absent' where treatment_id=6861390;              --student absent
--update para.treatments set outcome='therapist_cancel' where treatment_id=6982932;    --threapist cancelled
--update para.treatments set outcome='absent' where treatment_id=7100492;              --student absent
--update para.treatments set outcome='therapist_cancel' where treatment_id=9138370;    --threapist cancelled
