using PlacementCell.Models;
using PlacementCell.PlacementCellDBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlacementCell.BusinessLogicClasses
{
    public class HrLoginCheck
    {
        PlacementCellDBContext db = new PlacementCellDBContext();

        public bool CheckLogin(LoginDBModel user)
        {
            bool flag = false;
            LoginDBModel sd = user;
            IList<Int32> list = (from data in db.LoginTable
                                 where data.UserName == sd.UserName
                                 && data.Password == sd.Password
                                 select data.ID).ToList();

            if (list != null && list.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
    }

    //#region Authentication filter


    //public class CustomAuthenticationFilter : AuthorizeAttribute
    //{
    //    private readonly string[] alloweduser;
    //    public CustomAuthenticationFilter(params string[] user)
    //    {
    //        this.alloweduser = user;
    //    }

    //    protected override bool AuthorizeCore(HttpContextBase httpContext)
    //    {
    //        bool authorize = false;

    //        //foreach (var u in alloweduser)
    //        //{
    //        //    var role = "Admin1";
    //        //    if (role == u)
    //        //    {
    //        //        authorize = true;
    //        //    }
    //        //}
           
    //        return authorize;
    //    }

    //    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    //    {
    //        // if user is not authenticated rediect him to the login page

    //        filterContext.Controller.TempData["Exception"] = "You do not have sufficient privileges for this operation.";
    //        //filterContext.Result = new HttpUnauthorizedResult();
    //        filterContext.Result = new RedirectToRouteResult(
    //                new RouteValueDictionary {
    //                    { "action", "Login" },
    //                    { "controller", "Account" },
    //                    { "parameterName", "" }
    //                });
    //    }


    //}

    //public class CustomAuthorizationFilter : AuthorizeAttribute
    //{
    //    public override void OnAuthorization(AuthorizationContext filterContext)
    //    {

    //        filterContext.Controller.ViewBag.AutherizationMessage = "Custom Authorization: Message from OnAuthorization method.";
    //    }
    //}

    //#endregion


}