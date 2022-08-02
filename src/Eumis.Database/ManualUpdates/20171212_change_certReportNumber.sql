UPDATE CertReports SET  
    CertReportNumber = ListData.NewNumber
FROM (VALUES
    (4, 15, 1, '4/1/14.09.2017', '4/2/14.09.2017')
) AS ListData(MapNodeId, OrderNum, OrderVersionNum, OldNumber, NewNumber) 
WHERE 
    ListData.OrderNum = CertReports.OrderNum
    AND ListData.OrderVersionNum = CertReports.OrderVersionNum
    AND ListData.MapNodeId = CertReports.ProgrammeId
    AND ListData.OldNumber = CertReports.CertReportNumber
    

    