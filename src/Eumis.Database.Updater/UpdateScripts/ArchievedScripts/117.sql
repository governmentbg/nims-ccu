GO

-- adding ActuallyPaidAmountSequence
ALTER TABLE [dbo].[ActuallyPaidAmounts]
DROP CONSTRAINT [PK_ActuallyPaidAmounts]
GO

ALTER TABLE [dbo].[ActuallyPaidAmounts]
ADD [ActuallyPaidAmountIdNew] INT NOT NULL CONSTRAINT DEFAULT_ActuallyPaidAmounts DEFAULT 0;
GO

ALTER TABLE [dbo].[ActuallyPaidAmounts]
DROP CONSTRAINT DEFAULT_ActuallyPaidAmounts
GO

UPDATE [dbo].[ActuallyPaidAmounts]
SET [ActuallyPaidAmountIdNew] = [ActuallyPaidAmountId]
GO

ALTER TABLE [dbo].[ActuallyPaidAmounts] DROP COLUMN [ActuallyPaidAmountId];
GO

EXEC sp_rename '[ActuallyPaidAmounts].ActuallyPaidAmountIdNew', 'ActuallyPaidAmountId', 'COLUMN'
GO

ALTER TABLE [dbo].[ActuallyPaidAmounts]
ADD CONSTRAINT [PK_ActuallyPaidAmounts] PRIMARY KEY ([ActuallyPaidAmountId]);
GO

CREATE SEQUENCE [dbo].[ActuallyPaidAmountSequence] START WITH 1
GO

-- adding ReimbursedAmountSequence
ALTER TABLE [dbo].[ReimbursedAmounts]
DROP CONSTRAINT [PK_ReimbursedAmounts]
GO

ALTER TABLE [dbo].[ReimbursedAmounts]
ADD [ReimbursedAmountIdNew] INT NOT NULL CONSTRAINT DEFAULT_ReimbursedAmounts DEFAULT 0;
GO

ALTER TABLE [dbo].[ReimbursedAmounts]
DROP CONSTRAINT DEFAULT_ReimbursedAmounts
GO

UPDATE [dbo].[ReimbursedAmounts]
SET [ReimbursedAmountIdNew] = [ReimbursedAmountId]
GO

ALTER TABLE [dbo].[ReimbursedAmounts] DROP COLUMN [ReimbursedAmountId];
GO

EXEC sp_rename '[ReimbursedAmounts].ReimbursedAmountIdNew', 'ReimbursedAmountId', 'COLUMN'
GO

ALTER TABLE [dbo].[ReimbursedAmounts]
ADD CONSTRAINT [PK_ReimbursedAmounts] PRIMARY KEY ([ReimbursedAmountId]);
GO

CREATE SEQUENCE [dbo].[ReimbursedAmountSequence] START WITH 1
GO

-- sap Tables
CREATE TABLE [dbo].[SapSchemas] (
    [SapSchemaId]      INT               NOT NULL IDENTITY,
    [Content]          NVARCHAR(MAX)     NOT NULL,
    [IsActive]         BIT               NOT NULL,
    [CreateDate]       DATETIME2         NOT NULL,
    [ModifyDate]       DATETIME2         NOT NULL,
    [Version]          ROWVERSION        NOT NULL

    CONSTRAINT [PK_SapSchemas]          PRIMARY KEY ([SapSchemaId])
);
GO

SET IDENTITY_INSERT [SapSchemas] ON

INSERT INTO [SapSchemas]
    ([SapSchemaId], [Content], [IsActive], [CreateDate], [ModifyDate])
