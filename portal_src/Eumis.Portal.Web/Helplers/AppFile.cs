using System;

namespace Eumis.Portal.Web.Helpers
{
    /// <summary>
    /// Клас, определящ структурата на приложените документи
    /// </summary>
    public class AppFile
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Име
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Съдържание
        /// </summary>
        private byte[] _content;

        public byte[] Content
        {
            get { return _content; }
            set
            {
                if(value != null)
                    Size = value.Length;

                _content = value;
            }
        }

        /// <summary>
        /// Размер
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Тип на документа
        /// </summary>
        public string MimeType { get; set; }
    }
}