namespace Eumis.Domain.Projects.DataObjects
{
    public class AutomaticProjectVersionXmlDO
    {
        public AutomaticProjectVersionXmlDO()
        {
        }

        public AutomaticProjectVersionXmlDO(
            string projectRegNumber,
            decimal? amount)
        {
            this.ProjectRegNumber = projectRegNumber;
            this.Amount = amount;
        }

        public string ProjectRegNumber { get; set; }

        public decimal? Amount { get; set; }
    }
}
