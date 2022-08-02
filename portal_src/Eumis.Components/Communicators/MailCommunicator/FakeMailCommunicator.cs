using Eumis.Documents.Contracts;
namespace Eumis.Components.Communicators
{
    public class FakeMailCommunicator : IMailCommunicator
    {
        public void Send(string recipient, string subject, string name, string email, string messageText)
        {
        
        }
    }
}