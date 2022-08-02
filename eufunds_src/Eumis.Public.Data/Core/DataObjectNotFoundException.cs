﻿using System;

namespace Eumis.Public.Data.Core
{
    public class DataObjectNotFoundException : DataException
    {
        public DataObjectNotFoundException(string entitySet, int id)
            : base("Cannot find entity from set " + entitySet + " with id " + id)
        {
        }

        public DataObjectNotFoundException(string entitySet, Guid gid)
            : base("Cannot find entity from set " + entitySet + " with gid " + gid.ToString())
        {
        }

        public DataObjectNotFoundException(string entitySet, string lookup)
            : base("Cannot find entity from set " + entitySet + " looked up by " + lookup)
        {
        }
    }
}
