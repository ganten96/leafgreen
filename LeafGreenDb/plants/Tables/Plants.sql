CREATE TABLE [plants].[Plants] (
    [PlantId]        INT           IDENTITY (1, 1) NOT NULL,
    [Symbol]         VARCHAR (15)  NULL,
    [ScientificName] VARCHAR (150) NULL,
    [Author]         VARCHAR (250) NULL,
    [CommonName]     VARCHAR (250) NOT NULL,
    [Family]         VARCHAR (250) NOT NULL,
    [PlantHash]      VARCHAR (256) NULL,
    CONSTRAINT [PK_Plants] PRIMARY KEY CLUSTERED ([PlantId] ASC)
);



