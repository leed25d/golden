select username,password,entity_id from common.entity_user where password is not null;

select username,password,entity_id from common.entity_user where password is not null;

select u.username,u.password,u.entity_id, (select value from  common.entity_comm m where m.entity_id = u.entity_id and m.comm_type = 'Email') as email from common.entity_user u where u.password is not null;

select username,password,entity_id from common.entity_user where password='qlqvQVgvLCSiRe/CDHqtVA';

select count(m.entity_id) as ecount, m.entity_id from common.entity_comm m where m.comm_type='Email' group by m.entity_id order by ecount desc;

select count(m.entity_id) as ecount, m.entity_id from common.entity_comm m group by m.entity_id order by ecount asc;
