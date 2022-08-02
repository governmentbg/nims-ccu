$commitHash = (git rev-parse HEAD).Substring(0,6)

function Get-ScriptDirectory { Split-Path $script:MyInvocation.MyCommand.Path }

$buildPath = Join-Path (Get-Item $PSScriptRoot).Parent.FullName "build_scripts\build\"
$serverName = "ciela-isunweb1"
$Environment = "Ciela.CI"
$location = Join-Path $buildPath "eumis@$commitHash-$serverName-$Environment"
$deploy = Join-Path (Get-ScriptDirectory) 'deploy.ps1'
$user = "Administrator"
$password = "P@ssw0rd"
if(!(Test-Path -Path $location)){
    Write-Error "Expected location $location does not exist!"
    exit
}
$BranchName = "dev"
$SiteName = "$BranchName-Eumis"
$packageName ="Eumis.Web.Host"
Try{
    

                    & $deploy `
                    -PackageLocation "$location" `
                    -PackageName  "$packageName"`
                    -ComputerName "https://172.30.1.245:8172/msdeploy.axd?site=$siteName"`
                    -UserName "$serverName\$user" `
                    -Password "$password"`
                    -ExtraParameters @{
                        "IIS Web Application Name"="$SiteName";
                        "DbContext-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-Eumis;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "Logs1-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-EumisLogs1;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "Eumis.Web.Host:BlobServerLocation"="http://$BranchName.files.isun.zonebg.com/";
                        "Eumis.Web.Host:InternalBlobServerLocation"="http://$BranchName.files.isun.zonebg.com/";
                        "Eumis.Web.Host:BlobServerTokenLocation"="http://$BranchName.files.isun.zonebg.com/api/token";
                        "Eumis.Web.Api:PortalLocationForIFrame"="http://$BranchName.eumis.isun.zonebg.com";
                        "Eumis.ApplicationServices:PortalLocationForAPIs"="http://$BranchName.eumis.isun.zonebg.com";
		                "ErrorfileFilename"="C:\Logs\$BranchName\Eumis.Web.Host\`${date:format=dd.MM.yyyy}.txt";
                    }`

                    Write-Host "$packageName is deployed successwully!" -ForegroundColor Green

}
Catch{
    Write-Host "Application $SiteName is not deployed!" -ForegroundColor Red
    Write-Host $_.Exception.Message
}

$SiteName = "$BranchName-EumisBlob"
$packageName ="Eumis.Blob.Host"
Try{
    

                    & $deploy `
                    -PackageLocation "$location" `
                    -PackageName  "$packageName"`
                    -ComputerName "https://172.30.1.245:8172/msdeploy.axd?site=$siteName"`
                    -UserName "$serverName\$user" `
                    -Password "$password"`
                    -ExtraParameters @{
                        "IIS Web Application Name"="$SiteName";
                        "DbContext-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-Eumis;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "Logs1-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-EumisLogs1;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "Blobs1-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-EumisBlobs1;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
		                "ErrorfileFilename"="C:\Logs\$BranchName\Eumis.Blob.Host\`${date:format=dd.MM.yyyy}.txt";
                    }`

                    Write-Host "$packageName is deployed successfully!" -ForegroundColor Green

}
Catch{
    Write-Host "Application $SiteName is not deployed!" -ForegroundColor Red
    Write-Host $_.Exception.Message
}

