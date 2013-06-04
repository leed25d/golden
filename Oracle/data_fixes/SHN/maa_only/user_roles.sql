col ccode heading LEA format a4
col lname format a25
col fname format a15
col username format a12
col role_id format a25

set pages 999
set lines 120

select u.ccode, u.lname, u.fname, u.username, u.entity_id empid
, r.role_id
from common.entity_user u, common.entity_role r, maa_only m
where u.entity_id = r.entity_id
and m.empid = u.entity_id
and r.role_id like 'shn_%'
and u.ccode <> 'XX'
order by u.ccode, u.lname, r.role_id
/
