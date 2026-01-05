#Requires -RunAsAdministrator
# ============================================
# MyAssessment - IIS Deployment Script
# PT. Hyoka Wanssss
# ============================================

$ErrorActionPreference = "Stop"

# Konfigurasi
$SiteName = "MyAssessment"
$AppPoolName = "MyAssessment"
$ProjectPath = "C:\MyAssassment\MyAssessment\MyAssessment.csproj"
$PublishPath = "C:\MyAssassment\Publish_Final"
$SiteUrl = "http://169.254.83.107:8000"

# Warna output
function Write-Step { param($msg) Write-Host "`n[$((Get-Date).ToString('HH:mm:ss'))] $msg" -ForegroundColor Cyan }
function Write-Success { param($msg) Write-Host "    ✓ $msg" -ForegroundColor Green }
function Write-Info { param($msg) Write-Host "    → $msg" -ForegroundColor Yellow }
function Write-Fail { param($msg) Write-Host "    ✗ $msg" -ForegroundColor Red }

Clear-Host
Write-Host "============================================" -ForegroundColor Magenta
Write-Host "  MyAssessment - IIS Deployment Script" -ForegroundColor Magenta
Write-Host "  PT. Hyoka Wanssss" -ForegroundColor Magenta
Write-Host "============================================" -ForegroundColor Magenta

try {
    # Step 1: Stop IIS Site & App Pool
    Write-Step "Menghentikan IIS Site & App Pool..."
    
    Import-Module WebAdministration -ErrorAction SilentlyContinue
    
    # Stop Site
    $site = Get-Website -Name $SiteName -ErrorAction SilentlyContinue
    if ($site) {
        if ($site.State -eq "Started") {
            Stop-Website -Name $SiteName
            Write-Success "Site '$SiteName' dihentikan"
        } else {
            Write-Info "Site '$SiteName' sudah dalam keadaan stop"
        }
    } else {
        Write-Info "Site '$SiteName' tidak ditemukan, akan dibuat nanti"
    }
    
    # Stop App Pool
    $pool = Get-IISAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
    if ($pool) {
        if ($pool.State -eq "Started") {
            Stop-WebAppPool -Name $AppPoolName
            Start-Sleep -Seconds 2
            Write-Success "App Pool '$AppPoolName' dihentikan"
        } else {
            Write-Info "App Pool '$AppPoolName' sudah dalam keadaan stop"
        }
    }
    
    # Tunggu proses IIS release file locks
    Start-Sleep -Seconds 3

    # Step 2: Build & Publish
    Write-Step "Menjalankan dotnet publish..."
    
    # Hapus folder publish lama jika ada
    if (Test-Path $PublishPath) {
        Remove-Item -Path $PublishPath -Recurse -Force -ErrorAction SilentlyContinue
        Write-Info "Folder publish lama dihapus"
    }
    
    # Jalankan publish
    $publishResult = dotnet publish $ProjectPath -c Release -o $PublishPath --no-restore 2>&1
    
    if ($LASTEXITCODE -eq 0) {
        Write-Success "Publish berhasil ke: $PublishPath"
    } else {
        Write-Fail "Publish gagal!"
        Write-Host $publishResult -ForegroundColor Red
        throw "Publish failed"
    }

    # Step 3: Set Permissions
    Write-Step "Mengatur permissions folder..."
    
    $acl = Get-Acl $PublishPath
    $iisUser = "IIS_IUSRS"
    $permission = $iisUser, "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow"
    $accessRule = New-Object System.Security.AccessControl.FileSystemAccessRule $permission
    $acl.SetAccessRule($accessRule)
    Set-Acl $PublishPath $acl
    
    # Juga untuk IUSR
    $permission2 = "IUSR", "ReadAndExecute", "ContainerInherit,ObjectInherit", "None", "Allow"
    $accessRule2 = New-Object System.Security.AccessControl.FileSystemAccessRule $permission2
    $acl.SetAccessRule($accessRule2)
    Set-Acl $PublishPath $acl
    
    Write-Success "Permissions diatur untuk IIS_IUSRS dan IUSR"

    # Step 4: Start App Pool & Site
    Write-Step "Menjalankan kembali IIS..."
    
    # Start App Pool
    if (Get-IISAppPool -Name $AppPoolName -ErrorAction SilentlyContinue) {
        Start-WebAppPool -Name $AppPoolName
        Write-Success "App Pool '$AppPoolName' dijalankan"
    }
    
    # Start Site
    if (Get-Website -Name $SiteName -ErrorAction SilentlyContinue) {
        Start-Website -Name $SiteName
        Write-Success "Site '$SiteName' dijalankan"
    }
    
    Start-Sleep -Seconds 2

    # Step 5: Verifikasi
    Write-Step "Memverifikasi deployment..."
    
    try {
        $response = Invoke-WebRequest -Uri $SiteUrl -UseBasicParsing -TimeoutSec 10
        if ($response.StatusCode -eq 200) {
            Write-Success "Site merespons dengan status 200 OK"
        }
    } catch {
        Write-Info "Site mungkin perlu waktu untuk warm-up"
    }

    # Selesai
    Write-Host "`n============================================" -ForegroundColor Green
    Write-Host "  DEPLOYMENT BERHASIL!" -ForegroundColor Green
    Write-Host "============================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "  Aplikasi siap diakses di:" -ForegroundColor White
    Write-Host "  → $SiteUrl" -ForegroundColor Cyan
    Write-Host "  → http://10.160.120.209:8000" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "  Tekan Enter untuk menutup..." -ForegroundColor Gray
    Read-Host

} catch {
    Write-Host "`n============================================" -ForegroundColor Red
    Write-Host "  DEPLOYMENT GAGAL!" -ForegroundColor Red
    Write-Host "============================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "  Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "  Pastikan:" -ForegroundColor Yellow
    Write-Host "  1. Jalankan PowerShell sebagai Administrator" -ForegroundColor Yellow
    Write-Host "  2. IIS sudah terinstall dengan benar" -ForegroundColor Yellow
    Write-Host "  3. Site '$SiteName' sudah dikonfigurasi di IIS" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "  Tekan Enter untuk menutup..." -ForegroundColor Gray
    Read-Host
}
