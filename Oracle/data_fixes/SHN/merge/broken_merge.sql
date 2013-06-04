declare
p_new_id NUMBER;
p_student_id NUMBER;
BEGIN

  p_new_id := 22878680;
  p_student_id := 22888448;

  /* move keys */
  UPDATE para.ap
     SET sid = p_new_id
   WHERE sid = p_student_id;

  UPDATE para.assessments
     SET sid = p_new_id
   WHERE sid = p_student_id;

  UPDATE para.notes
     SET sid = p_new_id
   WHERE sid = p_student_id;

  /* remove possible duplicates first */
  DELETE FROM common.entity_group_link t1
   WHERE EXISTS (SELECT 'x'
                   FROM common.entity_group_link t2
                  WHERE t2.entity_id = p_new_id
                    AND t1.entity_id = p_student_id
                    AND t2.group_id  = t1.group_id);
  /* now do move */
  UPDATE common.entity_group_link
     SET entity_id = p_new_id
   WHERE entity_id = p_student_id;

  /* remove possible duplicates first */
  DELETE FROM common.entity_student_link t1
   WHERE EXISTS (SELECT 'x'
                   FROM common.entity_student_link t2
                  WHERE t2.student_id = p_new_id
                    AND t1.student_id = p_student_id
                    AND t2.entity_id  = t1.entity_id
                    AND t2.rowid      > t1.rowid);

  /* now do move */
  UPDATE common.entity_student_link
     SET student_id = p_new_id
   WHERE student_id = p_student_id;

  UPDATE para.screenings
     SET sid = p_new_id
   WHERE sid = p_student_id;

  UPDATE para.treatments
     SET sid = p_new_id
   WHERE sid = p_student_id;

  UPDATE para.treatment_areas
     SET sid = p_new_id
   WHERE sid = p_student_id;

  UPDATE nursing.office_visits
     SET sid = p_new_id
   WHERE sid = p_student_id;

  UPDATE nursing.referrals
     SET sid = p_new_id
   WHERE sid = p_student_id;

  UPDATE nursing.sp_students
     SET sid = p_new_id
   WHERE sid = p_student_id;

  UPDATE nursing.sp_visits
     SET sid = p_new_id
   WHERE sid = p_student_id;

END student_move;
/

declare
p_student_id NUMBER;
begin
	p_student_id := 22888448;
	common.student_pkg.student_delete(p_student_id);
end;
/

