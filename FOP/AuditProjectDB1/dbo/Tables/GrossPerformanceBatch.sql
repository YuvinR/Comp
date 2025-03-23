CREATE TABLE [dbo].[GrossPerformanceBatch] (
    [EntityID]                   INT              NOT NULL,
    [EntityName]                 VARCHAR (MAX)    NULL,
    [GroupName]                  VARCHAR (MAX)    NULL,
    [Benchmark]                  VARCHAR (MAX)    NULL,
    [PeriodBeginningMarketValue] DECIMAL (32, 18) NULL,
    [PeriodEndingMarketValue]    DECIMAL (32, 18) NULL,
    [PeriodPerformance]          VARCHAR (MAX)    NULL,
    [EntityPath]                 VARCHAR (MAX)    NULL,
    [UploadID]                   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_GPB] PRIMARY KEY CLUSTERED ([EntityID] ASC)
);


GO
GRANT VIEW DEFINITION
    ON OBJECT::[dbo].[GrossPerformanceBatch] TO [guest]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[dbo].[GrossPerformanceBatch] TO [guest]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[GrossPerformanceBatch] TO [guest]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[GrossPerformanceBatch] TO [guest]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[GrossPerformanceBatch] TO [guest]
    AS [dbo];


GO
GRANT CONTROL
    ON OBJECT::[dbo].[GrossPerformanceBatch] TO [guest]
    AS [dbo];


GO
GRANT ALTER
    ON OBJECT::[dbo].[GrossPerformanceBatch] TO [guest]
    AS [dbo];

