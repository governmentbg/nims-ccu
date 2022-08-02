using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eumis.Portal.Web.Helpers
{
    public enum MenuItems
    {
        Home = 1,
        Procedure = 2,
        Document = 3,
        Project = 4,
        Profile = 5,
        Feedback = 6,
        Help = 7,
        Submit = 8,
        Registered = 9,
        Contract = 10,
        Offers = 11,

        Other = 100
    }

    public static partial class Utils
    {
        public static MenuItems GetCurrentMenuItem(string actionName, string controllerName, bool isAuthenticated)
        {
            if (controllerName.Equals(MVC.Default.Name) && actionName.Equals(MVC.Default.ActionNames.Index))
                return MenuItems.Home;

            if (controllerName.Equals(MVC.Procedure.Name) && !actionName.Equals(MVC.Procedure.ActionNames.PublicPreview))
                return MenuItems.Procedure;

            if (controllerName.Equals(MVC.Procedure.Name) && actionName.Equals(MVC.Procedure.ActionNames.PublicPreview))
                return MenuItems.Document;

            if (controllerName.Equals(MVC.Draft.Name))
                return MenuItems.Project;

            if (controllerName.Equals(MVC.Finalized.Name))
                return MenuItems.Project;

            if (controllerName.Equals(MVC.Account.Name) && actionName.Equals(MVC.Account.ActionNames.ProfileEdit))
                return MenuItems.Profile;

            if (controllerName.Equals(MVC.Feedback.Name) && actionName.Equals(MVC.Feedback.ActionNames.Index))
                return MenuItems.Feedback;

            if (controllerName.Equals(MVC.Help.Name))
                return MenuItems.Help;

            if (controllerName.Equals(MVC.Submit.Name))
                return MenuItems.Submit;

            if (controllerName.Equals(MVC.Registered.Name) || controllerName.Equals(MVC.Message.Name))
                return MenuItems.Registered;

            if (controllerName.Equals(MVC.Project.Name))
                if (isAuthenticated)
                    return MenuItems.Project;
                else 
                    return MenuItems.Document;

            if (controllerName.Equals(MVC.Report.List.Name)
                    || controllerName.Equals(MVC.Report.BFPContract.Name)
                    || controllerName.Equals(MVC.Report.Package.Name)
                    || controllerName.Equals(MVC.Report.Communication.Name)
                    || controllerName.Equals(MVC.Report.AccessCode.Name))
            {
                return MenuItems.Contract;
            }

            if (controllerName.Equals(MVC.Offers.Name))
                return MenuItems.Offers;

            return MenuItems.Other;
        }
    }
}