UPDATE CertReports SET  
    CertReportNumber = ListData.NewNumber
FROM (VALUES
    (2, 12, 1, '', '4/2/09.10.2017'),
    (6, 11, 1, '', '4/2/09.10.2017'),
    (5, 11, 1, '', '4/2/16.10.2017'),
    (3, 8, 1, '', '4/2/12.10.2017'),
    (3, 7, 1, '1/4/11.09.2017', '4/1/11.09.2017')
) AS ListData(MapNodeId, OrderNum, OrderVersionNum, OldNumber, NewNumber) 
WHERE 
    ListData.OrderNum = CertReports.OrderNum
    AND ListData.OrderVersionNum = CertReports.OrderVersionNum
    AND ListData.MapNodeId = CertReports.ProgrammeId
    AND ListData.OldNumber = CertReports.CertReportNumber
