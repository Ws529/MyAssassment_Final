@echo off
:: ============================================
:: Setup Firewall untuk MyAssessment
:: Jalankan sebagai Administrator
:: ============================================

echo.
echo ============================================
echo   Setup Firewall - MyAssessment
echo ============================================
echo.

:: Check admin
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo [ERROR] Jalankan sebagai Administrator!
    pause
    exit /b 1
)

:: Hapus rule lama jika ada
netsh advfirewall firewall delete rule name="MyAssessment HTTP" >nul 2>&1
netsh advfirewall firewall delete rule name="MyAssessment Port 8000" >nul 2>&1
netsh advfirewall firewall delete rule name="MyAssessment Port 8000 Out" >nul 2>&1

:: Tambah rule baru
echo [1/2] Menambahkan rule Inbound...
netsh advfirewall firewall add rule name="MyAssessment Port 8000" dir=in action=allow protocol=tcp localport=8000

echo [2/2] Menambahkan rule Outbound...
netsh advfirewall firewall add rule name="MyAssessment Port 8000 Out" dir=out action=allow protocol=tcp localport=8000

echo.
echo ============================================
echo   Firewall berhasil dikonfigurasi!
echo   Port 8000 sekarang terbuka.
echo ============================================
echo.
pause
