param(
    [Parameter(Mandatory=$true)]
    [string]$BranchName
)

########### improve error handling ###########
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues['*:ErrorAction']='Stop'
##############################################

.\create-website.ps1 "$BranchName-EumisPortal" "http://$BranchName.eumis.pis.cielalan.com:80"
.\create-website.ps1 "$BranchName-EumisPublic" "http://$BranchName.eufunds.pis.cielalan.com:80"
.\create-website.ps1 "$BranchName-Eumis" "http://$BranchName.umis.pis.cielalan.com:80"
.\create-website.ps1 "$BranchName-EumisBlob" "http://$BranchName.files.umis.pis.cielalan.com:80"
.\create-website.ps1 "$BranchName-EumisJob" "http://$BranchName.job.umis.pis.cielalan.com:80"
.\create-website.ps1 "$BranchName-EumisPortalIntegration" "http://$BranchName.api.umis.pis.cielalan.com:80"
