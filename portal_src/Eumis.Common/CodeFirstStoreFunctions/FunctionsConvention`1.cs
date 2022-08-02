// Copyright (c) Pawel Kadluczka, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Data.Entity;
namespace Eumis.Common.CodeFirstStoreFunctions
{
    public class FunctionsConvention<T> : FunctionsConvention
        where T : DbContext
    {
        public FunctionsConvention(string defaultSchema)
            : base(defaultSchema, typeof(T))
        {
        }
    }
}