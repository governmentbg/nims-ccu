GO
ALTER TABLE [dbo].[SapFiles] ADD [Type] int NULL
GO

GO
UPDATE [dbo].[SapFiles] SET [Type] = 1 WHERE [Type] IS NULL
GO

GO
ALTER TABLE [dbo].[SapFiles] ALTER COLUMN [Type] int NOT NULL
GO

GO
ALTER TABLE [dbo].[SapFiles] WITH CHECK ADD CONSTRAINT [CHK_SapFiles_Type] CHECK ([Type] IN (1, 2))
GO

GO
ALTER TABLE [dbo].[SapSchemas] ADD [Type] int NULL
GO

GO
UPDATE [dbo].[SapSchemas] SET [Type] = 1 WHERE [Type] IS NULL
GO

GO
ALTER TABLE [dbo].[SapSchemas] ALTER COLUMN [Type] int NOT NULL
GO

GO
ALTER TABLE [dbo].[SapSchemas] WITH CHECK ADD CONSTRAINT [CHK_SapSchemas_Type] CHECK ([Type] IN (1, 2))
GO

GO

CREATE TABLE [dbo].[SapDistributedLimits] (
    [SapDistributedLimitId]     INT              NOT NULL IDENTITY,
    [SapFileId]                 INT              NOT NULL,
    [IsImported]                BIT              NOT NULL,
    [ActuallyPaidAmountId]      INT              NULL,

    [ProgrammeId]               INT              NULL,
    [ProgrammePriorityId]       INT              NULL,
    [ContractSapNum]            NVARCHAR(50)     NULL,
    [ContractId]                INT              NULL,
    [Fund]                      INT              NULL,
    [ContractReportPaymentNum]  NVARCHAR(50)     NULL,
    [ContractReportPaymentId]   INT              NULL,
    [ContractReportPaymentDate] DATETIME2        NULL,
    [PaidBfpBgAmount]           MONEY            NOT NULL,
    [PaidBfpEuAmount]           MONEY            NOT NULL,
    [Currency]                  INT              NULL,
    [PaymentType]               INT              NULL,
    [AccDate]                   DATETIME2        NULL,
    [BankDate]                  DATETIME2        NULL,
    [SapDate]                   DATETIME2        NULL,
    [Comment]                   NVARCHAR(MAX)    NULL,
    [StornoCode]                NVARCHAR(50)     NULL,
    [StornoDescr]               NVARCHAR(MAX)    NULL,

    [HasWarning]                BIT              NOT NULL,
    [Warnings]                  NVARCHAR(MAX)    NULL,

    [HasError]                  BIT              NOT NULL,
    [Errors]                    NVARCHAR(MAX)    NULL,

    CONSTRAINT [PK_SapDistributedLimits]                        PRIMARY KEY ([SapDistributedLimitId]),
    CONSTRAINT [FK_SapDistributedLimits_SapFiles]               FOREIGN KEY ([SapFileId])               REFERENCES [dbo].[SapFiles]               ([SapFileId]),
    CONSTRAINT [FK_SapDistributedLimits_ActuallyPaidAmounts]    FOREIGN KEY ([ActuallyPaidAmountId])    REFERENCES [dbo].[ActuallyPaidAmounts]    ([ActuallyPaidAmountId]),
    CONSTRAINT [FK_SapDistributedLimits_Programmes]             FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]               ([MapNodeId]),
    CONSTRAINT [FK_SapDistributedLimits_ProgrammePriorities]    FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]               ([MapNodeId]),
    CONSTRAINT [FK_SapDistributedLimits_Contracts]              FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]              ([ContractId]),
    CONSTRAINT [FK_SapDistributedLimits_ContractReportPayments] FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [CHK_SapDistributedLimits_Fund]                  CHECK       ([Fund]          IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)),
    CONSTRAINT [CHK_SapDistributedLimits_Currency]              CHECK       ([Currency]      IN (1)),
    CONSTRAINT [CHK_SapDistributedLimits_PaymentType]           CHECK       ([PaymentType]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
);
GO

ALTER TABLE [dbo].[SapSchemas] DROP CONSTRAINT [UQ_SapSchemas_SingleIsActive]
GO

SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[SapSchemas] ADD  CONSTRAINT [UQ_SapSchemas_SingleTypeIsActive] UNIQUE NONCLUSTERED 
(
    [IsActiveCheck] ASC, 
    [Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO
INSERT INTO [SapSchemas]
    ([Content], [IsActive], [CreateDate], [ModifyDate], [Type])
VALUES
    (N'<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" attributeFormDefault="unqualified" elementFormDefault="qualified"><xs:element name="SAPImport"><xs:complexType><xs:sequence><xs:element name="SapKey" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="Date" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="Time" type="xs:time" minOccurs="1" maxOccurs="1" /><xs:element name="SapUser" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="Contract" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="ContractSapNum" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="EuFund" type="_EuFund" minOccurs="1" maxOccurs="1" /><xs:element name="ReqPayment" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="ReqPaymentNum" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="ReqPaymentDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="ContractPayment" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="FinanceSource" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="PayedAmount" type="xs:double" minOccurs="1" maxOccurs="1" /><xs:element name="Currency" type="_Currency" minOccurs="1" maxOccurs="1" /><xs:element name="PaymentType" type="_PaymentType" minOccurs="0" maxOccurs="1" /><xs:element name="AccDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="BankDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="SAPDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="Comment" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="StornoCode" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="StornoDescr" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field3" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field4" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field5" type="xs:string" minOccurs="0" maxOccurs="1" /></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element><xs:simpleType name="_EuFund"><xs:restriction base="xs:string"><xs:pattern value="([A-Z0-9])+" /></xs:restriction></xs:simpleType><xs:simpleType name="_PaymentType"><xs:restriction base="xs:string"><xs:enumeration value="" /><xs:enumeration value="авансово" /><xs:enumeration value="междинно" /><xs:enumeration value="окончателно" /><xs:enumeration value="глоба" /><xs:enumeration value="лихва" /><xs:enumeration value="възстановяване при доброволно прекратяване" /><xs:enumeration value="възстановяване при грешка" /><xs:enumeration value="възстановяване при нередност" /><xs:enumeration value="банкова гаранция" /><xs:enumeration value="касов трансфер" /></xs:restriction></xs:simpleType><xs:simpleType name="_Currency"><xs:restriction base="xs:string"><xs:enumeration value="BGN" /><xs:enumeration value="EUR" /></xs:restriction></xs:simpleType></xs:schema>' , 1, GETDATE(), GETDATE(), 2);
GO
