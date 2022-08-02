--executed on 2019/09/11 ~ 09:30

--Contract /BG05SFOP001-2.008-0001-C02/ 
--ContractProcurement /OrderNum 3/
--FileKey /2EDEC393-7374-4163-BF09-0FF7E10AF5BB/
INSERT INTO [dbo].[BlobContents]
    ([BlobContentId], [PartitionId], [Hash], [Size],
    [Content], [IsDeleted], [CreateDate], [DeleteDate])
SELECT
    [bc].[BlobContentId], [bcl].[PartitionId], [bc].[Hash], [bc].[Size],
    [bc].[Content], [bc].[IsDeleted], [bcl].[CreateDate], [bcl].[DeleteDate]
FROM
    [EumisSrv].[EumisBlobs1].[dbo].[BlobContents] bc
    JOIN [EumisSrv].[Eumis].[dbo].[BlobContentLocations] bcl 
        ON [bc].[BlobContentId] = [bcl].[BlobContentId]
WHERE 
    [bc].[BlobContentId] = 1769198
GO

--Contract /BG05SFOP001-2.008-0001-C02/ 
--ContractProcurement /OrderNum 3/
--FileKey /59C60A17-BFA4-428B-9BCC-F8E15D369F90/
INSERT INTO [dbo].[BlobContents]
    ([BlobContentId], [PartitionId], [Hash], [Size],
    [Content], [IsDeleted], [CreateDate], [DeleteDate])
SELECT
    [bc].[BlobContentId], [bcl].[PartitionId], [bc].[Hash], [bc].[Size],
    [bc].[Content], [bc].[IsDeleted], [bcl].[CreateDate], [bcl].[DeleteDate]
FROM
    [EumisSrv].[EumisBlobs1].[dbo].[BlobContents] bc
    JOIN [EumisSrv].[Eumis].[dbo].[BlobContentLocations] bcl 
        ON [bc].[BlobContentId] = [bcl].[BlobContentId]
WHERE 
    [bc].[BlobContentId] = 1769236
GO
