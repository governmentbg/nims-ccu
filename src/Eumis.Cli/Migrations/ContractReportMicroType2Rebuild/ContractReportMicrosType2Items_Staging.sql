CREATE TABLE [dbo].[ContractReportMicrosType2Items_Staging] (
    [ContractReportMicrosType2ItemId]           INT           NOT NULL IDENTITY,
    [ContractReportMicroId]                     INT           NOT NULL,

    [Number]                                    NVARCHAR(200) NULL,
    [FirstName]                                 NVARCHAR(200) NULL,
    [MiddleName]                                NVARCHAR(200) NULL,
    [LastName]                                  NVARCHAR(200) NULL,
    [Uin]                                       NVARCHAR(200) NULL,
    [Gender]                                    INT           NULL,
    [Age]                                       INT           NULL,
    [Occupation]                                INT           NULL,
    [Education]                                 INT           NULL,
    [AddressDistrictId]                         INT           NULL,
    [AddressSettlementId]                       INT           NULL,
    [Phone]                                     NVARCHAR(200) NULL,
    [Email]                                     NVARCHAR(200) NULL,

    [IsEmigrant]                                BIT           NULL,
    [IsForeigner]                               BIT           NULL,
    [IsMinority]                                BIT           NULL,
    [IsGypsy]                                   BIT           NULL,
    [IsDisabledPerson]                          BIT           NULL,
    [IsHomeless]                                BIT           NULL,
    [DisadvantagedPerson]                       NVARCHAR(MAX) NULL,

    [IsLivingInUnemployedHousehold]             BIT           NULL,
    [IsLivingInUnemployedHouseholdWithChildren] BIT           NULL,
    [IsLivingInFamilyOfOneWithChildren]         BIT           NULL,

    [JoiningDate]                               DATETIME2     NULL,
    [Activity]                                  NVARCHAR(MAX) NULL,
    [ActivityPlaceDistrictId]                   INT           NULL,
    [ActivityPlaceSettlementId]                 INT           NULL,

    [ParticipationState]                        INT           NULL,
    [LeavingDate]                               DATETIME2     NULL,

    [CancelationReason]                         INT           NULL,
    [LeavingState]                              INT           NULL,

    CONSTRAINT [PK_ContractReportMicrosType2Items_Staging]                      PRIMARY KEY ([ContractReportMicrosType2ItemId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_ContractReportMicros_Staging] FOREIGN KEY ([ContractReportMicroId]) REFERENCES [dbo].[ContractReportMicros]            ([ContractReportMicroId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_AddressDistricts_Staging]     FOREIGN KEY ([AddressDistrictId])     REFERENCES [dbo].[ContractReportMicrosDistricts]   ([ContractReportMicrosDistrictId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_AddressSettlements_Staging]   FOREIGN KEY ([AddressSettlementId])   REFERENCES [dbo].[ContractReportMicrosSettlements] ([ContractReportMicrosSettlementId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_ActivityDistricts_Staging]    FOREIGN KEY ([ActivityPlaceDistrictId])     REFERENCES [dbo].[ContractReportMicrosDistricts]   ([ContractReportMicrosDistrictId]),
    CONSTRAINT [FK_ContractReportMicrosType2Items_ActivitySettlements_Staging]  FOREIGN KEY ([ActivityPlaceSettlementId])   REFERENCES [dbo].[ContractReportMicrosSettlements] ([ContractReportMicrosSettlementId]),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_Gender_Staging]              CHECK ([Gender]             IN (1, 2)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_Occupation_Staging]          CHECK ([Occupation]         IN (1, 2, 3, 4, 5, 6, 7, 8)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_Education_Staging]           CHECK ([Education]          IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_ParticipationState_Staging]  CHECK ([ParticipationState] IN (1, 2, 3, 4, 5)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_CancelationReason_Staging]   CHECK ([CancelationReason]  IN (1, 2, 3, 4, 5, 6)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_LeavingState_Staging]        CHECK ([LeavingState]       IN (1, 2, 3, 4, 5, 6, 7, 8))
)
