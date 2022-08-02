param(
    [Parameter(Mandatory=$true)]
    [string]$serverName
)

cmdkey /generic:$serverName /user:$ENV:EUMIS_DEFAULT_USERNAME /pass: $ENV:EUMIS_DEFAULT_PASSWORD
mstsc /v:$serverName