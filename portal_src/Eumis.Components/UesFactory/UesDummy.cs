using System.Security.Cryptography.X509Certificates;

namespace Eumis.Components
{
    public class UesDummy : UesBase
    {
        public UesDummy(X509Certificate2 certificate, string personalIdentifier, string name)
            : base(certificate, personalIdentifier, name)
        {
        }

        protected override bool CheckIsCompany()
        {
            return false;
        }

        protected override bool CheckIsPersonal()
        {
            return true;
        }

        protected override string GetPersonalIdentifier()
        {
            return "8406123845"; 
        }

        protected override string GetName()
        {
            return "Nikolay Ivanov Petrov";
        }
    }
}
