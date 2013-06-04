insert into para.rules (name, type, key, value, pos)
	(select 'cota_assessments', r.type, r.key, r.value, r.pos
	from para.rules r
	where r.name = 'ot_assessments')
