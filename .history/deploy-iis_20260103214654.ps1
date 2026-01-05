#Requires -RunAsAdministrator
# ============================================
# MyAssessment - IIS Deployment Script
# PT. Hyoka Wanssss
# Clean Build + Deploy to IIS
# ============================================

$ErrorActionPreference = "Stop"

# Konfigurasi
$AppPoolName = "MyAssessment"
$SiteName = "MyAssessment"
$ProjectPath = "C:\MyAssassment\MyAssessment"
$ProjectFile = "$ProjectPath\MyAssessment.csproj"
$PublishPath = "C:\MyAssassment\Publish_Final"
$SiteUrl = "http://169.254.83.107:8000"
$SiteUrl2 = "http://10.160.120.209:8000"

# Warna output
function Write-Step { param($msg) Write-Host "`n[$((Get-Date).ToString('HH:mm:ss'))] $msg" -ForegroundColor Cyan }
function Write-Success { param($msg) Write-Host "    [OK] $msg" -ForegroundColor Green }
function Write-Info { param($msg) Write-Host "    [i] $msg" -ForegroundColor Yellow }
function Write-Fail { param($msg) Write-Host "    [X] $msg" -ForegroundColor Red }

Clear-Host
Write-Host ""
Write-Host "  ============================================" -ForegroundColor Magenta
Write-Host "    MyAssessment - IIS Deployment Script" -ForegroundColor White
Write-Host "    PT. Hyoka Wanssss" -ForegroundColor Gray
Write-Host "  ============================================" -ForegroundColor Magenta
Write-Host ""

$startTime = Get-Date

try {
    # ==========================================
    # STEP 1: Stop IIS App Pool
    # ==========================================
    Write-Step "Menghentikan IIS Application Pool..."
    
    Import-Module WebAdministration -ErrorAction SilentlyContinue
    
    $pool = Get-IISAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
    if ($pool -and $pool.State -eq "Started") {
        Stop-WebAppPool -Name $AppPoolName
        Write-Success "App Pool '$AppPoolName' dihentikan"
        
        # Tunggu sampai benar-benar stop
        $timeout = 30
        $elapsed = 0
        while ((Get-IISAppPool -Name $AppPoolName).State -ne "Stopped" -and $elapsed -lt $timeout) {
            Start-Sleep -Seconds 1
            $elapsed++
        }
        Write-Info "Menunggu file lock dilepas..."
        Start-Sleep -Seconds 2
    } else {
        Write-Info "App Pool sudah dalam keadaan stop"
    }

    # ==========================================
    # STEP 2: Clean Build (Hapus bin & obj)
    # ==========================================
    Write-Step "Membersihkan folder build lama..."
    
    $binPath = "$ProjectPath\bin"
    $objPath = "$ProjectPath\obj"
    
    if (Test-Path $binPath) {
        Remove-Item -Path $binPath -Recurse -Force -ErrorAction SilentlyContinue
        Write-Success "Folder bin dihapus"
    }
    
    if (Test-Path $objPath) {
        Remove-Item -Path $objPath -Recurse -Force -ErrorAction SilentlyContinue
        Write-Success "Folder obj dihapus"
    }
    
    # Hapus folder publish lama
    if (Test-Path $PublishPath) {
        Remove-Item -Path $PublishPath -Recurse -Force -ErrorAction SilentlyContinue
        Write-Success "Folder publish lama dihapus"
    }

    # ==========================================
    # STEP 3: Restore & Publish
    # ==========================================
    Write-Step "Menjalankan dotnet restore..."
    
    $restoreResult = & dotnet restore $ProjectFile 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Success "Restore berhasil"
    } else {
        throw "Restore gagal: $restoreResult"
    }
    
    Write-Step "Menjalankan dotnet publish (Release)..."
    
    $publishResult = & dotnet publish $ProjectFile -c Release -o $PublishPath 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Success "Publish berhasil ke: $PublishPath"
    } else {
        Write-Fail "Publish gagal!"
        Write-Host $publishResult -ForegroundColor Red
        throw "Publish failed"
    }

    # ==========================================
    # STEP 4: Set Folder Permissions
    # ==========================================
    Write-Step "Mengatur permissions folder..."
    
    $acl = Get-Acl $PublishPath
    
    # IIS_IUSRS - Full Control
    $iisRule = New-Object System.Security.AccessControl.FileSystemAccessRule(
        "IIS_IUSRS", "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow"
    )
    $acl.SetAccessRule($iisRule)
    
    # IUSR - Read & Execute
    $iusrRule = New-Object System.Security.AccessControl.FileSystemAccessRule(
        "IUSR", "ReadAndExecute", "ContainerInherit,ObjectInherit", "None", "Allow"
    )
    $acl.SetAccessRule($iusrRule)
    
    Set-Acl $PublishPath $acl
    Write-Success "Permissions diatur untuk IIS_IUSRS dan IUSR"

    # ==========================================
    # STEP 5: Start IIS App Pool
    # ==========================================
    Write-Step "Menjalankan kembali IIS..."
    
    Start-WebAppPool -Name $AppPoolName
    Write-Success "App Pool '$AppPoolName' dijalankan"
    
    # Tunggu warm-up
    Start-Sleep -Seconds 3

    # ==========================================
    # STEP 6: Verifikasi
    # ==========================================
    Write-Step "Memverifikasi deployment..."
    
    try {
        $response = Invoke-WebRequest -Uri $SiteUrl -UseBasicParsing -TimeoutSec 15 -ErrorAction Stop
        if ($response.StatusCode -eq 200) {
            Write-Success "Site merespons dengan status 200 OK"
        }
    } catch {
        Write-Info "Site mungkin perlu waktu warm-up (coba refresh browser)"
    }

    # ==========================================
    # SELESAI
    # ==========================================
    $endTime = Get-Date
    $duration = ($endTime - $startTime).TotalSeconds

    Write-Host ""
    Write-Host "  ============================================" -ForegroundColor Green
    Write-Host "    DEPLOYMENT BERHASIL!" -ForegroundColor Green
    Write-Host "  ============================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "    Waktu: $([math]::Round($duration, 1)) detik" -ForegroundColor Gray
    Write-Host ""
    Write-Host "    Aplikasi siap diakses di:" -ForegroundColor White
    Write-Host "    -> $SiteUrl" -ForegroundColor Cyan
    Write-Host "    -> $SiteUrl2" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "    Tips: Tekan Ctrl+Shift+R di browser untuk" -ForegroundColor Gray
    Write-Host "          hard refresh dan clear cache" -ForegroundColor Gray
    Write-Host ""

} catch {
    Write-Host ""
    Write-Host "  ============================================" -ForegroundColor Red
    Write-Host "    DEPLOYMENT GAGAL!" -ForegroundColor Red
    Write-Host "  ============================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "    Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "    Pastikan:" -ForegroundColor Yellow
    Write-Host "    1. Jalankan sebagai Administrator" -ForegroundColor Yellow
    Write-Host "    2. IIS sudah terinstall" -ForegroundColor Yellow
    Write-Host "    3. App Pool '$AppPoolName' sudah ada di IIS" -ForegroundColor Yellow
    Write-Host "    4. MongoDB sudah berjalan" -ForegroundColor Yellow
    Write-Host ""
}

Write-Host "  Tekan Enter untuk menutup..." -ForegroundColor Gray
Read-Host
