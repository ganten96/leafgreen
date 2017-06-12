CREATE TABLE [plants].[Gardens] (
    [GardenId]   INT               IDENTITY (1, 1) NOT NULL,
    [GardenName] VARCHAR (250)     NOT NULL,
    [IsArchived] BIT               NOT NULL,
    [DateAdded]  DATETIME2 (7)     NOT NULL,
    [Location]   [sys].[geography] NOT NULL,
    CONSTRAINT [PK_Gardens] PRIMARY KEY CLUSTERED ([GardenId] ASC)
);

