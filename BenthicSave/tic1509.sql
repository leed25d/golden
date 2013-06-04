select
    P.fname || ' ' || P.lname name,
    T.empid,
    T.visit_date,
    T.data_entry

from
     common.entity_role R,
     common.users P,
     nursing.office_visits T

where
      T.empid = P.userid and
      P.password is not null and
      R.entity_id = T.empid and
--      R.role_id = 'shn_nursing' and
      T.recorded_by = 25399666

group by
      P.fname,
      P.lname,
      T.empid,
      T.visit_date,
      T.data_entry

--  having T.visit_date =
--        (select
--             max( N.visit_date )
--         from
--             nursing.office_visits N
--         where
--             N.recorded_by =  and
--             N.empid = T.empid)

order by T.visit_date desc;


select userid, ccode, prov_code, lname, fname,  username, password, set_password, type, remember_login, lea_type, whats_new from common.users where ccode='XX' and type='N' and password is not null;

desc para.bill;

select count(*) from para.bill ; --where bill_date < sysdate - 60;

delete from para.bill where bill_date sysdate - 60;
