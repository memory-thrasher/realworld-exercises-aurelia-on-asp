CREATE TABLE [dbo].[ArticleTag] (
    [ArticlesId] UNIQUEIDENTIFIER NOT NULL,
    [TagsId]     NVARCHAR (450)   NOT NULL,
    CONSTRAINT [PK_ArticleTag] PRIMARY KEY CLUSTERED ([ArticlesId] ASC, [TagsId] ASC),
    CONSTRAINT [FK_ArticleTag_Articles_ArticlesId] FOREIGN KEY ([ArticlesId]) REFERENCES [dbo].[Articles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ArticleTag_Tags_TagsId] FOREIGN KEY ([TagsId]) REFERENCES [dbo].[Tags] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ArticleTag_TagsId]
    ON [dbo].[ArticleTag]([TagsId] ASC);

