﻿PRINT 'Insert EvalSessionProjectStandingRejectionReasons'
GO

SET IDENTITY_INSERT [EvalSessionProjectStandingRejectionReasons] ON

INSERT INTO [EvalSessionProjectStandingRejectionReasons]
    ([EvalSessionProjectStandingRejectionReasonId], [Name])
VALUES
    --Липса на  административно съответствие (не е валидно за финансови инструменти). (Идентифицирано в резултат на извършена оценка на административното съответствие и допустимост (ОАСД))
    (1                                            , N'Липса на  административно съответствие - Документацията по проектното предложение не е получена в рамките на дефинирания в Насоки за кандидатстване срок'),
    (2                                            , N'Липса на  административно съответствие - Информацията не е предоставена според определените в Насоки за кандидатстване  шаблони или канали за комуникация'),
    (3                                            , N'Липса на  административно съответствие - Некоректно изготвена документация (лисващи или грешни справки, документи, атрибути)'),
    (4                                            , N'Липса на  административно съответствие - Липса на получен от бенефициента/получателя в срок отговор на поставени от комисията по оценка въпроси или допълнително изискани документи/разяснения'),
    --Недопустимост на проекта/кандидата (идентифицирано в резултат на извършена оценка на административното съответствие и допустимост (ОАСД))
    (5                                            , N'Недопустимост на проекта/кандидата - Правната форма или предметът на дейност/сектор на кандидата не отговарят на изискванията за допустимост или собствеността на дружеството не е ясна'),
    (6                                            , N'Недопустимост на проекта/кандидата - Партньорите по проекта  не отговарят на изискванията за допустимост'),
    (7                                            , N'Недопустимост на проекта/кандидата - Проектът не в съответствие с или не отговаря на приоритетите на съответната оперативна програма/приоритетна ос/инвестиционен приоритет'),
    (8                                            , N'Недопустимост на проекта/кандидата - Проектът е стартирал (или приключил) преди даването на одобрение (само когато това е в  противоречие със съответните зададени критерии)'),
    (9                                            , N'Недопустимост на проекта/кандидата - Установено е наличие на недопустимо дублиране с други проекти, финансирани по фондове от ЕС или с национални средства'),
    --Несъответствие на технически изисквания (идентифицирано в резултат на извършена Техническа и финансова оценка (ТФО)
    (10                                           , N'Несъответствие на технически изисквания - Проектът не доказва стратегическа необходимост и значителен ефект/ползи  - за самия кандидат, сектора или географския регион, в който ще се приложи проектът'),
    (11                                           , N'Несъответствие на технически изисквания - Не са формулирани ясни и постижими цели, съответстващи на изискванията на процедурата, заложени в поканата за изпращане на предложения'),
    (12                                           , N'Несъответствие на технически изисквания - Не са налице измерими и ясно формулирани крайни резултати от проекта')


SET IDENTITY_INSERT [EvalSessionProjectStandingRejectionReasons] OFF
GO