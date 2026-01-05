@echo off
:: ============================================
:: Tampilkan URL Akses MyAssessment
:: Otomatis detect IP aktif
:: ============================================

echo.
echo ============================================
echo   MyAssessment - URL Akses
echo ============================================
echo.

echo   [Akses Lokal]
echo   http://localhost:8000
echo.

echo   [Akses dari HP - gunakan salah satu IP di bawah]
echo.

:: Get WiFi IP
for /f "tokens=2 delims=:" %%a in ('ipconfig ^| findstr /c:"IPv4" ^| findstr /v "169.254"') do (
    set IP=%%a
    call :trim
    echo   http://!IP!:8000
)

setlocal enabledelayedexpansion
for /f "tokens=2 delims=:" %%a in ('ipconfig ^| findstr /c:"IPv4" ^| findstr /v "169.254"') do (
    set "IP=%%a"
    set "IP=!IP: =!"
    echo   http://!IP!:8000
)
endlocal

echo.
echo ============================================
echo   Pastikan HP dan laptop di WiFi yang sama!
echo ============================================
echo.
pause
goto :eof

:trim
set IP=%IP: =%
goto :eof
