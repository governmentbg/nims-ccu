PRINT 'ProjectErrands'
GO

CREATE TABLE [dbo].[ProjectErrands] (
    [ProjectErrandId]   INT             NOT NULL IDENTITY,
    [ProjectId]         INT             NOT NULL,
    [Name]              NVARCHAR(MAX)   NOT NULL,
    [ErrandAreaId]      INT             NOT NULL,
    [ErrandTypeId]      INT             NOT NULL,
    [ErrandLegalActId]  INT             NOT NULL,
    [Amount]            MONEY           NOT NULL,
    [PlanDate]          DATETIME2       NOT NULL,
    [Description]       NVARCHAR(MAX)   NULL,

    CONSTRAINT [PK_ProjectErrands]                  PRIMARY KEY ([ProjectErrandId]),
    CONSTRAINT [FK_ProjectErrands_Projects]         FOREIGN KEY ([ProjectId])           REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectErrands_ErrandAreas]      FOREIGN KEY ([ErrandAreaId])        REFERENCES [dbo].[ErrandAreas] ([ErrandAreaId]),
    CONSTRAINT [FK_ProjectErrands_ErrandTypes]      FOREIGN KEY ([ErrandTypeId])        REFERENCES [dbo].[ErrandTypes] ([ErrandTypeId]),
    CONSTRAINT [FK_ProjectErrands_ErrandLegalActs]  FOREIGN KEY ([ErrandLegalActId])    REFERENCES [dbo].[ErrandLegalActs] ([ErrandLegalActId])
);
GO

exec spDescTable  N'ProjectErrands', N'Външно възлагане на проектно предложение.'
exec spDescColumn N'ProjectErrands', N'ProjectErrandId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectErrands', N'ProjectId'           , N'Идентификатор на проектно предложние'
exec spDescColumn N'ProjectErrands', N'Name'                , N'Наименование'
exec spDescColumn N'ProjectErrands', N'ErrandAreaId'        , N'Идентификатор на приложна област на процедура за външно  възлагане'
exec spDescColumn N'ProjectErrands', N'ErrandTypeId'        , N'Идентификатор на тип на процедурата за външно възлагане'
exec spDescColumn N'ProjectErrands', N'ErrandLegalActId'    , N'Идентификатор на номенклатура на приложим нормативен акт на процедура за външно възлагане'
exec spDescColumn N'ProjectErrands', N'Amount'              , N'Стойност'
exec spDescColumn N'ProjectErrands', N'PlanDate'            , N'Планирана дата на обявяване'
exec spDescColumn N'ProjectErrands', N'Description'         , N'Описание'

GO
