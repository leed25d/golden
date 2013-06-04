@echo off
set ORACLE_HOME=%1

if "(%2)"=="(-sdkhome)" goto sdk
if "(%2)"=="(-nosdk)" goto nosdk
if "(%2)"=="(-removesdk)" goto removesdk
if "(%2)"=="(-removenosdk)" goto removenosdk
goto :EOF

:sdk
set GAC_HOME=%3
cd %GAC_HOME%
gacutil.exe /u Policy.9.2.Oracle.DataAccess
gacutil.exe /u Policy.10.1.Oracle.DataAccess
gacutil.exe /i %ORACLE_HOME%\bin\Oracle.DataAccess.dll
gacutil.exe /i %ORACLE_HOME%\ODP.NET\PublisherPolicy\Policy.9.2.Oracle.DataAccess.dll
gacutil.exe /i %ORACLE_HOME%\ODP.NET\PublisherPolicy\Policy.10.1.Oracle.DataAccess.dll
for %%i IN (de,es,fr,it,pt-BR,ja,ko,zh-CHS,zh-CHT) do gacutil.exe /i %ORACLE_HOME%\ODP.NET\Resources\%%i\Oracle.DataAccess.resources.dll
goto :EOF

:nosdk
cd %ORACLE_HOME%\bin
ODPReg.exe Oracle.DataAccess.dll
ODPReg.exe %ORACLE_HOME%\ODP.NET\PublisherPolicy\Policy.9.2.Oracle.DataAccess.dll
ODPReg.exe %ORACLE_HOME%\ODP.NET\PublisherPolicy\Policy.10.2.Oracle.DataAccess.dll
for %%i IN (de,es,fr,it,pt-BR,ja,ko,zh-CHS,zh-CHT) do ODPReg.exe %ORACLE_HOME%\ODP.NET\Resources\%%i\Oracle.DataAccess.resources.dll
goto :EOF

:removesdk
set GAC_HOME=%3
cd %GAC_HOME%
gacutil.exe /u Policy.9.2.Oracle.DataAccess
gacutil.exe /u Policy.10.1.Oracle.DataAccess
gacutil.exe /u Oracle.DataAccess
gacutil.exe /u Oracle.DataAccess.resources
goto :EOF

:removenosdk
cd %ORACLE_HOME%\bin
ODPReg.exe Policy.9.2.Oracle.DataAccess /u
ODPReg.exe Policy.10.1.Oracle.DataAccess /u
ODPReg.exe Oracle.DataAccess /u
ODPReg.exe Oracle.DataAccess.resources /u
goto :EOF
