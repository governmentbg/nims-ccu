GO

CREATE TABLE [dbo].[ContractIndicators] (
    [ContractIndicatorId]       INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [IndicatorId]               INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,
    [IsActive]                  BIT                 NOT NULL,

    [BaseTotalValue]            DECIMAL(15,3)      NULL,
    [BaseMenValue]              DECIMAL(15,3)      NULL,
    [BaseWomenValue]            DECIMAL(15,3)      NULL,
    [TargetTotalValue]          DECIMAL(15,3)      NULL,
    [TargetMenValue]            DECIMAL(15,3)      NULL,
    [TargetWomenValue]          DECIMAL(15,3)      NULL,
    [Description]               NVARCHAR(MAX)      NULL,

    CONSTRAINT [PK_ContractIndicators]                         PRIMARY KEY ([ContractIndicatorId]),
    CONSTRAINT [FK_ContractIndicators_Contracts]               FOREIGN KEY ([ContractId])               REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractIndicators_Countries_Indicators]    FOREIGN KEY ([IndicatorId])              REFERENCES [dbo].[Indicators] ([IndicatorId])
);
GO

WITH XMLNAMESPACES (
    N'http://ereg.egov.bg/segment/R-10040' as i,
    N'http://ereg.egov.bg/segment/R-10038' as si,
    N'http://ereg.egov.bg/segment/R-10038' as v
),
ContractIndicatorValues as
(
    SELECT
        c.ContractId,
        ContractIndicatorGid = x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/@gid)[1]', 'NVARCHAR(MAX)'),
        IndicatorGid = x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/si:SelectedIndicator/si:Id/text())[1]', 'NVARCHAR(MAX)'),
        IsActive =
            CASE x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/@isActive)[1]', 'NVARCHAR(MAX)')
                WHEN 'true' THEN 1
                ELSE 0
            END,
        BaseMen = x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/v:BaseMen/text())[1]', 'DECIMAL(15,3)'),
        BaseWomen = x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/v:BaseWomen/text())[1]', 'DECIMAL(15,3)'),
        BaseTotal = x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/v:BaseTotal/text())[1]', 'DECIMAL(15,3)'),
        TargetMen = x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/v:TargetMen/text())[1]', 'DECIMAL(15,3)'),
        TargetWomen = x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/v:TargetWomen/text())[1]', 'DECIMAL(15,3)'),
        TargetTotal = x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/v:TargetTotal/text())[1]', 'DECIMAL(15,3)'),
        Description = x.Xml.value('(/BFPContract/i:BFPContractIndicators/i:BFPContractIndicator/v:Description/text())[1]', 'NVARCHAR(MAX)')
    FROM ContractVersionXmls x
    JOIN Contracts c ON x.ContractId = c.ContractId
    WHERE c.ContractStatus = 2 and x.Status = 3
)
INSERT INTO ContractIndicators
SELECT
    ContractId,
    IndicatorId,
    Gid = ContractIndicatorGid,
    IsActive,
    BaseTotalValue = BaseTotal,
    BaseMenValue = BaseMen,
    BaseWomenValue = BaseWomen,
    TargetTotalValue = TargetTotal,
    TargetMenValue = TargetMen,
    TargetWomenValue = TargetWomen,
    Description
FROM ContractIndicatorValues civ
JOIN Indicators i ON civ.IndicatorGid = i.Gid

GO
