SET QUOTED_IDENTIFIER ON
GO

PRINT '------ Creating Eumis'
:setvar rootPath ".\Create"
:r $(rootPath)"\CreateDB.sql"
:r $(rootPath)"\CreatePartitionFunction.sql"
:r $(rootPath)"\CreatePartitionScheme.sql"
:r $(rootPath)"\Create.sql"
:r $(rootPath)"\InsertSystemData.sql"
:r $(rootPath)"\InsertTestData.sql"
