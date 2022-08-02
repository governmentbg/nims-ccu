param(
    [Parameter(Mandatory = $true)]
    [string]$Environment,

    [Parameter(Mandatory = $true)]
    [string] $pathToConfigRepository
)

$commitHash = (git rev-parse HEAD).Substring(0, 6)

if ([string]::IsNullOrEmpty($Env:EUMIS_DEPLOY_USER_NAME)) {
    Write-Error "Environment variable EUMIS_DEPLOY_USER_NAME is not set!"
    Exit
}

if ([string]::IsNullOrEmpty($Env:EUMIS_DEPLOY_USER_PASSWORD)) {
    Write-Error "Environment variable EUMIS_DEPLOY_USER_PASSWORD is not set!"
    Exit
}


if(-not (Test-Path -Path $pathToConfigRepository)){
    Write-Error "Folder $pathToConfigRepository does not exists!"
    Exit
}

function Get-ScriptDirectory { Split-Path $script:MyInvocation.MyCommand.Path }

$deploy = Join-Path (Get-ScriptDirectory) 'deploy.ps1'
$recycle = Join-Path (Get-ScriptDirectory) 'recycle.ps1' 
$settingsFile = Join-Path $pathToConfigRepository "Developer\Deploy_Config.json" 
$buildPath = Join-Path (Get-Item $PSScriptRoot).Parent.FullName "build_scripts\build\"

if (![System.IO.File]::Exists($settingsFile)) {
    Write-Error "Specified configuration file $settingsFile is not found!"
    Exit
}

Get-ChildItem $buildPath -Filter eumis@$commitHash* | ForEach-Object {
    
    $currentAppFolder = $_

    $settings = Get-Content $settingsFile | ConvertFrom-Json 
    $servers = $settings.$Environment
   
    ForEach ($server in $servers) {
        
        ForEach ($app in $server.apps) {

            $packageLocation = Join-Path $buildPath -ChildPath $currentAppFolder.Name -Resolve

            Try {
                & $deploy `
                    -PackageLocation "$packageLocation" `
                    -PackageName  $app.Package`
                    -ComputerName "https://$($server.ServerIpv4):8172/msdeploy.axd?site=$($app.Site)"`
                    -UserName "$($server.ServerName)\$Env:EUMIS_DEPLOY_USER_NAME" `
                    -Password "$Env:EUMIS_DEPLOY_USER_PASSWORD"`
                    -ExtraParameters @{
                    "IIS Web Application Name" = "$($app.Site)";
                }`
            
            }
            Catch {
                Write-Host "Package $($app.Package) failed to deploy to $($server.ServerName)" -ForegroundColor Red
                Write-Host $_.Exception.Message
                Continue
            }
            Write-Host "Package $($app.Package) deployed on $($server.ServerName)!" -ForegroundColor Green
            
            if ([System.Convert]::ToBoolean($app.Recycle)) {
                Try {                
                    & $recycle `
                        -SiteName $app.Site `
                        -ComputerName "https://$($server.ServerIpv4):8172/msdeploy.axd?site=$($app.Site)" `
                        -UserName "$($server.ServerName)\$Env:EUMIS_DEPLOY_USER_NAME" `
                        -Password "$Env:EUMIS_DEPLOY_USER_PASSWORD" `
                
                }
                Catch {
                    Write-Host "$($app.Site) appPoll failed to recycle!" -ForegroundColor Red
                    Write-Host $_.Exception.Message
                    Continue
                }
                Write-Host "$($app.Site) appPoll recycled successful!" -ForegroundColor Green
            }
        }
    }
}
