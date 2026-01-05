#Requires -RunAsAdministrator
# ============================================
# MyAssessment - Quick Deploy (tanpa clean)
# Untuk update cepat CSS/View saja
# ============================================

$AppPoolName = "MyAssessment"
$ProjectFile = "C:\MyAssassment\MyAssessment\MyAssessment.csproj"
$PublishPath = "C:\MyAssassment\Publish_Final"

Write-Host "`n[1/4] Stopping App Pool..." -ForegroundColor Yellow
Stop-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2

Write-Host "[2/4] Publishing..." -ForegroundColor Yellow
dotnet publish $ProjectFile -c Release -o $PublishPath --no-restore | Out-Null

Write-Host "[3/4] Starting App Pool..." -ForegroundColor Yellow
Start-WebAppPool -Name $AppPoolName
Start-Sleep -Seconds 2

Write-Host "[4/4] Done!" -ForegroundColor Green
Write-Host "`nAkses: http://169.254.83.107:8000" -ForegroundColor Cyan
Write-Host "       http://10.160.120.209:8000`n" -ForegroundColor Cyan
