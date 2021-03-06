UPDATE CertReports SET  
	certReportNumber = ListData.Number
FROM 
	(VALUES
		(1,	1,	'2/1/08.07.2016',	1),
        (2,	1,	'3/1/10.10.2016',	1),
        (3,	1,	'3/2/05.01.2017',	1),
        (4,	1,	'3/3/02.03.2017',	1),
        (5,	1,	'3/4/10.05.2017',	1),
        (6,	1,	'3/5/07.07.2017',	1),
        (7,	1,	'4/1/06.10.2017',	1),
        (1,	1,	'1/30.06.2015',	2),
        (2,	1,	'1/30.06.2015',	2),
        (3,	1,	'2/30.06.2016',	2),
        (4,	2,	'3/1/08.09.2016',	2),
        (4,	1,	'3/1/08.09.2016',	2),
        (5,	1,	'3/2/13.10.2016',	2),
        (6,	2,	'3/3/12.12.2016',	2),
        (6,	1,	'3/3/12.12.2016',	2),
        (7,	1,	'3/4/13.02.2017',	2),
        (8,	1,	'3/5/12.04.2017',	2),
        (9,	1,	'3/6/13.06.2017',	2),
        (10,	1,	'3/7/30.06.2017',	2),
        (11,	1,	'4/1/11.08.2017',	2),
        (1,	1,	'1/1/22.07.2015',	3),
        (2,	1,	'2/1/08.07.2016',	3),
        (3,	1,	'3/1/09.12.2016',	3),
        (4,	1,	'3/2/09.02.2017',	3),
        (5,	1,	'3/3/03.05.2017',	3),
        (6,	2,	'3/4/31.07.2017',	3),
        (6,	1,	'3/4/07.07.2017',	3),
        (7,	1,	'1/4/11.09.2017',	3),
        (1,	1,	'1/1/30.11.2015',	4),
        (2,	1,	'2/1/04.12.2015',	4),
        (3,	1,	'2/2/04.02.2016',	4),
        (4,	1,	'2/3/05.05.2016',	4),
        (5,	1,	'2/4/18.05.2016',	4),
        (6,	1,	'2/5/14.07.2016',	4),
        (7,	1,	'3/1/30.09.2016',	4),
        (8,	1,	'3/2/10.10.2016',	4),
        (9,	1,	'3/3/13.12.2016',	4),
        (10,	1,	'3/4/22.02.2017',	4),
        (11,	1,	'3/5/18.04.2017',	4),
        (12,	1,	'3/6/17.05.2017',	4),
        (13,	2,	'3/7/10.07.2017',	4),
        (13,	1,	'3/7/10.07.2017',	4),
        (14,	1,	'4/1/14.08.2017',	4),
        (15,	1,	'4/1/14.09.2017',	4),
        (1,	1,	'1/1/27.07.2015',	5),
        (2,	1,	'2/1/18.07.2016',	5),
        (3,	1,	'3/1/13.10.2016',	5),
        (4,	1,	'3/2/09.12.2016',	5),
        (5,	1,	'3/3/09.02.2017',	5),
        (6,	1,	'3/4/20.03.2017',	5),
        (7,	1,	'3/5/18.04.2017',	5),
        (8,	1,	'3/6/12.06.2017',	5),
        (9,	1,	'3/7/07.07.2017',	5),
        (10,	2,	'4/1/09.10.2017',	5),
        (10,	1,	'4/1/13.09.2017',	5),
        (1,	1,	'',	6),
        (2,	1,	'2/12.07.2016',	6),
        (3,	3,	'3/1/10.09.2016',	6),
        (3,	2,	'3/1/10.09.2016',	6),
        (3,	1,	'3/1/10.09.2016',	6),
        (4,	4,	'3/2/10.10.2016',	6),
        (4,	3,	'3/2/10.10.2016',	6),
        (4,	2,	'3/2/10.10.2016',	6),
        (4,	1,	'3/2/10.10.2016',	6),
        (5,	2,	'3/3/14.12.2016',	6),
        (5,	1,	'3/3/14.12.2016',	6),
        (6,	4,	'3/4/02.02.2017',	6),
        (6,	3,	'3/4/02.02.2017',	6),
        (6,	2,	'3/4/02.02.2017',	6),
        (6,	1,	'3/4/02.02.2017',	6),
        (7,	2,	'3/5/13.04.2017',	6),
        (7,	1,	'3/5/13.04.2017',	6),
        (8,	3,	'3/6/14.06.2017',	6),
        (8,	2,	'3/6/14.06.2017',	6),
        (8,	1,	'3/6/14.06.2017',	6),
        (9,	1,	'3/7/10.07.2017',	6),
        (10,	1,	'4/1/16.08.2017',	6),
        (1,	1,	'1/1/04.07.2016',	7),
        (2,	1,	'2/1/04.07.2016',	7),
        (3,	1,	'3/1/11.07.2017',	7),
        (1,	1,	'2/1/12.05.2016',	8),
        (2,	1,	'2/2/08.07.2016',	8),
        (3,	1,	'3/1/10.10.2016',	8),
        (4,	1,	'3/2/10.01.2017',	8),
        (5,	1,	'3/3/10.03.2017',	8),
        (6,	2,	'3/4/28.06.2017',	8),
        (6,	1,	'3/4/05.06.2017',	8),
        (7,	1,	'3/5/10.07.2017',	8),
        (8,	1,	'4/1/03.10.2017',	8),
        (1,	1,	'3/1/18.10.2016',	8010402),
        (2,	1,	'3/2/10.07.2017',	8010402)
) AS ListData(OrderNum, OrderVersionNum, Number, MapNodeId) 
WHERE 
	ListData.OrderNum = CertReports.OrderNum
	AND ListData.OrderVersionNum = CertReports.OrderVersionNum
	AND ListData.MapNodeId = CertReports.ProgrammeId
	

	