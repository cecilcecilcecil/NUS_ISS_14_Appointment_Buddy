CREATE TABLE [dbo].[PatientInfo]
(
	[PatientId]			    VARCHAR (50)		NOT NULL,
    [NRIC]                  VARCHAR(15)         NOT NULL,
    [PatientName]           VARCHAR(255)        NOT NULL,
    [UserId]                VARCHAR(50)         NULL,
    [BirthDate]   		    DATETIME            NULL,
    [DeathDate]   		    DATETIME            NULL,
    [Gender]                VARCHAR(2)          NULL,
    [Title]                 VARCHAR(5)          NULL,
    [ContactNumber]         VARCHAR(20)         NULL,
	[IsDeleted]			    BIT                 NULL, 
    [VersionNo]			    INT                 NULL        DEFAULT 1, 
    [CreatedById]		    VARCHAR(50)         NULL, 
    [CreatedBy]			    VARCHAR(255)        NULL, 
    [CreatedDate]		    DATETIME            NULL        DEFAULT         GETDATE(), 
    [LastUpdatedById]	    VARCHAR(50)         NULL, 
    [LastUpdatedBy]		    VARCHAR(255)        NULL, 
    [LastUpdatedDate]	    DATETIME            NULL,
	CONSTRAINT [PK_PatientInfo] PRIMARY KEY CLUSTERED ([PatientId] ASC)
)