VALUES
    (1             , N'<?xml version="1.0" encoding="utf-8"?><xs:schema attributeFormDefault="unqualified" elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema"><xs:element name="SAPImport"><xs:complexType><xs:sequence><xs:element name="SapKey" type="xs:string" minOccurs="1" maxOccurs="1"/><xs:element name="Date" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="Time" type="xs:time" minOccurs="1" maxOccurs="1" /><xs:element name="SapUser" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="Contract" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="ContractSapNum" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="EuFund" type="_EuFund" minOccurs="1" maxOccurs="1"/><xs:element name="ReqPayment" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="ReqPaymentNum" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="ReqPaymentDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="ContractPayment" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="FinanceSource" type="_FinanceSource" minOccurs="1" maxOccurs="1" /><xs:element name="PayedAmount" type="xs:double" minOccurs="1" maxOccurs="1" /><xs:element name="Currency" type="_Currency" minOccurs="1" maxOccurs="1" /><xs:element name="PaymentType" type="_PaymentType" minOccurs="0" maxOccurs="1" /><xs:element name="AccDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="BankDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="SAPDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="Comment" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="StornoCode" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="StornoDescr" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field3" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field4" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field5" type="xs:string" minOccurs="0" maxOccurs="1" /></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element><xs:simpleType name="_EuFund"><xs:restriction base="xs:string"><xs:enumeration value="ESF" /><xs:enumeration value="ERDF" /><xs:enumeration value="CF" /></xs:restriction></xs:simpleType><xs:simpleType name="_PaymentType"><xs:restriction base="xs:string"><xs:enumeration value="" /><xs:enumeration value="авансово" /><xs:enumeration value="междинно" /><xs:enumeration value="окончателно" /><xs:enumeration value="глоба" /><xs:enumeration value="лихва" /><xs:enumeration value="възстановяване при доброволно прекратяване" /><xs:enumeration value="възстановяване при грешка" /><xs:enumeration value="възстановяване при нередност" /><xs:enumeration value="банкова гаранция" /><xs:enumeration value="касов трансфер" /></xs:restriction></xs:simpleType><xs:simpleType name="_Currency"><xs:restriction base="xs:string"><xs:enumeration value="BGN" /></xs:restriction></xs:simpleType><xs:simpleType name="_FinanceSource"><xs:restriction base="xs:string"><xs:enumeration value="BG" /><xs:enumeration value="EU" /></xs:restriction></xs:simpleType></xs:schema>' , 1, GETDATE(), GETDATE());

SET IDENTITY_INSERT [SapSchemas] OFF
GO

CREATE TABLE [dbo].[SapFiles] (
    [SapFileId]        INT               NOT NULL IDENTITY,
    [SapSchemaId]      INT               NOT NULL,
    [Status]           INT               NOT NULL,
    [FileKey]          UNIQUEIDENTIFIER  NOT NULL,
    [FileName]         NVARCHAR(500)     NOT NULL,
    [SapKey]           NVARCHAR(200)     NOT NULL,
    [SapDate]          DATETIME2         NOT NULL,
    [SapUser]          NVARCHAR(200)     NOT NULL,
    [Xml]              XML               NOT NULL,
    [CreatedByUserId]  INT               NOT NULL,
    [CreateDate]       DATETIME2         NOT NULL,
    [ModifyDate]       DATETIME2         NOT NULL,
    [Version]          ROWVERSION        NOT NULL

    CONSTRAINT [PK_SapFiles]            PRIMARY KEY ([SapFileId]),
    CONSTRAINT [FK_SapFiles_SapSchemas] FOREIGN KEY ([SapSchemaId])     REFERENCES [dbo].[SapSchemas] ([SapSchemaId]),
    CONSTRAINT [FK_SapFiles_Blobs]      FOREIGN KEY ([FileKey])         REFERENCES [dbo].[Blobs]      ([Key]),
    CONSTRAINT [FK_SapFiles_Users]      FOREIGN KEY ([CreatedByUserId]) REFERENCES [dbo].[Users]      ([UserId]),
    CONSTRAINT [CHK_SapFiles_Status]    CHECK       ([Status] IN (1, 2))
);
GO

