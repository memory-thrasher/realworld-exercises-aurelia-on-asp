CREATE TABLE [dbo].[ArticleFavorites] (
    [Username]          NVARCHAR (450)   NOT NULL,
    [ArticleId]         UNIQUEIDENTIFIER NOT NULL,
    [ArticleFavoriteId] INT              IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_ArticleFavorites] PRIMARY KEY CLUSTERED ([ArticleId] ASC, [Username] ASC),
    CONSTRAINT [FK_ArticleFavorites_Articles_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Articles] ([Id]),
    CONSTRAINT [FK_ArticleFavorites_Users_Username] FOREIGN KEY ([Username]) REFERENCES [dbo].[Users] ([Username]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ArticleFavorites_Username]
    ON [dbo].[ArticleFavorites]([Username] ASC);

