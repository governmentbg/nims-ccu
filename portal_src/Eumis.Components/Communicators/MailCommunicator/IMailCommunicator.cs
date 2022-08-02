using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public interface IMailCommunicator
    {
        //[RoutePrefix("api/emails/send")]
        //[AllowAnonymous]
        void Send(string recipient, string subject, string name, string email, string messageText);
    }
}