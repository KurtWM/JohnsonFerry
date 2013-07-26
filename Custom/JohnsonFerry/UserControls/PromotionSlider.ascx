<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionSlider.ascx.cs" Inherits="ArenaWeb.Custom.JohnsonFerry.UserControls.PromotionSlider" %>

<!-- If the jQuery Cycle plugin is not loaded then try to load it from the CDN. -->
<script type="text/javascript">
  if (typeof $.fn.cycle === 'undefined') {
    document.write('<script src="http://malsup.github.io/jquery.cycle.all.js" type="text/javascript"><\/script>');
  }
</script>
<!-- Check once more to see if the jQuery Cycle plugin loaded successfully. If not, then load it from server. -->
<script type="text/javascript">
  $.fn.cycle || document.write('<script src="/Custom/JohnsonFerry/Scripts/jquery.cycle.all.js" type="text/javascript"><\/script>')
</script>

<asp:PlaceHolder ID="phSlider" runat="server"></asp:PlaceHolder>

