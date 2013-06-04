declare
	v_commit_count NUMBER;
begin
	v_commit_count := 0;

	for r_dupe in (select * from p_import.pdox_stu_dupes) loop
	begin
		common.student_pkg.student_move_and_delete(r_dupe.old_student_id, r_dupe.new_student_id);

		dbms_output.put_line(r_dupe.last ||' '|| r_dupe.first || ' ' || r_dupe.new_student_id);
		v_commit_count := v_commit_count + 1;
		if v_commit_count>10 then
			commit;
			dbms_output.put_line('Commit point');
			v_commit_count := 0;
		end if; /* v_commit_count */	
	end;
	end loop;
	commit;
	dbms_output.put_line('Final commit');
end;
/
