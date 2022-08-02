PRINT 'CertAuthorityChecks'
GO

CREATE TABLE [dbo].[CertAuthorityChecks] (
    [CertAuthorityCheckId]       INT               NOT NULL IDENTITY,
    [Status]                     INT               NOT NULL,
    [Level]                      INT               NOT NULL,
    [CheckKind]                  INT               NOT NULL,
    [CheckType]                  INT               NOT NULL,
    [CheckNum]                   INT               NULL,

    [DateFrom]                   DATETIME2         NULL,
    [DateTo]                     DATETIME2         NULL,
    [SubjectType]                INT               NULL,
    [SubjectName]                NVARCHAR(500)     NULL,
    [Team]                       NVARCHAR(500)     NULL,

    [IsActivated]                BIT               NOT NULL,
    [DeleteNote]                 NVARCHAR(MAX)     NULL,
    [CreateDate]                 DATETIME2         NOT NULL,
    [ModifyDate]                 DATETIME2         NOT NULL,
    [Version]                    ROWVERSION        NOT NULL

    CONSTRAINT [PK_CertAuthorityChecks]                   PRIMARY KEY ([CertAuthorityCheckId]),
    CONSTRAINT [CHK_CertAuthorityChecks_CheckKind]        CHECK ([CheckKind]   IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_CertAuthorityChecks_CheckType]        CHECK ([CheckType]   IN (1, 2)),
    CONSTRAINT [CHK_CertAuthorityChecks_SubjectType]      CHECK ([SubjectType] IN (1, 2, 3, 4, 5, 6, 7)),
    CONSTRAINT [CHK_CertAuthorityChecks_Status]           CHECK ([Status]      IN (1, 2, 3)),
    CONSTRAINT [CHK_CertAuthorityChecks_Level]            CHECK ([Level]       IN (1, 2, 3, 4))
);
GO

CREATE UNIQUE INDEX [UQ_CertAuthorityChecks_CheckNum]
ON [CertAuthorityChecks]([CheckNum])
WHERE [CheckNum] IS NOT NULL;
GO

exec spDescTable  N'CertAuthorityChecks', N'Проверки на СО.'
exec spDescColumn N'CertAuthorityChecks', N'CertAuthorityCheckId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'CertAuthorityChecks', N'Status'              , N'Статус на проверката: 1 - Чернова, 2 - Въведена, 3 - Анулирана.'
exec spDescColumn N'CertAuthorityChecks', N'Level'               , N'Ниво: 1 – Оперативна програма, 2 – Приоритетна ос, 3 – Процедура, 4 - Договор за БФП.'
exec spDescColumn N'CertAuthorityChecks', N'CheckKind'           , N'Вид  на проверката: 1 - при бенефициент/финансов посредник/краен получател; 2 - контрол на качеството в УО; 3 - проверка на Книга на длъжниците; 4 - регулярна проверка на проведените процедури за избор на изпълнител с верифицирани разходи.'
exec spDescColumn N'CertAuthorityChecks', N'CheckType'           , N'Тип на проверката: 1 – Планирана, 2 - Извънредна.'
exec spDescColumn N'CertAuthorityChecks', N'CheckNum'            , N'Пореден номер на проверката.'

exec spDescColumn N'CertAuthorityChecks', N'DateFrom'            , N'Период на проверката - От'
exec spDescColumn N'CertAuthorityChecks', N'DateTo'              , N'Период на проверката - До'
exec spDescColumn N'CertAuthorityChecks', N'SubjectType'         , N'Вид на проверяваната институция: 1 – УО, 2 – МЗ, 3 – Бенефициент, 4 – Краен получател, 5 – Финансов посредник, 6 - Изпълнители, 7 - Партньори.'
exec spDescColumn N'CertAuthorityChecks', N'SubjectName'         , N'Наименование на проверявания'
exec spDescColumn N'CertAuthorityChecks', N'Team'                , N'Екип, извършил проверката'

exec spDescColumn N'CertAuthorityChecks', N'IsActivated'         , N'Маркер дали записът е бил активиран.'
exec spDescColumn N'CertAuthorityChecks', N'DeleteNote'          , N'Причина за анулиране.'
exec spDescColumn N'CertAuthorityChecks', N'CreateDate'          , N'Дата на създаване на записа.'
exec spDescColumn N'CertAuthorityChecks', N'ModifyDate'          , N'Дата на последно редактиране на записа.'
exec spDescColumn N'CertAuthorityChecks', N'Version'             , N'Версия.'
GO
