CREATE TABLE [dbo].[Tours] (
    [TourID]          INT             IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (200)  NOT NULL,
    [Description]     NVARCHAR (MAX)  NULL,
    [DurationInDays]  INT             NOT NULL,
    [NumberOfPlaces]  INT             NOT NULL,
    [Price]           DECIMAL (18, 2) NOT NULL,
    [Active]          BIT             NOT NULL,
    [CreatedAt]       DATETIME        NOT NULL,
    [GradeSum]        INT             NULL,
    [NumberOfReviews] INT             NULL,
    [Grade]           DECIMAL (18, 2) NULL,
    [Image]           NVARCHAR (100)  NULL,
    CONSTRAINT [PK_Tours] PRIMARY KEY CLUSTERED ([TourID] ASC)
);

