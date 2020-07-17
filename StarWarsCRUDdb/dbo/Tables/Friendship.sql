CREATE TABLE [dbo].[Friendship] (
    [Friend_of] INT NOT NULL,
    [Friend_to] INT NOT NULL,
    CONSTRAINT [PK_Friends] PRIMARY KEY CLUSTERED ([Friend_of] ASC, [Friend_to] ASC),
    FOREIGN KEY ([Friend_of]) REFERENCES [dbo].[Character] ([CharacterId]),
    FOREIGN KEY ([Friend_to]) REFERENCES [dbo].[Character] ([CharacterId]),
    CONSTRAINT [UQ_Friends] UNIQUE NONCLUSTERED ([Friend_to] ASC, [Friend_of] ASC)
);

