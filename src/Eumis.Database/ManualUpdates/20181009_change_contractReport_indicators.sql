--executed on 2018/10/09 ~ 17:30

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  declare namespace iii="http://ereg.egov.bg/segment/R-10038";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[4]/ii:BFPContractIndicator/iii:TargetTotal/text())[1] with "854.78"')
where ContractReportTechnicalId = 29584
GO

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[4]/ii:PeriodAmountTotal/text())[1] with "854.78"')
where ContractReportTechnicalId = 29584
GO

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[4]/ii:CumulativeAmountTotal/text())[1] with "854.78"')
where ContractReportTechnicalId = 29584
GO
