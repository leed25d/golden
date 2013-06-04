@echo off
Rem OTT script for Oracle XE .
Rem Before running this script
Rem set jdk1.4.2/bin to PATH
Rem If ORACLE_HOME is not set, set the
Rem CLASSPATH to the directories containing
Rem ojdbc14.jar,orai18n.jar and ottclasses.zip 
Rem

if defined ORACLE_HOME set CLASSPATH=%ORACLE_HOME%\jdbc\lib\ojdbc14.jar;%ORACLE_HOME%\jlib\orai18n.jar;%ORACLE_HOME%\precomp\lib\ottclasses.zip;%CLASSPATH%

set NLSLANG=
if defined NLS_LANG set NLSLANG=NLS_LANG

if defined ORACLE_HOME (
java oracle.ott.c.CMain nlslang=%NLSLANG% orahome=%ORACLE_HOME%  %*
) else (
java oracle.ott.c.CMain nlslang=%NLSLANG% %*
)
