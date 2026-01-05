@echo off
echo ============================================
echo   Setup Firewall untuk MyAssessment
echo ============================================
echo.

:: Hapus rule lama jika ada
netsh advfirewall firewall delete rule name="MyAssessment Port 5000" >nul 2>&1

:: Tambah rule baru untuk port 5000
netsh advfirewall firewall add rule name="MyAssessment Port 5000" dir=in action=allow protocol=TCP localport=5000

echo.
echo ============================================
echo   Firewall rule berhasil ditambahkan!
echo   Port 5000 sekarang terbuka untuk akses eksternal.
echo ============================================
echo.
pause
