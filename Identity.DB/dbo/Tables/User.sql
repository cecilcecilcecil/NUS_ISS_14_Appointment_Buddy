CREATE TABLE [dbo].[User]
(
	[UserId]                    VARCHAR(50)     NOT NULL, 
    [UserLogin]                 VARCHAR(255)    NULL, 
    [Password]                  VARCHAR(16)     NULL,
    [UserTypeId]                VARCHAR(50)     NULL, 
    [Name]                      VARCHAR(255)    NULL, 
    [Email]                     VARCHAR(100)    NULL, 
    [PhoneNo]                   VARCHAR(25)     NULL, 
    [IsDeleted]                 BIT             NOT NULL,
    [VersionNo]                 INT             NULL            DEFAULT 1,
    [CreatedBy]                 VARCHAR (255)   NULL,
    [CreatedDate]               DATETIME        NULL            DEFAULT getdate(),
    [LastUpdatedBy]             VARCHAR (255)   NULL,
    [LastUpdatedDate]           DATETIME        NULL, 
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
)
