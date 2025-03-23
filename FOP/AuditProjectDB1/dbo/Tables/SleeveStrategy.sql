CREATE TABLE [dbo].[SleeveStrategy] (
    [SID]                INT              IDENTITY (1, 1) NOT NULL,
    [SleeveStrategyName] VARCHAR (MAX)    NOT NULL,
    [UploadID]           UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SleeveStrategy] PRIMARY KEY CLUSTERED ([SID] ASC)
);

