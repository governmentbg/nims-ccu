using Eumis.Documents.Contracts;

namespace Eumis.Components.Communicators
{
    public interface INewsCommunicator
    {
        NewsPVO GetNews(int offset = 0, int? limit = null);

        NewsPVO GetAllNews(int offset = 0, int? limit = null);

        News GetNewsInfo(int id);
    }
}
