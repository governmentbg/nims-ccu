--executed on 2020/03/13 ~ 08:00

GO
INSERT INTO UserPermissions (UserId, Permission, PermissionType, ProgrammeId)
SELECT
    up.UserId, 'CanMonitorCheckSheet', up.PermissionType, up.ProgrammeId 
FROM UserPermissions up
WHERE
    PermissionType IN ('Certification', 'Contract', 'IrregularitySignal', 'MonitoringFinancialControl','OperationalMap','Procedure','SpotCheck')
    AND (Permission = 'CanWrite' OR Permission = 'CanWriteFinancial')
GO
