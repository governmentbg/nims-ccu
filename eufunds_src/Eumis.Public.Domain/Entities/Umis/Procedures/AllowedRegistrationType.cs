using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public enum AllowedRegistrationType
    {
        [Description("Електронно")]
        Digital = 1,

        [Description("На хартия")]
        Paper = 2,

        [Description("Електронно или На хартия")]
        DigitalOrPaper = 3
    }
}
