GO

ALTER TABLE MapNodes ADD
    [PortalNameAlt]         NVARCHAR(MAX)   NULL,
    [PortalShortNameAlt]    NVARCHAR(MAX)   NULL;
GO

UPDATE MapNodes SET PortalNameAlt = N'Good governance'                        , PortalShortNameAlt = N'OPGG'    WHERE MapNodeId = 1 and Type = N'Programme';
UPDATE MapNodes SET PortalNameAlt = N'Transport and transport infrastructure' , PortalShortNameAlt = N'OPTTO'   WHERE MapNodeId = 2 and Type = N'Programme';
UPDATE MapNodes SET PortalNameAlt = N'Regions in growth'                      , PortalShortNameAlt = N'OPRG'    WHERE MapNodeId = 3 and Type = N'Programme';
UPDATE MapNodes SET PortalNameAlt = N'Human resources development'            , PortalShortNameAlt = N'OPHRD'   WHERE MapNodeId = 4 and Type = N'Programme';
UPDATE MapNodes SET PortalNameAlt = N'Innovations and competitiveness'        , PortalShortNameAlt = N'OPIC'    WHERE MapNodeId = 5 and Type = N'Programme';
UPDATE MapNodes SET PortalNameAlt = N'Environment'                            , PortalShortNameAlt = N'OPE'     WHERE MapNodeId = 6 and Type = N'Programme';
UPDATE MapNodes SET PortalNameAlt = N'Science and education for smart growth' , PortalShortNameAlt = N'OPSESG'  WHERE MapNodeId = 7 and Type = N'Programme';
UPDATE MapNodes SET PortalNameAlt = N'Foods'                                  , PortalShortNameAlt = N'OPF'     WHERE MapNodeId = 8 and Type = N'Programme';
GO