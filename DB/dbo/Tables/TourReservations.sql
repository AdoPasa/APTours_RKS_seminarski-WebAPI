CREATE TABLE [dbo].[TourReservations] (
    [TourReservationID]  INT      IDENTITY (1, 1) NOT NULL,
    [UserID]             INT      NOT NULL,
    [TourID]             INT      NOT NULL,
    [TourDateID]         INT      NOT NULL,
    [NumberOfPassengers] INT      NOT NULL,
    [CreatedAt]          DATETIME NOT NULL,
    CONSTRAINT [PK_TourReservations] PRIMARY KEY CLUSTERED ([TourReservationID] ASC),
    CONSTRAINT [FK_TourReservations_TourDates] FOREIGN KEY ([TourDateID]) REFERENCES [dbo].[TourDates] ([TourDateID]),
    CONSTRAINT [FK_TourReservations_Tours] FOREIGN KEY ([TourID]) REFERENCES [dbo].[Tours] ([TourID]),
    CONSTRAINT [FK_TourReservations_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

