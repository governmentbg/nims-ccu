$Database = "EumisBlobs"
$RemoveServerIp = "192.168.101.118"
$PackagePath = ".\BlobsUploadPackage.dtsx"

$RemoteServerUser = Read-Host -Prompt 'Enter remote server user'
$RemoteServerPassword = Read-Host -Prompt 'Enter remote server password'
$PartitionId = Read-Host -Prompt 'Enter partition id'

$PartitionInfo = Invoke-Sqlcmd -ServerInstance . -Database $Database -Query "SELECT * FROM $Database.dbo.vwBlobContentPartitions WHERE partition_number = `$PARTITION.pfBlobContents($PartitionId)"
If ($PartitionInfo.rows -gt 0) {
  Write-Error "Cannot run package. Partition is not empty."
  return
}

$RecoveryModel = (Invoke-Sqlcmd -ServerInstance . -Query "SELECT recovery_model_desc FROM sys.databases WHERE name = '$Database'").recovery_model_desc
If ($RecoveryModel -ne "SIMPLE") {
  Write-Error "Cannot run package. Database [$Database] is not in SIMPLE recovery mode."
  return
}

sqlcmd -b -S. -i"CreateSwitchTable.sql" -v `
  dbName="$Database" `
  partitionId="$PartitionId" `
  fileGroupName="$($PartitionInfo.filegroup)"

If ($LASTEXITCODE -ne 0) {
  return
}

&"dtexec" `
  "/FILE","$PackagePath",
  "/CONNECTION","Local;Data Source=.;Initial Catalog=$Database;Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;",
  "/CONNECTION","Log;Data Source=.;Initial Catalog=SsisLog;Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;",
  "/CONNECTION","Remote;Data Source=$RemoveServerIp;User ID=$RemoteServerUser;Password=$RemoteServerPassword;Initial Catalog=UsedBlobContents;Provider=SQLNCLI11.1;Auto Translate=False;",
  "/CHECKPOINTING","OFF",
  "/SET","\Package.Variables[User::PartitionId].Properties[Value];$PartitionId",
  "/REPORTING","N"
