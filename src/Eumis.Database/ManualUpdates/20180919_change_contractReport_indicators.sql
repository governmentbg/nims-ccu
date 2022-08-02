--executed on 2018/09/19 ~ 15:00

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[4]/ii:BFPContractIndicator/ii:TargetTotal/text())[1] with "854.78"')
where ContractReportTechnicalId = 27842
GO

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[4]/ii:ResidueAmountTotal/text())[1] with "854.78"')
where ContractReportTechnicalId = 27842
GO

update ContractReportIndicators
set
  ResidueAmountTotal = 854.78
where
  ContractReportIndicatorId = 45073

update ContractReportIndicators
set
  ResidueAmountTotal = 854.78
where
  ContractReportIndicatorId = 53826