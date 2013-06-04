    select+

      S.fname || ' ' || S.lname student,
      T.service_date as service_date,
      common.util_pkg.from_unixtime( T.time ) date_entered,
      T.minutes,
      CASE T.minutes_type
            WHEN '1'   THEN 'Regular: Small Group'
            WHEN '2'   THEN 'Regular: Individual'
            WHEN '3'   THEN 'Regular: Classroom'
            WHEN '4'   THEN 'Regular: Consultation'
            WHEN '5'   THEN 'Make-Up: Small Group'
            WHEN '6'   THEN 'Make-Up: Individual'
            WHEN '7'   THEN 'Make-Up: Classroom'
            WHEN '8'   THEN 'Make-Up: Consultation'
            WHEN '9'   THEN 'Compensatory: Small Group'
            WHEN '10'  THEN 'Compensatory: Individual'
            WHEN '11'  THEN 'Compensatory: Classroom'
            WHEN '12'  THEN 'Compensatory: Consultation'
            ELSE  'All Types'

      END as munites_type,
      T.outcome,
      T.comments,
      T.plan,
      SCH.name,
      P.fname || ' ' || P.lname provider

    from
      para.treatments T,
      common.users P,
      common.students S,
      common.schools SCH
    where
      T.empid = P.userid and
      T.sid = S.student_id and
      T.school_id = SCH.school_id(+) and
      P.type = 'S' and P.ccode = 'ZZ' and T.service_date >= to_date('2012-07-01', 'YYYY-MM-DD') and T.service_date <=  to_date('2013-06-30', 'YYYY-MM-DD')and P.type = 'S'
    order by
      T.service_date desc,
      T.time desc


--    select
--      T.billed,
--      T.service_date,
--      T.school_id,
--      T.outcome,
--      T.minutes,
--      T.minutes_type,
--      T.sid,
--      T.comments,
--      T.accuracy,
--      T.diagnostic_code,
--      T.empid,
--      T.dismissed,
--      T.dismissed_other,
--      T.treatment_id,
--      P.fname || ' ' || P.lname provider,
--      S.fname || ' ' || S.lname student,
--      SCH.name,
--      T.plan,
--      common.util_pkg.from_unixtime( T.time ) date_entered,
--      T.authorization_id
--    from
--      para.treatments T,
--      common.users P,
--      common.students S,
--      common.schools SCH
--    where
--      T.empid = P.userid and
--      T.sid = S.student_id and
--      T.school_id = SCH.school_id(+) and
--      P.type = 'S' and P.ccode = 'ZZ' and T.service_date >= to_date('2012-07-01', 'YYYY-MM-DD') and T.service_date <=  to_date('2013-06-30', 'YYYY-MM-DD')and P.type = 'S'
--    order by
--      T.service_date desc,
--      T.time desc
