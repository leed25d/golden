select userid, username, ccode, lname, fname from common.users where userid in
	(select participant_id from rms.sample_moment where id = &sample_moment_id)
/
