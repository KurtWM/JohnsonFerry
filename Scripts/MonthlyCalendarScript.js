$(document).ready(function() {
    $('.calendarTable tr td').each(function() {

        var hiddenElements = $('ul.calendarItemList li:gt(4)', this).hide();

        if (hiddenElements.size() > 0) {
            var showCaption = '...Show ' + hiddenElements.size() + ' More Events';
            $('ul.calendarItemList', this).append(
              $('<li class="toggler">' + showCaption + '</li>')
                  .toggle(
                      function() {
                          hiddenElements.show();
                          $(this).text('...Show Fewer Events');
                      },
                      function() {
                          hiddenElements.hide();
                          $(this).text(showCaption);
                          $('html,body').animate({ scrollTop: $(this).parent().offset().top - 24 }, { duration: 'slow', easing: 'swing' });
                      }
                  )
          );
        }
    });
});
