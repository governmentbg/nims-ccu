GO

ALTER TABLE [CompanyLegalTypes] ADD [CodeCommercialRegister] NVARCHAR(10) NULL;
ALTER TABLE [CompanyLegalTypes] ADD [CodeBulstatRegister] NVARCHAR(10) NULL;
GO

GO
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '505'  WHERE CompanyLegalTypeId =  1;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '1187' WHERE CompanyLegalTypeId =  2;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '498'  WHERE CompanyLegalTypeId =  3;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '1218' WHERE CompanyLegalTypeId =  4;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '1216' WHERE CompanyLegalTypeId =  5;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '496'  WHERE CompanyLegalTypeId =  6;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = 'АД'  , CodeBulstatRegister = NULL   WHERE CompanyLegalTypeId =  7;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = 'ЕАД' , CodeBulstatRegister = NULL   WHERE CompanyLegalTypeId =  8;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = 'ООД' , CodeBulstatRegister = NULL   WHERE CompanyLegalTypeId =  9;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = 'ЕООД', CodeBulstatRegister = NULL   WHERE CompanyLegalTypeId = 10;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = 'ЕТ'  , CodeBulstatRegister = NULL   WHERE CompanyLegalTypeId = 11;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = 'КД'  , CodeBulstatRegister = NULL   WHERE CompanyLegalTypeId = 12;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = 'КДА' , CodeBulstatRegister = NULL   WHERE CompanyLegalTypeId = 13;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = 'СД'  , CodeBulstatRegister = NULL   WHERE CompanyLegalTypeId = 14;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '486'  WHERE CompanyLegalTypeId = 16;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '485'  WHERE CompanyLegalTypeId = 18;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '488'  WHERE CompanyLegalTypeId = 20;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '517'  WHERE CompanyLegalTypeId = 21;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '521'  WHERE CompanyLegalTypeId = 22;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '522'  WHERE CompanyLegalTypeId = 23;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '502'  WHERE CompanyLegalTypeId = 24;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '503'  WHERE CompanyLegalTypeId = 26;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '1534' WHERE CompanyLegalTypeId = 29;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '1223' WHERE CompanyLegalTypeId = 33;
UPDATE [CompanyLegalTypes] SET CodeCommercialRegister = NULL  , CodeBulstatRegister = '533'  WHERE CompanyLegalTypeId = 35;
GO
