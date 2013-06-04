update maa.survey set empid=25399666 where survey_id=846028;

update maa.survey set esig_on='y' where survey_id=846028;

select * from  maa.survey where survey_id=846028;

INSERT INTO maa.survey (survey_id, lname, fname) VALUES (maa.survey_survey_id_seq.nextval, 'lee', 'duulan');