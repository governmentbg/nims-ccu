$EumisServerIp = Read-Host -Prompt 'Enter eumis server ip'
$EumisServerUser = Read-Host -Prompt 'Enter eumis server user'
$EumisServerPassword = Read-Host -Prompt 'Enter eumis server password'

sqlcmd -b -S. -i"Create.sql" -v `
  dbName="EumisBlobs" `
  eumisServerIp="$EumisServerIp" `
  eumisServerUser="$EumisServerUser" `
  eumisServerPassword="$EumisServerPassword"
