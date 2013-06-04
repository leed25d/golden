col fname format a15
col lname format a25
set lines 120
set pages 60

select userid, fname, lname from common.users
where lname like upper('&last%') and fname like upper('&first%') and ccode=upper('&lea')
/
