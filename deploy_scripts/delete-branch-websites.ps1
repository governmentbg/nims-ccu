param(
    [Parameter(Mandatory=$true)]
    [string]$BranchName
)

########### improve error handling ###########
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues['*:ErrorAction']='Stop'
##############################################

.\delete-website.ps1 "$BranchName-EumisPortal"
.\delete-website.ps1 "$BranchName-EumisPublic"
.\delete-website.ps1 "$BranchName-Eumis"
.\delete-website.ps1 "$BranchName-EumisBlob"
.\delete-website.ps1 "$BranchName-EumisJob"
.\delete-website.ps1 "$BranchName-EumisPortalIntegration"
