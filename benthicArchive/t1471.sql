update maa.survey set quarter_reporting=568 where survey_id=749016;

select * from maa.survey where survey_id=749016;

select * from maa.survey_date order by year1 desc, year2 desc, quarter asc;

select * from maa.survey_date order by qstart asc;


select survey_date_id from maa.survey_date where qstart >= to_date('2011-07-01', 'YYYY-MM-DD') and qend <= to_date('2012-07-01', 'YYYY-MM-DD');