namespace Eumis.Public.Web.Models.Home
{
    public class NomenclatureModel
    {
        public NomenclatureModel(int id, string text)
        {
            this.Id = id;
            this.Text = text;
        }

        public int Id { get; set; }

        public string Text { get; set; }
    }
}