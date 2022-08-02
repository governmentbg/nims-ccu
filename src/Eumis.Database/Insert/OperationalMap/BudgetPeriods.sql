PRINT 'Insert BudgetPeriods'
GO

SET IDENTITY_INSERT [BudgetPeriods] ON

INSERT INTO [BudgetPeriods]
    ([BudgetPeriodId], [Name], [Year])
VALUES
    (1               , '2014', 2014  ),
    (2               , '2015', 2015  ),
    (3               , '2016', 2016  ),
    (4               , '2017', 2017  ),
    (5               , '2018', 2018  ),
    (6               , '2019', 2019  ),
    (7               , '2020', 2020  )

SET IDENTITY_INSERT [BudgetPeriods] OFF
GO
