PRINT 'IrregularitySignals'
GO

CREATE TABLE [dbo].[IrregularitySignals] (
    [IrregularitySignalId]      INT                 NOT NULL IDENTITY,
    [ProgrammeId]               INT                 NOT NULL,
    [ProcedureId]               INT                 NOT NULL,
    [ProjectId]                 INT                 NOT NULL,
    [ContractId]                INT                 NULL,
    [Status]                    INT                 NOT NULL,

    [Number]                    INT                 NULL,
    [RegNumber]                 NVARCHAR(200)       NULL,
    [RegDate]                   DATETIME2           NULL,
    [SignalSource]              NVARCHAR(500)       NULL,
    [MASystemRegDate]           DATETIME2           NULL,
    [SignalKind]                NVARCHAR(MAX)       NULL,
    [ViolationDesrc]            NVARCHAR(MAX)       NULL,
    [Actions]                   NVARCHAR(MAX)       NULL,
    [ActRegNum]                 NVARCHAR(50)        NULL,
    [ActRegDate]                DATETIME2           NULL,
    [Comment]                   NVARCHAR(MAX)       NULL,

    [IsActivated]               BIT                 NOT NULL,
    [DeleteNote]                NVARCHAR(MAX)       NULL,
    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_IrregularitySignals]                      PRIMARY KEY ([IrregularitySignalId]),
    CONSTRAINT [FK_IrregularitySignals_Programmes]           FOREIGN KEY ([ProgrammeId])         REFERENCES [dbo].[MapNodes]      ([MapNodeId]),
    CONSTRAINT [FK_IrregularitySignals_Procedures]           FOREIGN KEY ([ProcedureId])         REFERENCES [dbo].[Procedures]    ([ProcedureId]),
    CONSTRAINT [FK_IrregularitySignals_Projects]             FOREIGN KEY ([ProjectId])           REFERENCES [dbo].[Projects]      ([ProjectId]),
    CONSTRAINT [FK_IrregularitySignals_Contracts]            FOREIGN KEY ([ContractId])          REFERENCES [dbo].[Contracts]     ([ContractId]),
    CONSTRAINT [CHK_IrregularitySignals_Status]              CHECK ([Status]     IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'IrregularitySignals', N'Сигнали за нередности.'
exec spDescColumn N'IrregularitySignals', N'IrregularitySignalId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IrregularitySignals', N'ProgrammeId'          , N'Идентификатор на оперативна програма'
exec spDescColumn N'IrregularitySignals', N'ProcedureId'          , N'Идентификатор на процедура.'
exec spDescColumn N'IrregularitySignals', N'ProjectId'            , N'Идентификатор на проектно предложение.'
exec spDescColumn N'IrregularitySignals', N'ContractId'           , N'Идентификатор на договор за БФП.'
exec spDescColumn N'IrregularitySignals', N'Status'               , N'Състояние на сигнала: 1 - Чернова; 2 - Активен; 3 - Приключен; 4 - Анулиран.'

exec spDescColumn N'IrregularitySignals', N'Number'               , N'Пореден номер на сигнал.'
exec spDescColumn N'IrregularitySignals', N'RegNumber'            , N'Номер на сигнал.'
exec spDescColumn N'IrregularitySignals', N'RegDate'              , N'Дата на регистрация на сигнала в ИСУН.'
exec spDescColumn N'IrregularitySignals', N'SignalSource'         , N'Източник на първия сигнал.'
exec spDescColumn N'IrregularitySignals', N'MASystemRegDate'      , N'Дата на регистрация на сигнала в деловодната система на УО.'
exec spDescColumn N'IrregularitySignals', N'SignalKind'           , N'Вид на сигнала.'
exec spDescColumn N'IrregularitySignals', N'ViolationDesrc'       , N'Описание на нарушението.'
exec spDescColumn N'IrregularitySignals', N'Actions'              , N'Предприети действия.'
exec spDescColumn N'IrregularitySignals', N'ActRegNum'            , N'Регистрационен № на акта по чл. 14.'
exec spDescColumn N'IrregularitySignals', N'ActRegDate'           , N'Дата на акта по чл. 14.'
exec spDescColumn N'IrregularitySignals', N'Comment'              , N'Коментар.'

exec spDescColumn N'IrregularitySignals', N'IsActivated'          , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'IrregularitySignals', N'DeleteNote'           , N'Причина за анулиране.'
exec spDescColumn N'IrregularitySignals', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'IrregularitySignals', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'IrregularitySignals', N'Version'              , N'Версия.'
GO
