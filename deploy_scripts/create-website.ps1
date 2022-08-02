param(
    [Parameter(Mandatory=$true)]
    [string]$SiteName,

    [Parameter(Mandatory=$true)]
    [string]$Bindings
)

########### improve error handling ###########
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues['*:ErrorAction']='Stop'
##############################################

$appcmd = "$env:SystemRoot\system32\inetsrv\appcmd"
$sitepath = "$env:SystemDrive\inetpub\$SiteName\"

if (Test-Path $sitepath -PathType Container) {
    exit
}

New-Item -ItemType Directory -Force -Path $sitepath

& $appcmd add site /name:$SiteName /bindings:$Bindings /physicalPath:$sitepath                                                                                          ; if (!$?) { throw 'native call failed' }

& $appcmd add apppool /name:$SiteName /managedRuntimeVersion:"v4.0" /managedPipelineMode:"Integrated"                                                                   ; if (!$?) { throw 'native call failed' }
& $appcmd set apppool $SiteName /startMode:"AlwaysRunning" /processModel.idleTimeout:"0.00:00:00" /recycling.periodicRestart.time:"00:00:00"                            ; if (!$?) { throw 'native call failed' }
& $appcmd set config -section:system.applicationHost/applicationPools /+"[name='$SiteName'].recycling.periodicRestart.schedule.[value='04:00:00']" /commit:apphost      ; if (!$?) { throw 'native call failed' }

& $appcmd set app "$SiteName/" /applicationPool:$SiteName /preloadEnabled:true                                                                                          ; if (!$?) { throw 'native call failed' }
