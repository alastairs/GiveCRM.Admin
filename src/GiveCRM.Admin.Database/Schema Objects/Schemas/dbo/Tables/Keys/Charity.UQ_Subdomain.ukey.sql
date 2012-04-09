ALTER TABLE [dbo].[Charity]
    ADD CONSTRAINT [UQ_Subdomain]
    UNIQUE (Subdomain)