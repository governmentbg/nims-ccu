namespace Eumis.Public.Domain.Core
{
    public interface IEventHandler
    {
        void Handle(IDomainEvent e);
    }
}
