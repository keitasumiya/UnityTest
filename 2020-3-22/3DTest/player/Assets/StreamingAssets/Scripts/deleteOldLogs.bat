@REM @echo off

cd /d %~dp0
forfiles /p ..\..\..\logs /d -30 /m "*.txt" /c "cmd /c del @file"
exit /b
