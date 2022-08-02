SET IDENTITY_INSERT [BlobContentLocations] ON
GO

INSERT INTO [BlobContentLocations]
    ([BlobContentLocationId], [BlobContentId], [PartitionId], [IsDeleted], [CreateDate], [ContentDbCSName], [Hash]                                                             , [Size])
VALUES
    (1                      , 1              , 116          , 0          , GETDATE()   , N'Blobs1'        , N'465C0ECBDDD91A4B9163798AA76D7B1EAFA40663797BE00F20059D97082A2543', 8848  ),
    (2                      , 2              , 117          , 0          , GETDATE()   , N'Blobs1'        , N'7BFBCE3BC3FC91D9DA6D21E40524E11669E467A2B99441DE6D8C9EA7BD560311', 12573 ),
    (3                      , 3              , 118          , 0          , GETDATE()   , N'Blobs1'        , N'823D8B8C1943DF8D17FDFC3D5F8B238A41C0139FE534E7D42D52A0ED17AEAE4A', 293   )
GO

SET IDENTITY_INSERT [BlobContentLocations] OFF
GO
