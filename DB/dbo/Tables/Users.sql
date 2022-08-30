CREATE TABLE [dbo].[Users] (
    [Username] NVARCHAR (450) NOT NULL,
    [Email]    NVARCHAR (450) NOT NULL,
    [Password] NVARCHAR (MAX) NOT NULL,
    [Bio]      NVARCHAR (MAX) NOT NULL,
    [Image]    NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Username] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email]
    ON [dbo].[Users]([Email] ASC);

