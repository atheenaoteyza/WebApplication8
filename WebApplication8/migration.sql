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

CREATE TABLE [Authors] (
    [AuthorId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(200) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([AuthorId])
);
GO

CREATE TABLE [Books] (
    [ISBN] int NOT NULL IDENTITY,
    [Title] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([ISBN])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251014174202_InitialCreate', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ISBN', N'Title') AND [object_id] = OBJECT_ID(N'[Books]'))
    SET IDENTITY_INSERT [Books] ON;
INSERT INTO [Books] ([ISBN], [Title])
VALUES (1, N'The Hobbit'),
(2, N'The Running Scissors');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ISBN', N'Title') AND [object_id] = OBJECT_ID(N'[Books]'))
    SET IDENTITY_INSERT [Books] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251015042456_InitBookstore', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Books] ADD [Discount] float NOT NULL DEFAULT 0.0E0;
GO

ALTER TABLE [Books] ADD [Price] float NOT NULL DEFAULT 0.0E0;
GO

UPDATE [Books] SET [Discount] = 0.0E0, [Price] = 5.0E0
WHERE [ISBN] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Books] SET [Discount] = 0.25E0, [Price] = 3.0E0
WHERE [ISBN] = 2;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251015073144_AddDiscount', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'Discount');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Books] ALTER COLUMN [Discount] float NULL;
GO

UPDATE [Books] SET [Discount] = NULL
WHERE [ISBN] = 1;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251015074826_MakeDiscountNullable', N'8.0.0');
GO

COMMIT;
GO

