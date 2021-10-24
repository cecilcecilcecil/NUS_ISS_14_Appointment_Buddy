IF NOT EXISTS (SELECT 1 FROM [dbo].[Role] where [RoleId] = '165723A8-DE4C-4150-8A45-8A04D46DDA54' )
BEGIN
INSERT INTO [dbo].[Role]
           ([RoleId]
           ,[Name]
		   ,[RoleTypeId]
           ,[IsDeleted]
           ,[VersionNo]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[LastUpdatedBy]
           ,[LastUpdatedDate])
     VALUES
           ('165723A8-DE4C-4150-8A45-8A04D46DDA54'
           ,'Admin'
		   ,'FA9DDCE8-387B-4018-B10C-A2C690B71B9F'
           ,0
           ,1
           ,'System'
           ,getdate()
           ,'System'
           ,getdate())
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Role] where [RoleId] = '143C6361-483F-4714-91DA-7421264FE66A' )
BEGIN
INSERT INTO [dbo].[Role]
           ([RoleId]
           ,[Name]
		   ,[RoleTypeId]
           ,[IsDeleted]
           ,[VersionNo]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[LastUpdatedBy]
           ,[LastUpdatedDate])
     VALUES
           ('143C6361-483F-4714-91DA-7421264FE66A'
           ,'Patient'
		   ,'8F0CD491-06CB-43EC-B874-F492C768E9EB'
           ,0
           ,1
           ,'System'
           ,getdate()
           ,'System'
           ,getdate())
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Role] where [RoleId] = '17592BC0-A36F-4BB0-BF65-416278E4AE5B' )
BEGIN
INSERT INTO [dbo].[Role]
           ([RoleId]
           ,[Name]
		   ,[RoleTypeId]
           ,[IsDeleted]
           ,[VersionNo]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[LastUpdatedBy]
           ,[LastUpdatedDate])
     VALUES
           ('17592BC0-A36F-4BB0-BF65-416278E4AE5B'
           ,'Staff'
		   ,'9B660716-4E47-4C17-A28D-09B66B44B6D6'
           ,0
           ,1
           ,'System'
           ,getdate()
           ,'System'
           ,getdate())
END