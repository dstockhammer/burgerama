@echo off

echo.
REM WEB
set pthvar="%~dp0\..\Web\"
set slnvar="Burgerama.Web.sln"

if [%1]==[] ( call Restore.Build.bat %pthvar% %slnvar%
) else ( call Restore.Build.bat %pthvar% %slnvar% silent)
if [%2]==[] pause
        