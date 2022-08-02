PRINT 'HistoricContractRequests'
GO

CREATE TABLE [dbo].[HistoricContractRequests] (
    [HistoricContractRequestId]     INT             NOT NULL,
    [CreateDate]                    DATETIME2       NOT NULL,
    [EndDate]                       DATETIME2       NOT NULL,
    [StatusCode]                    NVARCHAR(50)    NOT NULL,
    [ErrorMessage]                  NVARCHAR(MAX)   NULL,
    [CountContracts]                INT             NULL,
    [Json]                          NVARCHAR(MAX)   NOT NULL,
    [ModifyDate]                    DATETIME2       NOT NULL,
    [Version]                       ROWVERSION      NOT NULL

    CONSTRAINT [PK_HistoricContractRequests]                        PRIMARY KEY ([HistoricContractRequestId])
);
GO

exec spDescTable  N'HistoricContractRequests',      N'Заявки за импортиране на договори от ИСАК.'
exec spDescColumn N'HistoricContractRequests',      N'HistoricContractRequestId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'HistoricContractRequests',      N'CreateDate'                   , N'Дата на създаване на записа.'
exec spDescColumn N'HistoricContractRequests',      N'EndDate'                      , N'Дата на приключване.'
exec spDescColumn N'HistoricContractRequests',      N'StatusCode'                   , N'Кода за състояние на заявката.'
exec spDescColumn N'HistoricContractRequests',      N'ErrorMessage'                 , N'Съобщение за грешка.'
exec spDescColumn N'HistoricContractRequests',      N'CountContracts'               , N'Брой импортирани договори.'
exec spDescColumn N'HistoricContractRequests',      N'Json'                         , N'Тяло на заявката.'
exec spDescColumn N'HistoricContractRequests',      N'ModifyDate'                   , N'Дата на последно редактиране на записа.'
exec spDescColumn N'HistoricContractRequests',      N'Version'                      , N'Версия.'
GO
