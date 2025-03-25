CREATE TABLE [dbo].[TerminatedAccounts] (
    [ID]                  INT              IDENTITY (1, 1) NOT NULL,
    [OrionRegistrationId] INT              NOT NULL,
    [AIMAccountId]        INT              NULL,
    [AccountNumber]       VARCHAR (MAX)    NULL,
    [GoDate]              DATETIME         NULL,
    [CloseDate]           DATETIME         NULL,
    [UploadID]            UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_TerminatedAccounts] PRIMARY KEY CLUSTERED ([ID] ASC)
);

