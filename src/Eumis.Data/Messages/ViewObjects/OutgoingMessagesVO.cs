using System.Collections.Generic;

namespace Eumis.Data.Messages.ViewObjects
{
    public class OutgoingMessagesVO
    {
        public int Count { get; set; }

        public IList<OutgoingMessageVO> Messages { get; set; }
    }
}
