using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.Components.Communicators
{
    public interface IProjectCommunicator
    {
        //[Route("{gid:guid}")]
        ContractProject GetProject(Guid gid, string token);

        //[HttpPut]
        //[Route("{gid:guid}")]
        ContractProject PutProject(Guid gid, string token, string xml, byte[] version);

        //[HttpPost]
        //[Route("{projectXmlGid:guid}/activate")]
        void SubmitProject(Guid gid, string token, string xml, byte[] version);

        //[HttpPost]
        //[Route("validate")]
        List<ContractValidationError> ValidateDraft(string xml, string accessToken);

        void ResurrectFiles(string xml);

        //[Route("getFilesZip")]
        byte[] GetProjectFilesZip(string projectGid, string accessToken);
    }
}
