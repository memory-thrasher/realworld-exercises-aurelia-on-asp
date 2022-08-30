CREATE TABLE [dbo].[Comments] (
    [Id]        INT                IDENTITY (1, 1) NOT NULL,
    [Body]      NVARCHAR (MAX)     NOT NULL,
    [Username]  NVARCHAR (450)     NOT NULL,
    [ArticleId] UNIQUEIDENTIFIER   NOT NULL,
    [CreatedAt] DATETIMEOFFSET (7) NOT NULL,
    [UpdatedAt] DATETIMEOFFSET (7) NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comments_Articles_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Articles] ([Id]),
    CONSTRAINT [FK_Comments_Users_Username] FOREIGN KEY ([Username]) REFERENCES [dbo].[Users] ([Username])
);


GO
CREATE NONCLUSTERED INDEX [IX_Comments_Username]
    ON [dbo].[Comments]([Username] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Comments_ArticleId]
    ON [dbo].[Comments]([ArticleId] ASC);

