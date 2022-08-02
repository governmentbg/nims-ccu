param(
    [Parameter(Mandatory = $true)]
    [string]$Environment
)

if ([string]::IsNullOrEmpty($Env:EUMIS_CONFIG_REPO_PATH)) {
    Write-Error "Environment variable EUMIS_CONFIG_REPO_PATH is not set!"
    Exit
}

function Get-ScriptDirectory { Split-Path $script:MyInvocation.MyCommand.Path }

$coreDeploy = Join-Path (Get-ScriptDirectory) 'remote-deploy-core.ps1' 
$newTrelloCard = Join-Path (Get-ScriptDirectory) 'deploy-trello-card.ps1'

& $coreDeploy $Environment $Env:EUMIS_CONFIG_REPO_PATH

Write-Host "Do you want new card in Trello to be created? y\n" -ForegroundColor Yellow
$userAnswer = Read-Host
if (-not $userAnswer.ToLower().Equals("y")) {
    Exit;
}

$card = & $newTrelloCard $Environment

if ($card) {
    Write-Host $card.shortUrl
}

