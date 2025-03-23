CREATE TABLE [dbo].[PortfolioAuditRegistrations] (
    [RegistrationID]         INT              NOT NULL,
    [Active]                 VARCHAR (50)     NULL,
    [LastName]               VARCHAR (MAX)    NULL,
    [FirstName]              VARCHAR (MAX)    NULL,
    [Name]                   VARCHAR (MAX)    NULL,
    [AccountType]            VARCHAR (MAX)    NULL,
    [CurrentValue]           DECIMAL (18)     NULL,
    [SSNTaxID]               VARCHAR (MAX)    NULL,
    [DOB]                    DATETIME         NULL,
    [HouseholdID]            INT              NULL,
    [MissingInformation]     VARCHAR (MAX)    NULL,
    [InvestmentObjective]    VARCHAR (MAX)    NULL,
    [SleeveStrategy]         VARCHAR (MAX)    NULL,
    [UMA]                    VARCHAR (50)     NULL,
    [CustodialAccountNumber] VARCHAR (MAX)    NULL,
    [UploadID]               UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_PortfolioAuditRegistrations] PRIMARY KEY CLUSTERED ([RegistrationID] ASC)
);

