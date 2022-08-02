PRINT 'EvalSessionResultProjects'
GO

CREATE TABLE [dbo].[EvalSessionResultProjects] (
    [EvalSessionResultProjectId]                    INT                 NOT NULL IDENTITY,
    [EvalSessionResultId]                           INT                 NOT NULL,
    [ProjectId]                                     INT                 NOT NULL,
    [ProjectRegNumber]                              NVARCHAR(50)        NOT NULL,
    [ProjectName]                                   NVARCHAR(MAX)       NOT NULL,
    [ProjectNameAlt]                                NVARCHAR(MAX)       NULL,
    [ProjectRegDate]                                DATETIME2           NOT NULL,
    [CompanyName]                                   NVARCHAR(200)       NOT NULL,
    [CompanyNameAlt]                                NVARCHAR(200)       NULL,
    [CompanyUin]                                    NVARCHAR(200)       NOT NULL,
    [CompanyUinType]                                INT                 NOT NULL,
    [GrantAmount]                                   MONEY               NULL,
    [SelfAmount]                                    MONEY               NULL,
    [StandingPreliminaryResult]                     BIT                 NULL,
    [StandingPreliminaryPoints]                     DECIMAL(15,3)       NULL,
    [EvaluationAdminAdmissResult]                   BIT                 NULL,
    [StandingTechFinanceResult]                     BIT                 NULL,
    [StandingTechFinancePoints]                     DECIMAL(15,3)       NULL,
    [StandingComplexResult]                         BIT                 NULL,
    [StandingComplexPoints]                         DECIMAL(15,3)       NULL,
    [NonAdmissionReason]                            NVARCHAR(MAX)       NULL,
    [Note]                                          NVARCHAR(MAX)       NULL,
    [ProjectStandingNumber]                         INT                 NULL,
    [ProjectStandingStatus]                         INT                 NULL,
    [GrantAmountCorrected]                          MONEY               NULL,
    [SelfAmountCorrected]                           MONEY               NULL,
    

    CONSTRAINT [PK_EvalSessionResultProjects]                                  PRIMARY KEY ([EvalSessionResultProjectId]),
    CONSTRAINT [FK_EvalSessionResultProjects_EvalSessionResults]               FOREIGN KEY ([EvalSessionResultId])                 REFERENCES [dbo].[EvalSessionResults] ([EvalSessionResultId]),
    CONSTRAINT [FK_EvalSessionResultProjects_Projects]                         FOREIGN KEY ([ProjectId])                           REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [CHK_EvalSessionResultProjects_CompanyUinType]                  CHECK       ([CompanyUinType]   IN (0, 1, 2, 3))
);

exec spDescTable  N'EvalSessionResultProjects', N'Проекти към резултат на оценителна сесия.'
exec spDescColumn N'EvalSessionResultProjects', N'EvalSessionResultProjectId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionResultProjects', N'EvalSessionResultId'             , N'Идентификатор на резултат.'
exec spDescColumn N'EvalSessionResultProjects', N'ProjectId'                       , N'Идентификатор на проект.'
exec spDescColumn N'EvalSessionResultProjects', N'ProjectRegNumber'                , N'Номер на проект'
exec spDescColumn N'EvalSessionResultProjects', N'ProjectName'                     , N'Наименование на проект'
exec spDescColumn N'EvalSessionResultProjects', N'ProjectNameAlt'                  , N'Наименование на проект на чужд език.'
exec spDescColumn N'EvalSessionResultProjects', N'ProjectRegDate'                  , N'Дата на регистрация на проект.'
exec spDescColumn N'EvalSessionResultProjects', N'CompanyName'                     , N'Наименоване на кандидат.'
exec spDescColumn N'EvalSessionResultProjects', N'CompanyNameAlt'                  , N'Наименоване на кандидат на чужд език.'
exec spDescColumn N'EvalSessionResultProjects', N'CompanyUin'                      , N'Идентификатор на кандидат.'
exec spDescColumn N'EvalSessionResultProjects', N'CompanyUinType'                  , N'Вид на идентификатор на кандидат: 0 - ЕИК, 1 - Бултсат, 2 - ЕИК свободни професии (ЕГН), 3 - Чуждестранни фирми;'
exec spDescColumn N'EvalSessionResultProjects', N'GrantAmount'                     , N'Размер на БФП.'
exec spDescColumn N'EvalSessionResultProjects', N'SelfAmount'                      , N'Размер на съфинансиране.'
exec spDescColumn N'EvalSessionResultProjects', N'StandingPreliminaryResult'       , N'Резултат от предварително класиране'
exec spDescColumn N'EvalSessionResultProjects', N'StandingPreliminaryPoints'       , N'Точки от предварително класиране.'
exec spDescColumn N'EvalSessionResultProjects', N'EvaluationAdminAdmissResult'     , N'Резултат от оценка АСД'
exec spDescColumn N'EvalSessionResultProjects', N'StandingTechFinanceResult'       , N'Резултат от класиране ТФО.'
exec spDescColumn N'EvalSessionResultProjects', N'StandingTechFinancePoints'       , N'Точки от класиране ТФО.'
exec spDescColumn N'EvalSessionResultProjects', N'StandingComplexResult'           , N'Резултат от класиране КО.'
exec spDescColumn N'EvalSessionResultProjects', N'StandingComplexPoints'           , N'Точки от класиране КО.'
exec spDescColumn N'EvalSessionResultProjects', N'NonAdmissionReason'              , N'Основание за недопускане.'
exec spDescColumn N'EvalSessionResultProjects', N'Note'                            , N'Бележки.'
exec spDescColumn N'EvalSessionResultProjects', N'ProjectStandingNumber'           , N'Номер в класиране.'
exec spDescColumn N'EvalSessionResultProjects', N'ProjectStandingStatus'           , N'Статус в класиране: 1 - Одобрено, 2 - Резерва, 3 - Отхвърлено, 4 - Отхвърлено на ОАСД, 5 - Отхвърлено на ТФО, 6 - Отхвърлено на ПО;'
exec spDescColumn N'EvalSessionResultProjects', N'GrantAmountCorrected'            , N'Коригирана сума на БФП.'
exec spDescColumn N'EvalSessionResultProjects', N'SelfAmountCorrected'             , N'Коригирана сума на съфинансиране.'
GO
