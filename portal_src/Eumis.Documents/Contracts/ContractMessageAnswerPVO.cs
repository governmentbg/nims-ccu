using Eumis.Common.Localization;
using System;
using System.ComponentModel;

namespace Eumis.Documents.Contracts
{
    public enum RegMessageAnswerType
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Отговор финализиран")]
        AnswerFinalized = 2,

        [Description("Отговор")]
        Answer = 3,

        [Description("Изпратен отговор на хартия")]
        PaperAnswer = 4,

        [Description("Оттеглен")]
        Canceled = 5,
    }

    public class ContractMessageAnswerPVO
    {
        public ContractMessageAnswerPVO()
        {
        }

        public Guid answerGid { get; set; }

        public int orderNum { get; set; }

        public DateTime? answerDate { get; set; }

        public DateTime? readDate { get; set; }

        public RegMessageAnswerType status { get; set; }

        public string statusText { get; set; }

        public string statusTextAlt { get; set; }

        public string displayStatusText
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.statusText, this.statusTextAlt);
            }
        }

        public string hash { get; set; }
    }
}
