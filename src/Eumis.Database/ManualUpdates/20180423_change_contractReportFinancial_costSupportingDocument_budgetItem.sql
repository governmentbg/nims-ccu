UPDATE [dbo].[ContractReportFinancials]
SET [Xml].modify('
    declare namespace r10043="http://ereg.egov.bg/segment/R-10043";
    declare namespace r10066="http://ereg.egov.bg/segment/R-10066";
    declare namespace r10065="http://ereg.egov.bg/segment/R-10065";
    declare namespace r10000="http://ereg.egov.bg/segment/R-10000";
    replace value of (//FinanceReport/r10043:CostSupportingDocuments/r10043:CostSupportingDocument[@gid="8e47a5bc-65b3-4733-8464-69dc9fc44117"]/r10066:FinanceReportBudgetItemData/r10065:BudgetDetail/r10000:Id/text())[1]
            with "b04b0312-cb67-433d-8010-21b92252ebb6"')
WHERE ContractReportFinancialId = 4801

UPDATE [dbo].[ContractReportFinancials]
SET [Xml].modify('
    declare namespace r10043="http://ereg.egov.bg/segment/R-10043";
    declare namespace r10066="http://ereg.egov.bg/segment/R-10066";
    declare namespace r10065="http://ereg.egov.bg/segment/R-10065";
    declare namespace r10000="http://ereg.egov.bg/segment/R-10000";
    replace value of (//FinanceReport/r10043:CostSupportingDocuments/r10043:CostSupportingDocument[@gid="8e47a5bc-65b3-4733-8464-69dc9fc44117"]/r10066:FinanceReportBudgetItemData/r10065:BudgetDetail/r10000:Name/text())[1]
            with "VI.14.1 Разходи за рекламни материали при провеждане на информационни събития (фонд: ЕСФ, режим на финансиране: Неприложимо, допустим)"')
WHERE ContractReportFinancialId = 4801

UPDATE ContractReportFinancialCSDBudgetItems
SET
    ContractBudgetLevel3AmountId = 7745,
    BudgetDetailGid = 'B04B0312-CB67-433D-8010-21B92252EBB6',
    BudgetDetailName = 'VI.14.1 Разходи за рекламни материали при провеждане на информационни събития (фонд: ЕСФ, режим на финансиране: Неприложимо, допустим)'
WHERE
    ContractReportFinancialCSDBudgetItemId = 110603 AND
    ContractBudgetLevel3AmountId = 7739 AND
    BudgetDetailGid = '94DD9558-2F2D-449F-A653-A9543AE77CA8' AND
    BudgetDetailName = 'III.8.1 Разходи за разработване  и разпространение на печатни информационни материали (фонд: ЕСФ, режим на финансиране: Неприложимо, допустим)'
