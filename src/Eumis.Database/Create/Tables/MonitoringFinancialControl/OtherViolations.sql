PRINT 'OtherViolations'
GO

CREATE TABLE [dbo].[OtherViolations] (
    [OtherViolationId]    INT                NOT NULL IDENTITY,
    [Gid]                 UNIQUEIDENTIFIER   NOT NULL,
    [Name]                NVARCHAR(MAX)      NOT NULL,
    [Code]                NVARCHAR(100)      NULL,
    [IsActive]            BIT                NOT NULL,
    [CreateDate]          DATETIME2          NOT NULL,
    [ModifyDate]          DATETIME2          NOT NULL,
    [Version]             ROWVERSION         NOT NULL,

    CONSTRAINT [PK_OtherViolations] PRIMARY KEY ([OtherViolationId])
);
GO

exec spDescTable  N'OtherViolations', N'Други констатирани нарушения.'
exec spDescColumn N'OtherViolations', N'OtherViolationId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'OtherViolations', N'Gid'                , N'Уникален публичен системно генериран идентификатор.'
exec spDescColumn N'OtherViolations', N'Name'               , N'Наименование.'
exec spDescColumn N'OtherViolations', N'Code'               , N'Код.'
exec spDescColumn N'OtherViolations', N'IsActive'           , N'Маркер за активност.'
exec spDescColumn N'OtherViolations', N'CreateDate'         , N'Дата на създаване на записа.'
exec spDescColumn N'OtherViolations', N'ModifyDate'         , N'Дата на последно редактиране на записа.'
exec spDescColumn N'OtherViolations', N'Version'            , N'Версия.'
