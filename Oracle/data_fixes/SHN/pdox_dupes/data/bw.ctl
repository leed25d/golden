LOAD DATA
TRUNCATE INTO TABLE p_import.pdox_stu_dupes
FIELDS TERMINATED BY ','
OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
( lea
, old_lea_sid
, new_lea_sid
, last
, first
, adob
, gender
, iep
)
