IF NOT EXISTS (SELECT 1 FROM [dbo].[User] where [USERID] = 'E55EAD5B-E304-46F3-8399-D678678A501D' )
BEGIN
INSERT INTO [dbo].[User]
           ([UserId]
           ,[UserLogin]
           ,[UserTypeId]
           ,[Name]
           ,[Email]
           ,[PhoneNo]
           ,[IsDeleted]
           ,[VersionNo]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[LastUpdatedBy]
           ,[LastUpdatedDate])
     VALUES
           ('E55EAD5B-E304-46F3-8399-D678678A501D'
           ,'SuperAdmin'
           ,'F06EC0F3-D16F-4B9E-9261-05857F28B32D'
           ,'Super Admin 01'
           , 'see.see@ncs.com.sg'
           , '97282643'
           , 0
           , 1
           , 'System'
           , getdate()
           , 'System'
           , getdate())
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[UserRole] where [UserRoleId] = '269703A4-8BE9-4E48-9C43-4E45092FB12D' )
BEGIN
INSERT INTO [dbo].[UserRole]
           ([UserRoleId]
           ,[UserId]
           ,[RoleId]
           ,[IsDeleted]
           ,[VersionNo]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[LastUpdatedBy]
           ,[LastUpdatedDate])
     VALUES
           ( '269703A4-8BE9-4E48-9C43-4E45092FB12D'
           , 'E55EAD5B-E304-46F3-8399-D678678A501D'
           , '165723A8-DE4C-4150-8A45-8A04D46DDA54'
           , 0
           , 1
           , 'System'
           , getdate()
           , 'System'
           , getdate())
END