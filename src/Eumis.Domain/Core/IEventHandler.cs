namespace Eumis.Domain.Core
{
    public interface IEventHandler
    {
        void Handle(IDomainEvent e);
    }
}
