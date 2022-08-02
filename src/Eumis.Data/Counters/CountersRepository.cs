using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;
using Eumis.Domain.Debts;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Eumis.Data.Counters
{
    internal class CountersRepository : Repository, ICountersRepository
    {
        private const string PROJECT_COUNTER_FORMAT = "projects-for-procedure#{0}";
        private const string PROJECT_NUMBER_FORMAT = "{0}-{1:0000}";

        private const string REQUEST_PACKAGE_COUNTER_FORMAT = "request-package";
        private const string REQUEST_PACKAGE_NUMBER_FORMAT = "{0}";

        private const string EVAL_SESSION_COUNTER_FORMAT = "eval-session-for-procedure#{0}";
        private const string EVAL_SESSION_NUMBER_FORMAT = "{0}-S{1}";

        private const string EVAL_SESSION_DISTRIBUTION_COUNTER_FORMAT = "eval-session-distribution-for-eval-session#{0}";
        private const string EVAL_SESSION_DISTRIBUTION_NUMBER_FORMAT = "{0}-D{1}";

        private const string EVAL_SESSION_REPORT_COUNTER_FORMAT = "eval-session-report-for-eval-session#{0}";
        private const string EVAL_SESSION_REPORT_NUMBER_FORMAT = "{0}-R{1}{2}";

        private const string PROJECT_COMMUNICATION_COUNTER_FORMAT = "communication-for-project#{0}";
        private const string PROJECT_COMMUNICATION_NUMBER_FORMAT = "{0}-M{1:000}";

        private const string PROJECT_MANAGING_AUTHORITY_COMMUNICATION_COUNTER_FORMAT = "managing-authority-communication-for-project#{0}";
        private const string PROJECT_MANAGING_AUTHORITY_COMMUNICATION_NUMBER_FORMAT = "{0}-R{1:000}";

        private const string EVAL_SESSION_STANDING_COUNTER_FORMAT = "eval-session-standing-for-eval-session#{0}";
        private const string EVAL_SESSION_STANDING_NUMBER_FORMAT = "{0}-ST{1}";

        private const string CONTRACT_COMMUNICATION_COUNTER_FORMAT = "communication-for-contract#{0}";
        private const string CONTRACT_COMMUNICATION_NUMBER_FORMAT = "{0}-M{1:000}";

        private const string IRREQULARITY_SIGNAL_COUNTER_FORMAT = "irregularity-signal-for-programme#{0}";

        private const string IRREQULARITY_COUNTER_FORMAT = "irregularity-for-programme#{0}";

        private const string CONTRACT_DEBT_COUNTER_FORMAT = "debt-for-contract#{0}";
        private const string CONTRACT_DEBT_COUNTER_NUMBER_FORMAT = "{0}-Dt{1:00}";

        private const string CORRECTION_DEBT_COUNTER_FORMAT = "correction-debt-for-programme#{0}";
        private const string CORRECTION_DEBT_COUNTER_NUMBER_FORMAT = "{0}-DFKSP{1:0000}";

        private const string ACTUALLY_PAID_AMOUNT_COUNTER_FORMAT = "acrually-paid-amount-for-contract#{0}";
        private const string ACTUALLY_PAID_AMOUNT_NUMBER_FORMAT = "{0}-PA{1:000}";

        private const string COMPENSATION_DOCUMENT_COUNTER_FORMAT = "compensation-document#{0}";
        private const string COMPENSATION_DOCUMENT_NUMBER_FORMAT = "{0}";

        private const string DEBT_REIMBURSED_AMOUNT_COUNTER_FORMAT = "reimbursed-amount-for-contract-debt#{0}";
        private const string DEBT_REIMBURSED_AMOUNT_NUMBER_FORMAT = "{0}-{1:00}";

        private const string CONTRACT_REIMBURSED_AMOUNT_COUNTER_FORMAT = "reimbursed-amount-for-contract#{0}";
        private const string CONTRACT_REIMBURSED_AMOUNT_NUMBER_FORMAT = "{0}-{1:00}";

        private const string CERT_AUTHORITY_CHECK_COUNTER_FORMAT = "cert-authority-check";

        private const string CONTRACT_REPORT_CORRECTION_COUNTER_FORMAT = "contract-report-correction#{0}";
        private const string CONTRACT_REPORT_CORRECTION_NUMBER_FORMAT = "{0}";

        private const string CONTRACT_REPORT_REVALIDATION_COUNTER_FORMAT = "contract-report-revalidation#{0}";
        private const string CONTRACT_REPORT_REVALIDATION_NUMBER_FORMAT = "{0}";

        private const string CONTRACT_REPORT_CERT_CORRECTION_COUNTER_FORMAT = "contract-report-cert-correction#{0}";
        private const string CONTRACT_REPORT_CERT_CORRECTION_NUMBER_FORMAT = "{0}";

        private const string CONTRACT_REPORT_CERT_AUTHORITY_CORRECTION_COUNTER_FORMAT = "contract-report-cert-authority-correction#{0}";
        private const string CONTRACT_REPORT_CERT_AUTHORITY_CORRECTION_NUMBER_FORMAT = "{0}";

        private const string CONTRACT_REPORT_REVALIDATION_CERT_AUTHORITY_CORRECTION_COUNTER_FORMAT = "contract-report-revalidation-cert-authority-correction#{0}";
        private const string CONTRACT_REPORT_REVALIDATION_CERT_AUTHORITY_CORRECTION_NUMBER_FORMAT = "{0}";

        private const string FI_REIMBURSED_AMOUNT_COUNTER_FORMAT = "reimbursed-amount-for-fi#{0}";
        private const string FI_REIMBURSED_AMOUNT_NUMBER_FORMAT = "{0}-{1:00}";

        public CountersRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void CreateProjectCounter(int procedureId)
        {
            this.CreateCounter(string.Format(PROJECT_COUNTER_FORMAT, procedureId));
        }

        public string GetNextProjectNumber(int procedureId)
        {
            var procedureCode =
                this.unitOfWork.DbContext.Set<Procedure>()
                .Where(p => p.ProcedureId == procedureId)
                .Select(p => p.Code)
                .Single();

            int next = this.GetNextNumber(string.Format(PROJECT_COUNTER_FORMAT, procedureId));

            return string.Format(PROJECT_NUMBER_FORMAT, procedureCode, next);
        }

        public void CreateRequestPackageCounter()
        {
            this.CreateCounter(REQUEST_PACKAGE_COUNTER_FORMAT);
        }

        public string GetNextRequestPackageNumber()
        {
            int next = this.GetNextNumber(REQUEST_PACKAGE_COUNTER_FORMAT);

            return string.Format(REQUEST_PACKAGE_NUMBER_FORMAT, next);
        }

        public void CreateEvalSessionCounter(int procedureId)
        {
            this.CreateCounter(string.Format(EVAL_SESSION_COUNTER_FORMAT, procedureId));
        }

        public string GetNextEvalSessionNumber(int procedureId)
        {
            var procedureCode =
                   this.unitOfWork.DbContext.Set<Procedure>()
                   .Where(p => p.ProcedureId == procedureId)
                   .Select(p => p.Code)
                   .Single();

            int next = this.GetNextNumber(string.Format(EVAL_SESSION_COUNTER_FORMAT, procedureId));

            return string.Format(EVAL_SESSION_NUMBER_FORMAT, procedureCode, next);
        }

        public void CreateEvalSessionDistributionCounter(int evalSessionId)
        {
            this.CreateCounter(string.Format(EVAL_SESSION_DISTRIBUTION_COUNTER_FORMAT, evalSessionId));
        }

        public string GetNextEvalSessionDistributionNumber(int evalSessionId)
        {
            var evalSessionCode =
                this.unitOfWork.DbContext.Set<EvalSession>()
                .Where(p => p.EvalSessionId == evalSessionId)
                .Select(p => p.SessionNum)
                .Single();

            int next = this.GetNextNumber(string.Format(EVAL_SESSION_DISTRIBUTION_COUNTER_FORMAT, evalSessionId));

            return string.Format(EVAL_SESSION_DISTRIBUTION_NUMBER_FORMAT, evalSessionCode, next);
        }

        public void CreateEvalSessionReportCounter(int evalSessionId)
        {
            this.CreateCounter(string.Format(EVAL_SESSION_REPORT_COUNTER_FORMAT, evalSessionId));
        }

        public string GetNextEvalSessionReportNumber(int evalSessionId, EvalSessionReportType type)
        {
            var evalSessionCode =
                this.unitOfWork.DbContext.Set<EvalSession>()
                .Where(p => p.EvalSessionId == evalSessionId)
                .Select(p => p.SessionNum)
                .Single();

            int next = this.GetNextNumber(string.Format(EVAL_SESSION_REPORT_COUNTER_FORMAT, evalSessionId));
            string reportTypeCode = null;
            switch (type)
            {
                case EvalSessionReportType.Decision:
                    reportTypeCode = "D";
                    break;
                case EvalSessionReportType.Protocol:
                    reportTypeCode = "P";
                    break;
                case EvalSessionReportType.Report:
                    reportTypeCode = "R";
                    break;
                default:
                    throw new Exception("Invalid report type.");
            }

            return string.Format(EVAL_SESSION_REPORT_NUMBER_FORMAT, evalSessionCode, reportTypeCode, next);
        }

        public void CreateProjectCommunicationCounter(int projectId)
        {
            this.CreateCounter(string.Format(PROJECT_COMMUNICATION_COUNTER_FORMAT, projectId));
        }

        public string GetNextProjectCommunicationNumber(int projectId)
        {
            var projectRegNumber =
                this.unitOfWork.DbContext.Set<Project>()
                .Where(p => p.ProjectId == projectId)
                .Select(p => p.RegNumber)
                .Single();

            int next = this.GetNextNumber(string.Format(PROJECT_COMMUNICATION_COUNTER_FORMAT, projectId));

            return string.Format(PROJECT_COMMUNICATION_NUMBER_FORMAT, projectRegNumber, next);
        }

        public void CreateProjectManagingAuthorityCommunicationCounter(int projectId)
        {
            this.CreateCounter(string.Format(PROJECT_MANAGING_AUTHORITY_COMMUNICATION_COUNTER_FORMAT, projectId));
        }

        public string GetNextProjectManagingAuthorityCommunicationNumber(int projectId)
        {
            var projectRegNumber =
                this.unitOfWork.DbContext.Set<Project>()
                .Where(p => p.ProjectId == projectId)
                .Select(p => p.RegNumber)
                .Single();

            int nextNumber = this.GetNextNumber(string.Format(PROJECT_MANAGING_AUTHORITY_COMMUNICATION_COUNTER_FORMAT, projectId));

            return string.Format(PROJECT_MANAGING_AUTHORITY_COMMUNICATION_NUMBER_FORMAT, projectRegNumber, nextNumber);
        }

        public void CreateEvalSessionStandingCounter(int evalSessionId)
        {
            this.CreateCounter(string.Format(EVAL_SESSION_STANDING_COUNTER_FORMAT, evalSessionId));
        }

        public string GetNextEvalSessionStandingNumber(int evalSessionId)
        {
            var evalSessionCode =
                this.unitOfWork.DbContext.Set<EvalSession>()
                .Where(p => p.EvalSessionId == evalSessionId)
                .Select(p => p.SessionNum)
                .Single();

            int next = this.GetNextNumber(string.Format(EVAL_SESSION_STANDING_COUNTER_FORMAT, evalSessionId));

            return string.Format(EVAL_SESSION_STANDING_NUMBER_FORMAT, evalSessionCode, next);
        }

        public void CreateContractCommunicationCounter(int contractId)
        {
            this.CreateCounter(string.Format(CONTRACT_COMMUNICATION_COUNTER_FORMAT, contractId));
        }

        public string GetNextContractCommunicationNumber(int contractId)
        {
            var contractRegNumber =
                this.unitOfWork.DbContext.Set<Contract>()
                .Where(p => p.ContractId == contractId)
                .Select(p => p.RegNumber)
                .Single();

            int next = this.GetNextNumber(string.Format(CONTRACT_COMMUNICATION_COUNTER_FORMAT, contractId));

            return string.Format(CONTRACT_COMMUNICATION_NUMBER_FORMAT, contractRegNumber, next);
        }

        public void CreateIrregularitySignalCounter(int programmeId)
        {
            this.CreateCounter(string.Format(IRREQULARITY_SIGNAL_COUNTER_FORMAT, programmeId));
        }

        public int GetNextIrregularitySignalNumber(int programmeId)
        {
            return this.GetNextNumber(string.Format(IRREQULARITY_SIGNAL_COUNTER_FORMAT, programmeId));
        }

        public int GetCurrentIrregularitySignalNumber(int programmeId)
        {
            return this.GetCurrentNumber(string.Format(IRREQULARITY_SIGNAL_COUNTER_FORMAT, programmeId));
        }

        public void DecrementCurrentIrregularitySignalNumber(int programmeId)
        {
            this.DecrementCurrentNumber(string.Format(IRREQULARITY_SIGNAL_COUNTER_FORMAT, programmeId));
        }

        public void CreateIrregularityCounter(int programmeId)
        {
            this.CreateCounter(string.Format(IRREQULARITY_COUNTER_FORMAT, programmeId));
        }

        public void CreateContractDebtCounter(int contractId)
        {
            this.CreateCounter(string.Format(CONTRACT_DEBT_COUNTER_FORMAT, contractId));
        }

        public string GetNextContractDebtNumber(int contractId)
        {
            var contractRegNum =
                (from c in this.unitOfWork.DbContext.Set<Contract>()
                 where c.ContractId == contractId
                 select c.RegNumber)
                .Single();

            int next = this.GetNextNumber(string.Format(CONTRACT_DEBT_COUNTER_FORMAT, contractId));

            return string.Format(CONTRACT_DEBT_COUNTER_NUMBER_FORMAT, contractRegNum, next);
        }

        public void CreateCorrectionDebtCounter(int flatFinancialCorrectionId)
        {
            var programmeId =
                (from ffc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>()
                 where ffc.FlatFinancialCorrectionId == flatFinancialCorrectionId
                 select ffc.ProgrammeId)
                .Single();

            this.CreateCounter(string.Format(CORRECTION_DEBT_COUNTER_FORMAT, programmeId));
        }

        public string GetNextCorrectionDebtNumber(int flatFinancialCorrectionId)
        {
            var programmeData =
                (from ffc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>()
                 join p in this.unitOfWork.DbContext.Set<Programme>() on ffc.ProgrammeId equals p.MapNodeId
                 where ffc.FlatFinancialCorrectionId == flatFinancialCorrectionId
                 select new
                 {
                     ProgrammeId = p.MapNodeId,
                     ProgrammeCode = p.Code,
                 })
                .Single();

            int next = this.GetNextNumber(string.Format(CORRECTION_DEBT_COUNTER_FORMAT, programmeData.ProgrammeId));

            return string.Format(CORRECTION_DEBT_COUNTER_NUMBER_FORMAT, programmeData.ProgrammeCode, next);
        }

        public void CreateActuallyPaidAmountCounter(int contractId)
        {
            this.CreateCounter(string.Format(ACTUALLY_PAID_AMOUNT_COUNTER_FORMAT, contractId));
        }

        public string GetNextActuallyPaidAmountNumber(int contractId)
        {
            var contractRegNumber =
                this.unitOfWork.DbContext.Set<Contract>()
                .Where(p => p.ContractId == contractId)
                .Select(p => p.RegNumber)
                .Single();

            int next = this.GetNextNumber(string.Format(ACTUALLY_PAID_AMOUNT_COUNTER_FORMAT, contractId));

            return string.Format(ACTUALLY_PAID_AMOUNT_NUMBER_FORMAT, contractRegNumber, next);
        }

        public string[] GetNextNActuallyPaidAmountNumbers(int contractId, int n)
        {
            var contractRegNumber =
                this.unitOfWork.DbContext.Set<Contract>()
                .Where(p => p.ContractId == contractId)
                .Select(p => p.RegNumber)
                .Single();

            int[] next = this.GetNextNNumbers(string.Format(ACTUALLY_PAID_AMOUNT_COUNTER_FORMAT, contractId), n);

            return next.Select(num => string.Format(ACTUALLY_PAID_AMOUNT_NUMBER_FORMAT, contractRegNumber, num)).ToArray();
        }

        public void CreateCompensationDocumentCounter(int contractId)
        {
            this.CreateCounter(string.Format(COMPENSATION_DOCUMENT_COUNTER_FORMAT, contractId));
        }

        public string GetNextCompensationDocumentNumber(int contractId)
        {
            int next = this.GetNextNumber(string.Format(COMPENSATION_DOCUMENT_COUNTER_FORMAT, contractId));

            return string.Format(COMPENSATION_DOCUMENT_NUMBER_FORMAT, next);
        }

        public void CreateDebtReimbursedAmountCounter(int contractDebtId)
        {
            this.CreateCounter(string.Format(DEBT_REIMBURSED_AMOUNT_COUNTER_FORMAT, contractDebtId));
        }

        public string GetNextDebtReimbursedAmountNumber(int contractDebtId)
        {
            var contractDebtRegNumber =
                this.unitOfWork.DbContext.Set<ContractDebt>()
                .Where(p => p.ContractDebtId == contractDebtId)
                .Select(p => p.RegNumber)
                .Single();

            int next = this.GetNextNumber(string.Format(DEBT_REIMBURSED_AMOUNT_COUNTER_FORMAT, contractDebtId));

            return string.Format(DEBT_REIMBURSED_AMOUNT_NUMBER_FORMAT, contractDebtRegNumber, next);
        }

        public string[] GetNextNDebtReimbursedAmountNumbers(int contractDebtId, int n)
        {
            var contractDebtRegNumber =
                this.unitOfWork.DbContext.Set<ContractDebt>()
                .Where(p => p.ContractDebtId == contractDebtId)
                .Select(p => p.RegNumber)
                .Single();

            int[] next = this.GetNextNNumbers(string.Format(DEBT_REIMBURSED_AMOUNT_COUNTER_FORMAT, contractDebtId), n);

            return next.Select(num => string.Format(DEBT_REIMBURSED_AMOUNT_NUMBER_FORMAT, contractDebtRegNumber, num)).ToArray();
        }

        public void CreateContractReimbursedAmountCounter(int contractId)
        {
            this.CreateCounter(string.Format(CONTRACT_REIMBURSED_AMOUNT_COUNTER_FORMAT, contractId));
        }

        public string GetNextContractReimbursedAmountNumber(int contractId)
        {
            var contractRegNumber =
                this.unitOfWork.DbContext.Set<Contract>()
                .Where(p => p.ContractId == contractId)
                .Select(p => p.RegNumber)
                .Single();

            int next = this.GetNextNumber(string.Format(CONTRACT_REIMBURSED_AMOUNT_COUNTER_FORMAT, contractId));

            return string.Format(CONTRACT_REIMBURSED_AMOUNT_NUMBER_FORMAT, contractRegNumber, next);
        }

        public void CreateCertAuthorityCheckCounter()
        {
            this.CreateCounter(CERT_AUTHORITY_CHECK_COUNTER_FORMAT);
        }

        public int GetNextCertAuthorityCheckNumber()
        {
            return this.GetNextNumber(CERT_AUTHORITY_CHECK_COUNTER_FORMAT);
        }

        public void CreateContractReportCorrectionCounter(int programmeId)
        {
            this.CreateCounter(string.Format(CONTRACT_REPORT_CORRECTION_COUNTER_FORMAT, programmeId));
        }

        public string GetNextContractReportCorrectionNumber(int programmeId)
        {
            int next = this.GetNextNumber(string.Format(CONTRACT_REPORT_CORRECTION_COUNTER_FORMAT, programmeId));

            return string.Format(CONTRACT_REPORT_CORRECTION_NUMBER_FORMAT, next);
        }

        public void CreateContractReportRevalidationCounter(int programmeId)
        {
            this.CreateCounter(string.Format(CONTRACT_REPORT_REVALIDATION_COUNTER_FORMAT, programmeId));
        }

        public string GetNextContractReportRevalidationNumber(int programmeId)
        {
            int next = this.GetNextNumber(string.Format(CONTRACT_REPORT_REVALIDATION_COUNTER_FORMAT, programmeId));

            return string.Format(CONTRACT_REPORT_REVALIDATION_NUMBER_FORMAT, next);
        }

        public void CreateContractReportCertCorrectionCounter(int programmeId)
        {
            this.CreateCounter(string.Format(CONTRACT_REPORT_CERT_CORRECTION_COUNTER_FORMAT, programmeId));
        }

        public string GetNextContractReportCertCorrectionNumber(int programmeId)
        {
            int next = this.GetNextNumber(string.Format(CONTRACT_REPORT_CERT_CORRECTION_COUNTER_FORMAT, programmeId));

            return string.Format(CONTRACT_REPORT_CERT_CORRECTION_NUMBER_FORMAT, next);
        }

        public void CreateContractReportCertAuthorityCorrectionCounter(int programmeId)
        {
            this.CreateCounter(string.Format(CONTRACT_REPORT_CERT_AUTHORITY_CORRECTION_COUNTER_FORMAT, programmeId));
        }

        public string GetNextContractReportCertAuthorityCorrectionNumber(int programmeId)
        {
            int next = this.GetNextNumber(string.Format(CONTRACT_REPORT_CERT_AUTHORITY_CORRECTION_COUNTER_FORMAT, programmeId));

            return string.Format(CONTRACT_REPORT_CERT_AUTHORITY_CORRECTION_NUMBER_FORMAT, next);
        }

        public void CreateContractReportRevalidationCertAuthorityCorrectionCounter(int programmeId)
        {
            this.CreateCounter(string.Format(CONTRACT_REPORT_REVALIDATION_CERT_AUTHORITY_CORRECTION_COUNTER_FORMAT, programmeId));
        }

        public string GetNextContractReportRevalidationCertAuthorityCorrectionNumber(int programmeId)
        {
            int next = this.GetNextNumber(string.Format(CONTRACT_REPORT_REVALIDATION_CERT_AUTHORITY_CORRECTION_COUNTER_FORMAT, programmeId));

            return string.Format(CONTRACT_REPORT_REVALIDATION_CERT_AUTHORITY_CORRECTION_NUMBER_FORMAT, next);
        }

        public void CreateFIReimbursedAmountCounter(int contractId)
        {
            this.CreateCounter(string.Format(FI_REIMBURSED_AMOUNT_COUNTER_FORMAT, contractId));
        }

        public string GetNextFIReimbursedAmountNumber(int contractId)
        {
            var contractRegNumber =
                this.unitOfWork.DbContext.Set<Contract>()
                .Where(p => p.ContractId == contractId)
                .Select(p => p.RegNumber)
                .Single();

            int next = this.GetNextNumber(string.Format(FI_REIMBURSED_AMOUNT_COUNTER_FORMAT, contractId));

            return string.Format(FI_REIMBURSED_AMOUNT_NUMBER_FORMAT, contractRegNumber, next);
        }

        private void CreateCounter(string name, int? startIndex = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Property [Name] of [Counters] cannot be null!");
            }

            var selectSql = @"SELECT [CurrentNumber] FROM [Counters] WHERE Name = @name";
            List<SqlParameter> selectSqlParams = new List<SqlParameter>();
            selectSqlParams.Add(new SqlParameter("@name", name));

            var rowExists = this.SqlQuery<int>(selectSql, selectSqlParams).Any();

            if (!rowExists)
            {
                try
                {
                    var insertSql = @"INSERT INTO [Counters] ([Name], [CurrentNumber]) VALUES (@name, @currentNumber);";

                    this.ExecuteSqlCommand(
                        insertSql,
                        new SqlParameter("@name", name),
                        new SqlParameter("@currentNumber", startIndex.Value));
                }
                catch (SqlException sqlExc)
                {
                    if (sqlExc.Errors.Cast<SqlError>().Any(e => e.Number == 2627 && e.Message.Contains("PK_Counters")))
                    {
                        // swallow the exception
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private int GetNextNumber(string name)
        {
            var updateSelectSql = @"UPDATE [Counters]
                                    SET CurrentNumber = CurrentNumber + 1
                                    OUTPUT Inserted.CurrentNumber
                                    WHERE Name = @name";
            List<SqlParameter> updateSelectSqlParams = new List<SqlParameter>();
            updateSelectSqlParams.Add(new SqlParameter("@name", name));

            var nextNumber = this.SqlQuery<int>(updateSelectSql, updateSelectSqlParams).Single();

            return nextNumber;
        }

        private int GetCurrentNumber(string name)
        {
            var selectSql = @"SELECT [CurrentNumber] FROM [Counters] WHERE Name = @name";
            List<SqlParameter> selectSqlParams = new List<SqlParameter>();
            selectSqlParams.Add(new SqlParameter("@name", name));

            var currentNumber = this.SqlQuery<int>(selectSql, selectSqlParams).Single();

            return currentNumber;
        }

        private void DecrementCurrentNumber(string name)
        {
            var updateSelectSql = @"UPDATE [Counters]
                                    SET CurrentNumber = CurrentNumber - 1
                                    WHERE Name = @name";

            this.ExecuteSqlCommand(updateSelectSql, new SqlParameter("@name", name));
        }

        private int[] GetNextNNumbers(string name, int n)
        {
            var updateSelectSql = @"UPDATE [Counters]
                                    SET CurrentNumber = CurrentNumber + @n
                                    OUTPUT Inserted.CurrentNumber
                                    WHERE Name = @name";
            List<SqlParameter> updateSelectSqlParams = new List<SqlParameter>();
            updateSelectSqlParams.Add(new SqlParameter("@n", n));
            updateSelectSqlParams.Add(new SqlParameter("@name", name));

            var lastNumber = this.SqlQuery<int>(updateSelectSql, updateSelectSqlParams).Single();

            return Enumerable.Range(lastNumber - n + 1, n).ToArray();
        }
    }
}
