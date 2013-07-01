define trids=7798878,7860642,7922424,8020586,8020682,8126178,8126248,8238114,8239222,8337790,8723998,8810784,8925274,8925326,9035906,9135744,9190844,9306236,9306262,9449538,9564378,9674008,9674022,9800010,9800086,9905678,9905734,9999366,9999438,10126334


--  select treatment_id, sid, empid, service_date
--  from para.treatments
--  where treatment_id in (&trids)
--  or ((sid=18131270 or empid=28108932) and to_char(service_date, 'YYYY-MM-DD')= '2013-06-25')
/

select treatment_id, sid, empid, service_date
from para.treatments
where (sid=18131270 or empid=28108932) and service_date= to_date('2013-06-25','YYYY-MM-DD')
/

select *
from para.treatments where treatment_id in (&trids)
/

--  update para.treatments set outcome='absent' where treatment_id in (&trids)
--  update para.treatments set minutes_type=1 where treatment_id in (&trids)
--  delete from para.treatments where treatment_id in (&trids);

