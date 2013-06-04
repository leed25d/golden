declare
	v_commit_count NUMBER;
begin
	v_commit_count := 0;

	insert into temp_students (select student_id from common.students where lea='&lea_code');
	commit;

	for r_stu in (select * from temp_students) loop
	begin
		common.student_pkg.student_delete(r_stu.stu_id);

		/*dbms_output.put_line(r_dupe.last ||' '|| r_dupe.first || ' ' || r_dupe.new_student_id);*/
		v_commit_count := v_commit_count + 1;
		if v_commit_count>1000 then
			commit;
			dbms_output.put_line('Commit point');
			/* EXIT WHEN v_commit_count>100; */
			v_commit_count := 0;
		end if; /* v_commit_count */	
	end;
	end loop;
	commit;
	dbms_output.put_line('Final commit');
end;
/
