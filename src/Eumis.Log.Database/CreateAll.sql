SET QUOTED_IDENTIFIER ON
GO

PRINT '------ Creating EumisLogs'
:setvar rootPath ".\Create"
:r $(rootPath)"\CreateDB.sql"
:r $(rootPath)"\CreatePartitionFunction.sql"
:r $(rootPath)"\CreatePartitionScheme.sql"
:r $(rootPath)"\Create.sql"
