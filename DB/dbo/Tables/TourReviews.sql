CREATE TABLE [dbo].[TourReviews] (
    [TourReviewID]      INT            IDENTITY (1, 1) NOT NULL,
    [UserID]            INT            NOT NULL,
    [TourID]            INT            NOT NULL,
    [TourReservationID] INT            NOT NULL,
    [Grade]             INT            NOT NULL,
    [Review]            NVARCHAR (500) NOT NULL,
    [CreatedAt]         DATETIME       NOT NULL,
    CONSTRAINT [PK_TourReviews] PRIMARY KEY CLUSTERED ([TourReviewID] ASC),
    CONSTRAINT [FK_TourReviews_TourReservations] FOREIGN KEY ([TourReservationID]) REFERENCES [dbo].[TourReservations] ([TourReservationID]),
    CONSTRAINT [FK_TourReviews_Tours] FOREIGN KEY ([TourID]) REFERENCES [dbo].[Tours] ([TourID]),
    CONSTRAINT [FK_TourReviews_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

