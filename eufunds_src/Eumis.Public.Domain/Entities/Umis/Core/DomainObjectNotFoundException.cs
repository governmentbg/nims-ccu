namespace Eumis.Public.Domain.Entities.Umis
{
    public class DomainObjectNotFoundException : DomainException
    {
        public DomainObjectNotFoundException(string message)
            : base(message)
        {
        }
    }
}
