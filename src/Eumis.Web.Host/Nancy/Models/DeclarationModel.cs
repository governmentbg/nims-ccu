using Eumis.Common.Localization;
using Eumis.Data.Declarations.ViewObjects;
using Eumis.Data.Users.ViewObjects;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Host.Nancy.Models
{
    public class DeclarationModel : LayoutModel
    {
        public DeclarationModel(UserUnacceptedDeclarationVO model, string accessToken)
        {
            this.DeclarationId = model.DeclarationId;
            this.NameBg = model.NameBg;
            this.NameEn = model.NameEn;
            this.ContentBg = model.ContentBg;
            this.ContentEn = model.ContentEn;
            this.AccessToken = accessToken;
            this.Files = model.DeclarationFiles.Select(df => new DeclarationFileModel()
            {
                Name = df.Name,
                Description = df.Description,
                BlobKey = df.Key,
            })
            .ToList();
        }

        public string Name
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.NameBg, this.NameEn);
            }
        }

        public int DeclarationId { get; set; }

        public string NameBg { get; set; }

        public string NameEn { get; set; }

        public string Content
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.ContentBg, this.ContentEn);
            }
        }

        public string ContentBg { get; set; }

        public string ContentEn { get; set; }

        public string AccessToken { get; set; }

        public ICollection<DeclarationFileModel> Files { get; set; }
    }
}
