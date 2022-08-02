using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IEvalCommunicator
    {
        ContractEvalDocument GetEvalTable(Guid gid, string token);

        ContractEvalDocument PutEvalTable(Guid gid, string token, string xml, byte[] version);

        ContractEvalDocument SubmitEvalTable(Guid gid, string token, byte[] version);

        //[Route("{xmlGid:guid}")]
        ContractEvalDocument GetEvalSheet(Guid gid, string token);
        
        //[HttpPut]
        //[Route("{xmlGid:guid}")]
        ContractEvalDocument PutEvalSheet(Guid gid, string token, string xml, byte[] version);
        
        //[HttpPost]
        //[Route("{xmlGid:guid}/submit")]
        ContractEvalDocument SubmitEvalSheet(Guid gid, string token, byte[] version);
        
        //[HttpPost]
        //[Route("{xmlGid:guid}/pause")]
        ContractEvalDocument PauseEvalSheet(Guid gid, string token, byte[] version);
    }
}