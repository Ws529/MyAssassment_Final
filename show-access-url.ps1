# ============================================
# Tampilkan URL Akses MyAssessment
# Otomatis detect IP aktif di semua jaringan
# ============================================

Clear-Host
Write-Host ""
Write-Host "  ============================================" -ForegroundColor Cyan
Write-Host "    MyAssessment - URL Akses" -ForegroundColor White
Write-Host "  ============================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "  [Akses Lokal]" -ForegroundColor Yellow
Write-Host "  http://localhost:8000" -ForegroundColor Green
Write-Host ""

Write-Host "  [Akses dari HP/Device Lain]" -ForegroundColor Yellow
Write-Host "  Gunakan salah satu URL di bawah:" -ForegroundColor Gray
Write-Host ""

# Get all IPv4 addresses (exclude APIPA 169.254.x.x and loopback)
$ips = Get-NetIPAddress -AddressFamily IPv4 | Where-Object { 
    $_.IPAddress -notlike "127.*" -and 
    $_.IPAddress -notlike "169.254.*" -and
    $_.PrefixOrigin -ne "WellKnown"
}

if ($ips.Count -eq 0) {
    Write-Host "  [!] Tidak ada koneksi jaringan aktif" -ForegroundColor Red
} else {
    foreach ($ip in $ips) {
        $adapter = Get-NetAdapter | Where-Object { $_.ifIndex -eq $ip.InterfaceIndex }
        $adapterName = if ($adapter) { $adapter.Name } else { "Unknown" }
        
        Write-Host "  -> http://$($ip.IPAddress):8000" -ForegroundColor Cyan -NoNewline
        Write-Host "  ($adapterName)" -ForegroundColor DarkGray
    }
}

Write-Host ""
Write-Host "  ============================================" -ForegroundColor Cyan
Write-Host "  PENTING:" -ForegroundColor Yellow
Write-Host "  - HP dan laptop harus di WiFi yang SAMA" -ForegroundColor Gray
Write-Host "  - IP akan berubah jika pindah jaringan" -ForegroundColor Gray
Write-Host "  - Jalankan script ini lagi untuk cek IP baru" -ForegroundColor Gray
Write-Host "  ============================================" -ForegroundColor Cyan
Write-Host ""

# Copy to clipboard
$mainIP = ($ips | Select-Object -First 1).IPAddress
if ($mainIP) {
    $url = "http://${mainIP}:8000"
    $url | Set-Clipboard
    Write-Host "  [Copied!] $url sudah di-copy ke clipboard" -ForegroundColor Green
    Write-Host ""
}

Read-Host "  Tekan Enter untuk menutup"
