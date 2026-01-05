@echo off
echo ========================================
echo Setup IIS untuk MyAssessment
echo ========================================
echo.
echo PENTING: Script ini harus dijalankan sebagai Administrator!
echo.

REM Cek apakah running sebagai admin
net session >nul 2>&1
if %errorLevel% == 0 (
    echo [OK] Running sebagai Administrator
) else (
    echo [ERROR] Script ini memerlukan hak Administrator!
    echo Klik kanan pada file ini dan pilih "Run as administrator"
    pause
    exit /b 1
)

echo.
echo 1. Menghapus site lama jika ada...
%windir%\system32\inetsrv\appcmd delete site "MyAssessment" 2>nul
%windir%\system32\inetsrv\appcmd delete apppool "MyAssessment" 2>nul

echo.
echo 2. Membuat Application Pool...
%windir%\system32\inetsrv\appcmd add apppool /name:"MyAssessment" /managedRuntimeVersion:"v4.0" /processModel.identityType:ApplicationPoolIdentity

echo.
echo 3. Membuat Website...
%windir%\system32\inetsrv\appcmd add site /name:"MyAssessment" /physicalPath:"%CD%\WEBSAMPLE" /bindings:"http/*:9000:"

echo.
echo 4. Mengatur Application Pool untuk Website...
%windir%\system32\inetsrv\appcmd set app "MyAssessment/" /applicationPool:"MyAssessment"

echo.
echo 5. Memulai Application Pool...
%windir%\system32\inetsrv\appcmd start apppool /apppool.name:"MyAssessment"

echo.
echo 6. Memulai Website...
%windir%\system32\inetsrv\appcmd start site /site.name:"MyAssessment"

echo.
echo 7. Testing koneksi...
timeout /t 2 /nobreak >nul
curl -s http://localhost:9000 >nul 2>&1
if %errorLevel% == 0 (
    echo [OK] Website berhasil diakses
) else (
    echo [WARNING] Website mungkin belum siap, tunggu beberapa detik
)

echo.
echo ========================================
echo Setup selesai!
echo ========================================
echo.
echo Aplikasi dapat diakses di:
echo - Frontend (Python): http://127.0.0.1:8080
echo - Backend API (IIS): http://localhost:9000
echo - Test API: http://localhost:9000/api/test
echo.
echo Untuk menghentikan, jalankan: stop-iis.bat
echo.
pause