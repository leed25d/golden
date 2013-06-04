select P.fname || ' ' || P.lname name, T.empid, T.visit_date, T.data_entry from common.users P, nursing.office_visits T where      T.empid = P.userid and T.recorded_by = 122835 group by P.fname, P.lname, T.empid, T.visit_date, T.data_entry having T.visit_date = (select max( N.visit_date ) from nursing.office_visits N where N.recorded_by = 122835 and N.empid = T.empid) order by T.visit_date desc;

select P.fname || ' ' || P.lname name, T.empid, T.visit_, OTHER_COMPLAINTdate, T.data_entry from common.users P, nursing.office_visits T where      T.empid = P.userid and T.recorded_by = 21855672 group by P.fname, P.lname, T.empid, T.visit_date, T.data_entry having T.visit_date = (select max( N.visit_date ) from nursing.office_visits N where N.recorded_by = 21855672 and N.empid = T.empid) order by T.visit_date desc;

select * from common.users where userid in (122835, 21855672);

select T.empid, P.fname || ' ' || P.lname name, T.data_entry, P.ccode from common.users P, nursing.office_visits T where T.empid = P.userid and  T.recorded_by = T.empid group by P.fname, P.lname, T.empid, T.data_entry, P.ccode order by empid;

select * from nursing.office_visits where empid = recorded_by and  empid = 122835;