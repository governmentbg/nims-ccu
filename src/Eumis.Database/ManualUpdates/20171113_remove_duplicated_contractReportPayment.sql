--executed on 2017/11/13 ~ 12:00

update ContractReportPayments
set
    VersionNum = 1,
    VersionSubNum = 1,
    Xml.modify('
      declare namespace i="http://ereg.egov.bg/segment/R-10044";
      declare namespace ii="http://ereg.egov.bg/segment/R-10054";
      replace value of (//PaymentRequest/@docNumber)[1] with "1"')
where
    ContractReportPaymentId = 13298
    and VersionNum = 2
    and VersionSubNum = 2
    and Xml.value('(//PaymentRequest/@docNumber)[1]', 'int') = 2

delete ContractReportPayments
where
    ContractReportPaymentId = 13297
    and ContractReportId = 11478
    and ContractId = 1012
    and Gid = '477FA639-A09D-47D6-BC12-955151674E92'
    --and CONVERT(VARCHAR(MAX), Xml) = '<PaymentRequest xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" id="7a96a297-23b1-453d-8bfd-2f79ed338149" contractGid="c6f1138a-6942-43ba-b160-d57b099b15b7" packageGid="9c91d77e-4a74-4c9d-bfe2-8e16a0f145d1" contractNumber="BG16RFOP001-7.001-0010-C02" docNumber="1" createDate="2017-08-29T15:41:57.7914459+03:00" modificationDate="2017-08-29T15:46:07.2848404+03:00"><BasicData xmlns="http://ereg.egov.bg/segment/R-10045" isLocked="false"><StartDate xmlns="http://ereg.egov.bg/segment/R-10049">2016-05-09</StartDate><EndDate xmlns="http://ereg.egov.bg/segment/R-10049">2019-05-09</EndDate><Type xmlns="http://ereg.egov.bg/segment/R-10049"><Value xmlns="http://ereg.egov.bg/segment/R-09991">advance</Value><Description xmlns="http://ereg.egov.bg/segment/R-09991">Авансово</Description></Type><FinanceReportAmount xmlns="http://ereg.egov.bg/segment/R-10049">0</FinanceReportAmount><AdditionalIncome xmlns="http://ereg.egov.bg/segment/R-10049">0.00</AdditionalIncome><TotalAmount xmlns="http://ereg.egov.bg/segment/R-10049">4433448.58</TotalAmount><FinanceReportAmountWithoutIncome xmlns="http://ereg.egov.bg/segment/R-10049">4433448.58</FinanceReportAmountWithoutIncome><BeneficiaryRegistrationVAT xmlns="http://ereg.egov.bg/segment/R-10049"><Value xmlns="http://ereg.egov.bg/segment/R-09991">yes</Value><Description xmlns="http://ereg.egov.bg/segment/R-09991">Да</Description></BeneficiaryRegistrationVAT><BankAccount xmlns="http://ereg.egov.bg/segment/R-10049">BG57BNBG96613200188001</BankAccount></BasicData><AttachedDocuments xmlns="http://ereg.egov.bg/segment/R-10045" id="66b28055-2a13-4e66-bb7d-b3346382b161" /></PaymentRequest>'
    and Hash = 'E517A4A206'
    and VersionNum = 1
    and VersionSubNum = 1
    and Status = 1
    and StatusNote IS NULL
    and PaymentType = 1
    and RegDate = '2017-08-29 15:41:58.6440384'
    and OtherRegistration IS NULL
    and SubmitDate IS NULL
    and SubmitDeadline IS NULL
    and DateFrom = '2016-05-09 00:00:00.0000000'
    and DateTo = '2019-05-09 00:00:00.0000000'
    and AdditionalIncome = 0.00
    and TotalAmount = 4433448.58
    and RequestedAmount = 4433448.58
    and CreateDate = '2017-08-29 15:41:58.6440384'
    and ModifyDate = '2017-08-29 15:46:09.3785308'
    and Version = 0x000000000138CFE6

GO
