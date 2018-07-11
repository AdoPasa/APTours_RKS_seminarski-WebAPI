CREATE TABLE [dbo].[AdditionalInformationTypes] (
    [AdditionalInformationTypeID] INT            IDENTITY (1, 1) NOT NULL,
    [Value]                       NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_AdditionalInformationTypes] PRIMARY KEY CLUSTERED ([AdditionalInformationTypeID] ASC)
);

