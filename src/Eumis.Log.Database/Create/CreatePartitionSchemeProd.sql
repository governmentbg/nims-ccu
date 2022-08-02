PRINT 'Create production partition scheme'
GO

USE [$(dbName)]
GO

CREATE PARTITION SCHEME psLogs
AS PARTITION pfLogs TO (
  [FG_Data], -- before 2015
  [FG_Data1] ,[FG_Data1] ,[FG_Data1] ,[FG_Data1] , -- 2015
  [FG_Data2] ,[FG_Data2] ,[FG_Data2] ,[FG_Data2] , -- 2016
  [FG_Data3] ,[FG_Data3] ,[FG_Data3] ,[FG_Data3] , -- 2017
  [FG_Data4] ,[FG_Data4] ,[FG_Data4] ,[FG_Data4] , -- 2018
  [FG_Data5] ,[FG_Data5] ,[FG_Data5] ,[FG_Data5] , -- 2019
  [FG_Data6] ,[FG_Data6] ,[FG_Data6] ,[FG_Data6] , -- 2020
  [FG_Data7] ,[FG_Data7] ,[FG_Data7] ,[FG_Data7] , -- 2021
  [FG_Data8] ,[FG_Data8] ,[FG_Data8] ,[FG_Data8] , -- 2022
  [FG_Data] -- after 2022
);
GO
