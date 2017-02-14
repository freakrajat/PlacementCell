using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Newtonsoft.Json;
using Microsoft.AspNet.Mvc.Facebook;
using Facebook;
using System.Web.Security;

namespace PlacementCell.Controllers
{
    public class oAuthController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallBack");
                return uriBuilder.Uri;

            }
        }
        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1026334167472105",
                client_secret = "d887bcb015bd10428ad9724841ad6e8e",
                redirect_uri = RedirectUri.AbsoluteUri,
                responce_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallBack(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
                {
                    client_id = "1026334167472105",
                    client_secret = "d887bcb015bd10428ad9724841ad6e8e",
                    redirect_uri = RedirectUri.AbsoluteUri,
                    code = code
                });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email");
            string name = me.first_name;
            string email = me.email;
            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("GetAllAvailableVacancies", "Home");

        }

    }
}