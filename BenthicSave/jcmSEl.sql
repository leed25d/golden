select JCM_ID, ACTIVITY_GROUP from maa.ph_jcm where jcm_id in (2001296,2001298,2001300,2001310,2001314,2001304,2001308,2001320,2001318,2001316);

select JCM_ID, ACTIVITY_GROUP from maa.ph_jcm where cu_id= 900882;

update maa.ph_jcm set activity_group=1 where jcm_id in (2001296,2001298,2001300,2001310,2001314,2001304,2001308,2001320,2001318,2001316);


-- fix a bug noticed by Ruby on 30APR2013
define jcmid_list = "2001338,2001340,2001342,2001344,2001346,2001348,2001350,2001352,2001354,2001356,2001296,2001298,2001300,2001310,2001314,2001336,2001308,2001320,2001318,2001316";

select m.jcm_id, m.activity_group, c.name from maa.ph_jcm m, common.ph_claiming_units c where m.jcm_id in (&jcmid_list) and m.cu_id=c.cu_id order by c.name asc;

 
select j.jcm_id, j.cu_id, j.activity_group, j.year1, j.year2, c.ccode, c.name , to_char(j.start_date, 'Mon-YY') start_date , to_char(j.end_date, 'Mon-YY') end_date from maa.ph_jcm j, common.ph_claiming_units c where 0 = 0 and c.ccode = 'ZZ12' and j.cu_id = c.cu_id order by c.name asc;

update maa.ph_jcm set activity_group=1 where jcm_id in (&jcmid_list);

--oorts
select distinct * from common.lk_entity_ccode where upper(ccode)='ZZ12';

select cu_id, ccode, name from common.ph_claiming_units where upper(ccode)='ZZ12';