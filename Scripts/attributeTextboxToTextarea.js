//$('html').hide(); // Start out hidden to prevent the user from seeing the elements transform.

function pageLoad(){
//  $(window).one("load", function () {
//    // Run the JavaScript in the Edit btn's href. This shows the editing controls.
//    eval($("a[id*='lbEdit']").attr("href")); 
//  });
  $input = $("input[id*='tbAttr']");
  // Create an empty textarea.
  $textarea = $("<textarea></textarea>").attr({
    // Copy attributes from textbox to new textarea.
    id: $input.attr("id"),
    name: $input.attr("name"),
    class: $input.attr("class"),
    style: "width: 500px; height: 300px;",
    tabindex: $input.attr("tabindex")
  });
  $input.after($textarea).remove(); // Add new textarea after textbox and remove textbox.

  // Create a label for the checkbox.
  $checkbox = $("input[id*='cbAttr']");
  $cbLabel = $("<label></label>").attr({
    "for": $checkbox.attr("id")
  }).text("Yes. Johnson Ferry may share my testimony publicly.");
  $checkbox.after($cbLabel).delay(3000); // Add the label after the checkbox.
};

//function showHtml() {
//  $('html').show(); // Show the page.
//};

//// Add a slight delay before showing the page, to allow the jquery changes to be made invisibly.
//window.setTimeout(showHtml, 1200, true);  // won't pass "true" to the showHtml in IE

//// Increase compatibility with unnamed functions
//setTimeout(function () {
//  generateOutput(true);
//}, 1000);  // will work with every browser