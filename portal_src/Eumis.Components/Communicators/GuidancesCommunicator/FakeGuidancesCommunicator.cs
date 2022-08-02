using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeGuidancesCommunicator : IGuidancesCommunicator
    {
        IEnumerable<GuidanceVO> IGuidancesCommunicator.GetGuidances(string module)
        {
            return new List<GuidanceVO>();
        }
    }
}