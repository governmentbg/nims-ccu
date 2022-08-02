namespace Eumis.Public.Domain.Entities.Umis
{
    public class DomainValidationException : DomainException
    {
        public DomainValidationException(string message)
            : base(message)
        {
        }
    }
}
