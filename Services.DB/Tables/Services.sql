CREATE TABLE [dbo].[Services]
(
	[ServicesId]            VARCHAR (50)        NOT NULL, 
    [Description]           VARCHAR(255)        NULL,
    [IsDeleted]			    BIT                 NULL, 
    [VersionNo]			    INT                 NULL        DEFAULT 1, 
    [CreatedById]		    VARCHAR(50)         NULL, 
    [CreatedBy]			    VARCHAR(255)        NULL, 
    [CreatedDate]		    DATETIME            NULL        DEFAULT         GETDATE(), 
    [LastUpdatedById]	    VARCHAR(50)         NULL, 
    [LastUpdatedBy]		    VARCHAR(255)        NULL, 
    [LastUpdatedDate]	    DATETIME            NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED ([ServicesId] ASC)
)
