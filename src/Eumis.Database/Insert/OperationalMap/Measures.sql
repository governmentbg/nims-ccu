PRINT 'Insert Measures'
GO

SET IDENTITY_INSERT [Measures] ON

INSERT INTO [Measures]
    ([MeasureId], [ShortName]  , [Name]                                           , [NameAlt]      , [CreateDate], [ModifyDate])
VALUES
    (1          , '%'                                                           , '%'                                                           , NULL           , GETDATE()    ,GETDATE() ),
    (2          , '% от оборота'                                                , '% от оборота'                                                , NULL           , GETDATE()    ,GETDATE() ),
    (3          , '% от общия брой предприятия'                                 , '% от общия брой предприятия'                                 , NULL           , GETDATE()    ,GETDATE() ),
    (4          , '% от подкрепените проекти по ИП 3.2'                         , '% от подкрепените проекти по ИП 3.2'                         , NULL           , GETDATE()    ,GETDATE() ),
    (5          , 'EUR'                                                         , 'EUR'                                                         , NULL           , GETDATE()    ,GETDATE() ),
    (6          , 'ITU/год.'                                                    , 'ITU/год.'                                                    , NULL           , GETDATE()    ,GETDATE() ),
    (7          , 'km'                                                          , 'km'                                                          , NULL           , GETDATE()    ,GETDATE() ),
    (8          , 'MWh/y'                                                       , 'MWh/y'                                                       , NULL           , GETDATE()    ,GETDATE() ),
    (9          , 'базова стойност-над % / целева стойност-до %'                , 'базова стойност-над % / целева стойност-до %'                , NULL           , GETDATE()    ,GETDATE() ),
    (10         , 'Брой'                                                        , 'Брой'                                                        , NULL           , GETDATE()    ,GETDATE() ),
    (11         , 'брой на година'                                              , 'брой на година'                                              , NULL           , GETDATE()    ,GETDATE() ),
    (12         , 'Да/Не'                                                       , 'Да/Не'                                                       , NULL           , GETDATE()    ,GETDATE() ),
    (13         , 'дни'                                                         , 'дни'                                                         , NULL           , GETDATE()    ,GETDATE() ),
    (14         , 'добавена стойност по факторни разходи (хил. лв.)/заето лице' , 'добавена стойност по факторни разходи (хил. лв.)/заето лице' , NULL           , GETDATE()    ,GETDATE() ),
    (15         , 'Дял на годишния брой дни с минимална дълбочина по протежение', 'Дял на годишния брой дни с минимална дълбочина по протежение', NULL           , GETDATE()    ,GETDATE() ),
    (16         , 'евро'                                                        , 'евро'                                                        , NULL           , GETDATE()    ,GETDATE() ),
    (17         , 'евро /ЕС принос/'                                            , 'евро /ЕС принос/'                                            , NULL           , GETDATE()    ,GETDATE() ),
    (18         , 'Еквивалент на пълно работно време'                           , 'Еквивалент на пълно работно време'                           , NULL           , GETDATE()    ,GETDATE() ),
    (19         , 'километри'                                                   , 'километри'                                                   , NULL           , GETDATE()    ,GETDATE() ),
    (20         , 'км'                                                          , 'км'                                                          , NULL           , GETDATE()    ,GETDATE() ),
    (21         , 'км/ч'                                                        , 'км/ч'                                                        , NULL           , GETDATE()    ,GETDATE() ),
    (22         , 'м3/год.'                                                     , 'м3/год.'                                                     , NULL           , GETDATE()    ,GETDATE() ),
    (23         , 'млн. евро'                                                   , 'млн. евро'                                                   , NULL           , GETDATE()    ,GETDATE() ),
    (24         , 'млн. пътнико-километра'                                      , 'млн. пътнико-километра'                                      , NULL           , GETDATE()    ,GETDATE() ),
    (25         , 'млн. ткм'                                                    , 'млн. ткм'                                                    , NULL           , GETDATE()    ,GETDATE() ),
    (26         , 'млрд. евро'                                                  , 'млрд. евро'                                                  , NULL           , GETDATE()    ,GETDATE() ),
    (27         , 'предприятия'                                                 , 'предприятия'                                                 , NULL           , GETDATE()    ,GETDATE() ),
    (28         , 'самолетни движения'                                          , 'самолетни движения'                                          , NULL           , GETDATE()    ,GETDATE() ),
    (29         , 'Съотношение'                                                 , 'Съотношение'                                                 , NULL           , GETDATE()    ,GETDATE() ),
    (30         , 'Съотношение (%)'                                             , 'Съотношение (%)'                                             , NULL           , GETDATE()    ,GETDATE() ),
    (31         , 'т.н.е. на 1000 евро БВП'                                     , 'т.н.е. на 1000 евро БВП'                                     , NULL           , GETDATE()    ,GETDATE() ),
    (32         , 'тонове CO2 екв.'                                             , 'тонове CO2 екв.'                                             , NULL           , GETDATE()    ,GETDATE() ),
    (33         , 'т./год.'                                                     , 'т./год.'                                                     , NULL           , GETDATE()    ,GETDATE() ),
    (34         , 'mg/m3'                                                       , 'mg/m3'                                                       , NULL           , GETDATE()    ,GETDATE() ),
    (35         , 'лица'                                                        , 'лица'                                                        , NULL           , GETDATE()    ,GETDATE() ),
    (36         , 'хиляди души'                                                 , 'хиляди души'                                                 , NULL           , GETDATE()    ,GETDATE() ),
    (37         , 'хиляди тона'                                                 , 'хиляди тона'                                                 , NULL           , GETDATE()    ,GETDATE() ),
    (38         , 'хиляди тона нефтен еквивалент'                               , 'хиляди тона нефтен еквивалент'                               , NULL           , GETDATE()    ,GETDATE() ),
    (39         , 'Екв. ж.'                                                     , 'Екв. ж.'                                                     , NULL           , GETDATE()    ,GETDATE() ),
    (40         , 'т/год.'                                                      , 'т/год.'                                                      , NULL           , GETDATE()    ,GETDATE() ),
    (41         , 'тонове'                                                      , 'тонове'                                                      , NULL           , GETDATE()    ,GETDATE() ),
    (42         , 'kWh/година'                                                  , 'kWh/година'                                                  , NULL           , GETDATE()    ,GETDATE() ),
    (43         , 'брой дни'                                                    , 'брой дни'                                                    , NULL           , GETDATE()    ,GETDATE() ),
    (44         , 'домакинства'                                                 , 'домакинства'                                                 , NULL           , GETDATE()    ,GETDATE() ),
    (45         , 'еквивалент жители'                                           , 'еквивалент жители'                                           , NULL           , GETDATE()    ,GETDATE() ),
    (46         , 'жилища'                                                      , 'жилища'                                                      , NULL           , GETDATE()    ,GETDATE() ),
    (47         , 'квадратни метри'                                             , 'квадратни метри'                                             , NULL           , GETDATE()    ,GETDATE() ),
    (48         , 'Модернизирани обекти на спешна медицинска помощ'             , 'Модернизирани обекти на спешна медицинска помощ'             , NULL           , GETDATE()    ,GETDATE() ),
    (49         , 'обекти'                                                      , 'обекти'                                                      , NULL           , GETDATE()    ,GETDATE() ),
    (50         , 'посещения/година'                                            , 'посещения/година'                                            , NULL           , GETDATE()    ,GETDATE() ),
    (51         , 'хектари'                                                     , 'хектари'                                                     , NULL           , GETDATE()    ,GETDATE() )


SET IDENTITY_INSERT [Measures] OFF
GO

