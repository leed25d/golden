select jcml_id, jcm_id, jclass, spmp from maa.ph_jcm_class_link where jcml_id = 2000776;

--  this sql gets all of the information related to the job class participant matrix.  When a
--  claim plan is duplicated, this table should b e duplicated to but with the new jcm id and
--  new jcml ids.  This SQL comes from LsL::D_PHJCM::jcm_class_link_select()
select jcml_id, jcm_id, jclass, spmp, positions from maa.ph_jcm_class_link where jcm_id =  2000776 order by jclass;


select c.jcml_id, c.spmp, u.userid, u.fname, u.lname from maa.ph_jcm_pcode_link c, common.users u where jcml_id in (select jcml_id from maa.ph_jcm_class_link where jcm_id = 2000776) and c.userid = u.userid order by u.lname, u.fname;

--  given a new jcm_id and aN old jcm_id, this is a table correspondence of old jcml_id's and new ones.  
select o.jclass, o.jcml_id as o_jcml_id, o.jcm_id as o_jcm_id, n.jcml_id as n_jcml_id, n.jcm_id as n_jcm_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000876 order by n_jcm_id;

select t.*, l.userid, u.fname, u.lname from (select o.jclass, o.jcml_id as o_jcml_id, o.jcm_id as o_jcm_id, n.jcml_id as n_jcml_id, n.jcm_id as n_jcm_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000876) t, maa.ph_jcm_pcode_link l, common.users u where t.o_jcml_id = l.jcml_id and u.userid= l.userid order by t.jclass;


select t.*, l.* from (select o.jclass, o.jcml_id as o_jcml_id, o.jcm_id as o_jcm_id, n.jcml_id as n_jcml_id, n.jcm_id as n_jcm_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000876) t, maa.ph_jcm_pcode_link l where t.o_jcml_id = l.jcml_id order by t.jclass;


select n_jcml_id as jcml_id, userid, spmp from (select t.*, l.* from (select o.jclass, o.jcml_id as o_jcml_id, o.jcm_id as o_jcm_id, n.jcml_id as n_jcml_id, n.jcm_id as n_jcm_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000876) t, maa.ph_jcm_pcode_link l where t.o_jcml_id = l.jcml_id);

insert into maa.ph_jcm_pcode_link (select n_jcml_id as jcml_id, userid, spmp from (select t.*, l.* from (select o.jclass, o.jcml_id as o_jcml_id, o.jcm_id as o_jcm_id, n.jcml_id as n_jcml_id, n.jcm_id as n_jcm_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000876) t, maa.ph_jcm_pcode_link l where t.o_jcml_id = l.jcml_id));

select n_jcml_id as jcml_id, userid, spmp from (select t.*, l.* from (select o.jclass, o.jcml_id as o_jcml_id, o.jcm_id as o_jcm_id, n.jcml_id as n_jcml_id, n.jcm_id as n_jcm_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000882) t, maa.ph_jcm_pcode_link l where t.o_jcml_id = l.jcml_id);


------------------------------------------------------------------------
------------------------------------------------------------------------
--  this is the original (now in DIO)
select n_jcml_id as jcml_id, userid, spmp from (select t.*, l.* from (select o.jcml_id as o_jcml_id, o.jcm_id as o_jcm_id, n.jcml_id as n_jcml_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000882) t, maa.ph_jcm_pcode_link l where t.o_jcml_id = l.jcml_id) order by jcml_id, userid;

--  this one is slightly simplified.  it returns the same results
select t.n_jcml_id as jcml_id, l.userid, l.spmp from (select o.jcml_id as o_jcml_id, n.jcml_id as n_jcml_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000882) t, maa.ph_jcm_pcode_link l where t.o_jcml_id = l.jcml_id order by jcml_id, userid;

------------------------------------------------------------------------
------------------------------------------------------------------------



select o.jcml_id as o_jcml_id, n.jcml_id as n_jcml_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000882;

select o.jcml_id as o_jcml_id, n.jcml_id as n_jcml_id from maa.ph_jcm_class_link o, maa.ph_jcm_class_link n where o.jclass = n.jclass and o.jcm_id=2000776 and n.jcm_id=2000882;


select t.n_jcml_id as jcml_id, l.userid, l.spmp from (select o.jcml_id as o_jcml_id, n.jcml_id as n_jcml_id from maa.ph_jcm_class_link o left join maa.ph_jcm_class_link n on o.jclass = n.jclass where o.jcm_id=2000776 and n.jcm_id=2000882) t, maa.ph_jcm_pcode_link l where t.o_jcml_id = l.jcml_id order by jcml_id, userid;


select n.jcml_id, l.userid, l.spmp from maa.ph_jcm_class_link o, maa.ph_jcm_class_link n,  maa.ph_jcm_pcode_link l  where o.jclass = n.jclass and o.jcm_id=2000776 and n.jcm_id=2000882 and o.jcml_id = l.jcml_id;
