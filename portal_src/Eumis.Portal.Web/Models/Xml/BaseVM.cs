using System.Web.Mvc;
using System.Linq;
using Eumis.Common.Validation;
using Eumis.Components.Web;
using System.Collections.Generic;
using Eumis.Documents.Validation;
using Eumis.Portal.Web.Helpers;

namespace Eumis.Portal.Web.Models
{
    public abstract class BaseVM : IEngineValidatable, ILocalValidatable
    {
        public AppStep Step { get; set; }
        public AppStep CurrentStep { get; set; }
        public List<ModelValidationResultExtended> LocalValidationErrors { get; set; }

        #region IEngineValidatable Members

        /// <summary>
        /// Извлича масив от валидиращи се обекти
        /// </summary>
        /// <returns>масив от валидиращи се обекти</returns>
        public virtual IEnumerable<ValidatableObject> GetValidatableObjects()
        {
            return new List<ValidatableObject>()
            {
                new ValidatableObject
                {
                    Object = AppContext.Current.Document,
                },
            };
        }

        public static bool IsModelFieldValid(ModelStateDictionary modelState, string inputName)
        {
            return !(modelState.Keys.Contains(inputName)
                    && modelState[inputName].Errors.Any());
        }

        #endregion

        #region Angular Validation

        public virtual void Validate(ModelStateDictionary modelState)
        {
            List<string> validationProperties = new List<string>();

            GetAngularValidatoinProperties(string.Empty, this, validationProperties);

            foreach (var propertyName in validationProperties)
            {
                if (modelState.Keys.Contains(propertyName))
                {
                    if (modelState[propertyName].Errors.Any())
                    {
                        var isValidProperty = UpdateToIsValidPattern(propertyName);
                        SetAngularValidatoin(this, isValidProperty, true);
                    }
                }
            }
        }

        public object SetAngularValidatoin(object model, string validationPropertyName, object isValid)
        {
            if (model == null)
                return null;

            object obj = model, parentObj = model;

            // Split property name to parts (propertyName could be hierarchical, like obj.subobj.subobj.property
            string[] propertyNameParts = validationPropertyName.Split('.');
            System.Reflection.PropertyInfo pi = null;

            foreach (string propertyNamePart in propertyNameParts)
            {
                if (obj == null) return null;

                // propertyNamePart could contain reference to specific 
                // element (by index) inside a collection
                if (!propertyNamePart.Contains("["))
                {
                    pi = obj.GetType().GetProperty(propertyNamePart);
                    if (pi == null) return null;
                    parentObj = obj;
                    obj = pi.GetValue(obj, null);
                }
                else
                {   // propertyNamePart is areference to specific element 
                    // (by index) inside a collection
                    // like AggregatedCollection[123]
                    //   get collection name and element index
                    int indexStart = propertyNamePart.IndexOf("[") + 1;
                    string collectionPropertyName = propertyNamePart.Substring(0, indexStart - 1);
                    int collectionElementIndex = System.Int32.Parse(propertyNamePart.Substring(indexStart, propertyNamePart.Length - indexStart - 1));
                    //   get collection object
                    pi = obj.GetType().GetProperty(collectionPropertyName);
                    if (pi == null) return null;
                    object unknownCollection = pi.GetValue(obj, null);
                    //   try to process the collection as array
                    if (unknownCollection.GetType().IsArray)
                    {
                        object[] collectionAsArray = unknownCollection as System.Array[];
                        obj = collectionAsArray[collectionElementIndex];
                    }
                    else
                    {
                        //   try to process the collection as IList
                        System.Collections.IList collectionAsList = unknownCollection as System.Collections.IList;
                        if (collectionAsList != null)
                        {
                            obj = collectionAsList[collectionElementIndex];
                        }
                        else
                        {
                            // ??? Unsupported collection type
                        }
                    }
                }
            }

            if (obj != null)
            {
                pi.SetValue(parentObj, isValid, null);
            }

            return obj;
        }

        private void GetAngularValidatoinProperties(string root, object model, List<string> validationProperties)
        {
            if (model == null) return;
            if (!string.IsNullOrEmpty(root) && !root.EndsWith(".")) { root += "."; }
            System.Type objType = model.GetType();
            System.Reflection.PropertyInfo[] properties = objType.GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                object propValue = property.GetValue(model, null);
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                {
                    validationProperties.Add(root + property.Name);
                }
                else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    validationProperties.Add(root + property.Name);
                    System.Collections.IEnumerable enumerable = (System.Collections.IEnumerable)propValue;
                    var enumerable_counter = 0;
                    if (enumerable != null)
                    {
                        foreach (object child in enumerable)
                        {
                            GetAngularValidatoinProperties(root + property.Name + "[" + enumerable_counter++ + "]", child, validationProperties);
                        }
                    }
                }
                else
                {
                    validationProperties.Add(root + property.Name);
                    if (!(propValue is R_10000.PrivateNomenclature || propValue is R_10001.PublicNomenclature))
                    {
                        GetAngularValidatoinProperties(root + property.Name, propValue, validationProperties);    
                    }
                }
            }
        }

        private string UpdateToIsValidPattern(string propertyName)
        {
            var parts = propertyName.Split('.');
            if (parts.Count() > 1)
            {
                parts[parts.Count() - 1] = "Is" + parts.Last() + "Valid";
            }

            return parts.Aggregate((f, n) => f + "." + n);
        }

        protected bool IsNonModelFieldValid(ModelStateDictionary modelState, string inputName)
        {
            return !(modelState.Keys.Contains(inputName) && modelState[inputName].Errors.Any());
        }

        #endregion
    }
}