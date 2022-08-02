using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Eumis.Common.Validation;
using Eumis.Components.Web;
using Eumis.Components.ValidationEngine;
using System.Text.RegularExpressions;
using Eumis.Portal.Web.Helpers;

namespace Eumis.Portal.Web.Providers
{
    /// <summary>
    /// Модел валидатор
    /// </summary>
    public class ValidationEngineModelValidatorProvider : ModelValidatorProvider
    {
        /// <summary>
        /// Извлича всички валидатори
        /// </summary>
        /// <param name="metadata">мета данни</param>
        /// <param name="context">контекст</param>
        /// <returns>масив от валидатори</returns>
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException("metadata");
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return GetValidatorsImpl(metadata, context);
        }

        /// <summary>
        /// Извлича модел валидатори
        /// </summary>
        /// <param name="metadata">мета данни</param>
        /// <param name="context">контекст</param>
        /// <returns>масив от модел валидатори</returns>
        private static IEnumerable<ModelValidator> GetValidatorsImpl(ModelMetadata metadata, ControllerContext context)
        {
            if (TypeImplementsIValidatable(metadata.ModelType))
            {
                yield return new ValidationEngineClassModelValidator(metadata, context);
            }
        }

        /// <summary>
        /// Проверява за тип на валидатор
        /// </summary>
        /// <param name="type">тип</param>
        /// <returns>булева стойност</returns>
        private static bool TypeImplementsIValidatable(Type type)
        {
            return typeof(IEngineValidatable).IsAssignableFrom(type);
        }

        /// <summary>
        /// Валидатор
        /// </summary>
        internal sealed class ValidationEngineClassModelValidator : ModelValidator
        {
            public ValidationEngineClassModelValidator(ModelMetadata metadata, ControllerContext controllerContext)
                : base(metadata, controllerContext)
            {
            }

            /// <summary>
            /// Валидатор
            /// </summary>
            /// <param name="container"></param>
            /// <returns>масив от валидирани стойности</returns>
            public override IEnumerable<ModelValidationResult> Validate(object container)
            {
                throw new NotImplementedException();

                //List<ModelValidationResultExtended> result = new List<ModelValidationResultExtended>();
                //
                //if (AppContext.Current != null && Metadata.Model is IEngineValidatable)
                //{
                //    IValidationEngine validationEngine = DependencyResolver.Current.GetService<IValidationEngine>();
                //
                //    IEnumerable<ValidatableObject> validatableObjects = ((IEngineValidatable)Metadata.Model).GetValidatableObjects();
                //
                //    foreach (ValidatableObject vo in validatableObjects)
                //    {
                //        if (vo.Object == null)
                //            continue;
                //
                //        IDictionary<string, IEnumerable<ValidationOption>> errors = validationEngine.Validate(AppContext.Current.Code, AppContext.Current.Document, vo.Object, string.Empty);
                //
                //        foreach (var error in errors)
                //        {
                //            string modelPath = error.Key;
                //
                //            if (vo.ErrorFilters != null)
                //            {
                //                string matchedModelPath = null;
                //                foreach (var filter in vo.ErrorFilters)
                //                {
                //                    Match match = Regex.Match(modelPath, filter.FilterRegex, RegexOptions.Singleline);
                //
                //                    if (match.Success)
                //                    {
                //                        if (filter.PathFormat != null)
                //                        {
                //                            object[] groupValues = match.Groups.Cast<Group>().Skip(1).Select(g => (object)g.Value).ToArray();
                //
                //                            if (filter.SplitEnd)
                //                            {
                //                                for (int i = 0; i < groupValues.Count(); ++i)
                //                                {
                //                                    if (!string.IsNullOrEmpty(groupValues[i].ToString()) && groupValues[0].ToString().Contains("."))
                //                                        groupValues[i] = groupValues[i].ToString().Substring(0, groupValues[i].ToString().LastIndexOf("."));
                //                                }
                //                            }
                //
                //                            matchedModelPath = string.Format(filter.PathFormat, groupValues);
                //                        }
                //                        else
                //                        {
                //                            matchedModelPath = modelPath;
                //                        }
                //
                //                        //found the first match, break
                //                        break;
                //                    }
                //                }
                //
                //                if (matchedModelPath != null)
                //                {
                //                    modelPath = matchedModelPath;
                //                }
                //                else
                //                {
                //                    //no matches found, skip the error
                //                    continue;
                //                }
                //            }
                //
                //            foreach (var errorValue in error.Value)
                //            {
                //                result.Add(new ModelValidationResultExtended()
                //                    {
                //                        MemberName = modelPath,
                //                        Message = errorValue.ErrorComplexMessage,
                //                        ErrorSimpleMessage = errorValue.ErrorSimpleMessage,
                //                        ErrorComplexMessage = errorValue.ErrorComplexMessage,
                //                        IsStopError = errorValue.IsStopError
                //                    });
                //            }
                //        }
                //    }
                //}
                //
                //return result;
            }
        }
    }
}