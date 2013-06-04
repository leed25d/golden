drop user rmts cascade;
create user rmts identified by "blahblah"
default tablespace users quota unlimited on users;

grant connect,resource to rmts;
grant references on common.entity_user to rmts;
grant references on maa.survey_date to rmts;
insert into common.entity_user
	(entity_id, ccode, username)
values (0,'XX','system_placeholder');

