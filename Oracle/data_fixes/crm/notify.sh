ACTIVITY=`sqlplus -S paradba@timmy @open_issues < /local/oracle11g/cron/oracle.pwd`
if [ "${ACTIVITY}x" = 'x' ]; then echo 1; else echo 0; fi
