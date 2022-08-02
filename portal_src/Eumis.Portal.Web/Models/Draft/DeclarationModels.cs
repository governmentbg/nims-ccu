using System;
using System.Collections.Generic;
using System.Text;

namespace Eumis.Portal.Web.Models.Draft
{
    public class OperativeProgrammeInfo
    {
        public OperativeProgrammeInfo()
        {
            this.PriorityAxesNames = new List<string>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public List<string> PriorityAxesNames { get; set; }
    }

    public class MinistryInfo
    {
        public string Direction { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public MinistryInfo() { }

        public MinistryInfo(string direction, string name, string address)
        {
            this.Direction = direction;
            this.Name = name;
            this.Address = address;
        }
    }

    public static class MinistriesMappings
    {
        public static readonly Dictionary<string, MinistryInfo> Mappings = new Dictionary<string, MinistryInfo>() {
           { "2014BG05SFOP001", new MinistryInfo("Дирекция „Оперативна програма „Административен капацитет““", "Министерство на финансите", "1040 София, България, ул. \"Г. С. Раковски\" 102")},

           { "2014BG16M1OP001", new MinistryInfo("Дирекция „Координация на програми и проекти”", "Министерство на транспорта, информационните технологии и съобщенията", 
               "1000 София, България, ул. \"Дякон Игнатий\" № 9") },

           { "2014BG16RFOP001", new MinistryInfo("Главна дирекция „Програмиране на регионалното развитие”", "Министерство на регионалното развитие и благоустройството", 
               "1202 София, България, Ул. \"Св.Св. Кирил и Методий\" № 17-19")},

           { "2014BG05M9OP001", new MinistryInfo("Главна дирекция „Европейски фондове, международни програми и проекти”", "Министерство на труда и социалната политика", 
               "1051 София, България, ул. Триадица №2")},

           { "2014BG16RFOP002", new MinistryInfo("Главна дирекция  „Европейски фондове за конкурентоспособност”", "Министерство на икономиката", "1000 София, България, ул. \"6-ти септември\" 21")},

           { "2014BG16M1OP002", new MinistryInfo("Главна дирекция „Оперативна програма „Околна среда““", "Министерство на околната среда и водите", "1000 София, България, бул. “Мария Луиза” № 22")},

           { "2014BG05M2OP001", new MinistryInfo("Главна дирекция „Структурни фондове и международни образователни програми““", "Министерство на образованието и науката", 
               "1000  София, България, бул. \"Княз Дондуков\" 2A")}
        };
    }

    public class DeclarationVM
    {
        public string CandidateName { get; set; }

        public string CandidateEIK { get; set; }

        public string Email { get; set; }

        public string ProcedureCode { get; set; }

        public string ProjectCode { get; set; }

        public string FileName { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public static DeclarationVM GetVM(R_10019.Project project)
        {
            DeclarationVM vm = new DeclarationVM();

            if (project.ProjectBasicData != null && project.ProjectBasicData.Procedure != null)
            {
                vm.ProcedureCode = project.ProjectBasicData.Procedure.Code;
            }

            if (project.Candidate != null)
            {
                vm.CandidateName = project.Candidate.Name;
                vm.CandidateEIK = project.Candidate.Uin;
                //vm.Email = project.Candidate.Email;
            }

            return vm;
        }

        public const string EmptyReplaceLong = ".................................................................................................";
        public const string EmptyReplaceShort = "................................................";
        public const string EmptyReplaceVeryShort = "........";
    }

    public class LabelVM
    {
        public string CandidateName { get; set; }

        public string CompanyRepresentativePerson { get; set; }

        public string UinType { get; set; }

        public string CandidateEIK { get; set; }

        public string ProcedureCode { get; set; }

        public string ProcedureName { get; set; }

        public string ProjectCode { get; set; }

        public string ProjectName { get; set; }

        public string Email { get; set; }

        public string Phone1 { get; set; }

