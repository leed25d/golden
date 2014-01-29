col treatment_id format 9999999999
col service_date format a12
col mtyp format 9999
col mins format 9999
col outcome format a12
col billed format a12

set lines 120
set pages 60

define empid=116701
define student_id=27931154

select treatment_id, to_char(service_date, 'MM-DD-YYYY'), minutes_type mtyp, minutes, outcome, billed
from para.treatments
where empid=&empid and sid=&student_id
and to_char(service_date, 'MM-DD-YYYY') in ('09-18-2013','09-20-2013','09-23-2013','09-25-2013','09-27-2013','09-30-2013')
order by service_date
/
