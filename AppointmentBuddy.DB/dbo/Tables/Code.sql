CREATE TABLE [dbo].[Code]
(
	[CodeId]            VARCHAR(50)         NOT NULL, 
    [Category]          VARCHAR(255)        NULL, 
    [CodeValue]         VARCHAR(255)        NULL, 
    [ShortDescription]  VARCHAR(255)        NULL, 
    [LongDescription]   VARCHAR(255)        NULL, 
	[SequenceNo]        INT                 NULL, 
    [IsDeleted]         BIT                 NULL, 
    [VersionNo]         INT                 NULL,
	[CreatedBy]         VARCHAR(255)        NULL, 
    [CreatedDate]       DATETIME            NULL, 
    [LastUpdatedBy]     VARCHAR(255)        NULL, 
    [LastUpdatedDate]   DATETIME            NULL,
	CONSTRAINT [PK_Code] PRIMARY KEY CLUSTERED ([CodeId] ASC)
)
