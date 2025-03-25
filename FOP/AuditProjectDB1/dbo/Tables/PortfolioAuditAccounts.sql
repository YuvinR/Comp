﻿CREATE TABLE [dbo].[PortfolioAuditAccounts] (
    [ID]                  INT              IDENTITY (1, 1) NOT NULL,
    [AccountID]           INT              NOT NULL,
    [Active]              VARCHAR (50)     NULL,
    [HouseholdID]         INT              NULL,
    [Name]                VARCHAR (MAX)    NULL,
    [RegistrationID]      INT              NOT NULL,
    [AccountNumber]       VARCHAR (MAX)    NULL,
    [Custodian]           VARCHAR (MAX)    NULL,
    [AccountType]         VARCHAR (MAX)    NULL,
    [ManagementStyle]     VARCHAR (MAX)    NULL,
    [PerformanceReviewed] VARCHAR (50)     NULL,
    [Model]               VARCHAR (MAX)    NULL,
    [CurrentValue]        DECIMAL (18)     NULL,
    [FundFamily]          VARCHAR (MAX)    NULL,
    [CashBalance]         DECIMAL (18)     NULL,
    [CustodialRepCode]    VARCHAR (MAX)    NULL,
    [LastName]            VARCHAR (MAX)    NULL,
    [HistoricalDataReady] VARCHAR (50)     NULL,
    [Managed]             VARCHAR (50)     NULL,
    [SleeveStrategy]      VARCHAR (MAX)    NULL,
    [ModelAggID]          INT              NULL,
    [StartDate]           DATETIME         NULL,
    [BusinessLine]        VARCHAR (MAX)    NULL,
    [LegacyCLSAID]        INT              NULL,
    [UploadID]            UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_PortfolioAuditAccounts] PRIMARY KEY CLUSTERED ([ID] ASC)
);

