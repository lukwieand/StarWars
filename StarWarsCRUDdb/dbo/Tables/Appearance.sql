CREATE TABLE [dbo].[Appearance] (
    [CharacterId] INT NOT NULL,
    [EpisodeId]   INT NOT NULL,
    CONSTRAINT [PK_Friends_CharacterId_EpisodeId] PRIMARY KEY CLUSTERED ([CharacterId] ASC, [EpisodeId] ASC),
    FOREIGN KEY ([CharacterId]) REFERENCES [dbo].[Character] ([CharacterId]),
    FOREIGN KEY ([EpisodeId]) REFERENCES [dbo].[Episode] ([EpisodeId])
);

