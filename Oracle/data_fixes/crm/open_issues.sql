set feedback off
select log_id from crm.log where created_on>(sysdate - 1.5);
exit;

