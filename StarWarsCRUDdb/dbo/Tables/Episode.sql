CREATE TABLE [dbo].[Episode] (
    [EpisodeId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (128) NULL,
    CONSTRAINT [PK_Episode] PRIMARY KEY CLUSTERED ([EpisodeId] ASC)
);

