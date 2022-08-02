using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
namespace Eumis.Components.Communicators
{
    public interface IDraftCommunicator
    {
        //[RoutePrefix("api/registration")]
        //[Route("projects?type=")]
        ContractDraft GetDrafts(string accessToken, int limit, int offset);

        ContractFinalized GetFinalizedProjects(string accessToken, int limit, int offset);

        ContractRegistered GetRegisteredProjects(string accessToken, int limit, int offset);

        ContractRegistered GetRegisteredProjectsCommunications(string accessToken, int limit, int offset);

        ContractSubmitted GetSubmittedProjects(string accessToken, int limit, int offset);

        //[RoutePrefix("api/registration")]
        //[Route("projects/{gid:guid}")]
        ContractDocumentXml GetDraft(Guid gid, string accessToken);

        //[RoutePrefix("api/registration")]
        //[Route("projectVersions/{gid:guid}")]
        ContractDocumentXml GetProjectVersion(Guid gid, string accessToken);

        //[HttpPost]
        //[RoutePrefix("api/registration")]
        //[Route("projects")]
        ContractDocumentXml CreateDraft(string xml, byte[] version, string accessToken);

        //[HttpPut]
        //[RoutePrefix("api/registration")]
        //[Route("projects/{gid:guid}")]
        ContractDocumentXml UpdateDraft(Guid gid, string xml, byte[] version, string accessToken);

        //[HttpDelete]
        //[RoutePrefix("api/registration")]
        //[Route("projects/{gid:guid}")]
        void DeleteDraft(Guid gid, string xml, byte[] version, string accessToken);

        //[HttpPost]
        //[Route("drafts/{gid:guid}/finalize")]
        void FinalizeDraft(Guid gid, byte[] version, string accessToken);

        //[HttpPost]
        //[Route("drafts/{gid:guid}/definalize")]
        void DefinalizeDraft(Guid gid, string accessToken);

        //[HttpPost]
        //[Route("projects/{gid:guid}/submit")]
        string Submit(Guid gid, string accessToken);

        string Register(byte[] isun, List<byte[]> signatures, string accessToken);
    }
}
