GO

CREATE TABLE [#XmlData](
    [FileBlobKey]          NVARCHAR(50),
    [VersionNum]           INT,
    [VersionSubNum]        INT,
    [ActivationDate]       DATE,
    [XmlText]              XML);

WITH
    XMLNAMESPACES (DEFAULT N'http://ereg.egov.bg/segment/R-10018')
INSERT INTO [#XmlData] ([FileBlobKey], [VersionNum], [VersionSubNum], [ActivationDate], [XmlText])
SELECT
    [BlobKey]              AS [FileBlobKey],
    [OrderNum]             AS [VersionNum],
    NULL                   AS [VersionSubNum],
    [ModifyDate]           AS [ActivationDate],
    (SELECT [OrderNum] AS [VersionNum], CAST([ModifyDate] AS DATE) AS [ActivationDate] FOR XML PATH ('')) AS [XmlText]
FROM (SELECT cpxmlf.[BlobKey], cpxmlf.[Type], [OrderNum], [ModifyDate], ROW_NUMBER() OVER(PARTITION BY [BlobKey] ORDER BY [OrderNum] ASC) AS rn
        FROM [dbo].[ContractProcurementXmlFiles]                   AS cpxmlf
        JOIN [dbo].[ContractProcurementXmls]                       AS cpxml      ON cpxmlf.[ContractProcurementXmlId] = cpxml.[ContractProcurementXmlId]) AS T
WHERE rn = 1

UNION ALL

SELECT
    [BlobKey]              AS [FileBlobKey],
    [OrderNum]             AS [VersionNum],
    NULL                   AS [VersionSubNum],
    [ModifyDate]           AS [ActivationDate],
    (SELECT [OrderNum] AS [VersionNum], CAST([ModifyDate] AS DATE) AS [ActivationDate] FOR XML PATH ('')) AS [XmlText]
FROM (SELECT cpxmlf.[BlobKey], [OrderNum], [ModifyDate], ROW_NUMBER() OVER(PARTITION BY [BlobKey] ORDER BY [OrderNum] ASC) AS rn
        FROM [dbo].[ContractProcurementPlanAdditionalDocuments]    AS cpxmlf
        JOIN [dbo].[ContractProcurementPlans]                      AS cpp        ON cpxmlf.[ContractProcurementPlanId] = cpp.[ContractProcurementPlanId]
        JOIN [dbo].[ContractProcurementXmls]                       AS cpxml      ON cpp.[ContractId] = cpxml.[ContractId])                                 AS T
WHERE rn = 1
GO

DECLARE @XmlId INT,
        @XMLData XML,
        @NewXML XML

DECLARE xml_cursor CURSOR FOR
SELECT [ContractProcurementXmlId], [Xml] FROM [dbo].[ContractProcurementXmls]

OPEN xml_cursor
FETCH NEXT FROM xml_cursor INTO @XmlId, @XMLData
WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @seed INT = 0
    --Contract contractor / attached documents
    SET @seed = 0
    WHILE @seed < @XMLData.value('
            declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
            declare namespace R10047 = "http://ereg.egov.bg/segment/R-10047";
            count(/Procurements/R10041:ContractContractors/*:ContractContractor/R10047:AttachedDocument)', 'int')
    BEGIN
        SET @seed = @seed + 1;
        SET @NewXML = '';

        SELECT @NewXML = XmlText
        FROM #XmlData xdata
        WHERE
             [FileBlobKey] = (SELECT @XMLData.value('
                                declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
                                declare namespace R10047 = "http://ereg.egov.bg/segment/R-10047";
                                declare namespace R10018 = "http://ereg.egov.bg/segment/R-10018";
                                declare namespace R09992 = "http://ereg.egov.bg/segment/R-09992";
                                (/Procurements/R10041:ContractContractors/*:ContractContractor/R10047:AttachedDocument/R10018:AttachedDocumentContent/R09992:BlobContentId/text())[sql:variable("@seed")][1]', 'nvarchar(50)'))

        SET @XMLData.modify('
                declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
                declare namespace R10047 = "http://ereg.egov.bg/segment/R-10047";
                insert sql:variable("@NewXML") as first
                into (/Procurements/R10041:ContractContractors/*:ContractContractor/R10047:AttachedDocument)[sql:variable("@seed")][1]')
    END

    --Procurement plan / attached documents
    SET @seed = 0
    WHILE @seed < @XMLData.value('
            declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
            declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
            count(/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/R10048:AttachedDocument)', 'int')
    BEGIN
        SET @seed = @seed + 1;
        SET @NewXML = '';

        SELECT @NewXML = XmlText
        FROM #XmlData xdata
        WHERE
             [FileBlobKey] = (SELECT @XMLData.value('
                                declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
                                declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
                                declare namespace R10018 = "http://ereg.egov.bg/segment/R-10018";
                                declare namespace R09992 = "http://ereg.egov.bg/segment/R-09992";
                                (/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/R10048:AttachedDocument/R10018:AttachedDocumentContent/R09992:BlobContentId/text())[sql:variable("@seed")][1]', 'nvarchar(50)'))

        SET @XMLData.modify('
                declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
                declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
                insert sql:variable("@NewXML") as first
                into (/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/R10048:AttachedDocument)[sql:variable("@seed")][1]')
    END

    --Procurement plan / public attached documents
    SET @seed = 0
    WHILE @seed < @XMLData.value('
            declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
            declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
            count(/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/R10048:PublicAttachedDocument)', 'int')
    BEGIN
        SET @seed = @seed + 1;
        SET @NewXML = '';

        SELECT @NewXML = XmlText
        FROM #XmlData xdata
        WHERE
             [FileBlobKey] = (SELECT @XMLData.value('
                                declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
                                declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
                                declare namespace R10018 = "http://ereg.egov.bg/segment/R-10018";
                                declare namespace R09992 = "http://ereg.egov.bg/segment/R-09992";
                                (/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/R10048:PublicAttachedDocument/R10018:AttachedDocumentContent/R09992:BlobContentId/text())[sql:variable("@seed")][1]', 'nvarchar(50)'))

        SET @XMLData.modify('
                declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
                declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
                insert sql:variable("@NewXML") as first
                into (/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/R10048:PublicAttachedDocument)[sql:variable("@seed")][1]')
    END

    --Procurement plan / additional attached documents
    SET @seed = 0
    WHILE @seed < @XMLData.value('
            declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
            declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
            count(/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/R10048:AdditionalAttachedDocument)', 'int')
    BEGIN
        SET @seed = @seed + 1;
        SET @NewXML = '';

        SELECT @NewXML = XmlText
        FROM #XmlData xdata
        WHERE
             [FileBlobKey] = (SELECT @XMLData.value('
                                declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
                                declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
                                declare namespace R10018 = "http://ereg.egov.bg/segment/R-10018";
                                declare namespace R09992 = "http://ereg.egov.bg/segment/R-09992";
                                (/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/R10048:AdditionalAttachedDocument/R10018:AttachedDocumentContent/R09992:BlobContentId/text())[sql:variable("@seed")][1]', 'nvarchar(50)'))

        SET @XMLData.modify('
                declare namespace R10041 = "http://ereg.egov.bg/segment/R-10041";
                declare namespace R10048 = "http://ereg.egov.bg/segment/R-10048";
                insert sql:variable("@NewXML") as first
                into (/Procurements/R10041:ProcurementPlans/*:ProcurementPlan/R10048:AdditionalAttachedDocument)[sql:variable("@seed")][1]')
    END

    UPDATE ContractProcurementXmls SET Xml = @XMLData WHERE ContractProcurementXmlId = @XmlId
    FETCH NEXT FROM xml_cursor INTO @XmlId, @XMLData
END

CLOSE xml_cursor;
DEALLOCATE xml_cursor;

UPDATE [dbo].[ContractProcurementXmls] SET
    Xml.modify('insert
                attribute orderNum {sql:column("OrderNum")}
                into (/Procurements)[1]')

GO

TRUNCATE TABLE [#XmlData];
GO

--Financial report
WITH
    XMLNAMESPACES (DEFAULT N'http://ereg.egov.bg/segment/R-10018')
INSERT INTO [#XmlData] ([FileBlobKey], [VersionNum], [VersionSubNum], [ActivationDate], [XmlText])
SELECT
    [BlobKey]              AS [FileBlobKey],
    [VersionNum]           AS [VersionNum],
    [VersionSubNum]        AS [VersionSubNum],
    [ModifyDate]           AS [ActivationDate],
    (SELECT [VersionNum] AS [VersionNum], [VersionSubNum] AS [VersionSubNum], CAST([ModifyDate] AS DATE) AS [ActivationDate] FOR XML PATH ('')) AS [XmlText]
FROM (SELECT crff.[BlobKey], crf.[VersionNum], crf.[VersionSubNum], crf.[ModifyDate], ROW_NUMBER() OVER(PARTITION BY [BlobKey] ORDER BY [VersionNum], [VersionSubNum] ASC ) AS rn
        FROM [dbo].[ContractReportFinancialXmlFiles]                  AS crff
        JOIN [dbo].[ContractReportFinancials]                         AS crf      ON crff.[ContractReportFinancialId] = crf.[ContractReportFinancialId]) AS T
WHERE rn = 1
GO

DECLARE @XmlId INT,
        @XMLData XML,
        @NewXML XML

DECLARE xml_cursor CURSOR FOR
SELECT [ContractReportFinancialId], [Xml] FROM [dbo].[ContractReportFinancials]

OPEN xml_cursor
FETCH NEXT FROM xml_cursor INTO @XmlId, @XMLData
WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @seed INT = 0
    WHILE @seed < @XMLData.value('
            declare namespace R10043 = "http://ereg.egov.bg/segment/R-10043";
            declare namespace R10066 = "http://ereg.egov.bg/segment/R-10066";
            count(/FinanceReport/R10043:CostSupportingDocuments/*:CostSupportingDocument/R10066:AttachedDocument)', 'int')
    BEGIN
        SET @seed = @seed + 1;
        SET @NewXML = '';

        SELECT @NewXML = XmlText
        FROM #XmlData xdata
        WHERE
             [FileBlobKey] = (SELECT @XMLData.value('
                                declare namespace R10043 = "http://ereg.egov.bg/segment/R-10043";
                                declare namespace R10066 = "http://ereg.egov.bg/segment/R-10066";
                                declare namespace R10018 = "http://ereg.egov.bg/segment/R-10018";
                                declare namespace R09992 = "http://ereg.egov.bg/segment/R-09992";
                                (/FinanceReport/R10043:CostSupportingDocuments/*:CostSupportingDocument/R10066:AttachedDocument/R10018:AttachedDocumentContent/R09992:BlobContentId/text())[sql:variable("@seed")][1]', 'nvarchar(50)'))

        SET @XMLData.modify('
                declare namespace R10043 = "http://ereg.egov.bg/segment/R-10043";
                declare namespace R10066 = "http://ereg.egov.bg/segment/R-10066";
                insert sql:variable("@NewXML") as first
                into (/FinanceReport/R10043:CostSupportingDocuments/*:CostSupportingDocument/R10066:AttachedDocument)[sql:variable("@seed")][1]')
    END

    UPDATE [dbo].[ContractReportFinancials] SET Xml = @XMLData WHERE [ContractReportFinancialId] = @XmlId
    FETCH NEXT FROM xml_cursor INTO @XmlId, @XMLData
END

CLOSE xml_cursor;
DEALLOCATE xml_cursor;

UPDATE [dbo].[ContractReportFinancials] SET
    Xml.modify('insert
                attribute docSubNumber {sql:column("VersionSubNum")}
                into (/FinanceReport)[1]')
GO

TRUNCATE TABLE [#XmlData];
GO

--Payment request
WITH
    XMLNAMESPACES (DEFAULT N'http://ereg.egov.bg/segment/R-10018')
INSERT INTO [#XmlData] ([FileBlobKey], [VersionNum], [VersionSubNum], [ActivationDate], [XmlText])
SELECT
    [BlobKey]              AS [FileBlobKey],
    [VersionNum]           AS [VersionNum],
    [VersionSubNum]        AS [VersionSubNum],
    [ModifyDate]           AS [ActivationDate],
    (SELECT [VersionNum] AS [VersionNum], [VersionSubNum] AS [VersionSubNum], CAST([ModifyDate] AS DATE) AS [ActivationDate] FOR XML PATH ('')) AS [XmlText]
FROM (SELECT crpf.[BlobKey], crp.[VersionNum], crp.[VersionSubNum], crp.[ModifyDate], ROW_NUMBER() OVER(PARTITION BY [BlobKey] ORDER BY [VersionNum], [VersionSubNum] ASC ) AS rn
        FROM [dbo].[ContractReportPaymentXmlFiles]                    AS crpf
        JOIN [dbo].[ContractReportPayments]                           AS crp      ON crpf.[ContractReportPaymentId] = crp.[ContractReportPaymentId]) AS T
WHERE rn = 1
GO

DECLARE @XmlId INT,
        @XMLData XML,
        @NewXML XML

DECLARE xml_cursor CURSOR FOR
SELECT [ContractReportPaymentId], [Xml] FROM [dbo].[ContractReportPayments]

OPEN xml_cursor
FETCH NEXT FROM xml_cursor INTO @XmlId, @XMLData
WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @seed INT = 0
    WHILE @seed < @XMLData.value('
            declare namespace R10045 = "http://ereg.egov.bg/segment/R-10045";
            count(/PaymentRequest/R10045:AttachedDocuments/*:AttachedDocument)', 'int')
    BEGIN
        SET @seed = @seed + 1;
        SET @NewXML = '';

        SELECT @NewXML = XmlText
        FROM #XmlData xdata
        WHERE
             [FileBlobKey] = (SELECT @XMLData.value('
                                declare namespace R10045 = "http://ereg.egov.bg/segment/R-10045";
                                declare namespace R10018 = "http://ereg.egov.bg/segment/R-10018";
                                declare namespace R09992 = "http://ereg.egov.bg/segment/R-09992";
                                (/PaymentRequest/R10045:AttachedDocuments/*:AttachedDocument/R10018:AttachedDocumentContent/R09992:BlobContentId/text())[sql:variable("@seed")][1]', 'nvarchar(50)'))

        SET @XMLData.modify('
                declare namespace R10045 = "http://ereg.egov.bg/segment/R-10045";
                insert sql:variable("@NewXML") as first
                into (/PaymentRequest/R10045:AttachedDocuments/*:AttachedDocument)[sql:variable("@seed")][1]')
    END

    UPDATE [dbo].[ContractReportPayments] SET Xml = @XMLData WHERE [ContractReportPaymentId] = @XmlId
    FETCH NEXT FROM xml_cursor INTO @XmlId, @XMLData
END

CLOSE xml_cursor;
DEALLOCATE xml_cursor;

UPDATE [dbo].[ContractReportPayments] SET
    Xml.modify('insert
                attribute docSubNumber {sql:column("VersionSubNum")}
                into (/PaymentRequest)[1]')
GO

TRUNCATE TABLE [#XmlData];
GO

--Technical report
WITH
    XMLNAMESPACES (DEFAULT N'http://ereg.egov.bg/segment/R-10018')
INSERT INTO [#XmlData] ([FileBlobKey], [VersionNum], [VersionSubNum], [ActivationDate], [XmlText])
SELECT
    [BlobKey]              AS [FileBlobKey],
    [VersionNum]           AS [VersionNum],
    [VersionSubNum]        AS [VersionSubNum],
    [ModifyDate]           AS [ActivationDate],
    (SELECT [VersionNum] AS [VersionNum], [VersionSubNum] AS [VersionSubNum], CAST([ModifyDate] AS DATE) AS [ActivationDate] FOR XML PATH ('')) AS [XmlText]
FROM (SELECT crtf.[BlobKey], crt.[VersionNum], crt.[VersionSubNum], crt.[ModifyDate] , ROW_NUMBER() OVER(PARTITION BY [BlobKey] ORDER BY [VersionNum], [VersionSubNum] ASC ) AS rn
          FROM [dbo].[ContractReportTechnicalXmlFiles]                 AS crtf
          JOIN [dbo].[ContractReportTechnicals]                        AS crt      ON crtf.[ContractReportTechnicalId] = crt.[ContractReportTechnicalId]) AS T
WHERE rn = 1

GO

DECLARE @XmlId INT,
        @XMLData XML,
        @NewXML XML

DECLARE xml_cursor CURSOR FOR
SELECT [ContractReportTechnicalId], [Xml] FROM [dbo].[ContractReportTechnicals]

OPEN xml_cursor
FETCH NEXT FROM xml_cursor INTO @XmlId, @XMLData
WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @seed INT = 0
    WHILE @seed < @XMLData.value('
            declare namespace R10044 = "http://ereg.egov.bg/segment/R-10044";
            count(/TechnicalReport/R10044:AttachedDocuments/*:AttachedDocument)', 'int')
    BEGIN
        SET @seed = @seed + 1;
        SET @NewXML = '';

        SELECT @NewXML = XmlText
        FROM #XmlData xdata
        WHERE
             [FileBlobKey] = (SELECT @XMLData.value('
                                declare namespace R10044 = "http://ereg.egov.bg/segment/R-10044";
                                declare namespace R10018 = "http://ereg.egov.bg/segment/R-10018";
                                declare namespace R09992 = "http://ereg.egov.bg/segment/R-09992";
                                (/TechnicalReport/R10044:AttachedDocuments/*:AttachedDocument/R10018:AttachedDocumentContent/R09992:BlobContentId/text())[sql:variable("@seed")][1]', 'nvarchar(50)'))

        SET @XMLData.modify('
                declare namespace R10044 = "http://ereg.egov.bg/segment/R-10044";
                insert sql:variable("@NewXML") as first
                into (/TechnicalReport/R10044:AttachedDocuments/*:AttachedDocument)[sql:variable("@seed")][1]')
    END

    UPDATE [dbo].[ContractReportTechnicals] SET Xml = @XMLData WHERE [ContractReportTechnicalId] = @XmlId
    FETCH NEXT FROM xml_cursor INTO @XmlId, @XMLData
END

CLOSE xml_cursor;
DEALLOCATE xml_cursor;

UPDATE [dbo].[ContractReportTechnicals] SET
    Xml.modify('insert
                attribute docSubNumber {sql:column("VersionSubNum")}
                into (/TechnicalReport)[1]')
GO

DROP TABLE [#XmlData]
GO
