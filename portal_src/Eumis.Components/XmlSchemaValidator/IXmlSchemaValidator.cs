namespace Eumis.Components
{
    /// <summary>
    /// Интерфейс на компонента извършващ валидацията на XML според XML схемите на РИО
    /// </summary>
    public interface IXmlSchemaValidator
    {
        /// <summary>
        /// Директория в която са записани схемите
        /// </summary>
        //string ContentDirectory { get; set; }

        /// <summary>
        /// Валидира xml според схемите на РИО
        /// </summary>
        /// <param name="xml">съдържание на xml</param>
        /// <returns>true ако xml-а е валиден, иначе false</returns>
        bool Validate(string xml, string schemasDirecotryPath);
    }
}
