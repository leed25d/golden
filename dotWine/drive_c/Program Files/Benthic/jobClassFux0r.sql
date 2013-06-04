select id, title, start_date, end_date from common.job_class where id in (1013,1014,1016,1017,1019,1020,1021,1023,1024,1026,1027,1028,1029,1031,1032,1033,1035,1046,1048,1036,1039,1050,1051);

select id, title, start_date, end_date from common.job_class order by id;


update common.job_class set end_date = start_date + 1 where id in (1013,1014,1016,1017,1019,1020,1021,1023,1024,1026,1027,1028,1029,1031,1032,1033,1035,1046,1048,1036,1039,1050,1051);
