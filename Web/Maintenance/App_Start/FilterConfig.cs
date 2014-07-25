using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

namespace Burgerama.Web.Maintenance
{
    public sealed class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Contract.Requires<ArgumentException>(filters != null);

            filters.Add(new HandleErrorAttribute());
        }
    }
}
