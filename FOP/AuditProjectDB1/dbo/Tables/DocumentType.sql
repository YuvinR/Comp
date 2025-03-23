CREATE TABLE [dbo].[DocumentType] (
    [DocumentTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [DocumentName]     VARCHAR (MAX) NOT NULL,
    [DocumentTypeCode] VARCHAR (3)   NOT NULL,
    [IsActive]         BIT           NULL,
    CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED ([DocumentTypeID] ASC)
);

