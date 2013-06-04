select cu.ccode, count(*)
from maa.survey s, common.entity_cu cu
where s.claiming_unit_id = cu.cu_id
and cu.ccode in (&ccode_list)
and s.quarter_reporting = &quarter_id
and s.esig_on is null
group by cu.ccode
/
