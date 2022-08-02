PRINT 'LoginCertificates'
GO

CREATE TABLE [dbo].[LoginCertificates](
    [LoginCertificateId]    INT              NOT NULL IDENTITY,
	[LoginDate]				Datetime not null,
    [IP]                    NVARCHAR (50)   NOT NULL,
    [CertificateFile]       VARBINARY (MAX) NULL,
	[CertificateIssuer]     NVARCHAR (MAX)  NULL,
	[CertificatePolicies]   NVARCHAR (MAX)  NULL,
    [CertificateSubject]    NVARCHAR (MAX)  NULL,
	[AlternativeSubject]    NVARCHAR (MAX)  NULL,
    [CertificateThumbprint] NVARCHAR (200)  NULL,
	[ErrorCode]             NVARCHAR (10)   NULL,
	[IsIisErrorOccurred] BIT NOT NULL,
	[IsLoginSuccessful] BIT NOT NULL,

    CONSTRAINT [PK_LoginCertificates] PRIMARY KEY ([LoginCertificateId])
);
GO