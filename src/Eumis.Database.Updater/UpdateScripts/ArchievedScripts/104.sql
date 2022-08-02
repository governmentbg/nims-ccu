GO

ALTER TABLE CompanyTypes ADD [NameAlt] NVARCHAR(MAX) NULL;
GO

UPDATE CompanyTypes SET NameAlt = N'Non-profit organization' WHERE CompanyTypeId = 1 and Name = N'Нестопанска организация'
UPDATE CompanyTypes SET NameAlt = N'Educational establishment' WHERE CompanyTypeId = 2 and Name = N'Учебно заведение'
UPDATE CompanyTypes SET NameAlt = N'State administration' WHERE CompanyTypeId = 3 and Name = N'Държавна администрация'
UPDATE CompanyTypes SET NameAlt = N'Company' WHERE CompanyTypeId = 4 and Name = N'Компания'
UPDATE CompanyTypes SET NameAlt = N'Judiciary' WHERE CompanyTypeId = 5 and Name = N'Съдебна система'
UPDATE CompanyTypes SET NameAlt = N'Other' WHERE CompanyTypeId = 6 and Name = N'Друга'
UPDATE CompanyTypes SET NameAlt = N'Medical establishment' WHERE CompanyTypeId = 7 and Name = N'Медицинско заведение'
GO

ALTER TABLE CompanyLegalTypes ADD [NameAlt] NVARCHAR(MAX) NULL;
GO

UPDATE CompanyLegalTypes SET NameAlt = N'Municipal administration' WHERE CompanyLegalTypeId = 1 and Name = N'Общинска администрация'
UPDATE CompanyLegalTypes SET NameAlt = N'Regional administration' WHERE CompanyLegalTypeId = 2 and Name = N'Областна администрация'
UPDATE CompanyLegalTypes SET NameAlt = N'ACM/ ministry' WHERE CompanyLegalTypeId = 3 and Name = N'АМС / министерство'
UPDATE CompanyLegalTypes SET NameAlt = N'Executive agency/ administrative structure established by legal act' WHERE CompanyLegalTypeId = 4 and Name = N'Изпълнителна агенция / административна структура, създадена с нормативен акт'
UPDATE CompanyLegalTypes SET NameAlt = N'State agency/ state commission' WHERE CompanyLegalTypeId = 5 and Name = N'Държавна агенция / държавна комисия'
UPDATE CompanyLegalTypes SET NameAlt = N'Others' WHERE CompanyLegalTypeId = 6 and Name = N'Други'
UPDATE CompanyLegalTypes SET NameAlt = N'Joint Stock company - JSC (AD)' WHERE CompanyLegalTypeId = 7 and Name = N'Акционерно дружество АД'
UPDATE CompanyLegalTypes SET NameAlt = N'Single-member joint stock company – SMJSC (EAD)' WHERE CompanyLegalTypeId = 8 and Name = N'Еднолично акционерно дружество ЕАД'
UPDATE CompanyLegalTypes SET NameAlt = N'Limited liability company – LLC (OOD)' WHERE CompanyLegalTypeId = 9 and Name = N'Дружество с ограничена отговорност ООД'
UPDATE CompanyLegalTypes SET NameAlt = N'Single-member limited liability company – SMLLC (EOOD)' WHERE CompanyLegalTypeId = 10 and Name = N'Еднолично дружество с ограничена отговорност ЕООД'
UPDATE CompanyLegalTypes SET NameAlt = N'Sole Proprietor – SP (ET)' WHERE CompanyLegalTypeId = 11 and Name = N'Едноличен търговец ЕТ'
UPDATE CompanyLegalTypes SET NameAlt = N'Limited partnership – LP (KD)' WHERE CompanyLegalTypeId = 12 and Name = N'Командитно дружество КД'
UPDATE CompanyLegalTypes SET NameAlt = N'Limited partnership with a share capital - LPSC (KDA)' WHERE CompanyLegalTypeId = 13 and Name = N'Командитно дружество с акции КДА'
UPDATE CompanyLegalTypes SET NameAlt = N'General partnership – GP (SD)' WHERE CompanyLegalTypeId = 14 and Name = N'Събирателно дружество СД'
UPDATE CompanyLegalTypes SET NameAlt = N'Others' WHERE CompanyLegalTypeId = 15 and Name = N'Други'
UPDATE CompanyLegalTypes SET NameAlt = N'Public benefit association' WHERE CompanyLegalTypeId = 16 and Name = N'Сдружение в обществена полза'
UPDATE CompanyLegalTypes SET NameAlt = N'Private benefit association' WHERE CompanyLegalTypeId = 17 and Name = N'Сдружение в частна полза'
UPDATE CompanyLegalTypes SET NameAlt = N'Public benefit foundation' WHERE CompanyLegalTypeId = 18 and Name = N'Фондация в обществена полза'
UPDATE CompanyLegalTypes SET NameAlt = N'Private benefit foundation' WHERE CompanyLegalTypeId = 19 and Name = N'Фондация в частна полза'
UPDATE CompanyLegalTypes SET NameAlt = N'Community center' WHERE CompanyLegalTypeId = 20 and Name = N'Читалище'
UPDATE CompanyLegalTypes SET NameAlt = N'Higher education establishment/ University' WHERE CompanyLegalTypeId = 21 and Name = N'Висше училище/университет'
UPDATE CompanyLegalTypes SET NameAlt = N'School' WHERE CompanyLegalTypeId = 22 and Name = N'Училище'
UPDATE CompanyLegalTypes SET NameAlt = N'Kindergarten/ Nursery' WHERE CompanyLegalTypeId = 23 and Name = N'Детска градина/Детска ясла'
UPDATE CompanyLegalTypes SET NameAlt = N'Court' WHERE CompanyLegalTypeId = 24 and Name = N'Съд'
UPDATE CompanyLegalTypes SET NameAlt = N'Investigation' WHERE CompanyLegalTypeId = 25 and Name = N'Следствие'
UPDATE CompanyLegalTypes SET NameAlt = N'Prosecution' WHERE CompanyLegalTypeId = 26 and Name = N'Прокуратура'
UPDATE CompanyLegalTypes SET NameAlt = N'Association of natural persons and/or legal entities' WHERE CompanyLegalTypeId = 29 and Name = N'Обединение на физически и/или юридически лица'
UPDATE CompanyLegalTypes SET NameAlt = N'Natural person' WHERE CompanyLegalTypeId = 31 and Name = N'Физическо лице'
UPDATE CompanyLegalTypes SET NameAlt = N'Other legal entity' WHERE CompanyLegalTypeId = 33 and Name = N'Друго юридическо лице'
UPDATE CompanyLegalTypes SET NameAlt = N'Hospital/ Medical establishment' WHERE CompanyLegalTypeId = 34 and Name = N'Болница/медицинско заведение'
UPDATE CompanyLegalTypes SET NameAlt = N'Specialized territorial administration' WHERE CompanyLegalTypeId = 35 and Name = N'Специализирана териториална администрация'
UPDATE CompanyLegalTypes SET NameAlt = N'Foreign' WHERE CompanyLegalTypeId = 36 and Name = N'Чуждестранен'
UPDATE CompanyLegalTypes SET NameAlt = N'Supreme judicial court' WHERE CompanyLegalTypeId = 37 and Name = N'Висш съдебен съвет'
UPDATE CompanyLegalTypes SET NameAlt = N'SJC''s Inspectorate' WHERE CompanyLegalTypeId = 38 and Name = N'Инспекторат към ВСС'
GO
