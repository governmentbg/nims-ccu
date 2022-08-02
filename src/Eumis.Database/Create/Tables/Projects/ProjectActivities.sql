PRINT 'ProjectActivities'
GO

CREATE TABLE [dbo].[ProjectActivities] (
    [ProjectActivityId] INT             NOT NULL IDENTITY,
    [ProjectId]         INT             NOT NULL,
    [Code]              NVARCHAR(50)    NOT NULL,
    [CompanyId]         INT             NOT NULL,
    [Name]              NVARCHAR(MAX)   NOT NULL,
    [ExecutionMethod]   NVARCHAR(MAX)   NOT NULL,
    [Result]            NVARCHAR(MAX)   NOT NULL,
    [StartShift]        INT             NOT NULL,
    [Duration]          INT             NOT NULL,
    [Amount]            MONEY           NOT NULL,

    CONSTRAINT [PK_ProjectActivities]               PRIMARY KEY ([ProjectActivityId]),
    CONSTRAINT [FK_ProjectActivities_Projects]      FOREIGN KEY ([ProjectId])       REFERENCES [dbo].[Projects] ([ProjectId]),
    CONSTRAINT [FK_ProjectActivities_Companies]     FOREIGN KEY ([CompanyId])       REFERENCES [dbo].[Companies] ([CompanyId])
);
GO

exec spDescTable  N'ProjectActivities', N'Дейности на проектно предложение.'
exec spDescColumn N'ProjectActivities', N'ProjectActivityId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ProjectActivities', N'ProjectId'              , N'Идентификатор на проектно предложние'
exec spDescColumn N'ProjectActivities', N'Code'                   , N'Дейност.'
exec spDescColumn N'ProjectActivities', N'CompanyId'              , N'Идентификатор на компанията изпълнител.'
exec spDescColumn N'ProjectActivities', N'Name'                   , N'Описание'
exec spDescColumn N'ProjectActivities', N'ExecutionMethod'        , N'Начин на изпълнение'
exec spDescColumn N'ProjectActivities', N'Result'                 , N'Резултат'
exec spDescColumn N'ProjectActivities', N'StartShift'             , N'Месец за стартиране на дейността (пореден месец от стартиране на проекта)'
exec spDescColumn N'ProjectActivities', N'Duration'               , N'Продължителност'
exec spDescColumn N'ProjectActivities', N'Amount'                 , N'Стойност.'

GO

