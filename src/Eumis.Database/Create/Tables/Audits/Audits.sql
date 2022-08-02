PRINT 'Audits'
GO

CREATE TABLE [dbo].[Audits] (
    [AuditId]                   INT                 NOT NULL IDENTITY,
    [ProgrammeId]               INT                 NOT NULL,
    [AuditInstitution]          INT                 NOT NULL,
    [AuditType]                 INT                 NOT NULL,
    [AuditKind]                 INT                 NOT NULL,
    [Level]                     INT                 NOT NULL,
    [ContractId]                INT                 NULL,

    [FinalReportDate]           DATETIME2           NULL,
    [FinalReportNum]            NVARCHAR (100)      NULL,

    [CheckStartDate]            DATETIME2           NULL,
    [CheckEndDate]              DATETIME2           NULL,
    [AuditSubjectType]          INT                 NULL,
    [AuditSubjectName]          NVARCHAR (500)      NULL,
    [Comment]                   NVARCHAR (MAX)      NULL,

    [CreateDate]                DATETIME2           NOT NULL,
    [ModifyDate]                DATETIME2           NOT NULL,
    [Version]                   ROWVERSION          NOT NULL

    CONSTRAINT [PK_Audits]                         PRIMARY KEY ([AuditId]),
    CONSTRAINT [FK_Audits_Programmes]              FOREIGN KEY ([ProgrammeId])         REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_Audits_Contracts]               FOREIGN KEY ([ContractId])                 REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [CHK_Audits_AuditInstitution]       CHECK ([AuditInstitution]     IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_Audits_AuditType]              CHECK ([AuditType]            IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_Audits_AuditKind]              CHECK ([AuditKind]            IN (1, 2)),
    CONSTRAINT [CHK_Audits_Level]                  CHECK ([Level]                IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_Audits_AuditSubjectType]       CHECK ([AuditSubjectType]     IN (1, 2, 3, 4, 5, 6, 7, 8, 9)),
    CONSTRAINT [CHK_Audits_LevelContractContract]  CHECK ([Level] != 5 OR [ContractId]  IS NOT NULL)
);
GO

exec spDescTable  N'Audits', N'Одити.'
exec spDescColumn N'Audits', N'AuditId'              , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Audits', N'ProgrammeId'          , N'Идентификатор на оперативна програма'
exec spDescColumn N'Audits', N'AuditInstitution'     , N'Институция, извършваща одита: 1 – Сметна палата, 2 – Звено за вътрешен одит, 3 – АДФИ, 4 – ОО, 5 – ЕК, 6 – Европейска сметна палата, 7 – Друга.'
exec spDescColumn N'Audits', N'AuditType'            , N'Тип: 1 – Одит от Сметна палата, 2 – Одит от Звеното за вътрешен одит, 3 – Проверка от АДФИ, 4 - Системен одит, 5 – Одит на операциите, 6 - Тематичен одит, 7 – Друг.'
exec spDescColumn N'Audits', N'AuditKind'            , N'Вид: 1 – Планиран, 2 – Извънреден.'
exec spDescColumn N'Audits', N'Level'                , N'Ниво: 1 - Програма, 2 – Приоритетна ос, 3 – Процедура, 4 - Договор за БФП, 5 - Договор с изпълнител.'
exec spDescColumn N'Audits', N'ContractId'           , N'Идентификатор на договор за БФП.'

exec spDescColumn N'Audits', N'FinalReportDate'      , N'Дата на окончателния доклад.'
exec spDescColumn N'Audits', N'FinalReportNum'       , N'Номер на окончателния доклад.'

exec spDescColumn N'Audits', N'CheckStartDate'       , N'Начална дата на проверката.'
exec spDescColumn N'Audits', N'CheckEndDate'         , N'Крайна дата на проверката.'
exec spDescColumn N'Audits', N'AuditSubjectType'     , N'Вид на одитирания обект: 1 – Бенефициент, 2 – Партньор, 3 – Изпълнител, 4 - Финансов посредник, 5 - Краен получател, 6 – УО, 7 – СО, 8 – МЗ, 9 - Друг.'
exec spDescColumn N'Audits', N'AuditSubjectName'     , N'Наименование на одитирания обект.'
exec spDescColumn N'Audits', N'Comment'              , N'Коментар.'

exec spDescColumn N'Audits', N'CreateDate'           , N'Дата на създаване на записа.'
exec spDescColumn N'Audits', N'ModifyDate'           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'Audits', N'Version'              , N'Версия.'
GO
