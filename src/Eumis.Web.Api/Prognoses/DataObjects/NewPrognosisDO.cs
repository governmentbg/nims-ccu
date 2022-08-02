using Eumis.Domain.NonAggregates;

namespace Eumis.Web.Api.Prognoses.DataObjects
{
    public class NewPrognosisDO
    {
        public int? ScopeId { get; set; }

        public Year? Year { get; set; }

        public Month? Month { get; set; }
    }
}
