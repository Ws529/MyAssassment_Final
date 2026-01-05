@echo off
:: ============================================
:: Start Ngrok Tunnel untuk MyAssessment
:: Akses web dari mana saja dengan 1 URL tetap
:: ============================================

echo.
echo ============================================
echo   MyAssessment - Ngrok Tunnel
echo ============================================
echo.

:: Check if ngrok exists
where ngrok >nul 2>&1
if %errorLevel% neq 0 (
    echo [!] Ngrok belum terinstall!
    echo.
    echo Cara install:
    echo 1. Download dari: https://ngrok.com/download
    echo 2. Extract ngrok.exe ke C:\ngrok atau folder lain
    echo 3. Tambahkan folder ke PATH, atau copy ngrok.exe ke folder ini
    echo 4. Daftar akun gratis di ngrok.com
    echo 5. Jalankan: ngrok config add-authtoken YOUR_TOKEN
    echo.
    pause
    exit /b 1
)

echo [OK] Ngrok ditemukan!
echo.
echo Memulai tunnel ke port 8000...
echo.
echo ============================================
echo   Setelah ngrok jalan, copy URL yang muncul
echo   (yang https://xxxxx.ngrok-free.app)
echo   
echo   URL itu bisa diakses dari HP, komputer lain,
echo   bahkan dari luar jaringan WiFi!
echo ============================================
echo.

ngrok http 8000
