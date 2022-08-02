﻿using System.Text;

namespace Eumis.Public.Common
{
    /// <summary>
    /// Настройки на сериализатора на документи.
    /// </summary>
    public class DocumentSerializerSettings
    {
        /// <summary>
        /// Тип на кодирането на стринговете.
        /// </summary>
        public static Encoding DefaultEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
