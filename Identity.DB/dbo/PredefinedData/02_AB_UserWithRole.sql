IF NOT EXISTS (SELECT 1 FROM [dbo].[User] where [USERID] = 'E55EAD5B-E304-46F3-8399-D678678A501D' )
BEGIN
INSERT INTO [dbo].[User]
           ([UserId]
           ,[UserLogin]
           ,[Password]
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
           ,'abc'
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

IF NOT EXISTS (SELECT 1 FROM [dbo].[User] where [USERID] = 'D8B3EC50-23E0-4317-B243-36A5935DE344' )
BEGIN
INSERT INTO [dbo].[User]
           ([UserId]
           ,[UserLogin]
           ,[Password]
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
           ('D8B3EC50-23E0-4317-B243-36A5935DE344'
           ,'abjw'
           ,'abc'
           ,'005FB465-9E78-4FEE-BDD2-888C4890D462'
           ,'See Jun Wei'
           , 'see.see@ncs.com.sg'
           , '97282643'
           , 0
           , 1
           , 'System'
           , getdate()
           , 'System'
           , getdate())
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[UserRole] where [UserRoleId] = '40B3BC80-AB35-40A5-B454-878B0DB0D6A1' )
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
           ( '40B3BC80-AB35-40A5-B454-878B0DB0D6A1'
           , 'D8B3EC50-23E0-4317-B243-36A5935DE344'
           , '17592BC0-A36F-4BB0-BF65-416278E4AE5B'
           , 0
           , 1
           , 'System'
           , getdate()
           , 'System'
           , getdate())
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[User] where [USERID] = '6B2B669A-CAE7-4114-ABB5-20F4C1A736F9' )
BEGIN
INSERT INTO [dbo].[User]
           ([UserId]
           ,[UserLogin]
           ,[Password]
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
           ('6B2B669A-CAE7-4114-ABB5-20F4C1A736F9'
           ,'Patient01'
           ,'abc'
           ,'D939D14A-53B3-4479-A384-7C2278543A56'
           ,'Patient 01'
           , 'see.see@ncs.com.sg'
           , '97282643'
           , 0
           , 1
           , 'System'
           , getdate()
           , 'System'
           , getdate())
END