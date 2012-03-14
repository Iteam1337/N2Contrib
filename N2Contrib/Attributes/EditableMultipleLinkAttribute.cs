using System;
using System.Web.UI;
using N2;
using N2.Details;
using System.Web.UI.WebControls;
using N2.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace N2Contrib.Attributes
{
    /// <summary>
    /// Defines n editable links to other items on this site. The items are 
    /// selected through a popup window displaying the item tree.
    /// </summary>
    /// <example>
    ///	[EditableMultipleLink("Feed items", 90)]
    ///	public virtual ContentItem Feeds { get; set; }
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public class EditableMultipleLinkAttribute : AbstractEditableAttribute, IDisplayable
    {
        public EditableMultipleLinkAttribute() 
            : this(null, 100)
		{}

        public EditableMultipleLinkAttribute(string title, int sortOrder) 
            : base(title, sortOrder)
		{}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        protected override Control AddEditor(Control container)
        {
            // Add a placeholder to wrap our controls
            var holder = new PlaceHolder();
            holder.ID = Name;
            container.Controls.Add(holder);
            
            return holder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="editor"></param>
        public override void UpdateEditor(ContentItem item, Control editor)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="editor"></param>
        /// <returns></returns>
        public override bool UpdateItem(ContentItem item, Control editor)
        {
            return false;
        }
    }
}
