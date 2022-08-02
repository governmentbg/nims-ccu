using Eumis.Domain.NonAggregates;
using Newtonsoft.Json.Linq;
using System;

namespace Eumis.Domain.Emails
{
    public partial class Email : IAggregateRoot
    {
        public void SetStatus(EmailStatus status)
        {
            this.Status = status;
            this.ModifyDate = DateTime.Now;
        }

        public void IncrementFailedAttempts(string exception)
        {
            JObject fae;
            if (string.IsNullOrEmpty(this.FailedAttemptsErrors))
            {
                fae = new JObject();
            }
            else
            {
                fae = JObject.Parse(this.FailedAttemptsErrors);
            }

            fae.Add(this.FailedAttempts.ToString(), exception);
            this.FailedAttemptsErrors = fae.ToString();
            this.FailedAttempts++;
            this.ModifyDate = DateTime.Now;
        }
    }
}
