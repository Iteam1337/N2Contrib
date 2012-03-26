using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Web.Mvc;

namespace N2Contrib.Tests.Mvc3
{
    public class FakeControllerMapper : IControllerMapper
    {
        public bool ControllerHasAction(string controllerName, string actionName)
        {
            throw new NotImplementedException();
        }

        public string GetControllerName(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
