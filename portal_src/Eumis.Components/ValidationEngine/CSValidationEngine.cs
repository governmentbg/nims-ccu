using Eumis.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Interfaces;
using Eumis.Common;

namespace Eumis.Components.ValidationEngine
{
    /// <summary>
    /// C# валидиращ енджин
    /// </summary>
    public class CSValidationEngine : IValidationEngine, ICSValidationEngine
    {
        #region Public

        static CSValidationEngine()
        {
            _sharedValidators = new Dictionary<string, ICSValidator>();
            _specificValidators = new Dictionary<string, IDictionary<string, ICSValidator>>();

            // applications
            RegisterSpecificValidator("R10019", new Eumis.Documents.Validation.Eumis.Project());
            RegisterSharedValidator(new Eumis.Documents.Validation.Eumis.Project()); // embedded in message
            RegisterSpecificValidator("R10020", new Eumis.Documents.Validation.Eumis.Message());
            RegisterSpecificValidator("R10023", new Eumis.Documents.Validation.Eumis.EvalTable());
            RegisterSpecificValidator("R10026", new Eumis.Documents.Validation.Eumis.EvalSheet());
            RegisterSpecificValidator("R10027", new Eumis.Documents.Validation.Eumis.Standpoint());
            RegisterSpecificValidator("R10040", new Eumis.Documents.Validation.Eumis.BFPContract());
            RegisterSpecificValidator("R10041", new Eumis.Documents.Validation.Eumis.Procurements());
            RegisterSpecificValidator("R10042", new Eumis.Documents.Validation.Eumis.Communication());
            RegisterSpecificValidator("R10043", new Eumis.Documents.Validation.Eumis.FinanceReport());
            RegisterSpecificValidator("R10044", new Eumis.Documents.Validation.Eumis.TechnicalReport());
            RegisterSpecificValidator("R10045", new Eumis.Documents.Validation.Eumis.PaymentRequest());
            RegisterSpecificValidator("R10077", new Eumis.Documents.Validation.Eumis.SpendingPlan());
            RegisterSpecificValidator("R10080", new Eumis.Documents.Validation.Eumis.Offer());

            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.Address());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.AttachedDocument());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.BFPContractBasicData());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.BFPContractContractTeam());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.BFPContractDirectionsBudgetContract());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.BFPContractIndicators());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.Company());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ContractActivity());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ContractTeam());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.DirectionsBudgetContract());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.Indicator());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.InterventionCategoryDimensions());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.NutsAddress());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.PaperAttachedDocument());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.PaymentRequestBasicData());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.PaymentRequestDeclaration());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.PrivateNomenclature());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.PreliminaryContract());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.PreliminaryContractActivity());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProcurementPlan());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProgrammeBasicData());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProgrammeBudget());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProgrammeContractActivities());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProgrammeDetailsExpenseBudget());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProgrammeExpenseBudget());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProgrammeIndicators());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProjectBasicData());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProjectErrand());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProjectPayPlan());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.ProjectSpecField());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.PublicNomenclature());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.TechnicalReportActivity());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.TechnicalReportBasicData());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.TechnicalReportIndicator());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.TechnicalReportTeamMember());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.FinanceReportBasicData());
            RegisterSharedValidator(new Eumis.Documents.Validation.Shared.Direction());

            //RegisterSharedValidator(new RioObjects.Validation.Shared.AttachedDocument());

            //all static members should be immutable to ensure thread safety
            MakeDictionariesReadOnly();
        }

        string ICSValidationEngine.AppRioCode { get { return this._code; } }
        object ICSValidationEngine.RioApplication { get { return this._document; } }

        /// <summary>
        /// Валидира обект представляващ заявление
        /// </summary>
        /// <param name="appRioCode">код на заявлението</param>
        /// <param name="complexType">обект</param>
        /// <param name="complexTypePath">expression път до обекта</param>
        /// <returns>списък от грешки</returns>
        /// <remarks>ВНИМАНИЕ!!! ТОЗИ МЕТОД НЕ Е ThreadSafe</remarks>
        public IDictionary<string, IEnumerable<ValidationOption>> Validate(string code, object document, object complexType, string modelPath)
        {
            _code = code;
            _document = document;
            List<ValidationOption> errors = new List<ValidationOption>();

            ((ICSValidationEngine)this).Validate(
                complexType.GetType().FullName,
                complexType,
                modelPath,
                errors);

            return
                errors
                .Select(t => ValidationOption.Create(t.ModelPath.TrimStart('.').Trim(), t.ErrorSimpleMessage, t.ErrorComplexMessage, t.IsAngularValidation, t.IsStopError, t.ErrorRowIdentifier))
                .GroupBy(t => t.ModelPath)
                .ToDictionary(
                    g => g.Key,
                    g => (IEnumerable<ValidationOption>)g.ToList());
        }

        void ICSValidationEngine.Validate(string complexTypeName, object complexType, string modelPath, IList<ValidationOption> errors)
        {
            ICSValidator validator = null;

            if (_specificValidators.ContainsKey(_code))
            {
                var applicationSpecificValidators = _specificValidators[_code];

                if (applicationSpecificValidators.ContainsKey(complexTypeName))
                {
                    validator = applicationSpecificValidators[complexTypeName];
                }
            }

            if (validator == null)
            {
                if (_sharedValidators.ContainsKey(complexTypeName))
                {
                    validator = _sharedValidators[complexTypeName];
                }
                else
                {
                    throw new Exception("Cannot find type validator : " + complexTypeName);
                }
            }

            validator.Validate(this, complexType, modelPath, errors);
        }

        private string _code;
        private object _document;

        private static IDictionary<string, ICSValidator> _sharedValidators;
        private static IDictionary<string, IDictionary<string, ICSValidator>> _specificValidators;
        
        private static void RegisterSharedValidator(ICSValidator validator)
        {
            _sharedValidators.Add(validator.TypeFullName, validator);
        }
        
        private static void RegisterSpecificValidator(string appRioCode, ICSValidator validator)
        {
            IDictionary<string, ICSValidator> applicationSpecificValidators;
        
            if (_specificValidators.ContainsKey(appRioCode))
            {
                applicationSpecificValidators = _specificValidators[appRioCode];
            }
            else
            {
                applicationSpecificValidators = new Dictionary<string, ICSValidator>();
                _specificValidators.Add(appRioCode, applicationSpecificValidators);
            }
        
            applicationSpecificValidators.Add(validator.TypeFullName, validator);
        }
        
        private static void MakeDictionariesReadOnly()
        {
            _sharedValidators = new ReadOnlyDictionary<string, ICSValidator>(_sharedValidators);
        
            foreach (var key in _specificValidators.Keys.ToList())
            {
                _specificValidators[key] = new ReadOnlyDictionary<string, ICSValidator>(_specificValidators[key]);
            }
        
            _specificValidators = new ReadOnlyDictionary<string, IDictionary<string, ICSValidator>>(_specificValidators);
        }

        #endregion
    }
}
