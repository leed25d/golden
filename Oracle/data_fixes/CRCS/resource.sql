select concat('alter table '
, table_schema
, '.'
, table_name
, ' change column '
, column_name
, ' resource_code '
, column_type
, ';') stmt
from columns 
where table_schema='maa_load'
and upper(column_name) like 'RES%'
and upper(column_name) not like 'RESP%'
order by column_name;
