using System.Collections.Generic;

namespace Eumis.Data.Messages.ViewObjects
{
    public class IngoingMessagesVO
    {
        public int Count { get; set; }

        public IList<IngoingMessageVO> Messages { get; set; }
    }
}
