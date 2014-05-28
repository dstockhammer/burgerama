@echo off

echo.
REM WEB
set pthvar="%~dp0\..\Services\Ratings\"
set slnvar="Burgerama.Services.Ratings.sln"

call Copy.Configs.bat %pthvar%Api\Config\
call Copy.Configs.bat %pthvar%Endpoint\Config\
call Copy.Configs.bat %pthvar%Tests\Config\

if [%1]==[] ( call Restore.Build.bat %pthvar% %slnvar%
) else ( call Restore.Build.bat %pthvar% %slnvar% silent)
if [%2]==[] pause
        