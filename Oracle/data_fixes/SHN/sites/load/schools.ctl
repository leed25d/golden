LOAD DATA
TRUNCATE INTO TABLE p_import.school_imp
FIELDS TERMINATED BY ','
OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
( lea
, old_code
, new_code
, school_name
, disable_flag
, oracle_id
)
