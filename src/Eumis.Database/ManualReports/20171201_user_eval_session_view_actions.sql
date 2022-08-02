CREATE FUNCTION dbo._charindex_nth (
  @FindThis NVARCHAR(8000),
  @InThis NVARCHAR(MAX),
  @StartFrom INT,
  @NthOccurence TINYINT
)
RETURNS BIGINT
AS
BEGIN
  /*
  Recursive helper used by dbo.charindex_nth to RETURN the position of the nth occurance of @FindThis in @InThis
  */

  DECLARE @Pos BIGINT

  IF ISNULL(@NthOccurence, 0) <= 0 or ISNULL(@StartFrom, 0) <= 0
  BEGIN
    SELECT @Pos = 0
  END ELSE BEGIN
    IF @NthOccurence = 1
    BEGIN
      SELECT @Pos = charindex(@FindThis, @InThis, @StartFrom)
    END ELSE BEGIN
      SELECT @Pos = dbo._charindex_nth(@FindThis, @InThis, nullif(charindex(@FindThis, @InThis, @StartFrom), 0) + 1, @NthOccurence - 1)
    END
  END

  RETURN @Pos
END

CREATE FUNCTION dbo.charindex_nth (
  @FindThis NVARCHAR(8000),
  @InThis NVARCHAR(MAX),
  @NthOccurence TINYINT
)
RETURNS BIGINT
AS
BEGIN
  /*
  Returns the position of the nth occurance of @FindThis in @InThis
  */

  RETURN dbo._charindex_nth(@FindThis, @InThis, 1, @NthOccurence)
END

CREATE FUNCTION dbo.url_nth_segment (
  @url NVARCHAR(4000),
  @n TINYINT
)
RETURNS NVARCHAR(4000)
AS
BEGIN
  /*
  Returns the nth url segment
  */

  IF @n is NULL RETURN NULL

  DECLARE @FROM INT = dbo.charindex_nth('/', @url, @n) + 1;
  DECLARE @to INT = dbo.charindex_nth('/', @url, @n + 1);
  DECLARE @length INT;

  IF @to > 0
  BEGIN
    SELECT @length = @to - @FROM;
  END ELSE BEGIN
    DECLARE @qmIndex INT = CHARINDEX('?', @url);

    IF @qmIndex > 0
    BEGIN
        SELECT @length = @qmIndex - @FROM;
    END ELSE BEGIN
        SELECT @length = LEN(@url) - @FROM + 1;
    END
  END

  RETURN SUBSTRING(@url, @FROM, @length)
END

SELECT
    'Идентификатор' = actions.LogId,
    'Дата' = actions.LogDate,
    'Действие' = actions.Name,
    'Потребител' = actions.UserName,
    
    -- Procedures.Name
    'Процедура' = COALESCE(proc1.Name, proc2.Name, proc3.Name, proc4.Name, proc5.Name, proc6.Name, proc7.Name, proc8.Name, proc9.Name, proc10.Name),
    
    -- EvalSessions.SessionNum
    'Номер на Оцен. Сесия' = COALESCE(es1.SessionNum, es5.SessionNum, es6.SessionNum, es7.SessionNum, es8.SessionNum, es9.SessionNum, es10.SessionNum),
    
    -- Projects.Name
    'Проект' = COALESCE(proj2.Name, proj3.Name, proj4.Name, proj5.Name, proj6.Name, proj7.Name, proj8.Name, proj9.Name),
    
    -- Projects.RegNumber
    'Номер на проект' = COALESCE(proj2.RegNumber, proj3.RegNumber, proj4.RegNumber, proj5.RegNumber, proj6.RegNumber, proj7.RegNumber, proj8.RegNumber, proj9.RegNumber),
    
    -- ProjectVersionXmls.OrderNum
    'Номер на версия на проект' = COALESCE(projXmls3.OrderNum, projXmls4.OrderNum),
    
    --ProjectCommunications.RegNumber
    'Номер на комуникация' = COALESCE(projComs5.RegNumber, projComs6.RegNumber),

    -- EvalSessionEvaluations.EvalTableType
    'Тип на етап на оценка' =
        CASE COALESCE(esEvals7.EvalTableType, esSheets8.EvalTableType, esSheets9.EvalTableType)
            -- Тип на етап на оценка
            WHEN 1 THEN 'Оценка на административното съответствие и допустимостта'
            WHEN 2 THEN 'Техническа и финансова оценка'
            WHEN 3 THEN 'Комплексна оценка'
            ELSE NULL
        END,

    -- EvalSessionStandings.Code
    'Номер на класиране' = esStand10.Code
