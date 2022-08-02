using Eumis.Authentication.TokenProviders;
using Eumis.Common.Config;
using Eumis.Data.Monitorstat.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Communicators
{
    public class MonitorstatRestApiCommunicator : IMonitorstatRestApiCommunicator
    {
        private readonly string token;

        public MonitorstatRestApiCommunicator(IEumisTokenProvider eumisTokenProvider)
        {
            this.token = eumisTokenProvider.GenerateToken();
        }

        public IDictionary<string, string> GetSurveys(int year)
        {
            return MonitorstatAPI.GetSurveys(year, this.token).ToObject<Dictionary<string, string>>();
        }

        public IDictionary<string, string> GetReports(int year, string surveyCode)
        {
            return MonitorstatAPI.GetReports(year, surveyCode, this.token).ToObject<Dictionary<string, string>>();
        }

        public Guid CreateOperationalProgramme(ProgrammeDO programme)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(programme);

            return MonitorstatAPI.CreateOperationalProgramme(body, this.token).ToObject<Guid>();
        }

        public Guid CreateProgrammePriority(ProgrammePriorityDO programmePriority)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(programmePriority);

            return MonitorstatAPI.CreateProgrammePriority(body, this.token).ToObject<Guid>();
        }

        public Guid CreateProcedure(ProcedureDO procedure)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(procedure);

            return MonitorstatAPI.CreateProcedure(body, this.token).ToObject<Guid>();
        }

        public Guid CreateProcedureInquiryRequest(ProcedureInquiryDO procedureInquiry)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(procedureInquiry);

            return MonitorstatAPI.CreateProcedureInquiryRequest(body, this.token).ToObject<Guid>();
        }

        public Guid CreateSubjectRequest(SubjectRequestDO subjectRequest)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(subjectRequest);

            return MonitorstatAPI.CreateSubjectRequest(body, this.token).ToObject<Guid>();
        }
    }
}
