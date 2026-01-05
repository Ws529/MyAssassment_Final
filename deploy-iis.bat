@echo off
:: ============================================
:: MyAssessment - IIS Deployment Launcher
:: Jalankan sebagai Administrator!
:: ============================================

:: Check if running as admin
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo.
    echo ============================================
    echo   PERLU AKSES ADMINISTRATOR!
    echo ============================================
    echo.
    echo   Klik kanan file ini dan pilih:
    echo   "Run as administrator"
    echo.
    pause
    exit /b 1
)

:: Run PowerShell script
powershell -ExecutionPolicy Bypass -File "%~dp0deploy-iis.ps1"
