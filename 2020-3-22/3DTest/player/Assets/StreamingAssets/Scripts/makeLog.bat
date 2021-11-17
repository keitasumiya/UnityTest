@REM This bat is launched when a player application is destroyed.
@echo off
set time2=%time: =0%
set ulogfile=log_%date:~0,4%-%date:~5,2%-%date:~8,2%-%time2:~0,2%%time2:~3,2%.txt
@REM echo %ulogfile%
echo waiting for making a log file...
timeout 2 >nul
echo copying a log file
copy "C:\Users\%USERNAME%\AppData\LocalLow\TDome\player\Player.log" %~dp0..\..\..\logs\%ulogfile%
exit /b