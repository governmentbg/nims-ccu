using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Eumis.Portal.Web.Helpers;
using Eumis.Portal.Web.Helpers.Attributes;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Eumis.Components;
using Eumis.Components.Communicators;

namespace Eumis.Portal.Web.Areas.Private.Controllers
{
    public class InitializerController : ApiController
    {
        private IDocumentSerializer _documentSerializer;
        private IProcedureCommunicator _procedureCommunicator;
        private ICompaniesCommunicator _companiesCommunicator;

        public InitializerController(IProcedureCommunicator procedureCommunicator)
        {
            _documentSerializer = new DocumentSerializer();
            this._procedureCommunicator = procedureCommunicator;
            _companiesCommunicator = new CompaniesCommunicator();
        }

        private PrivateSignInManager SignInManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<PrivateSignInManager>();
            }
        }

        [HttpPost]
        public object ProjectNew(ContractInitializer document)
        {
            if (document == null || !document.gid.HasValue)
                throw new Exception("gid is null");

            ContractProcedure procedure = _procedureCommunicator.GetProcedureAppData(document.gid.Value);

            R_10019.Project project = R_10019.Project.Load(procedure, null, document.gid.Value);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(project) };
        }

        [HttpPost]
        public object ProjectFromMessage(ContractInitializer document)
        {
            if (document == null || String.IsNullOrWhiteSpace(document.xml))
                throw new Exception("xml is null");

            R_10020.Message message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(document.xml);

            R_10019.Project project = message.Project;

            R_10019.Project.LockUnlockAllSections(project, false);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(project) };
        }

        [HttpPost]
        public object MessageQuestion(ContractInitializer document)
        {
            if (document == null || String.IsNullOrWhiteSpace(document.xml))
                throw new Exception("xml is null");

            R_10019.Project project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(document.xml);
            R_10019.Project.LockUnlockAllSections(project, true);

            R_10020.Message message = new R_10020.Message();
            message.Project = project;
            R_10020.Message.InitQuestion(message);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(message) };
        }

        [HttpPost]
        public object MessageAnswer(ContractInitializer document)
        {
            if (document == null || String.IsNullOrWhiteSpace(document.xml))
            {
                throw new Exception("xml is null");
            }

            R_10020.Message message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(document.xml);

            R_10020.Message.InitAnswer(message);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(message) };
        }

        [HttpPost]
        public object ManagingAuthorityCommunicationQuestion()
        {
            R_10020.Message message = new R_10020.Message();
            R_10020.Message.InitQuestion(message);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(message) };
        }

        [HttpPost]
        public object ManagingAuthorityCommunicationQuestionTemplate(ContractProjectMassCommunicationInitializer massCommunication)
        {
            R_10020.Message message = new R_10020.Message();

            message = R_10020.Message.InitQuestion(message, massCommunication);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(message) };
        }

        [HttpPost]
        public object ManagingAuthorityCommunicationAnswer(ContractInitializer document)
        {
            if (document == null || String.IsNullOrWhiteSpace(document.xml))
            {
                throw new Exception("xml is null");
            }

            R_10020.Message message = _documentSerializer.XmlDeserializeFromString<R_10020.Message>(document.xml);

            message.type = R_09990.MessageTypeNomenclature.Answer;
            if (message.ReplyAttachedDocumentCollection == null)
            {
                message.ReplyAttachedDocumentCollection = new R_10020.AttachedDocumentCollection();
            }

            return new { xml = _documentSerializer.XmlSerializeObjectToString(message) };
        }

        [HttpPost]
        public object EvalTableRejection()
        {
            R_10023.EvalTable table = new R_10023.EvalTable();
            table.type = R_09993.EvalTypeNomenclature.Rejection;
            R_10023.EvalTable.Load(table);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(table) };
        }

        [HttpPost]
        public object EvalTableWeight()
        {
            R_10023.EvalTable table = new R_10023.EvalTable();
            table.type = R_09993.EvalTypeNomenclature.Weight;
            R_10023.EvalTable.Load(table);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(table) };
        }

        [HttpPost]
        public object CopyEvalTableRejection(ContractInitializer document)
        {
            R_10023.EvalTable evalTable = new R_10023.EvalTable();
            evalTable.type = R_09993.EvalTypeNomenclature.Rejection;
            R_10023.EvalTable.Load(evalTable);

            R_10023.EvalTable oldEvalTable = _documentSerializer.XmlDeserializeFromString<R_10023.EvalTable>(document.xml);

            evalTable.Copy(oldEvalTable);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(evalTable) };
        }

        [HttpPost]
        public object CopyEvalTableWeight(ContractInitializer document)
        {
            R_10023.EvalTable evalTable = new R_10023.EvalTable();
            evalTable.type = R_09993.EvalTypeNomenclature.Weight;
            R_10023.EvalTable.Load(evalTable);

            R_10023.EvalTable oldEvalTable = _documentSerializer.XmlDeserializeFromString<R_10023.EvalTable>(document.xml);

            evalTable.Copy(oldEvalTable);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(evalTable) };
        }

        [HttpPost]
        public object EvalSheet(ContractInitializer document)
        {
            if (document == null || String.IsNullOrWhiteSpace(document.xml))
                throw new Exception("xml is null");

            R_10023.EvalTable table = _documentSerializer.XmlDeserializeFromString<R_10023.EvalTable>(document.xml);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(R_10026.EvalSheet.Init(table)) };
        }

        [HttpPost]
        public object Standpoint(ContractInitializer document)
        {
            R_10027.Standpoint standpoint = null;

            standpoint = R_10027.Standpoint.Init(standpoint);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(standpoint) };
        }

        [HttpPost]
        public object BFPContract(ContractInitializerBFPContract document)
        {
            if (document == null
                || String.IsNullOrWhiteSpace(document.xml)
                || String.IsNullOrWhiteSpace(document.projectRegNumber)
                || String.IsNullOrWhiteSpace(document.contractRegNumber)
                || String.IsNullOrWhiteSpace(document.contractGid))
                throw new Exception("xml or projectRegNumber or contractRegNumber or contractGid is null");

            R_10019.Project project = _documentSerializer.XmlDeserializeFromString<R_10019.Project>(document.xml);

            R_10040.BFPContract contract = R_10040.BFPContract.Init(project, document.programmeCode);

            contract.projectRegNumber = document.projectRegNumber;
            contract.contractRegNumber = document.contractRegNumber;

            contract.contractGid = document.contractGid;

            return new { xml = _documentSerializer.XmlSerializeObjectToString(contract) };
        }

        [HttpPost]
        public object LoadBFPContract(ContractInitializerBFPContract document)
        {
            if (document == null
                || String.IsNullOrWhiteSpace(document.xml))
                throw new Exception("xml  is null");

            R_10040.BFPContract contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(document.xml);

            contract = R_10040.BFPContract.Load(contract);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(contract) };
        }

        [HttpPost]
        public object Procurements(ContractInitializerReport document)
        {
            if (document == null || document.gid == null)
                throw new Exception("gid is null");

            R_10041.Procurements lastProcurements = null;
            if (!String.IsNullOrWhiteSpace(document.xml))
            {
                lastProcurements = _documentSerializer.XmlDeserializeFromString<R_10041.Procurements>(document.xml);
            }
            R_10040.BFPContract contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(document.contractVersionXml);

            R_10041.Procurements procurements = R_10041.Procurements.Init(contract, lastProcurements, document.docNumber);

            procurements.modificationDate = DateTime.Now;
            procurements.contractVersionGid = document.gid.ToString();
            procurements.SetActivated();

            return new { xml = _documentSerializer.XmlSerializeObjectToString(procurements) };
        }

        [HttpPost]
        public object Communication(R_09987.CommunicationTypeNomenclature type)
        {
            R_10042.Communication communication = null;

            communication = R_10042.Communication.Init(communication, type);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(communication) };
        }

        [HttpPost]
        public object CommunicationTemplate(ContractCommunicationInitializer massCommunication)
        {
            R_10042.Communication communication = null;

            communication = R_10042.Communication.Init(communication, massCommunication);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(communication) };
        }

        [HttpPost]
        public object FinanceReport(ContractInitializerReport document)
        {
            R_10043.FinanceReport financeReport = this.InitializeFinanceReport(document);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(financeReport) };
        }

        [HttpPost]
        public object CopyFinanceReport(ContractInitializerReport document)
        {
            R_10043.FinanceReport financialReport = this.InitializeFinanceReport(document);
            R_10043.FinanceReport originFinancialReport = null;

            if (!string.IsNullOrWhiteSpace(document.originFinancialReportXml))
            {
                originFinancialReport = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(document.originFinancialReportXml);
                financialReport.Copy(originFinancialReport);
            }

            return new { xml = _documentSerializer.XmlSerializeObjectToString(financialReport) };
        }

        private R_10043.FinanceReport InitializeFinanceReport(ContractInitializerReport document)
        {
            if (document == null
                || String.IsNullOrWhiteSpace(document.packageGid)
                || String.IsNullOrWhiteSpace(document.contractGid)
                || String.IsNullOrWhiteSpace(document.contractNumber)
                || String.IsNullOrWhiteSpace(document.docNumber)
                || String.IsNullOrWhiteSpace(document.contractVersionXml))
                throw new Exception("packageGid or contractGid or contractNumber or docNumber or contractVersionXml is null");

            R_10040.BFPContract contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(document.contractVersionXml);

            R_10041.Procurements procurements = null;
            if (!String.IsNullOrWhiteSpace(document.contractProcurementXml))
            {
                procurements = _documentSerializer.XmlDeserializeFromString<R_10041.Procurements>(document.contractProcurementXml);
            }

            R_10043.FinanceReport lastFinanceReport = null;
            if (!String.IsNullOrWhiteSpace(document.lastFinancialReportXml))
            {
                lastFinanceReport = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(document.lastFinancialReportXml);
            }

            R_10045.PaymentRequest lastPaymentRequest = null;
            if (!String.IsNullOrWhiteSpace(document.advancePaymentReportXml))
            {
                lastPaymentRequest = _documentSerializer.XmlDeserializeFromString<R_10045.PaymentRequest>(document.advancePaymentReportXml);
            }

            return R_10043.FinanceReport.Init(document.packageGid, document.contractGid, document.contractNumber, document.docNumber, document.docSubNumber, contract, procurements, lastPaymentRequest, lastFinanceReport, document.procedureTypeAlias, document.approvedCumulativeCSDBudgetAmounts);
        }

        [HttpPost]
        public object LoadFinanceReport(ContractInitializerReport document)
        {
            if (document == null
                || String.IsNullOrWhiteSpace(document.xml))
                throw new Exception("xml is null");

            R_10043.FinanceReport financeReport = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(document.xml);

            R_10041.Procurements procurements = null;
            if (!String.IsNullOrWhiteSpace(document.contractProcurementXml))
            {
                procurements = _documentSerializer.XmlDeserializeFromString<R_10041.Procurements>(document.contractProcurementXml);
            }

            R_10043.FinanceReport lastFinanceReport = null;
            if (!String.IsNullOrWhiteSpace(document.lastFinancialReportXml))
            {
                lastFinanceReport = _documentSerializer.XmlDeserializeFromString<R_10043.FinanceReport>(document.lastFinancialReportXml);
            }

            R_10045.PaymentRequest lastPaymentRequest = null;
            if (!String.IsNullOrWhiteSpace(document.advancePaymentReportXml))
            {
                lastPaymentRequest = _documentSerializer.XmlDeserializeFromString<R_10045.PaymentRequest>(document.advancePaymentReportXml);
            }

            financeReport.Load(procurements, lastPaymentRequest, lastFinanceReport, document.approvedCumulativeCSDBudgetAmounts);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(financeReport) };
        }

        [HttpPost]
        public object TechnicalReport(ContractInitializerReport document)
        {
            R_10044.TechnicalReport technicalReport = this.InitializeTechnicalReport(document);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(technicalReport) };
        }

        [HttpPost]
        public object CopyTechnicalReport(ContractInitializerReport document)
        {
            R_10044.TechnicalReport technicalReport = this.InitializeTechnicalReport(document);

            R_10044.TechnicalReport originTechnicalReport = null;

            if (!string.IsNullOrWhiteSpace(document.originTechnicalReportXml))
            {
                originTechnicalReport = _documentSerializer.XmlDeserializeFromString<R_10044.TechnicalReport>(document.originTechnicalReportXml);
                technicalReport.Copy(originTechnicalReport);
            }

            return new { xml = _documentSerializer.XmlSerializeObjectToString(technicalReport) };
        }

        private R_10044.TechnicalReport InitializeTechnicalReport(ContractInitializerReport document)
        {
            if (document == null
                || String.IsNullOrWhiteSpace(document.packageGid)
                || String.IsNullOrWhiteSpace(document.contractGid)
                || String.IsNullOrWhiteSpace(document.contractNumber)
                || String.IsNullOrWhiteSpace(document.docNumber)
                || String.IsNullOrWhiteSpace(document.contractVersionXml))
                throw new Exception("packageGid or contractGid or contractNumber or docNumber or contractVersionXml is null");

            R_10040.BFPContract contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(document.contractVersionXml);

            R_10041.Procurements procurements = null;
            if (!String.IsNullOrWhiteSpace(document.contractProcurementXml))
            {
                procurements = _documentSerializer.XmlDeserializeFromString<R_10041.Procurements>(document.contractProcurementXml);
            }

            R_10044.TechnicalReport lastTechnicalReport = null;

            if (!String.IsNullOrWhiteSpace(document.lastTechnicalReportXml))
            {
                lastTechnicalReport = _documentSerializer.XmlDeserializeFromString<R_10044.TechnicalReport>(document.lastTechnicalReportXml);
            }

            return R_10044.TechnicalReport.Init(document.packageGid, document.contractGid, document.contractNumber, document.docNumber, document.docSubNumber, contract, procurements, lastTechnicalReport, document.approvedIndicators);
        }

        [HttpPost]
        public object LoadTechnicalReport(ContractInitializerReport document)
        {
            if (document == null
                || String.IsNullOrWhiteSpace(document.xml))
                throw new Exception("xml is null");

            R_10044.TechnicalReport technicalReport = _documentSerializer.XmlDeserializeFromString<R_10044.TechnicalReport>(document.xml);

            R_10041.Procurements procurements = null;
            if (!String.IsNullOrWhiteSpace(document.contractProcurementXml))
            {
                procurements = _documentSerializer.XmlDeserializeFromString<R_10041.Procurements>(document.contractProcurementXml);
            }

            R_10044.TechnicalReport lastTechnicalReport = null;
            if (!String.IsNullOrWhiteSpace(document.lastTechnicalReportXml))
            {
                lastTechnicalReport = _documentSerializer.XmlDeserializeFromString<R_10044.TechnicalReport>(document.lastTechnicalReportXml);
            }

            technicalReport.Load(procurements, lastTechnicalReport, document.approvedIndicators);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(technicalReport) };
        }

        [HttpPost]
        public object PaymentRequest(ContractInitializerPaymentRequest document)
        {
            if (document == null
                || String.IsNullOrWhiteSpace(document.packageGid)
                || String.IsNullOrWhiteSpace(document.contractGid)
                || String.IsNullOrWhiteSpace(document.contractNumber)
                || String.IsNullOrWhiteSpace(document.docNumber)
                || document.type == null
                || String.IsNullOrWhiteSpace(document.type.value)
                || String.IsNullOrWhiteSpace(document.type.description)
                || String.IsNullOrWhiteSpace(document.contractVersionXml))
                throw new Exception("packageGid or contractGid or contractNumber or docNumber or type or contractVersionXml is null");

            R_10040.BFPContract contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(document.contractVersionXml);

            R_10045.PaymentRequest paymentRequest = R_10045.PaymentRequest.Init(new R_09991.EnumNomenclature() { Value = document.type.value, Description = document.type.description }, document.packageGid, document.contractGid, document.contractNumber, document.docNumber,document.docSubNumber, contract);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(paymentRequest) };
        }

        [HttpPost]
        public object CopyPaymentRequest(ContractInitializerPaymentRequest document)
        {
            if (document == null
                || String.IsNullOrWhiteSpace(document.packageGid)
                || String.IsNullOrWhiteSpace(document.contractGid)
                || String.IsNullOrWhiteSpace(document.contractNumber)
                || String.IsNullOrWhiteSpace(document.docNumber)
                || document.type == null
                || String.IsNullOrWhiteSpace(document.type.value)
                || String.IsNullOrWhiteSpace(document.type.description)
                || String.IsNullOrWhiteSpace(document.contractVersionXml))
                throw new Exception("packageGid or contractGid or contractNumber or docNumber or type or contractVersionXml is null");

            R_10040.BFPContract contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(document.contractVersionXml);

            R_10045.PaymentRequest paymentRequest = R_10045.PaymentRequest.Init(new R_09991.EnumNomenclature() { Value = document.type.value, Description = document.type.description }, document.packageGid, document.contractGid, document.contractNumber, document.docNumber, document.docSubNumber, contract);
            R_10045.PaymentRequest originPaymentRequest;
            if (!string.IsNullOrEmpty(document.originPaymentReportXml))
            {
                originPaymentRequest = _documentSerializer.XmlDeserializeFromString<R_10045.PaymentRequest>(document.originPaymentReportXml);
                paymentRequest.Copy(originPaymentRequest);
            }
            return new { xml = _documentSerializer.XmlSerializeObjectToString(paymentRequest) };
        }

        [HttpPost]
        public object SpendingPlan(ContractInitializerReport document)
        {
            if (document == null || document.gid == null)
                throw new Exception("gid is null");

            R_10077.SpendingPlan lastSpendingPlan = null;
            if (!String.IsNullOrWhiteSpace(document.xml))
            {
                lastSpendingPlan = _documentSerializer.XmlDeserializeFromString<R_10077.SpendingPlan>(document.xml);
            }
            R_10040.BFPContract contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(document.contractVersionXml);

            R_10077.SpendingPlan spendingPlan = R_10077.SpendingPlan.Init(contract, lastSpendingPlan);

            spendingPlan.modificationDate = DateTime.Now;
            spendingPlan.contractVersionGid = document.gid.ToString();

            return new { xml = _documentSerializer.XmlSerializeObjectToString(spendingPlan) };
        }

        [HttpPost]
        public object Offer(ContractInitializerOffer document)
        {
            if (document == null
                || String.IsNullOrWhiteSpace(document.contractGid)
                || String.IsNullOrWhiteSpace(document.procurementsGid)
                || String.IsNullOrWhiteSpace(document.planGid)
                || String.IsNullOrWhiteSpace(document.positionGid)
                || String.IsNullOrWhiteSpace(document.contractVersionXml)
                || String.IsNullOrWhiteSpace(document.contractProcurementXml))
                throw new Exception("contractGid or procurementsGid or planGid or positionGid or contractVersionXml or contractProcurementXml is null");

            R_10040.BFPContract contract = _documentSerializer.XmlDeserializeFromString<R_10040.BFPContract>(document.contractVersionXml);
            R_10041.Procurements procurements = _documentSerializer.XmlDeserializeFromString<R_10041.Procurements>(document.contractProcurementXml);

            var offer = R_10080.Offer.Init(contract, procurements, document.contractGid, document.procurementsGid, document.planGid, document.positionGid);

            return new { xml = _documentSerializer.XmlSerializeObjectToString(offer) };
        }
    }
}
