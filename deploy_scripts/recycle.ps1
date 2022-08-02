param(
    [Parameter(Mandatory=$true)]
    [string]$SiteName,

    [Parameter(Mandatory=$false)]
    [string]$ComputerName,

    [Parameter(Mandatory=$false)]
    [string]$UserName,

    [Parameter(Mandatory=$false)]
    [string]$Password
)

########### improve error handling ###########
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues['*:ErrorAction']='Stop'
##############################################

function Get-MSWebDeployInstallPath(){
    return (Get-ChildItem "HKLM:\SOFTWARE\Microsoft\IIS Extensions\MSDeploy" | Select -last 1).GetValue("InstallPath")
}

$msdeploypath = Get-MSWebDeployInstallPath
$msdeploy = Join-Path $msdeploypath "msdeploy.exe"

$remote = ""
if ($ComputerName -and $UserName -and $Password)
{
    $remote = ",computerName=""$ComputerName"",userName=""$UserName"",password=""$Password"",authtype=""Basic"""
}

$msdeployArgs = @(
    "-verb:sync",
    "-source:recycleApp",
    "-dest:recycleApp=""$SiteName"",recycleMode=""RecycleAppPool""$remote",
    "-disableLink:AppPoolExtension",
    "-disableLink:ContentExtension",
    "-disableLink:CertificateExtension",
    "-allowUntrusted"
)

& $msdeploy $msdeployArgs
if (!$?) { throw 'native call failed' }
