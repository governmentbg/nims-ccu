using Eumis.Common.Db;
using System.Data.Entity;

namespace Eumis.Domain.Messages
{
    public class MessageModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new MessageRecipientMap());
            modelBuilder.Configurations.Add(new MessageFileMap());
        }
    }
}
