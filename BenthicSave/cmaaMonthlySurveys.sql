select * from maa.ph_survey where rownum < 1000;

select distinct survey_type_id from maa.ph_survey;

select distinct days_in_period from maa.ph_survey;

select * from common.entity_cu_ph where ccode='439';

--update  common.entity_cu_ph set survey_type_id=4 where ccode='439';

select jcm_id, j.cu_id, year1, year2, j.survey_type_id  from maa.ph_jcm j, common.entity_cu_ph c
where
j.cu_id = c.cu_id and
year1=2012 and
c.ccode='439';


select jcml_id from maa.ph_jcm_class_link where jcm_id in (2000928,2000936,2000940,2000944,2000946,2000948,2000950,2000952,2000964,2000968,2000972,2000974,2000930,2000954,2000932,2000956,2000958,2000970,2001142,2000938,2000942,2000960,2000962,2000966,2001140);

select virtual_day1, to_char(virtual_day1, 'dd') from maa.ph_survey where jcml_id in (select jcml_id from maa.ph_jcm_class_link where jcm_id in (2000928,2000936,2000940,2000944,2000946,2000948,2000950,2000952,2000964,2000968,2000972,2000974,2000930,2000954,2000932,2000956,2000958,2000970,2001142,2000938,2000942,2000960,2000962,2000966,2001140)) and rownum < 100;

select survey_type_id, days_in_period, virtual_day1,  to_char(virtual_day1, 'mm')  from  maa.ph_survey where jcml_id in (select jcml_id from maa.ph_jcm_class_link where jcm_id in (2000928,2000936,2000940,2000944,2000946,2000948,2000950,2000952,2000964,2000968,2000972,2000974,2000930,2000954,2000932,2000956,2000958,2000970,2001142,2000938,2000942,2000960,2000962,2000966,2001140));

--update maa.ph_survey set survey_type_id=4, days_in_period=31 where jcml_id in (select jcml_id from maa.ph_jcm_class_link where jcm_id in (2000928,2000936,2000940,2000944,2000946,2000948,2000950,2000952,2000964,2000968,2000972,2000974,2000930,2000954,2000932,2000956,2000958,2000970,2001142,2000938,2000942,2000960,2000962,2000966,2001140));

--thirty days hath september, april, june and november
--update maa.ph_survey set days_in_period=30 where jcml_id in (select jcml_id from maa.ph_jcm_class_link where jcm_id in (2000928,2000936,2000940,2000944,2000946,2000948,2000950,2000952,2000964,2000968,2000972,2000974,2000930,2000954,2000932,2000956,2000958,2000970,2001142,2000938,2000942,2000960,2000962,2000966,2001140)) and to_char(virtual_day1, 'mm') in ('09', '04', '06', '11');

--28 days hath february
--update maa.ph_survey set days_in_period=29 where jcml_id in (select jcml_id from maa.ph_jcm_class_link where jcm_id in (2000928,2000936,2000940,2000944,2000946,2000948,2000950,2000952,2000964,2000968,2000972,2000974,2000930,2000954,2000932,2000956,2000958,2000970,2001142,2000938,2000942,2000960,2000962,2000966,2001140)) and to_char(virtual_day1, 'mm') = '02';

--delete from maa.ph_survey where jcml_id in (select jcml_id from maa.ph_jcm_class_link where jcm_id in (2000928,2000936,2000940,2000944,2000946,2000948,2000950,2000952,2000964,2000968,2000972,2000974,2000930,2000954,2000932,2000956,2000958,2000970,2001142,2000938,2000942,2000960,2000962,2000966,2001140)) and  to_char(virtual_day1, 'dd') != '01';



--update maa.ph_jcm set survey_type_id=4 where jcm_id in (2000928,2000936,2000940,2000944,2000946,2000948,2000950,2000952,2000964,2000968,2000972,2000974,2000930,2000954,2000932,2000956,2000958,2000970,2001142,2000938,2000942,2000960,2000962,2000966,2001140)
--
