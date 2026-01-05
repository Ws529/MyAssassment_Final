@echo off
title Stop MyAssessment
color 0C

echo Menghentikan aplikasi MyAssessment...
taskkill /F /IM dotnet.exe 2>nul

echo.
echo Aplikasi berhasil dihentikan!
pause
