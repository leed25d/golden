variable p_id NUMBER;
variable v_school_id NUMBER;
begin
select school_id into :v_school_id
from common.entity_school
where lea='GZ'
and location='490';

common.student_pkg.student_insert(p_lea => 'GZ'
, p_lname => 'GARCIA'
, p_fname => 'JASMINE'
, p_mname => ''
, p_nickname => ''
, p_gender => 'F'
, p_birthdate => to_date('30-OCT-04', 'DD-MON-YY')
, p_student_number => '251623'
, p_school_id => :v_school_id 
, p_room => ''
, p_grade => '00'
, p_track => ''
, p_eld_level => ''
, p_plan_indicator => 'X'
, p_created_by => 1
, p_created_on => sysdate
, p_student_id => :p_id);
end;
/
