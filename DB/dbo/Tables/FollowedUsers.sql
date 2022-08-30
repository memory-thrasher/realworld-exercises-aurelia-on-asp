CREATE TABLE [dbo].[FollowedUsers] (
    [Username]         NVARCHAR (450) NOT NULL,
    [FollowerUsername] NVARCHAR (450) NOT NULL,
    [FollowedUsersId]  INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_FollowedUsers] PRIMARY KEY CLUSTERED ([Username] ASC, [FollowerUsername] ASC),
    CONSTRAINT [FK_FollowedUsers_Users_FollowerUsername] FOREIGN KEY ([FollowerUsername]) REFERENCES [dbo].[Users] ([Username]),
    CONSTRAINT [FK_FollowedUsers_Users_Username] FOREIGN KEY ([Username]) REFERENCES [dbo].[Users] ([Username])
);


GO
CREATE NONCLUSTERED INDEX [IX_FollowedUsers_FollowerUsername]
    ON [dbo].[FollowedUsers]([FollowerUsername] ASC);

