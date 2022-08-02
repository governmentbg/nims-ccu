--executed on 2019/11/14 ~ 11:00

GO
UPDATE [dbo].[ContractReportCorrections] SET
    [Sign] = [Sign] * (-1)
FROM (VALUES 
    ('14'), ('19'),  ('620')) AS ListData(RegNumber)
WHERE
    ContractReportCorrections.ProgrammeId = 5
    AND ContractReportCorrections.Type = 1
    AND ContractReportCorrections.RegNumber = ListData.RegNumber
GO
