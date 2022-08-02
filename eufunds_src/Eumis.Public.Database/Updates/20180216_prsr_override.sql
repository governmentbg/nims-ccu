CREATE TABLE [dbo].[OpStatOverrides]
(
    [ProgrammeCode]         NVARCHAR(200) PRIMARY KEY,
    [ProjectsCount]         INT NULL,
    [ContractsCount]        INT NULL,
    [ContractedEuAmount]    DECIMAL(18,3) NULL,
    [ContractedBgAmount]    DECIMAL(18,3) NULL,
    [ContractedSelfAmount]  DECIMAL(18,3) NULL,
    [PaidEuAmount]          DECIMAL(18,3) NULL,
    [PaidBgAmount]          DECIMAL(18,3) NULL
)
GO

INSERT INTO [OpStatOverrides]
  ([ProgrammeCode], [ProjectsCount], [ContractsCount], [ContractedEuAmount], [ContractedBgAmount], [ContractedSelfAmount], [PaidEuAmount], [PaidBgAmount])
VALUES
  (N'2014BG06RDNP001', 14650, 4977,  1275257014.65, 296965790.93, 0, 257711322.92, 60012566.85)
GO
