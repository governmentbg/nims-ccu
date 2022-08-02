namespace Eumis.Database.Configurator
{
    public class ScriptData
    {
        public ScriptData(string fileName, string content)
        {
            this.FileName = fileName;
            this.Content = content;
        }

        public string FileName { get; private set; }

        public string Content { get; private set; }
    }
}
