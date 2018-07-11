CREATE TABLE [dbo].[FavoriteTours] (
    [TourID] INT NOT NULL,
    [UserID] INT NOT NULL,
    CONSTRAINT [PK_FavoriteTours] PRIMARY KEY CLUSTERED ([TourID] ASC, [UserID] ASC),
    CONSTRAINT [FK_FavoriteTours_Tours] FOREIGN KEY ([TourID]) REFERENCES [dbo].[Tours] ([TourID]),
    CONSTRAINT [FK_FavoriteTours_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [YourTable_unique] UNIQUE NONCLUSTERED ([TourID] ASC, [UserID] ASC)
);

