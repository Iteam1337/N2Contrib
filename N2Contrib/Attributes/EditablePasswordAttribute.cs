using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Details;

namespace N2Contrib.Attributes
{
	public class EditablePasswordAttribute : EditableTextBoxAttribute
	{
		protected override System.Web.UI.WebControls.TextBox CreateEditor()
		{
			var textbox = base.CreateEditor();
			textbox.Attributes["type"] = "password";
			return textbox;
		}
	}
}
