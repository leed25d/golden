select * from common.users where lower(fname) like '%aura%' and lower(lname) like '%n%m%' order by lname asc;

select role_id, name, description, restrict from common.lk_entity_role order by name;

select survey_id, lname, fname, mi, job_class,  notes, s_type, state, valid from maa.survey S where (rtrim(ltrim(notes)) != '' or notes is not null);

select distinct state from maa.survey;

insert into maa.survey_notes (id, survey_id, created_by, created_on, note_text, note_type) values (maa.survey_notes_id_seq.nextval, 926632, 25399666, sysdate, 'Note text from survey_notes', 'coordinator_note');

select * from maa.survey_notes where created_by=25399666 order by id;

select crd_notes from maa.survey where survey_id=926632;

update maa.survey set crd_notes='love note from your coordinator' where survey_id=926632;

--delete from maa.survey_notes where id= 57665;

--  these two are needed to update the schema
alter table maa.survey add (cdr_notes varchar2(4000));
update  maa.survey set cdr_notes= NULL;

--  these two are no longer used but I will save them for posterity
--alter table maa.survey_notes add (note_type varchar2(25));
--update  maa.survey_notes set note_type= NULL;
