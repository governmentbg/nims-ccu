SET QUOTED_IDENTIFIER ON
GO

PRINT '------ Creating Eumis'
:setvar rootPath ".\Create"
:r $(rootPath)"\CreateDB.sql"
:r $(rootPath)"\Create.sql"
