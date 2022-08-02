ALTER TABLE [RegOfferXmls]
    ADD        [Status]                                     INT         NOT NULL    CONSTRAINT DF_RegOfferXmls_Status DEFAULT 2,
               [SubmitDate]                                 DATETIME2   NULL,
    CONSTRAINT [CHK_RegOfferXmls_Status]                    CHECK       ([Status] IN (1, 2, 3)),
    CONSTRAINT [CHK_RegOfferXmls_Tenderer]                  CHECK       ([Status] = 1 OR [Tenderer] IS NOT NULL),
    CONSTRAINT [CHK_RegOfferXmls_Email]                     CHECK       ([Status] = 1 OR [Email] IS NOT NULL);
GO

ALTER TABLE [RegOfferXmls]
    DROP CONSTRAINT DF_RegOfferXmls_Status;


ALTER TABLE [RegOfferXmls]
    ALTER COLUMN [Tenderer] NVARCHAR(200) NULL;

ALTER TABLE [RegOfferXmls]
    ALTER COLUMN [Email] NVARCHAR(200) NULL;


ALTER TABLE [RegOfferXmls]
    ALTER COLUMN [UinType] INT NULL;

ALTER TABLE [RegOfferXmls]
    DROP CONSTRAINT [CHK_RegOfferXmls_UinType];

ALTER TABLE [RegOfferXmls]
    ADD CONSTRAINT [CHK_RegOfferXmls_UinType] CHECK ([Status] = 1 OR ([UinType] IS NOT NULL AND [UinType] IN (0, 1, 2, 3)));


UPDATE [RegOfferXmls]
    SET [SubmitDate] = [CreateDate] WHERE [Status] = 2;

ALTER TABLE [RegOfferXmls]
    ADD CONSTRAINT [CHK_RegOfferXmls_SubmitDate] CHECK ([Status] = 1 OR [SubmitDate] IS NOT NULL);


exec spDescColumn N'RegOfferXmls', N'Status'                                , N'Статус: 1 - Чернова, 2 - Въведена, 3 - Оттеглена';
exec spDescColumn N'RegOfferXmls', N'SubmitDate'                            , N'Дата на подаване';
