insert into p_import.test_shn_billing
(lea, lea_sid, lname, fname, dob, gender, service_date, lea_empid, quantity)
select p.ccode
, s.student_number
, s.lname
, s.fname
, s.birthdate
, s.gender
, v.visit_date
, nvl(p.prov_code, p.username)
, v.time
from nursing.sp_visits V, common.users P,
      common.students S, nursing.sp_students D
where V.sp_student_id = D.sp_student_id and
      D.sid = S.student_id and
      V.empid = P.userid and
      P.ccode != 'XX' and
      V.visit_date > '2003-06-30' and
      V.time is null
/
