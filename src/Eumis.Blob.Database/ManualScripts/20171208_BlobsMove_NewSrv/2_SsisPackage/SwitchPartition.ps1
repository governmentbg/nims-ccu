$Database = "EumisBlobs"

$PartitionId = Read-Host -Prompt 'Enter partition id'

sqlcmd -b -S. -i"SwitchPartition.sql" -v `
  dbName="$Database" `
  partitionId="$PartitionId"
