using System.Web;
using System.Web.Mvc;

namespace Presentation
{
	public class AuthorizeCustom : AuthorizeAttribute
	{
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (HttpContext.Current.Request.Cookies[".ASPXAUTH"] != null)
			{
				return true;
			}

			httpContext.Response.Redirect("~/Login/");
			return false;
		}
	}
}