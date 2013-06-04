declare
p_school_id NUMBER;
begin
common.school_pkg.school_update('ER','OBREGON APEX PROGRAM','OBREG','', 21875324);  
common.school_pkg.school_update('ER','DO NOT USE -- OBREGON COMMUNITY/TRANSIT','11','',21875326);
common.school_pkg.school_update('ER','BIRNEY ELEM','BIRNE','', 21875328);     
common.school_pkg.school_update('ER','DURFEE ELEM','DURFE','', 21875330);      
common.school_pkg.school_update('ER','MAGEE ELEM','MAGEE','', 21875332);        
common.school_pkg.school_update('ER','NORTH RANCHITO ELEM','NRANC','', 21875334);
common.school_pkg.school_update('ER','RIO VISTA ELEM','RIOVI','', 21875336);   
common.school_pkg.school_update('ER','RIVERA ELEM','RIVEL','', 21875338);       
common.school_pkg.school_update('ER','SOUTH RANCHITO ELEM','SRANC','', 21875340);
common.school_pkg.school_update('ER','VALENCIA ELEM','VALEN','', 21875342);    
common.school_pkg.school_update('ER','MAIZELAND ELC','MAIZ','', 21877124);      
common.school_pkg.school_update('ER','BURKE MIDDLE','BURKE','', 21875344);       
common.school_pkg.school_update('ER','NORTH PARK MIDDLE','NPARK','', 21875346);   
common.school_pkg.school_update('ER','RIVERA MIDDLE','RIVMD','', 21875348);        
common.school_pkg.school_update('ER','EL RANCHO HIGH','ERHS','', 21875350);         
common.school_pkg.school_update('ER','SALAZAR HIGH','SALAZ','', 21875352);           
common.school_pkg.school_update('ER','EL RANCHO ALTERNATIVE SCH','ERAS','', 21875354);
common.school_pkg.school_insert('ER', 'HOME/HOSPITAL', 'HOMEH', '', p_school_id);
end;
