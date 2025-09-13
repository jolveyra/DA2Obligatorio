SELECT TOP (1000) [Id]
      ,[Name]
      ,[SharedExpenses]
      ,[AddressId]
      ,[ConstructorCompanyId]
      ,[ManagerId]
      ,[MaintenanceEmployees]
  FROM [BuildingBossDb].[dbo].[Buildings]



SELECT TOP (1000) [Id]
      ,[Name]
      ,[Surname]
      ,[Email]
      ,[Discriminator]
      ,[Password]
      ,[Role]
      ,[ConstructorCompanyId]
  FROM [BuildingBossDb].[dbo].[People]

INSERT INTO [BuildingBossDb].[dbo].[People]
           ([Name]
           ,[Surname]
           ,[Email]
           ,[Discriminator]
           ,[Password]
           ,[Role]
           ,[ConstructorCompanyId])
     VALUES
           ('John'
           , 'Doe'
           , 'john@mail.com'
           , 'User'
           , 'Password12345'
           , 0
           , NULL)



-- E1DCC326-2574-43DE-68BB-08DC688BE76B	Admin	Admin	admin@gmail.com	User	Admin1234	0	NULL
-- 79F06116-23AF-497E-001F-08DC827C89D1	UnManager		unmanager@mail.com	User	Default1234	1	NULL
-- ECD9BF48-0D61-4148-0020-08DC827C89D1	UnConComAdmin		unconcomadmin@mail.com	User	Default1234	3	NULL

INSERT INTO [BuildingBossDb].[dbo].[People]
           ([Name]
           ,[Surname]
           ,[Email]
           ,[Discriminator]
           ,[Password]
           ,[Role]
           ,[ConstructorCompanyId])
     VALUES(
        'E1DCC326-2574-43DE-68BB-08DC688BE76B'.
        'Admin',
        'Admin',
        'admin@gmail.com',
        'User',
        'Admin1234',
        0,
        NULL
     )

INSERT INTO [BuildingBossDb].[dbo].[People]
           ([Name]
           ,[Surname]
           ,[Email]
           ,[Discriminator]
           ,[Password]
           ,[Role]
           ,[ConstructorCompanyId])
     VALUES(
        '79F06116-23AF-497E-001F-08DC827C89D1',
        'UnManager',
        'unmanager@mail.com',
        'User',
        'Default1234',
        1,
        NULL)

INSERT INTO [BuildingBossDb].[dbo].[People]
           ([Name]
           ,[Surname]
           ,[Email]
           ,[Discriminator]
           ,[Password]
           ,[Role]
           ,[ConstructorCompanyId])
     VALUES(
        'ECD9BF48-0D61-4148-0020-08DC827C89D1',
        'UnConComAdmin',
        'unconcomadmin@mail.com',
        'User',
        'Default1234',
        3,
        NULL)

-- 5a9e6f6d-648b-49c9-93b8-08dc688be788	e1dcc326-2574-43de-68bb-08dc688be76b
-- 1439c6c4-ca11-4fac-1827-08dc827c89e4	79f06116-23af-497e-001f-08dc827c89d1
-- 51a580d2-d32c-4383-1828-08dc827c89e4	ecd9bf48-0d61-4148-0020-08dc827c89d1

INSERT INTO [BuildingBossDb].[dbo].[Sessions]
([Id],
[UserId])
VALUES(
    '5a9e6f6d-648b-49c9-93b8-08dc688be788',
    'e1dcc326-2574-43de-68bb-08dc688be76b'
)
INSERT INTO [BuildingBossDb].[dbo].[Sessions]
([Id],
[UserId])
VALUES(
    '1439c6c4-ca11-4fac-1827-08dc827c89e4',
    '79f06116-23af-497e-001f-08dc827c89d1'
)
INSERT INTO [BuildingBossDb].[dbo].[Sessions]
([Id],
[UserId])
VALUES(
    '51a580d2-d32c-4383-1828-08dc827c89e4',
    'ecd9bf48-0d61-4148-0020-08dc827c89d1'
)

