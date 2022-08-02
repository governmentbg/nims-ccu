PRINT 'Indicators'
GO

CREATE TABLE [dbo].[IndicatorItemTypes] (
    [IndicatorItemTypeId]           INT                NOT NULL    IDENTITY,
    [Name]                          NVARCHAR(1000)     NOT NULL,
    [NameAlt]                       NVARCHAR(1000)     NOT NULL,

    [CreateDate]                    DATETIME2          NOT NULL,
    [ModifyDate]                    DATETIME2          NOT NULL,
    [Version]                       ROWVERSION         NOT NULL,

    CONSTRAINT [PK_IndicatorItemTypes]                 PRIMARY KEY ([IndicatorItemTypeId]));
GO


exec spDescTable  N'IndicatorItemTypes', N'Видове индикатори.'
exec spDescColumn N'IndicatorItemTypes', N'IndicatorItemTypeId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'IndicatorItemTypes', N'Name'                 , N'Наименование.'
exec spDescColumn N'IndicatorItemTypes', N'NameAlt'              , N'Наименование на английски език.'
exec spDescColumn N'IndicatorItemTypes', N'CreateDate'           , N'Дата на създаване.'
exec spDescColumn N'IndicatorItemTypes', N'ModifyDate'           , N'Дата на последна промяна.'
exec spDescColumn N'IndicatorItemTypes', N'Version'              , N'Версия.'
GO
