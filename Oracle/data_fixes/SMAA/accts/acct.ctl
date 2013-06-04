LOAD DATA
TRUNCATE INTO TABLE p_import.accounts_new
FIELDS TERMINATED BY ','
OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
( ccode
, fname
, lname
, billing_provider_code
, type
, job_class
, claiming_unit
, email
, phone
, password
)
