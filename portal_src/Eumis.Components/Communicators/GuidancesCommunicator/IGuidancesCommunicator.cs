using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IGuidancesCommunicator
    {
        IEnumerable<GuidanceVO> GetGuidances(string module);
    }
}