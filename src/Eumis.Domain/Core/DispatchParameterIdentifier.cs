namespace Eumis.Domain.Core
{
    public class DispatchParameterIdentifier
    {
        private DispatchParameterIdentifier(string value)
        {
            this.Value = value;
        }

        public static DispatchParameterIdentifier Id
        {
            get
            {
                return new DispatchParameterIdentifier("id");
            }
        }

        public static DispatchParameterIdentifier Ind
        {
            get
            {
                return new DispatchParameterIdentifier("ind");
            }
        }

        public static DispatchParameterIdentifier Pid
        {
            get
            {
                return new DispatchParameterIdentifier("pid");
            }
        }

        public static DispatchParameterIdentifier Spid
        {
            get
            {
                return new DispatchParameterIdentifier("spid");
            }
        }

        public string Value { get; set; }
    }
}
