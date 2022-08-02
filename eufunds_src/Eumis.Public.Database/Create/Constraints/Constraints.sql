ALTER TABLE [dbo].[MapRegions]  WITH CHECK ADD  CONSTRAINT [FK_MapRegions_Maps] FOREIGN KEY([MapId])
REFERENCES [dbo].[Maps] ([Id])
GO
ALTER TABLE [dbo].[MapRegions] CHECK CONSTRAINT [FK_MapRegions_Maps]
GO
ALTER TABLE [dbo].[Maps]  WITH CHECK ADD  CONSTRAINT [FK_Maps_MapTypes] FOREIGN KEY([Type])
REFERENCES [dbo].[MapTypes] ([Id])
GO
ALTER TABLE [dbo].[Maps] CHECK CONSTRAINT [FK_Maps_MapTypes]
GO