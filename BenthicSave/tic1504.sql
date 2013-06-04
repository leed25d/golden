select name, type, key, value, pos from para.rules order by pos asc;

select distinct type from para.rules;

select name, type, key, value, pos from para.rules where name='nursing_care_given' order by pos asc;

select * from nursing.office_visits where sid=13732001;
