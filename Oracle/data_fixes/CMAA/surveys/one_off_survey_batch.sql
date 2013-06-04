set serveroutput on
begin
    maa.survey_generator.generate( sysdate-2 );
end;
/
