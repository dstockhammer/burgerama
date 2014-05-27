@echo off

echo.
REM MESSAGING
set pthvar="%~dp0\..\Messaging\"
set slnvar="Burgerama.Messaging.sln"

if [%1]==[] ( call Restore.Build.bat %pthvar% %slnvar%
) else ( call Restore.Build.bat %pthvar% %slnvar% silent)
if [%2]==[] pause
        