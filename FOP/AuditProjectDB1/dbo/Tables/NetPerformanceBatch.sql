CREATE TABLE [dbo].[NetPerformanceBatch] (
    [ID]                         INT              IDENTITY (1, 1) NOT NULL,
    [EntityID]                   INT              NULL,
    [EntityName]                 VARCHAR (MAX)    NULL,
    [GroupName]                  VARCHAR (MAX)    NULL,
    [Benchmark]                  VARCHAR (MAX)    NULL,
    [PeriodBeginningMarketValue] VARCHAR (MAX)    NULL,
    [PeriodEndingMarketValue]    VARCHAR (MAX)    NULL,
    [PeriodPerformance]          VARCHAR (MAX)    NULL,
    [EntityPath]                 VARCHAR (MAX)    NULL,
    [UploadID]                   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_NetPerformanceBatch] PRIMARY KEY CLUSTERED ([ID] ASC)
);

