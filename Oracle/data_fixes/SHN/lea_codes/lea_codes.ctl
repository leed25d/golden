LOAD DATA
TRUNCATE INTO TABLE p_import.leas_new
FIELDS TERMINATED BY ','
OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
( lea_code
)