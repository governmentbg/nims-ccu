PRINT 'CompensationDocuments'
GO

CREATE TABLE [dbo].[CompensationDocuments] (
    [CompensationDocumentId]  INT           NOT NULL IDENTITY,
    [ProgrammeId]             INT           NOT NULL,
    [ProcedureId]             INT           NOT NULL,
    [ProgrammePriorityId]     INT           NOT NULL,
    [FinanceSource]           INT           NOT NULL,
    [ContractId]              INT           NOT NULL,
    [ContractReportPaymentId] INT           NULL,
    [Type]                    INT           NOT NULL,
    [Status]                  INT           NOT NULL,
    [RegNumber]               NVARCHAR(200) NULL,
    [CompensationSign]        INT           NOT NULL,
    [CompensationDocDate]     DATETIME2     NOT NULL,
    [Description]             NVARCHAR(MAX) NULL,
    [CompensationReason]      NVARCHAR(MAX) NULL,

    [BfpEuAmount]             MONEY         NULL,
    [BfpBgAmount]             MONEY         NULL,
    [BfpTotalAmount]          MONEY         NULL,
    [BfpCrossAmount]          MONEY         NULL,
    [SelfAmount]              MONEY         NULL,
    [TotalAmount]             MONEY         NULL,

    [IsActivated]             BIT           NOT NULL,
    [DeleteNote]              NVARCHAR(MAX) NULL,
    [CreateDate]              DATETIME2     NOT NULL,
    [ModifyDate]              DATETIME2     NOT NULL,
    [Version]                 ROWVERSION    NOT NULL,

    CONSTRAINT [PK_CompensationDocuments]                        PRIMARY KEY ([CompensationDocumentId]),
    CONSTRAINT [FK_CompensationDocuments_Programmes]             FOREIGN KEY ([ProgrammeId])             REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_CompensationDocuments_Procedures]             FOREIGN KEY ([ProcedureId])             REFERENCES [dbo].[Procedures] ([ProcedureId]),
    CONSTRAINT [FK_CompensationDocuments_Contracts]              FOREIGN KEY ([ContractId])              REFERENCES [dbo].[Contracts]  ([ContractId]),
    CONSTRAINT [FK_CompensationDocuments_ProgrammePriorities]    FOREIGN KEY ([ProgrammePriorityId])     REFERENCES [dbo].[MapNodes]   ([MapNodeId]),
    CONSTRAINT [FK_CompensationDocuments_ContractReportPayments] FOREIGN KEY ([ContractReportPaymentId]) REFERENCES [dbo].[ContractReportPayments] ([ContractReportPaymentId]),
    CONSTRAINT [CHK_CompensationDocuments_Type]                  CHECK       ([Type]              IN (1, 2, 3)),
    CONSTRAINT [CHK_CompensationDocuments_Status]                CHECK       ([Status]            IN (1, 2, 3)),
    CONSTRAINT [CHK_CompensationDocuments_CompensationSign]      CHECK       ([CompensationSign]  IN (-1, 1)),
    CONSTRAINT [CHK_CompensationDocuments_FinanceSource]         CHECK       ([FinanceSource]     IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12))
);
GO

exec spDescTable  N'CompensationDocuments', N'Изравнителни документи.'
exec spDescColumn N'CompensationDocuments', N'CompensationDocumentId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CompensationDocuments', N'ProgrammeId'            , N'Идентификатор на програма.'
exec spDescColumn N'CompensationDocuments', N'ProcedureId'            , N'Идентификатор на процедура.'
exec spDescColumn N'CompensationDocuments', N'ProgrammePriorityId'    , N'Идентификатор на приоритетна ос.'
exec spDescColumn N'CompensationDocuments', N'FinanceSource'          , N'Фонд: 1 - ЕСФ; 2 - ЕФРР; 3 - КФ; 4 - ИМЗ; 5 - ФЕПНЛ.'
exec spDescColumn N'CompensationDocuments', N'ContractId'             , N'Идентификатор на договор.'
exec spDescColumn N'CompensationDocuments', N'ContractReportPaymentId', N'Идентификатор на искане за плащане.'
exec spDescColumn N'CompensationDocuments', N'Type'                   , N'Вид: 1 - Договорени;  2 - Поискани; 3 - Реално изплатени суми.'
exec spDescColumn N'CompensationDocuments', N'Status'                 , N'Статус: 1 - Чернова; 2 - Въведен; 3 - Анулиран.'
exec spDescColumn N'CompensationDocuments', N'RegNumber'              , N'Регистрационен номер.'
exec spDescColumn N'CompensationDocuments', N'CompensationSign'       , N'Знак: 1 - +, -1 - –.'
exec spDescColumn N'CompensationDocuments', N'CompensationDocDate'    , N'Дата.'
exec spDescColumn N'CompensationDocuments', N'Description'            , N'Описание.'
exec spDescColumn N'CompensationDocuments', N'CompensationReason'     , N'Основание.'

exec spDescColumn N'CompensationDocuments', N'BfpEuAmount'            , N'Финансиране от ЕС.'
exec spDescColumn N'CompensationDocuments', N'BfpBgAmount'            , N'Финансиране от НФ.'
exec spDescColumn N'CompensationDocuments', N'BfpTotalAmount'         , N'Общо БФП.'
exec spDescColumn N'CompensationDocuments', N'BfpCrossAmount'         , N'Кръстосано финансиране.'
exec spDescColumn N'CompensationDocuments', N'SelfAmount'             , N'Собствено съфинансиране.'
exec spDescColumn N'CompensationDocuments', N'TotalAmount'            , N'Общо финансиране.'

exec spDescColumn N'CompensationDocuments', N'IsActivated'            , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'CompensationDocuments', N'DeleteNote'             , N'Причина за изтриване.'
exec spDescColumn N'CompensationDocuments', N'CreateDate'             , N'Дата на създаване на записа.'
exec spDescColumn N'CompensationDocuments', N'ModifyDate'             , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CompensationDocuments', N'Version'                , N'Версия.'
GO