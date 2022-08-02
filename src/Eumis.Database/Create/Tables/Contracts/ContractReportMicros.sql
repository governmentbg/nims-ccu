PRINT 'ContractReportMicros'
GO

CREATE TABLE [dbo].[ContractReportMicros] (
    [ContractReportMicroId]         INT               NOT NULL IDENTITY,
    [ContractReportId]              INT               NOT NULL,
    [ContractId]                    INT               NOT NULL,
    [Gid]                           UNIQUEIDENTIFIER  NOT NULL UNIQUE,
    [Type]                          INT               NOT NULL,
    [ExcelBlobKey]                  UNIQUEIDENTIFIER  NULL,
    [ExcelName]                     NVARCHAR(200)     NULL,
    [IsFromExternalSystem]          BIT               NOT NULL,

    [VersionNum]                    INT               NOT NULL,
    [VersionSubNum]                 INT               NOT NULL,
    [Source]                        INT               NOT NULL,
    [Status]                        INT               NOT NULL,
    [StatusNote]                    NVARCHAR(MAX)     NULL,

    [SenderContractRegistrationId]  INT               NULL,

    [CreateDate]                    DATETIME2         NOT NULL,
    [ModifyDate]                    DATETIME2         NOT NULL,
    [Version]                       ROWVERSION        NOT NULL,

    CONSTRAINT [PK_ContractReportMicros]                        PRIMARY KEY ([ContractReportMicroId]),
    CONSTRAINT [FK_ContractReportMicros_Contracts]              FOREIGN KEY ([ContractId])         REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractReportMicros_ContractReports]        FOREIGN KEY ([ContractReportId])   REFERENCES [dbo].[ContractReports] ([ContractReportId]),
    CONSTRAINT [FK_ContractReportMicros_Blobs]                  FOREIGN KEY ([ExcelBlobKey])       REFERENCES [dbo].[Blobs] ([Key]),
    CONSTRAINT [FK_ContractReportMicros_ContractRegistrations]  FOREIGN KEY ([SenderContractRegistrationId])   REFERENCES [dbo].[ContractRegistrations] ([ContractRegistrationId]),
    CONSTRAINT [CHK_ContractReportMicros_Source]                CHECK ([Source]   IN (1, 2)),
    CONSTRAINT [CHK_ContractReportMicros_Status]                CHECK ([Status]   IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ContractReportMicros_Type]                  CHECK ([Type]     IN (1, 2, 3, 4))
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_ContractReportMicros_ContractReportMicroId_Status_Type]
ON [ContractReportMicros]([ContractReportId], [Type], [Status])
WHERE [Status] = 3;
GO

exec spDescTable  N'ContractReportMicros', N'Микроданни.'
exec spDescColumn N'ContractReportMicros', N'ContractReportMicroId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractReportMicros', N'ContractReportId'     , N'Идентификатор на пакет отчетни документи'
exec spDescColumn N'ContractReportMicros', N'ContractId'           , N'Идентификатор на договор'
exec spDescColumn N'ContractReportMicros', N'Gid'                  , N'Уникален системно генериран публичен идентификатор.'
exec spDescColumn N'ContractReportMicros', N'Type'                 , N'Вид:  1- Първи вид; 2 - Втори вид, 3 - Трети вид, 4 - Четвърти вид.'
exec spDescColumn N'ContractReportMicros', N'ExcelBlobKey'         , N'Идентификатор на Excel файл.'
exec spDescColumn N'ContractReportMicros', N'ExcelName'            , N'Име на Excel файл.'

exec spDescColumn N'ContractReportMicros', N'VersionNum'           , N'Номер на отчета.'
exec spDescColumn N'ContractReportMicros', N'VersionSubNum'        , N'Пореден номер на версията за отчет.'
exec spDescColumn N'ContractReportMicros', N'Source'               , N'Въведен от: 1 - Бенефициент, 2 – УО.'
exec spDescColumn N'ContractReportMicros', N'Status'               , N'Статус:  1- Чернова; 2 - Въведен, 3 - Актуален/Изпратен, 4 - Върнат.'
exec spDescColumn N'ContractReportMicros', N'StatusNote'           , N'Бележка.'

exec spDescColumn N'ContractReportMicros', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'ContractReportMicros', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractReportMicros', N'Version'              , N'Версия.'
GO
