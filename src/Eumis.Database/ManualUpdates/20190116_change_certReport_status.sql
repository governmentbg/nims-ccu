--executed on 2019/01/16 ~ 15:25

--Current values
--CertReportId=343; Status=4;
--CertReportId=345; Status=4;
--CertReportId=337; Status=5;

BEGIN TRANSACTION;
GO

UPDATE dbo.CertReports
SET 
    [Status] = 1,
    ApprovalDate = NULL
WHERE
    ProgrammeId = 5
    AND OrderNum = 16
    AND OrderVersionNum = 1
    AND CertReportNumber = '4/7/08.06.2018';
GO

UPDATE dbo.CertReports
SET 
    [Status] = 1,
    ApprovalDate = NULL
WHERE
    ProgrammeId = 8010402
    AND OrderNum = 3
    AND OrderVersionNum = 1
    AND CertReportNumber = '4/1/08.06.2018';
GO

UPDATE dbo.CertReports
SET 
    [Status] = 1,
    ApprovalDate = NULL
WHERE
    ProgrammeId = 3
    AND OrderNum = 13
    AND OrderVersionNum = 2
    AND CertReportNumber = '4/7/25.06.2018';
GO

COMMIT TRANSACTION;
GO
