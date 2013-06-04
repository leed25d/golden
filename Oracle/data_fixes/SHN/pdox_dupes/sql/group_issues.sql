select d.new_lea_sid, d.new_student_id, g1.group_id, d.old_student_id, g2.group_id
from p_import.pdox_stu_dupes d
, common.entity_group_link g1
, common.entity_group_link g2
where d.new_student_id = g1.entity_id
and d.old_student_id = g2.entity_id
and g1.group_id = g2.group_id;
