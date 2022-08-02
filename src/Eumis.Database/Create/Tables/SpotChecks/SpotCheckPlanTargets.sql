PRINT 'SpotCheckPlanTargets'
GO

CREATE TABLE [dbo].[SpotCheckPlanTargets] (
    [SpotCheckPlanTargetId]     INT              NOT NULL IDENTITY,
    [SpotCheckPlanId]           INT              NOT NULL,
    [Type]                      INT              NOT NULL,
    [Name]                      NVARCHAR(500)    NOT NULL,

    CONSTRAINT [PK_SpotCheckPlanTargets]                   PRIMARY KEY ([SpotCheckPlanTargetId]),
    CONSTRAINT [FK_SpotCheckPlanTargets_SpotCheckPlans]    FOREIGN KEY ([SpotCheckPlanId])          REFERENCES [dbo].[SpotCheckPlans]       ([SpotCheckPlanId]),
    CONSTRAINT [CHK_SpotCheckPlanTargets_Type]             CHECK ([Type] IN (1, 2, 3, 4))
);
GO

exec spDescTable  N'SpotCheckPlanTargets', N'Обекти на проверка към годишен план за проверки на място.'
exec spDescColumn N'SpotCheckPlanTargets', N'SpotCheckPlanTargetId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'SpotCheckPlanTargets', N'SpotCheckPlanId'      , N'Идентификатор на годишен план за проверки на място.'
exec spDescColumn N'SpotCheckPlanTargets', N'Type'                 , N'Обект на проверка: 1 – Бенефициент, 2 – Изпълнител, 3 – Краен получател, 4 - Партньор.'
exec spDescColumn N'SpotCheckPlanTargets', N'Name'                 , N'Наименование на проверявания.'
GO
