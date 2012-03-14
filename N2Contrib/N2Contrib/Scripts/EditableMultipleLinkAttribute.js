
(function($) {

  /**
   * Constructor for Editable Multiple Link
   */
  function EditableMultipleLink(el) {
    this.el = $(el);
  }

  /**
   * Functions
   */
  EditableMultipleLink.prototype = {
    addLink: function() {
    },

    defineLink: function() {
    }
  };


  $.fn.extend({
    /**
     * Define the Plugin
     */
    n2editablemultiplelink: function() {
      return this.each(function() {
        new EditableMultipleLink(this);
      });
    }
  });

})(jQuery);