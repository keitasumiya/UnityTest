@REM This bat is launched when a player application is destroyed. 
@REM Plaese put this bat file into "C:\sbsc\_log"
@echo off
set time2=%time: =0%
set ulogfile=log_%date:~0,4%-%date:~5,2%-%date:~8,2%-%time2:~0,2%%time2:~3,2%.txt
@REM echo %ulogfile%
timeout 2 >nul
copy "C:\Users\%USERNAME%\AppData\LocalLow\sbsc\k4k3\Player.log" "C:\sbsc\_log\"%ulogfile%
