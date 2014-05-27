@echo off

echo.
REM COMMON
set pthvar="%~dp0\..\Common\"
set slnvar="Burgerama.Common.sln"

if [%1]==[] ( call Restore.Build.bat %pthvar% %slnvar%
) else ( call Restore.Build.bat %pthvar% %slnvar% silent)
if [%2]==[] pause
        