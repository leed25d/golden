insert into paradba.survey_counts
select sysdate
, claiming_unit_id
, quarter_reporting
, state
, valid
, count(*)
from maa.survey
where quarter_reporting > 200
group by claiming_unit_id, quarter_reporting, state, valid
/

commit
/

