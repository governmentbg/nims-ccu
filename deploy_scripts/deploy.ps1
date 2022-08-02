param(
    [Parameter(Mandatory=$true)]
    [string]$PackageLocation,

    [Parameter(Mandatory=$true)]
    [string]$PackageName,

    [Parameter(Mandatory=$false)]
    [string]$ComputerName,

    [Parameter(Mandatory=$false)]
    [string]$UserName,

    [Parameter(Mandatory=$false)]
    [string]$Password,

    [Parameter(Mandatory=$false)]
    [hashtable]$ExtraParameters
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

$extraParams = @()
foreach ($key in $ExtraParameters.Keys)
{
    $extraParams += @("-setParam:name=""$key"",value=""$($ExtraParameters[$key])""")
}

$packageZipPath = Join-Path $PackageLocation "$PackageName.zip"
$packageParamsFilePath = Join-Path $PackageLocation "$PackageName.SetParameters.xml"

$msdeployArgs = @(
    "-verb:sync",
    "-source:package=""$packageZipPath""",
    "-dest:auto,includeAcls=""False""$remote",
    "-setParamFile:""$packageParamsFilePath""",
    "-disableLink:AppPoolExtension",
    "-disableLink:ContentExtension",
    "-disableLink:CertificateExtension",
    "-allowUntrusted"
) + $extraParams

& $msdeploy $msdeployArgs
if (!$?) { throw 'native call failed' }
