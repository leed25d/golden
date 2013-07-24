define spk_list=603068,603084,602636,602638,602640,602694,603754,603760,605952,605578,605592,605446,604988,604236

select sp_visit_id
from nursing.sp_visits n
where sp_visit_id in (&spk_list)
/