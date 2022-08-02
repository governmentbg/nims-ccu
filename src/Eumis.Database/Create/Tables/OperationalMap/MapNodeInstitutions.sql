PRINT 'MapNodeInstitutions'
GO

CREATE TABLE [dbo].[MapNodeInstitutions](
    [MapNodeId]          INT             NOT NULL,
    [InstitutionId]      INT               NOT NULL,
    [InstitutionTypeId]  INT               NOT NULL,
    [ContactName]        NVARCHAR(200)     NOT NULL,
    [ContactPosition]    NVARCHAR(50)      NOT NULL,
    [ContactPhone]       NVARCHAR(50)      NOT NULL,
    [ContactFax]         NVARCHAR(50)      NOT NULL,
    [ContactEmail]       NVARCHAR(50)      NOT NULL

    CONSTRAINT [PK_MapNodeInstitutions]                   PRIMARY KEY   ([MapNodeId], [InstitutionId]),
    CONSTRAINT [FK_MapNodeInstitutions_MapNodes]          FOREIGN KEY   ([MapNodeId])         REFERENCES [dbo].[MapNodes] ([MapNodeId]),
    CONSTRAINT [FK_MapNodeInstitutions_Institutions]      FOREIGN KEY   ([InstitutionId])     REFERENCES [dbo].[Institutions] ([InstitutionId]),
    CONSTRAINT [FK_MapNodeInstitutions_InstitutionTypes]  FOREIGN KEY   ([InstitutionTypeId]) REFERENCES [dbo].[InstitutionTypes] ([InstitutionTypeId])
);

GO

exec spDescTable  N'MapNodeInstitutions', N'Институции на елемент на оперативна карта.'
exec spDescColumn N'MapNodeInstitutions', N'MapNodeId'          , N'Идентификатор на елемент на оперативна карта.'
exec spDescColumn N'MapNodeInstitutions', N'InstitutionId'      , N'Идентификатор на Институция.'
exec spDescColumn N'MapNodeInstitutions', N'InstitutionTypeId'  , N'Тип(Роля) на Институцията по Оперативната програма.'
exec spDescColumn N'MapNodeInstitutions', N'ContactName'        , N'Име на лице за контакти.'
exec spDescColumn N'MapNodeInstitutions', N'ContactPosition'    , N'Позиция на лицето за контакти.'
exec spDescColumn N'MapNodeInstitutions', N'ContactPhone'       , N'Телефон на лицето за контакти'
exec spDescColumn N'MapNodeInstitutions', N'ContactFax'         , N'Факс на лицето за контакти'
exec spDescColumn N'MapNodeInstitutions', N'ContactEmail'       , N'Електронна поща на лицето за контакти'

GO
