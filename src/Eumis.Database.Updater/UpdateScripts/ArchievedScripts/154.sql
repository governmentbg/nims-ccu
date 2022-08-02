GO

ALTER TABLE [dbo].[ContractReportMicrosType2Items] DROP CONSTRAINT [CHK_ContractReportMicrosType2Items_Activity]
GO

ALTER TABLE [dbo].[ContractReportMicrosType2Items] ALTER COLUMN    [Activity]         NVARCHAR(MAX)         NULL
GO

UPDATE [dbo].[ContractReportMicrosType2Items]
SET [Activity] = CASE [Activity]
            WHEN N'1' THEN N'Потребител на услуги'
            WHEN N'2' THEN N'Личен асистент'
            WHEN N'3' THEN N'Обучение на служители от администрацията по ПО 2 по ОПДУ'
            WHEN N'4' THEN N'Съпътстващо обучение на служители от администрацията по ПО 2 по ОПДУ'
            WHEN N'5' THEN N'Обучение на магистрати, съдебни служители и служители на разследващите органи по НПК по ПО 3 по ОПДУ'
            WHEN N'6' THEN N'Съпътстващо обучение на магистрати, съдебни служители и служители на разследващите органи по НПК по ПО 3 по ОПДУ'
            WHEN N'7' THEN N'Обучение на служители на НПО и социално-икономически партньори по ОПДУ'
            WHEN N'8' THEN N'Обучение на служители по ПО 4 по ОПДУ'
            WHEN N'9' THEN N'Обучение на служители на УО и членове на КН по ПО 5 по ОПДУ'
            WHEN N'10' THEN N'Обучение по ПО 1 по ОПДУ'
        END
GO