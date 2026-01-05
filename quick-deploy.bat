@echo off
:: Quick Deploy - Jalankan sebagai Administrator
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo Perlu akses Administrator! Klik kanan - Run as administrator
    pause
    exit /b 1
)
powershell -ExecutionPolicy Bypass -File "%~dp0quick-deploy.ps1"
pause
