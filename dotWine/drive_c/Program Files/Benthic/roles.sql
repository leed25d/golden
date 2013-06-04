select * from common.lk_entity_role where role_id like '%email%';

update common.lk_entity_role set role_id= 'smaa_email_btn' where role_id= 'smaa_email_btin';

select * from COMMON.ENTITY_ROLE where role_id like '%email%';

update COMMON.ENTITY_ROLE set role_id='smaa_email_btn' where role_id= 'smaa_email_btin';

select * from COMMON.ENTITY_ROLE where entity_id=25399666;

insert into COMMON.ENTITY_ROLE (entity_id,role_id) values (25399666,'smaa_email_btn');

