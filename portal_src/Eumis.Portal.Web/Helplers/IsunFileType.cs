using Eumis.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Helplers
{
    public enum IsunFileType
    {
        [Description("suni")]
        ProjectFile = 1,

        [Description("asuni")]
        AnswerFile = 2,

        [Description("tsuni")]
        TechnicalReportFile = 3,

        [Description("fsuni")]
        FinanceReportFile = 4,

        [Description("psuni")]
        PaymentRequstFile = 5,
    }
}
