declare
	v_commit_count NUMBER;
begin
	v_commit_count := 0;

	for r_stu in (select * from p_import.pdox_stu_dupes) loop
	begin
		update common.entity_student set student_number = r_stu.new_lea_sid
		where entity_id = r_stu.old_student_id
		and ccode = r_stu.lea
		and student_number = r_stu.old_lea_sid;

		dbms_output.put_line(r_stu.last ||' '|| r_stu.first || ' ' || r_stu.new_student_id);
		v_commit_count := v_commit_count + 1;
		if v_commit_count>100 then
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
