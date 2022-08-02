using Eumis.Common.Config;
using Monitorstat.Common.Contracts;
using Monitorstat.Common.MonitorstatService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Eumis.Integration.Monitorstat.Communicators
{
    public class MonitorstatCommunicator : IMonitorstatCommunicator
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string MonitorstatUser = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:MonitorstatUser");
        private static readonly string MonitorstatPassword = ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:MonitorstatPassword");
        private static readonly bool MonitorstatValidаteCertificate = Convert.ToBoolean(ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:ValidateServerCertificate"));

        private readonly MonitorstatServiceClient client;

        public MonitorstatCommunicator(MonitorstatServiceClient client)
        {
            this.client = client;

            this.client.ClientCredentials.UserName.UserName = MonitorstatUser;
            this.client.ClientCredentials.UserName.Password = MonitorstatPassword;

            if (!MonitorstatValidаteCertificate)
            {
#pragma warning disable CA5359 // Server certificate of monitorstat might be expired
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => true;
#pragma warning restore CA5359 // Server certificate of monitorstat might be expired
            }
        }

        public IDictionary<string, string> GetSurveys(int year)
        {
            if (this.client.InnerChannel.State == System.ServiceModel.CommunicationState.Faulted)
            {
                Logger.Error("Inner channel state is faulted!");
                throw new Exception();
            }

            var surveys = this.client.GetSurveys(new GetSurveysRequest { Year = year, Language = Language.Bg });
            this.client.Close();

            return surveys;
        }

        public IDictionary<string, string> GetReports(int year, string surveyCode)
        {
            var reports = this.client.GetReports(new GetReportsRequest { Year = year, Language = Language.Bg, SurveyIdentifier = surveyCode });
            this.client.Close();

            return reports;
        }

        public Guid CreateOperationalProgramme(ProgrammeDO programme)
        {
            var request = new AddOperationalProgrammeRequest();
            var operationalProgramme = new AddOperationalProgramme(programme);

            request.Add(Language.Bg, operationalProgramme);
            Guid guid = this.client.AddOperationalProgramme(request);

            return guid;
        }

        public Guid CreateProgrammePriority(ProgrammePriorityDO programmePriority)
        {
            var priorityAxisRequest = new AddPriorityAxisRequest();
            var priorityAxis = new AddPriorityAxis(programmePriority);

            priorityAxisRequest.Add(Language.Bg, priorityAxis);
            Guid guid = this.client.AddPriorityAxis(priorityAxisRequest);

            return guid;
        }

        public Guid CreateProcedure(ProcedureDO procedure)
        {
            var request = new AddProcedureRequest();
            var procedureRequest = new AddProcedure(procedure);

            request.Add(Language.Bg, procedureRequest);
            Guid guid = this.client.AddProcedure(request);

            return guid;
        }

        public Guid CreateProcedureInquiry(ProcedureInquiryDO inquiry)
        {
            var request = new AddSurveyInquiryRequest();
            var inquiryRequest = new AddSurveyInquiry(inquiry);

            request.Add(Language.Bg, inquiryRequest);

            var gid = this.client.AddSurveyInquiry(request);

            return gid;
        }

        public Guid CreateSubjectRequest(SubjectRequestDO inquiry)
        {
            RegisterSubjectRequest request = new RegisterSubjectRequest();
            request.SurveyInquiryIdentifier = inquiry.ProcedureInquiryGid;

            request.SubjectRequests = inquiry.ToSubjectRequestArray();

            var response = this.client.RegisterSubjectRequest(request);
            if (response.Count == 0)
            {
                throw new Exception("Unexpected Monitorstat reply! Dictionary doesn't contain any keys");
            }

            return response.Keys.FirstOrDefault();
        }

        public Nomenclature[] GetProgrammeStatuses() => this.client.GetStatuses();

        public Nomenclature[] GetProgrammeGroups() => this.client.GetOperationalProgrammeGroups(Language.Bg);
    }
}
