using System;
using System.Collections.Generic;

namespace Eumis.Data.Messages.ViewObjects
{
    public class OutgoingMessageVO
    {
        public int MessageId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? SentDate { get; set; }

        public IList<string> Recipients { get; set; }
    }
}
