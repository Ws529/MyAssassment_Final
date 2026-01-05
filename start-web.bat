@echo off
:: ============================================
:: Start MyAssessment Web
:: Jalankan ini setiap mau pakai web
:: ============================================

echo.
echo ============================================
echo   MyAssessment - Start Web
echo ============================================
echo.

:: Check if running as admin for IIS commands
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo [!] Untuk restart IIS, jalankan sebagai Administrator
    echo     Tapi untuk lihat IP saja, lanjut...
    echo.
)

:: Start MongoDB (jika belum jalan)
echo [1/3] Mengecek MongoDB...
tasklist /FI "IMAGENAME eq mongod.exe" 2>NUL | find /I "mongod.exe" >NUL
if %errorLevel% neq 0 (
    echo       MongoDB belum jalan. Pastikan MongoDB sudah start.
) else (
    echo       [OK] MongoDB sudah jalan
)

:: Start IIS (jika admin)
echo.
echo [2/3] Mengecek IIS...
net session >nul 2>&1
if %errorLevel% equ 0 (
    %windir%\system32\inetsrv\appcmd start apppool /apppool.name:"MyAssessment" 2>nul
    %windir%\system32\inetsrv\appcmd start site /site.name:"MyAssessment" 2>nul
    echo       [OK] IIS MyAssessment started
) else (
    echo       [Skip] Butuh admin untuk start IIS
)

:: Show IP addresses
echo.
echo [3/3] URL Akses:
echo.
echo   ============================================
echo   AKSES LOKAL (di laptop ini):
echo   http://localhost:8000
echo   ============================================
echo.
echo   AKSES DARI HP (pilih salah satu):

setlocal enabledelayedexpansion
for /f "tokens=2 delims=:" %%a in ('ipconfig ^| findstr /c:"IPv4"') do (
    set "IP=%%a"
    set "IP=!IP: =!"
    if not "!IP:~0,7!"=="169.254" (
        if not "!IP:~0,3!"=="127" (
            echo   http://!IP!:8000
        )
    )
)
endlocal

echo.
echo   ============================================
echo   PENTING: HP harus di WiFi yang SAMA!
echo   ============================================
echo.

pause
