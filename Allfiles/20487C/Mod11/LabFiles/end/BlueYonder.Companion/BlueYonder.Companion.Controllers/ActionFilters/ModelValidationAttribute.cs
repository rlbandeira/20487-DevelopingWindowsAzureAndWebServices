using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlueYonder.Companion.Controllers.ActionFilters
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting
    (System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
                actionContext.Response = actionContext.Request.CreateErrorResponse
  (HttpStatusCode.BadRequest, new HttpError(actionContext.ModelState, true));
        }
    }
}
