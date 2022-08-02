$UsedBlobsServerIp = Read-Host -Prompt 'Enter used blobs server ip'
$UsedBlobsServerUser = Read-Host -Prompt 'Enter used blobs server user'
$UsedBlobsServerPassword = Read-Host -Prompt 'Enter used blobs server password'

sqlcmd -b -S. -i"Create.sql" -v `
  usedBlobsServerIp="$UsedBlobsServerIp" `
  usedBlobsServerUser="$UsedBlobsServerUser" `
  usedBlobsServerPassword="$UsedBlobsServerPassword"

