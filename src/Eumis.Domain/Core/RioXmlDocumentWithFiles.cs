using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Core
{
    public abstract class RioXmlDocumentWithFiles<TDoc, TFile> : RioXmlDocument<TDoc>
        where TDoc : class
        where TFile : RioXmlFile
    {
        public RioXmlDocumentWithFiles()
        {
            this.Files = new List<TFile>();
        }

        public abstract IList<TFile> XmlFiles { get; }

        public ICollection<TFile> Files { get; set; }

        public override void SetXml(string xml)
        {
            base.SetXml(xml);

            var oldFiles = this.Files.ToList();
            foreach (var oldFile in oldFiles)
            {
                this.Files.Remove(oldFile);
            }

            foreach (var file in this.XmlFiles)
            {
                this.Files.Add(file);
            }
        }
    }
}
