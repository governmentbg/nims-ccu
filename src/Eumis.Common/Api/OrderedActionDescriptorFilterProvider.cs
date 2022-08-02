using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Eumis.Common.Api
{
    public class OrderedActionDescriptorFilterProvider : IFilterProvider
    {
        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (actionDescriptor == null)
            {
                throw new ArgumentNullException(nameof(actionDescriptor));
            }

            IEnumerable<FilterInfo> controllerFilters = actionDescriptor.ControllerDescriptor.GetFilters().Select(instance => new FilterInfo(instance, FilterScope.Controller));
            IEnumerable<FilterInfo> actionFilters = actionDescriptor.GetFilters().Select(instance => new FilterInfo(instance, FilterScope.Action));

            var transactionFilters = actionFilters.Where(t => t.Instance is TransactionAttribute);
            if (transactionFilters.Count() > 1)
            {
                throw new Exception($"The should be at most one {nameof(TransactionAttribute)}");
            }

            var pessimisticLockFilters = actionFilters.Where(t => t.Instance is PessimisticLockAttribute);
            if (pessimisticLockFilters.Count() > 1)
            {
                throw new Exception($"The should be at most one {nameof(PessimisticLockAttribute)}");
            }

            // TODO: reconsider whether the TransactionAttribute should be after
            // the rest of the actionFilters, we are keeping it like that so that
            // we don't change the existing behavior
            return controllerFilters
                .Concat(actionFilters.Where(t => !(t.Instance is TransactionAttribute || t.Instance is PessimisticLockAttribute)))
                .Concat(transactionFilters)
                .Concat(pessimisticLockFilters);
        }
    }
}