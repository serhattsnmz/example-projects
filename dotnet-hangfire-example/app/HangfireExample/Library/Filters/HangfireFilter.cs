using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireExample.Library.Filters
{
    public class HangfireFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var result = httpContext.Request.Cookies.TryGetValue("hangfire-key", out string value);
            return (result == true && value == "123");
        }
    }
}
