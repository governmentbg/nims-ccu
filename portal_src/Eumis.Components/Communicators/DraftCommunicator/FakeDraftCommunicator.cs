using System;
using System.Collections.Generic;
using Eumis.Documents.Contracts;
using System.Linq;

namespace Eumis.Components.Communicators
{
    public class FakeDraftCommunicator : IDraftCommunicator
    {
        private ContractDraft drafts
        {
            get
            {
                return new ContractDraft
                {
                    results = new List<RegProjectXmlPVO>
                    {
                        new RegProjectXmlPVO()
                        {
                            gid = Guid.NewGuid(),  
                            registrationDate = DateTime.Now.AddDays(-10),
                            registrationTypeText = "Електронно",
                            projectStatusText = "Оценяване",
                            programmeName = "Транспорт и транспортна инфраструктура",
                            procedureCode = "2014BG16RFOP001-1.2015.001",
                            procedureName = "Подкрепа за дребно мащабни мерки за предотвратяване на наводнения в градските агломерации",
                            companyName = "Министерство на регионалното развитие и благоустройство",
                            projectName = "Водоснабдяване на пиринския район от извори белмекен и трансгранично сътрудничестов с Гърция, Румъния. Босна и Херцеговина, въвеждане на електронни услуги."
                        },
                        new RegProjectXmlPVO()
                        {
                            gid = Guid.NewGuid(),  
                            registrationTypeText = "На хартия",
                            projectStatusText = "Неодобрен",
                            programmeName = "Транспорт и транспортна инфраструктура",
                            procedureCode = "2014BG16RFOP001-1.2015.001",
                            procedureName = "Подкрепа за дребно мащабни мерки за предотвратяване на наводнения в градските агломерации",
                            companyName = "Министерство на регионалното развитие и благоустройство",
                            projectName = "Водоснабдяване на пиринския район от извори белмекен и трансгранично сътрудничестов с Гърция, Румъния. Босна и Херцеговина, въвеждане на електронни услуги."
                        }
                    },
                    count = 2
                };
            }
        }

        public ContractDraft GetDrafts(string accessToken, int limit, int offset)
        {
            return drafts;
        }

        public ContractFinalized GetFinalizedProjects(string accessToken, int limit, int offset)
        {
            return (ContractFinalized)drafts;
        }

        public ContractRegistered GetRegisteredProjects(string accessToken, int limit, int offset)
        {
            return (ContractRegistered)drafts;
        }

        public ContractRegistered GetRegisteredProjectsCommunications(string accessToken, int limit, int offset)
        {
            return (ContractRegistered)drafts;
        }

        public ContractSubmitted GetSubmittedProjects(string accessToken, int limit, int offset)
        {
            return (ContractSubmitted)drafts;
        }

        public ContractDocumentXml GetDraft(System.Guid gid, string accessToken)
        {
            return new ContractDocumentXml()
            {
                gid = gid,
                xml = FakeProject.Xml.Trim(),
                version = new byte[1]
            };
        }

        public ContractDocumentXml GetProjectVersion(System.Guid gid, string accessToken)
        {
            return new ContractDocumentXml()
            {
                gid = gid,
                xml = FakeProject.Xml.Trim(),
                version = new byte[1]
            };
        }

        public ContractDocumentXml CreateDraft(string xml, byte[] version, string accessToken)
        {
            return new ContractDocumentXml()
            {
                gid = Guid.NewGuid(),
                xml = FakeProject.Xml.Trim(),
                version = version
            };
        }

        public ContractDocumentXml UpdateDraft(System.Guid gid, string xml, byte[] version, string accessToken)
        {
            return new ContractDocumentXml()
            {
                gid = gid,
                xml = FakeProject.Xml.Trim(),
                version = version
            };
        }

        public void DeleteDraft(System.Guid gid, string xml, byte[] version, string accessToken)
        {

        }

        public void FinalizeDraft(Guid gid, byte[] version, string accessToken)
        {

        }

        public void DefinalizeDraft(Guid gid, string accessToken)
        {

        }

        public string Submit(Guid gid, string accessToken)
        {
            return string.Empty;
        }

        public string Register(byte[] isun, List<byte[]> signatures, string accessToken)
        {
            return string.Empty;
        }
    }
}