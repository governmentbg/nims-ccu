function Get-EnvironmentServer($userInput, $userMaxChoice) { 

    $userInput = 0;
    
    while($userInput -lt 1 -or $userInput -gt $userMaxChoice){
        $k = 0
        Write-Host
        Write-Host "Choose a server to prepare a build for"
        Write-Host
        foreach ($server in $envServers){
            $k++
            $serverName = $server.Name
            $serverIp = $server.Value.Ipv4
            Write-Host "*$k. $serverName ($serverIp)" -ForegroundColor Green
        }
        $k++
        Write-Host "*$k. All of the above" -ForegroundColor Green
        $k++
        Write-Host "*$k. Quit" -ForegroundColor Yellow
        $userInput = Read-Host
    }

    return $userInput

}

function Get-EnvironmentDictionary($environments){
    $environmentsDictionary = New-Object 'system.collections.generic.dictionary[int, string]'
    $s = 0
    foreach($env in $environments){
        if([string]::IsNullOrEmpty($env.Name)){
            continue
        }
        $s++
        $item = $env.Name
        $environmentsDictionary.Add($s, $item) 
    }

    return $environmentsDictionary
}

function Get-EnvironmentFullName($environmentsDictionary){
    
    Write-Host
    Write-Host "Choose an environment to prepare a build for"
    Write-Host ""
    
    $userEnvironment = 0
    while(-not $environmentsDictionary.ContainsKey($userEnvironment)){
        for($s = 1; $s -le  $environmentsDictionary.Count; $s++){
            $item = $environmentsDictionary[$s]
            Write-Host "*$s. $item" -ForegroundColor Green
        }
        $userEnvironment = Read-Host
    }

    return $environmentsDictionary[$userEnvironment]
}

function Get-EnvironmentShortName([string]$environmentFullName){

    return $environmentFullName.Replace("EGP06.", "")
}

function Set-NugetRestore([string]$nuget){
    
    $nugetConfigFolder = Join-Path (Get-ScriptDirectory) nuget_config
    $fileName = "$nuget.ps1"
    $configFile = Join-Path $nugetConfigFolder $fileName
    
    if([System.IO.File]::Exists($configFile)){
        & $configFile
    }

}

function Set-ProjectDependency($envServers, $serverName){

    [System.Collections.ArrayList]$NugetRestores = @()

    $dependencyFolder = Join-Path (Get-ScriptDirectory) project_scripts
    Set-Location ..
    $server = $envServers|Where-Object{$_.Name -like $serverName}
    
    $apps = @($server.Value.Apps)
    
    foreach($app in $apps){
        if(-not $NugetRestores.Contains($app.Nuget)){
            Set-Location $app.Nuget
            Set-NugetRestore $app.Nuget
            Set-Location ..
            $NugetRestores.Add($app.Nuget)
        }
        $package = $app.Package
        $dependencyFile = "$package.ps1"
        $file = Join-Path $dependencyFolder $dependencyFile
        if([System.IO.File]::Exists($file)){
            Set-Location $app.Nuget
            & $file
            Set-Location ..
        }
    }
}


function Get-ScriptDirectory { Split-Path $script:MyInvocation.MyCommand.Path }

if([string]::IsNullOrEmpty($Env:EUMIS_CONFIG_REPO_PATH))
{
    Write-Error "Environment variable EUMIS_CONFIG_REPO_PATH is not set!"
    Exit
}

$settingsFile = Join-Path $Env:EUMIS_CONFIG_REPO_PATH "Developer\Deploy_Config.json" 
    
if(![System.IO.File]::Exists($settingsFile)){
    Write-Error "Specified configuration file $settingsFile is not found!"
    Exit
}

$build = Join-Path (Get-ScriptDirectory) 'build.ps1'

$environmentsDictionary = New-Object 'system.collections.generic.dictionary[int, string]'
$configData = Get-Content $settingsFile | ConvertFrom-Json
$environments = $configData.psobject.Members | where-object membertype -like 'noteproperty'

$environmentsDictionary = Get-EnvironmentDictionary $environments

$environmentFullName = Get-EnvironmentFullName $environmentsDictionary
    
$environmentSettings = $configData | Select-Object $environmentFullName
$envServers = $environmentSettings.$environmentFullName.psobject.Members | where-object membertype -like 'noteproperty'

$environmentShortName = Get-EnvironmentShortName $environmentFullName

if($envServers.Length -eq 0){
    Write-Host "Servers not found for specified environment. Build with targeting the whole environment $environmentFullName" -ForegroundColor Yellow 
    & $build $environmentShortName

} else {
    $userMaxChoice = $envServers.Length + 2;    
    $userInput = Get-EnvironmentServer $envServers $userMaxChoice

    if($userInput -eq $userMaxChoice){
        Write-Host "Bye"
        Exit
    }

    if($userInput -gt $envServers.Count){
        Write-Host "Build with targeting whole $environmentFullName" -ForegroundColor Yellow
        & $build $environmentShortName  
    }else{
        $serverName = $envServers[$userInput-1].Name
        Write-Host "Build with targeting $environmentFullName for server $serverName" -ForegroundColor Yellow
        Set-ProjectDependency $envServers $serverName
        Set-Location build_scripts
        npm install --quiet
        npm run clean
        npm run gulp -- package -d $environmentShortName --host $serverName
    }
}

Set-Location Build
Get-Childitem -Recurse -filter "*.zip"| Get-Childitem
Set-Location ..

Write-Host "Do you want to continue with deploy to $environmentFullName? y\n" -ForegroundColor Yellow
$userAnswer = Read-Host
if(-not $userAnswer.ToLower().Equals("y")){
    Exit;
}

Set-Location ..

Set-Location deploy_scripts
if(-not (Test-Path -Path "DbUpdater")){
    Clear-Host
    Write-Host "Dbupdater not found." -ForegroundColor Red
    Write-Host "Deploy process is terminated, proceed it manually" -ForegroundColor Yellow

    Set-Location ../build_scripts
    Read-Host
    exit
}
Set-Location DbUpdater

$exitMessage = & .\Dbupdater.exe

if($LASTEXITCODE -eq 1){
    Clear-Host
    Write-Host "Dbupdater execution failed." -ForegroundColor Red
    Write-Host $exitMessage

    Write-Host "Deploy process is terminated, proceed it manually" -ForegroundColor Yellow

    Set-Location ../../build_scripts
    Read-Host
    exit
}

Write-Host $exitMessage

Set-Location ..

# if result of remote-deploy.ps1 is set to a variable
# $resultOfExecution = & '.\remote-deploy.ps1' $environmentFullName
# deploy process is less verbose
& '.\remote-deploy.ps1' $environmentFullName

Set-Location ../build_scripts

Read-Host