using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Eumis.Components.Web;
using System.IO;
using Eumis.Documents.Mappers;
using Eumis.Portal.Model.Repositories;
using Eumis.Common.Linq;
using Eumis.Portal.Model.Entities;
using Eumis.Documents.Enums;
using Eumis.Components.Communicators;
using Eumis.Documents.Contracts;
using Eumis.Common.ReCaptcha;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using Eumis.Common;
using System.Security.Claims;

namespace Eumis.Portal.Web.Controllers
{
    [Authorize]
    public class CompaniesController : ApiController
    {
        private IRegixCommunicator regixCommunicator;
        
        private EumisUser currentUser;

        public CompaniesController(IRegixCommunicator regixCommunicator)
        {
            this.regixCommunicator = regixCommunicator;

            ClaimsIdentity ci = this.User.Identity as ClaimsIdentity;
            this.currentUser = EumisUserManager.LoadUser(ci);
        }

        [HttpPost]
        public object SearchCompany(ContractSearchCompany vm)
        {
            if (String.IsNullOrWhiteSpace(vm.token))
            {
                return new { validation = false };
            }
            
            var gResponse = ReCaptchaCommunicator.GetReCaptchaResponse(vm.token, Constants.ReCaptchaServerKey);

            if (String.IsNullOrWhiteSpace(vm.uin) || String.IsNullOrWhiteSpace(vm.uinType) || !gResponse.Success)
            {
                return new { validation = false };
            }
            
            var company = this.regixCommunicator.GetCompany(this.currentUser.AccessToken, vm.procedureCode, vm.uin, vm.uinType);
            
            return company.Map();
        }

        public static R_10004.Company GetFakeCandidate()
        {
            R_10004.Company candidate = new R_10004.Company()
            {
                UinType = new R_10000.PrivateNomenclature(),
                CompanyType = new R_10000.PrivateNomenclature(),
                CompanyLegalType = new R_10000.PrivateNomenclature(),
                Seat = new R_10003.Address()
                {
                    Country = new R_10001.PublicNomenclature(),
                    Settlement = new R_10001.PublicNomenclature()
                },
                Correspondence = new R_10003.Address()
                {
                    Country = new R_10001.PublicNomenclature(),
                    Settlement = new R_10001.PublicNomenclature()
                }
            };

            candidate.Name = "БТК";
            candidate.UinType.Id = UinTypeNomenclature.Bulstat.Code;
            candidate.UinType.Name = UinTypeNomenclature.Bulstat.Name;
            candidate.Uin = "831642181";

            candidate.CompanyType.Id = "3";
            candidate.CompanyType.Name = "Компании";
            candidate.CompanyLegalType.Id = "3";
            candidate.CompanyLegalType.Name = "Компании / Акционерно дружество АД";

            var country = new SerializableSelectListItem { Value = "23", Text = "България" };

            candidate.Seat.Country.Code = country.Value;
            candidate.Seat.Country.Name = country.Text;
            candidate.Seat.FullAddress = "гр. София, бул. “Тотлебен” 8";
            candidate.Seat.PostCode = "1000";

            candidate.Seat.Settlement.Name = "гр. София";
            candidate.Seat.Settlement.Code = "1";
            candidate.Seat.Street = "бул. “Тотлебен” 8";

            candidate.Correspondence.Country.Code = country.Value;
            candidate.Correspondence.Country.Name = country.Text;
            candidate.Correspondence.PostCode = "1000";
            candidate.Correspondence.Settlement.Name = "гр. София";
            candidate.Correspondence.Settlement.Code = "1";
            candidate.Correspondence.Street = "ул. Княз Борис I №128";
            candidate.Correspondence.FullAddress = "гр. София, ул. Княз Борис I №128";

            candidate.Email = "btk@btk.bg";
            candidate.Phone1 = "+3592112233";
            candidate.Phone2 = "+359880111111";
            candidate.Fax = "(+359 2) 988 11 22";
            candidate.CompanyRepresentativePerson = "Иван Петров";
            candidate.CompanyContactPerson = "Димитър Славков";
            candidate.CompanyContactPersonPhone = "+359880123456";
            candidate.CompanyContactPersonEmail = "dimitar.slavkov@mail.com";

            return candidate;
        }
    }
}
