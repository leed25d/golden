select distinct m.entity_id
 from common.entity_user m, common.entity_role r, common.lk_entity_role er
 where m.entity_id = r.entity_id
 and r.role_id = er.role_id
 and er.role_id like 'shn_%'
 and m.user_type = 'MAA'
/
