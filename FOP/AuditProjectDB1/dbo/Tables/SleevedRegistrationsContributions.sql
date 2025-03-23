CREATE TABLE [dbo].[SleevedRegistrationsContributions] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [HHID]                   INT              NOT NULL,
    [RegID]                  INT              NOT NULL,
    [ClientName]             VARCHAR (MAX)    NULL,
    [BrokerDealerName]       VARCHAR (MAX)    NULL,
    [CustodialAccountNumber] VARCHAR (MAX)    NULL,
    [TransactionType]        VARCHAR (MAX)    NULL,
    [Amount]                 FLOAT (53)       NULL,
    [RepID]                  INT              NULL,
    [Date]                   DATETIME         NULL,
    [TransactionStatus]      VARCHAR (50)     NULL,
    [UploadID]               UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SleevedRegistrationsContributions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

