LOAD DATA
TRUNCATE INTO TABLE p_import.pdox_providers
FIELDS TERMINATED BY ','
OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
( lea
, pdox_prov_code
, name_last_first
, f1 filler
, user_type
, budget
, oracle_id
)
