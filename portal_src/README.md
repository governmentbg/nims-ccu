# Eumis.Portal

#### Create an ASP Session State DB
run `C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_regsql.exe -S . -E -ssadd -sstype c -d Eumis.Portal.Sessions`  
or on a remote server with `C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_regsql.exe -S <ip> -U <user> -P <pass> -ssadd -sstype c -d Eumis.Portal.Sessions`  

#### Test Accounts
Project subsite login - isun@isun.com / 12345678
Report regular login - report@report.com / 12345678
Report access Code login - accesscode@report.com / 0000
