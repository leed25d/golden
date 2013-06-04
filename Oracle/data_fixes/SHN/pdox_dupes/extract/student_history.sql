col lea format a3
col location format a12
col name format a25 trunc

insert into temp_students
select x.sid from nursing.office_visits x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';

insert into temp_students
select x.sid from nursing.sp_students x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';

insert into temp_students
select x.sid from nursing.sp_visits x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';


insert into temp_students
select x.sid from para.assessments x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';


insert into temp_students
select x.sid from para.assessments_iep x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';

insert into temp_students
select x.sid from para.screenings x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';

insert into temp_students
select x.sid from para.treatments x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';

insert into temp_students
select x.sid from para.ap x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';

insert into temp_students
select x.sid from nursing.referrals x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';

insert into temp_students
select x.sid from para.notes x, common.entity_student s
where s.entity_id = x.sid and s.ccode='&&lea_code';
commit;

select s.student_number
from common.entity_student s
where s.entity_id in (select distinct stu_id from temp_students);
