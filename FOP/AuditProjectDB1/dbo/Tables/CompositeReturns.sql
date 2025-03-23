CREATE TABLE [dbo].[CompositeReturns] (
    [CompositeID]              UNIQUEIDENTIFIER NOT NULL,
    [Year]                     INT              NOT NULL,
    [Month]                    INT              NOT NULL,
    [Composite]                VARCHAR (MAX)    NOT NULL,
    [CompositePeriodEnding]    DATE             NOT NULL,
    [CompositeAssetsEnding]    FLOAT (53)       NOT NULL,
    [NumberOfAccounts]         FLOAT (53)       NOT NULL,
    [AnnualGross]              FLOAT (53)       NOT NULL,
    [AnnualNet]                FLOAT (53)       NOT NULL,
    [BenchmarkReturn%]         FLOAT (53)       NULL,
    [AnnualDispersion]         FLOAT (53)       NULL,
    [TotalFirmAssets(USD mil)] FLOAT (53)       NULL,
    [3YrStdDevComposite]       FLOAT (53)       NULL,
    [3YrStdDevBenchmark]       FLOAT (53)       NULL,
    [Benchmark]                VARCHAR (MAX)    NULL,
    [CompositeCreationDate]    DATETIME         NULL,
    [CompositeStartDate]       DATETIME         NULL,
    CONSTRAINT [PK_CompositeReturns] PRIMARY KEY CLUSTERED ([CompositeID] ASC)
);

