update p_import.accounts_new a set userid = (
select e.entity_id
from common.entity_user e
where e.lname = upper(a.lname)
and e.fname = upper(a.fname)
and e.ccode = a.ccode)
/
