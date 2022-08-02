namespace Eumis.Public.Domain.Entities.Umis.Core
{
    public interface IEventHandler
    {
        void Handle(IDomainEvent e);
    }
}
