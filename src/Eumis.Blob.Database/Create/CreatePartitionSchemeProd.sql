PRINT 'Create production partition scheme'
GO

USE [$(dbName)]
GO

CREATE PARTITION SCHEME psBlobContents
AS PARTITION pfBlobContents TO (
  [FG_Data], -- i <= 99
  [FG_Data1] ,[FG_Data1] ,[FG_Data1] ,[FG_Data1] , -- i <= 103
  [FG_Data2] ,[FG_Data2] ,[FG_Data2] ,[FG_Data2] , -- i <= 107
  [FG_Data3] ,[FG_Data3] ,[FG_Data3] ,[FG_Data3] , -- i <= 111
  [FG_Data4] ,[FG_Data4] ,[FG_Data4] ,[FG_Data4] , -- i <= 115
  [FG_Data5] ,[FG_Data5] ,[FG_Data5] ,[FG_Data5] , -- i <= 119
  [FG_Data6] ,[FG_Data6] ,[FG_Data6] ,[FG_Data6] , -- i <= 123
  [FG_Data7] ,[FG_Data7] ,[FG_Data7] ,[FG_Data7] , -- i <= 127
  [FG_Data8] ,[FG_Data8] ,[FG_Data8] ,[FG_Data8] , -- i <= 131
  [FG_Data9] ,[FG_Data9] ,[FG_Data9] ,[FG_Data9] , -- i <= 135
  [FG_Data10],[FG_Data10],[FG_Data10],[FG_Data10], -- i <= 139
  [FG_Data11],[FG_Data11],[FG_Data11],[FG_Data11], -- i <= 143
  [FG_Data12],[FG_Data12],[FG_Data12],[FG_Data12], -- i <= 147
  [FG_Data] -- i >= 148
);
GO
