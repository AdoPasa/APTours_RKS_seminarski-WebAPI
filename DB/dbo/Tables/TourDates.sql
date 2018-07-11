CREATE TABLE [dbo].[TourDates] (
    [TourDateID] INT      IDENTITY (1, 1) NOT NULL,
    [TourID]     INT      NOT NULL,
    [Date]       DATETIME NOT NULL,
    CONSTRAINT [PK_TourDates] PRIMARY KEY CLUSTERED ([TourDateID] ASC),
    CONSTRAINT [FK_TourDates_Tours] FOREIGN KEY ([TourID]) REFERENCES [dbo].[Tours] ([TourID])
);

