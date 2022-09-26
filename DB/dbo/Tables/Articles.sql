CREATE TABLE [dbo].[Articles] (
    [Id]             UNIQUEIDENTIFIER   NOT NULL,
    [Slug]           NVARCHAR (450)     NOT NULL,
    [Title]          NVARCHAR (MAX)     NOT NULL,
    [Description]    NVARCHAR (MAX)     NOT NULL,
    [Body]           NVARCHAR (MAX)     NOT NULL,
    [AuthorUsername] NVARCHAR (450)     NOT NULL,
    [CreatedAt]      DATETIMEOFFSET (7) NOT NULL,
    [UpdatedAt]      DATETIMEOFFSET (7) NOT NULL,
    CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Articles_Users_AuthorUsername] FOREIGN KEY ([AuthorUsername]) REFERENCES [dbo].[Users] ([Username])
);


GO
CREATE NONCLUSTERED INDEX [IX_Articles_AuthorUsername]
    ON [dbo].[Articles]([AuthorUsername] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Articles_Slug]
    ON [dbo].[Articles]([Slug] ASC);

