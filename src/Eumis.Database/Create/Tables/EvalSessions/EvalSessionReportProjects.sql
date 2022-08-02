PRINT 'EvalSessionReportProjects'
GO

CREATE TABLE [dbo].[EvalSessionReportProjects] (
    [EvalSessionId]                 INT                NOT NULL,
    [EvalSessionReportId]           INT                NOT NULL,
    [ProjectId]                     INT                NOT NULL,
    [ProjectVersionId]              INT                NULL,

    [RegNumber]                     NVARCHAR(200)      NOT NULL,
    [RegDate]                       DATETIME2          NOT NULL,
    [RecieveDate]                   DATETIME2          NOT NULL,
    [RecieveType]                   INT                NOT NULL,
    [Name]                          NVARCHAR(MAX)      NOT NULL,
    [Duration]                      INT                NULL,
    [ProjectKidCodeId]              INT                NULL,
    [ProjectPlace]                  NVARCHAR(MAX)      NULL,
    [GrandAmount]                   MONEY              NULL,
    [CoFinancingAmount]             MONEY              NULL,

    [StandingStatus]                INT                NOT NULL,
    [StandingNum]                   INT                NULL,
    [StandingGrandAmount]           MONEY              NULL,

    [CompanyUin]                    NVARCHAR(200)      NOT NULL,
    [CompanyName]                   NVARCHAR(200)      NOT NULL,
    [CompanySizeTypeId]             INT                NOT NULL,
    [CompanyKidCodeId]              INT                NULL,
    [RegEmail]                      NVARCHAR (200)     NULL,
    [Correspondence]                NVARCHAR(MAX)      NULL,

    [HasAdminAdmiss]                BIT                NOT NULL,
    [AdminAdmissResult]             INT                NULL,
    [AdminAdmissPoints]             DECIMAL(15,3)      NULL,

    [HasTechFinance]                BIT                NOT NULL,
    [TechFinanceResult]             INT                NULL,
    [TechFinancePoints]             DECIMAL(15,3)      NULL,

    [HasComplex]                    BIT                NOT NULL,
    [ComplexResult]                 INT                NULL,
    [ComplexPoints]                 DECIMAL(15,3)      NULL,

    CONSTRAINT [PK_EvalSessionReportProjects]                              PRIMARY KEY ([EvalSessionId], [EvalSessionReportId], [ProjectId]),
    CONSTRAINT [FK_EvalSessionReportProjects_EvalSessions]                 FOREIGN KEY ([EvalSessionId])                          REFERENCES [dbo].[EvalSessions] ([EvalSessionId]),
    CONSTRAINT [FK_EvalSessionReportProjects_EvalSessionReports]           FOREIGN KEY ([EvalSessionReportId])                    REFERENCES [dbo].[EvalSessionReports]  ([EvalSessionReportId]),
    CONSTRAINT [FK_EvalSessionReportProjects_Projects]                     FOREIGN KEY ([ProjectId])                              REFERENCES [dbo].[Projects]            ([ProjectId]),
    CONSTRAINT [FK_EvalSessionReportProjects_ProjectVersionXmls]           FOREIGN KEY ([ProjectVersionId])                       REFERENCES [dbo].[ProjectVersionXmls]  ([ProjectVersionXmlId]),
    CONSTRAINT [FK_EvalSessionReportProjects_ProjectKidCodes]              FOREIGN KEY ([ProjectKidCodeId])                       REFERENCES [dbo].[KidCodes]            ([KidCodeId]),
    CONSTRAINT [FK_EvalSessionReportProjects_CompanySizeTypes]             FOREIGN KEY ([CompanySizeTypeId])                      REFERENCES [dbo].[CompanySizeTypes]    ([CompanySizeTypeId]),
    CONSTRAINT [FK_EvalSessionReportProjects_CompanyKidCodes]              FOREIGN KEY ([CompanyKidCodeId])                       REFERENCES [dbo].[KidCodes]            ([KidCodeId]),
    CONSTRAINT [CHK_EvalSessionReportProjects_RecieveType]                 CHECK       ([RecieveType]       IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_EvalSessionReportProjects_StandingStatus]              CHECK       ([StandingStatus]    IN (1, 2, 3, 4, 5, 6, 7, 8)),
    CONSTRAINT [CHK_EvalSessionReportProjects_AdminAdmissResult]           CHECK       ([AdminAdmissResult] IN (1, 2)),
    CONSTRAINT [CHK_EvalSessionReportProjects_TechFinanceResult]           CHECK       ([TechFinanceResult] IN (1, 2)),
    CONSTRAINT [CHK_EvalSessionReportProjects_ComplexResult]               CHECK       ([ComplexResult]     IN (1, 2))
);
GO

