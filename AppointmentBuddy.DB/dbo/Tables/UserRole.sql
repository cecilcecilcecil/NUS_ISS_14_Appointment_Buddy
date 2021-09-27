CREATE TABLE [dbo].[UserRole]
(
	[UserRoleId]                VARCHAR(50)     NOT NULL, 
    [UserId]                    VARCHAR(50)     NOT NULL, 
    [RoleId]                    VARCHAR(50)     NOT NULL,
	[IsDeleted]                 BIT             NOT NULL,
    [VersionNo]                 INT             NULL        DEFAULT 1,
    [CreatedById]               VARCHAR (50)    NULL,
	[CreatedBy]                 VARCHAR (255)   NULL,
    [CreatedDate]               DATETIME        NULL        DEFAULT getdate(),
    [LastUpdatedById]           VARCHAR (50)    NULL,
	[LastUpdatedBy]             VARCHAR (255)   NULL,
    [LastUpdatedDate]           DATETIME        NULL, 
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]), 
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role]([RoleId]),
	CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserRoleId] ASC)
)
