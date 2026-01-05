param(
    [string]$SiteName = "MyAssessment",
    [string]$ProjectPath = "C:\MyAssassment\MyAssessment\MyAssessment.csproj",
    [string]$PublishDir = "C:\MyAssassment\Publish_Final",
    [string]$NotifyIP = "169.254.83.107",
    [int]$NotifyPort = 8000
)

# Elevation check — relaunch elevated if needed
$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $isAdmin) {
    Write-Host "Not running as Administrator — relaunching elevated..."
    Start-Process -FilePath "powershell.exe" -ArgumentList "-NoProfile -ExecutionPolicy Bypass -File `"$PSCommandPath`" -SiteName `"$SiteName`" -ProjectPath `"$ProjectPath`" -PublishDir `"$PublishDir`" -NotifyIP `"$NotifyIP`" -NotifyPort $NotifyPort" -Verb RunAs
    exit
}

Try { Import-Module WebAdministration -ErrorAction Stop } Catch { Write-Warning "Failed to import WebAdministration: $_" }

Write-Host "Stopping IIS site '$SiteName' (if exists)..."
$appPool = $null
Try {
    if (Test-Path "IIS:\Sites\$SiteName") {
        $site = Get-Item "IIS:\Sites\$SiteName"
        $appPool = $site.applicationPool
        Stop-Website -Name $SiteName -ErrorAction SilentlyContinue
        if ($appPool) { Stop-WebAppPool -Name $appPool -ErrorAction SilentlyContinue }
        Start-Sleep -Seconds 1
    } else {
        Write-Host "Site '$SiteName' not found in IIS — continuing (publish will still run)."
    }
} Catch { Write-Warning "Error stopping site/app-pool: $_" }

Write-Host "Running: dotnet publish $ProjectPath -> $PublishDir"
$dotnetExe = "dotnet"
$publishArgs = @('publish', $ProjectPath, '-c', 'Release', '-o', $PublishDir)
Try {
    $proc = Start-Process -FilePath $dotnetExe -ArgumentList $publishArgs -NoNewWindow -Wait -PassThru -ErrorAction Stop
} Catch {
    Write-Error "Failed to start dotnet publish: $_"
    if ($appPool) { Start-WebAppPool -Name $appPool -ErrorAction SilentlyContinue }
    if (Test-Path "IIS:\Sites\$SiteName") { Start-Website -Name $SiteName -ErrorAction SilentlyContinue }
    exit 1
}
if ($proc.ExitCode -ne 0) {
    Write-Error "dotnet publish failed with exit code $($proc.ExitCode)."
    if ($appPool) { Start-WebAppPool -Name $appPool -ErrorAction SilentlyContinue }
    if (Test-Path "IIS:\Sites\$SiteName") { Start-Website -Name $SiteName -ErrorAction SilentlyContinue }
    exit $proc.ExitCode
}

Write-Host "Applying permissions to publish folder: $PublishDir"
Try {
    icacls $PublishDir /grant "IIS_IUSRS:(OI)(CI)M" /T | Out-Null
} Catch { Write-Warning "icacls failed: $_" }

Write-Host "Starting IIS site '$SiteName' (if exists)..."
Try {
    if ($appPool) { Start-WebAppPool -Name $appPool -ErrorAction SilentlyContinue }
    if (Test-Path "IIS:\Sites\$SiteName") { Start-Website -Name $SiteName -ErrorAction SilentlyContinue }
} Catch { Write-Warning "Error starting site/app-pool: $_" }

Write-Host "\nDeployment finished — site should be available at: http://$NotifyIP`:$NotifyPort"
Write-Host "If the site does not appear, check IIS bindings and firewall settings."

exit 0
