using System;
using Eumis.Domain.Projects;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectAnswerRegistrationDO
    {
        public string AnswerHash { get; set; }

        public DateTime RegDate { get; set; }
    }
}
