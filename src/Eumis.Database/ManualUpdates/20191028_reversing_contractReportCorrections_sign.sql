--executed on 2019/10/28 ~ 10:00

GO
UPDATE [dbo].[ContractReportCorrections] SET
    [Sign] = [Sign] * (-1)
FROM (VALUES 
    ('1'), ('2'),  ('3'),  ('4'),  ('5'),  ('6'),
    ('7'),  ('8'),  ('10'),  ('11'), ('14'), ('15'),
    ('16'),  ('17'),  ('18'),  ('19')) AS ListData(RegNumber)
WHERE
    ContractReportCorrections.ProgrammeId = 2
    AND ContractReportCorrections.Type = 1
    AND ContractReportCorrections.RegNumber = ListData.RegNumber
GO
