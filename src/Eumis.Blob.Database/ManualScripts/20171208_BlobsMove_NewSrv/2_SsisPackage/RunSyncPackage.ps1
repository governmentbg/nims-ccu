$Database = "EumisBlobs"
$RemoveServerIp = "192.168.101.118"
$PackagePath = ".\BlobsSyncPackage.dtsx"

$RemoteServerUser = Read-Host -Prompt 'Enter remote server user'
$RemoteServerPassword = Read-Host -Prompt 'Enter remote server password'

&"dtexec" `
  "/FILE","$PackagePath",
  "/CONNECTION","Local;Data Source=.;Initial Catalog=$Database;Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;",
  "/CONNECTION","Log;Data Source=.;Initial Catalog=SsisLog;Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;",
  "/CONNECTION","Remote;Data Source=$RemoveServerIp;User ID=$RemoteServerUser;Password=$RemoteServerPassword;Initial Catalog=UsedBlobContents;Provider=SQLNCLI11.1;Auto Translate=False;",
  "/CHECKPOINTING","OFF",
  "/REPORTING","N"
