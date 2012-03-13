using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Details;

namespace N2Contrib.Attributes
{
    class EditableMultipleLink : AbstractEditableAttribute
    {
        protected override System.Web.UI.Control AddEditor(System.Web.UI.Control container)
        {
            throw new NotImplementedException();
        }

        public override void UpdateEditor(N2.ContentItem item, System.Web.UI.Control editor)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateItem(N2.ContentItem item, System.Web.UI.Control editor)
        {
            throw new NotImplementedException();
        }
    }
}
