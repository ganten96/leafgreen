CREATE TABLE [plants].[GardenPlants] (
    [GardenId]      INT NOT NULL,
    [PlantId]       INT NOT NULL,
    [GardenPlantId] INT IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_GardenPlants] PRIMARY KEY CLUSTERED ([GardenPlantId] ASC),
    CONSTRAINT [FK_GardenPlants_Plants] FOREIGN KEY ([PlantId]) REFERENCES [plants].[Plants] ([PlantId]) ON DELETE CASCADE ON UPDATE CASCADE
);



