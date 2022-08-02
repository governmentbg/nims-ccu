Set-Location ..

Set-Location src
& ".\.nuget\NuGet.exe" restore .\Eumis.sln -verbosity quiet
Set-Location ..

Set-Location src\Eumis.Web.App
npm install --quiet
npm run eslint
npm run prettier-check
Set-Location ..\..

Set-Location portal_src
& ".\.nuget\NuGet.exe" restore .\Eumis.Portal.sln -configFile .\.nuget\NuGet.Config -verbosity quiet
Set-Location ..

Set-Location eufunds_src
& ".\.nuget\NuGet.exe" restore .\Eumis.Public.sln -configFile .\.nuget\NuGet.Config -verbosity quiet
Set-Location ..

Set-Location integrations_src
& ".\.nuget\NuGet.exe" restore .\Eumis.ExternalIntegrations.sln -configFile .\.nuget\NuGet.Config -verbosity quiet
Set-Location ..

Set-Location build_scripts
npm install --quiet
npm run clean
npm run gulp -- package -d $args[0] --host $args[1] 
