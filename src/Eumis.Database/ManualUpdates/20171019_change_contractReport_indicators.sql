--executed on 2017/10/19 ~ 15:30

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[1]/ii:LastReportCumulativeAmountTotal/text())[1] with "17122"')
where ContractReportTechnicalId in (1044, 638)
GO

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[1]/ii:CumulativeAmountTotal/text())[1] with "17122.00"')
where ContractReportTechnicalId in (1044, 638)
GO

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[1]/ii:ResidueAmountTotal/text())[1] with "-5122.00"')
where ContractReportTechnicalId in (1044, 638)
GO

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[2]/ii:LastReportCumulativeAmountTotal/text())[1] with "17122"')
where ContractReportTechnicalId in (1044, 638)
GO

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[2]/ii:CumulativeAmountTotal/text())[1] with "17122.00"')
where ContractReportTechnicalId in (1044, 638)
GO

update ContractReportTechnicals
set Xml.modify('
  declare namespace i="http://ereg.egov.bg/segment/R-10044";
  declare namespace ii="http://ereg.egov.bg/segment/R-10054";
  replace value of (//TechnicalReport/i:Indicators/i:Indicator[2]/ii:ResidueAmountTotal/text())[1] with "-7122.00"')
where
  ContractReportTechnicalId in (1044, 638)
GO


update ContractReportIndicators
set
  LastReportCumulativeAmountTotal = 17122,
  CumulativeAmountTotal = 17122,
  ResidueAmountTotal = -5122,
  ApprovedCumulativeAmountTotal = 17122,
  ApprovedResidueAmountTotal = -5122
where
  ContractReportIndicatorId = 1292

update ContractReportIndicators
set
  LastReportCumulativeAmountTotal = 17122,
  CumulativeAmountTotal = 17122,
  ResidueAmountTotal = -7122,
  ApprovedCumulativeAmountTotal = 17122,
  ApprovedResidueAmountTotal = -7122
where
  ContractReportIndicatorId = 1293

update ContractReportIndicators
set
  LastReportCumulativeAmountTotal = 17122,
  CumulativeAmountTotal = 17122,
  ResidueAmountTotal = -5122
where
  ContractReportIndicatorId = 668

update ContractReportIndicators
set
  LastReportCumulativeAmountTotal = 17122,
  CumulativeAmountTotal = 17122,
  ResidueAmountTotal = -7122
where
  ContractReportIndicatorId = 669

update ContractReportIndicators
set
  ApprovedPeriodAmountTotal = 881,
  ApprovedCumulativeAmountTotal = 17122,
  ApprovedResidueAmountTotal = -5122
where
  ContractReportIndicatorId = 429

update ContractReportIndicators
set
  ApprovedPeriodAmountTotal = 881,
  ApprovedCumulativeAmountTotal = 17122,
  ApprovedResidueAmountTotal = -7122
where
  ContractReportIndicatorId = 430
