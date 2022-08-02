param(
    [Parameter(Mandatory=$true)]
    [string]$Environment
)
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
function Get-ScriptDirectory { Split-Path $script:MyInvocation.MyCommand.Path }
function Get-MSWebDeployInstallPath(){
    return (Get-ChildItem "HKLM:\SOFTWARE\Microsoft\IIS Extensions\MSDeploy" | Select -last 1).GetValue("InstallPath")
}
function Get-ObjectMembers {
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory=$True, ValueFromPipeline=$True)]
        [PSCustomObject]$obj
    )
    $obj | Get-Member -MemberType NoteProperty | ForEach-Object {
        $key = $_.Name
        [PSCustomObject]@{Key = $key; Value = $obj."$key"}
    }
}
$msdeploypath = Get-MSWebDeployInstallPath
$msdeploy = Join-Path $msdeploypath "msdeploy.exe"
$settingsFile = Join-Path $Env:EUMIS_CONFIG_REPO_PATH "Developer\Deploy_Config.json"
$offlineFile = "app_offline.htm"

if(![System.IO.File]::Exists($settingsFile)){
    Write-Error "Specified configuration file $settingsFile is not found!"
    Exit
}
Get-Content $settingsFile | ConvertFrom-Json | Get-ObjectMembers | Where-Object Key -Match "$Environment" | ForEach-Object{
    $_.Value | Get-ObjectMembers | ForEach-Object{
        $serverName = $_.Key
        $apps = ($_.Value | Get-ObjectMembers | Where-Object Key -Match "apps").Value
        $ip = ($_.Value | Get-ObjectMembers | Where-Object Key -Match "ipv4").Value
        foreach($app in $apps){
            $siteName = $app.Site
            $remote = ",computerName=""https://$($ip):8172/msdeploy.axd?site=$($siteName)"",userName=""$ServerName\$Env:EUMIS_DEPLOY_USER_NAME"",password=""$Env:EUMIS_DEPLOY_USER_PASSWORD"",authtype=""Basic"""
            Try{
                $msdeployArgs = @(
                    "-verb:delete",
                    "-dest:contentPath=""$($siteName)/$offlineFile"",includeAcls=""False""$remote",
                    "-allowUntrusted"
                )
                & $msdeploy $msdeployArgs
                if (!$?) { throw 'native call failed' }
                }
            Catch{
                Write-Host "Application $SiteName is still offline!" -ForegroundColor Red
                Write-Host $_.Exception.Message
                Continue
            }
            Write-Host "Application $SiteName is online" -ForegroundColor Green
        }
    }	
}