FROM (
    SELECT
        ai.LogId,
        ai.LogDate,
        ai.UserName,
        ai.Name,
        ai.RawUrl,
        EvalSessionId = PARSE(ai.EvalSessionId AS INT),
        ProjectId = PARSE(ai.ProjectId AS INT),
        ProjectVersionXmlId = PARSE(ai.ProjectVersionXmlId AS INT),
        ProjectVersionXmlGid = CONVERT(UNIQUEIDENTIFIER, ai.ProjectVersionXmlGid),
        ProjectCommunicationId = PARSE(ai.ProjectCommunicationId AS INT),
        ProjectCommunicationGid = CONVERT(UNIQUEIDENTIFIER, ai.ProjectCommunicationGid),
        EvalSessionEvaluationId = PARSE(ai.EvalSessionEvaluationId AS INT),
        EvalSessionSheetXmlGid = CONVERT(UNIQUEIDENTIFIER, ai.EvalSessionSheetXmlGid),
        EvalSessionSheetId = PARSE(ai.EvalSessionSheetId AS INT),
        EvalSessionStandingId = PARSE(ai.EvalSessionStandingId AS INT)
    FROM (
        SELECT
            l.LogId,
            l.LogDate,
            u.UserName,
            a.Name,
            l.RawUrl,
            EvalSessionId = dbo.url_nth_segment(l.RawUrl, a.EvalSessionIdIndex),
            ProjectId = dbo.url_nth_segment(l.RawUrl, a.ProjectIdIndex),
            ProjectVersionXmlId = dbo.url_nth_segment(l.RawUrl, a.ProjectVersionXmlIdIndex),
            ProjectVersionXmlGid = dbo.url_nth_segment(l.RawUrl, a.ProjectVersionXmlGidIndex),
            ProjectCommunicationId = dbo.url_nth_segment(l.RawUrl, a.ProjectCommunicationIdIndex),
            ProjectCommunicationGid = dbo.url_nth_segment(l.RawUrl, a.ProjectCommunicationGidIndex),
            EvalSessionEvaluationId = dbo.url_nth_segment(l.RawUrl, a.EvalSessionEvaluationIdIndex),
            EvalSessionSheetXmlGid = dbo.url_nth_segment(l.RawUrl, a.EvalSessionSheetXmlGidIndex),
            EvalSessionSheetId = dbo.url_nth_segment(l.RawUrl, a.EvalSessionSheetIdIndex),
            EvalSessionStandingId = dbo.url_nth_segment(l.RawUrl, a.EvalSessionStandingIdIndex)
        FROM [TestEumisLogs1].[dbo].[Logs] l
        JOIN (VALUES
        ('Проектни предложения'                                                                                                                     , 'Eumis.Web.Host'                  , '/api/evalSessions/%/projects?rf=%'           , '/api/evalSessions/%/%/projects?rf=%'         , NULL                                          , 3     , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL),
        ('Проектни предложения > Преглед на проектно предложение'                                                                                   , 'Eumis.Web.Host'                  , '/api/evalSessions/%/projects/%'              , '/api/evalSessions/%/%/projects/%'            , '/api/evalSessions/%/projects/%/%'            , 3     , 5     , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL),
        ('Проектни предложения > Преглед на проектно предложение > Преглед на версия от история на промените'                                       , 'Eumis.Web.Host'                  , '/api/projects/%/versions/%'                  , '/api/projects/%/%/versions/%'                , '/api/projects/%/versions/%/%'                , NULL  , 3     , 5     , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL),
        ('Преглед на проектно предложение (Структуриран документ)'                                                                                  , 'Eumis.PortalIntegration.Host'    , '/api/projects/%/'                            , '/api/projects/%/%/'                          , NULL                                          , NULL  , NULL  , NULL  , 3     , NULL  , NULL  , NULL  , NULL  , NULL  , NULL),
        ('Проектни предложения > Преглед на проектно предложение > Преглед на Комуникация с кандидата'                                              , 'Eumis.Web.Host'                  , '/api/projects/%/communications/%'            , '/api/projects/%/%/communications/%'          , '/api/projects/%/communications/%/%'          , NULL  , 3     , NULL  , NULL  , 5     , NULL  , NULL  , NULL  , NULL  , NULL),
        ('Преглед на въпрос към кандидат (Структуриран документ)'                                                                                   , 'Eumis.PortalIntegration.Host'    , '/api/projectMessages/%?type=message'         , '/api/projectMessages/%/%?type=message'       , NULL                                          , NULL  , NULL  , NULL  , NULL  , NULL  , 3     , NULL  , NULL  , NULL  , NULL),
        ('Преглед на отговор (Структуриран документ)'                                                                                               , 'Eumis.PortalIntegration.Host'    , '/api/projectMessages/%?type=reply'           , '/api/projectMessages/%/%?type=reply'         , NULL                                          , NULL  , NULL  , NULL  , NULL  , NULL  , 3     , NULL  , NULL  , NULL  , NULL),
        ('Проектни предложения > Преглед на проектно предложение > Преглед на обобщена оценка'                                                      , 'Eumis.Web.Host'                  , '/api/evalSessions/%/evaluations/%'           , '/api/evalSessions/%/%/evaluations/%'         , '/api/evalSessions/%/evaluations/%/%'         , 3     , NULL  , NULL  , NULL  , NULL  , NULL  , 5     , NULL  , NULL  , NULL),
        ('Преглед на оценителен лист (Структуриран документ)'                                                                                       , 'Eumis.PortalIntegration.Host'    , '/api/evalSessionSheets/%'                    , '/api/evalSessionSheets/%/%'                  , NULL                                          , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , 3     , NULL  , NULL),
        ('Проектни предложения > Преглед на проектно предложение > Преглед на класиране'                                                            , 'Eumis.Web.Host'                  , '/api/evalSessions/%/projects/%/standings/%'  , '/api/evalSessions/%/%/projects/%/standings/%', '/api/evalSessions/%/projects/%/%/standings/%', 3     , 5     , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL),
        ('Оценителни листове'                                                                                                                       , 'Eumis.Web.Host'                  , '/api/evalSessions/%/sheets?%'                , '/api/evalSessions/%/%/sheets?%'              , NULL                                          , 3     , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL),
        ('Оценителни листове > Преглед на оценителен лист'                                                                                          , 'Eumis.Web.Host'                  , '/api/evalSessions/%/sheets/%'                , '/api/evalSessions/%/%/sheets/%'              , '/api/evalSessions/%/sheets/%/%'              , 3     , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , 5     , NULL),
        ('Класиране'                                                                                                                                , 'Eumis.Web.Host'                  , '/api/evalSessions/%/standings'               , '/api/evalSessions/%/%/standings'             , NULL                                          , 3     , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL),
        ('Класиране > Преглед на класиране'                                                                                                         , 'Eumis.Web.Host'                  , '/api/evalSessions/%/standings/%'             , '/api/evalSessions/%/%/standings/%'           , '/api/evalSessions/%/standings/%/%'           , 3     , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , NULL  , 5   )
    ) AS a(
        Name,
        App,
        Pattern,
        NotPattern1,
        NotPattern2,
        EvalSessionIdIndex,
        ProjectIdIndex,
        ProjectVersionXmlIdIndex,
        ProjectVersionXmlGidIndex,
        ProjectCommunicationIdIndex,
        ProjectCommunicationGidIndex,
        EvalSessionEvaluationIdIndex,
        EvalSessionSheetXmlGidIndex,
        EvalSessionSheetIdIndex,
        EvalSessionStandingIdIndex
    ) ON
        l.Application = a.App
        AND l.RawUrl LIKE a.Pattern
        AND (a.NotPattern1 IS NULL OR l.RawUrl NOT LIKE a.NotPattern1)
        AND (a.NotPattern2 IS NULL OR l.RawUrl NOT LIKE a.NotPattern2)
        JOIN (SELECT UserId, Username FROM [TestEumis].[dbo].[Users]
            WHERE Username in (
                'admin'
            )
        ) AS u ON l.UserId = u.UserId
        WHERE logid > 3400000
    ) as ai
    WHERE 
        (ai.EvalSessionId IS NULL OR ISNUMERIC(ai.EvalSessionId) = 1)
        AND (ai.ProjectId IS NULL OR ISNUMERIC(ai.ProjectId) = 1)
        AND (ai.ProjectVersionXmlId IS NULL OR ISNUMERIC(ai.ProjectVersionXmlId) = 1)
        AND (ai.ProjectVersionXmlGid IS NULL OR TRY_CONVERT(UNIQUEIDENTIFIER, ai.ProjectVersionXmlGid) IS NOT NULL)
        AND (ai.ProjectCommunicationId IS NULL OR ISNUMERIC(ai.ProjectCommunicationId) = 1)
        AND (ai.ProjectCommunicationGid IS NULL OR TRY_CONVERT(UNIQUEIDENTIFIER, ai.ProjectCommunicationGid) IS NOT NULL)
        AND (ai.EvalSessionEvaluationId IS NULL OR ISNUMERIC(ai.EvalSessionEvaluationId) = 1)
        AND (ai.EvalSessionSheetXmlGid IS NULL OR TRY_CONVERT(UNIQUEIDENTIFIER, ai.EvalSessionSheetXmlGid) IS NOT NULL)
        AND (ai.EvalSessionSheetId IS NULL OR ISNUMERIC(ai.EvalSessionSheetId) = 1)
        AND (ai.EvalSessionStandingId IS NULL OR ISNUMERIC(ai.EvalSessionStandingId) = 1)
) AS actions

