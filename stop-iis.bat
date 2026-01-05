@echo off
echo ========================================
echo Menghentikan MyAssessment
echo ========================================

echo.
echo 1. Menghentikan Website...
%windir%\system32\inetsrv\appcmd stop site /site.name:"MyAssessment"

echo.
echo 2. Menghentikan Application Pool...
%windir%\system32\inetsrv\appcmd stop apppool /apppool.name:"MyAssessment"

echo.
echo ========================================
echo MyAssessment telah dihentikan
echo ========================================
echo.
pause