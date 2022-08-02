param(
    [Parameter(Mandatory=$true)]
    [string]$Environment
)
$commitHash = (git rev-parse HEAD).Substring(0,6)
if([string]::IsNullOrEmpty($Env:EUMIS_DEPLOY_USER_NAME))
{
    Write-Error "System variable EUMIS_DEPLOY_USER_NAME is not set!"
    Exit;
}
if([string]::IsNullOrEmpty($Env:EUMIS_DEPLOY_USER_PASSWORD))
{
    Write-Error "System variable EUMIS_DEPLOY_USER_PASSWORD is not set!"
    Exit;
}
if([string]::IsNullOrEmpty($Env:EUMIS_CONFIG_REPO_PATH))
{
    Write-Error "Environment variable EUMIS_CONFIG_REPO_PATH is not set!"
    Exit
}
function Get-MSWebDeployInstallPath(){
    return (Get-ChildItem "HKLM:\SOFTWARE\Microsoft\IIS Extensions\MSDeploy" | Select -last 1).GetValue("InstallPath")
}
function Get-ScriptDirectory { Split-Path $script:MyInvocation.MyCommand.Path }
$msdeploypath = Get-MSWebDeployInstallPath
$msdeploy = Join-Path $msdeploypath "msdeploy.exe"
$buildPath = Join-Path (Get-Item $PSScriptRoot).Parent.FullName "build_scripts\build\"
$settingsFile = Join-Path $Env:EUMIS_CONFIG_REPO_PATH "Developer\Deploy_Config.json"
$offlineFile = "app_offline.htm"
if(![System.IO.File]::Exists($settingsFile)){
    Write-Error "Specified configuration file $settingsFile is not found!"
    Exit
}
Get-ChildItem $buildPath -Filter eumis@$commitHash* | ForEach-Object{
    
    $serverFolder = $_
    $serverName = ($_.BaseName).Replace("eumis@$commitHash-", "")
    $serverName =  $serverName -ireplace [regex]::Escape("-$Environment"), ""
    
    if([string]::IsNullOrEmpty($ServerName))
    {
        Write-Host "Server name not found" 
        Exit
    }
    $settings = Get-Content $settingsFile | ConvertFrom-Json | select $Environment 
    
    ForEach($deploySettings in @($settings)){
        
        @($deploySettings.$Environment.$serverName.apps) | ForEach-Object{
            
            $siteName = $_.Site
            $remote = ",computerName=""https://$($deploySettings.$Environment.$serverName.ipv4):8172/msdeploy.axd?site=$SiteName"",userName=""$ServerName\$Env:EUMIS_DEPLOY_USER_NAME"",password=""$Env:EUMIS_DEPLOY_USER_PASSWORD"",authtype=""Basic"""
            $offlineFilePath = Join-Path $buildPath $ServerFolder
            $offlineFilePath = Join-Path $offlineFilePath $offlineFile

            Try{
                $msdeployArgs = @(
                    "-verb:sync",
                    "-source:contentPath=""$offlineFilePath""",
                    "-dest:contentPath=""$SiteName/$offlineFile"",includeAcls=""False""$remote",
                    "-allowUntrusted"
                    "-enableRule:DoNotDeleteRule"
                ) 
                & $msdeploy $msdeployArgs
                if (!$?) { throw 'deploy script call failed' }
            }
            Catch{
                Write-Host "Application $SiteName is not offline!" -ForegroundColor Red
                Write-Host $_.Exception.Message
                Continue
            }
            Write-Host "Application $SiteName is offline" -ForegroundColor Green
        }
    }
}
