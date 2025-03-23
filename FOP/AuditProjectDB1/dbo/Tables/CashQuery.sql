CREATE TABLE [dbo].[CashQuery] (
    [RegistrationID]               INT              NOT NULL,
    [ClientLastName]               VARCHAR (MAX)    NULL,
    [RegistrationName]             VARCHAR (MAX)    NULL,
    [RegistrationCode]             VARCHAR (50)     NULL,
    [RegistrationValue]            DECIMAL (32, 18) NULL,
    [MoneyMarketValue]             DECIMAL (32, 18) NULL,
    [RegistrationPercentageInCash] DECIMAL (32, 18) NULL,
    [RepName]                      VARCHAR (MAX)    NULL,
    [RepNo]                        VARCHAR (MAX)    NULL,
    [SleeveStrategy]               VARCHAR (MAX)    NULL,
    [UploadID]                     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_CashQuery_10154] PRIMARY KEY CLUSTERED ([RegistrationID] ASC)
);

