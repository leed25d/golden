insert into p_import.test_shn_billing
(lea, lea_sid, lname, fname, dob, gender, service_date, lea_empid, quantity)
select s.lea
, s.student_number
, s.lname
, s.fname
, s.birthdate
, s.gender
, o.visit_date
, nvl(p.prov_code, p.username)
, o.complaint_minutes
from nursing.office_visits O, common.students S, common.users P
where O.sid = S.student_id and
      O.empid = P.userid and
      O.locked is not null and
      P.ccode != 'XX' and
      O.visit_date > '2003-06-30' and
      (
        ( O.data_entry is null )
          OR
        ( O.data_entry is not null and O.approved is not null )
      )
/
