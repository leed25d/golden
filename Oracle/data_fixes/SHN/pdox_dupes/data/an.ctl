LOAD DATA
TRUNCATE INTO TABLE p_import.pdox_stu_dupes
FIELDS TERMINATED BY ','
OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
( old_student_id
, lea
, new_lea_sid
, old_lea_sid
, last
, first
, f7 filler
, adob
, gender
, f1 filler
, f2 filler
, f3 filler
, f4 filler
, iep
)
