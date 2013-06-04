$(document).ready(function() {
    $('.myRegistrations h1 a').css({ 'font': 'inherit' });
    $('.myMissions h1 a').css({ 'font': 'inherit' });
    $('.myGroupsDetail h1 a').css({ 'font': 'inherit' });
    $('.myGroups h1 a').css({ 'font': 'inherit' });

    $('.myServingDetail h1').each(function() {
        $(this).replaceWith("<h3>" + $(this).html() + "</h3>");
    });

    $('.myRegistrationsDetail span').each(function() {
        $(this).replaceWith($(this).html());
    });

    $('.myRegistrationsDetail h1').each(function() {
        $(this).replaceWith("<h3>" + $(this).html() + "</h3>");
    });

    $('.myMissions h1').each(function() {
        $(this).replaceWith("<h3>" + $(this).html() + "</h3>");
    });

    $('.myGroupsDetail h1').each(function() {
        $(this).replaceWith("<h3>" + $(this).html() + "</h3>");
    });

    $('.myGroups h1').each(function() {
        $(this).replaceWith("<h3>" + $(this).html() + "</h3>");
    });
});
