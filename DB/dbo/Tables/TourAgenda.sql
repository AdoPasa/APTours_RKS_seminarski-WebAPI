CREATE TABLE [dbo].[TourAgenda] (
    [TourAgendaID] INT            IDENTITY (1, 1) NOT NULL,
    [TourID]       INT            NOT NULL,
    [Day]          INT            NOT NULL,
    [Time]         NVARCHAR (10)  NOT NULL,
    [Value]        NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TourAgenda] PRIMARY KEY CLUSTERED ([TourAgendaID] ASC),
    CONSTRAINT [FK_TourAgenda_Tours] FOREIGN KEY ([TourID]) REFERENCES [dbo].[Tours] ([TourID])
);

