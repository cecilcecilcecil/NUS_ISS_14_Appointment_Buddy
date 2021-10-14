CREATE TABLE [dbo].[Appointment]
(
	[AppointmentId]			VARCHAR (50)		NOT NULL,
    [UserId]                VARCHAR(50)         NULL,
    [Name]                  VARCHAR(255)         NULL,
    [AppointmentDate]		DATETIME            NULL,
	[IsDeleted]			    BIT                 NULL, 
    [VersionNo]			    INT                 NULL        DEFAULT 1, 
    [CreatedById]		    VARCHAR(50)         NULL, 
    [CreatedBy]			    VARCHAR(255)        NULL, 
    [CreatedDate]		    DATETIME            NULL        DEFAULT         getdate(), 
    [LastUpdatedById]	    VARCHAR(50)         NULL, 
    [LastUpdatedBy]		    VARCHAR(255)        NULL, 
    [LastUpdatedDate]	    DATETIME            NULL,
	CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED ([AppointmentId] ASC)
)
