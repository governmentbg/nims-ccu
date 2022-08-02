param(
    [Parameter(Mandatory=$true)]
    [string]$ConfigFile,

    [Parameter(Mandatory=$true)]
    [hashtable]$ConnectionStrings
)

########### improve error handling ###########
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues['*:ErrorAction']='Stop'
##############################################

$file = Get-Content $ConfigFile
$xml = [XML]$file

foreach ($key in $ConnectionStrings.Keys)
{
    $attr = $xml.SelectSingleNode("/configuration/connectionStrings/add[@name='$key']/@connectionString")

    if (!$attr)
    {
        throw "Could not find connectionString '$key'"
    }

    $attr.Value = $ConnectionStrings[$key]
}

$xml.OuterXml | Out-File $ConfigFile -Encoding UTF8
