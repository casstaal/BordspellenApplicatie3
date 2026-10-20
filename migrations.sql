IF OBJECT_ID(N'[Bordspellenavond].[EFMigrationHistory]') IS NULL
BEGIN
    IF SCHEMA_ID(N'Bordspellenavond') IS NULL EXEC(N'CREATE SCHEMA [Bordspellenavond];');
    CREATE TABLE [Bordspellenavond].[EFMigrationHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_EFMigrationHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    IF SCHEMA_ID(N'Bordspellenavond') IS NULL EXEC(N'CREATE SCHEMA [Bordspellenavond];');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    CREATE TABLE [Bordspellenavond].[Addresses] (
        [Id] uniqueidentifier NOT NULL,
        [Street] nvarchar(max) NOT NULL,
        [Number] nvarchar(max) NOT NULL,
        [City] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    CREATE TABLE [Bordspellenavond].[BoardGameNights] (
        [Id] int NOT NULL IDENTITY,
        [OrganizerId] int NOT NULL,
        [DateTime] datetime2 NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [MaxPlayers] int NOT NULL,
        [Food] nvarchar(max) NOT NULL,
        [IsLactoseIntoleranceFriendly] bit NOT NULL,
        [IsVegetarianFriendly] bit NOT NULL,
        [IsNutAllergyFriendly] bit NOT NULL,
        [IsNonAlcoholicFriendly] bit NOT NULL,
        CONSTRAINT [PK_BoardGameNights] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    CREATE TABLE [Bordspellenavond].[Games] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Genre] int NOT NULL,
        [Is18Plus] bit NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [Type] int NOT NULL,
        [BoardGameNightId] int NULL,
        CONSTRAINT [PK_Games] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Games_BoardGameNights_BoardGameNightId] FOREIGN KEY ([BoardGameNightId]) REFERENCES [Bordspellenavond].[BoardGameNights] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    CREATE TABLE [Bordspellenavond].[User] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [BirthDate] date NOT NULL,
        [Gender] int NOT NULL,
        [AddressId] uniqueidentifier NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [IsLactoseIntolerant] bit NOT NULL,
        [IsVegetarian] bit NOT NULL,
        [HasNutAllergy] bit NOT NULL,
        [DrinksAlcohol] bit NOT NULL,
        [BoardGameNightId] int NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_User_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Bordspellenavond].[Addresses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_User_BoardGameNights_BoardGameNightId] FOREIGN KEY ([BoardGameNightId]) REFERENCES [Bordspellenavond].[BoardGameNights] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_BoardGameNights_OrganizerId] ON [Bordspellenavond].[BoardGameNights] ([OrganizerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Games_BoardGameNightId] ON [Bordspellenavond].[Games] ([BoardGameNightId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_User_AddressId] ON [Bordspellenavond].[User] ([AddressId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_User_BoardGameNightId] ON [Bordspellenavond].[User] ([BoardGameNightId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[BoardGameNights] ADD CONSTRAINT [FK_BoardGameNights_User_OrganizerId] FOREIGN KEY ([OrganizerId]) REFERENCES [Bordspellenavond].[User] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241014135659_InitialCreate'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241014135659_InitialCreate', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017111512_Added-IdentityID'
)
BEGIN
    EXEC sp_rename N'[Bordspellenavond].[User].[BirthDate]', N'DateOfBirth', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017111512_Added-IdentityID'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[User]') AND [c].[name] = N'ImageUrl');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[User] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Bordspellenavond].[User] ALTER COLUMN [ImageUrl] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017111512_Added-IdentityID'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[User] ADD [IdentityId] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017111512_Added-IdentityID'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[User] ADD [Role] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017111512_Added-IdentityID'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241017111512_Added-IdentityID', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[BoardGameNights] DROP CONSTRAINT [FK_BoardGameNights_User_OrganizerId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[User] DROP CONSTRAINT [FK_User_Addresses_AddressId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[User] DROP CONSTRAINT [FK_User_BoardGameNights_BoardGameNightId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[User] DROP CONSTRAINT [PK_User];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    EXEC sp_rename N'[Bordspellenavond].[User]', N'Users';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    EXEC sp_rename N'[Bordspellenavond].[Users].[IX_User_BoardGameNightId]', N'IX_Users_BoardGameNightId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    EXEC sp_rename N'[Bordspellenavond].[Users].[IX_User_AddressId]', N'IX_Users_AddressId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[BoardGameNights] ADD CONSTRAINT [FK_BoardGameNights_Users_OrganizerId] FOREIGN KEY ([OrganizerId]) REFERENCES [Bordspellenavond].[Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD CONSTRAINT [FK_Users_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Bordspellenavond].[Addresses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD CONSTRAINT [FK_Users_BoardGameNights_BoardGameNightId] FOREIGN KEY ([BoardGameNightId]) REFERENCES [Bordspellenavond].[BoardGameNights] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017122423_Added-User-DbSet'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241017122423_Added-User-DbSet', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017124140_Added-Owned-In-Address'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [FK_Users_Addresses_AddressId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017124140_Added-Owned-In-Address'
)
BEGIN
    DROP TABLE [Bordspellenavond].[Addresses];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017124140_Added-Owned-In-Address'
)
BEGIN
    DROP INDEX [IX_Users_AddressId] ON [Bordspellenavond].[Users];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017124140_Added-Owned-In-Address'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'AddressId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [AddressId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017124140_Added-Owned-In-Address'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [Address_City] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017124140_Added-Owned-In-Address'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [Address_Number] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017124140_Added-Owned-In-Address'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [Address_Street] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017124140_Added-Owned-In-Address'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241017124140_Added-Owned-In-Address', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017141442_Changed-DateOnly-To-DateTime'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'DateOfBirth');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Bordspellenavond].[Users] ALTER COLUMN [DateOfBirth] datetime2 NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017141442_Changed-DateOnly-To-DateTime'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241017141442_Changed-DateOnly-To-DateTime', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017142745_Deleted-Required'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241017142745_Deleted-Required', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'Address_City');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [Address_City];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'Address_Number');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [Address_Number];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'Address_Street');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [Address_Street];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'DateOfBirth');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [DateOfBirth];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'DrinksAlcohol');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [DrinksAlcohol];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'FirstName');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [FirstName];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'Gender');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [Gender];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'HasNutAllergy');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [HasNutAllergy];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'ImageUrl');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [ImageUrl];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'IsLactoseIntolerant');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [IsLactoseIntolerant];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'IsVegetarian');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [IsVegetarian];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'LastName');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [LastName];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241017160425_Test'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241017160425_Test', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [Address_City] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [Address_Number] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [Address_Street] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [DateOfBirth] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [DrinksAlcohol] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [FirstName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [Gender] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [HasNutAllergy] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [ImageUrl] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [IsLactoseIntolerant] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [IsVegetarian] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] ADD [LastName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241020162546_ChangedUser'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241020162546_ChangedUser', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241021163919_Changed-BoardGameNight'
)
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[BoardGameNights]') AND [c].[name] = N'ImageUrl');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[BoardGameNights] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Bordspellenavond].[BoardGameNights] ALTER COLUMN [ImageUrl] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241021163919_Changed-BoardGameNight'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[BoardGameNights] ADD [Address_City] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241021163919_Changed-BoardGameNight'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[BoardGameNights] ADD [Address_Number] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241021163919_Changed-BoardGameNight'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[BoardGameNights] ADD [Address_Street] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241021163919_Changed-BoardGameNight'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[BoardGameNights] ADD [Is18Plus] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241021163919_Changed-BoardGameNight'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241021163919_Changed-BoardGameNight', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241022211311_Added-UserBoardGameNight'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[BoardGameNights] ADD [IsSignedUp] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241022211311_Added-UserBoardGameNight'
)
BEGIN
    CREATE TABLE [Bordspellenavond].[UserBoardGameNights] (
        [Id] int NOT NULL IDENTITY,
        [BoardGameNightId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_UserBoardGameNights] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserBoardGameNights_BoardGameNights_BoardGameNightId] FOREIGN KEY ([BoardGameNightId]) REFERENCES [Bordspellenavond].[BoardGameNights] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserBoardGameNights_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Bordspellenavond].[Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241022211311_Added-UserBoardGameNight'
)
BEGIN
    CREATE INDEX [IX_UserBoardGameNights_BoardGameNightId] ON [Bordspellenavond].[UserBoardGameNights] ([BoardGameNightId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241022211311_Added-UserBoardGameNight'
)
BEGIN
    CREATE INDEX [IX_UserBoardGameNights_UserId] ON [Bordspellenavond].[UserBoardGameNights] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241022211311_Added-UserBoardGameNight'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241022211311_Added-UserBoardGameNight', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [FK_Users_BoardGameNights_BoardGameNightId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    DROP INDEX [IX_Users_BoardGameNightId] ON [Bordspellenavond].[Users];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Users]') AND [c].[name] = N'BoardGameNightId');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Users] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Bordspellenavond].[Users] DROP COLUMN [BoardGameNightId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[UserBoardGameNights] ADD [BoardGameNightId1] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bordspellenavond].[Games]') AND [c].[name] = N'ImageUrl');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Bordspellenavond].[Games] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Bordspellenavond].[Games] ALTER COLUMN [ImageUrl] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    CREATE TABLE [Bordspellenavond].[Reviews] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Score] int NOT NULL,
        [TextReview] nvarchar(max) NOT NULL,
        [AuthorId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [BoardGameNightId] int NOT NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reviews_BoardGameNights_BoardGameNightId] FOREIGN KEY ([BoardGameNightId]) REFERENCES [Bordspellenavond].[BoardGameNights] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Reviews_Users_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Bordspellenavond].[Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    CREATE INDEX [IX_UserBoardGameNights_BoardGameNightId1] ON [Bordspellenavond].[UserBoardGameNights] ([BoardGameNightId1]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    CREATE INDEX [IX_Reviews_AuthorId] ON [Bordspellenavond].[Reviews] ([AuthorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    CREATE INDEX [IX_Reviews_BoardGameNightId] ON [Bordspellenavond].[Reviews] ([BoardGameNightId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    ALTER TABLE [Bordspellenavond].[UserBoardGameNights] ADD CONSTRAINT [FK_UserBoardGameNights_BoardGameNights_BoardGameNightId1] FOREIGN KEY ([BoardGameNightId1]) REFERENCES [Bordspellenavond].[BoardGameNights] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [Bordspellenavond].[EFMigrationHistory]
    WHERE [MigrationId] = N'20241024120508_AddedReviews'
)
BEGIN
    INSERT INTO [Bordspellenavond].[EFMigrationHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241024120508_AddedReviews', N'8.0.10');
END;
GO

COMMIT;
GO

