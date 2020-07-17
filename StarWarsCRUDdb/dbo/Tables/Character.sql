CREATE TABLE [dbo].[Character] (
    [CharacterId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (128) NULL,
    CONSTRAINT [PK_Character] PRIMARY KEY CLUSTERED ([CharacterId] ASC)
);

