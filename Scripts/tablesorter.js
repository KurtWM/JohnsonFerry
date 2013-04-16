$(document).ready(function () {
  $(".tablesorter").each(function (index) {
    $(this).tablesorter({ sortList: [[1, 0], [0, 0]] });
  });
}); 