define ccode_list="&enter_quoted_ccodes"

set pages 999
set lines 200
col cuName format a40 trunc

select cu_id, name cuName, esig_on
from common.entity_cu cu
where cu.ccode in (&ccode_list)
/
