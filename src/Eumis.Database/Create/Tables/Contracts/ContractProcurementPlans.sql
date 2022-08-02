PRINT 'ContractProcurementPlans'
GO

CREATE TABLE [dbo].[ContractProcurementPlans] (
    [ContractProcurementPlanId] INT                 NOT NULL IDENTITY,
    [ContractId]                INT                 NOT NULL,
    [Gid]                       UNIQUEIDENTIFIER    NOT NULL UNIQUE,

    [Name]                      NVARCHAR(MAX)       NOT NULL,
    [ErrandAreaId]              INT                 NOT NULL,
    [ErrandLegalActId]          INT                 NOT NULL,
    [ErrandTypeId]              INT                 NOT NULL,
    [Amount]                    MONEY               NOT NULL,
    [Description]               NVARCHAR(MAX)       NOT NULL,
    [MAPreliminaryControl]      INT                 NOT NULL,
    [PPAPreliminaryControl]     INT                 NOT NULL,
    [InternetAddress]           NVARCHAR(MAX)       NULL,
    [ExpectedAmount]            MONEY               NOT NULL,
    [PPANumber]                 NVARCHAR(MAX)       NULL,
    [PlanDate]                  DATETIME2           NULL,
    [NoticeDate]                DATETIME2           NULL,
    [OffersDeadlineDate]        DATETIME2           NULL,
    [AnnouncedDate]             DATETIME2           NULL,
    [TerminatedDate]            DATETIME2           NULL,

    CONSTRAINT [PK_ContractProcurementPlans]                        PRIMARY KEY ([ContractProcurementPlanId]),
    CONSTRAINT [FK_ContractProcurementPlans_Contracts]              FOREIGN KEY ([ContractId])                  REFERENCES [dbo].[Contracts] ([ContractId]),
    CONSTRAINT [FK_ContractProcurementPlans_ErrandAreas]            FOREIGN KEY ([ErrandAreaId])                REFERENCES [dbo].[ErrandAreas] ([ErrandAreaId]),
    CONSTRAINT [FK_ContractProcurementPlans_ErrandLegalActs]        FOREIGN KEY ([ErrandLegalActId])            REFERENCES [dbo].[ErrandLegalActs] ([ErrandLegalActId]),
    CONSTRAINT [FK_ContractProcurementPlans_ErrandTypes]            FOREIGN KEY ([ErrandTypeId])                REFERENCES [dbo].[ErrandTypes] ([ErrandTypeId]),
    CONSTRAINT [CHK_ContractProcurementPlans_MAPreliminaryControl]  CHECK       ([MAPreliminaryControl] IN (1, 2, 3)),
    CONSTRAINT [CHK_ContractProcurementPlans_PPAPreliminaryControl] CHECK       ([PPAPreliminaryControl] IN (1, 2, 3))
);
GO

exec spDescTable  N'ContractProcurementPlans', N' Процедури за избор на изпълнител към договор.'
exec spDescColumn N'ContractProcurementPlans', N'ContractProcurementPlanId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ContractProcurementPlans', N'ContractId'                , N'Идентификатор на договор.'
exec spDescColumn N'ContractProcurementPlans', N'Gid'                       , N'Публичен идентификатор.'

exec spDescColumn N'ContractProcurementPlans', N'Name'                      , N'Предмет на предвидената процедура.'
exec spDescColumn N'ContractProcurementPlans', N'ErrandAreaId'              , N'Обект на процедурата.'
exec spDescColumn N'ContractProcurementPlans', N'ErrandLegalActId'          , N'Приложим нормативен акт.'
exec spDescColumn N'ContractProcurementPlans', N'ErrandTypeId'              , N'Тип на процедурата.'
exec spDescColumn N'ContractProcurementPlans', N'Amount'                    , N'Прогнозна стойност.'
exec spDescColumn N'ContractProcurementPlans', N'Description'               , N'Описание.'
exec spDescColumn N'ContractProcurementPlans', N'MAPreliminaryControl'      , N'Процедурата е преминала през предварителен контрол от УО.'
exec spDescColumn N'ContractProcurementPlans', N'PPAPreliminaryControl'     , N'Процедурата е преминала през предварителен контрол от АОП.'
exec spDescColumn N'ContractProcurementPlans', N'InternetAddress'           , N'Интернет адрес.'
exec spDescColumn N'ContractProcurementPlans', N'ExpectedAmount'            , N'Прогнозна стойност съгласно обявление.'
exec spDescColumn N'ContractProcurementPlans', N'PPANumber'                 , N'Уникален номер от регистъра на АОП за процедури проведени по ЗОП.'
exec spDescColumn N'ContractProcurementPlans', N'PlanDate'                  , N'Планирана дата на обявяване.'
exec spDescColumn N'ContractProcurementPlans', N'NoticeDate'                , N'Дата на обявление на процедура за външно възлагане.'
exec spDescColumn N'ContractProcurementPlans', N'OffersDeadlineDate'        , N'Крайна дата за подаване на оферти.'
exec spDescColumn N'ContractProcurementPlans', N'AnnouncedDate'             , N'Служебна дата на обявление на процедура за външно възлагане.'
exec spDescColumn N'ContractProcurementPlans', N'TerminatedDate'            , N'Служебна дата на прекратяване на процедура за външно възлагане.'
GO
