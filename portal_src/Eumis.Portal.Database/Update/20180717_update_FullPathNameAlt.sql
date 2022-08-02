-- append the nuts code to the name
update ProtectedZones set NameAlt = NameAlt + ' (' + NutsCode + ')'
update Nuts1s set NameAlt = NameAlt + ' (' + NutsCode + ')'
update Nuts2s set NameAlt = NameAlt + ' (' + NutsCode + ')'
update Districts set NameAlt = NameAlt + ' (' + NutsCode + ')'
GO

-- rebuild the FullPathName of the nuts nomenclature
update pz
  set FullPathNameAlt = c.NameAlt + ', ' + pz.NameAlt
from ProtectedZones pz
  inner join Countries c on pz.CountryId = c.CountryId

-- handle exception
update ProtectedZones
  set FullPathNameAlt = N'Bulgaria, All protected zones (BG0000000)'
where ProtectedZoneId = 0

update n1
  set FullPathNameAlt = c.NameAlt + ', ' + n1.NameAlt
from Nuts1s n1
  inner join Countries c on n1.CountryId = c.CountryId

-- handle exception
update Nuts1s
  set FullPathNameAlt = N'Bulgaria, Extra-Regio NUTS 1 (BGZ)'
where Nuts1Id = 3

update n2
  set FullPathNameAlt = c.NameAlt + ', ' + n1.NameAlt + ', ' + n2.NameAlt
from Nuts2s n2
  inner join Nuts1s n1 on n2.Nuts1Id = n1.Nuts1Id
  inner join Countries c on n1.CountryId = c.CountryId

-- handle exception
update Nuts2s
  set FullPathNameAlt = N'Bulgaria, Extra-Regio NUTS 1 (BGZ), Extra-Regio NUTS 2 (BGZZ)'
where Nuts2Id = 7

update d
  set FullPathNameAlt = c.NameAlt + ', ' + n1.NameAlt + ', ' + n2.NameAlt + ', ' + d.NameAlt
from Districts d
  inner join Nuts2s n2 on d.Nuts2Id = n2.Nuts2Id
  inner join Nuts1s n1 on n2.Nuts1Id = n1.Nuts1Id
  inner join Countries c on n1.CountryId = c.CountryId

-- handle exception
update Districts
  set FullPathNameAlt = N'Bulgaria, Extra-Regio NUTS 1 (BGZ), Extra-Regio NUTS 2 (BGZZ), Extra-Regio NUTS 3 (BGZZZ)'
where DistrictId = 29

update m
  set FullPathNameAlt = c.NameAlt + ', ' + n1.NameAlt + ', ' + n2.NameAlt + ', ' + d.NameAlt + ', ' + m.NameAlt
from Municipalities m
  inner join Districts d on m.DistrictId = d.DistrictId
  inner join Nuts2s n2 on d.Nuts2Id = n2.Nuts2Id
  inner join Nuts1s n1 on n2.Nuts1Id = n1.Nuts1Id
  inner join Countries c on n1.CountryId = c.CountryId

update s
  set FullPathNameAlt = c.NameAlt + ', ' + n1.NameAlt + ', ' + n2.NameAlt + ', ' + d.NameAlt + ', ' + m.NameAlt + ', ' + s.NameAlt
from Settlements s
  inner join Municipalities m on s.MunicipalityId = m.MunicipalityId
  inner join Districts d on m.DistrictId = d.DistrictId
  inner join Nuts2s n2 on d.Nuts2Id = n2.Nuts2Id
  inner join Nuts1s n1 on n2.Nuts1Id = n1.Nuts1Id
  inner join Countries c on n1.CountryId = c.CountryId
GO
