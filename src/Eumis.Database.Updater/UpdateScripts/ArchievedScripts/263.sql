DROP INDEX [UQ_Irregularities_Number]
ON [Irregularities];
GO

CREATE UNIQUE INDEX [UQ_Irregularities_Number]
ON [Irregularities]([RegNumber])
WHERE [Status] = 2;
GO
