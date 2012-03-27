using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;

namespace N2Contrib.TestHelper
{
    /// <summary>
    /// Extensions for Content Item
    /// </summary>
    public static class ContentItemExtensions
    {
        /// <summary>
        /// Adds the provided content items as children to 
        /// this content item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static T AddChildren<T>(this T item, params ContentItem [] children)
            where T : ContentItem
        {
            if (children == null)
                throw new ArgumentNullException("children");

            foreach (var child in children) 
            {  
                child.Parent = item;
                item.Children.Add(child);
            }

            return item;
        }
    }
}
