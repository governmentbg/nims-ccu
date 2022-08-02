param(
    [Parameter(Mandatory=$true)]
    [string]$PackageLocation,

    [Parameter(Mandatory=$true)]
    [string]$BranchName,

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

function Get-ScriptDirectory { Split-Path $script:MyInvocation.MyCommand.Path }

$deploy = Join-Path (Get-ScriptDirectory) 'deploy.ps1'
$recycle = Join-Path (Get-ScriptDirectory) 'recycle.ps1'

& $deploy `
    -PackageLocation $PackageLocation `
    -PackageName "Eumis.Web.Host" `
    -ExtraParameters @{
        "IIS Web Application Name"="$BranchName";
    } `
    -ComputerName $ComputerName `
    -UserName $UserName `
    -Password $Password

& $deploy `
    -PackageLocation $PackageLocation `
    -PackageName "Eumis.Blob.Host" `
    -ExtraParameters @{
        "IIS Web Application Name"="$($BranchName)Blob";
    } `
    -ComputerName $ComputerName `
    -UserName $UserName `
    -Password $Password

& $deploy `
    -PackageLocation $PackageLocation `
    -PackageName "Eumis.Job.Host" `
    -ExtraParameters @{
        "IIS Web Application Name"="$($BranchName)Job";
    } `
    -ComputerName $ComputerName `
    -UserName $UserName `
    -Password $Password

& $deploy `
    -PackageLocation $PackageLocation `
    -PackageName "Eumis.PortalIntegration.Host" `
    -ExtraParameters @{
        "IIS Web Application Name"="$($BranchName)PortalIntegration";
    } `
    -ComputerName $ComputerName `
    -UserName $UserName `
    -Password $Password

& $deploy `
    -PackageLocation $PackageLocation `
    -PackageName "Eumis.Portal.Web" `
    -ExtraParameters @{
        "IIS Web Application Name"="$($BranchName)Portal";
    } `
    -ComputerName $ComputerName `
    -UserName $UserName `
    -Password $Password
	
& $recycle `
    -SiteName "$($BranchName)Job" `
    -ComputerName $ComputerName `
    -UserName $UserName `
    -Password $Password
