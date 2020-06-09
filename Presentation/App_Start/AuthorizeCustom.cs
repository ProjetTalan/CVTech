using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Application.Models;
using Domaine.Entities;
using Newtonsoft.Json;

namespace Presentation
{
	public class AuthorizeCustom : AuthorizeAttribute
	{
		public AuthorizeCustom(params string[] roles) : base()
		{
			Roles = string.Join(",", roles);
		}
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (HttpContext.Current.Request.Cookies[".ASPXAUTH"] != null)
			{
				FormsAuthenticationTicket ticket =
					FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[".ASPXAUTH"].Value);


				var role = ticket != null ? JsonConvert.DeserializeObject<ProfileModel>(ticket.UserData).Role.ToString() : Role.Consultant.ToString();

				if (string.IsNullOrEmpty(Roles) || Roles.Contains(role))
				{
					return true;
				}

				return false;
			}

			httpContext.Response.Redirect("~/Login/");
			return false;
		}
	}
}