USE [$(dbName)]
GO
SET NOCOUNT ON
---------------------------------------------------------------
--Inserts
---------------------------------------------------------------

-- processing
:r $(rootPath)"\Inserts\MapTypes.sql"
PRINT 'Maps'
GO
:r $(rootPath)"\Inserts\Maps.sql"
PRINT 'MapRegions'
GO
:r $(rootPath)"\Inserts\MapRegions.sql"

:r $(rootPath)"\Updates\MapRegions.sql"

PRINT 'InterventionCategories'
GO

SET NOCOUNT OFF
