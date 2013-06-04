insert into p_import.test_shn_billing
(lea, lea_sid, lname, fname, dob, gender, service_date, lea_empid, quantity)
select p.ccode
, s.student_number
, s.lname
, s.fname
, s.birthdate
, s.gender
, v.service_date
, nvl(p.prov_code, p.username)
, v.minutes
from common.users P, common.students S, para.assessments_iep V
where V.sid = S.student_id and
           V.empid = P.userid and
           P.ccode != 'XX' and
           V.service_date > '2003-06-30' and
           (
             ( V.data_entry is null )
               OR
             ( V.data_entry is not null and V.approved is not null )
           )
/
