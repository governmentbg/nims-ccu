using Eumis.Data.Declarations.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.Users.ViewObjects
{
    public class UserUnacceptedDeclarationVO
    {
        public int DeclarationId { get; set; }

        public string NameBg { get; set; }

        public string NameEn { get; set; }

        public string ContentBg { get; set; }

        public string ContentEn { get; set; }

        public ICollection<DeclarationFileVO> DeclarationFiles { get; set; }
    }
}
