namespace Eumis.Web.Api.Core
{
    public class InternalFileVO
    {
        public InternalFileVO()
        {
        }

        public InternalFileVO(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
