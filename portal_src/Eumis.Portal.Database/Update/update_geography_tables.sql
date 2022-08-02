-- update ProtectedZones
select * from ProtectedZones
update ProtectedZones set FullPathName = 
(
	select c.Name + ', ' + r.Name from Countries c
	inner join ProtectedZones r on c.CountryId = r.CountryId
	where ProtectedZones.ProtectedZoneId = r.ProtectedZoneId
)

update ProtectedZones set FullPath = 
(
	select c.NutsCode + ', ' + r.NutsCode from Countries c
	inner join ProtectedZones r on c.CountryId = r.CountryId
	where ProtectedZones.ProtectedZoneId = r.ProtectedZoneId
)

-- update Nuts1s
select * from Nuts1s
update Nuts1s set FullPathName = 
(
	select c.Name + ', ' + n1.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	where Nuts1s.Nuts1Id = n1.Nuts1Id
)

update Nuts1s set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	where Nuts1s.Nuts1Id = n1.Nuts1Id
)

-- update Nuts2s
select * from Nuts2s
update Nuts2s set FullPathName = 
(
	select c.Name + ', ' + n1.Name + ', ' + n2.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	where Nuts2s.Nuts2Id = n2.Nuts2Id
)

update Nuts2s set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode + ', ' + n2.NutsCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	where Nuts2s.Nuts2Id = n2.Nuts2Id
)

-- update Districts
select * from Districts
update Districts set FullPathName = 
(
	select c.Name + ', ' + n1.Name + ', ' + n2.Name + ', ' + d.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	where Districts.DistrictId = d.DistrictId
)

update Districts set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode + ', ' + n2.NutsCode + ', ' + d.NutsCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	where Districts.DistrictId = d.DistrictId
)

-- update Municipalities
select * from Municipalities
update Municipalities set FullPathName = 
(
	select c.Name + ', ' + n1.Name + ', ' + n2.Name + ', ' + d.Name + ', ' + m.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	inner join Municipalities m on m.DistrictId = d.DistrictId
	where Municipalities.MunicipalityId = m.MunicipalityId
)

update Municipalities set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode + ', ' + n2.NutsCode + ', ' + d.NutsCode + ', ' + m.LauCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	inner join Municipalities m on m.DistrictId = d.DistrictId
	where Municipalities.MunicipalityId = m.MunicipalityId
)

-- update Settlements
select * from Settlements
update Settlements set FullPathName = 
(
	select c.Name + ', ' + n1.Name + ', ' + n2.Name + ', ' + d.Name + ', ' + m.Name + ', ' + s.Name from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	inner join Municipalities m on m.DistrictId = d.DistrictId
	inner join Settlements s on s.MunicipalityId = m.MunicipalityId
	where Settlements.SettlementId = s.SettlementId
)

update Settlements set FullPath = 
(
	select c.NutsCode + ', ' + n1.NutsCode + ', ' + n2.NutsCode + ', ' + d.NutsCode + ', ' + m.LauCode + ', ' + s.LauCode from Countries c
	inner join Nuts1s n1 on c.CountryId = n1.CountryId
	inner join Nuts2s n2 on n2.Nuts1Id = n1.Nuts1Id
	inner join Districts d on d.Nuts2Id = n2.Nuts2Id
	inner join Municipalities m on m.DistrictId = d.DistrictId
	inner join Settlements s on s.MunicipalityId = m.MunicipalityId
	where Settlements.SettlementId = s.SettlementId
)