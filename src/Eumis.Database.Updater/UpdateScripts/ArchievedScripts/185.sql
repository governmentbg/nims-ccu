update s
  set Name = N'Берайнци'
from ContractReportMicrosSettlements s
  inner join ContractReportMicrosMunicipalities m on s.ContractReportMicrosMunicipalityId = m.ContractReportMicrosMunicipalityId
  inner join ContractReportMicrosDistricts d on m.ContractReportMicrosDistrictId = m.ContractReportMicrosDistrictId
where s.Name = N'Бераинци' and d.Name = N'Перник'

update s
  set Name = N'Вълчовци (кметство Майско)'
from ContractReportMicrosSettlements s
  inner join ContractReportMicrosMunicipalities m on s.ContractReportMicrosMunicipalityId = m.ContractReportMicrosMunicipalityId
  inner join ContractReportMicrosDistricts d on m.ContractReportMicrosDistrictId = m.ContractReportMicrosDistrictId
where s.Name = N'Вълчовци(кметство Майско),' and d.Name = N'Велико_Търново'
