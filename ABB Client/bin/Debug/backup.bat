rem +-------------- setting -----------------+
rem --set ORA_HOME=C:\oracle\product\10.2.0\db_1\bin
set ORA_HOME=E:\oracle\product\10.2.0\CLIENT_1\BIN
set DMP_PATH=C:\Database\
set TNS_NAME=apb
set USERNAME=apb
set PASSWORD=gvru[u
rem +----------------------------------------+

rem --- get timestamp ---
for /F "tokens=2-4 delims=/ " %%i in ('date /t') do set date=%%k%%i%%j
for /F "tokens=1-2 delims=: " %%i in ('time /t') do set time=%%i%%j
set DMP_FILE=%DMP_PATH%\bakup_APB_ABBDBSV_%date%_%time%.dmp
set LOG_FILE=%DMP_PATH%\bakup_APB_ABBDBSV_%date%_%time%.log

rem --- export data ---
%ORA_HOME%\exp.exe %USERNAME%/%PASSWORD%@%TNS_NAME% file=%DMP_FILE% log=%LOG_FILE% owner=%USERNAME% statistics=none