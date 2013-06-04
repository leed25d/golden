OPTIONS (SKIP=1)
LOAD DATA
TRUNCATE INTO TABLE p_import.school_imp
FIELDS TERMINATED BY ','
OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
( lea
, location
, name
, f1 filler
, f2 filler
, oracle_id
)
