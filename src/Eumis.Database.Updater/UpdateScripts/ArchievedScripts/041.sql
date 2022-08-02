GO

UPDATE [dbo].[Districts]
SET
    Name = N'София-Област',
    FullPathName =  N'България, Югозападна и южно-централна България, Югозападен, София-Област'
WHERE DistrictId = 23

UPDATE m
SET
    FullPathName = d.FullPathName + N', ' + m.Name
FROM Municipalities m
INNER JOIN Districts d ON m.DistrictId = d.DistrictId
WHERE d.DistrictId = 23

GO
