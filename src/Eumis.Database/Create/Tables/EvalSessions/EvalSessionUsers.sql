PRINT 'EvalSessionUsers'
GO

CREATE TABLE [dbo].[EvalSessionUsers] (
    [EvalSessionUserId]     INT             NOT NULL IDENTITY,
    [EvalSessionId]         INT             NOT NULL,
    [UserId]                INT             NOT NULL,
    [Type]                  INT             NOT NULL,
    [Position]              NVARCHAR(300)   NULL,
    [Status]                INT             NOT NULL,

    CONSTRAINT [PK_EvalSessionUsers]                        PRIMARY KEY ([EvalSessionUserId]),
    CONSTRAINT [CHK_EvalSessionUsers_Type]                  CHECK       ([Type] IN (1, 2, 3, 4)),
    CONSTRAINT [UQ_EvalSessionUsers_EvalSeesuin_User_Type]  UNIQUE      ([EvalSessionId], [UserId], [Type]),
    CONSTRAINT [FK_EvalSessionUsers_EvalSessions]           FOREIGN KEY ([EvalSessionId])       REFERENCES [dbo].[EvalSessions] ([EvalSessionId])
);
GO



exec spDescTable  N'EvalSessionUsers', N'Членове към оценителна сесия.'
exec spDescColumn N'EvalSessionUsers', N'EvalSessionUserId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EvalSessionUsers', N'EvalSessionId'         , N'Идентификатор на оценителна сесия.'
exec spDescColumn N'EvalSessionUsers', N'UserId'                , N'Идентификатор на потребител.'
exec spDescColumn N'EvalSessionUsers', N'Type'                  , N'Тип на потребителя в сесията: 1 - Администратор на сесия, 2 – Оценител, 3 – Помощник оценител, 4 - Наблюдател'
exec spDescColumn N'EvalSessionUsers', N'Position'              , N'Позиция.'
exec spDescColumn N'EvalSessionUsers', N'Status'                , N'Статус на потребителя в сесията: 1 - Добавен, 2 - Активиран, 3 - Деактивиран'


GO

