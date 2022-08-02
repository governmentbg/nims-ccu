using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.IntegrationRegiX.Host
{
    public class RegixAction
    {
        private RegixAction(string actionName)
        {
            this.Value = actionName;
        }

        public static RegixAction ValidPersonSearch { get; } = new RegixAction("TechnoLogica.RegiX.GraoNBDAdapter.APIService.INBDAPI.ValidPersonSearch");

        public static RegixAction PersonDataSearch { get; } = new RegixAction("TechnoLogica.RegiX.GraoNBDAdapter.APIService.INBDAPI.PersonDataSearch");

        public static RegixAction GetValidUICInfo { get; } = new RegixAction("TechnoLogica.RegiX.AVTRAdapter.APIService.ITRAPI.GetValidUICInfo");

        public static RegixAction SearchByIdentifier { get; } = new RegixAction("TechnoLogica.RegiX.DaeuReportsAdapter.APIService.IDaeuReportsAPI.SearchByIdentifier");

        public static RegixAction GetStateOfPlay { get; } = new RegixAction("TechnoLogica.RegiX.AVBulstat2Adapter.APIService.IAVBulstat2API.GetStateOfPlay");

        public static RegixAction GetObligatedPersons { get; } = new RegixAction("TechnoLogica.RegiX.NRAObligatedPersonsAdapter.APIService.INRAObligatedPersonsAPI.GetObligatedPersons");

        public static RegixAction GetPersonalIdentity { get; } = new RegixAction("TechnoLogica.RegiX.MVRBDSAdapter.APIService.IMVRBDSAPI.GetPersonalIdentity");

        public static RegixAction GetActualState { get; } = new RegixAction("TechnoLogica.RegiX.AVTRAdapter.APIService.ITRAPI.GetActualState");

        public static RegixAction GetNPORegistrationInfo { get; } = new RegixAction("TechnoLogica.RegiX.MPNPOAdapter.APIService.IMPNPOAPI.GetNPORegistrationInfo");

        public static RegixAction CheckObligations { get; } = new RegixAction("TechnoLogica.RegiX.REZMAAdapter.APIService.IREZMAAPI.CheckObligations");

        public static RegixAction GetRegisteredAnimalsByCategory { get; } = new RegixAction("TechnoLogica.RegiX.BABHZhSAdapter.APIService.IBABHZhSAPI.GetRegisteredAnimalsByCategory");

        public static RegixAction GetRegisteredAnimalsInOEZByCategory { get; } = new RegixAction("TechnoLogica.RegiX.BABHZhSAdapter.APIService.IBABHZhSAPI.GetRegisteredAnimalsInOEZByCategory");

        public static RegixAction GetPenalProvisionForPeriod { get; } = new RegixAction("TechnoLogica.RegiX.GitPenalProvisionsAdapter.APIService.IGitPenalProvisionsAPI.GetPenalProvisionForPeriod");

        public string Value { get; set; }
    }
}
