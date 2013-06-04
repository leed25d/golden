select concat('alter table '
, table_schema
, '.'
, table_name
, ' change column '
, column_name
, ' function_code '
, column_type
, ';') stmt
from columns
where table_schema='maa_load'
and upper(column_name) like 'FUNC%'
or upper(column_name) = 'FU';
