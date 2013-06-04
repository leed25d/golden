declare
p_school_id NUMBER;
begin
/*common.school_pkg.school_insert('HR', 'AVALON ELEM', '245', '', p_school_id);*/
/*common.school_pkg.school_insert('HR', 'CITRUS HILL ELEM', '320', '', p_school_id);*/
common.school_pkg.school_insert('HR', 'COLUMBIA ELEM', '210', '', p_school_id);
p_school_id := NULL;
common.school_pkg.school_insert('HR', 'GLENVIEW PRESCHOOL', 'GLENV', '', p_school_id);
p_school_id := NULL;
common.school_pkg.school_insert('HR', 'LASSELLE ELEM', '235', '', p_school_id);
p_school_id := NULL;
common.school_pkg.school_insert('HR', 'MARCH MIDDLE', '295', '', p_school_id);
p_school_id := NULL;
common.school_pkg.school_insert('HR', 'MAY RANCH ELEM', '215', '', p_school_id);
p_school_id := NULL;
common.school_pkg.school_insert('HR', 'RED MAPLE ELEM', '200', '', p_school_id);
p_school_id := NULL;
common.school_pkg.school_insert('HR', 'STUDENT SUCCESS ACAD', '430', '', p_school_id);
p_school_id := NULL;
common.school_pkg.school_insert('HR', 'TRIPLE CROWN ELEM', '230', '', p_school_id);
end;
