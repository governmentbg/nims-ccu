using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eumis.Documents.Mappers;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class DocumentsNomenclature
    {
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public bool IsOriginal { get; set; }
        public bool IsSignatureRequired { get; set; }

        public static readonly DocumentsNomenclature El1 = new DocumentsNomenclature { Name = "Елемент 1", NameAlt = "Елемент 1 Alt", Value = "1", IsRequired = true, IsOriginal = false, IsSignatureRequired = true };
        public static readonly DocumentsNomenclature El2 = new DocumentsNomenclature { Name = "Елемент 2", NameAlt = "Елемент 2 Alt", Value = "2", IsRequired = true, IsOriginal = false, IsSignatureRequired = true };
        public static readonly DocumentsNomenclature El3 = new DocumentsNomenclature { Name = "Елемент 3", NameAlt = "Елемент 3 Alt", Value = "3", IsRequired = true, IsOriginal = false, IsSignatureRequired = true };
        public static readonly DocumentsNomenclature El4 = new DocumentsNomenclature { Name = "Елемент 4", NameAlt = "Елемент 4 Alt", Value = "4", IsRequired = false, IsOriginal = false, IsSignatureRequired = true };
        public static readonly DocumentsNomenclature El5 = new DocumentsNomenclature { Name = "Елемент 5", NameAlt = "Елемент 5 Alt", Value = "5", IsRequired = false, IsOriginal = false, IsSignatureRequired = true };
        public static readonly DocumentsNomenclature El6 = new DocumentsNomenclature { Name = "Елемент 6", NameAlt = "Елемент 6 Alt", Value = "6", IsRequired = true, IsOriginal = true, IsSignatureRequired = true };
        public static readonly DocumentsNomenclature El7 = new DocumentsNomenclature { Name = "Елемент 7", NameAlt = "Елемент 7 Alt", Value = "7", IsRequired = true, IsOriginal = true, IsSignatureRequired = true };
        public static readonly DocumentsNomenclature El8 = new DocumentsNomenclature { Name = "Елемент 8", NameAlt = "Елемент 8 Alt", Value = "8", IsRequired = true, IsOriginal = true, IsSignatureRequired = true };
        public static readonly DocumentsNomenclature El9 = new DocumentsNomenclature { Name = "Елемент 9", NameAlt = "Елемент 9 Alt", Value = "9", IsRequired = false, IsOriginal = true, IsSignatureRequired = true };
        public static readonly DocumentsNomenclature El10 = new DocumentsNomenclature { Name = "Елемент 10", Value = "10", NameAlt = "Елемент 10 Alt", IsRequired = false, IsOriginal = true, IsSignatureRequired = true };

        public List<DocumentsNomenclature> GetItems()
        {
            return new List<DocumentsNomenclature>() {
                El1,
                El2,
                El3,
                El4,
                El5,
                El6,
                El7,
                El8,
                El9,
                El10
            };
        }
    }
}
