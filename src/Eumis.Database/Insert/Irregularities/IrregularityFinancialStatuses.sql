PRINT 'Insert IrregularityFinancialStatuses'
GO

SET IDENTITY_INSERT [IrregularityFinancialStatuses] ON

INSERT INTO [IrregularityFinancialStatuses]
    ([IrregularityFinancialStatusId], [Code]   , [Name]                                                                              )
VALUES
    (1                              , N'A-NATR', N'Няма сума за възстановяване'                                                      ),
    (2                              , N'B-ATBC', N'Сумата за възстановяване се изчислява'                                            ),
    (3                              , N'C-RTBS', N'Започва възстановяване'                                                           ),
    (4                              , N'D-RUNW', N'В процес на възстановяване'                                                       ),
    (5                              , N'E-ALRS', N'Прекъснато възстановяване поради жалба'                                           ),
    (6                              , N'F-ACRL', N'Възстановяване след отхвърлена жалба'                                             ),
    (7                              , N'G-FULR', N'Пълно възстановяване'                                                             ),
    (8                              , N'H-EUSW', N'Оттегляне на съфинансирането от ЕС, разход изцяло за сметка на националния бюджет'),
    (9                              , N'I-NRW4', N'Невъзстановена сума след 4 години'                                                ),
    (10                             , N'J-NRW8', N'Невъзстановена сума след 8 години'                                                ),
    (11                             , N'K-AIRR', N'Невъзстановима сума'                                                              ),
    (12                             , N'L-CCEU', N'Минаване по сметка на бюджета на ЕС'                                              ),
    (13                             , N'M-CCNB', N'Минаване по сметка на националния бюджет'                                         ),
    (14                             , N'N-CCBB', N'Минаване по сметка на бюджета на ЕС и на националния бюджет'                      );
GO

SET IDENTITY_INSERT [IrregularityFinancialStatuses] OFF
GO