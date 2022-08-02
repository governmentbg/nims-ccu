-- append the nuts code to the name
update ProtectedZones set Name = Name + ' (' + NutsCode + ')'
update Nuts1s set Name = Name + ' (' + NutsCode + ')'
update Nuts2s set Name = Name + ' (' + NutsCode + ')'
update Districts set Name = Name + ' (' + NutsCode + ')'

GO

-- rebuild the FullPathName of the nuts nomenclature
update pz
  set FullPathName = c.Name + ', ' + pz.Name
from ProtectedZones pz
  inner join Countries c on pz.CountryId = c.CountryId

-- handle exception
update ProtectedZones
  set FullPathName = N'България, Всички защитени зони (BG0000000)'
where ProtectedZoneId = 0

update n1
  set FullPathName = c.Name + ', ' + n1.Name
from Nuts1s n1
  inner join Countries c on n1.CountryId = c.CountryId

update n2
  set FullPathName = c.Name + ', ' + n1.Name + ', ' + n2.Name
from Nuts2s n2
  inner join Nuts1s n1 on n2.Nuts1Id = n1.Nuts1Id
  inner join Countries c on n1.CountryId = c.CountryId

update d
  set FullPathName = c.Name + ', ' + n1.Name + ', ' + n2.Name + ', ' + d.Name
from Districts d
  inner join Nuts2s n2 on d.Nuts2Id = n2.Nuts2Id
  inner join Nuts1s n1 on n2.Nuts1Id = n1.Nuts1Id
  inner join Countries c on n1.CountryId = c.CountryId

update m
  set FullPathName = c.Name + ', ' + n1.Name + ', ' + n2.Name + ', ' + d.Name + ', ' + m.Name
from Municipalities m
  inner join Districts d on m.DistrictId = d.DistrictId
  inner join Nuts2s n2 on d.Nuts2Id = n2.Nuts2Id
  inner join Nuts1s n1 on n2.Nuts1Id = n1.Nuts1Id
  inner join Countries c on n1.CountryId = c.CountryId

update s
  set FullPathName = c.Name + ', ' + n1.Name + ', ' + n2.Name + ', ' + d.Name + ', ' + m.Name + ', ' + s.Name
from Settlements s
  inner join Municipalities m on s.MunicipalityId = m.MunicipalityId
  inner join Districts d on m.DistrictId = d.DistrictId
  inner join Nuts2s n2 on d.Nuts2Id = n2.Nuts2Id
  inner join Nuts1s n1 on n2.Nuts1Id = n1.Nuts1Id
  inner join Countries c on n1.CountryId = c.CountryId

GO

-- Add extra region NUTS 1
SET IDENTITY_INSERT [Nuts1s] ON
GO

INSERT [dbo].[Nuts1s] ([Nuts1Id], [CountryId], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (3, 23, N'BGZ', N'Extra-Regio NUTS 1 (BGZ)', N'България, Extra-Regio NUTS 1 (BGZ)', N'BG, BGZ')
GO

SET IDENTITY_INSERT [Nuts1s] OFF
GO


-- Add extra region NUTS 2
SET IDENTITY_INSERT [Nuts2s] ON
GO

INSERT [dbo].[Nuts2s] ([Nuts2Id], [Nuts1Id], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (7, 3, N'BGZZ', N'Extra-Regio NUTS 2 (BGZZ)', N'България, Extra-Regio NUTS 1 (BGZ), Extra-Regio NUTS 2 (BGZZ)', N'BG, BGZ, BGZZ')
GO

SET IDENTITY_INSERT [Nuts2s] OFF
GO

-- Add extra region NUTS 3
SET IDENTITY_INSERT [Districts] ON
GO

INSERT [dbo].[Districts] ([DistrictId], [Nuts2Id], [NutsCode], [Name], [FullPathName], [FullPath]) VALUES (29, 7, N'BGZZZ', N'Extra-Regio NUTS 3 (BGZZZ)', N'България, Extra-Regio NUTS 1 (BGZ), Extra-Regio NUTS 2 (BGZZ), Extra-Regio NUTS 3 (BGZZZ)', N'BG, BGZ, BGZZ, BGZZZ')
GO

SET IDENTITY_INSERT [Districts] OFF
GO

-- PORTAL UPDATE SCRIPT ENDS HERE !!!!

-- update ContractBudgetLevel3Amounts
with nuts as
(
  select FullPath, FullPathName from ProtectedZones
  union all
  select FullPath, FullPathName from Nuts1s
  union all
  select FullPath, FullPathName from Nuts2s
  union all
  select FullPath, FullPathName from Districts
  union all
  select FullPath, FullPathName from Municipalities
  union all
  select FullPath, FullPathName from Settlements
)
update cbl
  set NutsFullPathName = nuts.FullPathName
from ContractBudgetLevel3Amounts cbl
  inner join nuts on cbl.NutsFullPath = nuts.FullPath

GO

-- update ContractLocations
with nuts as
(
  select FullPath, FullPathName from ProtectedZones
  union all
  select FullPath, FullPathName from Nuts1s
  union all
  select FullPath, FullPathName from Nuts2s
  union all
  select FullPath, FullPathName from Districts
  union all
  select FullPath, FullPathName from Municipalities
  union all
  select FullPath, FullPathName from Settlements
)
update cl
  set FullPathName = nuts.FullPathName
from ContractLocations cl
  inner join nuts on cl.FullPath = nuts.FullPath

GO

-- update ContractVersionXmlAmounts
with nuts as
(
  select FullPath, FullPathName from ProtectedZones
  union all
  select FullPath, FullPathName from Nuts1s
  union all
  select FullPath, FullPathName from Nuts2s
  union all
  select FullPath, FullPathName from Districts
  union all
  select FullPath, FullPathName from Municipalities
  union all
  select FullPath, FullPathName from Settlements
)
update cvx
  set NutsFullPathName = nuts.FullPathName
from ContractVersionXmlAmounts cvx
  inner join nuts on cvx.NutsFullPath = nuts.FullPath

GO

-- update Projects
with
  nuts as
  (
    select FullPath, FullPathName from ProtectedZones
    union all
    select FullPath, FullPathName from Nuts1s
    union all
    select FullPath, FullPathName from Nuts2s
    union all
    select FullPath, FullPathName from Districts
    union all
    select FullPath, FullPathName from Municipalities
    union all
    select FullPath, FullPathName from Settlements
  ),
  projectsNutsPathSplit as
  (
    select p.ProjectId, LTRIM(cs.value) as FullPath, cs.OrderNum
    from Projects p
      cross apply (
        select
          ROW_NUMBER() OVER (order by (select 1)) as OrderNum,
          value
        from STRING_SPLIT (NutsAddressFullPath, ';')
      ) cs
    where LTRIM(cs.value) <> ''
  ),
  projectsNutsPathSplitName as
  (
    select p.ProjectId, p.FullPath, p.OrderNum,(nuts.FullPathName + N'; ') as FullPathName
    from projectsNutsPathSplit p
      inner join nuts on p.FullPath = nuts.FullPath
  )
update p
  set NutsAddressFullPathName = (
    select pn.FullPathName
    from projectsNutsPathSplitName pn
    where pn.ProjectId = p.ProjectId
    order by pn.OrderNum
    for xml path(''), type
  ).value('.','NVARCHAR(MAX)')
from
  Projects p
where
  EXISTS(select NULL from projectsNutsPathSplitName pn where pn.ProjectId = p.ProjectId)

GO
