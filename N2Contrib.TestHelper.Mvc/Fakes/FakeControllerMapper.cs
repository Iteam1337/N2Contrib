using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Web.Mvc;

namespace N2Contrib.TestHelper.Mvc.Fakes
{
    public class FakeControllerMapper : IControllerMapper
    {
		public Func<string, string, bool> HasActionFunc;

        public bool ControllerHasAction(string controllerName, string actionName)
        {
			if (HasActionFunc == null)
				throw new NotImplementedException("Set the HasActionFunc");
            return HasActionFunc(controllerName, actionName);
        }

		public string controllerName;

        public string GetControllerName(Type type)
	    {
            return controllerName;
        }


        public bool IsContentController(string controllerName)
        {
            throw new NotImplementedException();
        }
    }
}
