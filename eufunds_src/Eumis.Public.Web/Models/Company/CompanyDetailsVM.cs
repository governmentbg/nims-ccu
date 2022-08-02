using System.Collections.Generic;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.Companies.ViewObjects;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Web.Models.Company
{
    public class CompanyDetailsVM
    {
        public string Name { get; set; }

        public string Seat { get; set; }

        public string Uin { get; set; }

        public UinType UinType { get; set; }

        public CompanyEnumType Type { get; set; }

        public bool IsHistoric { get; set; }

        public CompanyProjectsVO Projects { get; set; }

        public object GetRouteValues(CompanyEnumType type)
        {
            return new
            {
                uin = ConfigurationBasedStringEncrypter.Encrypt(this.Uin),
                uinType = this.UinType,
                isHistoric = this.IsHistoric,
                type,
            };
        }
    }
}