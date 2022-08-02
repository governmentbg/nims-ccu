PRINT 'IndicativeAnnualWorkingProgrammes'
GO

CREATE TABLE [dbo].[IndicativeAnnualWorkingProgrammes] (
    [IndicativeAnnualWorkingProgrammeId]    INT             NOT NULL IDENTITY,
    [ProgrammeId]                           INT             NOT NULL,
    [Year]                                  INT             NOT NULL,
    [Type]                                  INT             NOT NULL,
    [Status]                                INT             NOT NULL,
    [StatusNote]                            NVARCHAR(MAX)   NULL,
    [OrderVersionNum]                       INT             NOT NULL,
    [PublicatedByUserId]                    INT             NULL,
    [PublicationDate]                       DATETIME2       NULL,
    [CreatedByUserId]                       INT             NOT NULL,
    [CreateDate]                            DATETIME2       NOT NULL,
    [ModifyDate]                            DATETIME2       NOT NULL,
    [Version]                               ROWVERSION      NOT NULL,

    CONSTRAINT [PK_IndicativeAnnualWorkingProgrammes]                   PRIMARY KEY ([IndicativeAnnualWorkingProgrammeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammes_Programmes]        FOREIGN KEY ([ProgrammeId])         REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammes_PublicatedByUser]  FOREIGN KEY ([PublicatedByUserId])  REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_IndicativeAnnualWorkingProgrammes_CreatedByUser]     FOREIGN KEY ([CreatedByUserId])     REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammes_Year]             CHECK   ([Year] IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammes_Type]             CHECK   ([Type] IN (1, 2)),
    CONSTRAINT [CHK_IndicativeAnnualWorkingProgrammes_Status]           CHECK   ([Status] IN (1, 2, 3, 4)),
);
GO

exec spDescTable  N'IndicativeAnnualWorkingProgrammes', N'Индикативни годишни работни програми.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'IndicativeAnnualWorkingProgrammeId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'ProgrammeId'                          , N'Идентификатор на оперативна програма.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'Year'                                 , N'Година.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'Type'                                 , N'Тип: 1 - ИГРП, 2 - ИГРП интегрирани процедури.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'Status'                               , N'Статус: 1 - Чернова, 2 - Публикувана, 3 - Архивирана, 4 - Анулирана.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'StatusNote'                           , N'Бележка.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'OrderVersionNum'                      , N'Номер на версията.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'PublicatedByUserId'                   , N'Публикувано от.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'PublicationDate'                      , N'Дата на публикуване.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'CreatedByUserId'                      , N'Създадено от.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'CreateDate'                           , N'Дата на създаване на записа.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'ModifyDate'                           , N'Дата на последно редактиране на записа.'
exec spDescColumn N'IndicativeAnnualWorkingProgrammes', N'Version'                              , N'Версия.'
GO