LEFT JOIN [TestEumis].[dbo].[EvalSessions] es1 ON actions.EvalSessionId = es1.EvalSessionId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc1 ON es1.ProcedureId = proc1.ProcedureId

LEFT JOIN [TestEumis].[dbo].[Projects] proj2 ON actions.ProjectId = proj2.ProjectId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc2 ON proj2.ProcedureId = proc2.ProcedureId

LEFT JOIN [TestEumis].[dbo].[ProjectVersionXmls] projXmls3 ON actions.ProjectVersionXmlId = projXmls3.ProjectVersionXmlId
LEFT JOIN [TestEumis].[dbo].[Projects] proj3 ON projXmls3.ProjectId = proj3.ProjectId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc3 ON proj3.ProcedureId = proc3.ProcedureId

LEFT JOIN [TestEumis].[dbo].[ProjectVersionXmls] projXmls4 ON actions.ProjectVersionXmlGid = projXmls4.Gid
LEFT JOIN [TestEumis].[dbo].[Projects] proj4 ON projXmls4.ProjectId = proj4.ProjectId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc4 ON proj4.ProcedureId = proc4.ProcedureId

LEFT JOIN [TestEumis].[dbo].[ProjectCommunications] projComs5 ON actions.ProjectCommunicationId = projComs5.ProjectCommunicationId
LEFT JOIN [TestEumis].[dbo].[Projects] proj5 ON projComs5.ProjectId = proj5.ProjectId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc5 ON proj5.ProcedureId = proc5.ProcedureId
LEFT JOIN [TestEumis].[dbo].[EvalSessions] es5 ON projComs5.EvalSessionId = es5.EvalSessionId

