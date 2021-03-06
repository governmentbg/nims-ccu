--executed on 2018/02/07 ~ 11:45

update crp
set
PaymentType = 4,
Xml.modify('
    declare namespace R10045="http://ereg.egov.bg/segment/R-10045";
    declare namespace R10049="http://ereg.egov.bg/segment/R-10049";
    declare namespace R09991="http://ereg.egov.bg/segment/R-09991";
    replace value of (//PaymentRequest/R10045:BasicData/R10049:Type/R09991:Value/text())[1] with "final"')
from ContractReportPayments crp
join ContractReports cr on crp.ContractReportId = cr.ContractReportId
join Contracts c on cr.ContractId = c.ContractId
where c.RegNumber = 'BG05M9OP001-1.002-0022-C03'
and cr.OrderNum = 4
and crp.Status = 1
and crp.VersionNum = 4
and crp.VersionSubNum = 4
and crp.PaymentType = 3
and Xml.value('
    declare namespace R10045="http://ereg.egov.bg/segment/R-10045";
    declare namespace R10049="http://ereg.egov.bg/segment/R-10049";
    declare namespace R09991="http://ereg.egov.bg/segment/R-09991";
    (//PaymentRequest/R10045:BasicData/R10049:Type/R09991:Value)[1]', 'nvarchar(max)') = 'intermediate'

update crp
set
Xml.modify('
    declare namespace R10045="http://ereg.egov.bg/segment/R-10045";
    declare namespace R10049="http://ereg.egov.bg/segment/R-10049";
    declare namespace R09991="http://ereg.egov.bg/segment/R-09991";
    replace value of (//PaymentRequest/R10045:BasicData/R10049:Type/R09991:Description/text())[1] with "Окончателно"')
from ContractReportPayments crp
join ContractReports cr on crp.ContractReportId = cr.ContractReportId
join Contracts c on cr.ContractId = c.ContractId
where c.RegNumber = 'BG05M9OP001-1.002-0022-C03'
and cr.OrderNum = 4
and crp.Status = 1
and crp.VersionNum = 4
and crp.VersionSubNum = 4
and Xml.value('
    declare namespace R10045="http://ereg.egov.bg/segment/R-10045";
    declare namespace R10049="http://ereg.egov.bg/segment/R-10049";
    declare namespace R09991="http://ereg.egov.bg/segment/R-09991";
    (//PaymentRequest/R10045:BasicData/R10049:Type/R09991:Description)[1]', 'nvarchar(max)') = 'Междинно'

GO
