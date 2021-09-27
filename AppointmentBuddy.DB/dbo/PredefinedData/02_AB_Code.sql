IF NOT EXISTS (SELECT 1 FROM [dbo].[Code] where [CodeId] = '005FB465-9E78-4FEE-BDD2-888C4890D462' )
BEGIN
INSERT INTO [dbo].[Code]
           ([CodeId]
           ,[Category]
           ,[CodeValue]
           ,[ShortDescription]
           ,[LongDescription]
           ,[SequenceNo]
           ,[IsDeleted]
           ,[VersionNo]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[LastUpdatedBy]
           ,[LastUpdatedDate])
     VALUES('005FB465-9E78-4FEE-BDD2-888C4890D462'
           ,'UserType'
           ,'Staff'
           ,'Staff'
           ,'Staff'
           ,0
           ,0
           ,1
           ,'System'
           ,getdate()
           ,'System'
           ,getdate())
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Code] where [CodeId] = 'D939D14A-53B3-4479-A384-7C2278543A56' )
BEGIN
INSERT INTO [dbo].[Code]
           ([CodeId]
           ,[Category]
           ,[CodeValue]
           ,[ShortDescription]
           ,[LongDescription]
           ,[SequenceNo]
           ,[IsDeleted]
           ,[VersionNo]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[LastUpdatedBy]
           ,[LastUpdatedDate])
     VALUES('D939D14A-53B3-4479-A384-7C2278543A56'
           ,'UserType'
           ,'Patient'
           ,'Patient'
           ,'Patient'
           ,0
           ,0
           ,1
           ,'System'
           ,getdate()
           ,'System'
           ,getdate())
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Code] where [CodeId] = 'F06EC0F3-D16F-4B9E-9261-05857F28B32D' )
BEGIN
INSERT INTO [dbo].[Code]
           ([CodeId]
           ,[Category]
           ,[CodeValue]
           ,[ShortDescription]
           ,[LongDescription]
           ,[SequenceNo]
           ,[IsDeleted]
           ,[VersionNo]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[LastUpdatedBy]
           ,[LastUpdatedDate])
     VALUES('F06EC0F3-D16F-4B9E-9261-05857F28B32D'
           ,'UserType'
           ,'Admin'
           ,'Admin'
           ,'Admin'
           ,0
           ,0
           ,1
           ,'System'
           ,getdate()
           ,'System'
           ,getdate())
END