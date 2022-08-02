param(
    [Parameter(Mandatory=$true)]
    [string]$SiteName
)

########### improve error handling ###########
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues['*:ErrorAction']='Stop'
##############################################

$appcmd = "$env:SystemRoot\system32\inetsrv\appcmd"
$sitepath = "$env:SystemDrive\inetpub\$SiteName\"

& $appcmd delete site $SiteName             ; if (!$?) { throw 'native call failed' }
& $appcmd delete apppool $SiteName          ; if (!$?) { throw 'native call failed' }
Remove-Item $sitepath -Recurse
