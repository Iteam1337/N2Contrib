using System;
using System.Web.UI;
using N2;
using N2.Details;

namespace N2Contrib.Attributes
{
    /// <summary>
    /// Defines n editable links to other items on this site. The items are 
    /// selected through a popup window displaying the item tree.
    /// </summary>
    /// <example>
    ///		[EditableMultipleLink("Feed items", 90)]
    ///		public virtual ContentItem Feeds { get; set; }
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public class EditableMultipleLinkAttribute : AbstractEditableAttribute, IRelativityTransformer
    {
        public EditableMultipleLinkAttribute() : this(null, 100)
		{}

        public EditableMultipleLinkAttribute(string title, int sortOrder) : base(title, sortOrder)
		{}

        protected override Control AddEditor(Control container)
        {
            throw new NotImplementedException();
        }

        public override void UpdateEditor(ContentItem item, Control editor)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateItem(ContentItem item, Control editor)
        {
            throw new NotImplementedException();
        }

        public string Rebase(string value, string fromAppPath, string toAppPath)
        {
            return null;
        }

        public RelativityMode RelativeWhen { get; set; }
    }
}