LEFT JOIN [TestEumis].[dbo].[ProjectCommunications] projComs6 ON actions.ProjectCommunicationGid = projComs6.Gid
LEFT JOIN [TestEumis].[dbo].[Projects] proj6 ON projComs6.ProjectId = proj6.ProjectId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc6 ON proj6.ProcedureId = proc6.ProcedureId
LEFT JOIN [TestEumis].[dbo].[EvalSessions] es6 ON projComs6.EvalSessionId = es6.EvalSessionId

LEFT JOIN [TestEumis].[dbo].[EvalSessionEvaluations] esEvals7
    ON actions.EvalSessionId = esEvals7.EvalSessionId
    AND actions.EvalSessionEvaluationId = esEvals7.EvalSessionEvaluationId
LEFT JOIN [TestEumis].[dbo].[Projects] proj7 ON esEvals7.ProjectId = proj7.ProjectId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc7 ON proj7.ProcedureId = proc7.ProcedureId
LEFT JOIN [TestEumis].[dbo].[EvalSessions] es7 ON esEvals7.EvalSessionId = es7.EvalSessionId

LEFT JOIN [TestEumis].[dbo].[EvalSessionSheetXmls] esSheetXmls8 ON actions.EvalSessionSheetXmlGid = esSheetXmls8.Gid
LEFT JOIN [TestEumis].[dbo].[EvalSessionSheets] esSheets8
    ON esSheetXmls8.EvalSessionId = esSheets8.EvalSessionId
    AND esSheetXmls8.EvalSessionSheetId = esSheets8.EvalSessionSheetId
LEFT JOIN [TestEumis].[dbo].[Projects] proj8 ON esSheets8.ProjectId = proj8.ProjectId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc8 ON proj8.ProcedureId = proc8.ProcedureId
LEFT JOIN [TestEumis].[dbo].[EvalSessions] es8 ON esSheets8.EvalSessionId = es8.EvalSessionId


LEFT JOIN [TestEumis].[dbo].[EvalSessionSheets] esSheets9
    ON actions.EvalSessionId = esSheets9.EvalSessionId
    AND actions.EvalSessionSheetId = esSheets9.EvalSessionSheetId
LEFT JOIN [TestEumis].[dbo].[Projects] proj9 ON esSheets9.ProjectId = proj9.ProjectId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc9 ON proj9.ProcedureId = proc9.ProcedureId
LEFT JOIN [TestEumis].[dbo].[EvalSessions] es9 ON esSheets9.EvalSessionId = es9.EvalSessionId

LEFT JOIN [TestEumis].[dbo].[EvalSessionStandings] esStand10
    ON actions.EvalSessionId = esStand10.EvalSessionId
    AND actions.EvalSessionStandingId = esStand10.EvalSessionStandingId
LEFT JOIN [TestEumis].[dbo].[EvalSessions] es10 ON esStand10.EvalSessionId = es10.EvalSessionId
LEFT JOIN [TestEumis].[dbo].[Procedures] proc10 ON es10.ProcedureId = proc10.ProcedureId

ORDER BY actions.LogId DESC
