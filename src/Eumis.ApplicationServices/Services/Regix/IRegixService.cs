using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Data.Companies.PortalViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.ApplicationServices.Services.Regix
{
    public interface IRegixService
    {
        Domain.Companies.Company GetCommercialRegisterCompany(string uin);

        Domain.Companies.Company GetBulstatRegisterCompany(string uin);

        string GetPersonNames(string personalBulstat);

        CompanyPVO GetCompany(string uin, UinType uinType, string code);
    }
}
