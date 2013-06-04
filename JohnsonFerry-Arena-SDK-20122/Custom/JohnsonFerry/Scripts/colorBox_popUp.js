/*
cbPopup - Creates a popup iframe when an appropriate item is clicked.
by Kurt Meredith.
Requires the ColorBox v1.3.20.1 - jQuery lightbox plugin by Jack Moore - jacklmoore.com.

The HTML tag that will be clickable must contain the following attributes:
class             (at least one class, "cbPopup" is required)
title             (optional: a title to appear underneath the iframe content)
data-src          (the source URL of the content to include in the iframe)
data-width        (pixel width of content area)
data-height       (pixel height of content area)
data-frameborder  (pixel width of frame)
data-extra        (optional: any additional content neede for the iframe (i.e.: webkitAllowFullScreen mozallowfullscreen allowFullScreen))
*/

var cssId = 'colorboxCss';
//Check to see if colorbox.css is currently loaded and load it if it is not.
if (!document.getElementById(cssId)) {
  var head = document.getElementsByTagName('head')[0];
  var link = document.createElement('link');
  link.id = cssId;
  link.rel = 'stylesheet';
  link.type = 'text/css';
  link.href = '/Custom/JohnsonFerry/CSS/colorbox.css';
  link.media = 'all';
  head.appendChild(link);
}

$.getScript("/Custom/JohnsonFerry/Scripts/jquery.colorbox-min.js", function () {
  $(".cbPopup").each(function () {
    $(this).colorbox({
      innerWidth: $(this).attr('data-width'),
      innerHeight: $(this).attr('data-height'),
      html: "<iframe src='" + $(this).attr('data-src') + "' width='" + $(this).attr('data-width') + "' height='" + $(this).attr('data-height') + "' frameborder='" + $(this).attr('data-frameborder') + "' " + $(this).attr('data-extra') + "></iframe>"
    });
  });
});

/*
HTML Format Examples:
<a class="cbPopup" 
title="Elevate Introduction Video" 
href="#" 
data-src="http://player.vimeo.com/video/49341848?title=0&amp;byline=0&amp;portrait=0&amp;autoplay=1" 
data-width="640" data-height="360" 
data-frameborder="0" 
data-extra="webkitAllowFullScreen mozallowfullscreen allowFullScreen">
<img alt="" border="0" 
src="http://www.johnsonferry.org/Portals/0/assets/newsEvents/images/Elevate_BryantSummaryVideo_200px.jpg" 
width="200" 
height="145" 
style="margin-right: 12px; float: left; -webkit-box-shadow: 4px 6px 6px 0px rgba(0, 0, 0, 0.3); box-shadow: 4px 6px 6px 0px rgba(0, 0, 0, 0.3);" />
</a>

<a class="cbPopup" 
title="Kitten Test Image" 
href="#" 
data-src="http://placekitten.com/640/360" 
data-width="640" 
data-height="360" 
data-frameborder="0" 
data-extra="webkitAllowFullScreen mozallowfullscreen allowFullScreen">
<img alt="" border="0" 
src="http://placekitten.com/200/145" 
width="200" 
height="145" 
style="margin-right: 12px; float: left; -webkit-box-shadow: 4px 6px 6px 0px rgba(0, 0, 0, 0.3); box-shadow: 4px 6px 6px 0px rgba(0, 0, 0, 0.3);" />
</a>
*/


