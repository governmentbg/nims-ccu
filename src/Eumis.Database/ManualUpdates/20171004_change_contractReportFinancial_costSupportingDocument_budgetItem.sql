UPDATE [dbo].[ContractReportFinancials]
SET [Xml].modify('
    declare namespace r10043="http://ereg.egov.bg/segment/R-10043";
    declare namespace r10066="http://ereg.egov.bg/segment/R-10066";
    declare namespace r10065="http://ereg.egov.bg/segment/R-10065";
    declare namespace r10000="http://ereg.egov.bg/segment/R-10000"
    replace value of (//FinanceReport/r10043:CostSupportingDocuments/r10043:CostSupportingDocument[@gid="419e253d-8171-4b90-83e4-76bb1475c921"]/r10066:FinanceReportBudgetItemData/r10065:BudgetDetail/r10000:Id/text())[1]
            with "e4fffb0e-6100-451b-a2b2-3e62ab777345"')
WHERE ContractReportFinancialId = 9573

UPDATE [dbo].[ContractReportFinancials]
SET [Xml].modify('
    declare namespace r10043="http://ereg.egov.bg/segment/R-10043";
    declare namespace r10066="http://ereg.egov.bg/segment/R-10066";
    declare namespace r10065="http://ereg.egov.bg/segment/R-10065";
    declare namespace r10000="http://ereg.egov.bg/segment/R-10000"
    replace value of (//FinanceReport/r10043:CostSupportingDocuments/r10043:CostSupportingDocument[@gid="419e253d-8171-4b90-83e4-76bb1475c921"]/r10066:FinanceReportBudgetItemData/r10065:BudgetDetail/r10000:Name/text())[1]
            with "V.16.1 Разходи за организация и управление (фонд: ЕСФ, режим на финансиране: Неприложимо, допустим)"')
WHERE ContractReportFinancialId = 9573

UPDATE ContractReportFinancialCSDBudgetItems
SET
    ContractBudgetLevel3AmountId = 720,
    BudgetDetailGid = 'E4FFFB0E-6100-451B-A2B2-3E62AB777345',
    BudgetDetailName = 'V.16.1 Разходи за организация и управление (фонд: ЕСФ, режим на финансиране: Неприложимо, допустим)'
WHERE
    ContractReportFinancialCSDBudgetItemId = 239535 AND
    ContractBudgetLevel3AmountId = 721 AND
    BudgetDetailGid = 'D4D7A88F-7B3E-416B-98C6-221B4172152B' AND
    BudgetDetailName = 'V.16.2 Невъзстановим ДДС за разходите по раздел V (фонд: ЕСФ, режим на финансиране: Неприложимо, допустим)'
