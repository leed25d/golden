-- Hey dude.  Can you tell me which clients have health aides that
-- have data entered something for a nurse in the past year?

select * from nursing.office_visits where visit_date > sysdate - 365 and recorded_by is not null and empid  != recorded_by;

select t.empid, u1.username, u1.fname, u1.lname, u1.ccode as NCC, t.recorded_by, u2.username, u2.fname, u2.lname, u2.ccode as HCC from common.entity_user u1,   common.entity_user u2,  (select distinct recorded_by, empid from nursing.office_visits where visit_date > sysdate - 365 and recorded_by is not null and empid  != recorded_by order by recorded_by, empid) t where u1.entity_id = t.empid and u2.entity_id = t.recorded_by order by NCC,HCC;


select distinct HCC from (select t.empid, u1.username, u1.fname, u1.lname, u1.ccode as NCC, t.recorded_by, u2.username, u2.fname, u2.lname, u2.ccode as HCC from common.entity_user u1,   common.entity_user u2,  (select distinct recorded_by, empid from nursing.office_visits where visit_date > sysdate - 365 and recorded_by is not null and empid  != recorded_by order by recorded_by, empid) t where u1.entity_id = t.empid and u2.entity_id = t.recorded_by order by NCC,HCC) order by HCC asc;



-- select distinct recorded_by, empid, school_id, visit_date from nursing.office_visits n where visit_date > sysdate - 365 and recorded_by is not null and empid  != recorded_by;

select distinct recorded_by, empid from nursing.office_visits where visit_date > sysdate - 365 and recorded_by is not null and empid  != recorded_by order by recorded_by, empid;

select * from nursing.office_visits where visit_id > 1601360 order by visit_id asc;
