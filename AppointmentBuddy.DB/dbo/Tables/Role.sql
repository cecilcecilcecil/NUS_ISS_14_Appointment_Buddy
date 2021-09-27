CREATE TABLE [dbo].[Role]
(
	[RoleId]                    VARCHAR(50)     NOT NULL, 
    [Name]                      VARCHAR(255)    NULL,
	[RoleTypeId]                VARCHAR(50)     NOT NULL,
	[IsDeleted]                 BIT             NOT NULL,
    [VersionNo]                 INT             NULL DEFAULT 1,
    [CreatedById]               VARCHAR (50)    NULL,
	[CreatedBy]                 VARCHAR (255)   NULL,
    [CreatedDate]               DATETIME        NULL DEFAULT getdate(),
    [LastUpdatedById]           VARCHAR (50)    NULL,
	[LastUpdatedBy]             VARCHAR (255)   NULL,
    [LastUpdatedDate]           DATETIME        NULL, 
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId] ASC)
)
