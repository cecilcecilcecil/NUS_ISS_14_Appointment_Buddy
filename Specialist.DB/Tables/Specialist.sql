CREATE TABLE [dbo].[Specialist]
(
	[SpecialistId]          VARCHAR (50)        NOT NULL, 
	[ServicesId]            VARCHAR (50)        NULL, 
    [Name]                  VARCHAR(255)        NULL,
    [Nric]                  VARCHAR(10)         NULL,
    [ContactNo]             VARCHAR(10)         NULL,
    [EmailLocalPart]		VARCHAR (64)		NULL,
    [EmailDomain]		    VARCHAR (255)		NULL,
    [Availability]          BIT                 NULL,
    [IsDeleted]			    BIT                 NULL, 
    [VersionNo]			    INT                 NULL        DEFAULT 1, 
    [CreatedById]		    VARCHAR(50)         NULL, 
    [CreatedBy]			    VARCHAR(255)        NULL, 
    [CreatedDate]		    DATETIME            NULL        DEFAULT         GETDATE(), 
    [LastUpdatedById]	    VARCHAR(50)         NULL, 
    [LastUpdatedBy]		    VARCHAR(255)        NULL, 
    [LastUpdatedDate]	    DATETIME            NULL,
    CONSTRAINT [PK_Specialist] PRIMARY KEY CLUSTERED ([SpecialistId] ASC)
)
