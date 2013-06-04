@echo off
set ORACLE_HOME=%1
set PATH=%ORACLE_HOME%\bin;%PATH%

if "(%2)"=="(-register)" goto register
if "(%2)"=="(-deregister)" goto deregister
goto :EOF

:register
regsvr32 /s OraOLEDB10.dll
goto :EOF

:deregister
regsvr32 /s /u OraOLEDB10.dll
goto :EOF
