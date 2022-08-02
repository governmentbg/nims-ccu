using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectQuestionExpirationEmailVO
    {
        public int ProjectCommunicationId { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName { get; set; }

        public string QuestionRegNumber { get; set; }

        public string Recipient { get; set; }
    }
}
