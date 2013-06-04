update rms.sample_moment
 set assigned_date = (sysdate - 43)
 , response_date = (sysdate - 33)
 , modified_on = (sysdate - 33)
 , modified_by = 20654894
 where id=&sample_moment_id
/
