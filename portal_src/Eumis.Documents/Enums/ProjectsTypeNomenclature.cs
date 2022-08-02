using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class ProjectsTypeNomenclature
    {
        public string Description{get;set;}
        public string Code { get; set; }

        public static readonly ProjectsTypeNomenclature Draft = new ProjectsTypeNomenclature { Description = "Работни формуляри", Code = "draft" };
        public static readonly ProjectsTypeNomenclature Finalized = new ProjectsTypeNomenclature { Description = "Приключили формуляри", Code = "finalized" };
        public static readonly ProjectsTypeNomenclature Registered = new ProjectsTypeNomenclature { Description = "Подадени", Code = "registered" };
        public static readonly ProjectsTypeNomenclature Submitted = new ProjectsTypeNomenclature { Description = "За подаване на хартия", Code = "submitted" };

    }
}
