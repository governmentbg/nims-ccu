using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R_10070
{
    public partial class BFPContractPlan
    {
        public static BFPContractPlan Load(R_10016.ProjectErrand errand)
        {
            R_10001.PublicNomenclature errandArea = new R_10001.PublicNomenclature();
            if(errand.ErrandArea != null)
            {
                errandArea.Code = errand.ErrandArea.Id;
                errandArea.Name = errand.ErrandArea.Name;
            }

            R_10001.PublicNomenclature errandType = new R_10001.PublicNomenclature();
            if (errand.ErrandType != null)
            {
                errandType.Code = errand.ErrandType.Id;
                errandType.Name = errand.ErrandType.Name;
            }

            BFPContractPlan plan = new BFPContractPlan()
                {
                    Name = errand.Name,
                    ErrandArea = errandArea,
                    ErrandLegalAct = errand.ErrandLegalAct,
                    ErrandType = errandType,
                    Description = errand.Description,
                    IsCentralProcurement = errand.IsCentralProcurement,
                };

            return plan;
        }
    }
}
