$(document).ready(function () {
  $("textarea").each(function (i) {
    if ($(this).attr('maxlength')) {
      var maxLen = $(this).attr('maxlength');
      var countClass = 'counter' + i;
      $(this).after('<div style="font-size: 0.8em;" class="' + countClass + '">' + maxLen + ' characters remaining</div>');
      $(this).keyup(function () {
        //alert('test');
        var box = $(this).val();
        var count = maxLen - box.length;

        if (box.length <= maxLen) {
          $('.' + countClass).html(count + ' characters remaining');
        } else {
          alert(' Sorry. No more characters may be entered in that text box. ');
        }
        return false;
      });
    }
  });
});