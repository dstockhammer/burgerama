@echo off

echo.
REM WEB
set pthvar="%~dp0\..\Services\OutingScheduler\"
set slnvar="Burgerama.Services.OutingScheduler.sln"

if [%1]==[] ( call Restore.Build.bat %pthvar% %slnvar%
) else ( call Restore.Build.bat %pthvar% %slnvar% silent)
if [%2]==[] pause
        