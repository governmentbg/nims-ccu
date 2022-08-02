using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Model.Repositories;

namespace Eumis.Public.Web.Controllers
{
    public partial class NomenclatureController : BaseController
    {
        private INomenclatureRepository nomenclatureRepository;

        public NomenclatureController(INomenclatureRepository nomenclatureRepository)
        {
            this.nomenclatureRepository = nomenclatureRepository;
        }

        [HttpPost]
        public virtual JsonResult GetCompanyType(string id)
        {
            var companyTypeName = this.nomenclatureRepository.GetCompanyTypes()
                            .Where(c => c.CompanyTypeId.ToString() == id)
                            .FirstOrDefault().TransName;

            return this.Json(new { id = id, text = companyTypeName });
        }

        [HttpPost]
        public virtual JsonResult GetCompanyTypes(string term)
        {
            var companyTypes = this.nomenclatureRepository.GetCompanyTypes()
                            .ToList()
                            .Select(e => new
                            {
                                id = e.CompanyTypeId.ToString(),
                                text = e.TransName,
                            });

            if (!string.IsNullOrWhiteSpace(term))
            {
                companyTypes = companyTypes.Where(e => e.text.ToLower().Contains(term.ToLower())).ToList();
            }

            return this.Json(companyTypes);
        }

        [HttpPost]
        public virtual JsonResult GetCompanyLegalType(string id)
        {
            var companyLegalTypeName = this.nomenclatureRepository.GetCompanyLegalTypes()
                            .Where(c => c.CompanyLegalTypeId.ToString() == id)
                            .FirstOrDefault().TransName;

            return this.Json(new { id = id, text = companyLegalTypeName });
        }

        [HttpPost]
        public virtual JsonResult GetCompanyLegalTypes(string term, string parentId)
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                return this.Json(new List<object>());
            }

            var companyLegalTypes = this.nomenclatureRepository.GetCompanyLegalTypes()
                            .Where(e => e.CompanyTypeId.ToString() == parentId)
                            .ToList()
                            .Select(e => new
                            {
                                id = e.CompanyLegalTypeId.ToString(),
                                text = e.TransName,
                            });

            if (!string.IsNullOrWhiteSpace(term))
            {
                companyLegalTypes = companyLegalTypes.Where(e => e.text.ToLower().Contains(term.ToLower())).ToList();
            }

            return this.Json(companyLegalTypes);
        }
    }
}