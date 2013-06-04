select * from maa.survey where survey_id=721502;

SELECT SUM(NVL(h.time,0)) FROM maa.survey_hour h, maa.activities a WHERE h.activity_id = a.activity_id AND h.survey_id = 721502 AND a.activity_number in (4,6,8,10,12,14,15,16); 

select * from maa.survey_hour where survey_id=721502 order by activity_id;

select * from maa.activities order by activity_number;