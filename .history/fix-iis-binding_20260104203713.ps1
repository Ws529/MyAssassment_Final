#Requires -RunAsAdministrator
# ============================================
# Fix IIS Binding untuk Akses dari IP Eksternal
# Jalankan sebagai Administrator
# ============================================

$ErrorActionPreference = "Stop"

$SiteName = "MyAssessment"
$Port = 8000

Write-Host ""
Write-Host "  ============================================" -ForegroundColor Cyan
Write-Host "    Fix IIS Binding - MyAssessment" -ForegroundColor White
Write-Host "  ============================================" -ForegroundColor Cyan
Write-Host ""

try {
    Import-Module WebAdministration -ErrorAction Stop
    
    # ==========================================
    # STEP 1: Tampilkan binding saat ini
    # ==========================================
    Write-Host "[1/4] Binding saat ini:" -ForegroundColor Yellow
    $currentBindings = Get-WebBinding -Name $SiteName
    foreach ($b in $currentBindings) {
        Write-Host "       - $($b.bindingInformation)" -ForegroundColor Gray
    }
    
    # ==========================================
    # STEP 2: Hapus semua binding lama
    # ==========================================
    Write-Host ""
    Write-Host "[2/4] Menghapus binding lama..." -ForegroundColor Yellow
    
    # Hapus semua binding yang ada
    Get-WebBinding -Name $SiteName | Remove-WebBinding
    Write-Host "       [OK] Binding lama dihapus" -ForegroundColor Green
    
    # ==========================================
    # STEP 3: Tambah binding baru dengan * (All Unassigned)
    # ==========================================
    Write-Host ""
    Write-Host "[3/4] Menambahkan binding baru..." -ForegroundColor Yellow
    
    # Binding untuk semua IP (*)
    New-WebBinding -Name $SiteName -Protocol "http" -Port $Port -IPAddress "*"
    Write-Host "       [OK] Binding *:$Port ditambahkan (All Unassigned)" -ForegroundColor Green
    
    # ==========================================
    # STEP 4: Restart website
    # ==========================================
    Write-Host ""
    Write-Host "[4/4] Restart website..." -ForegroundColor Yellow
    
    Stop-WebSite -Name $SiteName -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    Start-WebSite -Name $SiteName
    Write-Host "       [OK] Website di-restart" -ForegroundColor Green
    
    # ==========================================
    # Verifikasi
    # ==========================================
    Write-Host ""
    Write-Host "  ============================================" -ForegroundColor Green
    Write-Host "    BINDING BERHASIL DIKONFIGURASI!" -ForegroundColor Green
    Write-Host "  ============================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "  Binding baru:" -ForegroundColor White
    $newBindings = Get-WebBinding -Name $SiteName
    foreach ($b in $newBindings) {
        Write-Host "    -> $($b.bindingInformation)" -ForegroundColor Cyan
    }
    
    # Tampilkan IP address yang tersedia
    Write-Host ""
    Write-Host "  IP Address komputer ini:" -ForegroundColor White
    $ips = Get-NetIPAddress -AddressFamily IPv4 | Where-Object { $_.IPAddress -notlike "127.*" -and $_.IPAddress -notlike "169.254.*" }
    foreach ($ip in $ips) {
        Write-Host "    -> http://$($ip.IPAddress):$Port" -ForegroundColor Cyan
    }
    
    Write-Host ""
    Write-Host "  Akses dari HP:" -ForegroundColor Yellow
    Write-Host "    1. Pastikan HP dan laptop di WiFi yang sama" -ForegroundColor Gray
    Write-Host "    2. Buka browser di HP" -ForegroundColor Gray
    Write-Host "    3. Ketik: http://10.160.120.209:$Port" -ForegroundColor Cyan
    Write-Host ""
    
} catch {
    Write-Host ""
    Write-Host "  [ERROR] $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "  Pastikan:" -ForegroundColor Yellow
    Write-Host "    1. Jalankan sebagai Administrator" -ForegroundColor Gray
    Write-Host "    2. IIS sudah terinstall" -ForegroundColor Gray
    Write-Host "    3. Website '$SiteName' sudah ada di IIS" -ForegroundColor Gray
    Write-Host ""
}

Write-Host "Tekan Enter untuk menutup..." -ForegroundColor Gray
Read-Host
