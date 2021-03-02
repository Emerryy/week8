
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='pet-info')
DROP DATABASE [pet-info];
GO

-- Create a new World Database
CREATE DATABASE [pet-info];
GO

-- Switch to the World Database
USE [pet-info]
GO

-- Begin a TRANSACTION that must complete with no errors
BEGIN TRANSACTION;

CREATE TABLE [owners](
	[owner_id] [int] IDENTITY(3000,1) NOT NULL,
	[full_name] [varchar](120) NOT NULL,
	[phone] [varchar](12) NOT NULL,
	[email] [varchar](60) NOT NULL,
	CONSTRAINT pk_owners_owner_id PRIMARY KEY (owner_id)
);
GO

CREATE TABLE [pets](
	[id] [int] IDENTITY(1000,1) NOT NULL,
	[name] [nvarchar](60) NOT NULL,
	[type] [nvarchar](60) NOT NULL,
	[breed] [nvarchar](60) NOT NULL,
	[owner] [int] NOT NULL,
	CONSTRAINT pk_pets_id PRIMARY KEY (id),
	CONSTRAINT fk_owner_owners_owner_id FOREIGN KEY (owner) REFERENCES [owners](owner_id)
);

CREATE TABLE [users](
	[user_id] [int] IDENTITY(2000,1) NOT NULL,
	[username] [varchar](255) NOT NULL,
	[password_hash] [varchar](48) NOT NULL,
	[salt] [varchar](256) NOT NULL,
	CONSTRAINT pk_users_user_id PRIMARY KEY (user_id)
);



GO
SET IDENTITY_INSERT [dbo].[owners] ON 
INSERT [dbo].[owners] ([owner_id], [full_name], [phone], [email]) VALUES (3000, 'John Fulton', '614-565-8382', N'john@johnfulton.org')
SET IDENTITY_INSERT [dbo].[owners] OFF


GO
SET IDENTITY_INSERT [dbo].[pets] ON 
INSERT [dbo].[pets] ([id], [name], [type], [breed], [owner]) VALUES (1000, N'Bella', N'dog', N'GSD', 3000)
INSERT [dbo].[pets] ([id], [name], [type], [breed], [owner]) VALUES (1001, N'Primrose', N'cat', N'DSH', 3000)
INSERT [dbo].[pets] ([id], [name], [type], [breed], [owner]) VALUES (1002, N'Gabriel', N'cat', N'DSH', 3000)
INSERT [dbo].[pets] ([id], [name], [type], [breed], [owner]) VALUES (1003, N'Penelope', N'cat', N'DSH', 3000)
SET IDENTITY_INSERT [dbo].[pets] OFF

GO

SET IDENTITY_INSERT [dbo].[users] ON 
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt]) VALUES (2000, N'admin', N'BdxyDMZv4QOGdg6OzM/rTMeHO2k=', N't23AJ8cY+HI=')
SET IDENTITY_INSERT [dbo].[users] OFF
GO

COMMIT TRANSACTION;