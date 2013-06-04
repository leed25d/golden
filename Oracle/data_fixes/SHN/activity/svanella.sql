select service_date, count(*) from para.assessments where empid=21254508
and service_date >(sysdate-20)
group by service_date;


select treatment_id, service_date from para.treatments where empid=21254508
and service_date > (sysdate-20)
and to_char(service_date,'DAY') in ('TUESDAY','THURSDAY');

select treatment_id, service_date, to_char(service_date,'Day') day
from para.treatments where empid=21254508
and service_date > to_date('7-1-2009','MM-DD-YYYY')
and to_char(service_date,'Day') like 'T%'
order by service_date
/


