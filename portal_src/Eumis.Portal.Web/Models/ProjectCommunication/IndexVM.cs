using Eumis.Documents.Contracts;
using PagedList;
using System;

namespace Eumis.Portal.Web.Models.ProjectCommunication
{
    public class IndexVM
    {
        public StaticPagedList<MessagePVO> Communications { get; set; }

        public string ProjectRegNumber { get; set; }

        public Guid registeredGid { get; set; }
    }
}
