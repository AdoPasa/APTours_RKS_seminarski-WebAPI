CREATE TABLE [dbo].[AuthenticationTokens] (
    [AuthenticationTokenID] INT            IDENTITY (1, 1) NOT NULL,
    [UserID]                INT            NOT NULL,
    [AuthenticationToken]   NVARCHAR (100) NOT NULL,
    [DeviceToken]           NVARCHAR (100) NOT NULL,
    [DateTimeCreated]       DATETIME       NOT NULL,
    [Info_Version_Release]  NVARCHAR (100) NOT NULL,
    [Info_Device]           NVARCHAR (50)  NOT NULL,
    [Info_Model]            NVARCHAR (50)  NOT NULL,
    [Info_Product]          NVARCHAR (50)  NOT NULL,
    [Info_Manufacturer]     NVARCHAR (50)  NOT NULL,
    [Info_Brand]            NVARCHAR (50)  NOT NULL,
    [Android_SerialOrID]    NVARCHAR (100) NOT NULL,
    [DateTimeDeleted]       DATETIME       NULL,
    [IsDeleted]             BIT            NOT NULL,
    CONSTRAINT [PK_AuthenticationTokens] PRIMARY KEY CLUSTERED ([AuthenticationTokenID] ASC),
    CONSTRAINT [FK_AuthenticationTokens_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

