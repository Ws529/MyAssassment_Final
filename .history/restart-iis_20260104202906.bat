@echo off
:: ============================================
:: Restart IIS dan App Pool MyAssessment
:: Jalankan sebagai Administrator
:: ============================================

echo.
echo ============================================
echo   Restart IIS - MyAssessment
echo ============================================
echo.

:: Check admin
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo [ERROR] Jalankan sebagai Administrator!
    echo Klik kanan - Run as administrator
    pause
    exit /b 1
)

echo [1/4] Stopping IIS...
iisreset /stop

echo [2/4] Starting IIS...
iisreset /start

echo [3/4] Starting App Pool MyAssessment...
%windir%\system32\inetsrv\appcmd start apppool /apppool.name:"MyAssessment"

echo [4/4] Starting Website MyAssessment...
%windir%\system32\inetsrv\appcmd start site /site.name:"MyAssessment"

echo.
echo ============================================
echo   IIS Restarted!
echo ============================================
echo.
echo   Coba akses:
echo   - http://localhost:8000
echo   - http://10.160.120.209:8000
echo.

:: Test koneksi
echo Testing connection...
powershell -Command "try { $r = Invoke-WebRequest -Uri 'http://localhost:8000' -UseBasicParsing -TimeoutSec 10; Write-Host '[OK] Website merespons!' -ForegroundColor Green } catch { Write-Host '[ERROR] Website tidak merespons: ' $_.Exception.Message -ForegroundColor Red }"

echo.
pause
