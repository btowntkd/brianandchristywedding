using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrianChristyWedding
{
    public static class HtmlHelperExtensions
    {
        public static string ActiveController(this HtmlHelper helper, string controller, string classStyle)
        {
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (currentController == controller)
            {
                return classStyle;
            }

            return "";
        }
    }
}