using System;

namespace Eumis.Data.Messages.ViewObjects
{
    public class IngoingMessageVO
    {
        public int MessageId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Sender { get; set; }

        public DateTime? SentDate { get; set; }

        public DateTime? RecieveDate { get; set; }
    }
}
