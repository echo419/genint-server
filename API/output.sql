IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Channels] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [TopicText] nvarchar(max) NOT NULL,
    [TopicJson] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [TopicSetOn] datetime2 NULL,
    [TopicSetOnUtc] datetime2 NULL,
    [TopicSetByUserId] int NULL,
    [PasswordSetOn] datetime2 NULL,
    [PasswordSetOnUtc] datetime2 NULL,
    [PasswordSetByUserId] int NULL,
    [Flags] nvarchar(max) NULL,
    [Json] nvarchar(max) NULL,
    [AddTime] datetime2 NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_Channels] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [NickText] nvarchar(max) NOT NULL,
    [NickJson] nvarchar(max) NULL,
    [Password] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [JoinMessage] nvarchar(max) NOT NULL,
    [QuitMessage] nvarchar(max) NOT NULL,
    [LastConnectedAt] datetime2 NULL,
    [LastDisconnectedAt] datetime2 NULL,
    [IsBot] int NULL,
    [IsMuted] int NULL,
    [Json] nvarchar(max) NULL,
    [AddTime] datetime2 NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Connections] (
    [Id] int NOT NULL IDENTITY,
    [HubConnectionId] nvarchar(max) NOT NULL,
    [UserId] int NULL,
    [AddTime] datetime2 NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_Connections] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Connections_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [Logs] (
    [Id] int NOT NULL IDENTITY,
    [ChannelName] nvarchar(max) NOT NULL,
    [ChannelId] int NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [TimeStamp] datetime2 NOT NULL,
    [TimeStampUtc] datetime2 NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [UserId] int NULL,
    [NickText] nvarchar(max) NOT NULL,
    [NickJson] nvarchar(max) NULL,
    [MessageText] nvarchar(max) NULL,
    [MessageJson] nvarchar(max) NULL,
    [Year] int NOT NULL,
    [Month] int NOT NULL,
    [Day] int NOT NULL,
    [TargetNickText] nvarchar(max) NULL,
    [TargetNickJson] nvarchar(max) NULL,
    [TargetUserId] int NULL,
    [TargetUserName] nvarchar(max) NULL,
    [Json] nvarchar(max) NULL,
    [AddTime] datetime2 NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_Logs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Logs_Channels_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channels] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Logs_Users_TargetUserId] FOREIGN KEY ([TargetUserId]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_Logs_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [UserChannels] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ChannelId] int NOT NULL,
    [Flags] nvarchar(max) NULL,
    [Json] nvarchar(max) NULL,
    [AddTime] datetime2 NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_UserChannels] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserChannels_Channels_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channels] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserChannels_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ChannelConnections] (
    [Id] int NOT NULL IDENTITY,
    [ChannelId] int NOT NULL,
    [ChannelName] nvarchar(max) NOT NULL,
    [ConnectionId] int NOT NULL,
    [HubConnectionId] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ChannelConnections] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ChannelConnections_Channels_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channels] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ChannelConnections_Connections_ConnectionId] FOREIGN KEY ([ConnectionId]) REFERENCES [Connections] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'IsBot', N'IsMuted', N'JoinMessage', N'Json', N'LastConnectedAt', N'LastDisconnectedAt', N'NickJson', N'NickText', N'Password', N'QuitMessage', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [Email], [IsBot], [IsMuted], [JoinMessage], [Json], [LastConnectedAt], [LastDisconnectedAt], [NickJson], [NickText], [Password], [QuitMessage], [UserName])
VALUES (1, N'', NULL, NULL, N'', NULL, NULL, NULL, N'', N'jpzwei', N'e7f3db7fb9210b1ce3551ce23b91fd67', N'', N'jpzwei'),
(2, N'', NULL, NULL, N'', NULL, NULL, NULL, N'', N'Thacskau', N'e7f3db7fb9210b1ce3551ce23b91fd67', N'', N'Thacskau'),
(3, N'', NULL, NULL, N'', NULL, NULL, NULL, N'', N'test', N'098f6bcd4621d373cade4e832627b4f6', N'', N'test');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'IsBot', N'IsMuted', N'JoinMessage', N'Json', N'LastConnectedAt', N'LastDisconnectedAt', N'NickJson', N'NickText', N'Password', N'QuitMessage', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

CREATE INDEX [IX_ChannelConnections_ChannelId] ON [ChannelConnections] ([ChannelId]);
GO

CREATE INDEX [IX_ChannelConnections_ConnectionId] ON [ChannelConnections] ([ConnectionId]);
GO

CREATE INDEX [IX_Connections_UserId] ON [Connections] ([UserId]);
GO

CREATE INDEX [IX_Logs_ChannelId] ON [Logs] ([ChannelId]);
GO

CREATE INDEX [IX_Logs_TargetUserId] ON [Logs] ([TargetUserId]);
GO

CREATE INDEX [IX_Logs_UserId] ON [Logs] ([UserId]);
GO

CREATE INDEX [IX_UserChannels_ChannelId] ON [UserChannels] ([ChannelId]);
GO

CREATE INDEX [IX_UserChannels_UserId] ON [UserChannels] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240415193908_init', N'8.0.3');
GO

COMMIT;
GO

