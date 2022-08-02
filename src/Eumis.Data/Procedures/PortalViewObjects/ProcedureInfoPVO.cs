using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common;
using Eumis.Common.Localization;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.Json;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureInfoPVO
    {
        public ProcedureInfoPVO(ProcedureVersionJson procVersion, bool isActive, Guid procedureGid, ProcedureStatus status, DateTime endDate)
        {
            this.Gid = procedureGid;
            this.Name = procVersion.Name;
            this.NameAlt = procVersion.NameAlt;
            this.Code = procVersion.Code;
            this.Description = procVersion.Description.MakeHtml();
            this.DescriptionAlt = procVersion.DescriptionAlt.MakeHtml();
            this.Status = status;
            this.StatusText = status;
            this.QaBlobKey = procVersion.QaBlobKey;
            this.QaFileName = procVersion.QaFileName;
            this.QaModifyDate = procVersion.QaModifyDate;
            this.IsActive = isActive;
            this.ApplicationGuidelines = procVersion.AppGuidelines.Select(ag => new AppGuidelinePVO(ag)).ToList();
            this.EndingDate = endDate;
        }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public ProcedureStatus Status { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public ProcedureStatus StatusText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public ProcedureStatus StatusTextAlt
        {
            get
            {
                return this.StatusText;
            }
        }

        public string InternetAddress { get; set; }

        public DateTime EndingDate { get; set; }

        public string EndingDateNotes { get; set; }

        public Guid? QaBlobKey { get; set; }

        public string QaFileName { get; set; }

        public DateTime? QaModifyDate { get; set; }

        public bool IsActive { get; set; }

        public IList<AppGuidelinePVO> ApplicationGuidelines { get; set; }
    }
}
