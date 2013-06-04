LOAD DATA
TRUNCATE INTO TABLE p_import.pdox_providers
FIELDS TERMINATED BY ','
OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
( lea
, pdox_prov_code
, name_last_first
, user_type
, oracle_id
)