exec spDescTable  N'EvalSessionReportProjects', N'Проектни предложения към документ.'
exec spDescColumn N'EvalSessionReportProjects', N'EvalSessionId'                      , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionReportProjects', N'EvalSessionReportId'                , N'Идентификатор на документ към оценителна сесия.'
exec spDescColumn N'EvalSessionReportProjects', N'ProjectId'                          , N'Идентификатор на проектно предложние към оценителна сесия.'
exec spDescColumn N'EvalSessionReportProjects', N'ProjectVersionId'                   , N'Идентификатор на версия на проектно предложние към оценителна сесия.'

exec spDescColumn N'EvalSessionReportProjects', N'RegNumber'                          , N'Регистрационен номер на ПП.'
exec spDescColumn N'EvalSessionReportProjects', N'RegDate'                            , N'Дата на регистрация.'
exec spDescColumn N'EvalSessionReportProjects', N'RecieveDate'                        , N'Дата на получаване.'
exec spDescColumn N'EvalSessionReportProjects', N'RecieveType'                        , N'Начин на получване. 1. На ръка, 2. Поща, 3. Куриер, 4. Факс, 5. Електронен път'
exec spDescColumn N'EvalSessionReportProjects', N'Name'                               , N'Наименование.'
exec spDescColumn N'EvalSessionReportProjects', N'Duration'                           , N'Продължителност на проекта (месеци).'
exec spDescColumn N'EvalSessionReportProjects', N'ProjectKidCodeId'                   , N'Код на проекта по КИД 2008.'
exec spDescColumn N'EvalSessionReportProjects', N'ProjectPlace'                       , N'Място на изпълнение на проекта.'
exec spDescColumn N'EvalSessionReportProjects', N'GrandAmount'                        , N'Общ размер на безвъзмездната финансова помощ.'
exec spDescColumn N'EvalSessionReportProjects', N'CoFinancingAmount'                  , N'Общ размер на съфинансиране.'

exec spDescColumn N'EvalSessionReportProjects', N'StandingStatus'                     , N'Класиране - Статус: 1 - Одобрено, 2 - Резерва, 3 - Отхвърлено, 4 - Отхвърлено на ОАСД, 5 - Отхвърлено на ТФО; 6 - Оттеглено; 7 - Анулирано; 8 - Без класиране.'
exec spDescColumn N'EvalSessionReportProjects', N'StandingNum'                        , N'Класиране - Пореден номер.'
exec spDescColumn N'EvalSessionReportProjects', N'StandingGrandAmount'                , N'Класиране - Одобрено БФП.'

exec spDescColumn N'EvalSessionReportProjects', N'CompanyUin'                         , N'Кандидат - УИН.'
exec spDescColumn N'EvalSessionReportProjects', N'CompanyName'                        , N'Кандидат - Наименование.'
exec spDescColumn N'EvalSessionReportProjects', N'CompanySizeTypeId'                  , N'Кандидат - Идентификатор на предприятието спрямо „Закона на малки и средни предприятия“.'
exec spDescColumn N'EvalSessionReportProjects', N'CompanyKidCodeId'                   , N'Кандидат - Код на организацията по КИД 2008.'
exec spDescColumn N'EvalSessionReportProjects', N'RegEmail'                           , N'Профил за комуникация - Ел.поща.'
exec spDescColumn N'EvalSessionReportProjects', N'Correspondence'                     , N'Адрес за кореспондениця.'

exec spDescColumn N'EvalSessionReportProjects', N'HasAdminAdmiss'                     , N'Оценка на административното съответствие и допустимостта - Маркер за наличност.'
exec spDescColumn N'EvalSessionReportProjects', N'AdminAdmissResult'                  , N'Оценка на административното съответствие и допустимостта - Маркер за преминаване.'
exec spDescColumn N'EvalSessionReportProjects', N'AdminAdmissPoints'                  , N'Оценка на административното съответствие и допустимостта – Точки.'

exec spDescColumn N'EvalSessionReportProjects', N'HasTechFinance'                     , N'Техническа и финансова оценка - Маркер за наличност.'
exec spDescColumn N'EvalSessionReportProjects', N'TechFinanceResult'                  , N'Техническа и финансова оценка - Маркер за преминаване.'
exec spDescColumn N'EvalSessionReportProjects', N'TechFinancePoints'                  , N'Техническа и финансова оценка – Точки.'

exec spDescColumn N'EvalSessionReportProjects', N'HasComplex'                         , N'Комплексна оценка - Маркер за наличност.'
exec spDescColumn N'EvalSessionReportProjects', N'ComplexResult'                      , N'Комплексна оценка - Маркер за преминаване.'
exec spDescColumn N'EvalSessionReportProjects', N'ComplexPoints'                      , N'Комплексна оценка – Точки.'
GO

