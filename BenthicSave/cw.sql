--declare
--p_new_id NUMBER;
--p_student_id NUMBER;
--BEGIN
--
--  p_new_id := 29003394;
--  p_student_id := 26645082;
--
  /* move keys */
  UPDATE para.ap
     SET sid = 29003394
   WHERE sid = 26645082;

  UPDATE para.assessments
     SET sid = 29003394
   WHERE sid = 26645082;

  UPDATE para.notes
     SET sid = 29003394
   WHERE sid = 26645082;

  /* remove possible duplicates first */
  DELETE FROM common.entity_group_link t1
   WHERE EXISTS (SELECT 'x'
                   FROM common.entity_group_link t2
                  WHERE t2.entity_id = 29003394
                    AND t1.entity_id = 26645082
                    AND t2.group_id  = t1.group_id);
  /* now do move */
  UPDATE common.entity_group_link
     SET entity_id = 29003394
   WHERE entity_id = 26645082;

  /* remove possible duplicates first */
  DELETE FROM common.entity_student_link t1
   WHERE EXISTS (SELECT 'x'
                   FROM common.entity_student_link t2
                  WHERE t2.student_id = 29003394
                    AND t1.student_id = 26645082
                    AND t2.entity_id  = t1.entity_id
                    --AND t2.rowid      > t1.rowid
                    );

  /* now do move */
  UPDATE common.entity_student_link
     SET student_id = 29003394
   WHERE student_id = 26645082;

  UPDATE para.screenings
     SET sid = 29003394
   WHERE sid = 26645082;

  UPDATE para.treatments
     SET sid = 29003394
   WHERE sid = 26645082;

  UPDATE para.treatment_areas
     SET sid = 29003394
   WHERE sid = 26645082;

  UPDATE nursing.office_visits
     SET sid = 29003394
   WHERE sid = 26645082;

  UPDATE nursing.referrals
     SET sid = 29003394
   WHERE sid = 26645082;

  UPDATE nursing.sp_students
     SET sid = 29003394
   WHERE sid = 26645082;

  UPDATE nursing.sp_visits
     SET sid = 29003394
   WHERE sid = 26645082;

--declare
--26645082 NUMBER;
--begin
--	26645082 := 22888448;
--	common.student_pkg.student_delete(26645082);
--end;
--/


begin
	common.student_pkg.student_delete(26645082);
end;
/
