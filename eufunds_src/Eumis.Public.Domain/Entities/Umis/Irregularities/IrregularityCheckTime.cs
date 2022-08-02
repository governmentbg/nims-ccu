using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public enum IrregularityCheckTime
    {
        [Description("Преди плащане")]
        BeforePayment = 1,

        [Description("След плащане")]
        AfterPayment = 2,

        [Description(" Преди, както и след плащане")]
        BeforeAndAfterPayment = 3
    }
}
