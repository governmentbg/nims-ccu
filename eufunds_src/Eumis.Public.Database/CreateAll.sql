SET QUOTED_IDENTIFIER ON
GO

PRINT '------ Creating database'
:setvar rootPath "."
:r $(rootPath)"\Create\CreateEumisPublicDB.sql"

PRINT '------ Creating tables'
:r $(rootPath)"\Create\Create.sql"

PRINT '------ Inserting nomens'
:r $(rootPath)"\Inserts.sql"

