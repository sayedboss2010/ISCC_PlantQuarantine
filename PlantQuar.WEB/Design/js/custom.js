$(function(){


	// This is the file where you call all other Plugins.		
	// List of all available options can be found in help file in "D) JavaScript (jQuery)" section.
	
	
	$('.main-navigation').animatedHorizontalSubmenu();
	

	// Advanced Example 1
	// -------------------------------
	/*
	$('.main-navigation').animatedHorizontalSubmenu({
		submenuStaysOpen:false,
		animation:2
	});
	*/
	
	
	// Advanced Example 2
	// -------------------------------
	/*
	$('.main-navigation').animatedHorizontalSubmenu({
		mouseHover:false,
		animation:3,
		animationSpeed:260
	});
	*/


	// Advanced Example 3
	// -------------------------------
	/*
	$('.main-navigation').animatedHorizontalSubmenu({
		animation:4,
		animationSpeed:250,
		animationTiming:'ease-in-out',
		animationDelay:20
	});
	*/
	
	
});