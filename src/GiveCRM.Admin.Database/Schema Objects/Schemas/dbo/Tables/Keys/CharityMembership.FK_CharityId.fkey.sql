ALTER TABLE [dbo].[CharityMembership]
	ADD CONSTRAINT [FK_CharityId] 
	FOREIGN KEY (CharityId)
	REFERENCES [Charity] ([Id])	

