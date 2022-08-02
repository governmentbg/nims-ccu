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