$SiteName = "$BranchName-EumisPortal"
$packageName ="Eumis.Portal.Web"
Try{
    

                    & $deploy `
                    -PackageLocation "$location" `
                    -PackageName  "$packageName"`
                    -ComputerName "https://172.30.1.245:8172/msdeploy.axd?site=$siteName"`
                    -UserName "$serverName\$user" `
                    -Password "$password"`
                    -ExtraParameters @{
                        "IIS Web Application Name"="$SiteName";
                        "DbContext-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-Eumis.Portal;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;MultipleActiveResultSets=True";
                        "PortalSessions-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-Eumis.Portal.Sessions;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;MultipleActiveResultSets=True";
                        "Eumis.Components:ServerLocation"="http://$BranchName.api.isun.zonebg.com/api/";
                        "Eumis.Components:BlobServerLocation"="http://$BranchName.files.isun.zonebg.com/";
                        "Eumis.Components:InternalBlobServerLocation"="http://$BranchName.files.isun.zonebg.com/";
                        "Eumis.Portal.Web:UmisGovernmentLocation"="http://$BranchName.umis.pis.cielalan.com/";
                    }`

                    Write-Host "$packageName is deployed successfully!" -ForegroundColor Green
}
Catch{
    Write-Host "Application $SiteName is not deployed!" -ForegroundColor Red
    Write-Host $_.Exception.Message
}

$SiteName = "$BranchName-EumisPortalIntegration"
$packageName ="Eumis.PortalIntegration.Host"
Try{
    

                    & $deploy `
                    -PackageLocation "$location" `
                    -PackageName  "$packageName"`
                    -ComputerName "https://172.30.1.245:8172/msdeploy.axd?site=$siteName"`
                    -UserName "$serverName\$user" `
                    -Password "$password"`
                    -ExtraParameters @{
                        "IIS Web Application Name"="$SiteName";
                        "DbContext-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-Eumis;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "Logs1-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-EumisLogs1;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "Eumis.Web.Host:BlobServerLocation"="http://$BranchName.files.isun.zonebg.com/";
                        "Eumis.Web.Host:InternalBlobServerLocation"="http://$BranchName.files.isun.zonebg.com/";
                        "Eumis.Web.Host:BlobServerTokenLocation"="http://$BranchName.files.isun.zonebg.com/api/token";
                        "Eumis.Web.Api:PortalLocationForIFrame"="http://$BranchName.eumis.isun.zonebg.com";
                        "Eumis.ApplicationServices:PortalLocationForAPIs"="http://$BranchName.eumis.isun.zonebg.com";
                        "ErrorfileFilename"="C:\Logs\$BranchName\Eumis.PortalIntegration.Host\`${date:format=dd.MM.yyyy}.txt";
                    }`

                    Write-Host "$packageName is deployed successfully!" -ForegroundColor Green
                    

}
Catch{
    Write-Host "Application $SiteName is not deployed!" -ForegroundColor Red
    Write-Host $_.Exception.Message
}

$SiteName = "$BranchName-EumisJob"
$packageName ="Eumis.Job.Host"
Try{
    

                    & $deploy `
                    -PackageLocation "$location" `
                    -PackageName  "$packageName"`
                    -ComputerName "https://172.30.1.245:8172/msdeploy.axd?site=$siteName"`
                    -UserName "$serverName\$user" `
                    -Password "$password"`
                    -ExtraParameters @{
                        "IIS Web Application Name"="$SiteName";
                        "DbContext-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-Eumis;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "Logs1-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-EumisLogs1;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "Eumis.ApplicationServices:PortalLocationForAPIs"="http://$BranchName.eumis.pis.cielalan.com";
                        "Eumis.Job.Host:PortalUrl"="http://$BranchName.eumis.isun.zonebg.com";
                        "Eumis.Job.Host:PortalReportUrl"="http://$BranchName.eumis.isun.zonebg.com/report";
                        "Eumis.Job.Host:SystemUrl"="http://$BranchName.umis.isun.zonebg.com";
                        "ErrorfileFilename"="C:\Logs\$BranchName\Eumis.Job.Host\`${date:format=dd.MM.yyyy}.txt";
                    }`

                    Write-Host "$packageName is deployed successfully!" -ForegroundColor Green
                    

}
Catch{
    Write-Host "Application $SiteName is not deployed!" -ForegroundColor Red
    Write-Host $_.Exception.Message
}

$SiteName = "$BranchName-EumisPublic"
$packageName ="Eumis.Public.Web"
Try{
    

                    & $deploy `
                    -PackageLocation "$location" `
                    -PackageName  "$packageName"`
                    -ComputerName "https://172.30.1.245:8172/msdeploy.axd?site=$siteName"`
                    -UserName "$serverName\$user" `
                    -Password "$password"`
                    -ExtraParameters @{
                        "IIS Web Application Name"="$SiteName";
                        "DbContextUmis-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-Eumis;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "DbContextMain-Web.config Connection String"="Data Source=%EUMIS_SQL_SERVER_IP%;Initial Catalog=$BranchName-EumisPublic;MultipleActiveResultSets=True;User Id=%EUMIS_SQL_SERVER_USER%;Password=%EUMIS_SQL_SERVER_PASS%;";
                        "Eumis.Public.Common:PortalLocation"="http://$BranchName.eumis.isun.zonebg.com";
                    }`

                    Write-Host "$packageName is deployed successfully!" -ForegroundColor Green
                    

}
Catch{
    Write-Host "Application $SiteName is not deployed!" -ForegroundColor Red
    Write-Host $_.Exception.Message
}