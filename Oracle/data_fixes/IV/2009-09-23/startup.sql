

accept kill_date date prompt "Enter date for last day to purge (YYYY-MM-DD): "
accept lea_code char prompt "Enter LEA code to purge data: "

insert into temp_emp (
	select u.entity_id from common.entity_user u, common.entity_role r
	where u.entity_id = r.entity_id
	and r.role_id = 'shn_speech'
	and ccode = '&lea_code');
commit;

