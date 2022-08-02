namespace Eumis.Portal.Web.Models
{
    public interface IEditVM<TDocument>
    {
        TDocument Set(TDocument document);
    }
}