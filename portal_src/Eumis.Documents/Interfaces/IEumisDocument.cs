using System;
namespace Eumis.Documents.Interfaces
{
    public interface IEumisDocument
    {
        string Id { get; set; }
        DateTime ModificationDate { get; set; }
        DateTime CreateDate { get; set; }
    }
}