        public string Address { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<OperativeProgrammeInfo> OperativeProgrammes { get; set; }

        public MinistryInfo Ministry { get; set; }

        public static LabelVM GetVM(R_10019.Project project)
        {
            LabelVM vm = new LabelVM();

            if (project.ProjectBasicData != null)
            {
                vm.ProjectName = project.ProjectBasicData.Name;

                if (project.ProjectBasicData.ProgrammeBasicDataCollection != null && project.ProjectBasicData.ProgrammeBasicDataCollection.Count > 0)
                {
                    vm.OperativeProgrammes = new List<OperativeProgrammeInfo>();

                    for (int i = 0; i < project.ProjectBasicData.ProgrammeBasicDataCollection.Count; i++)
                    {
                        OperativeProgrammeInfo programmeInfo = new OperativeProgrammeInfo();

                        if (project.ProjectBasicData.ProgrammeBasicDataCollection[i].Programme != null)
                        {
                            programmeInfo.Name = project.ProjectBasicData.ProgrammeBasicDataCollection[i].Programme.Name;
                            programmeInfo.Code = project.ProjectBasicData.ProgrammeBasicDataCollection[i].Programme.Code;
                        }

                        if (project.ProjectBasicData.ProgrammeBasicDataCollection[i].ProgrammePriorityCollection != null &&
                        project.ProjectBasicData.ProgrammeBasicDataCollection[i].ProgrammePriorityCollection.Count > 0)
                        {
                            for (int j = 0; j < project.ProjectBasicData.ProgrammeBasicDataCollection[i].ProgrammePriorityCollection.Count; j++)
                            {
                                var priorityAxis = project.ProjectBasicData.ProgrammeBasicDataCollection[i].ProgrammePriorityCollection[j];
                                programmeInfo.PriorityAxesNames.Add(priorityAxis.Name);
                                //vm.PriorityAxisNum = priorityAxis.Code;
                            }
                        }

                        vm.OperativeProgrammes.Add(programmeInfo);
                    }

                    string operativeProgrammeCode = vm.OperativeProgrammes[0].Code;

                    if (MinistriesMappings.Mappings.ContainsKey(operativeProgrammeCode))
                    {
                        vm.Ministry = MinistriesMappings.Mappings[operativeProgrammeCode];
                    }
                    else
                        vm.Ministry = new MinistryInfo();
                }

                if (project.ProjectBasicData.Procedure != null)
                {
                    vm.ProcedureCode = project.ProjectBasicData.Procedure.Code;
                    vm.ProcedureName = project.ProjectBasicData.Procedure.Name;
                }
            }

            if (project.Candidate != null)
            {
                vm.CandidateName = project.Candidate.Name;
                vm.CompanyRepresentativePerson = project.Candidate.CompanyRepresentativePerson;

                if (project.Candidate.UinType != null) 
                {
                    vm.UinType = project.Candidate.UinType.Name;
                }
                
                vm.CandidateEIK = project.Candidate.Uin;
                //vm.RepresentativeName = project.Candidate.CompanyRepresentativePerson;
                vm.Email = project.Candidate.Email;
                vm.Phone1 = project.Candidate.Phone1;

                if (project.Candidate.Correspondence != null)
                {
                    bool hasCountry = false;
                    string country = string.Empty;

                    if (project.Candidate.Correspondence.Country != null)
                    {
                        hasCountry = true;
                        country = project.Candidate.Correspondence.Country.Name;
                    }

                    if (!string.IsNullOrWhiteSpace(project.Candidate.Correspondence.FullAddress))
                    {
                        if (hasCountry)
                        {
                            vm.Address = string.Format("{0}, {1}", country, project.Candidate.Correspondence.FullAddress);
                        }
                        else
                            vm.Address = project.Candidate.Correspondence.FullAddress;
                    }
                    else
                    {
                        string street = project.Candidate.Correspondence.Street;
                        string postCode = project.Candidate.Correspondence.PostCode;
                        string city = string.Empty;

                        if (project.Candidate.Correspondence.Settlement != null)
                        {
                            city = project.Candidate.Correspondence.Settlement.Name;
                        }

                        StringBuilder addressBuilder = new StringBuilder();

                        if (!string.IsNullOrWhiteSpace(postCode))
                        {
                            addressBuilder.AppendFormat("{0} ", postCode);
                        }
                        if (!string.IsNullOrWhiteSpace(city))
                        {
                            addressBuilder.AppendFormat("{0}, ", city);
                        }
                        if (!string.IsNullOrWhiteSpace(country))
                        {
                            addressBuilder.AppendFormat("{0}, ", country);
                        }
                        if (!string.IsNullOrWhiteSpace(street))
                        {
                            addressBuilder.Append(street);
                        }

                        vm.Address = addressBuilder.ToString();
                    }
                }
            }

            return vm;
        }
    }
}