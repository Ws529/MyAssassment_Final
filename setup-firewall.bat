@echo off
:: ============================================
:: Setup Firewall untuk MyAssessment
:: HARUS dijalankan sebagai Administrator!
:: ============================================

echo.
echo ============================================
echo   Setup Firewall - MyAssessment Port 8000
echo ============================================
echo.

:: Check admin
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo [ERROR] Jalankan sebagai Administrator!
    echo.
    echo Klik kanan file ini - Run as administrator
    echo.
    pause
    exit /b 1
)

echo [1/4] Menghapus rule lama...
netsh advfirewall firewall delete rule name="MyAssessment Port 8000" >nul 2>&1
netsh advfirewall firewall delete rule name="MyAssessment Port 8000 Out" >nul 2>&1
netsh advfirewall firewall delete rule name="MyAssessment HTTP" >nul 2>&1
netsh advfirewall firewall delete rule name="IIS Port 8000" >nul 2>&1

echo [2/4] Menambahkan rule Inbound TCP...
netsh advfirewall firewall add rule name="MyAssessment Port 8000" dir=in action=allow protocol=tcp localport=8000 profile=any

echo [3/4] Menambahkan rule Inbound untuk semua program di port 8000...
netsh advfirewall firewall add rule name="IIS Port 8000" dir=in action=allow protocol=tcp localport=8000 program=any profile=any

echo [4/4] Verifikasi rule...
netsh advfirewall firewall show rule name="MyAssessment Port 8000"

echo.
echo ============================================
echo   Firewall berhasil dikonfigurasi!
echo ============================================
echo.
echo   Port 8000 sekarang terbuka untuk semua IP.
echo.
echo   Akses dari HP (pastikan WiFi sama):
echo   - http://10.160.120.209:8000
echo.
echo   Akses lokal:
echo   - http://localhost:8000
echo.
pause
