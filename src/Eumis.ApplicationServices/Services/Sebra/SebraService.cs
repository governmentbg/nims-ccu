using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.Sebra.Parsers;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Sebra.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Sebra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Eumis.ApplicationServices.Services.Sebra
{
    internal class SebraService : ISebraService
    {
        private const string CurrencyCode = "BGN";
        private static readonly Regex ProjectRegNumberRegex = new Regex(@"([A-Z0-9\-]*)-(?<projectNumber>\d\d\d\d)");

        private ISebraRepository sebraRepository;
        private IProjectsRepository projectsRepository;
        private IBlobServerCommunicator blobServerCommunicator;
        private ISebraProjectParser sebraProjectParser;

        public SebraService(
            ISebraRepository sebraRepository,
            IProjectsRepository projectsRepository,
            IBlobServerCommunicator blobServerCommunicator,
            ISebraProjectParser sebraProjectParser)
        {
            this.sebraRepository = sebraRepository;
            this.projectsRepository = projectsRepository;
            this.blobServerCommunicator = blobServerCommunicator;
            this.sebraProjectParser = sebraProjectParser;
        }

        public f GetSebraReport(
            int programmeId,
            int procedureId,
            DateTime fromDate,
            DateTime toDate,
            int fromNumber,
            int toNumber,
            string sendername,
            string acc,
            string o1,
            SebraPaymentType? type)
        {
            var projects = this.sebraRepository.GetProjects(procedureId, fromDate, toDate, fromNumber, toNumber);

            List<d> dList = new List<d>();
            var maxLength = 35;
            var ipolMaxLength = 30;
            foreach (var (project, index) in projects.Select((v, i) => (v, i)))
            {
                var projectNumber = ProjectRegNumberRegex.Match(project.RegNumber).Groups["projectNumber"].Value.Replace("0", string.Empty);

                dList.Add(new d
                {
                    doc = dDoc.ПНВБ,
                    nok = (index + 1).ToString(),
                    ipol = project.BeneficiaryName.Length <= ipolMaxLength ? project.BeneficiaryName.Replace("'", string.Empty).Replace("\"", string.Empty) : project.BeneficiaryName.Substring(0, ipolMaxLength),
                    iban = project.BankAccount.Replace(" ", string.Empty),
                    cur = CurrencyCode,
                    su = Math.Round(project.PaidBfpTotalAmount.Value, 2),
                    o1 = o1,
                    o2 = project.RegNumber,
                    kd = string.Empty,
                    vpn = type.HasValue ? ((int)type).ToString() : string.Empty,
                });
            }

            List<a> aList = new List<a>();
            aList.Add(new a
            {
                acc = acc,
                cur = CurrencyCode,
                @do = dList.Sum(dl => dl.su),
                d = dList.ToArray(),
            });

            var procedureCode = this.sebraRepository.GetProcedureCode(procedureId);
            var refidPartOne = procedureCode.Replace(".", string.Empty).Replace("-", string.Empty);
            refidPartOne = refidPartOne.Length <= 12 ? refidPartOne : refidPartOne.Substring(refidPartOne.Length - 12);
            var refidPartTwo = fromNumber.ToString().PadLeft(4, '0');
            var refidPartThree = toNumber.ToString().PadLeft(4, '0');

            var programmeCompany = this.sebraRepository.GetProgrammeCompany(programmeId);
            DateTime currentTime = TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
            currentTime = new DateTime(
                currentTime.Ticks - (currentTime.Ticks % TimeSpan.TicksPerSecond),
                currentTime.Kind);

            var sender = string.IsNullOrEmpty(sendername) ?
                (programmeCompany.Name.Length <= maxLength ?
                programmeCompany.Name :
                programmeCompany.Name.Substring(0, maxLength)) :
                (sendername.Trim().Length <= maxLength ?
                sendername.Trim() :
                sendername.Trim().Substring(0, maxLength));

            return new f
            {
                h = new h
                {
                    refid = $"{refidPartOne}{refidPartTwo}{refidPartThree}",
                    timestamp = currentTime,
                    sender = programmeCompany.Uin,
                    sendername = sender,
                    receiver = hReceiver.BNBOnline,
                },
                a = aList.ToArray(),
            };
        }

        public (f xml, string procedureCode) GetSebraReportByFile(
            Guid blobKey,
            string sendername,
            string acc,
            string o1,
            SebraPaymentType? type)
        {
            IList<int> projectIds = new List<int>();
            IList<string> projectRegNumbers = new List<string>();

            try
            {
                using (var excelStream = this.blobServerCommunicator.GetBlobStream(blobKey, true))
                {
                    projectRegNumbers = this.sebraProjectParser.ParseExcel(excelStream);
                }
            }
            catch (FileFormatException)
            {
                return (xml: new f(), procedureCode: string.Empty);
            }

            foreach (var projectRegNumber in projectRegNumbers)
            {
                var projectId = this.projectsRepository.GetProjectId(projectRegNumber);

                if (!projectId.HasValue)
                {
                    continue;
                }

                projectIds.Add(projectId.Value);
            }

            var projects = this.sebraRepository.GetProjects(projectIds.ToArray());

            List<d> dList = new List<d>();
            var maxLength = 35;
            var ipolMaxLength = 30;
            foreach (var (project, index) in projects.Select((v, i) => (v, i)))
            {
                var projectNumber = ProjectRegNumberRegex.Match(project.RegNumber).Groups["projectNumber"].Value.Replace("0", string.Empty);

                dList.Add(new d
                {
                    doc = dDoc.ПНВБ,
                    nok = (index + 1).ToString(),
                    ipol = project.BeneficiaryName.Length <= ipolMaxLength ? project.BeneficiaryName.Replace("'", string.Empty).Replace("\"", string.Empty) : project.BeneficiaryName.Substring(0, ipolMaxLength),
                    iban = project.BankAccount.Replace(" ", string.Empty),
                    cur = CurrencyCode,
                    su = Math.Round(project.PaidBfpTotalAmount.Value, 2),
                    o1 = o1,
                    o2 = project.RegNumber,
                    kd = string.Empty,
                    vpn = type.HasValue ? ((int)type).ToString() : string.Empty,
                });
            }

            List<a> aList = new List<a>();
            aList.Add(new a
            {
                acc = acc,
                cur = CurrencyCode,
                @do = dList.Sum(dl => dl.su),
                d = dList.ToArray(),
            });

            var projectsInfo = this.sebraRepository.GetProjectsInfo(projectIds.ToArray());
            var refidPartOne = projectsInfo.ProcedureCode.Replace(".", string.Empty).Replace("-", string.Empty);
            refidPartOne = refidPartOne.Length <= 12 ? refidPartOne : refidPartOne.Substring(refidPartOne.Length - 12);

            var refidPartTwo = projectsInfo.FirstProjectNumber;
            var refidPartThree = projectsInfo.LastProjectNumber;

            DateTime currentTime = TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
            currentTime = new DateTime(
                currentTime.Ticks - (currentTime.Ticks % TimeSpan.TicksPerSecond),
                currentTime.Kind);

            var sender = string.IsNullOrEmpty(sendername) ?
                (projectsInfo.CompanyName.Length <= maxLength ?
                projectsInfo.CompanyName :
                projectsInfo.CompanyName.Substring(0, maxLength)) :
                (sendername.Trim().Length <= maxLength ?
                sendername.Trim() :
                sendername.Trim().Substring(0, maxLength));

            var xml = new f
            {
                h = new h
                {
                    refid = $"{refidPartOne}{refidPartTwo}{refidPartThree}",
                    timestamp = currentTime,
                    sender = projectsInfo.CompanyUin,
                    sendername = sender,
                    receiver = hReceiver.BNBOnline,
                },
                a = aList.ToArray(),
            };

            return (xml: xml, procedureCode: projectsInfo.ProcedureCode);
        }
    }
}
