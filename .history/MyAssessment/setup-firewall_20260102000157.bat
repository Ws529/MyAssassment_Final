@echo off
echo ============================================
echo   Setup Firewall untuk MyAssessment
echo ============================================
echo.

:: Hapus rule lama jika ada
netsh advfirewall firewall delete rule name="MyAssessment Port 5000" >nul 2>&1
netsh advfirewall firewall delete rule name="MyAssessment Port 7000" >nul 2>&1
netsh advfirewall firewall delete rule name="MyAssessment Port 8000" >nul 2>&1

:: Tambah rule baru untuk port 8000
netsh advfirewall firewall add rule name="MyAssessment Port 8000" dir=in action=allow protocol=TCP localport=8000

echo.
echo ============================================
echo   Firewall rule berhasil ditambahkan!
echo   Port 8000 sekarang terbuka untuk akses eksternal.
echo ============================================
echo.
pause
