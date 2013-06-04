set pages 999
set lines 150
col lname format a25
col fname format a25
col user_type format a10
col role_id format a20

select e.entity_id, e.lname, e.fname, e.user_type, r.role_id
from p_import.accounts_new a
, common.entity_user e
, common.entity_role r
where a.userid = e.entity_id
and e.entity_id = r.entity_id
and r.role_id like 'shn%'
/
