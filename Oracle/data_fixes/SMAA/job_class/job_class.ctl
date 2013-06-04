LOAD DATA
INFILE job_class_main.csv
INFILE job_class_mcoe.csv
INFILE job_class_sonoma.csv
TRUNCATE INTO TABLE p_import.job_class_imp
FIELDS TERMINATED BY ','
OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
(
TITLE,
CLAIMING_UNIT
)

