CREATE TABLE [dbo].[MonthlyUploads] (
    [MonthlyUploadID] UNIQUEIDENTIFIER NOT NULL,
    [Month]           INT              NOT NULL,
    [Year]            INT              NOT NULL,
    [CreatedDate]     DATETIME         NOT NULL,
    [IsActive]        BIT              NOT NULL,
    [Message]         VARCHAR (MAX)    NULL,
    [Status]          INT              NULL,
    CONSTRAINT [PK_MonthlyUploads] PRIMARY KEY CLUSTERED ([MonthlyUploadID] ASC)
);

