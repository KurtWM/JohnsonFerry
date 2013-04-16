$(document).ready(function () {
  //remove single spaces from within TD tags so that CSS can correctly handle those empty TDs.
  //array of all the td elements
  var tdArray = $('table.GridBox tr td');
  //iterate the array
  tdArray.each(function () {
    var tdText = $(this).text();
    if (tdText == " ") {
      $(this).html($.trim(tdText));
    }
  });
});