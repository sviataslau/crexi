CREATE TABLE [dbo].[UserAccessHistory]
(
	[user_access_history_id] INT identity(1,1) NOT NULL,
	[user_ip_address] nvarchar(20) NOT NULL,
	[access_date] datetime not null,
	[user_agent] nvarchar(100) NULL,
	CONSTRAINT [PK_user_access_history]	PRIMARY KEY CLUSTERED ([user_access_history_id] ASC)
)
