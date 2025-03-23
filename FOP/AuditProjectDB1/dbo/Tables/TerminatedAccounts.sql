CREATE TABLE [dbo].[TerminatedAccounts] (
    [OrionRegistrationId] INT              NOT NULL,
    [AIMAccountId]        INT              NULL,
    [AccountNumber]       VARCHAR (MAX)    NULL,
    [GoDate]              DATETIME         NULL,
    [CloseDate]           DATETIME         NULL,
    [UploadID]            UNIQUEIDENTIFIER NULL
);

