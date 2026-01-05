@echo off
title MyAssessment K13 - PT. Hyoka Wanssss
color 0A

echo ========================================
echo    MyAssessment - Sistem Penilaian K13
echo    PT. Hyoka Wanssss
echo ========================================
echo.

echo [1] Memastikan MongoDB berjalan...
net start MongoDB 2>nul
if %errorlevel%==0 (
    echo     MongoDB started successfully!
) else (
    echo     MongoDB sudah berjalan atau perlu start manual
)
echo.

echo [2] Menjalankan aplikasi...
echo     Akses: http://10.160.120.209:8000
echo     Tekan Ctrl+C untuk stop
echo.
echo ========================================

cd /d "%~dp0"
dotnet run --project .\MyAssessment\MyAssessment.csproj --urls "http://0.0.0.0:8000"

pause
