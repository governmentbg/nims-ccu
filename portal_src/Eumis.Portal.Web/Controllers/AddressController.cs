using Eumis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Controllers
{
    [Authorize]
    public partial class AddressController : Controller
    {
        public virtual JsonResult GetMunicipalitiesByDistrictCode(string code)
        {
            var municipalities =
                Eumis.Portal.Model.Repositories.DataCache
                .GetMunicipalitiesByDistrict(int.Parse(code))
                .Select(e => new SerializableSelectListItem() { Value = e.MunicipalityId.ToString(), Text = e.Name })
                .OrderBy(e => e.Text)
                .ToList();

            InsertEmptyItemIfNecessary(municipalities);

            return Json(municipalities);
        }

        public virtual JsonResult GetSettlementsByMunicipalityCode(string code)
        {
            var settlements =
                Eumis.Portal.Model.Repositories.DataCache
                .GetSettlementsByMunicipality(int.Parse(code))
                .Select(e => new SerializableSelectListItem() { Value = e.SettlementId.ToString(), Text = e.Name })
                .OrderBy(e => e.Text)
                .ToList();

            InsertEmptyItemIfNecessary(settlements);

            return Json(settlements);
        }

        private void InsertEmptyItemIfNecessary(List<SerializableSelectListItem> selectList)
        {
            if (selectList.Count > 1)
            {
                selectList.Insert(0, new SerializableSelectListItem() { Value = string.Empty, Text = Eumis.Common.Extensions.UITextConstants.DropDownEmptyItemText });
            }
        }
    }
}