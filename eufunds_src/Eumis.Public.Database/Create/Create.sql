USE [$(dbName)]
GO

---------------------------------------------------------------
--Tables
---------------------------------------------------------------

:r $(rootPath)"\Create\Tables\MapTypes.sql"
:r $(rootPath)"\Create\Tables\Maps.sql"
:r $(rootPath)"\Create\Tables\MapRegions.sql"
:r $(rootPath)"\Create\Tables\OpStatOverrides.sql"


---------------------------------------------------------------
--Constraints
---------------------------------------------------------------

:r $(rootPath)"\Create\Constraints\Constraints.sql"