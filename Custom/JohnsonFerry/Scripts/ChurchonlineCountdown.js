// nextUrl   = The URL that returns a JSON string containing the countdown timer data.
//             The JSON data should be in the format:
//             ({"next_timestamp":1374409800,
//               "next_duration":3600,
//               "next_title":"Title text",
//               "next_description":"Description text"})
// timerId   = A text string that will be used as the ID attribute for the countdown timer.
// container = A jQuery selector string for the DOM element where the countdown timer will be placed.
//             If the container does not exist, the countdown timer will be placed in the body of the page.
$(countdowntimer = function (nextUrl, timerId, container) {
  // If no timerId then don't create countdown timer.
  if (typeof timerId != 'undefined') {
    var days;
    var hours;
    var minutes;
    var seconds;
    var intervalId;
    // Write HTML for countdown timer elements.
    var counterCode = '<div id="' + timerId + '" class="churchonline-countdown" style="display: none;">\n';
    counterCode = counterCode + '\t<div class="live">Live Now</div>\n';
    counterCode = counterCode + '\t<div class="info">\n';
    counterCode = counterCode + '\t\t<div class="title"></div>\n';
    counterCode = counterCode + '\t\t<div class="description"></div>\n';
    counterCode = counterCode + '\t</div>\n';
    counterCode = counterCode + '\t<ul class="time">\n';
    counterCode = counterCode + '\t\t<li><span class="days"></span>\n';
    counterCode = counterCode + '\t\t\t<span class="label">days</span>\n';
    counterCode = counterCode + '\t\t</li>\n';
    counterCode = counterCode + '\t\t<li><span class="hours"></span>\n';
    counterCode = counterCode + '\t\t\t<span class="label">hrs</span>\n';
    counterCode = counterCode + '\t\t</li>\n';
    counterCode = counterCode + '\t\t<li><span class="minutes"></span>\n';
    counterCode = counterCode + '\t\t\t<span class="label">mins</span>\n';
    counterCode = counterCode + '\t\t</li>\n';
    counterCode = counterCode + '\t\t<li><span class="seconds"></span>\n';
    counterCode = counterCode + '\t\t\t<span class="label">secs</span>\n';
    counterCode = counterCode + '</li>\n';
    counterCode = counterCode + '\t</ul>\n';
    counterCode = counterCode + '</div>\n';

    if ($(container).length > 0) {
      // The container element exists--add the counter to the element. 
      $(container).prepend(counterCode);
    } else {
      // The container element does not exist--add the counter to the body.
      $('body').prepend(counterCode);
    }

    function goLive() {
      $('#' + timerId + ' .live').show();
      $('#' + timerId + ' .time, #' + timerId + ' .info').hide();
    }

    $.ajax({
      url: nextUrl,
      dataType: "jsonp",
      success: function (data) {
        $('#' + timerId).show();

        if (typeof (data.current_timestamp) !== "undefined") {
          goLive();
        } else if (typeof (data.next_timestamp) !== "undefined") {
          $('#' + timerId + ' .title').html(data.next_title);
          $('#' + timerId + ' .description').html(data.next_description);

          var seconds_till = data.next_timestamp - (new Date().getTime() / 1000);

          days = Math.floor(seconds_till / 86400);
          hours = Math.floor((seconds_till % 86400) / 3600);
          minutes = Math.floor((seconds_till % 3600) / 60);
          seconds = Math.floor(seconds_till % 60);

          intervalId = setInterval(function () {
            if (--seconds < 0) {
              seconds = 59;
              if (--minutes < 0) {
                minutes = 59;
                if (--hours < 0) {
                  hours = 23;
                  if (--days < 0) {
                    days = 0;
                  }
                }
              }
            }

            $('#' + timerId + ' .days').html((days.toString().length < 2) ? "0" + days : days);
            $('#' + timerId + ' .hours').html((hours.toString().length < 2) ? "0" + hours : hours);
            $('#' + timerId + ' .minutes').html((minutes.toString().length < 2) ? "0" + minutes : minutes);
            $('#' + timerId + ' .seconds').html((seconds.toString().length < 2) ? "0" + seconds : seconds);

            if (seconds === 0 && minutes === 0 && hours === 0 && days === 0) {
              goLive();
              clearInterval(intervalId);
            }
          }, 1000);
        }
      }
    });
    return countdowntimer;
  }
});