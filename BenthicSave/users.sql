select  distinct s.cu_id, cu.name, s.userid, s.fname, s.lname, i.username, c.scount
from maa.ph_survey s, common.users_info i, common.entity_cu_ph cu,
(select userid, count(userid) scount
from maa.ph_survey
where virtual_day1 >= to_date('2013-07-01','YYYY-MM-DD')
group by userid
having count(userid) > 1) c
where 1=1
and s.cu_id=cu.cu_id
and i.userid=s.userid
and s.userid = c.userid
order by s.userid;

select distinct cu.ccode, cu.name
from common.entity_cu_ph cu, maa.ph_jcm j
where 1=1
and cu.cu_id = j.cu_id
and j.survey_type_id = 4
and j.start_date < sysdate
and sysdate <= j.end_date
order by ccode, name;

select userid, count(userid)
from maa.ph_survey
where virtual_day1 >= to_date('2013-07-01','YYYY-MM-DD')
group by userid
having count(userid) > 1;

