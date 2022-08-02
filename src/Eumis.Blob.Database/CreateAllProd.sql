SET QUOTED_IDENTIFIER ON
GO

PRINT '------ Creating EumisBlobs'
:setvar rootPath ".\Create"
:r $(rootPath)"\CreateDBProd.sql"
:r $(rootPath)"\CreatePartitionFunction.sql"
:r $(rootPath)"\CreatePartitionSchemeProd.sql"
:r $(rootPath)"\Create.sql"
