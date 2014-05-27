@echo off
title %~2
echo.
echo --------------------
echo %~2 (%~1)
echo --------------------
echo.

nuget.exe restore "%~1%~2"

call "%VS120COMNTOOLS%vsvars32.bat"

if [%3]==[] (
	msbuild "%~1%~2" /t:rebuild /nologo /v:n /m
) else (
	msbuild "%~1%~2" /t:rebuild /nologo /v:m /m
)