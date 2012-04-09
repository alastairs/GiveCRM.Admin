ALTER TABLE [dbo].[CharityMembership]
    ADD CONSTRAINT [UQ_CharityIdUserName]
    UNIQUE (CharityId, UserName)