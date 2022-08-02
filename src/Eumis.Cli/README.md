
Eumis.Cli
=====

### Use the `changelog` command to return all cards between two labels in the repository

1. Create a personal access token for authenticating to GitHub:
 - In the upper-right corner of any GitHub page, click your profile photo, then click Settings
 - In the left sidebar, click Developer settings
 - In the left sidebar, click Personal access tokens
 - Click Generate new token
 - Give your token a descriptive name
 - To use your token to access repositories from the command line, select scope `repo`
 - Click Generate token
 - Copy the token to your clipboard. For security reasons, after you navigate off the page, you will not be able to see the token again

2. Create developer API key and token for authenticating to Trello:
 - You can get your API key by logging into Trello and visiting https://trello.com/app-key.
 - Click on the hyperlinked "Token"
 - Click on the button "Allow"

3. Set the following environment variables:
 * `EUMIS_CLI_GIT_REPOPATH` The path to the directory of your local Eumis repository
 * `EUMIS_CLI_GIT_ACCESSTOKEN` A personal access token for authenticating to GitHub
 * `EUMIS_CLI_TRELLO_KEY` A developer API key for Trello
 * `EUMIS_CLI_TRELLO_TOKEN` A personal token for authenticating to Trello

4. Build the Eumis.Cli project, open the build directory and run `Eumis.Cli.exe changelog <old commit> <new commit>`

### Use the `report` command to return all cards in list

If you already have the necessary environment variables for the previous command (`EUMIS_CLI_TRELLO_KEY` and `EUMIS_CLI_TRELLO_TOKEN`), skip point 1. and point 2.

1. Create developer API key and token for authenticating to Trello (like in `changelog` command):
 - You can get your API key by logging into Trello and visiting https://trello.com/app-key.
 - Click on the hyperlinked "Token"
 - Click on the button "Allow"

2. Set the following environment variables:
 * `EUMIS_CLI_TRELLO_KEY` A developer API key for Trello
 * `EUMIS_CLI_TRELLO_TOKEN` A personal token for authenticating to Trello

3. Build the Eumis.Cli project, open the build directory and run `Eumis.Cli.exe report <list name> <path to report xml file>`
