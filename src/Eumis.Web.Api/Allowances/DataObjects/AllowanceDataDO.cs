using Eumis.Domain.Allowances;

namespace Eumis.Web.Api.Allowances.DataObjects
{
    public class AllowanceDataDO
    {
        public AllowanceDataDO()
        {
        }

        public AllowanceDataDO(Allowance allowance)
        {
            this.AllowanceId = allowance.AllowanceId;
            this.Name = allowance.Name;
            this.Version = allowance.Version;
        }

        public int? AllowanceId { get; set; }

        public string Name { get; set; }

        public byte[] Version { get; set; }
    }
}
