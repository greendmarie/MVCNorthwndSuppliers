namespace MVCNorthwndCRUD.Custom
{

    using System.Web;
    using System.Web.Mvc;

    public class SecurityFilter  : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            base.OnActionExecuting(filterContext);

        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}