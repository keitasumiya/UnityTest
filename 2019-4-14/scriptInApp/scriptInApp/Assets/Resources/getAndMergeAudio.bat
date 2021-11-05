@REM getAndMergeAudio.bat "data/FL.wav" "data/FR.wav" "data/BL.wav" "data/BR.wav" "output.wav"

@echo off
setlocal
cd %~dp0

set FL=%1
set filename=%1
set FR=%2
set FC=%filename%_sirent-monoral-FC.wav
set LFE=%filename%_sirent-monoral-LFE.wav
set BL=%3
set BR=%4
set out=%5
set CMD="ffprobe -v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 %filename%"
set duration=
for /f "usebackq delims=" %%a in (`%CMD%`) do set duration=%%a
echo %duration%

ffmpeg -y -f lavfi -i "aevalsrc=0:c=mono:s=48000:d=%duration%" %FC%
ffmpeg -y -f lavfi -i "aevalsrc=0:c=mono:s=48000:d=%duration%" %LFE%

ffmpeg -y -i %FL% -i %FR% -i %FC% -i %LFE% -i %BL% -i %BR% -filter_complex "[0:a][1:a][2:a][3:a][4:a][5:a]join=inputs=6:channel_layout=5.1:map=0.0-FL|1.0-FR|2.0-FC|3.0-LFE|4.0-BL|5.0-BR[a]" -map "[a]" %out%

endlocal
exit /b 0