CREATE TABLE [dbo].[ModelChanges] (
    [OrionRegistrationId] INT              NOT NULL,
    [AccountNumber]       VARCHAR (MAX)    NULL,
    [GoDate]              DATETIME         NULL,
    [CloseDate]           DATETIME         NULL,
    [AIMAccountID]        INT              NULL,
    [OldManagerCode]      VARCHAR (MAX)    NULL,
    [OldStyleCode]        VARCHAR (MAX)    NULL,
    [OldManagerStyle]     VARCHAR (MAX)    NULL,
    [OldEffectiveDate]    DATETIME         NULL,
    [OldExpirationDate]   DATETIME         NULL,
    [ManagerCode]         VARCHAR (50)     NULL,
    [StyleCode]           VARCHAR (50)     NULL,
    [ManagerStyle]        VARCHAR (MAX)    NULL,
    [EffectiveDate]       DATETIME         NULL,
    [ExpirationDate]      DATETIME         NULL,
    [UploadID]            UNIQUEIDENTIFIER NULL
);

