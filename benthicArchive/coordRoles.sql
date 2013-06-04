--  Assign the 'smaa_email_btn' role to all smaa coordinators.
------------------------------------------------------------------------


--  this is the real SQL to do the task
create table para.save_enre as select * from common.entity_role;

delete from common.entity_role where role_id = 'smaa_email_btn';

insert into common.entity_role (select entity_id, 'smaa_email_btn' from common.entity_role where role_id = 'maa_coordinator');

drop table para.save_enre;


--  this is just splashing around while writing tha above
select * from common.lk_entity_role where role_id like '%email%';

select * from common.entity_role where role_id like '%email%';

select * from common.lk_entity_role where role_id like '%coo%';

select * from common.lk_entity_role where role_id = 'maa_coordinator';

select count(*) from common.entity_role where role_id = 'maa_coordinator';

select entity_id, 'maa_coordinator' from common.entity_role where role_id = 'maa_coordinator';


select count(*) from para.save_enre;

select count(*) from common.entity_role;


