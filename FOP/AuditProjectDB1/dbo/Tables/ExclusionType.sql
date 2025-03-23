CREATE TABLE [dbo].[ExclusionType] (
    [ExclusionCode] VARCHAR (5)   NOT NULL,
    [ExclusionType] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ExclusionType] PRIMARY KEY CLUSTERED ([ExclusionCode] ASC)
);

