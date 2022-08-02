using Eumis.Documents.Contracts;

namespace Eumis.Components.Communicators
{
    public class NewsCommunicator : INewsCommunicator
    {
        NewsPVO INewsCommunicator.GetNews(int offset, int? limit)
        {
            return NewsApi.GetNews(offset, limit).ToObject<NewsPVO>();
        }

        NewsPVO INewsCommunicator.GetAllNews(int offset, int? limit)
        {
            return NewsApi.GetAllNews(offset, limit).ToObject<NewsPVO>();
        }

        News INewsCommunicator.GetNewsInfo(int id)
        {
            return NewsApi.GetNewsInfo(id).ToObject<News>();
        }
    }
}
