select DISTINCT quarter_id, review_STATUS from rmts.survey_detaiL;

select * from  rmts.survey_detail;

select * from  rmts.survey_detail where detail_id=1666;

select * from  rmts.coding_plan_detail where coder_id=25399666;

select * from  rmts.survey_header where respondent_id=25399666;

select * from  rmts.survey_detail where respondent_id=25399666;

update rmts.survey_detail set review_status='Ready' where detail_id=2196;

select * from  rmts.coding_pass where coding_header_id=10003;

select * from  rmts.coding_pass;

insert into rmts.coding_pass
	(claim_plan_id, , )
values (0,'XX','system_placeholder');


insert into coding_pass set

select * from  rmts.survey_detaiL where quarter_id=654 and review_status in ('Entered', 'Coded');

select * from rmts.survey_code_history;

select * from rmts.survey_code_history where detail_id in (select detail_id from  rmts.survey_detaiL where quarter_id=654 and review_status in ('Entered', 'Coded'));

select coding_pass_id, code_id from rmts.survey_code_history where detail_id in (select detail_id from  rmts.survey_detaiL where quarter_id=654 and review_status in ('Entered', 'Coded'));


create sequence common.notf_id_seq;
grant select,insert,update,delete on common.notifications to www;
grant select on common.notf_id_seq to www;
