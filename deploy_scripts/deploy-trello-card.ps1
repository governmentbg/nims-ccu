param(
    [Parameter(Mandatory = $true)]
    [string]$Environment
)

function New-DeploymentCard {
    param(
        [string] $listId,
        [string] $commitMessages)

    #$currenGitBranch = &git branch --show-current
    $currentGitCommit = &git rev-parse HEAD
    $gitData = &git log -1
    $description = "";
    foreach ($row in $gitData) {
        $description = $description + "$row `r`n"    
    }

    $description = $description + "https://github.com/hristo-ciela/Eumis/commit/$currentGitCommit"
    if($commitMessages)
    {
        $description = $description + "`r`n **Current deploy contains following commits:**`r`n"
        $description = $description + $commitMessages
    }
    $card = New-TrelloCard `
        -ListId $listId `
        -MemberId $Env:EUMIS_DEPLOY_TRELLO_MEMBER_ID `
        -Description $description `
        -Name (Get-Date -Format "yyyyMMddHHmmss") 
     
    return $card
}

function Get-DeploymentListName {

    param (
        [string] $Environment
    )

    $currenGitBranch = &git branch --show-current
    
    if ($currenGitBranch -like "*suni*") {
        if ($Environment -like "EGP06.TEST") {
            return "Suni Test"
        }
        if ($Environment -like "EGP06.PROD") {
            return "Suni Production"
        }
        return "";
    }

    if ($Environment -like "EGP06.TEST") {
        return "Test 2021"
    }
    if ($Environment -like "EGP06.LEARN") {
        return "Learn 2021"
    }
    if ($Environment -like "EGP06.DRC") {
        return "DRC 2021"
    }
    if ($Environment -like "EGP06.PROD") {
        return "Production 2021"
    }
    return "";
}

function Get-LastDeploymentCard {
    param(
        [PSCustomObject] $trelloBoard,
        [PSCustomObject] $trelloList)

    #Get all cards in list
    $cards = Get-TrelloCard -Board $trelloBoard -List $trelloList
    
    #Get last card in list
    $lastCard = $cards[-1]

    $oldCommitHash = Get-GitCommitHash $lastCard
    if ($oldCommitHash) {
        $currentCommitHash = & git rev-parse HEAD
        $currentCommitHash = [string]$currentCommitHash    
        $gitCommits = Invoke-Expression "git rev-list --ancestry-path $oldCommitHash..$currentCommitHash"

        $messages = Get-GitCommitMessages $gitCommits $oldCommitHash
        return $messages
    }
    
        
}

function Get-GitCommitHash {
    param([PSCustomObject] $trelloCard)
    if ($trelloCard) {
        $oldDescription = ([string]$trelloCard.desc).Split("`n")
        if ($oldDescription.Count -gt 0) {
            if ($oldDescription[0].Contains("commit")) {
                $commitHash = $oldDescription[0].Replace("commit ", "").Trim();
                return $commitHash;
            }
        }
    }
}

function Get-GitCommitMessages {
    param([PSCustomObject] $gitCommitHashes)
    $description = ""
    if ($gitCommitHashes) {
        foreach ($commitHash in $gitCommitHashes) {
            $message = Invoke-Expression "git show -s --format=%B $commitHash"
            $description = $description + "`r`n- " + $message
        }
    }
    return $description
}

$allInstalledModules = Get-InstalledModule
$trelloInstalled = $false 
foreach ($psModule in $allInstalledModules) {
    if ($psModule.Name -eq "PowerTrello") {
        $trelloInstalled = $true
    }
}

if (-not $trelloInstalled) {
    Write-Warning "Powershell module PowerTrello not found. Skipping auto deployment card."
    Write-Host "The module is available at https://github.com/adbertram/PowerTrello"
    exit
}

$board = Get-TrelloBoard -Name Deployment

if (-not $board) {
    Write-Host "Board not found!"
    return
}

if ([string]::IsNullOrEmpty($Env:EUMIS_DEPLOY_TRELLO_MEMBER_ID)) {
    $members = &Get-TrelloBoardMember -BoardId $board.id
    Write-Warning "Environment variable EUMIS_DEPLOY_TRELLO_MEMBER_ID is not set"
    Write-Host $members 
    Exit
}

$deploymentListName = Get-DeploymentListName $Environment

if (-not $deploymentListName) {
    Write-Host "Deployment list not found. Card creation is canceled"
    return
}

$trelloLists = Get-TrelloList -BoardId $board.id

$deploymentListId = ""

foreach ($list in $trelloLists) {
    if ($list.name -eq $deploymentListName) {
        $deploymentListId = $list.id

        $commitMessages = Get-LastDeploymentCard $board $list
    }
}

if (-not $deploymentListId) {
    Write-Host -BackgroundColor Yellow -ForegroundColor Red "Deployment list name found as $deploymentListName but such list name is missing in Trello board: $board.name"
    return
}

$card = New-DeploymentCard $deploymentListId $commitMessages

if ($card) {
    Write-Host "Card created"
}
else {
    Write-Error "Failed to create a card"
}

return $card
