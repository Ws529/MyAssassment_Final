@echo off
:: ============================================
:: Setup Network Access - MyAssessment
:: Konfigurasi IIS + Firewall untuk akses HP
:: HARUS dijalankan sebagai Administrator!
:: ============================================

echo.
echo ============================================
echo   Setup Network Access - MyAssessment
echo   Agar bisa diakses dari HP via WiFi
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

echo ============================================
echo   STEP 1: Konfigurasi IIS Binding
echo ============================================
echo.

echo [1.1] Menghapus binding lama...
%windir%\system32\inetsrv\appcmd set site /site.name:"MyAssessment" /-bindings.[protocol='http',bindingInformation='169.254.83.107:8000:'] 2>nul
%windir%\system32\inetsrv\appcmd set site /site.name:"MyAssessment" /-bindings.[protocol='http',bindingInformation='*:8000:'] 2>nul
%windir%\system32\inetsrv\appcmd set site /site.name:"MyAssessment" /-bindings.[protocol='http',bindingInformation='10.160.120.209:8000:'] 2>nul
%windir%\system32\inetsrv\appcmd set site /site.name:"MyAssessment" /-bindings.[protocol='http',bindingInformation='localhost:8000:'] 2>nul

echo [1.2] Menambahkan binding * (All Unassigned) port 8000...
%windir%\system32\inetsrv\appcmd set site /site.name:"MyAssessment" /+bindings.[protocol='http',bindingInformation='*:8000:']

echo [OK] IIS Binding dikonfigurasi
echo.

echo ============================================
echo   STEP 2: Konfigurasi Windows Firewall
echo ============================================
echo.

echo [2.1] Menghapus rule lama...
netsh advfirewall firewall delete rule name="MyAssessment Port 8000" >nul 2>&1
netsh advfirewall firewall delete rule name="IIS Port 8000" >nul 2>&1

echo [2.2] Menambahkan rule Inbound TCP port 8000...
netsh advfirewall firewall add rule name="MyAssessment Port 8000" dir=in action=allow protocol=tcp localport=8000 profile=any

echo [OK] Firewall dikonfigurasi
echo.

echo ============================================
echo   STEP 3: Restart IIS
echo ============================================
echo.

echo [3.1] Restart App Pool...
%windir%\system32\inetsrv\appcmd stop apppool /apppool.name:"MyAssessment" 2>nul
timeout /t 2 >nul
%windir%\system32\inetsrv\appcmd start apppool /apppool.name:"MyAssessment"

echo [3.2] Restart Website...
%windir%\system32\inetsrv\appcmd stop site /site.name:"MyAssessment" 2>nul
timeout /t 2 >nul
%windir%\system32\inetsrv\appcmd start site /site.name:"MyAssessment"

echo [OK] IIS di-restart
echo.

echo ============================================
echo   STEP 4: Verifikasi
echo ============================================
echo.

echo Binding saat ini:
%windir%\system32\inetsrv\appcmd list site /site.name:"MyAssessment"

echo.
echo Firewall rule:
netsh advfirewall firewall show rule name="MyAssessment Port 8000" | findstr "Rule Name Enabled Action LocalPort"

echo.
echo ============================================
echo   SETUP SELESAI!
echo ============================================
echo.
echo   Website MyAssessment sekarang bisa diakses:
echo.
echo   [Dari Laptop]
echo   - http://localhost:8000
echo.
echo   [Dari HP - pastikan WiFi sama]
echo   - http://10.160.120.209:8000
echo.
echo   TROUBLESHOOTING jika masih tidak bisa:
echo   1. Pastikan HP dan laptop di WiFi yang SAMA
echo   2. Cek IP laptop: ipconfig (cari IPv4 di WiFi adapter)
echo   3. Matikan antivirus sementara untuk test
echo   4. Coba ping dari HP ke laptop
echo.
pause
