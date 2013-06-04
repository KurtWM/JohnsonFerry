$(document).ready(function() {
    $('table.module').css({ 'width': '100%', 'margin-top': '0px' });
    $('table.module img').css({ 'vertical-align': 'text-top', 'margin-right': '12px' });
    $('table.module tr td table').css({ 'margin-top': '12px' });
    $('table.module tr td table tr').each(function(index) {
        $(this).children(":first").css({ 'width': '47%' });
        if ($(this).children(":first").children().size() <= 0) {
            $(this).children(":first").append('<div style="height: 120px; vertical-align: center; text-align: center; color: silver;"><img src="/images/generic_unknown_adult.jpg" style="vertical-align: text-top; border: solid 1px silver;"></img></div>');
        }
    });

});
