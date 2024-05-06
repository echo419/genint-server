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

CREATE TABLE [AppContentElements] (
    [Id] int NOT NULL IDENTITY,
    [ParentId] int NULL,
    [Title] nvarchar(max) NULL,
    [Text] nvarchar(max) NULL,
    [Icon] nvarchar(max) NULL,
    [AddTime] datetime2 NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_AppContentElements] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AppContentElements_AppContentElements_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [AppContentElements] ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [AddTime] datetime2 NULL DEFAULT (getutcdate()),
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'PasswordHash', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [PasswordHash], [UserName])
VALUES (1, N'04980744f74f4ec36ad5a9d5fec8876f', N'genesys');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'PasswordHash', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

CREATE INDEX [IX_AppContentElements_ParentId] ON [AppContentElements] ([ParentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240505182444_init', N'8.0.3');
GO

COMMIT;
GO

