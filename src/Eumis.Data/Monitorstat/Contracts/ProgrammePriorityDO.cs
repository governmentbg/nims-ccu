using System;

namespace Eumis.Data.Monitorstat.Contracts
{
    public class ProgrammePriorityDO
    {
        private string definition;
        private string targets;

        public Guid ProgrammeIdentifier { get; set; }

        public string ProgrammeCode { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string SpecificTargets
        {
            get => string.IsNullOrEmpty(this.targets) || this.targets.Length < 3 ? "Липсва" : this.targets;
            set => this.targets = value;
        }

        public string Definition
        {
            get => string.IsNullOrEmpty(this.definition) || this.definition.Length < 3 ? "Липсва" : this.definition;
            set => this.definition = value;
        }
    }
}
