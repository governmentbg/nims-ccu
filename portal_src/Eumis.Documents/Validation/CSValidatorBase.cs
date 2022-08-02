using Eumis.Common.Validation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Eumis.Documents.Validation
{
    public abstract class CSValidatorBase<T> : ICSValidator
    {
        public string TypeFullName
        {
            get 
            {
                return typeof(T).FullName;
            }
        }

        public void Validate(ICSValidationEngine csValidationEngine, object complexType, string modelPath, IList<ValidationOption> errors)
        {
            Validate(csValidationEngine, (T)complexType, modelPath, errors);
        }

        protected abstract void Validate(ICSValidationEngine csValidationEngine, T complexType, string modelPath, IList<ValidationOption> errors);

        protected void EngineValidate<TProperty>(ICSValidationEngine csValidationEngine, TProperty propertyComplexType, string modelPath, IList<ValidationOption> errors)
        {
            csValidationEngine.Validate(
                typeof(TProperty).FullName,
                propertyComplexType,
                modelPath,
                errors);
        }

        protected string GetResourceKey(string appRioCode , string resourceKey)
        {
            Assembly assembly = Assembly.Load("Eumis.Common");

            //Type resource = Type.GetType("Eumis.Common.Validation.Resources." + appRioCode);
            Type resource = assembly.DefinedTypes.FirstOrDefault(t => t.FullName == ("Eumis.Common.Validation.Resources." + appRioCode));

            if (resource != null)
            {
                PropertyInfo pi = resource.GetProperty("ResourceManager");

                MethodInfo mi = pi.PropertyType.GetMethod("GetString", new Type[] { typeof(string) });

                return (string)mi.Invoke(pi.GetValue(resource), new object[] { resourceKey.TrimStart('.').Replace('.', '_') });
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
