namespace Eumis.Domain
{
    public class DomainObjectNotFoundException : DomainException
    {
        public DomainObjectNotFoundException(string message)
            : base(message)
        {
        }
    }
}
