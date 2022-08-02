using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Authorization.ClaimsContexts.MapNodeIndicator
{
    internal delegate IMapNodeIndicatorClaimsContext MapNodeIndicatorClaimsContextFactory(int mapNodeId);

    internal interface IMapNodeIndicatorClaimsContext
    {
        int MapNodeId { get; }

        int ProgrammeId { get; }
    }
}
