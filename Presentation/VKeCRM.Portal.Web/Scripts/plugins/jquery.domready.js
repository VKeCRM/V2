(function($) {

$.event.special.domready = {
	setup: function() {
		// Make sure the ready event is setup
		bindDOMReady();
		return;
	},
	
	teardown: function() { return; }
};

jQuery.fn.extend({
	domready: function(f) {
		// Attach the listeners
		bindDOMReady();

		// If the DOM is already ready
		if ( jQuery.isDOMReady )
			// Execute the function immediately
			f.apply( document, [jQuery] );

		// Otherwise, remember the function for later
		else
			// Add the function to the wait list
			jQuery.domreadyList.push( function() { return f.apply(this, [jQuery]); } );

		return this;
	}
});

jQuery.extend({
	/*
	 * All the code that makes DOM Ready work nicely.
	 */
	isDOMReady: false,
	domreadyList: [],

	// Handle when the DOM is ready
	domready: function() {
		// Make sure that the DOM is not already loaded
		if ( !jQuery.isDOMReady ) {
			// Remember that the DOM is ready
			jQuery.isDOMReady = true;
			
			// If there are functions bound, to execute
			if ( jQuery.domreadyList ) {
				// Execute all of them
				jQuery.each( jQuery.domreadyList, function(){
					this.apply( document );
				});
				
				// Reset the list of functions
				jQuery.domreadyList = null;
			}
			
			// Trigger any bound domready events
			jQuery(document).triggerHandler("domready");
			
			// Remove event listener to avoid memory leak
			if ( document.removeEventListener )
				document.removeEventListener( "DOMContentLoaded", jQuery.domready, false );
			
			// Remove script element used by IE hack
			if( !window.frames.length ) // don't remove if frames are present (#1187)
				jQuery(window).load(function(){ jQuery("#__ie_init").remove(); });
		}
	}
});

var domreadyBound = false;

function bindDOMReady(){
	if ( domreadyBound ) return;
	domreadyBound = true;

	if ( document.addEventListener )
		// Use the handy event callback
		document.addEventListener( "DOMContentLoaded", jQuery.domready, false );
	
	// If IE is used, use the excellent hack by Matthias Miller
	// http://www.outofhanwell.com/blog/index.php?title=the_window_onload_problem_revisited
	if ( jQuery.browser.msie ) {
	
		// Only works if you document.write() it
		document.write("<scr" + "ipt id=__ie_init defer=true " + 
			"src=//:><\/script>");
	
		// Use the defer script hack
		var script = document.getElementById("__ie_init");
		
		// script does not exist if jQuery is loaded dynamically
		if ( script ) 
			script.onreadystatechange = function() {
				if ( this.readyState != "complete" ) return;
				jQuery.domready();
			};
	
		// Clear from memory
		script = null;
	
	// If Safari is used
	} else if ( jQuery.browser.safari )
		// Continually check to see if the document.readyState is valid
		jQuery.safariTimer = setInterval(function(){
			// loaded and complete are both valid states
			if ( document.readyState == "loaded" || 
				document.readyState == "complete" ) {
	
				// If either one are found, remove the timer
				clearInterval( jQuery.safariTimer );
				jQuery.safariTimer = null;
	
				// and execute any waiting functions
				jQuery.domready();
			}
		}, 10); 

	// A fallback to window.onload, that will always work
	jQuery.event.add( window, "load", jQuery.domready );
}

})(jQuery);