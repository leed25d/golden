select * from common.entity_group where entity_id in
(146462,20297016)
/
select * from common.group_students where group_id in (
    select group_id from common.entity_group where entity_id in
(146462,20297016)
    )
/
