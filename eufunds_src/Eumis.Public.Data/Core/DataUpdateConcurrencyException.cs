namespace Eumis.Public.Data.Core
{
    public class DataUpdateConcurrencyException : DataException
    {
        public DataUpdateConcurrencyException()
            : base("Entity already modified")
        {
        }
    }
}
