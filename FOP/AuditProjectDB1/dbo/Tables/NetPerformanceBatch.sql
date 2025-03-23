CREATE TABLE [dbo].[NetPerformanceBatch] (
    [EntityID]                   INT              NOT NULL,
    [EntityName]                 VARCHAR (MAX)    NULL,
    [GroupName]                  VARCHAR (MAX)    NULL,
    [Benchmark]                  VARCHAR (MAX)    NULL,
    [PeriodBeginningMarketValue] DECIMAL (32, 18) NULL,
    [PeriodEndingMarketValue]    DECIMAL (32, 18) NULL,
    [PeriodPerformance]          VARCHAR (MAX)    NULL,
    [EntityPath]                 VARCHAR (MAX)    NULL,
    [UploadID]                   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Table4] PRIMARY KEY CLUSTERED ([EntityID] ASC)
);

