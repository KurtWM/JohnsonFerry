$(document).ready(function() {
	$("a.facebook").contents().before("<img src='/Custom/JohnsonFerry/Images/facebook_16px.gif' title='Facebook' class='Facebook Icon' style='position: relative; top: 3px; margin-right: 2px;' />");
	$("a.twitter").contents().before("<img src='/Custom/JohnsonFerry/Images/twitter_16px.gif' title='Twitter' class='Twitter Icon' style='position: relative; top: 3px; margin-right: 2px;' />");
	$("a.email").contents().before("<img src='/Custom/JohnsonFerry/Images/email_16px.gif' title='Email' class='Email Icon' style='position: relative; top: 3px; margin-right: 2px;' />");
	$("a:contains('Add To Your Calendar')").contents().before("<img src='/Custom/JohnsonFerry/Images/calendar_16px.gif' title='Add To Your Calendar' class='Email Icon' style='position: relative; top: 3px; margin-right: 2px;' />");
	$(".registrationTitle").not(".registrationDiscountCode").contents().wrap('<h1 />');
	$(".registrationEventTitle").contents().wrap('<h1 />');
	$(".registrationHeading").contents().wrap('<h2 />');
	$(".registrationContent").contents().wrap('<p />');
	$(".registrationDetails table tr td.registrationLabelRequired").contents().after("<span style='color: Red;'>*</span>");
	
	// Add a 'btnStyle' class to every input with a type of 'submit'
	$('input[type|="Submit"]').each(function(index) {
	    $(this).addClass('btnStyle');
	});
	$('input[type|="button"]').each(function(index) {
	    $(this).addClass('btnStyle');
	});
	$('a.button').each(function(index) {
	    $(this).addClass('btnStyle');
	});
	
	
	
	});