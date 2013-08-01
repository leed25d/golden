select * from COMMON.ENTITY_USER where username='genglar' and ccode='BI';
select * from COMMON.ENTITY_USER where username='sbattaglia' and ccode='BI';
select * from COMMON.ENTITY_USER where username='lsarver' and ccode='BI';

select * from COMMON.ENTITY_CU where name like 'Pacific View%';

select eu.entity_id, eu.ccode, eu.username, ec.cu_id, ec.ccode, ec.name
from COMMON.ENTITY_USER eu, COMMON.ENTITY_CU_LINK ecl,
COMMON.ENTITY_CU ec
where eu.entity_id=ecl.entity_id and ec.cu_id = ecl.cu_id
and eu.username='genglar'
order by eu.entity_id, eu.ccode;

select eu.entity_id, eu.ccode, eu.username, ec.cu_id, ec.ccode, ec.name
from COMMON.ENTITY_USER eu, COMMON.ENTITY_CU_LINK ecl,
COMMON.ENTITY_CU ec
where eu.entity_id=ecl.entity_id and ec.cu_id = ecl.cu_id
and eu.username='sbattaglia'
order by eu.entity_id, eu.ccode;


select eu.entity_id, eu.ccode, eu.username, ec.cu_id, ec.ccode, ec.name
from COMMON.ENTITY_USER eu, COMMON.ENTITY_CU_LINK ecl,
COMMON.ENTITY_CU ec
where eu.entity_id=ecl.entity_id and ec.cu_id = ecl.cu_id
and eu.username='lsarver'
order by eu.entity_id, eu.ccode;

-- select all of the claiming units that are attached to Greg Englars'
-- account.
select * from COMMON.ENTITY_CU_LINK where entity_id=133946;

-- Insert the Pacific Academy Charter Claiming Unit
-- insert into COMMON.ENTITY_CU_LINK values (133946,21749462);
-- Insert the Arcata Elementary School District Claiming Unit from his AE ccode account
-- insert into COMMON.ENTITY_CU_LINK values (133946,21749326);

select entity_id from COMMON.ENTITY_USER where username='sbattaglia' and ccode='BI';
select entity_id from COMMON.ENTITY_USER where username='lsarver' and ccode='BI';

-- Select the current set of clamining units attached to a user/ccode
select cu_id from COMMON.ENTITY_CU_LINK
where entity_id=
	(select entity_id from COMMON.ENTITY_USER
	where username='genglar' and ccode='BI');

select cu_id from COMMON.ENTITY_CU_LINK
where entity_id=
	(select entity_id from COMMON.ENTITY_USER
	where username='lsarver' and ccode='BI');

select cu_id from COMMON.ENTITY_CU_LINK
where entity_id=
	(select entity_id from COMMON.ENTITY_USER
	where username='sbattaglia' and ccode='BI');

/*
insert into COMMON.ENTITY_CU_LINK
(entity_id, cu_id)
select
	( select entity_id from COMMON.ENTITY_USER
		where username='sbattaglia' and ccode='BI') as entity_id,
	cu_id
from COMMON.ENTITY_CU_LINK
where entity_id=
	(select entity_id from COMMON.ENTITY_USER
	where username='genglar' and ccode='BI');

insert into COMMON.ENTITY_CU_LINK
(entity_id, cu_id)
select
	( select entity_id from COMMON.ENTITY_USER
		where username='lsarver' and ccode='BI') as entity_id,
	cu_id
from COMMON.ENTITY_CU_LINK
where entity_id=
	(select entity_id from COMMON.ENTITY_USER
	where username='genglar' and ccode='BI');
*/
--commit;
