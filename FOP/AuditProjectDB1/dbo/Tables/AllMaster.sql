CREATE TABLE [dbo].[AllMaster] (
    [ID]                         INT              IDENTITY (1, 1) NOT NULL,
    [EntityID]                   INT              NOT NULL,
    [AccountNumber]              VARCHAR (MAX)    NULL,
    [StartDate]                  DATETIME         NULL,
    [SleeveStrategy]             VARCHAR (MAX)    NULL,
    [BusinessLine]               VARCHAR (MAX)    NULL,
    [PeriodBeginningMarketValue] DECIMAL (32, 18) NULL,
    [PeriodEndingMarketValue]    DECIMAL (32, 18) NULL,
    [PeriodPerformanceGross]     VARCHAR (MAX)    NULL,
    [PeriodPerformanceNet]       VARCHAR (MAX)    NULL,
    [EntityPath]                 VARCHAR (MAX)    NULL,
    [IsUMA]                      BIT              NULL,
    [LegacyCLSAID]               INT              NULL,
    [NetFlow]                    DECIMAL (32, 18) NULL,
    [FlowPercentage]             DECIMAL (32, 18) NULL,
    [CashPercentage]             DECIMAL (32, 18) NULL,
    [Terminated]                 DATETIME         NULL,
    [ModelChange]                DATETIME         NULL,
    [EntityName]                 VARCHAR (MAX)    NULL,
    [GroupName]                  VARCHAR (MAX)    NULL,
    [Benchmark]                  VARCHAR (MAX)    NULL,
    [NoComposite]                BIT              NULL,
    [ToExclusions]               BIT              NULL,
    [ExclusionType]              VARCHAR (5)      NULL,
    [NoCompositeType]            INT              NULL,
    [UploadID]                   UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_AllMaster] PRIMARY KEY CLUSTERED ([ID] ASC)
);






GO
GRANT SELECT
    ON OBJECT::[dbo].[AllMaster] TO [guest]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[AllMaster] TO [guest]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[AllMaster] TO [guest]
    AS [dbo];


GO
GRANT ALTER
    ON OBJECT::[dbo].[AllMaster] TO [guest]
    AS [dbo];

