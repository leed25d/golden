------------------------------------------------------------------------
------------------------------------------------------------------------
--  this is a bunch of queries that I used while I was testint the    --
--  billing script with Joe Peyton                                    --
--                                                                    --
--  when you are working with the billing script, don't forget to     --
--  clear the para.bill table occasianally if you are doing           --
--  multiple runs                                                     --
------------------------------------------------------------------------
------------------------------------------------------------------------
select count(*) from para.bill;

delete from para.bill;

--  each of these selects grabs all of the properties of my test rows
select * from nursing.office_visits where visit_date = to_date('2012/05/18','yyyy/mm/dd') order by time_in asc;

select * from nursing.office_visits where visit_id > 1601360 order by visit_id asc;

select * from nursing.office_visits where sid=20881084 order by visit_id asc;

--  this select grabs an abbreviated list of properties
select sid,visit_id, visit_date, rep_date, time_in, recorded_by, empid, data_entry, billed, approved, locked from nursing.office_visits where visit_id > 1601360 order by visit_id asc;


--  visit_date 366 days from today.
update  nursing.office_visits set visit_date = sysdate - 366 where visit_id = 1601362;

--  visit_date 364 days from today.
update  nursing.office_visits set visit_date = sysdate - 364 where visit_id = 1601364;

--   visit_date yesterday.
update  nursing.office_visits set visit_date = sysdate - 1 where visit_id = 1601366;

--   visit_date 40 days ago.
update  nursing.office_visits set visit_date = sysdate - 40 where visit_id = 1601368;

--   visit_date 40 days ago.
update  nursing.office_visits set visit_date = sysdate - 40 where visit_id = 1601370;

--   visit_date 8 days ago.
update  nursing.office_visits set visit_date = sysdate - 8 where visit_id = 1601372;

update nursing.office_visits set billed = NULL where visit_id in (1601368,1601370);

select * from nursing.office_visits ov where ov.locked is not null and ov.billed is null and ((ov.approved = 1 and (ov.recorded_by is not null or ov.data_entry is not null)) or (ov.recorded_by is not null and ov.empid is not null and ov.recorded_by <> ov.empid and data_entry = 0) or (ov.approved is null  and (ov.recorded_by is null and  ov.data_entry is null))) and ov.visit_date > (sysdate - 365) and ov.visit_date < to_date('2012-05-20','YYYY-MM-DD') and ov.visit_date < (sysdate - 7) and ov.visit_date > to_date('2006-06-30', 'YYYY-MM-DD');

select * from nursing.office_visits ov where (ov.recorded_by is not null and ov.empid is not null and ov.recorded_by <> ov.empid and data_entry = 0);