CREATE TABLE [dbo].[SapPaidAmounts] (
    [SapPaidAmountId]           INT              NOT NULL IDENTITY,
    [SapFileId]                 INT              NOT NULL,
    [IsImported]                BIT              NOT NULL,
    [ActuallyPaidAmountId]      INT              NULL,
    [ReimbursedAmountId]        INT              NULL,

    [ProgrammeId]               INT              NULL,
    [ContractSapNum]            NVARCHAR(50)     NULL,
    [ContractId]                INT              NULL,
    [ContractDebtId]            INT              NULL,
    [Fund]                      INT              NULL,
    [ContractReportPaymentNum]  NVARCHAR(50)     NULL,
    [ContractReportPaymentId]   INT              NULL,
    [ContractReportPaymentDate] DATETIME2        NULL,
    [FinanceSource]             INT              NULL,
    [PaidAmount]                MONEY            NULL,
    [Currency]                  INT              NULL,
    [PaymentType]               INT              NULL,
    [AccDate]                   DATETIME2        NULL,
    [BankDate]                  DATETIME2        NULL,
    [SapDate]                   DATETIME2        NULL,
    [Comment]                   NVARCHAR(MAX)    NULL,
    [StornoCode]                NVARCHAR(50)     NULL,
    [StornoDescr]               NVARCHAR(MAX)    NULL,

    [HasError]                  BIT              NOT NULL,
    [Errors]                    NVARCHAR(MAX)    NULL,

    CONSTRAINT [PK_SapPaidAmounts]                        PRIMARY KEY ([SapPaidAmountId]),
    CONSTRAINT [FK_SapPaidAmounts_SapFiles]               FOREIGN KEY ([SapFileId])               REFERENCES [dbo].[SapFiles]               ([SapFileId]),
    CONSTRAINT [FK_SapPaidAmounts_ActuallyPaidAmounts]    FOREIGN KEY ([ActuallyPaidAmountId])    REFERENCES [dbo].[ActuallyPaidAmounts]    ([ActuallyPaidAmountId]),
    CONSTRAINT [FK_SapPaidAmounts_ReimbursedAmounts]      FOREIGN KEY ([ReimbursedAmountId])      REFERENCES [dbo].[ReimbursedAmounts]      ([ReimbursedAmountId]),
    CONSTRAINT [FK_SapPaidAmounts_Programmes]             FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]               ([MapNodeId]),
    CONSTRAINT [FK_SapPaidAmounts_Contracts]              FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]              ([ContractId]),
    CONSTRAINT [FK_SapPaidAmounts_ContractDebts]          FOREIGN KEY ([ContractDebtId])          REFERENCES [dbo].[ContractDebts]          ([ContractDebtId]),
    CONSTRAINT [FK_SapPaidAmounts_ContractReportPayments] FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [CHK_SapPaidAmounts_Fund]                  CHECK       ([Fund]          IN (1, 2, 3)),
    CONSTRAINT [CHK_SapPaidAmounts_FinanceSource]         CHECK       ([FinanceSource] IN (1, 2)),
    CONSTRAINT [CHK_SapPaidAmounts_Currency]              CHECK       ([Currency]      IN (1)),
    CONSTRAINT [CHK_SapPaidAmounts_PaymentType]           CHECK       ([PaymentType]   IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10))
);
GO

ALTER TABLE [dbo].[ActuallyPaidAmounts]
ADD [SapFileId]         INT           NULL,
    CONSTRAINT [FK_ActuallyPaidAmounts_SapFiles] FOREIGN KEY ([SapFileId]) REFERENCES [dbo].[SapFiles] ([SapFileId]);
GO

ALTER TABLE [dbo].[ReimbursedAmounts]
ADD [SapFileId] INT NULL,
    CONSTRAINT [FK_ReimbursedAmounts_SapFiles] FOREIGN KEY ([SapFileId]) REFERENCES [dbo].[SapFiles] ([SapFileId]);
GO
