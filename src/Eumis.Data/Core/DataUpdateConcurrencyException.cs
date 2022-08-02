namespace Eumis.Data
{
    public class DataUpdateConcurrencyException : DataException
    {
        public DataUpdateConcurrencyException()
            : base("Entity already modified")
        {
        }
    }
}
