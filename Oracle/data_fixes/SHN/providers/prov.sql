select p.lea, p.name_last_first, p.budget shn_prov_code
, p.pdox_prov_code
, p.cont shn_user_type
, p.user_type
, p.oracle_id
from p_import.pdox_providers p, common.users u
where u.ccode= p.lea
and (u.lname || ', ' || u.fname) = p.name_last_first
and u.prov_code = p.budget
