using System;
using System.Collections.Generic;
using Eumis.Domain.Projects;

namespace Eumis.ApplicationServices.Services.ProjectRegistration
{
    public interface IProjectRegistrationService
    {
        int RegisterEmpty(
            int procedureId,
            string projectName,
            string projectNameAlt,
            int companyId,
            int projectTypeId,
            ProjectRegistrationStatus regStatus,
            DateTime regDate,
            ProjectRecieveType recieveType,
            DateTime recieveDate,
            DateTime submitDate,
            string storagePlace,
            int? originals,
            int? copies,
            string notes);

        int RegisterSubmitted(
            int regProjectXmlId,
            int companyId,
            int projectTypeId,
            ProjectRegistrationStatus regStatus,
            DateTime regDate,
            ProjectRecieveType recieveType,
            DateTime recieveDate,
            DateTime submitDate,
            string storagePlace,
            int? originals,
            int? copies,
            string notes);

        string RegisterRegistrationXml(int registrationId, string xml, byte[] isunFile, IList<byte[]> signatures);
    }
}
