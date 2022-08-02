namespace Eumis.Data.OperationalMap.Programmes.ViewObjects
{
    public class ProgrammeInstitutionsVO
    {
        public int ProgrammeId { get; set; }

        public int? InstitutionId { get; set; }

        public string InstitutionName { get; set; }

        public string InstitutionTypeName { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }
    }
}
