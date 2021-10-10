CREATE TABLE [dbo].[Room]
(
	[RoomId]                VARCHAR (50)        NOT NULL, 
    [SpecialiesId]          VARCHAR (50)        NULL, 
    [ServicesId]            VARCHAR (50)        NULL, 
    [Availabilities]        VARCHAR(255)        NULL,
    [IsDeleted]			    BIT                 NULL, 
    [VersionNo]			    INT                 NULL        DEFAULT 1, 
    [CreatedById]		    VARCHAR(50)         NULL, 
    [CreatedBy]			    VARCHAR(255)        NULL, 
    [CreatedDate]		    DATETIME            NULL        DEFAULT         getdate(), 
    [LastUpdatedById]	    VARCHAR(50)         NULL, 
    [LastUpdatedBy]		    VARCHAR(255)        NULL, 
    [LastUpdatedDate]	    DATETIME            NULL,
    CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([RoomId] ASC)
)
