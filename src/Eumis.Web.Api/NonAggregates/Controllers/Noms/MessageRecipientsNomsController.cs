using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Messages.Repositories;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/messageRecipients")]
    public class MessageRecipientsNomsController : ApiController
    {
        private IMessageRecipientsNomsRepository nomsRepository;

        public MessageRecipientsNomsController(IMessageRecipientsNomsRepository nomsRepository)
        {
            this.nomsRepository = nomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetUsersNoms([FromUri]int[] ids, string term = null, int offset = 0, int? limit = null)
        {
            return this.nomsRepository.GetUsers(ids, term, offset, limit);
        }
    }
}
