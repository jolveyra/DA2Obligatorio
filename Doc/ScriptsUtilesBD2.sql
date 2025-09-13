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

INSERT INTO [BuildingBoss].[dbo].[ConstructorCompany]

INSERT INTO [BuildingBoss].[dbo].[People]
VALUES('E1DCC326-2574-43DE-68BB-08DC688BE76B'.
        'Admin',
        'Admin',
        'admin@gmail.com')

INSERT INTO [BuildingBoss].[dbo].[Users]
VALUES(
    'E1DCC326-2574-43DE-68BB-08DC688BE76B',
    'Admin1234'
)   

INSERT INTO [BuildingBoss].[dbo].[Sessions],
VALUES(
    '5a9e6f6d-648b-49c9-93b8-08dc688be788',
    'E1DCC326-2574-43DE-68BB-08DC688BE76B'
)


SELECT TOP (1000) [Id]
      ,[Name]
      ,[Surname]
      ,[Email]
      ,[Discriminator]
      ,[Password]
      ,[Role]
  FROM [BuildingBossDb].[dbo].[People]
  --6FF87925-F347-4CC5-E613-08DC6974338B

  select TOP(1000) *
  FROM [BuildingBossDb].[dbo].[Sessions]
  WHERE UserId= '26B62925-A299-4FFB-B6AC-08DC694CDCC6';


  select TOP(1000) *
  FROM [BuildingBossDb].[dbo].[People]
  WHERE Id= '26B62925-A299-4FFB-B6AC-08DC694CDCC6';

  SELECT TOP (1000) [Id]
      ,[Name]
      ,[SharedExpenses]
      ,[AddressId]
      ,[ConstructorCompany]
      ,[ManagerId]
      ,[MaintenanceEmployees]
  FROM [BuildingBossDb].[dbo].[Buildings]


  SELECT TOP (1000) [Id]
      ,[Description]
      ,[FlatId]
      ,[CategoryId]
      ,[AssignedEmployeeId]
      ,[Status]
      ,[StartingDate]
      ,[CompletionDate]
      ,[ManagerId]
  FROM [BuildingBossDb].[dbo].[Requests]

  SELECT TOP (1000) [Id]
      ,[BuildingId]
      ,[Number]
      ,[Floor]
      ,[OwnerId]
      ,[Rooms]
      ,[Bathrooms]
      ,[HasBalcony]
  FROM [BuildingBossDb].[dbo].[Flats]

  

SELECT TOP (1000) [Id]
      ,[Name]
      ,[Surname]
      ,[Email]
      ,[Discriminator]
      ,[Password]
      ,[Role]
  FROM [BuildingBossDb].[dbo].[People]
  --6FF87925-F347-4CC5-E613-08DC6974338B

  select TOP(1000) *
  FROM [BuildingBossDb].[dbo].[Sessions]
  WHERE UserId= 'E3D80D66-FB3C-4490-5FE4-08DC69A18232';

  select TOP(1000) *
  FROM [BuildingBossDb].[dbo].[Sessions]
  WHERE Id= '5663863A-EADE-4615-5E72-08DC694CDCCB';


SELECT *
FROM [BuildingBossDb].[dbo].[Sessions] s, [BuildingBossDb].[dbo].[People] p
WHERE s.Id = p.Id
AND p.Email = 'unconcomadmin@mail.com'

  --
SELECT TOP (1000) b.Id buildingId, f.Id flatId, f.*
  FROM [BuildingBossDb].[dbo].[Buildings] b,
  [BuildingBossDb].[dbo].[Flats] f
  WHERE b.Id = f.BuildingId
  AND b.Id = 'a700533c-f191-49fb-fc14-08dc6a25f5e5';

  select TOP(1000) *
  FROM [BuildingBossDb].[dbo].[People]
  WHERE Id= '26B62925-A299-4FFB-B6AC-08DC694CDCC6';

  SELECT TOP (1000) *
  FROM [BuildingBossDb].[dbo].[Buildings]
  where ManagerId = '26B62925-A299-4FFB-B6AC-08DC694CDCC6';

select TOP(10) *
FROM [BuildingBossDb].[dbo].[Requests]
where Id = '7BE782A4-B8FC-4D42-2C11-08DC6999A5BF';

SELECT TOP(1) *
FROM [BuildingBossDb].[dbo].[Categories]
WHERE ID = '71CA16A8-993C-472A-88E8-08DC6999A5CB';

SELECT TOP(1) *
FROM [BuildingBossDb].[dbo].[Buildings]
WHERE ID = 'd172bf20-793d-4562-9e6d-08dc6a2015aa';
--d172bf20-793d-4562-9e6d-08dc6a2015aa

--46b33102-e1e3-4ada-963d-28bc62e9a58a
SELECT TOP (1) *
FROM [BuildingBossDb].[dbo].[Flats]
WHERE ID = '2390dfcb-cfc8-44a0-a0bd-b4ecccfb3486';

SELECT TOP (1) *
FROM [BuildingBossDb].[dbo].[People]
WHERE ID = 'CCEE3757-5640-47F5-A702-08DC6A2C9A64';

