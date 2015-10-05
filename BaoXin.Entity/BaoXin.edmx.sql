
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/29/2015 20:23:20
-- Generated from EDMX file: E:\bx\Code\BaoXin\BaoXin.Entity\BaoXin.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db_baoxin];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ChatInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChatInfo];
GO
IF OBJECT_ID(N'[dbo].[SendFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SendFile];
GO
IF OBJECT_ID(N'[dbo].[SpeechInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpeechInfo];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UsersFriend]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersFriend];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ChatInfo'
CREATE TABLE [dbo].[ChatInfo] (
    [id] int IDENTITY(1,1) NOT NULL,
    [user_from] varchar(50)  NOT NULL,
    [user_to] varchar(50)  NOT NULL,
    [content] varchar(255)  NOT NULL,
    [expression] varchar(50)  NOT NULL,
    [color] varchar(50)  NOT NULL,
    [ispublic] bit  NOT NULL,
    [sendtime] datetime  NOT NULL
);
GO

-- Creating table 'SendFile'
CREATE TABLE [dbo].[SendFile] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Accept_UserName] varchar(50)  NULL,
    [Send_UserName] varchar(50)  NULL,
    [Send_File] varchar(100)  NULL,
    [Send_Time] datetime  NULL,
    [Send_FileName] varchar(100)  NULL
);
GO

-- Creating table 'SpeechInfo'
CREATE TABLE [dbo].[SpeechInfo] (
    [Id] uniqueidentifier  NOT NULL,
    [FromUser] uniqueidentifier  NULL,
    [SpeachContent] nvarchar(350)  NULL,
    [SpeechImage] nvarchar(100)  NULL,
    [IsVip] tinyint  NULL,
    [State] tinyint  NULL,
    [SumbitTime] datetime  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [NickName] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Contactqq] nvarchar(50)  NULL,
    [Contactaddr] nvarchar(50)  NULL,
    [UserName] nvarchar(50)  NULL,
    [Mobile] nvarchar(50)  NULL,
    [avatar] nvarchar(50)  NULL,
    [RankCredits] int  NULL,
    [IsOnline] tinyint  NULL,
    [AddTime] datetime  NULL,
    [HeadImage] nvarchar(100)  NULL,
    [HeadSmallImage] nvarchar(100)  NULL
);
GO

-- Creating table 'UsersFriend'
CREATE TABLE [dbo].[UsersFriend] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [FriendId] uniqueidentifier  NOT NULL,
    [AddTime] datetime  NULL,
    [IsDel] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'ChatInfo'
ALTER TABLE [dbo].[ChatInfo]
ADD CONSTRAINT [PK_ChatInfo]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'SendFile'
ALTER TABLE [dbo].[SendFile]
ADD CONSTRAINT [PK_SendFile]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Id] in table 'SpeechInfo'
ALTER TABLE [dbo].[SpeechInfo]
ADD CONSTRAINT [PK_SpeechInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsersFriend'
ALTER TABLE [dbo].[UsersFriend]
ADD CONSTRAINT [PK_UsersFriend]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------