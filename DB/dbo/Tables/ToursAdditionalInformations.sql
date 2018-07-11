CREATE TABLE [dbo].[ToursAdditionalInformations] (
    [TourAdditionalInformationID] INT            IDENTITY (1, 1) NOT NULL,
    [TourID]                      INT            NOT NULL,
    [AdditionalInformationTypeID] INT            NOT NULL,
    [Value]                       NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TourAdditionalInformations] PRIMARY KEY CLUSTERED ([TourAdditionalInformationID] ASC),
    CONSTRAINT [FK_TourAdditionalInformations_AdditionalInformationTypes] FOREIGN KEY ([AdditionalInformationTypeID]) REFERENCES [dbo].[AdditionalInformationTypes] ([AdditionalInformationTypeID]),
    CONSTRAINT [FK_TourAdditionalInformations_Tours] FOREIGN KEY ([TourID]) REFERENCES [dbo].[Tours] ([TourID])
);

