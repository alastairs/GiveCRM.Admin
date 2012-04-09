ALTER TABLE [dbo].[Charity]
    ADD CONSTRAINT [UQ_RegisteredCharityNumber]
    UNIQUE (RegisteredCharityNumber)