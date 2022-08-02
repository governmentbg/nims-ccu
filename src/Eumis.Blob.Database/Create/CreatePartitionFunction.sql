PRINT 'Create partition function'
GO

USE [$(dbName)]
GO

-- Partitions
--        i <= 99  - Empty reserved
-- 100 <= i <= 115 - Old data (16 partitions)
-- 116 <= i <= 147 - Data (32 partitions)
-- 148 <= i        - Empty reserved
CREATE PARTITION FUNCTION pfBlobContents (int)
AS RANGE LEFT FOR VALUES (
  99, -- "Empty reserved"
  100,101,102,103,
  104,105,106,107,
  108,109,110,111,
  112,113,114,115,
  116,117,118,119,
  120,121,122,123,
  124,125,126,127,
  128,129,130,131,
  132,133,134,135,
  136,137,138,139,
  140,141,142,143,
  144,145,146,147
);
GO
