using System;
using System.Collections.Generic;
using Eumis.Public.Common.Crypto;
using Eumis.Public.Data.UmisVOs;

namespace Eumis.Public.Web.Models.EvalSessionResult
{
    public class EvalSessionResultSearchVM
    {
        public string ProgrammeId { get; set; }

        public string ProcedureId { get; set; }

        public string ResultType { get; set; }

        public bool ShowRes { get; set; }

        public List<string> Errors { get; set; }

        public IList<EvalSessionResultVO> SearchResults { get; set; }

        public static void EncryptProperties(EvalSessionResultSearchVM vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException(nameof(vm));
            }

            vm.ProgrammeId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProgrammeId);
            vm.ProcedureId = ConfigurationBasedStringEncrypter.Encrypt(vm.ProcedureId);
            vm.ResultType = ConfigurationBasedStringEncrypter.Encrypt(vm.ResultType);
        }
    }
}
