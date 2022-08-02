using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;
using Eumis.Documents.Enums;

namespace Eumis.Components.Communicators
{
    public class FakeEvalCommunicator : IEvalCommunicator
    {
        public ContractEvalDocument GetEvalTable(Guid gid, string token)
        {
            return new ContractEvalDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractEvalDocument PutEvalTable(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractEvalDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractEvalDocument SubmitEvalTable(Guid gid, string token, byte[] version)
        {
            return new ContractEvalDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractEvalDocument GetEvalSheet(Guid gid, string token)
        {
            return new ContractEvalDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractEvalDocument PutEvalSheet(Guid gid, string token, string xml, byte[] version)
        {
            return new ContractEvalDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractEvalDocument SubmitEvalSheet(Guid gid, string token, byte[] version)
        {
            return new ContractEvalDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }

        public ContractEvalDocument PauseEvalSheet(Guid gid, string token, byte[] version)
        {
            return new ContractEvalDocument()
            {
                xml = "xml",
                version = new byte[] { }
            };
        }
    }
}