-- 83c5581 Fix companyType, companyLegalType and companySize nomenclatures
GO

ALTER TABLE [CompanySizeTypes]
ADD [Order]             DECIMAL(15,3)       NOT NULL DEFAULT 0,
    [Alias]             NVARCHAR(200)       NULL;
GO

UPDATE [CompanySizeTypes]
SET [Order] = [CompanySizeTypeId],
    [Alias] = CASE
                WHEN [CompanySizeTypeId] = 5 THEN N'inapplicable'
                ELSE NULL
              END;
 GO

ALTER TABLE [CompanyTypes]
ADD [Order]             DECIMAL(15,3)       NOT NULL DEFAULT 0,
    [Alias]             NVARCHAR(200)       NULL;
GO

UPDATE [CompanyTypes]
SET [Name] = CASE
                WHEN [CompanyTypeId] = 1 THEN N'Нестопанска организация'
                WHEN [CompanyTypeId] = 2 THEN N'Учебно заведение'
                WHEN [CompanyTypeId] = 3 THEN N'Държавна администрация'
                WHEN [CompanyTypeId] = 4 THEN N'Компания'
                WHEN [CompanyTypeId] = 5 THEN N'Съдебна система'
                WHEN [CompanyTypeId] = 6 THEN N'Друга'
                WHEN [CompanyTypeId] = 7 THEN N'Медицинско заведение'
              END,
    [Order] = CASE
                WHEN [CompanyTypeId] = 6 THEN 7
                WHEN [CompanyTypeId] = 7 THEN 6
                ELSE [CompanyTypeId]
              END,
    [Alias] = CASE
                WHEN [CompanyTypeId] = 4 THEN N'company'
                ELSE NULL
              END;
 GO

ALTER TABLE [CompanyLegalTypes]
ADD [Order]             DECIMAL(15,3)       NOT NULL DEFAULT 0;
GO

--3, Държавни институции
UPDATE [CompanyLegalTypes]
SET [Name] = CASE
                WHEN [CompanyLegalTypeId] = 1 THEN N'Общинска администрация'
                WHEN [CompanyLegalTypeId] = 2 THEN N'Областна администрация'
                WHEN [CompanyLegalTypeId] = 3 THEN N'АМС / министерство'
                WHEN [CompanyLegalTypeId] = 4 THEN N'Изпълнителна агенция / административните структури, създадени с нормативен акт'
                WHEN [CompanyLegalTypeId] = 5 THEN N'Държавна агенция / държавна комисия'
                WHEN [CompanyLegalTypeId] = 6 THEN N'Други'
              END,
    [Order] = CASE
                WHEN [CompanyLegalTypeId] = 1 THEN 5
                WHEN [CompanyLegalTypeId] = 2 THEN 4
                WHEN [CompanyLegalTypeId] = 3 THEN 1
                WHEN [CompanyLegalTypeId] = 4 THEN 3
                WHEN [CompanyLegalTypeId] = 5 THEN 2
                WHEN [CompanyLegalTypeId] = 6 THEN 7
              END
WHERE [CompanyTypeId] = 3;
GO

SET IDENTITY_INSERT [CompanyLegalTypes] ON
INSERT INTO [CompanyLegalTypes]
    ([CompanyLegalTypeId], [Gid]                                  , [CompanyTypeId], [Name]                                      , [Order])
VALUES
    (35                  , N'BEF68C51-EB45-4C09-ADE6-0A639D9082A1', 3              , N'Специализирана териториална администрация', 6      );
SET IDENTITY_INSERT [CompanyLegalTypes] OFF
GO

--4, Компании
UPDATE [CompanyLegalTypes]
SET [Order] = CASE
                WHEN [CompanyLegalTypeId] = 8 THEN 3
                WHEN [CompanyLegalTypeId] = 9 THEN 2
                ELSE [CompanyLegalTypeId] - 6
              END
WHERE [CompanyTypeId] = 4;
GO

--1, Нестопански организации
UPDATE [CompanyLegalTypes]
SET [Order] = [CompanyLegalTypeId] - 15
WHERE [CompanyTypeId] = 1;
GO

--2, Учебни заведения
UPDATE [CompanyLegalTypes]
SET [Name] = CASE
                WHEN [CompanyLegalTypeId] = 22 THEN N'Училище'
                ELSE [Name]
              END,
    [Order] = [CompanyLegalTypeId] - 20
WHERE [CompanyTypeId] = 2;
GO


--5, Съдебна система
UPDATE [CompanyLegalTypes]
SET [Order] = CASE
                WHEN [CompanyLegalTypeId] = 24 THEN 3
                WHEN [CompanyLegalTypeId] = 25 THEN 5
                WHEN [CompanyLegalTypeId] = 26 THEN 4
              END
WHERE [CompanyTypeId] = 5;
GO

SET IDENTITY_INSERT [CompanyLegalTypes] ON
INSERT INTO [CompanyLegalTypes]
    ([CompanyLegalTypeId], [Gid]                                  , [CompanyTypeId], [Name]                , [Order])
VALUES
    (37                  , N'6ACC2A03-F8F0-4926-8F59-893D3CA6E2B6', 5              , N'Висш съдебен съвет' , 1      ),
    (38                  , N'21C812BE-35AD-467E-B152-D56939E207A3', 5              , N'Инспекторат към ВСС', 2      );
SET IDENTITY_INSERT [CompanyLegalTypes] OFF
GO

--6, Други
DELETE FROM [CompanyLegalTypes]
WHERE [CompanyLegalTypeId] IN (27, 28, 30, 32);
GO

UPDATE [CompanyLegalTypes]
SET [Name] = CASE
                WHEN [CompanyLegalTypeId] = 29 THEN N'Обединение на физически и/или юридически лица'
                WHEN [CompanyLegalTypeId] = 31 THEN N'Физическо лице'
                WHEN [CompanyLegalTypeId] = 36 THEN N'Чуждестранен'
                WHEN [CompanyLegalTypeId] = 33 THEN N'Друго юридическо лице'
              END,
    [Order] = CASE
                WHEN [CompanyLegalTypeId] = 29 THEN 3
                WHEN [CompanyLegalTypeId] = 31 THEN 2
                WHEN [CompanyLegalTypeId] = 36 THEN 4
                WHEN [CompanyLegalTypeId] = 33 THEN 1
              END
WHERE [CompanyTypeId] = 6;
GO

--7, Медицински заведения
UPDATE [CompanyLegalTypes]
SET [Order] = 1
WHERE [CompanyTypeId] = 7;
GO
