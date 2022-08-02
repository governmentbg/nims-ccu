GO

ALTER TABLE MapNodes ADD
    [PortalName]            NVARCHAR(MAX)   NULL,
    [PortalOrderNum]        INT             NULL;
GO

UPDATE MapNodes SET PortalOrderNum = 7, PortalName = N'Добро управление'                            WHERE MapNodeId = 1 and Type = N'Programme';
UPDATE MapNodes SET PortalOrderNum = 1, PortalName = N'Транспорт и транспортна инфраструктура'      WHERE MapNodeId = 2 and Type = N'Programme';
UPDATE MapNodes SET PortalOrderNum = 3, PortalName = N'Региони в растеж'                            WHERE MapNodeId = 3 and Type = N'Programme';
UPDATE MapNodes SET PortalOrderNum = 6, PortalName = N'Развитие на човешките ресурси'               WHERE MapNodeId = 4 and Type = N'Programme';
UPDATE MapNodes SET PortalOrderNum = 4, PortalName = N'Иновации и конкурентоспособност'             WHERE MapNodeId = 5 and Type = N'Programme';
UPDATE MapNodes SET PortalOrderNum = 2, PortalName = N'Околна среда'                                WHERE MapNodeId = 6 and Type = N'Programme';
UPDATE MapNodes SET PortalOrderNum = 5, PortalName = N'Наука и образование за интелигентен растеж'  WHERE MapNodeId = 7 and Type = N'Programme';
UPDATE MapNodes SET PortalOrderNum = 8, PortalName = N'Храни'                                       WHERE MapNodeId = 8 and Type = N'Programme';
GO
