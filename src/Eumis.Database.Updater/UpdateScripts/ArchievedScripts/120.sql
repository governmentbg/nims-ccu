GO

ALTER TABLE [dbo].[ContractReportMicrosType2Items]
DROP COLUMN [IsDisadvantaged];
GO

ALTER TABLE [dbo].[ContractReportMicrosType2Items]
ADD [DisadvantagedPerson]                       NVARCHAR(MAX) NULL,
    [CancelationReason]                         INT           NULL,
    [LeavingState]                              INT           NULL,
    CONSTRAINT [CHK_ContractReportMicrosType2Items_CancelationReason]   CHECK ([CancelationReason]  IN (1, 2, 3, 4)),
    CONSTRAINT [CHK_ContractReportMicrosType2Items_LeavingState]        CHECK ([LeavingState]       IN (1, 2, 3, 4, 5, 6));
GO
