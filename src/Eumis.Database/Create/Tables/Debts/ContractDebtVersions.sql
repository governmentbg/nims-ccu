PRINT 'ContractDebtVersions'
GO

CREATE TABLE [dbo].[ContractDebtVersions] (
    [ContractDebtVersionId]     INT             NOT NULL IDENTITY,
    [ContractDebtId]            INT             NOT NULL,
    [OrderNum]                  INT             NOT NULL,
    [Status]                    INT             NOT NULL,
    [ExecutionStatus]           INT             NULL,
    [ActivationDate]            DATETIME2       NULL,
    [EuAmount]                  MONEY           NULL,
    [BgAmount]                  MONEY           NULL,
    [TotalAmount]               MONEY           NULL,
    [CertStatus]                INT             NULL,
    [CertEuAmount]              MONEY           NULL,
    [CertBgAmount]              MONEY           NULL,
    [CertTotalAmount]           MONEY           NULL,
    [CreateNotes]               NVARCHAR(MAX)   NULL,
    [CreatedByUserId]           INT             NOT NULL,
    [CreateDate]                DATETIME2       NOT NULL,
    [ModifyDate]                DATETIME2       NOT NULL,
    [Version]                   ROWVERSION      NOT NULL,

    CONSTRAINT [PK_ContractDebtVersions]                        PRIMARY KEY ([ContractDebtVersionId]),
    CONSTRAINT [FK_ContractDebtVersions_ContractDebts]          FOREIGN KEY ([ContractDebtId])          REFERENCES [dbo].[ContractDebts] ([ContractDebtId]),
    CONSTRAINT [FK_ContractDebtVersions_Users]                  FOREIGN KEY ([CreatedByUserId])         REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [CHK_ContractDebtVersions_Status]                CHECK ([Status] IN (1,2,3)),
    CONSTRAINT [CHK_ContractDebtVersions_CertStatus]            CHECK ([CertStatus] IN (1,2,3)),
    CONSTRAINT [CHK_ContractDebtVersions_ExecutionStatus]       CHECK ([ExecutionStatus] IN (1, 2, 3, 4, 5, 6, 7, 8)),
);
GO

exec spDescTable  N'ContractDebtVersions', N'Версия на дълг към договор за БФП.'
exec spDescColumn N'ContractDebtVersions', N'ContractDebtVersionId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractDebtVersions', N'ContractDebtId'            , N'Идентификатор на дълг към договор за БФП.'
exec spDescColumn N'ContractDebtVersions', N'OrderNum'                  , N'Пореден номер.'
exec spDescColumn N'ContractDebtVersions', N'Status'                    , N'Статус: 1 - Чернова, 2 - Актуален, 3 - Архивиран.'
exec spDescColumn N'ContractDebtVersions', N'ExecutionStatus'           , N'Статус: 1 – за дълга тече 14 дневен срок за доброволно възстановяване, 2 – дългът е в процес на принудително възстановяване, 3 - дългът е в процес на принудително възстановяване от НАП, 4 – дългът е напълно възстановен, 5 – дългът е закрит, поради наличие на други основания, 6 – дългът е невъзстановим; 7 – дългът е разсрочен, 8 - друго.'
exec spDescColumn N'ContractDebtVersions', N'ActivationDate'            , N'Дата на въвеждане на записа в статус актуален.'
exec spDescColumn N'ContractDebtVersions', N'EuAmount'                  , N'Дължима сума на БФП от ЕС.'
exec spDescColumn N'ContractDebtVersions', N'BgAmount'                  , N'Дължима сума на БФП от НФ.'
exec spDescColumn N'ContractDebtVersions', N'TotalAmount'               , N'Дължима сума на БФП общо.'
exec spDescColumn N'ContractDebtVersions', N'CertStatus'                , N'Сертифициран. 1 - Да, 2 - Не, 3 - Частично.'
exec spDescColumn N'ContractDebtVersions', N'CertEuAmount'              , N'Сертифицирана част ЕС.'
exec spDescColumn N'ContractDebtVersions', N'CertBgAmount'              , N'Сертифицирана част НФ.'
exec spDescColumn N'ContractDebtVersions', N'CertTotalAmount'           , N'Сертифицирана част общо.'
exec spDescColumn N'ContractDebtVersions', N'CreateNotes'               , N'Основание за промяната.'
exec spDescColumn N'ContractDebtVersions', N'CreatedByUserId'           , N'Идентификатор на потребител създал записа.'

exec spDescColumn N'ContractDebtVersions', N'CreateDate'                , N'Дата на създаване на записа.'
exec spDescColumn N'ContractDebtVersions', N'ModifyDate'                , N'Дата на последно редактиране на записа.'
exec spDescColumn N'ContractDebtVersions', N'Version'                   , N'Версия.'

GO
