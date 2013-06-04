--  select the small count grades with last modified year
select gc.grade as gcGrade, gc.ctr as gcCtr, my.yr as mxYear from (select b.* from (select grade, count(grade) as ctr from (select nvl(cast(grade as varchar(255)), 'NULL') as grade from common.entity_student) a group by a.grade) b  where b.ctr < 1000) gc, (select grade, max(cast(to_char(modified_on, 'YYYY') as number)) as yr from common.entity_student where modified_on is not null and grade is not null group by grade) my where my.grade=gc.grade order by gcGrade asc;

--  select large count grades
select * from (select nvl(cast(grade as varchar(255)), 'NULL') as grade, count(*) as ctr from common.entity_student group by grade order by ctr desc)  a where a.ctr > 1000 order by grade;


--  select small count grades
select grade, sum(ctr) from (select trim(nvl(cast(grade as varchar(255)), 'NULL')) as grade, count(*) as ctr from common.entity_student group by grade order by ctr desc)  a where a.ctr <= 1000  group by grade order by grade asc;


(select grade, max(cast(to_char(modified_on, 'YYYY') as number)) as yr from common.entity_student where modified_on is not null and grade is not null group by grade) my


select * from (select grade, max(yr) from (select distinct grade, yr from (select nvl(cast(grade as varchar(255)), 'NULL') as grade, to_char(modified_on, 'YYYY') as yr from common.entity_student where modified_on is not null) order by grade desc, yr asc) group by grade,yr) order by grade asc;


select b.* from (select grade, count(grade) as ctr from (select nvl(cast(grade as varchar(255)), 'NULL') as grade, to_char(modified_on, 'YYYY') as yr from common.entity_student) a group by a.grade,a.yr) b where b.ctr < 1000 ;

select nvl(cast(grade as varchar(255)), 'NULL') as grade, count(*) as ctr from common.entity_student group by grade order by ctr desc;

select count(*) as ctr from common.entity_student where grade is null;

select grade, count(grade) from common.students group by grade;

select * from (select nvl(cast(grade as varchar(255)), 'NULL') as grade, count(*) as ctr from common.entity_student group by grade order by ctr desc)  a where a.ctr > 1000 order by grade;

select * from (select nvl(cast(grade as varchar(255)), 'NULL') as grade, count(*) as ctr, to_char(modified_on, 'YYYY') as yr from common.entity_student group by grade,yr order by ctr desc)  a where a.ctr <= 1000;

