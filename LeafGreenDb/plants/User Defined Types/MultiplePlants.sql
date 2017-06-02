CREATE TYPE [plants].[MultiplePlants] AS TABLE (
    [Symbol]         VARCHAR (15)  NULL,
    [ScientificName] VARCHAR (150) NULL,
    [Author]         VARCHAR (250) NULL,
    [CommonName]     VARCHAR (250) NOT NULL,
    [Family]         VARCHAR (250) NULL);

