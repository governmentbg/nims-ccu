using System;

namespace Eumis.Components
{
    /// <summary>
    /// Временна имплементация на компомпонента за валидиране на xml от xsd схеми
    /// </summary>
    public class FakeXmlSchemaValidator : IXmlSchemaValidator
    {
        /// <summary>
        /// 
        /// </summary>
        public string ContentDirectory
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public bool Validate(string xml, string schemasDirecotryPath)
        {
            return true;
        }
    }
}
