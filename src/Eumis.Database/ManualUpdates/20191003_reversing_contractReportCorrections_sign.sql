--executed on 2019/10/03 ~ 10:00

GO
UPDATE [dbo].[ContractReportCorrections] SET
    [Sign] = [Sign] * (-1)
FROM (VALUES ('9')) AS ListData(RegNumber)
WHERE
    ContractReportCorrections.ProgrammeId = 2
    AND ContractReportCorrections.Type = 1
    AND ContractReportCorrections.RegNumber = ListData.RegNumber
GO
