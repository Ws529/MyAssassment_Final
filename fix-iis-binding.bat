@echo off
:: ============================================
:: Fix IIS Binding - Jalankan sebagai Admin
:: ============================================

echo.
echo ============================================
echo   Fix IIS Binding - MyAssessment
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

echo [1/5] Menghapus binding lama...
%windir%\system32\inetsrv\appcmd set site /site.name:"MyAssessment" /-bindings.[protocol='http',bindingInformation='169.254.83.107:8000:'] 2>nul
%windir%\system32\inetsrv\appcmd set site /site.name:"MyAssessment" /-bindings.[protocol='http',bindingInformation='*:8000:'] 2>nul
%windir%\system32\inetsrv\appcmd set site /site.name:"MyAssessment" /-bindings.[protocol='http',bindingInformation='10.160.120.209:8000:'] 2>nul

echo [2/5] Menambahkan binding baru (All Unassigned)...
%windir%\system32\inetsrv\appcmd set site /site.name:"MyAssessment" /+bindings.[protocol='http',bindingInformation='*:8000:']

echo [3/5] Restart App Pool...
%windir%\system32\inetsrv\appcmd stop apppool /apppool.name:"MyAssessment" 2>nul
timeout /t 2 >nul
%windir%\system32\inetsrv\appcmd start apppool /apppool.name:"MyAssessment"

echo [4/5] Restart Website...
%windir%\system32\inetsrv\appcmd stop site /site.name:"MyAssessment" 2>nul
timeout /t 2 >nul
%windir%\system32\inetsrv\appcmd start site /site.name:"MyAssessment"

echo [5/5] Verifikasi binding...
echo.
%windir%\system32\inetsrv\appcmd list site /site.name:"MyAssessment"

echo.
echo ============================================
echo   BINDING BERHASIL DIKONFIGURASI!
echo ============================================
echo.
echo   Sekarang website bisa diakses dari:
echo.
echo   - http://localhost:8000
echo   - http://10.160.120.209:8000 (dari HP)
echo.
echo   PENTING: Pastikan HP dan laptop di WiFi yang sama!
echo.
pause
