PRINT 'SpotCheckPlanDocs'
GO

CREATE TABLE [dbo].[SpotCheckPlanDocs] (
    [SpotCheckPlanDocId]  INT               NOT NULL IDENTITY,
    [SpotCheckPlanId]     INT               NOT NULL,
    [Description]         NVARCHAR(MAX)     NOT NULL,
    [FileName]            NVARCHAR(200)     NOT NULL,
    [FileKey]             UNIQUEIDENTIFIER  NOT NULL,

    CONSTRAINT [PK_SpotCheckPlanDocs]                 PRIMARY KEY ([SpotCheckPlanDocId]),
    CONSTRAINT [FK_SpotCheckPlanDocs_SpotCheckPlans]  FOREIGN KEY ([SpotCheckPlanId]) REFERENCES [dbo].[SpotCheckPlans] ([SpotCheckPlanId]),
    CONSTRAINT [FK_SpotCheckPlanDocs_Blobs]           FOREIGN KEY ([FileKey])         REFERENCES [dbo].[Blobs] ([Key]),
);
GO

exec spDescTable  N'SpotCheckPlanDocs', N'Документи към годишни планове за проверки на място.'
exec spDescColumn N'SpotCheckPlanDocs', N'SpotCheckPlanDocId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckPlanDocs', N'SpotCheckPlanId'   , N'Идентификатор на проверка на място.'
exec spDescColumn N'SpotCheckPlanDocs', N'Description'       , N'Описание.'
exec spDescColumn N'SpotCheckPlanDocs', N'FileName'          , N'Наименование.'
exec spDescColumn N'SpotCheckPlanDocs', N'FileKey'           , N'Идентификатор на файл.'
GO
