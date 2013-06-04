/* Update to do a mass merge of students identified via sql */
declare
	v_commit_count NUMBER;
	v_min_id NUMBER;
	v_max_id NUMBER;
begin
	v_commit_count := 0;

	for r_dupe in (select e.ccode, e.student_number, e.lname,e.fname,e.gender, count(*)
	        from common.entity_student e, p_import.pdox_stu_dupes d
	        where e.student_number = d.new_lea_sid
	        and e.ccode = d.lea
	        and e.lname = d.last
	        and e.fname = d.first
	        and e.gender = d.gender
		group by e.ccode, e.student_number, e.lname,e.fname,e.gender
		having count(*) > 1) loop
	begin
		select min(entity_id) into v_min_id from common.entity_student
		where student_number = r_dupe.student_number
		and ccode = r_dupe.ccode and lname = r_dupe.lname
		and fname = r_dupe.fname and gender = r_dupe.gender;

                select max(entity_id) into v_max_id from common.entity_student
                where student_number = r_dupe.student_number
                and ccode = r_dupe.ccode and lname = r_dupe.lname
                and fname = r_dupe.fname and gender = r_dupe.gender;

		common.student_pkg.student_move_and_delete(v_min_id, v_max_id);

		dbms_output.put_line(r_dupe.lname ||' '|| r_dupe.fname || ' ' || r_dupe.student_number);
		v_commit_count := v_commit_count + 1;
		if v_commit_count>1000 then
			/*commit;*/
			dbms_output.put_line('Commit point');
			v_commit_count := 0;
		end if; /* v_commit_count */	
	end;
	end loop;
	/*commit;*/
	dbms_output.put_line('Final commit');
end;
/
