function Get-EnvironmentServers($environments, $environment) {
    return $environment.SERVERS
}

function Get-ScriptDirectory { Split-Path $script:MyInvocation.MyCommand.Path }


function Update-Content{
    param(
        [string]$logPath,
        [Collections.Generic.List[LogEntry]] $logs)
        
    Clear-Content -Path $logsPath
    Add-Content -Path $logsPath -Value ($logs | ConvertTo-Json -AsArray) 
}

$sessionId = [guid]::NewGuid()
$vpn = Join-Path (Get-ScriptDirectory) 'vpn.ps1'
$rdp = Join-Path (Get-ScriptDirectory) 'rdp.ps1'

$settingsFile = Join-Path (Get-ScriptDirectory) 'rdp.json' 

if (![System.IO.File]::Exists($settingsFile)) {
    Write-Error "Specified configuration file $settingsFile is not found!"
    Exit
}

$settings = Get-Content $settingsFile | ConvertFrom-Json | select Environments
Write-Host "Select an environment to connect:"
$k = 0;
ForEach ($environment in $settings.ENVIRONMENTS) {
    $k++;
    Write-host "$k." $environment.NAME
}

$environmentChoise = Read-Host
$environmentIndex = [int]$environmentChoise - 1;

$environment = $settings.ENVIRONMENTS[$environmentIndex];

$vpnUser = [Environment]::GetEnvironmentVariable($environment.VPN_PROFILE.USER, 'Machine');
$vpnPwd = [string][Environment]::GetEnvironmentVariable($environment.VPN_PROFILE.PWD, 'Machine')

Write-Host "Initialize VPN connection"
& $vpn $environment.VPN_PROFILE.NAME $vpnUser $vpnPwd

Start-Sleep -m 1000

$availableServers = Get-EnvironmentServers $settings.ENVIRONMENTS $environment

$j = 99;
while ($j -ne 0) {
    Write-Host "Select a server to connect"
    $k = 0;
    ForEach ($server in $availableServers) {
        $serverInfo = $server.psobject.Members | where-object membertype -like 'noteproperty'
        $serverName = $serverInfo.Name
        $serverValue = $serverInfo.Value
        $k++;
        Write-host "$k. $serverName ($serverValue)"
    }
    Write-Host -BackgroundColor DarkMagenta "0. For proper exit"

    $userInput = Read-Host
    $j = [int]$userInput
    if ($j -ne 0) {
        $srv = $availableServers[$j - 1].psobject.Members | where-object membertype -like 'noteproperty'
        $serverValue = $srv.Value
        $serverName = $srv.Name
        Write-Host "Running rdp to $serverValue"

        & $rdp $serverName
       
    }
}

# Terminate all vpnui processes.
Get-Process | ForEach-Object {if($_.ProcessName.ToLower() -eq "vpnui")
{$Id = $_.Id; Stop-Process $Id; echo "Process vpnui with id: $Id was stopped"}}
# Terminate all vpncli processes.
Get-Process | ForEach-Object {if($_.ProcessName.ToLower() -eq "vpncli")
{$Id = $_.Id; Stop-Process $Id; echo "Process vpncli with id: $Id was stopped"}}
