/**
 * Main JS file for Casper behaviours
 */

/*globals jQuery, document */
(function ($) {
    "use strict";

    $(document).ready(function(){

        $(".post-content").fitVids();
        var bgIndex = Math.floor(Math.random() * 5);

        var colors = [
        	'#db552d', // orange
        	'#5937b3', // purple
        	'#bb1d49', // red
        	'#2c86ee', // blue
        	'#000000', // black
        ];

        $("#author_box").css({
        	'background-color': colors[bgIndex]
        });
    });

	hljs.configure({
		languages: ['cs']
    });
	
	hljs.initHighlightingOnLoad();
}(jQuery));