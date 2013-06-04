update maa.survey x set x.esig_on='y'
where x.esig_on is null
and x.quarter_reporting = &quarter_id
and x.claiming_unit_id in (
	select cu.cu_id from common.entity_cu cu where cu.ccode in (&ccode_list)
	)
/
