select a.assessment_id, to_char(a.service_date, 'YYYY-MM-DD') as service_date
from para.assessments_iep a
where a.assessment_id in (
