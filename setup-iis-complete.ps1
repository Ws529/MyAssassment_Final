#Requires -RunAsAdministrator
# ============================================
# MyAssessment - Complete IIS Setup
# Jalankan sebagai Administrator
# ============================================

$ErrorActionPreference = "Stop"

# Konfigurasi
$SiteName = "MyAssessment"
$AppPoolName = "MyAssessment"
$PublishPath = "C:\MyAssassment\Publish_Final"
$ProjectFile = "C:\MyAssassment\MyAssessment\MyAssessment.csproj"
$Port = 8000

Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  MyAssessment - Complete IIS Setup" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Step 1: Publish aplikasi
Write-Host "[1/5] Publishing aplikasi..." -ForegroundColor Yellow

if (Test-Path $PublishPath) {
    # Stop IIS dulu agar file tidak terkunci
    try {
        Import-Module WebAdministration -ErrorAction SilentlyContinue
        Stop-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
        Start-Sleep -Seconds 2
    } catch {}
    
    Remove-Item -Path $PublishPath -Recurse -Force -ErrorAction SilentlyContinue
}

$result = & dotnet publish $ProjectFile -c Release -o $PublishPath 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "  [ERROR] Publish gagal!" -ForegroundColor Red
    Write-Host $result
    exit 1
}
Write-Host "  [OK] Publish berhasil" -ForegroundColor Green

# Step 2: Buat web.config untuk IIS
Write-Host "[2/5] Membuat web.config..." -ForegroundColor Yellow

$webConfig = @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" 
                  arguments=".\MyAssessment.dll" 
                  stdoutLogEnabled="true" 
                  stdoutLogFile=".\logs\stdout" 
                  hostingModel="InProcess">
        <environmentVariables>
          <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Production" />
        </environmentVariables>
      </aspNetCore>
    </system.webServer>
  </location>
</configuration>
"@

$webConfig | Out-File -FilePath "$PublishPath\web.config" -Encoding UTF8 -Force
Write-Host "  [OK] web.config dibuat" -ForegroundColor Green

# Buat folder logs
$logsPath = "$PublishPath\logs"
if (!(Test-Path $logsPath)) {
    New-Item -ItemType Directory -Path $logsPath -Force | Out-Null
}

# Step 3: Setup App Pool
Write-Host "[3/5] Mengkonfigurasi App Pool..." -ForegroundColor Yellow

Import-Module WebAdministration

# Hapus App Pool lama jika ada
if (Test-Path "IIS:\AppPools\$AppPoolName") {
    Remove-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
}

# Buat App Pool baru
New-WebAppPool -Name $AppPoolName
Set-ItemProperty "IIS:\AppPools\$AppPoolName" -Name "managedRuntimeVersion" -Value ""
Set-ItemProperty "IIS:\AppPools\$AppPoolName" -Name "startMode" -Value "AlwaysRunning"
Set-ItemProperty "IIS:\AppPools\$AppPoolName" -Name "processModel.identityType" -Value "LocalSystem"

Write-Host "  [OK] App Pool '$AppPoolName' dikonfigurasi" -ForegroundColor Green

# Step 4: Setup Website
Write-Host "[4/5] Mengkonfigurasi Website..." -ForegroundColor Yellow

# Hapus site lama jika ada
if (Test-Path "IIS:\Sites\$SiteName") {
    Remove-Website -Name $SiteName -ErrorAction SilentlyContinue
}

# Buat website baru dengan binding ke semua IP
New-Website -Name $SiteName `
            -PhysicalPath $PublishPath `
            -ApplicationPool $AppPoolName `
            -Port $Port `
            -IPAddress "*" `
            -Force

Write-Host "  [OK] Website '$SiteName' dikonfigurasi di port $Port" -ForegroundColor Green

# Step 5: Set Permissions
Write-Host "[5/5] Mengatur permissions..." -ForegroundColor Yellow

$acl = Get-Acl $PublishPath

# IIS_IUSRS
$rule1 = New-Object System.Security.AccessControl.FileSystemAccessRule(
    "IIS_IUSRS", "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow"
)
$acl.SetAccessRule($rule1)

# IUSR
$rule2 = New-Object System.Security.AccessControl.FileSystemAccessRule(
    "IUSR", "ReadAndExecute", "ContainerInherit,ObjectInherit", "None", "Allow"
)
$acl.SetAccessRule($rule2)

# Local System
$rule3 = New-Object System.Security.AccessControl.FileSystemAccessRule(
    "SYSTEM", "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow"
)
$acl.SetAccessRule($rule3)

Set-Acl $PublishPath $acl
Write-Host "  [OK] Permissions diatur" -ForegroundColor Green

# Start App Pool dan Website
Write-Host ""
Write-Host "Menjalankan website..." -ForegroundColor Yellow
Start-WebAppPool -Name $AppPoolName
Start-Website -Name $SiteName
Start-Sleep -Seconds 3

# Dapatkan IP addresses
$ips = Get-NetIPAddress -AddressFamily IPv4 | 
       Where-Object { $_.IPAddress -notlike "127.*" -and $_.PrefixOrigin -ne "WellKnown" } |
       Select-Object -ExpandProperty IPAddress

Write-Host ""
Write-Host "============================================" -ForegroundColor Green
Write-Host "  SETUP BERHASIL!" -ForegroundColor Green
Write-Host "============================================" -ForegroundColor Green
Write-Host ""
Write-Host "  Website dapat diakses di:" -ForegroundColor White
Write-Host "  -> http://localhost:$Port" -ForegroundColor Cyan
foreach ($ip in $ips) {
    Write-Host "  -> http://${ip}:$Port" -ForegroundColor Cyan
}
Write-Host ""
Write-Host "  Pastikan:" -ForegroundColor Yellow
Write-Host "  1. MongoDB sudah berjalan (localhost:27017)" -ForegroundColor Yellow
Write-Host "  2. Firewall mengizinkan port $Port" -ForegroundColor Yellow
Write-Host ""
Write-Host "  Tekan Enter untuk menutup..." -ForegroundColor Gray
Read-Host
