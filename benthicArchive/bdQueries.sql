select to_char(bill_date, 'mm/dd/yyyy') from para.bill order by bill_date desc;


select to_char(bill_date, 'mm/dd/yyyy') as bill_date, count(bill_date) as count from para.bill group by to_char(bill_date, 'mm/dd/yyyy') order by bill_date desc;

select billed, count(billed) as count, to_char(billed, 'MM/DD/YYYY') as st_billed from nursing.office_visits where billed is not null group by billed order by billed desc;

select count(*) from nursing.office_visits  where billed = to_date('2012-04-18', 'yyyy-mm-dd');


select sid, visit_date, billed, approved, locked, recorded_by, data_entry  from nursing.office_visits  where sid = 27682892;

select count(*) from nursing.office_visits  where billed = to_date('2012-04-18', 'yyyy-mm-dd');

select count(*) from para.bill where bill_date = to_date('2012-04-18', 'yyyy-mm-dd');


delete from para.bill where bill_date = to_date('2012-04-18', 'yyyy-mm-dd');

update nursing.office_visits set billed = NULL where billed = to_date('2012-04-18', 'yyyy-mm-dd');


select count(*) from nursing.office_visits  where approved is null;

update nursing.office_visits set visit_date=  to_date('2012-03-28', 'yyyy-mm-dd')where sid= 27682892 and visit_date= to_date('2012-04-18', 'yyyy-mm-dd');