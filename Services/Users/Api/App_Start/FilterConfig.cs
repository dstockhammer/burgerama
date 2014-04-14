using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

namespace Burgerama.Services.Users.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Contract.Requires<ArgumentException>(filters != null);

            filters.Add(new HandleErrorAttribute());
        }
    }
}
