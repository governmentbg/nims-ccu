using System;

namespace Eumis.Domain.Measures
{
    public partial class Measure
    {
        public void UpdateMeasure(string shortName, string name, string nameAlt)
        {
            this.ShortName = shortName;
            this.Name = name;
            this.NameAlt = nameAlt;

            this.ModifyDate = DateTime.Now;
        }
    }
}
