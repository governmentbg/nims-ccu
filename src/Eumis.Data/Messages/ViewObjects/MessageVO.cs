using System;
using System.Collections.Generic;

namespace Eumis.Data.Messages.ViewObjects
{
    public class MessageVO
    {
        public int MessageId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Sender { get; set; }

        public DateTime SentDate { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsArchived { get; set; }

        public IList<string> Recipients { get; set; }

        public IList<MessageFileVO> Files { get; set; }
    }
}
