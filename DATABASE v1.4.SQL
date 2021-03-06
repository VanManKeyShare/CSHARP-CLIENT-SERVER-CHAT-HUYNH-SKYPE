USE [HUYNH_SKYPE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[accounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[account] [varchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[full_name] [nvarchar](50) NOT NULL,
	[avatar] [nvarchar](max) NOT NULL,
	[date_time_create] [datetime] NOT NULL,
	CONSTRAINT [PK_accounts] PRIMARY KEY CLUSTERED ([id] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [unique_account_accounts] UNIQUE NONCLUSTERED ([account] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[accounts] ADD  CONSTRAINT [DF_accounts_date_time_create]  DEFAULT (getdate()) FOR [date_time_create]
GO

CREATE TABLE [dbo].[sessions](
	[account] [varchar](50) NOT NULL,
	[session_key] [varchar](50) NOT NULL,
	[last_active] [datetime] NOT NULL,
	CONSTRAINT [unique_account_sessions] UNIQUE NONCLUSTERED ([account] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[sessions]  WITH CHECK ADD  CONSTRAINT [FK_account_sessions__account_accounts] FOREIGN KEY([account]) REFERENCES [dbo].[accounts] ([account])
GO
ALTER TABLE [dbo].[sessions] CHECK CONSTRAINT [FK_account_sessions__account_accounts]
GO
ALTER TABLE [dbo].[sessions] ADD  CONSTRAINT [DF_sessions_last_active]  DEFAULT (getdate()) FOR [last_active]
GO

CREATE TABLE [dbo].[friends](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[acc1] [varchar](50) NOT NULL,
	[acc2] [varchar](50) NOT NULL,
	CONSTRAINT [PK_friends] PRIMARY KEY CLUSTERED ([id] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
	CONSTRAINT [unique_acc1_acc2_friends] UNIQUE NONCLUSTERED ([acc1] ASC,[acc2] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[friends]  WITH CHECK ADD  CONSTRAINT [FK_acc1_friends__account_accounts] FOREIGN KEY([acc1]) REFERENCES [dbo].[accounts] ([account])
GO
ALTER TABLE [dbo].[friends] CHECK CONSTRAINT [FK_acc1_friends__account_accounts]
GO
ALTER TABLE [dbo].[friends]  WITH CHECK ADD  CONSTRAINT [FK_acc2_friends__account_accounts] FOREIGN KEY([acc2]) REFERENCES [dbo].[accounts] ([account])
GO
ALTER TABLE [dbo].[friends] CHECK CONSTRAINT [FK_acc2_friends__account_accounts]
GO

CREATE TABLE [dbo].[attachments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[acc1] [varchar](50) NOT NULL,
	[acc2] [varchar](50) NOT NULL,
	[file_name] [nvarchar](max) NOT NULL,
	[data] [nvarchar](max) NOT NULL,
	CONSTRAINT [PK_attachments] PRIMARY KEY CLUSTERED ([id] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[attachments]  WITH CHECK ADD  CONSTRAINT [FK_acc1_attachments__account_accounts] FOREIGN KEY([acc1]) REFERENCES [dbo].[accounts] ([account])
GO
ALTER TABLE [dbo].[attachments] CHECK CONSTRAINT [FK_acc1_attachments__account_accounts]
GO
ALTER TABLE [dbo].[attachments]  WITH CHECK ADD  CONSTRAINT [FK_acc2_attachments__account_accounts] FOREIGN KEY([acc2]) REFERENCES [dbo].[accounts] ([account])
GO
ALTER TABLE [dbo].[attachments] CHECK CONSTRAINT [FK_acc2_attachments__account_accounts]
GO

CREATE TABLE [dbo].[chats](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[msg] [nvarchar](max) NOT NULL,
	[acc1] [varchar](50) NOT NULL,
	[acc2] [varchar](50) NOT NULL,
	[date_time_create] [datetime] NOT NULL,
	[mask_delete_by_acc1] [bit] NOT NULL,
	[mask_delete_by_acc2] [bit] NOT NULL,
	[id_attachment] [varchar] NOT NULL,
	CONSTRAINT [PK_chats] PRIMARY KEY CLUSTERED ([id] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[chats]  WITH CHECK ADD  CONSTRAINT [FK_acc1_chats__account_accounts] FOREIGN KEY([acc1]) REFERENCES [dbo].[accounts] ([account])
GO
ALTER TABLE [dbo].[chats] CHECK CONSTRAINT [FK_acc1_chats__account_accounts]
GO
ALTER TABLE [dbo].[chats]  WITH CHECK ADD  CONSTRAINT [FK_acc2_chats__account_accounts] FOREIGN KEY([acc2]) REFERENCES [dbo].[accounts] ([account])
GO
ALTER TABLE [dbo].[chats] CHECK CONSTRAINT [FK_acc2_chats__account_accounts]
GO
ALTER TABLE [dbo].[chats] ADD  CONSTRAINT [DF_chats_date_time_create]  DEFAULT (getdate()) FOR [date_time_create]
GO
ALTER TABLE [dbo].[chats] ADD  CONSTRAINT [DF_chats_mask_delete_by_user1]  DEFAULT ((0)) FOR [mask_delete_by_acc1]
GO
ALTER TABLE [dbo].[chats] ADD  CONSTRAINT [DF_chats_mask_delete_by_user2]  DEFAULT ((0)) FOR [mask_delete_by_acc2]
GO

CREATE TABLE [dbo].[chats_public](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[msg] [nvarchar](max) NOT NULL,
	[acc1] [varchar](50) NOT NULL,
	[date_time_create] [datetime] NOT NULL CONSTRAINT [DF_chats_public_date_time_create]  DEFAULT (getdate()),
	[mask_delete_by_acc1] [bit] NOT NULL CONSTRAINT [DF_chats_public_mask_delete_by_acc1]  DEFAULT ((0)),
	CONSTRAINT [PK_chats_public] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[chats_public]  WITH CHECK ADD  CONSTRAINT [FK_acc1_chats_public__account_accounts] FOREIGN KEY([acc1])REFERENCES [dbo].[accounts] ([account])
GO
ALTER TABLE [dbo].[chats_public] CHECK CONSTRAINT [FK_acc1_chats_public__account_accounts]
