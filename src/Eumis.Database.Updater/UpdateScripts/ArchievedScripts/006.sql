-- 173eee6 Add a name for hidden investment priorities; Disable edit in the backend for hidden investment priorities;

GO
UPDATE [dbo].[MapNodes] SET [Name] = N'Без инвестиционен приоритет' WHERE [MapNodeId] IN (48, 49, 56, 68, 79, 80, 86, 92, 101);