SELECT TOP (1) *
FROM [BuildingBossDb].[dbo].[People]
WHERE Surname = 'Wallace';

    update [BuildingBossDb].[dbo].[Requests] set ManagerId = '26B62925-A299-4FFB-B6AC-08DC694CDCC6'
  where Id = '7BE782A4-B8FC-4D42-2C11-08DC6999A5BF'

  --5663863A-EADE-4615-5E72-08DC694CDCCB

SELECT TOP (500) Id
FROM [BuildingBossDb].[dbo].[People] p
where (p.Email IS NULL OR p.Email = '')
AND not exists(
    SELECT TOP (100) *
    FROM [BuildingBossDb].[dbo].[Flats] flats
    WHERE flats.OwnerId = p.Id);

SELECT TOP (100) *
FROM [BuildingBossDb].[dbo].[Flats] flats;

SELECT TOP (500) *
FROM [BuildingBossDb].[dbo].[People] p;

SELECT COUNT(*)
FROM [BuildingBossDb].[dbo].[People] p
WHERE Discriminator = 'Person';

SELECT COUNT(*)
FROM [BuildingBossDb].[dbo].[Flats];

SELECT *
FROM [BuildingBossDb].[dbo].[People] p
WHERE NOT EXISTS(
    SELECT *
    FROM [BuildingBossDb].[dbo].[Flats] f
    WHERE f.OwnerId = p.Id);


update [BuildingBossDb].[dbo].[People]
SET Email = 'pedrobolazo@mail.com'
WHERE ID = '6FF87925-F347-4CC5-E613-08DC6974338B';

SELECT TOP (100) *
FROM  [BuildingBossDb].[dbo].[Requests] r
where r.CategoryId in(
'C1329541-268A-4D4D-5547-08DC699017C3',
'71CA16A8-993C-472A-88E8-08DC6999A5CB',
'207DC9E5-16C7-4CDB-5777-08DC69A2B07B',
'D4B638A3-51A0-4DE0-6C0C-08DC6A25B4CA',
'7B6BFD7E-8F5F-406B-6C0D-08DC6A25B4CA');


delete FROM  [BuildingBossDb].[dbo].[Categories]
where Id in(
'C1329541-268A-4D4D-5547-08DC699017C3',
'71CA16A8-993C-472A-88E8-08DC6999A5CB',
'207DC9E5-16C7-4CDB-5777-08DC69A2B07B',
'D4B638A3-51A0-4DE0-6C0C-08DC6A25B4CA');

update [BuildingBossDb].[dbo].[Requests]
SET CategoryId = '7B6BFD7E-8F5F-406B-6C0D-08DC6A25B4CA'
WHERE CategoryID in(
'C1329541-268A-4D4D-5547-08DC699017C3',
'71CA16A8-993C-472A-88E8-08DC6999A5CB',
'207DC9E5-16C7-4CDB-5777-08DC69A2B07B',
'D4B638A3-51A0-4DE0-6C0C-08DC6A25B4CA',
'7B6BFD7E-8F5F-406B-6C0D-08DC6A25B4CA');

--C1329541-268A-4D4D-5547-08DC699017C3
--71CA16A8-993C-472A-88E8-08DC6999A5CB
--207DC9E5-16C7-4CDB-5777-08DC69A2B07B
--D4B638A3-51A0-4DE0-6C0C-08DC6A25B4CA
--7B6BFD7E-8F5F-406B-6C0D-08DC6A25B4CA


SELECT TOP (100) *
FROM [BuildingBossDb].[dbo].[Flats] f, [BuildingBossDb].[dbo].[People] p
where (p.Email IS NULL OR p.Email = '')
AND p.Id = f.OwnerId;

DELETE FROM [BuildingBossDb].[dbo].[People] 
WHERE Id in(
	SELECT TOP (500) Id
	FROM [BuildingBossDb].[dbo].[People] p
	where (p.Email IS NULL OR p.Email = '')
	AND not exists(
		SELECT TOP (100) *
		FROM [BuildingBossDb].[dbo].[Flats] flats
		WHERE flats.OwnerId = p.Id));

		SELECT TOP (100) *
		FROM [BuildingBossDb].[dbo].[Flats] flats;



DELETE FROM [BuildingBossDb].[dbo].[People] 
WHERE Id in(
	SELECT TOP (500) Id
	FROM [BuildingBossDb].[dbo].[People] p
	where (p.Email IS NULL OR p.Email = '')
	AND not exists(
		SELECT TOP (100) *
		FROM [BuildingBossDb].[dbo].[Flats] flats
		WHERE flats.OwnerId = p.Id));


SELECT TOP (500) Id
FROM [BuildingBossDb].[dbo].[Address] a
where not exists(
	SELECT TOP (100) *
	FROM [BuildingBossDb].[dbo].[Buildings] b
	WHERE a.Id = b.AddressId);

delete from [BuildingBossDb].[dbo].[Address]
where Id in(
'664395C4-DF61-4A02-532F-08DC695103D8',
'FD85ABC0-4DAB-4886-DD56-08DC695A4705');